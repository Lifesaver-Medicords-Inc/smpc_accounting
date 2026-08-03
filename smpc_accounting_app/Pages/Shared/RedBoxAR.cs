using smpc_accounting_app.Models;
using smpc_accounting_app.Services;
using smpc_accounting_app.Shared;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace smpc_accounting_app.Pages.Shared
{
    // "RED BOX" - Accounts Receivable performance-metrics panel, mounted inside
    // Layout's permanent right-side red panel (panel5, below the "RED BOX" title
    // and its divider - see Layout.Designer.cs). Same idea as the RED BOX
    // dashboards already shipped in the Sales/Purchasing/Engineering/Dispatching
    // apps: everything shown here is DERIVED (read-only) from existing data -
    // there is no new "red box" table, and nothing here writes anything back.
    //
    // Source data & business rules (per the FOR A/R mockup notes):
    //  - One card per open (not-yet-fully-paid) Sales Invoice. The list is built
    //    by calling GET /accounting/customer for every customer, then GET
    //    /accounting/payment_receipt/sales_invoice/{customer_id} per customer -
    //    the same endpoint PaymentReceiptPage already uses to look up a
    //    customer's open invoices. That endpoint's stored procedure
    //    (sp_GetSIPaymentReceipt) already HAVING-filters to open_amount > 0, so
    //    "remove if balance = 0" (fully paid invoices) is enforced server-side;
    //    the client-side check below is just a defensive backstop.
    //  - PAYMENT DUE is that endpoint's due_date column as-is (server computes
    //    it from si.posting_date) - "kailan magbabayad si client".
    //  - BALANCE is that endpoint's open_amount - "kung magkano pa babayaran".
    //  - DOCUMENT NO. is the invoice's doc_no, shown as "SI#..." - only invoices
    //    with an open balance ever reach this list, which is exactly the
    //    mockup's "SI na incomplete pa yung payment" condition.
    //  - Sorted soonest PAYMENT DUE first ("arranged according to closest client
    //    due"); invoices with an unparsable due date sink to the bottom.
    //
    // Known gap (confirmed all the way down to the DB views - vw_get_customer
    // and sp_GetSalesOrderInvoice - neither one carries these columns today):
    // PROJECT NAME and CLIENT CONTACT NO. are NOT available anywhere in the
    // Accounting module's current data model, so both are shown as "-" rather
    // than guessed at. Populating them for real would need a schema/API change
    // (e.g. exposing BPI's branch contact number and the originating Sales
    // Order's project name through vw_get_customer / sp_GetSIPaymentReceipt).
    public partial class RedBoxAR : UserControl
    {
        // Auto-refresh every 5 minutes so the balances stay current without the
        // user having to click Refresh, matching the convention already used by
        // smpc_sales_system's RedBox control.
        private readonly Timer _autoRefreshTimer = new Timer { Interval = 5 * 60 * 1000 };

        public RedBoxAR()
        {
            InitializeComponent();
            _autoRefreshTimer.Tick += async (s, e) => await LoadData();
            this.Disposed += (s, e) => _autoRefreshTimer.Dispose();
        }

        private class ArEntry
        {
            public string ClientName;
            public string ProjectName;
            public DateTime? PaymentDueDate;
            public decimal Balance;
            public string DocumentNoDisplay;
            public string ContactNo;
        }

        // Deliberately NOT wired to this control's own Load event - RedBoxAR is
        // mounted inside Layout's permanent panel5, which is constructed before
        // the modal Login dialog resolves. Layout.cs calls RefreshData()
        // explicitly once login has actually succeeded.
        public async Task RefreshData()
        {
            await LoadData();

            if (!_autoRefreshTimer.Enabled)
                _autoRefreshTimer.Start();
        }

        private async void btn_refresh_Click(object sender, EventArgs e)
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            btn_refresh.Enabled = false;
            lbl_status.Text = "Loading...";
            try
            {
                var entries = await BuildArEntries();
                RenderArSection(entries);

                lbl_status.Text = $"{DateTime.Now:h:mm tt} - {entries.Count} open invoice(s)";
            }
            catch (Exception ex)
            {
                lbl_status.Text = "Failed to load: " + ex.Message;
            }
            finally
            {
                btn_refresh.Enabled = true;
            }
        }

        // ------------------------------------------------------------------
        // Data
        // ------------------------------------------------------------------

        private async Task<List<ArEntry>> BuildArEntries()
        {
            var result = new List<ArEntry>();

            var customerService = new GeneralService<CustomerViewModel>(ApiEndPoints.CUSTOMER_VIEW);
            List<CustomerViewModel> customers;
            try
            {
                customers = await customerService.GetAsList();
            }
            catch
            {
                customers = new List<CustomerViewModel>();
            }

            if (customers.Count == 0)
                return result;

            var invoiceTasks = customers.Select(c => GetOpenInvoicesForCustomer(c.customer_id)).ToList();
            var invoiceResults = await Task.WhenAll(invoiceTasks);

            for (int i = 0; i < customers.Count; i++)
            {
                var customer = customers[i];
                foreach (var invoice in invoiceResults[i])
                {
                    // Defensive - sp_GetSIPaymentReceipt already HAVING-filters
                    // to open_amount > 0, this just guards against drift.
                    if (invoice.open_amount <= 0)
                        continue;

                    DateTime? dueDate = null;
                    if (DateTime.TryParse(invoice.due_date, out var parsedDue))
                        dueDate = parsedDue;

                    result.Add(new ArEntry
                    {
                        ClientName = string.IsNullOrWhiteSpace(customer.customer) ? "-" : customer.customer,
                        ProjectName = "-", // not available anywhere in the current data model - see class comment
                        PaymentDueDate = dueDate,
                        Balance = (decimal)invoice.open_amount,
                        DocumentNoDisplay = EnsurePrefix(invoice.doc_no, "SI#"),
                        ContactNo = "-" // not available anywhere in the current data model - see class comment
                    });
                }
            }

            return result.OrderBy(en => en.PaymentDueDate ?? DateTime.MaxValue).ToList();
        }

        // A customer with no open invoices makes sp_GetSIPaymentReceipt return
        // an empty result set, which the API surfaces as a 404 ("no sales
        // invoice found") - that is the normal, expected outcome for a
        // fully-paid-up customer, not a real error, so it's swallowed here
        // exactly like PaymentReceiptPage.btn_customer_Click already does.
        private async Task<List<SalesInvoiceReceiptView>> GetOpenInvoicesForCustomer(int customerId)
        {
            try
            {
                var service = new GeneralService<SalesInvoiceReceiptView>(ApiEndPoints.SALES_INVOICE_RECEIPT + customerId);
                return await service.GetAsList();
            }
            catch
            {
                return new List<SalesInvoiceReceiptView>();
            }
        }

        // ------------------------------------------------------------------
        // Rendering
        // ------------------------------------------------------------------

        private void RenderArSection(List<ArEntry> entries)
        {
            pnl_cards.SuspendLayout();
            pnl_cards.Controls.Clear();
            if (entries.Count == 0)
            {
                pnl_cards.Controls.Add(MakeEmptyLabel("No open receivables right now."));
            }
            else
            {
                foreach (var entry in entries)
                    pnl_cards.Controls.Add(BuildArCard(entry));
            }
            pnl_cards.ResumeLayout();
        }

        // Narrow (~300px) panel - fields laid out 2-per-row exactly like the
        // mockup, built out of nested FlowLayoutPanels (card -> row -> field
        // block), matching the pattern already proven in smpc_sales_system's
        // and smpc_dispatching's RedBox controls.
        private static readonly Color CardBackColor = Color.MistyRose;
        private static readonly Color HeaderColor = Color.FromArgb(150, 20, 20);
        private static readonly int CardWidth = 264;
        private static readonly int CardColumnWidth = (CardWidth - 16) / 2 - 4;

        private FlowLayoutPanel BuildArCard(ArEntry entry)
        {
            var card = StartCard();

            AddFieldRow(card, "CLIENT NAME", MakeValueLabel(entry.ClientName), "PROJECT NAME", MakeValueLabel(entry.ProjectName));
            AddFieldRow(card, "PAYMENT DUE", MakeValueLabel(entry.PaymentDueDate?.ToString("M/d/yy") ?? "-"),
                "BALANCE", MakeValueLabel(entry.Balance.ToString("C2", CultureInfo.GetCultureInfo("en-PH"))));
            AddFieldRow(card, "DOCUMENT NO.", MakeValueLabel(entry.DocumentNoDisplay), "CLIENT CONTACT NO.", MakeValueLabel(entry.ContactNo));

            return card;
        }

        private FlowLayoutPanel StartCard()
        {
            return new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                BackColor = CardBackColor,
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(8),
                Margin = new Padding(4),
                MinimumSize = new Size(CardWidth, 0),
                MaximumSize = new Size(CardWidth, 0)
            };
        }

        // One row = up to two field blocks placed side by side (LeftToRight flow).
        private void AddFieldRow(FlowLayoutPanel card, string header1, Control value1, string header2 = null, Control value2 = null)
        {
            var row = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Margin = new Padding(0, 0, 0, 4)
            };
            row.Controls.Add(BuildFieldBlock(header1, value1));
            if (header2 != null)
                row.Controls.Add(BuildFieldBlock(header2, value2));

            card.Controls.Add(row);
        }

        private FlowLayoutPanel BuildFieldBlock(string header, Control valueControl)
        {
            var block = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                MinimumSize = new Size(CardColumnWidth, 0),
                MaximumSize = new Size(CardColumnWidth, 0),
                Margin = new Padding(0, 0, 4, 0),
                Padding = new Padding(0)
            };

            var lbl = new Label
            {
                Text = header,
                AutoSize = true,
                Font = new Font("Segoe UI", 7F, FontStyle.Bold),
                ForeColor = HeaderColor,
                Margin = new Padding(0)
            };
            valueControl.Margin = new Padding(0);
            valueControl.MaximumSize = new Size(CardColumnWidth, 0);

            block.Controls.Add(lbl);
            block.Controls.Add(valueControl);
            return block;
        }

        private Label MakeValueLabel(string text)
        {
            return new Label
            {
                Text = string.IsNullOrWhiteSpace(text) ? "-" : text,
                AutoSize = true,
                Font = new Font("Segoe UI", 9F)
            };
        }

        private Label MakeEmptyLabel(string text)
        {
            return new Label
            {
                Text = text,
                AutoSize = true,
                Margin = new Padding(10),
                ForeColor = Color.Gray,
                Font = new Font("Segoe UI", 9F, FontStyle.Italic)
            };
        }

        private static string EnsurePrefix(string raw, string prefix)
        {
            if (string.IsNullOrWhiteSpace(raw))
                return "-";
            return raw.StartsWith(prefix, StringComparison.OrdinalIgnoreCase) ? raw : prefix + raw;
        }
    }
}
