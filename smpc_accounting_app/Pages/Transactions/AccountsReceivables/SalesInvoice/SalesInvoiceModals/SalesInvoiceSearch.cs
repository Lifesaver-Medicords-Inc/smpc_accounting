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

namespace smpc_accounting_app.Pages.Transactions.AccountsReceivables.SalesInvoice.SalesInvoiceModals
{
    public partial class SalesInvoiceSearch : Form
    {
        public string SelectedSIId { get; private set; } = null;
        private string placeHolderText = "Sales Invoice Search...";
        private SalesInvoice2List SalesInvoice;
        readonly SalesInvoiceService2 salesInvoiceService = new SalesInvoiceService2();
        private DataTable siTable;

        public SalesInvoiceSearch()
        {
            InitializeComponent();

            // Center the modal relative to its parent form
            this.StartPosition = FormStartPosition.CenterParent;

            dgv_si_search.AutoGenerateColumns = false;
            Helpers.DataGridViewDocumentFormatter.DataGridViewDocumentFormat(dgv_si_search, "doc_no", "SI");
            InitializeSearchBox();
        }

        private void InitializeSearchBox()
        {
            txt_search = Helpers.CreateSearchBox(placeHolderText, txt_search_TextChanged);
            this.Controls.Add(txt_search);
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            if (siTable == null || siTable.Rows.Count == 0)
                return;

            string searchText = txt_search.Text.Trim();

            if (string.IsNullOrEmpty(searchText) || searchText == placeHolderText)
            {
                dgv_si_search.DataSource = siTable;
            }
            else
            {
                var searchedData = Helpers.FilterDataTable(siTable, searchText,
                    "customer", "customer_code", "tax_code", "journal", "customer_address", "doc_no", "doc_date", "posting_date");
                dgv_si_search.DataSource = searchedData;
            }
        }

        private async void SalesInvoiceSearch_Load(object sender, EventArgs e)
        {
            try
            {
                Helpers.Loading.ShowLoading(dgv_si_search, "Fetching data...");
                await SalesInvoices();
            }
            catch (Exception ex)
            {
                Helpers.ShowDialogMessage("error", $"Failed to load: {ex.Message}");
            }
            finally
            {
                Helpers.Loading.HideLoading(dgv_si_search);
            }
        }

        private async Task SalesInvoices()
        {
            SalesInvoice = await salesInvoiceService.GetAsModel();

            SalesInvoice.sales_invoice2.Reverse();

            // Convert journal entry list to DataTable using helper
            siTable = Helpers.ToDataTable(SalesInvoice.sales_invoice2);

            if (siTable?.Rows.Count > 0)
            {
                dgv_si_search.DataSource = siTable;
            }
            else
            {
                dgv_si_search.DataSource = null;
                Helpers.ShowDialogMessage("error", "No sales invoice found.");
            }
        }

        private void dgv_si_search_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            var row = dgv_si_search.Rows[e.RowIndex];

            // Always get the id value from the row, regardless of which column was clicked
            var idValue = row.Cells["id"].Value;

            if (idValue != null)
            {
                SelectedSIId = idValue.ToString();

                this.DialogResult = DialogResult.OK; // close the modal with OK
                this.Close();
            }
        }
    }
}
