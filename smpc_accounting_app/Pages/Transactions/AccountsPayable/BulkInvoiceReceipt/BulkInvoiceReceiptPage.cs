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
using smpc_accounting_app.Pages.Transactions.AccountsPayable.BulkInvoiceReceipt.BulkInvoiceReceiptModals;
using smpc_accounting_app.Printing;
using Microsoft.Reporting.WinForms;
using System.IO;

namespace smpc_accounting_app.Pages.Transactions.AccountsPayable.BulkInvoiceReceipt
{
    public partial class BulkInvoiceReceiptPage : UserControl
    {
        BulkInvoiceReceiptService bulkInvoiceReceiptService = new BulkInvoiceReceiptService();
        GeneralService<TaxViewModel> generalServiceTaxView;
        GeneralService<ChartOfAccountsModel> generalServiceChartOfAccounts;
        private List<ChartOfAccountsModel> _coadata;
        private int _currentBIRIndex = -1;
        private int _previousBIRIndex = -1;
        private BulkInvoiceReceiptList _birdata;
        private List<BulkInvoiceReceiptModel> _bulkInvoiceReceipts;
        private DataTable _birTable;
        private BindingList<BulkInvoiceReceiptDetailsModel> _currentDetails;
        private CompanySetupModel _companySetup = CacheData.CompanySetup;
        private DataTable _taxSetupTable;
        private bool _isNewMode = false;
        private string _userName;
        private bool _isEditing;
        private int? _nextCursor = null;
        private int? _currentCursor = null;
        private Stack<int?> _cursorHistory = new Stack<int?>(); // for going back
        private bool _hasNext = false;

        public BulkInvoiceReceiptPage()
        {
            InitializeComponent();

            _userName = CacheData.CurrentUser.first_name + " " + CacheData.CurrentUser.last_name;
            Helpers.NumericTextBox.HandleNumericTextBox(new TextBox[] { txt_other_charges, txt_net_amount, txt_twas_amount }, '.');
            Helpers.TextboxFormatter.TextboxDecimalFormat(new[] { txt_other_charges, txt_net_amount, txt_twas_amount });
            Helpers.DataGridViewFormatter.DataGridViewDecimalFormat(dgv_main, new[] { "line_amount" });
        }

        private void SetEditableColumns(bool isEdit)
        {
            var editableColumns = new[] { "cmb_account_code", "line_amount" };

            foreach (var colName in editableColumns)
            {
                if (dgv_main.Columns.Contains(colName))
                {
                    var column = dgv_main.Columns[colName];

                    column.ReadOnly = !isEdit;

                    // Toggle background color based on readonly state
                    column.DefaultCellStyle.BackColor = column.ReadOnly ? Color.Gainsboro : Color.White;
                }
            }
        }

        private void SetEditMode(bool enable, bool isNewMode = false)
        {
            _isNewMode = isNewMode;
            _isEditing = enable;

            dgv_main.AllowUserToAddRows = enable;

            SetEditableColumns(enable);
            btn_supplier.Enabled = enable;

            // buttons
            string[] editButtons = { "btn_save", "btn_cancel", "btn_supplier" };
            string[] navButtons = { "btn_new", "btn_print", "btn_edit", "btn_delete", "btn_next", "btn_prev", "btn_search" };

            Helpers.SetButtonVisibility(
                toolStrip1,
                pnl_main,
                visibleButtons: enable ? editButtons : navButtons,
                hiddenButtons: enable ? navButtons : editButtons
            );

            Helpers.SetChildControlsEnabled(new[] { pnl_main }, !enable, new string[] { "txt_doc_no", "txt_supplier_code",
                "txt_payment_term", "txt_currency","txt_supplier", "txt_invoice_type", "txt_type", "txt_ap_voucher",
                "txt_twas_amount", "txt_net_amount", "dtp_doc_date" });
        }

        private async void ChangeRecord(int step)
        {
            if (_bulkInvoiceReceipts == null || !_bulkInvoiceReceipts.Any()) return;

            int newIndex = _currentBIRIndex + step;

            if (newIndex >= 0 && newIndex < _bulkInvoiceReceipts.Count)
            {
                // Normal navigation within current page
                _currentBIRIndex = newIndex;
                ShowCurrentRecord();
            }
            else if (step > 0 && _hasNext)
            {
                // Moving forward — push current cursor to history before advancing
                _cursorHistory.Push(_currentCursor);
                _currentCursor = _nextCursor;
                _currentBIRIndex = 0;
                await LoadBulkInvoiceReceipts(_currentCursor);
            }
            else if (step < 0 && _cursorHistory.Count > 0)
            {
                // Moving backward — pop previous cursor from history
                _currentCursor = _cursorHistory.Pop();
                await LoadBulkInvoiceReceipts(_currentCursor);
                _currentBIRIndex = _bulkInvoiceReceipts.Count - 1;
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

        private void btn_new_Click(object sender, EventArgs e)
        {
            // Save current index before clearing
            _previousBIRIndex = _currentBIRIndex;
            Helpers.ResetControls(pnl_main);
            SetEditMode(true, isNewMode: true);

            //Clear only the rows, keep columns
            dgv_main.DataSource = null;
            dgv_main.Rows.Clear();
        }

        private async void btn_search_Click(object sender, EventArgs e)
        {
            if (_bulkInvoiceReceipts == null || _bulkInvoiceReceipts.Count == 0)
            {
                await LoadBulkInvoiceReceipts();
            }

            using (var searchForm = new BulkInvoiceSearch())
            {
                if (searchForm.ShowDialog(this) == DialogResult.OK && !string.IsNullOrEmpty(searchForm.SelectedBIRId))
                {
                    if (int.TryParse(searchForm.SelectedBIRId, out int selectedId))
                    {
                        int index = _bulkInvoiceReceipts.FindIndex(r => r.id == selectedId);

                        if (index >= 0)
                        {
                            // Found locally — just navigate
                            _currentBIRIndex = index;
                            ShowCurrentRecord();
                        }
                        else
                        {
                            // Push current cursor to history BEFORE clearing,
                            // so btn_prev can navigate back to where the user was
                            _cursorHistory.Push(_currentCursor);

                            _currentCursor = null;        // remove the Clear() call
                            _currentBIRIndex = 0;

                            await LoadBulkInvoiceReceipts(seekId: selectedId);
                        }
                    }
                    else
                    {
                        Helpers.ShowDialogMessage("error", "Invalid record ID selected.");
                    }
                }
            }
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            if (_currentBIRIndex < 0) return;

            var reportPath = Path.Combine(Application.StartupPath, "Printing", "AccountsPayables", "BulkInvoiceReceiptReport.rdlc");

            // DEBUG CHECK
            if (!File.Exists(reportPath))
            {
                MessageBox.Show("RDLC file not found:\n" + reportPath);
                return;
            }

            var dataSources = new List<ReportDataSource>()
            {
                new ReportDataSource("DataSet2", _currentDetails),
                new ReportDataSource("DataSet1", new List<BulkInvoiceReceiptModel> { _bulkInvoiceReceipts[_currentBIRIndex] })
            };

            var preview = new PrintPreview(reportPath, dataSources);

            preview.ShowDialog();
        }

        private async void btn_save_Click(object sender, EventArgs e)
        {
            btn_save.Enabled = false;
            btn_cancel.Enabled = false;

            try
            {
                dgv_main.EndEdit();

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

                string[] columnsToValidate = new[] { "payment_charge_code", "charge_description", "account_code", "line_amount" };

                if (await Helpers.ValidateDataGridViewCells(dgv_main, columnsToValidate))
                    return;

                var bulkInvoiceReceiptParent = Helpers.BuildModelFromPanels<BulkInvoiceReceiptModel>(new Panel[] { pnl_main });

                //get tax_code_id FROM COMBOBOX
                if (cmb_tax_code.SelectedItem is DataRowView row)
                {
                    bulkInvoiceReceiptParent.tax_code_id = Convert.ToInt32(row["view_id"]);
                }
                else
                {
                    bulkInvoiceReceiptParent.tax_code_id = 0;
                }

                bulkInvoiceReceiptParent.prepared_by = _userName;


                var bulkInvoiceReceiptDetails = Helpers.DatagridviewMapper.BuildModelsFromData<BulkInvoiceReceiptDetailsModel>(dgv_main);

                //Check if bulk invoice receipt details is null or empty
                if (bulkInvoiceReceiptDetails == null || bulkInvoiceReceiptDetails.Count == 0)
                {
                    Helpers.ShowDialogMessage("error", "Please select at least one item.");
                    return;
                }

                // Assign created_by and posting reference info for each detail
                foreach (DataGridViewRow rows in dgv_main.Rows)
                {
                    if (rows.IsNewRow) continue;

                    var detail = bulkInvoiceReceiptDetails[rows.Index];

                    // Get account_code_id and posting_ref from the ComboBox column
                    var accountCodeCell = rows.Cells["cmb_account_code"];
                    if (accountCodeCell.Value != null)
                    {
                        int accountCodeId = 0;
                        if (int.TryParse(accountCodeCell.Value.ToString(), out accountCodeId))
                        {
                            detail.account_id = accountCodeId;
                        }
                    }
                }

                // Wrap everything into Bulk Invoice Receipt Payload
                var birPayload = new BulkInvoiceReceiptPayload
                {
                    bulk_invoice_receipt = bulkInvoiceReceiptParent,
                    bulk_invoice_receipt_details = bulkInvoiceReceiptDetails,
                };

                Helpers.Loading.ShowLoading(dgv_main, "Saving data...");

                var result = await bulkInvoiceReceiptService.CreateBulkInvoiceReceiptRecord(birPayload);

                if (!result.success)
                {
                    Helpers.ShowDialogMessage("error", "Bulk Invoice Receipt not created.");
                    return;
                }

                Helpers.ShowDialogMessage("success", "Bulk Invoice Receipt created successfully.");

                SetEditMode(false);
                _currentCursor = null;
                await LoadBulkInvoiceReceipts();
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

            if (_bulkInvoiceReceipts == null || !_bulkInvoiceReceipts.Any())
            {
                ClearBulkInvoiceReceiptUI();
                return;
            }

            if (_previousBIRIndex >= 0)
            {
                _currentBIRIndex = _previousBIRIndex;
                await LoadBulkInvoiceReceipts(_currentCursor);
            }
            else
            {
                Helpers.ResetControls(pnl_main);
                dgv_main.Rows.Clear();
            }
        }

        private async void BulkInvoiceReceiptPage_Load(object sender, EventArgs e)
        {
            try
            {
                Helpers.Loading.ShowLoading(dgv_main, "Fetching data...");
                await LoadTaxSetup();
                await LoadBulkInvoiceReceipts();
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

        private void ClearBulkInvoiceReceiptUI()
        {
            _bulkInvoiceReceipts = new List<BulkInvoiceReceiptModel>();
            _currentBIRIndex = -1;
            _previousBIRIndex = -1;
            _nextCursor = null;
            _currentCursor = null;
            _cursorHistory.Clear();
            _hasNext = false;

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

        private async Task LoadBulkInvoiceReceipts(int? cursor = null, int? seekId = null)
        {
            var data = await bulkInvoiceReceiptService.GetAsModelPaginated(id: cursor, seekId: seekId);

            _birdata = data.Data;
            _hasNext = data.Pagination?.has_next ?? false;
            _nextCursor = _birdata?.bulk_invoice_receipt?.LastOrDefault()?.id;

            generalServiceChartOfAccounts = new GeneralService<ChartOfAccountsModel>(ApiEndPoints.CHART_OF_ACCOUNT_SETUP);
            _coadata = await generalServiceChartOfAccounts.GetAsList();

            if (_coadata != null && _coadata.Count > 0)
            {
                var postingRefColumn = (DataGridViewComboBoxColumn)dgv_main.Columns["cmb_account_code"];
                postingRefColumn.DataSource = _coadata;
                postingRefColumn.ValueMember = "id";
                postingRefColumn.DisplayMember = "code";
                postingRefColumn.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                postingRefColumn.FlatStyle = FlatStyle.Flat;
                postingRefColumn.DefaultCellStyle.NullValue = "";
            }

            if (_birdata != null && _birdata.bulk_invoice_receipt != null && _birdata.bulk_invoice_receipt.Count > 0)
            {
                _bulkInvoiceReceipts = _birdata.bulk_invoice_receipt;

                // Only reset to 0 if _currentBIRIndex is out of range
                if (_currentBIRIndex < 0 || _currentBIRIndex >= _bulkInvoiceReceipts.Count)
                    _currentBIRIndex = 0;

                ShowCurrentRecord();
            }
            else
            {
                ClearBulkInvoiceReceiptUI();
            }
        }

        private void ShowCurrentRecord()
        {
            if (_currentBIRIndex < 0 || _birdata == null || _birdata.bulk_invoice_receipt == null || !_birdata.bulk_invoice_receipt.Any())
                return;

            // Convert bulk invoice receipt list to DataTable using helper
            _birTable = Helpers.ToDataTable(_birdata.bulk_invoice_receipt);

            Helpers.BindControls(new Panel[] { pnl_main }, _birTable, _currentBIRIndex);

            // Format txt_doc_no with IR prefix and 8 digit number
            if (!string.IsNullOrEmpty(txt_doc_no.Text))
            {
                if (int.TryParse(txt_doc_no.Text, out int number))
                {
                    txt_doc_no.Text = "IR" + number.ToString("D8");
                }
            }

            //Disable auto column generation before setting the data source
            dgv_main.AutoGenerateColumns = false;

            var current = _bulkInvoiceReceipts[_currentBIRIndex];

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
            if (_birdata?.bulk_invoice_receipt_details != null)
            {
                _currentDetails = new BindingList<BulkInvoiceReceiptDetailsModel>(
                    _birdata.bulk_invoice_receipt_details
                        .Where(d => d.bulk_invoice_receipt_id == current.id)
                        .ToList()
                );

                dgv_main.DataSource = _currentDetails;

                foreach (DataGridViewRow row in dgv_main.Rows)
                {
                    if (row.IsNewRow) continue;

                    var detail = _currentDetails[row.Index];
                    if (detail.account_id > 0)
                    {
                        var postingRefColumn = dgv_main.Columns["cmb_account_code"] as DataGridViewComboBoxColumn;
                        if (postingRefColumn != null)
                        {
                            var matched = _coadata.FirstOrDefault(c => c.id == detail.account_id);
                            if (matched != null)
                            {
                                row.Cells["cmb_account_code"].Value = matched.id;
                            }
                        }
                    }
                }
            }
            else
            {
                dgv_main.DataSource = null;
            }

            //Enable/disable navigation buttons
            bool isFirstRecord = _currentBIRIndex == 0 && _cursorHistory.Count == 0;
            bool isLastRecord = _currentBIRIndex == _bulkInvoiceReceipts.Count - 1 && !_hasNext;

            btn_prev.Enabled = !isFirstRecord;
            btn_next.Enabled = !isLastRecord;
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
                    txt_supplier_address.Text = row["supplier_address"]?.ToString();
                    txt_currency.Text = _companySetup.currency_code;
                }
            }
        }

        private void dgv_main_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgv_main.IsCurrentCellDirty)
            {
                dgv_main.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgv_main_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            Helpers.HandleNumericColumns(dgv_main, e, new[] { "line_amount" }, '.');

            // Handle posting_ref keydown
            if (dgv_main.CurrentCell.OwningColumn.Name == "cmb_account_code"
                && e.Control is ComboBox combo)
            {
                // Avoid multiple subscriptions
                combo.KeyDown -= PostingRef_KeyDown;
                combo.KeyDown += PostingRef_KeyDown;
            }
        }

        private void PostingRef_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Delete &&
                e.KeyCode != Keys.Back &&
                e.KeyCode != Keys.Escape)
                return;

            if (!(sender is ComboBox combo))
                return;

            var row = dgv_main.CurrentRow;
            if (row == null)
                return;

            e.Handled = true;
            e.SuppressKeyPress = true;

            // Defer clearing until AFTER DataGridView commits
            BeginInvoke(new Action(() =>
            {
                row.Cells["cmb_account_code"].Value = DBNull.Value;

                row.Cells["payment_charge_code"].Value = null;

                row.Cells["charge_description"].Value = null;

                combo.SelectedIndex = -1;
                combo.Text = string.Empty;

                dgv_main.EndEdit();
            }));
        }

        private void dgv_main_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!_isEditing)
                return;

            if (e.RowIndex < 0)
                return;

            var row = dgv_main.Rows[e.RowIndex];
            var columnName = dgv_main.Columns[e.ColumnIndex].Name;

            if (columnName == "cmb_account_code")
            {
                var postingRefValue = row.Cells["cmb_account_code"].Value;

                if (int.TryParse(postingRefValue.ToString(), out int selectedId))
                {
                    var coa = _coadata.FirstOrDefault(c => c.id == selectedId);
                    if (coa != null)
                    {
                        row.Cells["payment_charge_code"].Value = coa.account_class;
                        row.Cells["charge_description"].Value = coa.name;
                        row.Cells["account_code"].Value = coa.code;
                    }
                }
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