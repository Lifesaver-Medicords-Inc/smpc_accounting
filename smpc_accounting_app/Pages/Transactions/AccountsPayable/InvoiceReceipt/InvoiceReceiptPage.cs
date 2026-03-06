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

namespace smpc_accounting_app.Pages.Transactions.AccountsPayable.InvoiceReceipt
{
    public partial class InvoiceReceiptPage : UserControl
    {
        InvoiceReceiptService invoiceReceiptService = new InvoiceReceiptService();
        GeneralService<TaxViewModel> generalServiceTaxView;
        private int _currentIRIndex = -1;
        private int _previousIRIndex = -1;
        private InvoiceReceiptList _irdata;
        private List<InvoiceReceiptModel> _invoiceReceipts;
        private DataTable _irTable;
        private BindingList<InvoiceReceiptDetailsModel> _currentDetails;
        private CompanySetupModel _companySetup = CacheData.CompanySetup;
        private DataTable _taxSetupTable;
        private bool _isNewMode = false;
        private string _userName;
        private bool _isEditing;

        public InvoiceReceiptPage()
        {
            InitializeComponent();

            Helpers.NumericTextBox.HandleNumericTextBox(new TextBox[] { txt_other_charges, txt_net_amount, txt_twas_amount }, '.');
            Helpers.TextboxFormatter.TextboxDecimalFormat(new[] { txt_other_charges, txt_net_amount, txt_twas_amount });
            _userName = CacheData.CurrentUser.first_name + " " + CacheData.CurrentUser.last_name;
            Helpers.DataGridViewFormatter.DataGridViewDecimalFormat(dgv_main, new[] { "discount", "unit_price", "total_cost", "line_amount" });
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
            _isEditing = enable;

            SetEditableColumns(enable);
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
                "txt_payment_term", "txt_currency","txt_supplier", "txt_invoice_type", "txt_type", "txt_ap_voucher",
                "txt_twas_amount", "txt_net_amount", "dtp_doc_date", "txt_reference_po" });
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
            _currentDetails = new BindingList<InvoiceReceiptDetailsModel>();
            dgv_main.AutoGenerateColumns = false;
            dgv_main.DataSource = _currentDetails;
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
            btn_save.Enabled = false;
            btn_cancel.Enabled = false;

            try
            {
                dgv_main.EndEdit();

                // Validate discount column values
                foreach (DataGridViewRow dgRow in dgv_main.Rows)
                {
                    if (dgRow.IsNewRow) continue;

                    var discountCell = dgRow.Cells["discount"];

                    if (discountCell?.Value != null &&
                        !string.IsNullOrWhiteSpace(discountCell.Value.ToString()))
                    {
                        if (!float.TryParse(discountCell.Value.ToString(), out _))
                        {
                            Helpers.ShowDialogMessage(
                                "error",
                                "One or more discount values are invalid. Please enter a valid number."
                            );

                            dgv_main.CurrentCell = discountCell;
                            dgv_main.BeginEdit(true);
                            return;
                        }
                    }
                }

                // Validate Other Charges textbox
                if (!string.IsNullOrWhiteSpace(txt_other_charges.Text))
                {
                    if (!float.TryParse(txt_other_charges.Text, out _))
                    {
                        Helpers.ShowDialogMessage(
                            "error",
                            "Other Charges must be a valid numeric value."
                        );

                        txt_other_charges.Focus();
                        txt_other_charges.SelectAll();
                        return;
                    }
                }

                // Validate required controls in selected panel
                bool hasError = Helpers.ValidateControlsValues(pnl_main);

                if (hasError) // if validation failed
                {
                    Helpers.ShowDialogMessage("error", "Please fill in all required fields.");
                    return;
                }

                string[] columnsToValidate = new[] { "item_code", "item_description", "req_qty", "unit_price", "total_cost", "line_amount" };

                if (await Helpers.ValidateDataGridViewCells(dgv_main, columnsToValidate))
                    return;

                var invoiceReceiptParent = Helpers.BuildModelFromPanels<InvoiceReceiptModel>(new Panel[] { pnl_main });

                //get tax_code_id FROM COMBOBOX
                if (cmb_tax_code.SelectedItem is DataRowView row)
                {
                    invoiceReceiptParent.tax_code_id = Convert.ToInt32(row["view_id"]);
                }
                else
                {
                    invoiceReceiptParent.tax_code_id = 0;
                }

                invoiceReceiptParent.prepared_by = _userName;

                var invoiceReceiptDetails = Helpers.DatagridviewMapper.BuildModelsFromData<InvoiceReceiptDetailsModel>(dgv_main);

                //Check if invoice receipt details is null or empty
                if (invoiceReceiptDetails == null || invoiceReceiptDetails.Count == 0)
                {
                    Helpers.ShowDialogMessage("error", "Please select at least one item.");
                    return;
                }

                // Wrap everything into Invoice Receipt Payload
                var irPayload = new InvoiceReceiptPayload
                {
                    invoice_receipt = invoiceReceiptParent,
                    invoice_receipt_details = invoiceReceiptDetails,
                };

                Helpers.Loading.ShowLoading(dgv_main, "Saving data...");

                var result = await invoiceReceiptService.CreateInvoiceReceiptRecord(irPayload);

                if (!result.success)
                {
                    Helpers.ShowDialogMessage("error", "Invoice Receipt not created.");
                    return;
                }

                Helpers.ShowDialogMessage("success", "Invoice Receipt created successfully.");

                SetEditMode(false);
                await LoadInvoiceReceipts();
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
            if (_invoiceReceipts == null || !_invoiceReceipts.Any())
            {
                ClearInvoiceReceiptUI();
                return;
            }

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
                await LoadTaxSetup();
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

        private async Task LoadTaxSetup()
        {
            generalServiceTaxView = new GeneralService<TaxViewModel>(ApiEndPoints.INVOICE_RECEIPT_TAX);
            _taxSetupTable = await generalServiceTaxView.GetAsDatatable();

            if (_taxSetupTable == null || _taxSetupTable.Rows.Count == 0)
                return;

            cmb_tax_code.DataSource = null;
            cmb_tax_code.DataBindings.Clear();
            cmb_tax_code.Items.Clear();

            cmb_tax_code.ValueMember = "view_id";
            cmb_tax_code.DisplayMember = "tax_desc";
            cmb_tax_code.DataSource = _taxSetupTable;

            cmb_tax_code.SelectedIndex = -1;
            cmb_tax_code.Text = "";
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

            // Convert invoice receipt list to DataTable using helper
            _irTable = Helpers.ToDataTable(_irdata.invoice_receipt);

            Helpers.BindControls(new Panel[] { pnl_main }, _irTable, _currentIRIndex);

            //Disable auto column generation before setting the data source
            dgv_main.AutoGenerateColumns = false;

            var current = _invoiceReceipts[_currentIRIndex];

            // set txt_ap_voucher based on boolean value
            txt_ap_voucher.Text = (current.ap_voucher ?? false) ? "Yes" : "No";

            // FORCE COMBOBOX BINDING REFRESH
            cmb_tax_code.BindingContext[cmb_tax_code.DataSource]?.EndCurrentEdit();
            cmb_tax_code.Refresh();

            //SET TAX CODE COMBOBOX
            if (current.tax_code_id > 0 && _taxSetupTable != null)
            {
                cmb_tax_code.SelectedValue = current.tax_code_id;

                if (cmb_tax_code.SelectedIndex == -1)
                    cmb_tax_code.SelectedIndex = -1;
            }
            else
            {
                cmb_tax_code.SelectedIndex = -1;
                cmb_tax_code.Text = string.Empty;
            }

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
                    txt_invoice_type.Text = row["invoice_type"]?.ToString();
                    txt_payment_term.Text = row["payment_term"]?.ToString();
                    txt_type.Text = row["type"]?.ToString();
                    txt_currency.Text = _companySetup.currency_code;

                    txt_reference_po.Text = string.Empty;

                    dgv_main.DataSource = null;
                    dgv_main.Rows.Clear();
                }
            }
        }

        private void btn_reference_po_Click(object sender, EventArgs e)
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

            using (var modal = new InvoiceSearchPO(supplierId))
            {
                if (modal.ShowDialog(this) == DialogResult.OK &&
                    modal.SelectedRows != null &&
                    modal.SelectedRows.Rows.Count > 0)
                {
                    //Bind SelectedRows to dgv_main
                    foreach (DataRow soRow in modal.SelectedRows.Rows)
                    {
                        int newRowIndex = dgv_main.Rows.Add();
                        var targetRow = dgv_main.Rows[newRowIndex];

                        targetRow.Cells["purchase_order_details_id"].Value = soRow["purchase_order_details_id"];
                        targetRow.Cells["item_code"].Value = soRow["item_code"];
                        targetRow.Cells["item_description"].Value = soRow["item_description"];
                        targetRow.Cells["req_qty"].Value = soRow["req_qty"];
                        targetRow.Cells["unit_price"].Value = soRow["unit_price"];
                        targetRow.Cells["total_cost"].Value = soRow["total_cost"];
                        targetRow.Cells["line_amount"].Value = soRow["total_cost"];
                    }

                    dgv_main.Refresh();

                    //Set Reference PO textbox
                    if (modal.SelectedPOLabels != null && modal.SelectedPOLabels.Any())
                    {
                        txt_reference_po.Text = string.Join(", ", modal.SelectedPOLabels);
                    }
                    else
                    {
                        txt_reference_po.Text = string.Empty;
                    }
                }
            }
        }

        private void dgv_main_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            Helpers.HandleNumericColumns(dgv_main, e, new[] { "discount" }, '.');
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
            if (!_isEditing)
                return;

            if (e.RowIndex < 0) return;

            var dgRow = dgv_main.Rows[e.RowIndex];

            string totalCostCol = "total_cost";
            string discountCol = "discount";
            string lineAmountCol = "line_amount";

            if (dgv_main.Columns[e.ColumnIndex].Name == totalCostCol ||
                dgv_main.Columns[e.ColumnIndex].Name == discountCol)
            {
                decimal totalCost = 0m;
                decimal discount = 0m;

                if (dgRow.Cells[totalCostCol].Value != null)
                    decimal.TryParse(dgRow.Cells[totalCostCol].Value.ToString(), out totalCost);

                if (dgRow.Cells[discountCol].Value != null &&
                    !string.IsNullOrWhiteSpace(dgRow.Cells[discountCol].Value.ToString()))
                {
                    decimal.TryParse(dgRow.Cells[discountCol].Value.ToString(), out discount);
                }

                decimal lineAmount = discount <= 0 ? totalCost : totalCost - discount;
                dgRow.Cells[lineAmountCol].Value = Math.Max(Helpers.ZeroIfNearZero(lineAmount), 0);
            }

            //Re-assign binding list as datasource
            if (_currentDetails != null)
            {
                dgv_main.DataSource = null;
                dgv_main.DataSource = _currentDetails;
            }

            if (_isNewMode)
            {
                // Update net amount
                UpdateNetAmount();
            }
        }

        private void UpdateNetAmount()
        {
            if (!_isEditing)
                return;

            decimal totalLineAmount = 0m;
            decimal otherCharges = 0m;

            // Sum all line_amounts from dgv_main
            foreach (DataGridViewRow dgRow in dgv_main.Rows)
            {
                if (dgRow.IsNewRow) continue;

                if (dgRow.Cells["line_amount"]?.Value != null &&
                    decimal.TryParse(dgRow.Cells["line_amount"].Value.ToString(), out decimal lineAmount))
                {
                    totalLineAmount += Helpers.ZeroIfNearZero(lineAmount);
                }
            }

            // Parse Other Charges (default to 0 if empty or invalid)
            if (!string.IsNullOrWhiteSpace(txt_other_charges.Text))
            {
                decimal.TryParse(txt_other_charges.Text, out otherCharges);
            }

            // Determine TWAS Amount based on tax code
            decimal twasAmount = 0m;
            if (cmb_tax_code.SelectedValue != null && int.TryParse(cmb_tax_code.SelectedValue.ToString(), out int taxId))
            {
                if (taxId == 10006)
                {
                    // Only divide by 1.12 if tax code is 10006
                    twasAmount = Helpers.ZeroIfNearZero((totalLineAmount / 1.12m) * 0.10m);
                }
                else
                {
                    // For other tax codes, do not divide
                    twasAmount = Helpers.ZeroIfNearZero(totalLineAmount * 0.10m);
                }
            }
            else
            {
                // For other tax codes, do not divide
                twasAmount = Helpers.ZeroIfNearZero(totalLineAmount * 0.10m);
            }

            // Compute Net Amount
            decimal netAmount = Helpers.ZeroIfNearZero((totalLineAmount - twasAmount) - otherCharges);

            // Prevent negative display
            if (twasAmount < 0) twasAmount = 0m;
            if (netAmount < 0) netAmount = 0m;

            // Display full precision
            txt_twas_amount.Text = twasAmount.ToString("C2", System.Globalization.CultureInfo.GetCultureInfo("en-PH"));
            txt_twas_amount.AccessibleDescription = twasAmount.ToString(); // Store full precise value

            txt_net_amount.Text = netAmount.ToString("C2", System.Globalization.CultureInfo.GetCultureInfo("en-PH"));
            txt_net_amount.AccessibleDescription = netAmount.ToString(); // Store full precise value
        }

        private void txt_other_charges_TextChanged(object sender, EventArgs e)
        {
            if (_isNewMode)
            {
                UpdateNetAmount();
            }
        }

        private void btn_ap_voucher_Click(object sender, EventArgs e)
        {
            try
            {
                // Disable button immediately
                btn_ap_voucher.Enabled = false;

                var layout = this.FindForm() as Layout;

                if (layout != null)
                {
                    layout.OpenRoute("AP Voucher"); // route key
                }
            }
            catch (Exception ex)
            {
                // Re-enable only if something failed
                btn_ap_voucher.Enabled = true;

                Helpers.ShowDialogMessage("error", $"Failed to open AP Voucher: {ex.Message}");
            }
        }

        private void txt_twas_amount_TextChanged(object sender, EventArgs e)
        {
            if (_isNewMode)
            {
                UpdateNetAmount();
            }
        }

        private void cmb_tax_code_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isNewMode)
            {
                UpdateNetAmount();
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
