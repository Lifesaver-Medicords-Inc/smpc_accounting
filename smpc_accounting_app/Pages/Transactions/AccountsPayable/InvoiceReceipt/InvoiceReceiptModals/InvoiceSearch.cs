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

namespace smpc_accounting_app.Pages.Transactions.AccountsPayable.InvoiceReceipt.InvoiceReceiptModals
{
    public partial class InvoiceSearch : Form
    {
        public string SelectedIRId { get; private set; } = null;
        private string placeHolderText = "Invoice Receipt Search...";
        private InvoiceReceiptList InvoiceReceipt;
        readonly InvoiceReceiptService invoiceReceiptService = new InvoiceReceiptService();
        private DataTable irTable;
        public InvoiceSearch()
        {
            InitializeComponent();

            // Center the modal relative to its parent form
            this.StartPosition = FormStartPosition.CenterParent;

            dgv_ir_search.AutoGenerateColumns = false;
            InitializeSearchBox();
            Helpers.DataGridViewFormatter.DataGridViewDecimalFormat(dgv_ir_search, new[] { "net_amount" });
            Helpers.DataGridViewDocumentFormatter.DataGridViewDocumentFormat(dgv_ir_search, "doc_no", "IR");
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
                    "supplier", "supplier_code", "tax_code", "currency", "invoice_due", "doc_no", "doc_date", "net_amount");
                dgv_ir_search.DataSource = searchedData;
            }
        }

        private async void InvoiceReceiptSearch_Load(object sender, EventArgs e)
        {
            try
            {
                Helpers.Loading.ShowLoading(dgv_ir_search, "Fetching data...");
                await InvoiceReceipts();
            }
            catch (NullReferenceException)
            {
                Helpers.ShowDialogMessage("error", "No Invoice Receipt found.");
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
            InvoiceReceipt = await invoiceReceiptService.GetAsModel();

            // Convert journal entry list to DataTable using helper
            irTable = Helpers.ToDataTable(InvoiceReceipt.invoice_receipt);

            if (irTable?.Rows.Count > 0)
            {
                dgv_ir_search.DataSource = irTable;
            }
            else
            {
                dgv_ir_search.DataSource = null;
                Helpers.ShowDialogMessage("error", "No invoice receipt found.");
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
                SelectedIRId = idValue.ToString();

                this.DialogResult = DialogResult.OK; // close the modal with OK
                this.Close();
            }
        }
    }
}
