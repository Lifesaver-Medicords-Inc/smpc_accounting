using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using smpc_accounting_app.Services;
using smpc_accounting_app.Models;
using smpc_accounting_app.Services.Helpers;
using smpc_accounting_app.Shared;

namespace smpc_accounting_app.Pages.Transactions.AccountsPayable.InvoiceReceipt.InvoiceReceiptModals
{
    public partial class InvoiceSearchSupplier : Form
    {
        public DataTable SelectedSupplier { get; private set; } = null;
        private string _placeHolderText = "Supplier Search...";
        GeneralService<SupplierTradeViewModel> serviceSetup;
        private DataTable _supplierTable;

        public InvoiceSearchSupplier()
        {
            InitializeComponent();

            // Center the modal relative to its parent form
            this.StartPosition = FormStartPosition.CenterParent;
            Helpers.DataGridViewFormatter.DataGridViewDecimalFormat(dgv_suplier_search, new[] { "overpayment_amount" });

            dgv_suplier_search.AutoGenerateColumns = false;
            InitializeSearchBox();
        }

        private void InitializeSearchBox()
        {
            txt_search = Helpers.CreateSearchBox(_placeHolderText, txt_search_TextChanged);
            this.Controls.Add(txt_search);
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            if (_supplierTable == null || _supplierTable.Rows.Count == 0)
                return;

            string searchText = txt_search.Text.Trim();

            if (string.IsNullOrEmpty(searchText) || searchText == _placeHolderText)
            {
                dgv_suplier_search.DataSource = _supplierTable;
            }
            else
            {
                var searchedData = Helpers.FilterDataTable(_supplierTable, searchText, 
                    "supplier_code", "supplier", "invoice_type", "payment_term", "type", "overpayment_amount", "supplier_address");
                dgv_suplier_search.DataSource = searchedData;
            }
        }

        private async void InvoiceSearchSupplier_Load(object sender, EventArgs e)
        {
            try
            {
                Helpers.Loading.ShowLoading(dgv_suplier_search, "Fetching data...");
                await LoadSupplier();
            }
            catch (Exception ex)
            {
                Helpers.ShowDialogMessage("error", $"Failed to load: {ex.Message}");
            }
            finally
            {
                Helpers.Loading.HideLoading(dgv_suplier_search);
            }
        }

        private async Task LoadSupplier()
        {
            try
            {
                serviceSetup = new GeneralService<SupplierTradeViewModel>(ApiEndPoints.INVOICE_RECEIPT_SUPPLIER);
                var data = await serviceSetup.GetAsDatatable();

                _supplierTable = data;

                if (_supplierTable?.Rows.Count > 0)
                {
                    dgv_suplier_search.DataSource = _supplierTable;
                }
                else
                {
                    dgv_suplier_search.DataSource = null;
                    Helpers.ShowDialogMessage("error", "No supplier found.");
                }
            }
            catch (NullReferenceException)
            {
                Helpers.ShowDialogMessage("error", "No supplier found.");
            }
            catch (Exception ex)
            {
                Helpers.ShowDialogMessage("error", $"Failed to load: {ex.Message}");
            }
        }

        private void dgv_suplier_search_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            var row = dgv_suplier_search.Rows[e.RowIndex];

            // Create a new DataTable
            DataTable selectedTable = new DataTable();

            // Create columns based on DataGridView columns
            foreach (DataGridViewColumn col in dgv_suplier_search.Columns)
            {
                selectedTable.Columns.Add(col.DataPropertyName ?? col.Name);
            }

            // Create new row
            DataRow dataRow = selectedTable.NewRow();

            // Copy values from the selected DataGridView row
            foreach (DataGridViewColumn col in dgv_suplier_search.Columns)
            {
                string columnName = col.DataPropertyName ?? col.Name;
                dataRow[columnName] = row.Cells[col.Index].Value ?? DBNull.Value;
            }

            selectedTable.Rows.Add(dataRow);

            // Assign to property
            SelectedSupplier = selectedTable;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
