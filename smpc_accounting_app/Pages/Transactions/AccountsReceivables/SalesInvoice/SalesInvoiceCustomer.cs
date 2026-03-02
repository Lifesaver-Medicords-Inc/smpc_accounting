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

namespace smpc_accounting_app.Pages.Transactions.AccountsReceivables.SalesInvoice
{
    public partial class SalesInvoiceCustomer : Form
    {
        public DataTable SelectedCustomer { get; private set; } = null;
        private string placeHolderText = "Customer Search...";
        GeneralService<CustomerViewModel> serviceSetup;
        private DataTable customerTable;

        public SalesInvoiceCustomer()
        {
            InitializeComponent();

            // Center the modal relative to its parent form
            this.StartPosition = FormStartPosition.CenterParent;

            dgv_customer_search.AutoGenerateColumns = false;
            InitializeSearchBox();

            Helpers.DataGridViewFormatter.DataGridViewDecimalFormat(dgv_customer_search, new[] { "overpayment_amount" });
        }

        private void InitializeSearchBox()
        {
            txt_search = Helpers.CreateSearchBox(placeHolderText, txt_search_TextChanged);
            this.Controls.Add(txt_search);
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            if (customerTable == null || customerTable.Rows.Count == 0)
                return;

            string searchText = txt_search.Text.Trim();

            if (string.IsNullOrEmpty(searchText) || searchText == placeHolderText)
            {
                dgv_customer_search.DataSource = customerTable;
            }
            else
            {
                var searchedData = Helpers.FilterDataTable(customerTable, searchText,
                    "customer_code", "customer", "tax_code", "payment_term", "customer_address", "overpayment_amount");
                dgv_customer_search.DataSource = searchedData;
            }
        }

        private async void SalesInvoiceCustomer_Load(object sender, EventArgs e)
        {
            try
            {
                Helpers.Loading.ShowLoading(dgv_customer_search, "Fetching data...");
                await LoadCustomer();
            }
            catch (Exception ex)
            {
                Helpers.ShowDialogMessage("error", $"Failed to load: {ex.Message}");
            }
            finally
            {
                Helpers.Loading.HideLoading(dgv_customer_search);
            }
        }

        private async Task LoadCustomer()
        {
            serviceSetup = new GeneralService<CustomerViewModel>(ApiEndPoints.CUSTOMER_VIEW);
            var data = await serviceSetup.GetAsDatatable();

            customerTable = data;

            if (customerTable?.Rows.Count > 0)
            {
                dgv_customer_search.DataSource = customerTable;
            }
            else
            {
                dgv_customer_search.DataSource = null;
                Helpers.ShowDialogMessage("error", "No customer found.");
            }
        }

        private void dgv_customer_search_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            var row = dgv_customer_search.Rows[e.RowIndex];

            // Create a new DataTable
            DataTable selectedTable = new DataTable();

            // Create columns based on DataGridView columns
            foreach (DataGridViewColumn col in dgv_customer_search.Columns)
            {
                selectedTable.Columns.Add(col.DataPropertyName ?? col.Name);
            }

            // Create new row
            DataRow dataRow = selectedTable.NewRow();

            // Copy values from the selected DataGridView row
            foreach (DataGridViewColumn col in dgv_customer_search.Columns)
            {
                string columnName = col.DataPropertyName ?? col.Name;
                dataRow[columnName] = row.Cells[col.Index].Value ?? DBNull.Value;
            }

            selectedTable.Rows.Add(dataRow);

            // Assign to property
            SelectedCustomer = selectedTable;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
