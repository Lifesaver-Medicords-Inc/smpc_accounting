using smpc_accounting_app.Models;
using smpc_accounting_app.Services;
using smpc_accounting_app.Services.Helpers;
using smpc_accounting_app.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace smpc_accounting_app.Pages.Transactions.AccountsReceivables.Billing
{
    // The Billing list (spec 12.5), keyed on the SO. A row sits in ACTIVE until
    // its BALANCE reaches zero - never on the first Official Receipt, however
    // many a partly-paid SO already carries (14.37) - and then in CLOSED, where
    // every OR recorded against it is listed with its date and amount.
    //
    // Below the list sits the SO's transaction history (12.4): the hand-keyed
    // ledger accounting types itself, and the only place a deposit appears. It
    // is not the down-payment feature, which is out of scope (15).
    public partial class BillingListPage : UserControl
    {
        // The types the ledger uses (spec 12.4). BEGINING BALANCE and ADDITIONAL
        // are positive; CLIENT PAY is entered negative.
        private static readonly string[] TransactionTypes = { "BEGINING BALANCE", "CLIENT PAY", "ADDITIONAL" };

        private List<BillingRowModel> _rows = new List<BillingRowModel>();
        private BindingList<SoBillingTransactionModel> _ledger = new BindingList<SoBillingTransactionModel>();
        private string _selectedSo = "";

        public BillingListPage()
        {
            InitializeComponent();
            BuildColumns();

            btn_refresh.Click += async (s, e) => await LoadAsync();
            btn_print.Click += btn_print_Click;
            txt_search.TextChanged += (s, e) => Bind();
            dgv_active.SelectionChanged += async (s, e) => await SelectionChanged(dgv_active);
            dgv_closed.SelectionChanged += async (s, e) => await SelectionChanged(dgv_closed);
            btn_add_row.Click += btn_add_row_Click;
            btn_save_rows.Click += btn_save_rows_Click;
            btn_delete_row.Click += btn_delete_row_Click;
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await LoadAsync();
        }

        private void BuildColumns()
        {
            foreach (var grid in new[] { dgv_active, dgv_closed })
            {
                grid.AutoGenerateColumns = false;
                grid.Columns.Clear();
                grid.Columns.Add(Text("date", "DATE"));
                grid.Columns.Add(Text("so_no", "SO"));
                grid.Columns.Add(Text("project_name", "PROJECT NAME"));
                grid.Columns.Add(Text("sales_executive", "SALES EXECUTIVE"));
                grid.Columns.Add(Money("total_due", "TOTAL DUE"));
                grid.Columns.Add(Money("adjustments", "ADJUSTMENTS"));
                grid.Columns.Add(Money("paid", "PAID"));
                grid.Columns.Add(Money("balance", "BALANCE"));
                grid.Columns.Add(Text("next_due", "NEXT DUE"));
            }

            dgv_receipts.AutoGenerateColumns = false;
            dgv_receipts.Columns.Clear();
            dgv_receipts.Columns.Add(Text("or_no", "OFFICIAL RECEIPT"));
            dgv_receipts.Columns.Add(Text("date", "DATE"));
            dgv_receipts.Columns.Add(Money("amount", "AMOUNT"));

            dgv_ledger.AutoGenerateColumns = false;
            dgv_ledger.Columns.Clear();
            dgv_ledger.Columns.Add(Text("date", "DATE"));
            var type = new DataGridViewComboBoxColumn
            {
                Name = "transaction_type",
                DataPropertyName = "transaction_type",
                HeaderText = "TRANSACTION TYPE",
                FlatStyle = FlatStyle.Flat,
            };
            type.Items.AddRange(TransactionTypes);
            dgv_ledger.Columns.Add(type);
            dgv_ledger.Columns.Add(Text("description", "DESCRIPTION"));
            dgv_ledger.Columns.Add(Money("amount", "AMOUNT"));
            dgv_ledger.Columns.Add(Text("reference", "TRANSACTION REFERENCE"));
            dgv_ledger.Columns.Add(Text("remarks", "REMARKS"));
        }

        private static DataGridViewTextBoxColumn Text(string property, string header)
        {
            return new DataGridViewTextBoxColumn
            {
                Name = property,
                DataPropertyName = property,
                HeaderText = header,
                SortMode = DataGridViewColumnSortMode.Automatic,
            };
        }

        private static DataGridViewTextBoxColumn Money(string property, string header)
        {
            var column = Text(property, header);
            column.DefaultCellStyle.Format = "N2";
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            return column;
        }

        private async System.Threading.Tasks.Task LoadAsync()
        {
            var service = new GeneralService<BillingRowModel>(ApiEndPoints.BILLING);
            _rows = await service.GetAsList() ?? new List<BillingRowModel>();
            Bind();
        }

        private void Bind()
        {
            string term = (txt_search.Text ?? "").Trim();
            IEnumerable<BillingRowModel> rows = _rows;

            if (term.Length > 0)
            {
                rows = rows.Where(r => Has(r.so_no, term) || Has(r.project_name, term)
                                    || Has(r.customer, term) || Has(r.sales_executive, term));
            }

            var all = rows.ToList();
            dgv_active.DataSource = new BindingList<BillingRowModel>(all.Where(r => r.status == "ACTIVE").ToList());
            dgv_closed.DataSource = new BindingList<BillingRowModel>(all.Where(r => r.status == "CLOSED").ToList());
        }

        private static bool Has(string text, string term)
        {
            return text != null && text.IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private async System.Threading.Tasks.Task SelectionChanged(DataGridView grid)
        {
            var row = grid.CurrentRow?.DataBoundItem as BillingRowModel;
            if (row == null || row.so_no == _selectedSo) return;

            _selectedSo = row.so_no ?? "";
            lbl_ledger_so.Text = "   " + (_selectedSo.Length == 0 ? "(no SO selected)" : _selectedSo);
            dgv_receipts.DataSource = new BindingList<BillingReceiptModel>(row.receipts ?? new List<BillingReceiptModel>());
            await LoadLedgerAsync();
        }

        private async System.Threading.Tasks.Task LoadLedgerAsync()
        {
            if (_selectedSo.Length == 0)
            {
                _ledger = new BindingList<SoBillingTransactionModel>();
                dgv_ledger.DataSource = _ledger;
                return;
            }

            var service = new GeneralService<SoBillingTransactionModel>(
                ApiEndPoints.BILLING_TRANSACTIONS + "?so_no=" + Uri.EscapeDataString(_selectedSo));
            var rows = await service.GetAsList() ?? new List<SoBillingTransactionModel>();
            _ledger = new BindingList<SoBillingTransactionModel>(rows);
            dgv_ledger.DataSource = _ledger;
        }

        private void btn_add_row_Click(object sender, EventArgs e)
        {
            if (_selectedSo.Length == 0)
            {
                MessageBox.Show("Pick an SO first.", "Transaction History",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _ledger.Add(new SoBillingTransactionModel
            {
                so_no = _selectedSo,
                date = DateTime.Now.ToString("yyyy-MM-dd"),
                transaction_type = TransactionTypes[2],   // ADDITIONAL, the usual hand-keyed row
                amount = 0,
            });
        }

        // Saves every row: new ones are created, existing ones updated. The list
        // then reloads, so the balances above reflect what was typed.
        private async void btn_save_rows_Click(object sender, EventArgs e)
        {
            Helpers.Loading.ShowLoading(this);
            try
            {
                dgv_ledger.EndEdit();

                foreach (var row in _ledger.ToList())
                {
                    row.so_no = string.IsNullOrWhiteSpace(row.so_no) ? _selectedSo : row.so_no;
                    var service = new GeneralService<SoBillingTransactionModel>(ApiEndPoints.BILLING_TRANSACTIONS);
                    var result = row.id == 0 ? await service.Insert(row) : await service.Update(row);

                    if (result == null || !result.Success)
                    {
                        MessageBox.Show(result?.message ?? "Saving the transaction history failed.",
                            "Transaction History", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                await LoadLedgerAsync();
                await LoadAsync();
            }
            finally
            {
                Helpers.Loading.HideLoading(this);
            }
        }

        private async void btn_delete_row_Click(object sender, EventArgs e)
        {
            var row = dgv_ledger.CurrentRow?.DataBoundItem as SoBillingTransactionModel;
            if (row == null) return;

            if (MessageBox.Show("Delete this row?", "Transaction History",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            if (row.id == 0)
            {
                _ledger.Remove(row);
                return;
            }

            Helpers.Loading.ShowLoading(this);
            try
            {
                var service = new GeneralService<SoBillingTransactionModel>(
                    ApiEndPoints.BILLING_TRANSACTIONS + "/" + row.id);
                if (!await service.Delete(row))
                {
                    MessageBox.Show("Deleting the row failed.", "Transaction History",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                await LoadLedgerAsync();
                await LoadAsync();
            }
            finally
            {
                Helpers.Loading.HideLoading(this);
            }
        }

        // Prints the tab on screen, on the house template (spec 2.10).
        private void btn_print_Click(object sender, EventArgs e)
        {
            bool active = tab_status.SelectedTab == tab_active;
            var report = smpc_accounting_app.Printing.HouseTemplateReport.FromGrid(
                active ? dgv_active : dgv_closed, active ? "BILLING - ACTIVE" : "BILLING - CLOSED");

            if (report.Rows.Count == 0)
            {
                MessageBox.Show("Nothing to print.", "Print", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            report.ShowPreview();
        }
    }
}
