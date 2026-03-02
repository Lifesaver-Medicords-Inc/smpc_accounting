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
        private BindingList<PaymentReceiptDetailsModel> _currentDetails;
        private CompanySetupModel _companySetup = CacheData.CompanySetup;
        private bool _isNewMode = false;
        private string _userName;
        private string _currencyCode;

        public PaymentReceiptPage()
        {
            InitializeComponent();

            Helpers.NumericTextBox.HandleNumericTextBox(new TextBox[] { txt_cash_amount, txt_transaction_amount, txt_unapplied_amount, txt_check_amount}, '.');
            _userName = CacheData.CurrentUser.first_name + " " + CacheData.CurrentUser.last_name;
            Helpers.TextboxFormatter.TextboxDecimalFormat(new[] { txt_cash_amount, txt_transaction_amount, txt_unapplied_amount, txt_check_amount });
            Helpers.DataGridViewFormatter.DataGridViewDecimalFormat(dgv_main, new[] { "open_amount", "amount_applied", "twas_applied", "balance" });
            _currencyCode = CacheData.CompanySetup.currency_code;
        }

        private void SetEditableColumns(bool isEdit)
        {
            var editableColumns = new[] { "amount_applied" };

            foreach (var colName in editableColumns)
            {
                if (dgv_main.Columns.Contains(colName))
                    dgv_main.Columns[colName].ReadOnly = !isEdit;
            }
        }

        private void SetEditMode(bool enable, bool isNewMode = false)
        {
            _isNewMode = isNewMode;

            btn_customer.Enabled = enable;

            SetEditableColumns(enable);

            // buttons
            string[] editButtons = { "btn_save", "btn_cancel" };
            string[] navButtons = { "btn_new", "btn_print", "btn_edit", "btn_delete", "btn_next", "btn_prev", "btn_search" };

            Helpers.SetButtonVisibility(
                toolStrip1,
                visibleButtons: enable ? editButtons : navButtons,
                hiddenButtons: enable ? navButtons : editButtons
            );

            Helpers.SetChildControlsEnabled(new[] { pnl_main }, enable, new string[] { "txt_customer", "txt_customer_code",
                "txt_transaction_amount", "txt_unapplied_amount", "txt_doc_no", "txt_doc_date", "txt_currency", "txt_check_type" });
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
            dgv_main.DataSource = null;
            dgv_main.Rows.Clear();
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

                string[] columnsToValidate = new[] { "doc_no", "due_date", "amount_applied" };

                if (await Helpers.ValidateDataGridViewCells(dgv_main, columnsToValidate))
                    return;

                var paymentReceiptParent = Helpers.BuildModelFromPanels<PaymentReceiptModel>(new Panel[] { pnl_main });

                paymentReceiptParent.prepared_by = _userName;
                paymentReceiptParent.check_type = "LOCAL";

                var paymentReceiptDetails = Helpers.DatagridviewMapper.BuildModelsFromData<PaymentReceiptDetailsModel>(dgv_main);

                //Check if payment receipt details is null or empty
                if (paymentReceiptDetails == null || paymentReceiptDetails.Count == 0)
                {
                    Helpers.ShowDialogMessage("error", "Please select at least one item.");
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

        private void btn_customer_Click(object sender, EventArgs e)
        {
            using (var modal = new SalesInvoiceCustomer())
            {
                if (modal.ShowDialog(this) == DialogResult.OK && modal.SelectedCustomer != null)
                {
                    DataRow row = modal.SelectedCustomer.Rows[0];

                    // Assign values to controls
                    txt_customer_id.Text = row["customer_id"]?.ToString();
                    txt_customer.Text = row["customer"]?.ToString();
                    txt_customer_code.Text = row["customer_code"]?.ToString();
                    txt_currency.Text = _currencyCode;

                    dgv_main.DataSource = null;
                    dgv_main.Rows.Clear();
                }
            }
        }

        private void dgv_main_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            Helpers.HandleNumericColumns(dgv_main, e, new[] { "amount_applied" });
        }
    }
}
