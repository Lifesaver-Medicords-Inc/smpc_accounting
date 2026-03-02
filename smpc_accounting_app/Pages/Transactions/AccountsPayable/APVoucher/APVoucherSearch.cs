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

namespace smpc_accounting_app.Pages.Transactions.AccountsPayable.APVoucher
{
    public partial class APVoucherSearch : Form
    {
        public string SelectedAVId { get; private set; } = null;
        private string placeHolderText = "AP Voucher Search...";
        private APVoucherList APVoucher;
        readonly APVoucherService apVoucherService = new APVoucherService();
        private DataTable avTable;

        public APVoucherSearch()
        {
            InitializeComponent();

            // Center the modal relative to its parent form
            this.StartPosition = FormStartPosition.CenterParent;

            dgv_av_search.AutoGenerateColumns = false;
            Helpers.DataGridViewFormatter.DataGridViewDecimalFormat(dgv_av_search, new[] { "transaction_amount" });
            InitializeSearchBox();
        }

        private void InitializeSearchBox()
        {
            txt_search = Helpers.CreateSearchBox(placeHolderText, txt_search_TextChanged);
            this.Controls.Add(txt_search);
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            if (avTable == null || avTable.Rows.Count == 0)
                return;

            string searchText = txt_search.Text.Trim();

            if (string.IsNullOrEmpty(searchText) || searchText == placeHolderText)
            {
                dgv_av_search.DataSource = avTable;
            }
            else
            {
                var searchedData = Helpers.FilterDataTable(avTable, searchText,
                    "supplier", "supplier_code", "currency", "invoice_due", "doc_no", "doc_date", "transaction_amount");
                dgv_av_search.DataSource = searchedData;
            }
        }

        private async void APVoucherSearch_Load(object sender, EventArgs e)
        {
            try
            {
                Helpers.Loading.ShowLoading(dgv_av_search, "Fetching data...");
                await APVouchers();
            }
            catch (Exception ex)
            {
                Helpers.ShowDialogMessage("error", $"Failed to load: {ex.Message}");
            }
            finally
            {
                Helpers.Loading.HideLoading(dgv_av_search);
            }
        }

        private async Task APVouchers()
        {
            APVoucher = await apVoucherService.GetAsModel();

            APVoucher.ap_voucher.Reverse();

            // Convert ap voucher list to DataTable using helper
           avTable = Helpers.ToDataTable(APVoucher.ap_voucher);

            if (avTable?.Rows.Count > 0)
            {
                dgv_av_search.DataSource = avTable;
            }
            else
            {
                dgv_av_search.DataSource = null;
                Helpers.ShowDialogMessage("info", "No ap voucher found.");
            }
        }

        private void dgv_av_search_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            var row = dgv_av_search.Rows[e.RowIndex];

            // Always get the id value from the row, regardless of which column was clicked
            var idValue = row.Cells["id"].Value;

            if (idValue != null)
            {
                SelectedAVId = idValue.ToString();

                this.DialogResult = DialogResult.OK; // close the modal with OK
                this.Close();
            }
        }
    }
}
