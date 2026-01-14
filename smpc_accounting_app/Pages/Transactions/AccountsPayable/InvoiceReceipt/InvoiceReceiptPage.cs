using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using smpc_accounting_app.Pages.Transactions.AccountsPayable.InvoiceReceipt.InvoiceReceiptModals;
using smpc_accounting_app.Services.Helpers;
using smpc_accounting_app.Services;
using smpc_accounting_app.Models;
using smpc_accounting_app.Services.Transactions;
using smpc_accounting_app.Shared;
using smpc_accounting_app.Pages.Transactions.Journal.JournalEntry;

namespace smpc_accounting_app.Pages.Transactions.AccountsPayable.InvoiceReceipt
{
    public partial class InvoiceReceiptPage : UserControl
    {
        InvoiceReceiptService invoiceReceiptService = new InvoiceReceiptService();
        GeneralService<ChartOfAccountsModel> serviceSetup;
        private string placeHolderText = "Invoice Receipt Search...";
        private int _currentIRIndex = -1;
        private int _previousIRIndex = -1;
        private InvoiceReceiptList _irdata;
        private List<InvoiceReceiptModel> _invoiceReceipts;
        private DataTable _irTable;
        private BindingList<InvoiceReceiptDetailsModel> _currentDetails;
        private CompanySetupModel _companySetup = CacheData.CompanySetup;

        private bool _isNewMode = false;
        private bool _isEditMode = false;
        public InvoiceReceiptPage()
        {
            InitializeComponent();
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
            _isEditMode = !isNewMode && enable;

            SetEditableColumns(enable);
            dgv_main.AllowUserToAddRows = enable;
            btn_reference_po.Enabled = enable;
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
                "txt_payment_term", "txt_currency","txt_supplier_name", "txt_invoice_type", "txt_type", "txt_ap_voucher" });
        }

        private void ChangeRecord(int step)
        {
            if (_invoiceReceipts == null || !_invoiceReceipts.Any()) return;

            int newIndex = _currentIRIndex + step;
            if (newIndex >= 0 && newIndex < _invoiceReceipts.Count)
            {
                _currentIRIndex = newIndex;
                ShowCurrentRecord();
            }
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            // Save current index before clearing
            _previousIRIndex = _currentIRIndex;
            SetEditMode(true, isNewMode: true);

            //Clear only the rows, keep columns
            dgv_main.DataSource = null;
            dgv_main.Rows.Clear();
            Helpers.ResetControls(pnl_main);
        }

        private async void btn_search_Click(object sender, EventArgs e)
        {
            if (_invoiceReceipts == null || _invoiceReceipts.Count == 0)
            {
                await LoadInvoiceReceipts();
            }

            using (var searchForm = new InvoiceSearch())
            {
                if (searchForm.ShowDialog(this) == DialogResult.OK && !string.IsNullOrEmpty(searchForm.SelectedIRId))
                {
                    if (int.TryParse(searchForm.SelectedIRId, out int selectedId))
                    {
                        int index = _invoiceReceipts.FindIndex(r => r.id == selectedId);
                        if (index >= 0)
                        {
                            _currentIRIndex = index;
                            await LoadInvoiceReceipts();
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
            dgv_main.EndEdit();

            // Validate required controls in selected panel
            bool hasError = Helpers.ValidateControlsValues(pnl_main);

            if (hasError) // if validation failed
            {
                Helpers.ShowDialogMessage("error", "Please fill in all required fields.");
                return;
            }

            string[] columnsToValidate = new[] { "item_code", "item_description", "item_qty", "unit_price", "total_cost", "discount", "line_amount" };

            if (await Helpers.ValidateDataGridViewCells(dgv_main, columnsToValidate))
                return;

            var invoiceReceiptParent = Helpers.BuildModelFromPanels<InvoiceReceiptModel>(new Panel[] { pnl_main });
            var invoiceReceiptDetails = Helpers.DatagridviewMapper.BuildModelsFromData<InvoiceReceiptDetailsModel>(dgv_main);

            //Check if invoice receipt details is null or empty
            if (invoiceReceiptDetails == null || invoiceReceiptDetails.Count == 0)
            {
                Helpers.ShowDialogMessage("error", "Please select at least one item.");
                return;
            }

            // Wrap everything into Item Request Payload
            var irPayload = new InvoiceReceiptPayload
            {
                invoice_receipt = invoiceReceiptParent,
                invoice_receipt_details = invoiceReceiptDetails,
            };

            try
            {
                Helpers.Loading.ShowLoading(dgv_main, "Saving data...");

                if (_isNewMode)
                {
                    var result = await invoiceReceiptService.CreateInvoiceReceiptRecord(irPayload);
                    Helpers.ShowDialogMessage("success", "Invoice Receipt created successfully.");
                }
                else
                {
                    var result = await invoiceReceiptService.UpdateInvoiceReceiptRecord(irPayload);
                    Helpers.ShowDialogMessage("success", "Invoice Receipt updated successfully.");
                }
            }
            catch (Exception ex)
            {
                Helpers.ShowDialogMessage("error", $"Failed to save: {ex.Message}");
            }
            finally
            {
                SetEditMode(false);
                await LoadInvoiceReceipts();

                Helpers.Loading.HideLoading(dgv_main);
            }
        }

        private async void btn_cancel_Click(object sender, EventArgs e)
        {
            SetEditMode(false);

            // Return to the previous record index if available
            if (_previousIRIndex >= 0 && _invoiceReceipts != null && _invoiceReceipts.Count > 0)
            {
                _currentIRIndex = _previousIRIndex;
                await LoadInvoiceReceipts();
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

        private async void InvoiceReceiptPage_Load(object sender, EventArgs e)
        {
            try
            {
                Helpers.Loading.ShowLoading(dgv_main, "Fetching data...");
                await LoadInvoiceReceipts();
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

        private void ClearInvoiceReceiptUI()
        {
            _invoiceReceipts = new List<InvoiceReceiptModel>();
            _currentIRIndex = -1;
            _previousIRIndex = -1;

            // Clear panel fields
            Helpers.ResetControls(new Panel[] { pnl_main });

            // Clear grid
            dgv_main.DataSource = null;
            dgv_main.Rows.Clear();

            // Disable navigation buttons
            btn_prev.Enabled = false;
            btn_next.Enabled = false;
        }

        private async Task LoadInvoiceReceipts()
        {
            // save current index before reload
            int oldIndex = _currentIRIndex;

            //fill this declared value by the invoice receipt data
            _irdata = await invoiceReceiptService.GetAsModel();

            // Reverse order so newest records appear first
            _irdata.invoice_receipt.Reverse();

            if (_irdata != null && _irdata.invoice_receipt != null && _irdata.invoice_receipt.Count > 0)
            {
                //set this variable to the parent of the invoice receipt
                _invoiceReceipts = _irdata.invoice_receipt;

                // restore old index if valid, otherwise fallback to 0
                if (oldIndex >= 0 && oldIndex < _invoiceReceipts.Count)
                    _currentIRIndex = oldIndex;
                else
                    _currentIRIndex = 0;

                ShowCurrentRecord();
            }
            else
            {
                ClearInvoiceReceiptUI();
            }
        }

        private void ShowCurrentRecord()
        {
            if (_currentIRIndex < 0 || _irdata == null || _irdata.invoice_receipt == null || !_irdata.invoice_receipt.Any())
                return;

            // Convert receiving report list to DataTable using helper
            _irTable = Helpers.ToDataTable(_irdata.invoice_receipt);

            Helpers.BindControls(new Panel[] { pnl_main }, _irTable, _currentIRIndex);

            //Disable auto column generation before setting the data source
            dgv_main.AutoGenerateColumns = false;

            var current = _invoiceReceipts[_currentIRIndex];

            //Bind child details (grids)
            if (_irdata?.invoice_receipt_details != null)
            {
                _currentDetails = new BindingList<InvoiceReceiptDetailsModel>(
                    _irdata.invoice_receipt_details
                        .Where(d => d.invoice_receipt_id == current.id)
                        .ToList()
                );

                dgv_main.DataSource = _currentDetails;
            }
            else
            {
                dgv_main.DataSource = null;
            }

            //Enable/disable navigation buttons
            btn_prev.Enabled = _currentIRIndex > 0;
            btn_next.Enabled = _currentIRIndex < _invoiceReceipts.Count - 1;
        }
    }
}
