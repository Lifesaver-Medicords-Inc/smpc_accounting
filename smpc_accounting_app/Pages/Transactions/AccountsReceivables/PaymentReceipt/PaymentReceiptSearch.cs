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

namespace smpc_accounting_app.Pages.Transactions.AccountsReceivables.PaymentReceipt
{
    public partial class PaymentReceiptSearch : Form
    {
        public string SelectedPRId { get; private set; } = null;
        private string placeHolderText = "Payment Receipt Search...";
        private PaymentReceiptList PaymentReceipt;
        readonly PaymentReceiptService paymentReceiptService = new PaymentReceiptService();
        private DataTable prTable;

        public PaymentReceiptSearch()
        {
            InitializeComponent();

            // Center the modal relative to its parent form
            this.StartPosition = FormStartPosition.CenterParent;

            dgv_pr_search.AutoGenerateColumns = false;
            InitializeSearchBox();
        }

        private void InitializeSearchBox()
        {
            txt_search = Helpers.CreateSearchBox(placeHolderText, txt_search_TextChanged);
            this.Controls.Add(txt_search);
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            if (prTable == null || prTable.Rows.Count == 0)
                return;

            string searchText = txt_search.Text.Trim();

            if (string.IsNullOrEmpty(searchText) || searchText == placeHolderText)
            {
                dgv_pr_search.DataSource = prTable;
            }
            else
            {
                var searchedData = Helpers.FilterDataTable(prTable, searchText,
                    "customer", "customer_code", "reference_cr_no", "date_collect", "reference_or_no", "doc_no", "doc_date", "transaction_amount");
                dgv_pr_search.DataSource = searchedData;
            }
        }

        private async void PaymentReceiptSearch_Load(object sender, EventArgs e)
        {
            try
            {
                Helpers.Loading.ShowLoading(dgv_pr_search, "Fetching data...");
                await PaymentReceipts();
            }
            catch (Exception ex)
            {
                Helpers.ShowDialogMessage("error", $"Failed to load: {ex.Message}");
            }
            finally
            {
                Helpers.Loading.HideLoading(dgv_pr_search);
            }
        }

        private async Task PaymentReceipts()
        {
            PaymentReceipt = await paymentReceiptService.GetAsModel();

            PaymentReceipt.payment_receipt.Reverse();

            // Convert journal entry list to DataTable using helper
            prTable = Helpers.ToDataTable(PaymentReceipt.payment_receipt);

            if (prTable?.Rows.Count > 0)
            {
                dgv_pr_search.DataSource = prTable;
            }
            else
            {
                dgv_pr_search.DataSource = null;
                Helpers.ShowDialogMessage("error", "No payment receipt found.");
            }
        }

        private void dgv_pr_search_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            var row = dgv_pr_search.Rows[e.RowIndex];

            // Always get the id value from the row, regardless of which column was clicked
            var idValue = row.Cells["id"].Value;

            if (idValue != null)
            {
                SelectedPRId = idValue.ToString();

                this.DialogResult = DialogResult.OK; // close the modal with OK
                this.Close();
            }
        }
    }
}
