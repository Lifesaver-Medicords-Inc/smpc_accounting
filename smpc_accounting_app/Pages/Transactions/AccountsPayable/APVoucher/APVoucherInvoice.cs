using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using smpc_accounting_app.Models;
using smpc_accounting_app.Services.Transactions;
using smpc_accounting_app.Services.Helpers;
using smpc_accounting_app.Services;
using smpc_accounting_app.Shared;

namespace smpc_accounting_app.Pages.Transactions.AccountsPayable.APVoucher
{
    public partial class APVoucherInvoice : Form
    {
        private string placeHolderText = "Invoice Receipt Search...";
        private List<InvoiceViewModel> InvoiceReceipt;
        GeneralService<InvoiceViewModel> generalServiceInvoiceView;
        private DataTable irTable;
        public DataTable SelectedIR { get; private set; } = null;
        public DataTable ExistingIRs { get; set; }
        public int SupplierId { get; set; }

        public APVoucherInvoice()
        {
            InitializeComponent();

            // Center the modal relative to its parent form
            this.StartPosition = FormStartPosition.CenterParent;
            Helpers.DataGridViewFormatter.DataGridViewDecimalFormat(dgv_ir_search, new[] { "line_amount" });

            dgv_ir_search.AutoGenerateColumns = false;
            InitializeSearchBox();

        }

        private void InitializeSearchBox()
        {
            txt_search = Helpers.CreateSearchBox(placeHolderText, txt_search_TextChanged);
            this.Controls.Add(txt_search);
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            if (irTable == null || irTable.Rows.Count == 0)
                return;

            string searchText = txt_search.Text.Trim();

            if (string.IsNullOrEmpty(searchText) || searchText == placeHolderText)
            {
                dgv_ir_search.DataSource = irTable;
            }
            else
            {
                var searchedData = Helpers.FilterDataTable(irTable, searchText,
                    "receipt_no", "ir_doc_date", "line_amount", "receipt_type");
                dgv_ir_search.DataSource = searchedData;
            }
        }

        private async void APVoucherInvoice_Load(object sender, EventArgs e)
        {
            try
            {
                Helpers.Loading.ShowLoading(dgv_ir_search, "Fetching data...");
                await InvoiceReceipts();
            }
            catch (Exception ex)
            {
                Helpers.ShowDialogMessage("error", $"Failed to load: {ex.Message}");
            }
            finally
            {
                Helpers.Loading.HideLoading(dgv_ir_search);
            }
        }

        private async Task InvoiceReceipts()
        {
            generalServiceInvoiceView = new GeneralService<InvoiceViewModel>(ApiEndPoints.AP_VOUCHER_INVOICE + SupplierId);
            InvoiceReceipt = await generalServiceInvoiceView.GetAsList();
            InvoiceReceipt.Reverse();

            // Convert journal entry list to DataTable using helper
            irTable = Helpers.ToDataTable(InvoiceReceipt);

            // If there are existing IRs, exclude them
            if (ExistingIRs != null && ExistingIRs.Rows.Count > 0)
            {
                var usedIds = ExistingIRs.AsEnumerable()
                    .Where(r => r["invoice_receipt_id"] != DBNull.Value)
                    .Select(r => Convert.ToInt32(r["invoice_receipt_id"]))
                    .ToHashSet();

                var filteredRows = irTable.AsEnumerable()
                    .Where(r => !usedIds.Contains(Convert.ToInt32(r["invoice_receipt_id"])));

                irTable = filteredRows.Any()
                    ? filteredRows.CopyToDataTable()
                    : irTable.Clone(); // empty table with same structure
            }

            if (irTable?.Rows.Count > 0)
            {
                dgv_ir_search.DataSource = irTable;
            }
            else
            {
                dgv_ir_search.DataSource = null;
                Helpers.ShowDialogMessage("error", "No invoice receipt found.");
                this.Close();
            }
        }

        private void dgv_ir_search_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ignore header clicks
            if (e.RowIndex < 0)
                return;

            // Initialize SelectedIR if not yet created
            if (SelectedIR == null)
            {
                SelectedIR = irTable.Clone(); // copies column structure only
            }
            else
            {
                SelectedIR.Rows.Clear(); // optional: ensure only 1 selected row
            }

            // Get selected row from DataGridView
            DataRowView rowView = dgv_ir_search.Rows[e.RowIndex].DataBoundItem as DataRowView;

            if (rowView != null)
            {
                SelectedIR.ImportRow(rowView.Row);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
