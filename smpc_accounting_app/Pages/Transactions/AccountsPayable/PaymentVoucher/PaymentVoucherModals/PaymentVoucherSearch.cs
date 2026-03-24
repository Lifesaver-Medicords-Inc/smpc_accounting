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

namespace smpc_accounting_app.Pages.Transactions.AccountsPayable.PaymentVoucher.PaymentVoucherModals
{
    public partial class PaymentVoucherSearch : Form
    {
        public string SelectedPVId { get; private set; } = null;
        private string placeHolderText = "Payment Voucher Search...";
        private PaymentVoucherList PaymentVoucher;
        readonly PaymentVoucherService paymentVoucherService = new PaymentVoucherService();
        private DataTable pvTable;

        public PaymentVoucherSearch()
        {
            InitializeComponent();

            // Center the modal relative to its parent form
            this.StartPosition = FormStartPosition.CenterParent;

            dgv_pv_search.AutoGenerateColumns = false;
            InitializeSearchBox();
            Helpers.DataGridViewFormatter.DataGridViewDecimalFormat(dgv_pv_search, new[] { "transaction_amount" });
            Helpers.DataGridViewDocumentFormatter.DataGridViewDocumentFormat(dgv_pv_search, "doc_no", "PV");
        }

        private void InitializeSearchBox()
        {
            txt_search = Helpers.CreateSearchBox(placeHolderText, txt_search_TextChanged);
            this.Controls.Add(txt_search);
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            if (pvTable == null || pvTable.Rows.Count == 0)
                return;

            string searchText = txt_search.Text.Trim();

            if (string.IsNullOrEmpty(searchText) || searchText == placeHolderText)
            {
                dgv_pv_search.DataSource = pvTable;
            }
            else
            {
                var searchedData = Helpers.FilterDataTable(pvTable, searchText,
                    "supplier", "supplier_code", "reference_apv", "currency", "doc_no", "doc_date", "transaction_amount");
                dgv_pv_search.DataSource = searchedData;
            }
        }

        private async void PaymentVoucherSearch_Load(object sender, EventArgs e)
        {
            try
            {
                Helpers.Loading.ShowLoading(dgv_pv_search, "Fetching data...");
                await PaymentVouchers();
            }
            catch (Exception ex)
            {
                Helpers.ShowDialogMessage("error", $"Failed to load: {ex.Message}");
            }
            finally
            {
                Helpers.Loading.HideLoading(dgv_pv_search);
            }
        }

        private async Task PaymentVouchers()
        {
            try
            {
                PaymentVoucher = await paymentVoucherService.GetAsModel();

                // Convert journal entry list to DataTable using helper
                pvTable = Helpers.ToDataTable(PaymentVoucher.payment_voucher);

                if (pvTable?.Rows.Count > 0)
                {
                    dgv_pv_search.DataSource = pvTable;
                }
                else
                {
                    dgv_pv_search.DataSource = null;
                    Helpers.ShowDialogMessage("error", "No payment voucher found.");
                }
            }
            catch (NullReferenceException)
            {
                Helpers.ShowDialogMessage("error", "Nopayment voucher found.");
            }
            catch (Exception ex)
            {
                Helpers.ShowDialogMessage("error", $"Failed to load: {ex.Message}");
            }
        }

        private void dgv_pv_search_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            var row = dgv_pv_search.Rows[e.RowIndex];

            // Always get the id value from the row, regardless of which column was clicked
            var idValue = row.Cells["id"].Value;

            if (idValue != null)
            {
                SelectedPVId = idValue.ToString();

                this.DialogResult = DialogResult.OK; // close the modal with OK
                this.Close();
            }
        }
    }
}
