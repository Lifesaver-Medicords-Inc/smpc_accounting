using smpc_accounting_app.Models;
using smpc_accounting_app.Services;
using smpc_accounting_app.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace smpc_accounting_app.Pages.Transactions.AccountsPayable.APRecords
{
    // A/P Record of Transactions (spec 3.2's A/P module list), the mirror of the
    // A/R screen in 12.3. One row per Invoice Receipt - trade and bulk alike,
    // since A/P owns both doors into the payable (5.14, 5.15) - tagged PAID or
    // ONGOING, with what the receipt still carries after everything applied
    // against it.
    //
    // Read-only, and deliberately so. A/R's twin has one editable field, NEXT DUE,
    // because A/R types it. The A/P counterpart is INVOICE DUE, which is typed on
    // the Invoice Receipt itself and carried forward unchanged to the APV and the
    // PV (5.14, invariant 18) - editing it here would let a due date diverge from
    // the document every downstream voucher reads it off.
    //
    // BALANCE is computed server-side at read time and never stored (12.9).
    public partial class APRecordOfTransactionsPage : UserControl
    {
        private List<APRecordModel> _rows = new List<APRecordModel>();

        public APRecordOfTransactionsPage()
        {
            InitializeComponent();
            BuildColumns();

            btn_refresh.Click += async (s, e) => await LoadAsync();
            btn_print.Click += btn_print_Click;
            txt_search.TextChanged += (s, e) => Bind();
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await LoadAsync();
        }

        private void BuildColumns()
        {
            dgv_list.AutoGenerateColumns = false;
            dgv_list.ReadOnly = true;
            dgv_list.Columns.Clear();
            dgv_list.Columns.Add(Column("tag", "TAG"));
            dgv_list.Columns.Add(Column("supplier", "SUPPLIER NAME"));
            dgv_list.Columns.Add(Column("invoice_receipt_no", "INVOICE RECEIPT"));
            // Trade and non-trade sit in one list, so the row has to say which it
            // is - the two are separate documents with separate numbering.
            dgv_list.Columns.Add(Column("receipt_type", "TYPE"));
            dgv_list.Columns.Add(Column("reference_po", "REFERENCE"));
            dgv_list.Columns.Add(Money("net_amount", "TRANS AMOUNT"));
            dgv_list.Columns.Add(Money("balance", "BALANCE"));
            dgv_list.Columns.Add(Column("payment_term", "PAYMENT TERMS"));
            dgv_list.Columns.Add(Column("invoice_due", "INVOICE DUE"));
        }

        private static DataGridViewTextBoxColumn Column(string property, string header)
        {
            return new DataGridViewTextBoxColumn
            {
                Name = property,
                DataPropertyName = property,
                HeaderText = header,
                ReadOnly = true,
                SortMode = DataGridViewColumnSortMode.Automatic,
            };
        }

        private static DataGridViewTextBoxColumn Money(string property, string header)
        {
            var column = Column(property, header);
            column.DefaultCellStyle.Format = "N2";
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            return column;
        }

        private async System.Threading.Tasks.Task LoadAsync()
        {
            var service = new GeneralService<APRecordModel>(ApiEndPoints.AP_RECORDS);
            _rows = await service.GetAsList() ?? new List<APRecordModel>();
            Bind();
        }

        private void Bind()
        {
            string term = (txt_search.Text ?? "").Trim();
            IEnumerable<APRecordModel> rows = _rows;

            if (term.Length > 0)
            {
                rows = rows.Where(r =>
                    Has(r.supplier, term) || Has(r.invoice_receipt_no, term) ||
                    Has(r.reference_po, term) || Has(r.tag, term));
            }

            dgv_list.DataSource = new BindingList<APRecordModel>(rows.ToList());
        }

        private static bool Has(string text, string term)
        {
            return text != null && text.IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        // Prints the list as it stands, on the house template (spec 2.10).
        private void btn_print_Click(object sender, EventArgs e)
        {
            var report = smpc_accounting_app.Printing.HouseTemplateReport.FromGrid(
                dgv_list, "A/P RECORD OF TRANSACTIONS");

            if (report.Rows.Count == 0)
            {
                MessageBox.Show("Nothing to print.", "Print", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            report.ShowPreview();
        }
    }
}
