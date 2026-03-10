using smpc_accounting_app.Pages.Components;
using smpc_accounting_app.Services.Setup;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using smpc_accounting_app.Services.Helpers;
using smpc_accounting_app.Services;
using smpc_accounting_app.Models;
using smpc_accounting_app.Services.Transactions;
using smpc_accounting_app.Shared;
using smpc_accounting_app.Pages.Transactions.AccountsPayable.InvoiceReceipt.InvoiceReceiptModals;
using smpc_accounting_app.Printing;
using Microsoft.Reporting.WinForms;
using System.IO;

namespace smpc_accounting_app.Pages.Transactions.AccountsPayable.APVoucher
{
    public partial class APVoucherPage : UserControl
    {
        APVoucherService apVoucherService = new APVoucherService();
        private int _currentAVIndex = -1;
        private int _previousAVIndex = -1;
        private APVoucherList _avdata;
        private List<APVoucherModel> _apVouchers;
        private DataTable _avTable;
        private BindingList<APVoucherDetailsModel> _currentDetails;
        private CompanySetupModel _companySetup = CacheData.CompanySetup;
        private bool _isNewMode = false;
        private bool _isEditing = false;
        private string _userName;

        public APVoucherPage()
        {
            InitializeComponent();

            _userName = CacheData.CurrentUser.first_name + " " + CacheData.CurrentUser.last_name;

            Helpers.TextboxFormatter.TextboxDecimalFormat(new[] { txt_transaction_amount });
            Helpers.DataGridViewFormatter.DataGridViewDecimalFormat(dgv_main, new[] { "line_amount" });
        }

        private void SetEditMode(bool enable, bool isNewMode = false)
        {
            _isNewMode = isNewMode;
            dgv_main.AllowUserToAddRows = enable;
            _isEditing = enable;

            btn_supplier.Enabled = enable;

            // buttons
            string[] editButtons = { "btn_save", "btn_cancel" };
            string[] navButtons = { "btn_new", "btn_print", "btn_edit", "btn_delete", "btn_next", "btn_prev", "btn_search" };

            Helpers.SetButtonVisibility(
                toolStrip1,
                visibleButtons: enable ? editButtons : navButtons,
                hiddenButtons: enable ? navButtons : editButtons
            );

            Helpers.SetChildControlsEnabled(new[] { pnl_main }, enable, new string[] { "txt_doc_no", "txt_supplier_code",
                "txt_currency","txt_supplier", "txt_transaction_amount", "dtp_doc_date" });
        }

        private void ChangeRecord(int step)
        {
            if (_apVouchers == null || !_apVouchers.Any()) return;

            int newIndex = _currentAVIndex + step;
            if (newIndex >= 0 && newIndex < _apVouchers.Count)
            {
                _currentAVIndex = newIndex;
                ShowCurrentRecord();
            }
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            // Save current index before clearing
            _previousAVIndex = _currentAVIndex;
            SetEditMode(true, isNewMode: true);

            //Clear only the rows, keep columns
            _currentDetails = new BindingList<APVoucherDetailsModel>();
            dgv_main.AutoGenerateColumns = false;
            dgv_main.DataSource = _currentDetails;
            Helpers.ResetControls(pnl_main);
        }

        private async void btn_search_Click(object sender, EventArgs e)
        {
            if (_apVouchers == null || _apVouchers.Count == 0)
            {
                await LoadAPVouchers();
            }

            using (var searchForm = new APVoucherSearch())
            {
                if (searchForm.ShowDialog(this) == DialogResult.OK && !string.IsNullOrEmpty(searchForm.SelectedAVId))
                {
                    if (int.TryParse(searchForm.SelectedAVId, out int selectedId))
                    {
                        int index = _apVouchers.FindIndex(r => r.id == selectedId);
                        if (index >= 0)
                        {
                            _currentAVIndex = index;
                            await LoadAPVouchers();
                        }
                    }
                    else
                    {
                        Helpers.ShowDialogMessage("error", "Invalid record ID selected.");
                    }
                }
            }
        }

        private async void btn_save_Click(object sender, EventArgs e)
        {
            btn_save.Enabled = false;
            btn_cancel.Enabled = false;

            try
            {
                dgv_main.EndEdit();

                // Validate required controls in selected panel
                bool hasError = Helpers.ValidateControlsValues(pnl_main);

                if (hasError) // if validation failed
                {
                    Helpers.ShowDialogMessage("error", "Please fill in all required fields.");
                    return;
                }

                string[] columnsToValidate = new[] { "receipt_no", "ir_doc_no", "line_amount" };

                if (await Helpers.ValidateDataGridViewCells(dgv_main, columnsToValidate))
                    return;

                var apVoucherParent = Helpers.BuildModelFromPanels<APVoucherModel>(new Panel[] { pnl_main });

                apVoucherParent.prepared_by = _userName;

                var apVoucherDetails = Helpers.DatagridviewMapper.BuildModelsFromData<APVoucherDetailsModel>(dgv_main);

                //Check if ap voucher details is null or empty
                if (apVoucherDetails == null || apVoucherDetails.Count == 0)
                {
                    Helpers.ShowDialogMessage("error", "Please select at least one item.");
                    return;
                }

                // Wrap everything into Invoice R Payload
                var avPayload = new APVoucherPayload
                {
                    ap_voucher = apVoucherParent,
                    ap_voucher_details = apVoucherDetails,
                };

                Helpers.Loading.ShowLoading(dgv_main, "Saving data...");

                var result = await apVoucherService.CreateAPVoucherRecord(avPayload);

                if (!result.success)
                {
                    Helpers.ShowDialogMessage("error", "AP Voucher not created.");
                    return;
                }

                Helpers.ShowDialogMessage("success", "AP Voucher created successfully.");

                SetEditMode(false);
                await LoadAPVouchers();
            }
            catch (Exception ex)
            {
                Helpers.ShowDialogMessage("error", $"Failed to save: {ex.Message}");
            }
            finally
            {
                btn_save.Enabled = true;
                btn_cancel.Enabled = true;

                Helpers.Loading.HideLoading(dgv_main);
            }
        }

        private async void btn_cancel_Click(object sender, EventArgs e)
        {
            SetEditMode(false);

            // If no records exist, clear everything
            if (_apVouchers == null || !_apVouchers.Any())
            {
                ClearAPVoucherUI();
                return;
            }

            // Return to the previous record index if available
            if (_previousAVIndex >= 0 && _apVouchers != null && _apVouchers.Count > 0)
            {
                _currentAVIndex = _previousAVIndex;
                await LoadAPVouchers();
            }
            else
            {
                Helpers.ResetControls(pnl_main);
                dgv_main.Rows.Clear();
            }
        }

        private void btn_prev_Click(object sender, EventArgs e)
        {
            ChangeRecord(-1);
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            ChangeRecord(1);
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            if (_currentAVIndex < 0) return;

            var reportPath = Path.Combine(Application.StartupPath, "Printing", "AccountsPayables", "APVoucherReport.rdlc");

            // DEBUG CHECK
            if (!File.Exists(reportPath))
            {
                MessageBox.Show("RDLC file not found:\n" + reportPath);
                return;
            }

            var dataSources = new List<ReportDataSource>()
            {
                new ReportDataSource("DataSet2", _currentDetails),
                new ReportDataSource("DataSet1", new List<APVoucherModel> { _apVouchers[_currentAVIndex] })
            };

            var preview = new PrintPreview(reportPath, dataSources);

            preview.ShowDialog();
        }

        private void btn_supplier_Click(object sender, EventArgs e)
        {
            using (var modal = new InvoiceSearchSupplier())
            {
                if (modal.ShowDialog(this) == DialogResult.OK && modal.SelectedSupplier != null)
                {
                    DataRow row = modal.SelectedSupplier.Rows[0];

                    // Assign values to controls
                    txt_supplier_id.Text = row["supplier_id"]?.ToString();
                    txt_supplier.Text = row["supplier"]?.ToString();
                    txt_supplier_code.Text = row["supplier_code"]?.ToString();
                    txt_currency.Text = _companySetup.currency_code;

                    dgv_main.DataSource = null;
                    dgv_main.Rows.Clear();
                }
            }
        }

        private async void APVoucherPage_Load(object sender, EventArgs e)
        {
            try
            {
                Helpers.Loading.ShowLoading(dgv_main, "Fetching data...");
                await LoadAPVouchers();
            }
            catch (Exception ex)
            {
                Helpers.ShowDialogMessage("error", $"Failed to load: {ex.Message}");
            }
            finally
            {
                Helpers.Loading.HideLoading(dgv_main);
            }
        }

        private void ClearAPVoucherUI()
        {
            _apVouchers = new List<APVoucherModel>();
            _currentAVIndex = -1;
            _previousAVIndex = -1;

            // Clear panel fields
            Helpers.ResetControls(new Panel[] { pnl_main });

            // Clear grid
            dgv_main.DataSource = null;
            dgv_main.Rows.Clear();

            // Disable navigation buttons
            btn_prev.Enabled = false;
            btn_next.Enabled = false;
        }

        private async Task LoadAPVouchers()
        {
            // save current index before reload
            int oldIndex = _currentAVIndex;

            //fill this declared value by the ap voucher data
            _avdata = await apVoucherService.GetAsModel();

            // Reverse order so newest records appear first
            _avdata.ap_voucher.Reverse();

            if (_avdata != null && _avdata.ap_voucher != null && _avdata.ap_voucher.Count > 0)
            {
                //set this variable to the parent of the ap voucher
                _apVouchers = _avdata.ap_voucher;

                // restore old index if valid, otherwise fallback to 0
                if (oldIndex >= 0 && oldIndex < _apVouchers.Count)
                    _currentAVIndex = oldIndex;
                else
                    _currentAVIndex = 0;

                ShowCurrentRecord();
            }
            else
            {
                ClearAPVoucherUI();
            }
        }

        private void ShowCurrentRecord()
        {
            if (_currentAVIndex < 0 || _avdata == null || _avdata.ap_voucher == null || !_avdata.ap_voucher.Any())
                return;

            // Convert ap voucher list to DataTable using helper
            _avTable = Helpers.ToDataTable(_avdata.ap_voucher);

            Helpers.BindControls(new Panel[] { pnl_main }, _avTable, _currentAVIndex);

            //Disable auto column generation before setting the data source
            dgv_main.AutoGenerateColumns = false;

            var current = _apVouchers[_currentAVIndex];

            //Bind child details (grids)
            if (_avdata?.ap_voucher_details != null)
            {
                _currentDetails = new BindingList<APVoucherDetailsModel>(
                    _avdata.ap_voucher_details
                        .Where(d => d.ap_voucher_id == current.id)
                        .ToList()
                );

                dgv_main.DataSource = _currentDetails;
            }
            else
            {
                dgv_main.DataSource = null;
            }

            //Enable/disable navigation buttons
            btn_prev.Enabled = _currentAVIndex > 0;
            btn_next.Enabled = _currentAVIndex < _apVouchers.Count - 1;
        }

        private void dgv_main_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Do nothing if supplier is not selected
            if (string.IsNullOrWhiteSpace(txt_supplier_id.Text))
            {
                Helpers.ShowDialogMessage("error", "Please select a supplier first.");
                return;
            }

            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            if (!_isNewMode)
                return;

            string columnName = dgv_main.Columns[e.ColumnIndex].Name;
            if (columnName != "receipt_no")
                return;

            using (var modal = new APVoucherInvoice())
            {
                // Convert current dgv_main rows → DataTable
                modal.ExistingIRs = Helpers.ToDataTable(
                    Helpers.DatagridviewMapper.BuildModelsFromData<InvoiceViewModel>(dgv_main)
                );

                if (int.TryParse(txt_supplier_id.Text, out int supplierId))
                {
                    modal.SupplierId = supplierId;
                }

                if (modal.ShowDialog(this) == DialogResult.OK && modal.SelectedIR != null && modal.SelectedIR.Rows.Count > 0)
                {
                    DataRow irRow = modal.SelectedIR.Rows[0];

                    // Create a new row instead of overwriting the current one
                    int newRowIndex = dgv_main.Rows.Add();
                    var targetRow = dgv_main.Rows[newRowIndex];

                    targetRow.Cells["invoice_receipt_id"].Value = irRow["invoice_receipt_id"];
                    targetRow.Cells["receipt_no"].Value = irRow["receipt_no"];
                    targetRow.Cells["ir_due_date"].Value = irRow["ir_due_date"];
                    targetRow.Cells["ir_doc_date"].Value = irRow["ir_doc_date"];
                    targetRow.Cells["line_amount"].Value = irRow["line_amount"];
                    targetRow.Cells["receipt_type"].Value = irRow["receipt_type"];
                    targetRow.Cells["twas_amount"].Value = irRow["twas_amount"];

                    dgv_main.Refresh();
                }
            }
        }

        private void UpdateNetAmount()
        {
            if (!_isEditing)
                return;

            decimal totalLineAmount = 0m;

            // Sum all line_amounts from dgv_main
            foreach (DataGridViewRow dgRow in dgv_main.Rows)
            {
                if (dgRow.IsNewRow) continue;

                if (dgRow.Cells["line_amount"]?.Value != null &&
                    decimal.TryParse(dgRow.Cells["line_amount"].Value.ToString(), out decimal lineAmount))
                {
                    totalLineAmount += lineAmount;
                }
            }

            // Compute Transaction Amount
            decimal transactionAmount = Helpers.ZeroIfNearZero(totalLineAmount);

            txt_transaction_amount.Text = transactionAmount.ToString("C2", System.Globalization.CultureInfo.GetCultureInfo("en-PH"));
            txt_transaction_amount.AccessibleDescription = transactionAmount.ToString(); // Store full precise value
        }

        private void dgv_main_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (_isNewMode)
            {
                // Update net amount
                UpdateNetAmount();
            }
        }

        private void btn_payment_voucher_Click(object sender, EventArgs e)
        {
            try
            {
                // Disable button immediately
                btn_payment_voucher.Enabled = false;

                var layout = this.FindForm() as Layout;

                if (layout != null)
                {
                    layout.OpenRoute("Payment Voucher"); // route key
                }
            }
            catch (Exception ex)
            {
                // Re-enable only if something failed
                btn_payment_voucher.Enabled = true;

                Helpers.ShowDialogMessage("error", $"Failed to open Payment Voucher: {ex.Message}");
            }
        }

        private void dgv_main_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // Prevent default crash dialog
            e.ThrowException = false;

            Helpers.ShowDialogMessage("error", "Invalid numeric value. Please enter a valid amount.");
        }
    }
}