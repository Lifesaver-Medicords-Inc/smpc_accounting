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
using smpc_accounting_app.Models;
using smpc_accounting_app.Services.Transactions;
using smpc_accounting_app.Shared;
using smpc_accounting_app.Pages.Transactions.AccountsReceivables.SalesInvoice;
using smpc_accounting_app.Services;

namespace smpc_accounting_app.Pages.Transactions.AccountsReceivables.PaymentReceipt
{
    public partial class PaymentReceiptPage : UserControl
    {
        PaymentReceiptService paymentReceiptService = new PaymentReceiptService();
        private int _currentPRIndex = -1;
        private int _previousPRIndex = -1;
        private PaymentReceiptList _prdata;
        private List<PaymentReceiptModel> _paymentReceipt;
        private DataTable _prTable;
        private DataTable _salesViewTable;
        private BindingList<PaymentReceiptDetailsModel> _currentDetails;
        private CompanySetupModel _companySetup = CacheData.CompanySetup;
        GeneralService<SalesInvoiceReceiptView> generalSalesInvoiceView;
        private bool _isNewMode = false;
        private string _userName;
        private string _currencyCode;
        private bool _isEditing;

        public PaymentReceiptPage()
        {
            InitializeComponent();

            Helpers.NumericTextBox.HandleNumericTextBox(new TextBox[] { txt_cash_amount, txt_transaction_amount, txt_unapplied_amount, txt_check_amount, txt_overpayment_amount}, '.');
            _userName = CacheData.CurrentUser.first_name + " " + CacheData.CurrentUser.last_name;
            Helpers.TextboxFormatter.TextboxDecimalFormat(new[] { txt_cash_amount, txt_transaction_amount, txt_unapplied_amount, txt_check_amount, txt_overpayment_amount });
            Helpers.DataGridViewFormatter.DataGridViewDecimalFormat(dgv_main, new[] { "open_amount", "amount_applied", "twas_applied", "balance" });
            _currencyCode = CacheData.CompanySetup.currency_code;
        }

        private void SetEditableColumns(bool isEdit)
        {
            var editableColumns = new[] { "isSelected" };

            foreach (var colName in editableColumns)
            {
                if (dgv_main.Columns.Contains(colName))
                    dgv_main.Columns[colName].ReadOnly = !isEdit;
            }
        }

        private void SetEditMode(bool enable, bool isNewMode = false)
        {
            _isNewMode = isNewMode;
            _isEditing = enable;

            SetEditableColumns(enable);

            btn_customer.Enabled = enable;

            if (enable)
            {
                txt_overpayment_amount.Clear();
            }

            // buttons
            string[] editButtons = { "btn_save", "btn_cancel" };
            string[] navButtons = { "btn_new", "btn_print", "btn_edit", "btn_delete", "btn_next", "btn_prev", "btn_search" };

            Helpers.SetButtonVisibility(
                toolStrip1,
                visibleButtons: enable ? editButtons : navButtons,
                hiddenButtons: enable ? navButtons : editButtons
            );

            Helpers.SetChildControlsEnabled(new[] { pnl_main }, enable, new string[] { "txt_customer", "txt_customer_code",
                "txt_transaction_amount", "txt_unapplied_amount", "txt_doc_no", "txt_doc_date", "txt_currency", "txt_check_type",
                "txt_overpayment_amount" });
        }

        private void ChangeRecord(int step)
        {
            if (_paymentReceipt == null || !_paymentReceipt.Any()) return;

            int newIndex = _currentPRIndex + step;
            if (newIndex >= 0 && newIndex < _paymentReceipt.Count)
            {
                _currentPRIndex = newIndex;
                ShowCurrentRecord();
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

        private async void btn_search_Click(object sender, EventArgs e)
        {
            if (_paymentReceipt == null || _paymentReceipt.Count == 0)
            {
                await LoadPaymentReceipts();
            }

            using (var searchForm = new PaymentReceiptSearch())
            {
                if (searchForm.ShowDialog(this) == DialogResult.OK && !string.IsNullOrEmpty(searchForm.SelectedPRId))
                {
                    if (int.TryParse(searchForm.SelectedPRId, out int selectedId))
                    {
                        int index = _paymentReceipt.FindIndex(r => r.id == selectedId);
                        if (index >= 0)
                        {
                            _currentPRIndex = index;
                            await LoadPaymentReceipts();
                        }
                    }
                    else
                    {
                        Helpers.ShowDialogMessage("error", "Invalid record ID selected.");
                    }
                }
            }
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            // Save current index before clearing
            _previousPRIndex = _currentPRIndex;
            SetEditMode(true, isNewMode: true);

            //Clear only the rows, keep columns
            _currentDetails = new BindingList<PaymentReceiptDetailsModel>();
            dgv_main.AutoGenerateColumns = false;
            dgv_main.DataSource = _currentDetails;
            Helpers.ResetControls(pnl_main);
        }

        private async void btn_cancel_Click(object sender, EventArgs e)
        {
            SetEditMode(false);

            // If no records exist, clear everything
            if (_paymentReceipt == null || !_paymentReceipt.Any())
            {
                ClearPaymentReceiptUI();
                return;
            }

            // Return to the previous record index if available
            if (_previousPRIndex >= 0 && _paymentReceipt != null && _paymentReceipt.Count > 0)
            {
                _currentPRIndex = _previousPRIndex;
                await LoadPaymentReceipts();
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

                string[] columnsToValidate = new[] { "doc_no", "due_date" };

                if (await Helpers.ValidateDataGridViewCells(dgv_main, columnsToValidate))
                    return;

                var paymentReceiptParent = Helpers.BuildModelFromPanels<PaymentReceiptModel>(new Panel[] { pnl_main });

                paymentReceiptParent.prepared_by = _userName;
                paymentReceiptParent.check_type = "LOCAL";

                // Check Amount
                decimal checkAmount = 0;
                decimal.TryParse(txt_check_amount.Text, out checkAmount);

                if (checkAmount == 0)
                {
                    paymentReceiptParent.check_date = null;   // Make sure this is nullable DateTime?
                }

                var paymentReceiptDetails = Helpers.DatagridviewMapper.BuildModelsFromData<PaymentReceiptDetailsModel>(dgv_main).Where(x => x.isSelected).ToList();

                // Custom validation
                if (!ValidatePaymentReceipt(paymentReceiptParent, paymentReceiptDetails))
                {
                    return;
                }

                // Wrap everything into Payment Receipt Payload
                var prPayload = new PaymentReceiptPayload
                {
                    payment_receipt = paymentReceiptParent,
                    payment_receipt_details = paymentReceiptDetails,
                };

                Helpers.Loading.ShowLoading(dgv_main, "Saving data...");

                var result = await paymentReceiptService.CreatePaymentReceiptRecord(prPayload);

                if (!result.success)
                {
                    Helpers.ShowDialogMessage("error", "Payment Receipt not created.");
                    return;
                }

                Helpers.ShowDialogMessage("success", "Payment Receipt created successfully.");

                SetEditMode(false);
                await LoadPaymentReceipts();
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

        private bool ValidatePaymentReceipt(PaymentReceiptModel paymentReceiptParent, List<PaymentReceiptDetailsModel> paymentReceiptDetails)
        {
            // Validate parent
            if (paymentReceiptParent == null)
            {
                Helpers.ShowDialogMessage("error", "Invalid payment receipt data.");
                return false;
            }

            // Validate details existence
            if (paymentReceiptDetails == null || paymentReceiptDetails.Count == 0)
            {
                Helpers.ShowDialogMessage("error", "Please have at least one payment receipt.");
                return false;
            }

            bool hasPositiveAmountApplied = false;

            foreach (var detail in paymentReceiptDetails)
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
            if (!paymentReceiptDetails.Any(x => x.amount_applied > 0))
            {
                Helpers.ShowDialogMessage("error",
                    "Please enter at least one Amount Applied greater than zero.");
                return false;
            }

            return true;
        }

        private async void PaymentReceiptPage_Load(object sender, EventArgs e)
        {
            try
            {
                Helpers.Loading.ShowLoading(dgv_main, "Fetching data...");
                await LoadPaymentReceipts();
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

        private async Task LoadPaymentReceipts()
        {
            // save current index before reload
            int oldIndex = _currentPRIndex;

            //fill this declared value by the payment receipt data
            _prdata = await paymentReceiptService.GetAsModel();

            // Reverse order so newest records appear first
            _prdata.payment_receipt.Reverse();

            if (_prdata != null && _prdata.payment_receipt != null && _prdata.payment_receipt.Count > 0)
            {
                //set this variable to the parent of the payment receipt
                _paymentReceipt = _prdata.payment_receipt;

                // restore old index if valid, otherwise fallback to 0
                if (oldIndex >= 0 && oldIndex < _paymentReceipt.Count)
                    _currentPRIndex = oldIndex;
                else
                    _currentPRIndex = 0;

                ShowCurrentRecord();
            }
            else
            {
                ClearPaymentReceiptUI();
            }
        }

        private void ShowCurrentRecord()
        {
            if (_currentPRIndex < 0 || _prdata == null || _prdata.payment_receipt == null || !_prdata.payment_receipt.Any())
                return;

            // Convert payment receipt list to DataTable using helper
            _prTable = Helpers.ToDataTable(_prdata.payment_receipt);

            Helpers.BindControls(new Panel[] { pnl_main }, _prTable, _currentPRIndex);

            //Disable auto column generation before setting the data source
            dgv_main.AutoGenerateColumns = false;

            var current = _paymentReceipt[_currentPRIndex];

            //Bind child details (grids)
            if (_prdata?.payment_receipt_details != null)
            {
                _currentDetails = new BindingList<PaymentReceiptDetailsModel>(
                    _prdata.payment_receipt_details
                        .Where(d => d.payment_receipt_id == current.id)
                        .ToList()
                );

                dgv_main.DataSource = _currentDetails;
            }
            else
            {
                dgv_main.DataSource = null;
            }

            //Enable/disable navigation buttons
            btn_prev.Enabled = _currentPRIndex > 0;
            btn_next.Enabled = _currentPRIndex < _paymentReceipt.Count - 1;
        }

        private void ClearPaymentReceiptUI()
        {
            _paymentReceipt = new List<PaymentReceiptModel>();
            _currentPRIndex = -1;
            _previousPRIndex = -1;

            // Clear panel fields
            Helpers.ResetControls(new Panel[] { pnl_main });

            // Clear grid
            dgv_main.DataSource = null;
            dgv_main.Rows.Clear();

            // Disable navigation buttons
            btn_prev.Enabled = false;
            btn_next.Enabled = false;
        }

        private async void btn_customer_Click(object sender, EventArgs e)
        {
            using (var modal = new SalesInvoiceCustomer())
            {
                if (modal.ShowDialog(this) == DialogResult.OK && modal.SelectedCustomer != null)
                {
                    try
                    {
                        DataRow row = modal.SelectedCustomer.Rows[0];

                        // Assign values to controls
                        txt_customer_id.Text = row["customer_id"]?.ToString();
                        txt_customer.Text = row["customer"]?.ToString();
                        txt_customer_code.Text = row["customer_code"]?.ToString();
                        txt_overpayment_amount.Text = row["overpayment_amount"]?.ToString();
                        txt_currency.Text = _currencyCode;

                        generalSalesInvoiceView = new GeneralService<SalesInvoiceReceiptView>(
                            ApiEndPoints.SALES_INVOICE_RECEIPT + txt_customer_id.Text
                        );

                        _salesViewTable = await generalSalesInvoiceView.GetAsDatatable();

                        if (_salesViewTable == null || _salesViewTable.Rows.Count == 0)
                        {
                            _salesViewTable = new DataTable();
                            Helpers.ShowDialogMessage("error", "No sales invoices found for this customer.");
                        }
                    }
                    catch (Exception)
                    {
                        // Handle the error properly
                        _salesViewTable = new DataTable(); // prevent null reference issues
                        Helpers.ShowDialogMessage("error", "No sales invoices found for this customer.");
                    }

                    dgv_main.DataSource = null;
                    dgv_main.Rows.Clear();

                    // Bind SelectedRows to dgv_main
                    foreach (DataRow svRow in _salesViewTable.Rows)
                    {
                        int newRowIndex = dgv_main.Rows.Add();
                        var targetRow = dgv_main.Rows[newRowIndex];

                        targetRow.Cells["sales_invoice_id"].Value = svRow["sales_invoice_id"];
                        targetRow.Cells["doc_no"].Value = svRow["doc_no"];
                        targetRow.Cells["doc_date"].Value = svRow["doc_date"];
                        targetRow.Cells["due_date"].Value = svRow["due_date"];
                        targetRow.Cells["twas_applied"].Value = svRow["twas_applied"];
                        targetRow.Cells["open_amount"].Value = svRow["open_amount"];
                        targetRow.Cells["amount_applied"].ReadOnly = true;
                        targetRow.Cells["amount_applied"].Value = 0;
                    }

                    dgv_main.Refresh();
                }
            }
        }

        private void dgv_main_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            Helpers.HandleNumericColumns(dgv_main, e, new[] { "amount_applied" });
        }

        private void UpdateTransactionAmount()
        {
            if (!_isEditing)
                return;

            decimal cashAmount = 0m;
            decimal checkAmount = 0m;

            decimal.TryParse(txt_cash_amount.Text, out cashAmount);
            decimal.TryParse(txt_check_amount.Text, out checkAmount);

            decimal total = checkAmount + cashAmount;

            txt_transaction_amount.Text = total.ToString("C2", System.Globalization.CultureInfo.GetCultureInfo("en-PH"));
            txt_transaction_amount.AccessibleDescription = total.ToString(); // Store full precise value
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

            decimal.TryParse(txt_cash_amount.Text, out cash);
            decimal.TryParse(txt_check_amount.Text, out check);

            return cash + check;
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

        private void txt_cash_amount_TextChanged(object sender, EventArgs e)
        {
            UpdateTransactionAmount();
            UpdateUnappliedAmount();
        }

        private void txt_check_amount_TextChanged(object sender, EventArgs e)
        {
            bool hasValue = !string.IsNullOrWhiteSpace(txt_check_amount.Text) &&
                    txt_check_amount.Text != "0" &&
                    txt_check_amount.Text != "0.00";

            if (hasValue)
            {
                // Add REQUIRED tag
                cmb_bank_code.Tag = "REQUIRED";
                txt_bank_branch.Tag = "REQUIRED";
                txt_check_no.Tag = "REQUIRED";
                txt_check_type.Tag = "REQUIRED";
                dtp_check_date.Tag = "REQUIRED";
            }
            else
            {
                // Remove REQUIRED tag
                cmb_bank_code.Tag = null;
                txt_bank_branch.Tag = null;
                txt_check_no.Tag = null;
                txt_check_type.Tag = null;
                dtp_check_date.Tag = null;
            }

            UpdateTransactionAmount();
            UpdateUnappliedAmount();
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

            var row = dgv_main.Rows[e.RowIndex];

            //When checkbox changes
            if (dgv_main.Columns[e.ColumnIndex].Name == "isSelected")
            {
                bool isChecked = false;

                if (row.Cells["isSelected"].Value != null)
                    bool.TryParse(row.Cells["isSelected"].Value.ToString(), out isChecked);

                // Enable/disable amount_applied for this row
                row.Cells["amount_applied"].ReadOnly = !isChecked;

                if (!isChecked)
                {
                    // Clear values when unchecked
                    row.Cells["amount_applied"].Value = 0;
                    row.Cells["balance"].Value = row.Cells["open_amount"].Value;
                }

                UpdateUnappliedAmount();
            }

            // When amount_applied changes
            if (dgv_main.Columns[e.ColumnIndex].Name == "amount_applied")
            {
                decimal openAmount = 0m;
                decimal amountApplied = 0m;

                if (row.Cells["open_amount"].Value != null)
                    decimal.TryParse(row.Cells["open_amount"].Value.ToString(), out openAmount);

                if (row.Cells["amount_applied"].Value != null)
                    decimal.TryParse(row.Cells["amount_applied"].Value.ToString(), out amountApplied);

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
    }
}
