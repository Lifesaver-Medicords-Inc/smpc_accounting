using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using smpc_accounting_app.Pages.Transactions.AccountsPayable.PaymentVoucher;
using smpc_accounting_app.Services.Helpers;
using smpc_accounting_app.Services;
using smpc_accounting_app.Models;
using smpc_accounting_app.Services.Transactions;
using smpc_accounting_app.Shared;
using smpc_accounting_app.Pages.Transactions.AccountsPayable.InvoiceReceipt.InvoiceReceiptModals;

namespace smpc_accounting_app.Pages.Transactions.AccountsPayable.PaymentVoucher
{
    public partial class PaymentVoucherPage2 : UserControl
    {
        PaymentVoucherService paymentVoucherService = new PaymentVoucherService();
        private int _currentPVIndex = -1;
        private int _previousPVIndex = -1;
        private PaymentVoucherList _pvdata;
        private List<PaymentVoucherModel> _paymentVouchers;
        private DataTable _pvTable;
        private BindingList<PaymentVoucherDetailsModel> _currentDetails;
        private CompanySetupModel _companySetup = CacheData.CompanySetup;
        private bool _isNewMode = false;
        private string _userName;

        public PaymentVoucherPage2()
        {
            InitializeComponent();

            Helpers.NumericTextBox.HandleNumericTextBox(new TextBox[] { txt_cash_amount, txt_check_amount, txt_transfer_amount }, '.');

            _userName = CacheData.CurrentUser.first_name + " " + CacheData.CurrentUser.last_name;

            Helpers.TextboxFormatter.TextboxDecimalFormat(new[] { txt_cash_amount, txt_check_amount, txt_transfer_amount, txt_transaction_amount });
            Helpers.DataGridViewFormatter.DataGridViewDecimalFormat(dgv_main, new[] { "trans_amount", "open_amount", "amount_applied", "twas_applied", "balance" });
        }

        private void SetEditableColumns(bool isEdit)
        {
            var editableColumns = new[] { "discount" };

            foreach (var colName in editableColumns)
            {
                if (dgv_main.Columns.Contains(colName))
                    dgv_main.Columns[colName].ReadOnly = !isEdit;
            }
        }

        private void SetEditMode(bool enable, bool isNewMode = false)
        {
            _isNewMode = isNewMode;

            SetEditableColumns(enable);
            btn_apv.Enabled = enable;
            btn_supplier.Enabled = enable;

            // buttons
            string[] editButtons = { "btn_save", "btn_cancel" };
            string[] navButtons = { "btn_new", "btn_print", "btn_edit", "btn_delete", "btn_next", "btn_prev", "btn_search" };

            Helpers.SetButtonVisibility(
                toolStrip1,
                visibleButtons: enable ? editButtons : navButtons,
                hiddenButtons: enable ? navButtons : editButtons
            );

            Helpers.SetChildControlsEnabled(new[] { pnl_main }, enable, new string[] { "txt_doc_no", "txt_supplier", "txt_supplier_code", "txt_currency",
                "txt_transaction_amount", "dtp_doc_date", "txt_reference_apv" });
        }

        private void ChangeRecord(int step)
        {
            if (_paymentVouchers == null || !_paymentVouchers.Any()) return;

            int newIndex = _currentPVIndex + step;
            if (newIndex >= 0 && newIndex < _paymentVouchers.Count)
            {
                _currentPVIndex = newIndex;
                ShowCurrentRecord();
            }
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            // Save current index before clearing
            _previousPVIndex = _currentPVIndex;
            SetEditMode(true, isNewMode: true);

            //Clear only the rows, keep columns
            dgv_main.DataSource = null;
            dgv_main.Rows.Clear();
            Helpers.ResetControls(pnl_main);
        }

        private async void btn_search_Click(object sender, EventArgs e)
        {
            if (_paymentVouchers == null || _paymentVouchers.Count == 0)
            {
                await LoadPaymentVouchers();
            }

            using (var searchForm = new PaymentVoucherSearch())
            {
                if (searchForm.ShowDialog(this) == DialogResult.OK && !string.IsNullOrEmpty(searchForm.SelectedPVId))
                {
                    if (int.TryParse(searchForm.SelectedPVId, out int selectedId))
                    {
                        int index = _paymentVouchers.FindIndex(r => r.id == selectedId);
                        if (index >= 0)
                        {
                            _currentPVIndex = index;
                            await LoadPaymentVouchers();
                        }
                    }
                    else
                    {
                        Helpers.ShowDialogMessage("error", "Invalid record ID selected.");
                    }
                }
            }
        }

        private async void btn_cancel_Click(object sender, EventArgs e)
        {
            SetEditMode(false);

            // Return to the previous record index if available
            if (_previousPVIndex >= 0 && _paymentVouchers != null && _paymentVouchers.Count > 0)
                
                _currentPVIndex = _previousPVIndex;
                await LoadPaymentVouchers();
            }

        private void btn_prev_Click(object sender, EventArgs e)
        {
            ChangeRecord(-1);
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            ChangeRecord(1);
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

                var paymentVoucherParent = Helpers.BuildModelFromPanels<PaymentVoucherModel>(new Panel[] { pnl_main });

                paymentVoucherParent.prepared_by = _userName;

                var paymentVoucherDetails = Helpers.DatagridviewMapper.BuildModelsFromData<PaymentVoucherDetailsModel>(dgv_main);

                //Check if payment voucher details is null or empty
                if (paymentVoucherDetails == null || paymentVoucherDetails.Count == 0)
                {
                    Helpers.ShowDialogMessage("error", "Please have at least one invoice receipt.");
                    return;
                }

                // Wrap everything into Invoice R Payload
                var pvPayload = new PaymentVoucherPayload
                {
                    payment_voucher = paymentVoucherParent,
                    payment_voucher_details = paymentVoucherDetails,
                };

                Helpers.Loading.ShowLoading(dgv_main, "Saving data...");

                var result = await paymentVoucherService.CreatePaymentVoucherRecord(pvPayload);
                Helpers.ShowDialogMessage("success", "Payment Voucher created successfully.");
            }
            catch (Exception ex)
            {
                Helpers.ShowDialogMessage("error", $"Failed to save: {ex.Message}");
            }
            finally
            {
                SetEditMode(false);
                await LoadPaymentVouchers();

                btn_save.Enabled = true;
                btn_cancel.Enabled = true;

                Helpers.Loading.HideLoading(dgv_main);
            }
        }

        private async void PaymentVoucherPage2_Load(object sender, EventArgs e)
        {
            try
            {
                Helpers.Loading.ShowLoading(dgv_main, "Fetching data...");
                await LoadPaymentVouchers();
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

        private async Task LoadPaymentVouchers()
        {
            // save current index before reload
            int oldIndex = _currentPVIndex;

            //fill this declared value by the payment voucher data
            _pvdata = await paymentVoucherService.GetAsModel();

            // Reverse order so newest records appear first
            _pvdata.payment_voucher.Reverse();

            if (_pvdata != null && _pvdata.payment_voucher != null && _pvdata.payment_voucher.Count > 0)
            {
                //set this variable to the parent of the payment voucher
                _paymentVouchers = _pvdata.payment_voucher;

                // restore old index if valid, otherwise fallback to 0
                if (oldIndex >= 0 && oldIndex < _paymentVouchers.Count)
                    _currentPVIndex = oldIndex;
                else
                    _currentPVIndex = 0;

                ShowCurrentRecord();
            }
            else
            {
                ClearPaymentVoucherUI();
            }
        }

        private void ShowCurrentRecord()
        {
            if (_currentPVIndex < 0 || _pvdata == null || _pvdata.payment_voucher == null || !_pvdata.payment_voucher.Any())
                return;

            // Convert payment voucher list to DataTable using helper
            _pvTable = Helpers.ToDataTable(_pvdata.payment_voucher);

            Helpers.BindControls(new Panel[] { pnl_main }, _pvTable, _currentPVIndex);

            //Disable auto column generation before setting the data source
            dgv_main.AutoGenerateColumns = false;

            var current = _paymentVouchers[_currentPVIndex];

            //Bind child details (grids)
            if (_pvdata?.payment_voucher_details != null)
            {
                _currentDetails = new BindingList<PaymentVoucherDetailsModel>(
                    _pvdata.payment_voucher_details
                        .Where(d => d.payment_voucher_id == current.id)
                        .ToList()
                );

                dgv_main.DataSource = _currentDetails;
            }
            else
            {
                dgv_main.DataSource = null;
            }

            //Enable/disable navigation buttons
            btn_prev.Enabled = _currentPVIndex > 0;
            btn_next.Enabled = _currentPVIndex < _paymentVouchers.Count - 1;
        }

        private void ClearPaymentVoucherUI()
        {
            _paymentVouchers = new List<PaymentVoucherModel>();
            _currentPVIndex = -1;
            _previousPVIndex = -1;

            // Clear panel fields
            Helpers.ResetControls(new Panel[] { pnl_main });

            // Clear grid
            dgv_main.DataSource = null;
            dgv_main.Rows.Clear();

            // Disable navigation buttons
            btn_prev.Enabled = false;
            btn_next.Enabled = false;
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

                    txt_reference_apv.Text = string.Empty;

                    dgv_main.DataSource = null;
                    dgv_main.Rows.Clear();
                }
            }
        }

        private void btn_apv_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_supplier_id.Text))
            {
                Helpers.ShowDialogMessage("error", "Please select a supplier first.");
                return;
            }

            if (!int.TryParse(txt_supplier_id.Text, out int supplierId))
            {
                Helpers.ShowDialogMessage("error", "Invalid Supplier selected.");
                return;
            }

            using (var modal = new PaymentVoucherSearchAPV(supplierId))
            {
                // Convert current dgv_main rows → DataTable
                modal.ExistingAPVs = Helpers.ToDataTable(
                    Helpers.DatagridviewMapper.BuildModelsFromData<ApVoucherViewModel>(dgv_main)
                );

                if (modal.ShowDialog(this) == DialogResult.OK &&
                    modal.SelectedAPVs != null &&
                    modal.SelectedAPVs.Rows.Count > 0)
                {
                    foreach (DataRow apvRow in modal.SelectedAPVs.Rows)
                    {
                        int newRowIndex = dgv_main.Rows.Add();
                        var targetRow = dgv_main.Rows[newRowIndex];

                        targetRow.Cells["ap_voucher_id"].Value = apvRow["ap_voucher_id"];
                        targetRow.Cells["doc_no"].Value = apvRow["doc_no"];
                        targetRow.Cells["due_date"].Value = apvRow["due_date"];
                        targetRow.Cells["trans_amount"].Value = apvRow["trans_amount"];
                    }

                    dgv_main.Refresh();

                    foreach (DataGridViewRow row in dgv_main.Rows)
                    {
                        if (row.IsNewRow) continue;

                        if (row.Cells["trans_amount"].Value != null)
                        {
                            row.Cells["open_amount"].Value = row.Cells["trans_amount"].Value;
                        }
                    }

                    //Set Reference PO textbox
                    if (modal.SelectedAVLabels != null && modal.SelectedAVLabels.Any())
                    {
                        txt_reference_apv.Text = string.Join(", ", modal.SelectedAVLabels);
                    }
                    else
                    {
                        txt_reference_apv.Text = string.Empty;
                    }
                }
            }
        }
    }
}
