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
        private bool _isEditing = false;
        private string _userName;

        public PaymentVoucherPage2()
        {
            InitializeComponent();

            Helpers.NumericTextBox.HandleNumericTextBox(new TextBox[] { txt_cash_amount, txt_check_amount, txt_transfer_amount, txt_unapplied_amount, txt_overpayment_amount }, '.');

            _userName = CacheData.CurrentUser.first_name + " " + CacheData.CurrentUser.last_name;

            Helpers.TextboxFormatter.TextboxDecimalFormat(new[] { txt_cash_amount, txt_check_amount, txt_transfer_amount, txt_transaction_amount, txt_overpayment_amount, txt_unapplied_amount });
            Helpers.DataGridViewFormatter.DataGridViewDecimalFormat(dgv_main, new[] { "trans_amount", "open_amount", "amount_applied", "twas_applied", "balance" });
        }

        private void SetEditMode(bool enable, bool isNewMode = false)
        {
            _isNewMode = isNewMode;
            _isEditing = enable;

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
                "txt_transaction_amount", "dtp_doc_date", "txt_reference_apv", "txt_overpayment_amount", "txt_unapplied_amount" });
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

            // If no records exist, clear everything
            if (_paymentVouchers == null || !_paymentVouchers.Any())
            {
                ClearPaymentVoucherUI();
                return;
            }

            // Return to the previous record index if available
            if (_previousPVIndex >= 0 && _paymentVouchers != null && _paymentVouchers.Count > 0)
            {
                _currentPVIndex = _previousPVIndex;
                await LoadPaymentVouchers();
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

                string[] columnsToValidate = new[] { "doc_no", "due_date", "trans_amount" };

                if (await Helpers.ValidateDataGridViewCells(dgv_main, columnsToValidate))
                    return;

                var paymentVoucherParent = Helpers.BuildModelFromPanels<PaymentVoucherModel>(new Panel[] { pnl_main });

                // Check Amount
                decimal checkAmount = 0;
                decimal.TryParse(txt_check_amount.Text, out checkAmount);

                if (checkAmount == 0)
                {
                    paymentVoucherParent.check_date = null;   // Make sure this is nullable DateTime?
                }

                // Transfer Amount
                decimal transferAmount = 0;
                decimal.TryParse(txt_transfer_amount.Text, out transferAmount);

                if (transferAmount == 0)
                {
                    paymentVoucherParent.ref_doc_date = null;  // Make sure this is nullable DateTime?
                }

                paymentVoucherParent.prepared_by = _userName;

                var paymentVoucherDetails = Helpers.DatagridviewMapper.BuildModelsFromData<PaymentVoucherDetailsModel>(dgv_main);

                // Custom validation
                if (!ValidatePaymentVoucher(paymentVoucherParent, paymentVoucherDetails))
                {
                    return;
                }

                // Wrap everything into Payment Voucher Payload
                var pvPayload = new PaymentVoucherPayload
                {
                    payment_voucher = paymentVoucherParent,
                    payment_voucher_details = paymentVoucherDetails,
                };

                Helpers.Loading.ShowLoading(dgv_main, "Saving data...");

                var result = await paymentVoucherService.CreatePaymentVoucherRecord(pvPayload);

                if (!result.success)
                {
                    Helpers.ShowDialogMessage("error", "Payment Voucher not created.");
                    return;
                }

                Helpers.ShowDialogMessage("success", "Payment Voucher created successfully.");

                SetEditMode(false);
                txt_overpayment_amount.Clear();
                await LoadPaymentVouchers();
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

        private bool ValidatePaymentVoucher(
    PaymentVoucherModel paymentVoucherParent,
    List<PaymentVoucherDetailsModel> paymentVoucherDetails)
        {
            // Validate parent
            if (paymentVoucherParent == null)
            {
                Helpers.ShowDialogMessage("error", "Invalid payment voucher data.");
                return false;
            }

            // Validate details existence
            if (paymentVoucherDetails == null || paymentVoucherDetails.Count == 0)
            {
                Helpers.ShowDialogMessage("error", "Please have at least one payment voucher.");
                return false;
            }

            bool hasPositiveAmountApplied = false;

            foreach (var detail in paymentVoucherDetails)
            {
                decimal amountApplied = Convert.ToDecimal(detail.amount_applied);

                // Only validate rows where amount_applied is not zero
                if (amountApplied != 0)
                {
                    hasPositiveAmountApplied = amountApplied > 0;

                    if (amountApplied < 0)
                    {
                        Helpers.ShowDialogMessage("error",
                            $"Amount applied cannot be negative.\nDoc No: {detail.doc_no}");
                        return false;
                    }

                    if (amountApplied > Convert.ToDecimal(detail.open_amount))
                    {
                        Helpers.ShowDialogMessage("error",
                            $"Amount applied cannot be greater than open amount.\nDoc No: {detail.doc_no}");
                        return false;
                    }
                }
            }

            // If no row has amount_applied greater than zero
            if (!paymentVoucherDetails.Any(x => x.amount_applied > 0))
            {
                Helpers.ShowDialogMessage("error",
                    "Please enter at least one Amount Applied greater than zero.");
                return false;
            }

            return true;
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

                    decimal overpaymentAmount = 0m;

                    if (row["overpayment_amount"] != DBNull.Value)
                    {
                        decimal.TryParse(row["overpayment_amount"].ToString(), out overpaymentAmount);
                    }

                    txt_overpayment_amount.Text = overpaymentAmount.ToString("C2", System.Globalization.CultureInfo.GetCultureInfo("en-PH"));
                    txt_overpayment_amount.AccessibleDescription = overpaymentAmount.ToString(); // Store full precise value

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

                        targetRow.Cells["ap_voucher_details_id"].Value = apvRow["ap_voucher_details_id"];
                        targetRow.Cells["doc_no"].Value = apvRow["doc_no"];
                        targetRow.Cells["due_date"].Value = apvRow["due_date"];
                        targetRow.Cells["trans_amount"].Value = apvRow["trans_amount"];
                        targetRow.Cells["open_amount"].Value = apvRow["open_amount"];
                        targetRow.Cells["twas_applied"].Value = apvRow["twas_applied"];
                    }

                    dgv_main.Refresh();

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

        private void dgv_main_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            Helpers.HandleNumericColumns(dgv_main, e, new[] { "amount_applied" }, '.');
        }

        private void dgv_main_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgv_main.IsCurrentCellDirty)
            {
                dgv_main.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgv_main_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Only trigger when amount_applied changes
            if (dgv_main.Columns[e.ColumnIndex].Name == "amount_applied")
            {
                var row = dgv_main.Rows[e.RowIndex];

                decimal openAmount = 0m;
                decimal amountApplied = 0m;

                // Get open_amount
                if (row.Cells["open_amount"].Value != null)
                    decimal.TryParse(row.Cells["open_amount"].Value.ToString(), out openAmount);

                // Get amount_applied
                if (row.Cells["amount_applied"].Value != null)
                    decimal.TryParse(row.Cells["amount_applied"].Value.ToString(), out amountApplied);

                // Compute balance
                decimal balance = openAmount - amountApplied;

                row.Cells["balance"].Value = balance;

                UpdateUnappliedAmount();
            }
        }

        private void dgv_main_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgv_main.Columns[e.ColumnIndex].Name == "amount_applied")
            {
                var row = dgv_main.Rows[e.RowIndex];

                decimal openAmount = 0m;
                decimal newAmountApplied = 0m;

                // Get open amount
                if (row.Cells["open_amount"].Value != null)
                    decimal.TryParse(row.Cells["open_amount"].Value.ToString(), out openAmount);

                // Get the NEW value being entered (important!)
                if (!decimal.TryParse(e.FormattedValue?.ToString(), out newAmountApplied))
                    newAmountApplied = 0m;

                //If it will cause negative balance
                if (newAmountApplied > openAmount)
                {
                    MessageBox.Show(
                        "Amount applied cannot be greater than Open Amount.",
                        "Validation Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    e.Cancel = true; //This prevents the value from being committed
                }

                // Validate total does not exceed payment total
                decimal totalPayment = GetTotalPaymentAmount();
                decimal totalApplied = GetTotalAmountApplied(e.RowIndex, newAmountApplied);

                if (totalApplied > totalPayment)
                {
                    MessageBox.Show(
                        "Total Amount Applied cannot exceed Total Payment Amount.",
                        "Validation Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    e.Cancel = true;
                }
            }
        }

        private void txt_check_amount_TextChanged(object sender, EventArgs e)
        {
            bool hasValue = !string.IsNullOrWhiteSpace(txt_check_amount.Text) &&
                    txt_check_amount.Text != "0" &&
                    txt_check_amount.Text != "0.00";

            if (hasValue)
            {
                // Add REQUIRED tag
                cmb_check_bank.Tag = "REQUIRED";
                txt_check_account_no.Tag = "REQUIRED";
                txt_ref_check_no.Tag = "REQUIRED";
                dtp_check_date.Tag = "REQUIRED";
            }
            else
            {
                // Remove REQUIRED tag
                cmb_check_bank.Tag = null;
                txt_check_account_no.Tag = null;
                txt_ref_check_no.Tag = null;
                dtp_check_date.Tag = null;
            }

            UpdateTransactionAmount();
            UpdateAmountAppliedColumnState();
            UpdateUnappliedAmount();

        }

        private void txt_transfer_amount_TextChanged(object sender, EventArgs e)
        {
            bool hasValue = !string.IsNullOrWhiteSpace(txt_transfer_amount.Text) &&
                    txt_transfer_amount.Text != "0" &&
                    txt_transfer_amount.Text != "0.00";

            if (hasValue)
            {
                // Add REQUIRED tag
                txt_transfer_type.Tag = "REQUIRED";
                txt_transfer_bank.Tag = "REQUIRED";
                txt_transfer_account_no.Tag = "REQUIRED";
                txt_ref_doc_no.Tag = "REQUIRED";
                dtp_ref_doc_date.Tag = "REQUIRED";
            }
            else
            {
                // Remove REQUIRED tag
                txt_transfer_type.Tag = null;
                txt_transfer_bank.Tag = null;
                txt_transfer_account_no.Tag = null;
                txt_ref_doc_no.Tag = null;
                dtp_ref_doc_date.Tag = null;
            }

            UpdateTransactionAmount();
            UpdateAmountAppliedColumnState();
            UpdateUnappliedAmount();
        }

        private void txt_cash_amount_TextChanged(object sender, EventArgs e)
        {
            UpdateTransactionAmount();
            UpdateAmountAppliedColumnState();
            UpdateUnappliedAmount();
        }

        private void UpdateTransactionAmount()
        {
            if (!_isEditing)
                return;

            decimal cashAmount = 0m;
            decimal checkAmount = 0m;
            decimal transferAmount = 0m;

            decimal.TryParse(txt_cash_amount.Text, out cashAmount);
            decimal.TryParse(txt_check_amount.Text, out checkAmount);
            decimal.TryParse(txt_transfer_amount.Text, out transferAmount);

            decimal total = checkAmount + transferAmount + cashAmount;

            txt_transaction_amount.Text = total.ToString("C2", System.Globalization.CultureInfo.GetCultureInfo("en-PH"));
            txt_transaction_amount.AccessibleDescription = total.ToString(); // Store full precise value
        }

        private void UpdateAmountAppliedColumnState()
        {
            bool hasCash = !string.IsNullOrWhiteSpace(txt_cash_amount.Text) &&
                           txt_cash_amount.Text != "0" &&
                           txt_cash_amount.Text != "0.00";

            bool hasCheck = !string.IsNullOrWhiteSpace(txt_check_amount.Text) &&
                            txt_check_amount.Text != "0" &&
                            txt_check_amount.Text != "0.00";

            bool hasTransfer = !string.IsNullOrWhiteSpace(txt_transfer_amount.Text) &&
                               txt_transfer_amount.Text != "0" &&
                               txt_transfer_amount.Text != "0.00";

            bool enableEditing = hasCash || hasCheck || hasTransfer;

            if (dgv_main.Columns["amount_applied"] != null)
            {
                dgv_main.Columns["amount_applied"].ReadOnly = !enableEditing;
            }

            //If ALL payment types are cleared
            if (!enableEditing)
            {
                foreach (DataGridViewRow row in dgv_main.Rows)
                {
                    if (row.IsNewRow) continue;

                    // Clear amount_applied
                    row.Cells["amount_applied"].Value = 0;

                    // Clear balance
                    row.Cells["balance"].Value = 0;
                }

                dgv_main.Refresh();
            }
        }

        private decimal GetTotalAmountApplied(int? editingRowIndex = null, decimal? newValue = null)
        {
            if (!_isEditing)
                return 0m;

            decimal totalApplied = 0m;

            foreach (DataGridViewRow row in dgv_main.Rows)
            {
                if (row.IsNewRow) continue;

                decimal value = 0m;

                // If currently editing a row, use the new value instead of old value
                if (editingRowIndex.HasValue &&
                    row.Index == editingRowIndex.Value &&
                    newValue.HasValue)
                {
                    value = newValue.Value;
                }
                else if (row.Cells["amount_applied"].Value != null)
                {
                    decimal.TryParse(row.Cells["amount_applied"].Value.ToString(), out value);
                }

                totalApplied += value;
            }

            return totalApplied;
        }

        private decimal GetTotalPaymentAmount()
        {
            decimal cash = 0m;
            decimal check = 0m;
            decimal transfer = 0m;

            decimal.TryParse(txt_cash_amount.Text, out cash);
            decimal.TryParse(txt_check_amount.Text, out check);
            decimal.TryParse(txt_transfer_amount.Text, out transfer);

            return cash + check + transfer;
        }

        private void UpdateUnappliedAmount()
        {
            if (!_isEditing)
                return;

            decimal totalPayment = GetTotalPaymentAmount();
            decimal totalApplied = GetTotalAmountApplied();

            decimal unapplied = totalPayment - totalApplied;

            // If applied exceeds payment (should not happen because of validation),
            // just force unapplied to zero
            if (unapplied < 0)
                unapplied = 0;

            txt_unapplied_amount.Text = unapplied.ToString("C2", System.Globalization.CultureInfo.GetCultureInfo("en-PH"));
            txt_unapplied_amount.AccessibleDescription = unapplied.ToString(); // Store full precise value
        }
    }
}
