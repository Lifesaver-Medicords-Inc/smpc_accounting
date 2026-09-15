using smpc_accounting_app.Models;
using smpc_accounting_app.Services;
using smpc_accounting_app.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace smpc_accounting_app.Pages.Transactions.AccountsReceivables.ARRecords
{
    // A/R Record of Transactions (spec 12.3). One row per Sales Invoice, tagged
    // PAID or ONGOING, with the balance the invoice still carries after the
    // payments applied to it.
    //
    // NEXT DUE is the only editable field: A/R types it, because no setup says
    // how many days a payment term means (12.3). It is saved as it is typed.
    public partial class ARRecordOfTransactionsPage : UserControl
    {
        private List<ARRecordModel> _rows = new List<ARRecordModel>();

        public ARRecordOfTransactionsPage()
        {
            InitializeComponent();
            BuildColumns();

            btn_refresh.Click += async (s, e) => await LoadAsync();
            btn_print.Click += btn_print_Click;
            txt_search.TextChanged += (s, e) => Bind();
            dgv_list.CellEndEdit += dgv_list_CellEndEdit;
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await LoadAsync();
        }

        private void BuildColumns()
        {
            dgv_list.AutoGenerateColumns = false;
            dgv_list.Columns.Clear();
            dgv_list.Columns.Add(Column("tag", "TAG", true));
            dgv_list.Columns.Add(Column("customer", "COMPANY NAME", true));
            dgv_list.Columns.Add(Column("sales_invoice_no", "SALES INVOICE", true));
            dgv_list.Columns.Add(Column("so_no", "SO", true));
            dgv_list.Columns.Add(Money("balance", "BALANCE"));
            dgv_list.Columns.Add(Column("payment_term", "PAYMENT TERMS", true));

            // The one field A/R fills in.
            var nextDue = Column("next_due", "NEXT DUE", false);
            nextDue.ToolTipText = "Typed by A/R - saved as soon as you leave the cell.";
            dgv_list.Columns.Add(nextDue);
        }

        private static DataGridViewTextBoxColumn Column(string property, string header, bool readOnly)
        {
            return new DataGridViewTextBoxColumn
            {
                Name = property,
                DataPropertyName = property,
                HeaderText = header,
                ReadOnly = readOnly,
                SortMode = DataGridViewColumnSortMode.Automatic,
            };
        }

        private static DataGridViewTextBoxColumn Money(string property, string header)
        {
            var column = Column(property, header, true);
            column.DefaultCellStyle.Format = "N2";
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            return column;
        }

        private async System.Threading.Tasks.Task LoadAsync()
        {
            var service = new GeneralService<ARRecordModel>(ApiEndPoints.AR_RECORDS);
            _rows = await service.GetAsList() ?? new List<ARRecordModel>();
            Bind();
        }

        private void Bind()
        {
            string term = (txt_search.Text ?? "").Trim();
            IEnumerable<ARRecordModel> rows = _rows;

            if (term.Length > 0)
            {
                rows = rows.Where(r =>
                    Has(r.customer, term) || Has(r.sales_invoice_no, term) ||
                    Has(r.so_no, term) || Has(r.tag, term));
            }

            dgv_list.DataSource = new BindingList<ARRecordModel>(rows.ToList());
        }

        private static bool Has(string text, string term)
        {
            return text != null && text.IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        // Saves NEXT DUE for the invoice on that row. Nothing else on the row is
        // editable, so there is no other cell to save.
        private async void dgv_list_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgv_list.Columns[e.ColumnIndex].Name != "next_due") return;

            var row = dgv_list.Rows[e.RowIndex].DataBoundItem as ARRecordModel;
            if (row == null) return;

            var service = new GeneralService<ARNextDueModel>(ApiEndPoints.AR_RECORDS_NEXT_DUE);
            var result = await service.Update(new ARNextDueModel
            {
                sales_invoice_id = row.sales_invoice_id,
                next_due = row.next_due ?? "",
            });

            if (result == null || !result.Success)
            {
                MessageBox.Show(result?.message ?? "Saving the due date failed.", "Next Due",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                await LoadAsync();
            }
        }

        // Prints the list as it stands, on the house template (spec 2.10).
        private void btn_print_Click(object sender, EventArgs e)
        {
            var report = smpc_accounting_app.Printing.HouseTemplateReport.FromGrid(
                dgv_list, "A/R RECORD OF TRANSACTIONS");

            if (report.Rows.Count == 0)
            {
                MessageBox.Show("Nothing to print.", "Print", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            report.ShowPreview();
        }
    }
}
