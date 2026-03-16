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

namespace smpc_accounting_app.Pages.Transactions.AccountsPayable.BulkInvoiceReceipt.BulkInvoiceReceiptModals
{
    public partial class BulkInvoiceSearch : Form
    {
        public string SelectedBIRId { get; private set; } = null;
        private string placeHolderText = "Bulk Invoice Receipt Search...";
        private BulkInvoiceReceiptList BulkInvoiceReceipt;
        readonly BulkInvoiceReceiptService bulkinvoiceReceiptService = new BulkInvoiceReceiptService();
        private DataTable birTable;
        public BulkInvoiceSearch()
        {
            InitializeComponent();

            // Center the modal relative to its parent form
            this.StartPosition = FormStartPosition.CenterParent;

            dgv_ir_search.AutoGenerateColumns = false;
            Helpers.DataGridViewDocumentFormatter.DataGridViewDocumentFormat(dgv_ir_search, "doc_no", "IR");
            Helpers.DataGridViewFormatter.DataGridViewDecimalFormat(dgv_ir_search, new[] { "net_amount" });
            InitializeSearchBox();
        }

        private void InitializeSearchBox()
        {
            txt_search = Helpers.CreateSearchBox(placeHolderText, txt_search_TextChanged);
            this.Controls.Add(txt_search);
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            if (birTable == null || birTable.Rows.Count == 0)
                return;

            string searchText = txt_search.Text.Trim();

            if (string.IsNullOrEmpty(searchText) || searchText == placeHolderText)
            {
                dgv_ir_search.DataSource = birTable;
            }
            else
            {
                var searchedData = Helpers.FilterDataTable(birTable, searchText,
                    "supplier", "supplier_code", "tax_code", "invoice_due", "doc_no", "doc_date", "net_amount");
                dgv_ir_search.DataSource = searchedData;
            }
        }

        private async void BulkInvoiceSearch_Load(object sender, EventArgs e)
        {
            try
            {
                Helpers.Loading.ShowLoading(dgv_ir_search, "Fetching data...");
                await BulkInvoiceReceipts();
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

        private async Task BulkInvoiceReceipts()
        {
            BulkInvoiceReceipt = await bulkinvoiceReceiptService.GetAsModel();

            BulkInvoiceReceipt.bulk_invoice_receipt.Reverse();

            // Convert journal entry list to DataTable using helper
            birTable = Helpers.ToDataTable(BulkInvoiceReceipt.bulk_invoice_receipt);

            if (birTable?.Rows.Count > 0)
            {
                dgv_ir_search.DataSource = birTable;
            }
            else
            {
                dgv_ir_search.DataSource = null;
                Helpers.ShowDialogMessage("error", "No bulk invoice receipt found.");
            }
        }

        private void dgv_ir_search_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            var row = dgv_ir_search.Rows[e.RowIndex];

            // Always get the id value from the row, regardless of which column was clicked
            var idValue = row.Cells["id"].Value;

            if (idValue != null)
            {
                SelectedBIRId = idValue.ToString();

                this.DialogResult = DialogResult.OK; // close the modal with OK
                this.Close();
            }
        }
    }
}
