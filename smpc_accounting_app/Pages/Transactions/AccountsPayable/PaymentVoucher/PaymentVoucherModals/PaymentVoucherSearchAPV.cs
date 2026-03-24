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

namespace smpc_accounting_app.Pages.Transactions.AccountsPayable.PaymentVoucher.PaymentVoucherModals
{
    public partial class PaymentVoucherSearchAPV : Form
    {
        public DataTable SelectedAPVs { get; private set; } = null;
        public float? OverpaymentAmount { get; private set; } = null;
        private string placeHolderText = "AP Voucher Search...";
        GeneralService<ApVoucherViewList> serviceSetup;
        private DataTable supplierTable;
        private int _supplierID;
        public List<string> SelectedAVLabels { get; private set; } = new List<string>();
        public DataTable ExistingAPVs { get; set; }

        private List<ApVoucherViewModel> _parentdata;
        private List<ApVoucherDetailsViewModel> _childdata;
        private ApVoucherViewList _avdata;

        public PaymentVoucherSearchAPV(int supplierId)
        {
            InitializeComponent();

            // Center the modal relative to its parent form
            this.StartPosition = FormStartPosition.CenterParent;

            dgv_apv_search.AutoGenerateColumns = false;
            InitializeSearchBox();
            Helpers.DataGridViewFormatter.DataGridViewDecimalFormat(dgv_apv_search, new[] { "transaction_amount" });
            Helpers.DataGridViewDocumentFormatter.DataGridViewDocumentFormat(dgv_apv_search, "doc_no", "AV");
            _supplierID = supplierId;
        }

        private void InitializeSearchBox()
        {
            txt_search = Helpers.CreateSearchBox(placeHolderText, txt_search_TextChanged);
            this.Controls.Add(txt_search);
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            if (supplierTable == null || supplierTable.Rows.Count == 0)
                return;

            string searchText = txt_search.Text.Trim();

            if (string.IsNullOrEmpty(searchText) || searchText == placeHolderText)
            {
                dgv_apv_search.DataSource = supplierTable;
            }
            else
            {
                var searchedData = Helpers.FilterDataTable(supplierTable, searchText,
                    "supplier_code", "supplier", "doc_no", "doc_date", "currency", "doc_no", "doc_date", "transaction_amount", "twas_amount");
                dgv_apv_search.DataSource = searchedData;
            }
        }

        private async void PaymentVoucherSearchAPV_Load(object sender, EventArgs e)
        {
            try
            {
                Helpers.Loading.ShowLoading(dgv_apv_search, "Fetching data...");
                await LoadAPVoucher();
            }
            catch (Exception ex)
            {
                Helpers.ShowDialogMessage("error", $"Failed to load: {ex.Message}");
            }
            finally
            {
                Helpers.Loading.HideLoading(dgv_apv_search);
            }
        }

        private async Task LoadAPVoucher()
        {
            try
            {
                serviceSetup = new GeneralService<ApVoucherViewList>(ApiEndPoints.AP_VOUCHER_PAYMENT + _supplierID);
                _avdata = await serviceSetup.GetAsModel();

                _parentdata = _avdata?.ap_voucher_view;
                _childdata = _avdata?.ap_voucher_details_view;

                if (_parentdata == null || !_parentdata.Any())
                    throw new InvalidOperationException("No Purchase Orders found.");

                supplierTable = Helpers.ToDataTable(_parentdata);

                // If there are existing IRs, exclude them
                if (ExistingAPVs != null && ExistingAPVs.Rows.Count > 0)
                {
                    var usedIds = ExistingAPVs.AsEnumerable()
                        .Where(r => r["ap_voucher_id"] != DBNull.Value)
                        .Select(r => Convert.ToInt32(r["ap_voucher_id"]))
                        .ToHashSet();

                    var filteredRows = supplierTable.AsEnumerable()
                        .Where(r => !usedIds.Contains(Convert.ToInt32(r["ap_voucher_id"])));

                    supplierTable = filteredRows.Any()
                        ? filteredRows.CopyToDataTable()
                        : supplierTable.Clone(); // empty table with same structure
                }

                if (supplierTable?.Rows.Count > 0)
                {
                    dgv_apv_search.DataSource = supplierTable;
                }
                else
                {
                    dgv_apv_search.DataSource = null;
                    Helpers.ShowDialogMessage("error", "No ap voucher found.");
                    this.Close();
                }
            }
            catch (InvalidOperationException)
            {
                // Catch real unexpected errors
                Helpers.ShowDialogMessage("error", "No AP Voucher found.");
                this.Close();
            }
            catch (NullReferenceException)
            {
                Helpers.ShowDialogMessage("error", "No AP Voucher found.");
            }
            catch (Exception)
            {
                // Catch real unexpected errors
                Helpers.ShowDialogMessage("error", "No AP Voucher found.");
                this.Close();
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            SelectedAVLabels.Clear();

            if (supplierTable == null || supplierTable.Rows.Count == 0)
                return;

            //Collect selected ap_voucher_ids
            var selectedIds = new HashSet<int>();

            foreach (DataGridViewRow row in dgv_apv_search.Rows)
            {
                if (row.IsNewRow) continue;

                bool isChecked = false;

                if (row.Cells["isSelected"].Value != null)
                {
                    bool.TryParse(row.Cells["isSelected"].Value.ToString(), out isChecked);
                }

                if (isChecked)
                {
                    // Get ap_voucher_id
                    if (row.Cells["ap_voucher_id"].Value != null)
                    {
                        int id = Convert.ToInt32(row.Cells["ap_voucher_id"].Value);
                        selectedIds.Add(id);
                    }

                    // still collect doc_no if needed
                    var docNo = row.Cells["doc_no"].Value?.ToString();
                    if (!string.IsNullOrEmpty(docNo))
                    {
                        SelectedAVLabels.Add(docNo);
                    }
                }
            }

            if (!selectedIds.Any())
            {
                Helpers.ShowDialogMessage("error", "Please select at least one AP Voucher.");
                return;
            }

            // Filter child data based on selected ap_voucher_id
            var filteredChildData = _childdata
                ?.Where(c => selectedIds.Contains(c.ap_voucher_id))
                .ToList();

            if (filteredChildData == null || !filteredChildData.Any())
            {
                Helpers.ShowDialogMessage("error", "No matching AP Voucher details found.");
                return;
            }

            // Convert filtered child data to DataTable
            SelectedAPVs = Helpers.ToDataTable(filteredChildData);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
