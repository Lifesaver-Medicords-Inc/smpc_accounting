using smpc_accounting_app.Models;
using smpc_accounting_app.Services.Helpers;
using smpc_accounting_app.Services.Setup;
using smpc_accounting_app.Shared;
using smpc_sales_app.Services.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using smpc_accounting_app.Services;

namespace smpc_accounting_app.Pages.Setup.Tax
{
    public partial class InputVatPage : UserControl
    {
        public static DataTable ChartOfAccountPurchasing { get; set; } = new DataTable();
        public static DataTable ChartOfAccountSales { get; set; } = new DataTable();
        TaxSetupList setupList = new TaxSetupList();
        readonly ChartOfAccountsService chartOfAccountService = new ChartOfAccountsService();

        readonly TaxSetupService taxSetupService = new TaxSetupService();
        GeneralService<ChartOfAccountsViewModel> serviceSetup;
        private bool _isNewMode = false;
        private bool _isEditMode = false;
        private TaxList _taxdata;
        private DataTable _taxTable;
        private DataTable _coaTable;
        private string placeHolderText = "Tax Code Search...";

        public InputVatPage()
        {
            InitializeComponent();

            Helpers.Placeholder.SetPlaceholder(txt_search, placeHolderText);
            Helpers.AllowOnlyNumbers(txt_code);
        }

        private void SetEditableColumns(bool isEdit)
        {
            var editableColumns = new[] { "valid_from", "valid_to", "tax_rate" };

            foreach (var colName in editableColumns)
            {
                if (dgv_tax_details.Columns.Contains(colName))
                    dgv_tax_details.Columns[colName].ReadOnly = !isEdit;
            }
        }

        private void SetEditMode(bool enable, bool isNewMode = false)
        {
            SetEditableColumns(enable);
            _isNewMode = isNewMode;
            _isEditMode = !isNewMode && enable;
            dgv_tax_details.AllowUserToAddRows = enable;

            // buttons
            string[] editButtons = { "btn_save", "btn_cancel" };
            string[] navButtons = { "btn_new", "btn_print", "btn_edit", "btn_delete" };

            Helpers.SetButtonVisibility(
                toolStrip1,
                pnl_content,
                visibleButtons: enable ? editButtons : navButtons,
                hiddenButtons: enable ? navButtons : editButtons
            );

            pnl_content.Enabled = enable;
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            SetEditMode(true, isNewMode: true);
            Helpers.ResetControls(new Panel[] { pnl_content });

            //Clear only the rows, keep columns
            dgv_tax_details.DataSource = null;
            dgv_tax_details.Rows.Clear();
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            SetEditMode(true);
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            SetEditMode(false);
            LoadSelectedTaxSetup();
        }

        private async void btn_delete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_id.Text))
            {
                Helpers.ShowDialogMessage("error", "Please select a Tax Setup to delete.");
                return;
            }

            var confirm = MessageBox.Show($"Are you sure you want to delete this Tax Setup {txt_id.Text}?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            try
            {
                Helpers.Loading.ShowLoading(dgv_tax_details, "Deleting data...");
                Helpers.Loading.ShowLoading(dgv_tax_code_list, "Deleting data...");

                var taxModel = new TaxSetupModel
                {
                    id = int.Parse(txt_id.Text),
                };

                var taxPayload = new TaxSetupPayload
                {
                    tax_setup = taxModel
                };

                var result = await taxSetupService.DeleteTaxRecord(taxPayload);

                if (!result.success)
                {
                    Helpers.ShowDialogMessage("error", "Tax Setup not Deleted.");
                    return;
                }

                Helpers.ShowDialogMessage("success", "Tax Setup deleted successfully.");
            }
            catch (Exception ex)
            {
                Helpers.ShowDialogMessage("error", $"Failed to delete: {ex.Message}");
            }
            finally
            {
                await LoadTaxSetups();

                Helpers.Loading.HideLoading(dgv_tax_details);
                Helpers.Loading.HideLoading(dgv_tax_code_list);
            }
        }

        private async void btn_save_Click(object sender, EventArgs e)
        {
            btn_save.Enabled = false;
            btn_cancel.Enabled = false;

            try
            {
                dgv_tax_details.EndEdit();

                if (!await ValidateTaxSetupAsync())
                    return;

                var taxSetupParent = Helpers.BuildModelFromPanels<TaxSetupModel>(new Panel[] { pnl_content });
                taxSetupParent.input_tax_creditable = chk_input_tax_creditable.Checked;

                taxSetupParent.coa_sales_id = cmb_coa_sales.SelectedIndex != -1 ? Convert.ToInt32(cmb_coa_sales.SelectedValue) : 0;

                taxSetupParent.coa_purchase_id = cmb_coa_purchase.SelectedIndex != -1 ? Convert.ToInt32(cmb_coa_purchase.SelectedValue) : 0;

                var taxSetupDetails = Helpers.DatagridviewMapper.BuildModelsFromData<TaxDetailsModel>(dgv_tax_details);

                if (ValidateDateFields(taxSetupDetails, out string overlapError))
                {
                    Helpers.ShowDialogMessage("error", overlapError);
                    return;
                }

                taxSetupDetails = taxSetupDetails
                    .OrderBy(d =>
                        DateTime.ParseExact(
                            d.valid_from,
                            "MM/dd/yyyy",
                            CultureInfo.InvariantCulture))
                    .ToList();

                for (int i = 0; i < taxSetupDetails.Count - 1; i++)
                {
                    var current = taxSetupDetails[i];
                    var next = taxSetupDetails[i + 1];

                    if (string.IsNullOrWhiteSpace(current.valid_to))
                    {
                        current.valid_to = next.valid_from;
                    }
                }

                // Wrap everything into ReceivingReportPayload
                var taxPayload = new TaxSetupPayload
                {
                    tax_setup = taxSetupParent,
                    tax_setup_details = taxSetupDetails
                };

                Helpers.Loading.ShowLoading(dgv_tax_details, "Saving data...");
                Helpers.Loading.ShowLoading(dgv_tax_code_list, "Saving data...");

                if (_isNewMode && (txt_id.Text == null || txt_id.Text == ""))
                {
                    var result = await taxSetupService.CreateTaxRecord(taxPayload);

                    if (!result.success)
                    {
                        Helpers.ShowDialogMessage("error", "Tax Setup not Created.");
                        return;
                    }

                    Helpers.ShowDialogMessage("success", "Tax Setup created successfully.");
                }
                else
                {
                    var result = await taxSetupService.UpdateTaxRecord(taxPayload);

                    if (!result.success)
                    {
                        Helpers.ShowDialogMessage("error", "Tax Setup not Updated.");
                        return;
                    }

                    Helpers.ShowDialogMessage("success", "Tax Setup updated successfully.");
                }

                SetEditMode(false);
                await LoadTaxSetups();
            }
            catch (Exception ex)
            {
                Helpers.ShowDialogMessage("error", $"Failed to save: {ex.Message}");
            }
            finally
            {
                btn_save.Enabled = true;
                btn_cancel.Enabled = true;

                Helpers.Loading.HideLoading(dgv_tax_details);
                Helpers.Loading.HideLoading(dgv_tax_code_list);
            }
        }

        private async Task<bool> ValidateTaxSetupAsync()
        {
            // 1. Validate required controls in the panel
            if (Helpers.ValidateControlsValues(pnl_content))
            {
                Helpers.ShowDialogMessage("error", "Please fill in all required fields.");
                return false;
            }

            // 2. Validate that tax details are not empty
            var taxSetupDetails = Helpers.DatagridviewMapper.BuildModelsFromData<TaxDetailsModel>(dgv_tax_details);
            if (taxSetupDetails == null || taxSetupDetails.Count == 0)
            {
                Helpers.ShowDialogMessage("error", "Validity Period cannot be empty.");
                return false;
            }

            // 3. Validate required DataGridView columns
            string[] columnsToValidate = { "item_description", "req_qty" };
            if (await Helpers.ValidateDataGridViewCells(dgv_tax_details, columnsToValidate))
                return false;

            // 4. Validate that at least one COA is selected
            bool hasSales = cmb_coa_sales.SelectedIndex != -1 && !string.IsNullOrWhiteSpace(cmb_coa_sales.Text);
            bool hasPurchase = cmb_coa_purchase.SelectedIndex != -1 && !string.IsNullOrWhiteSpace(cmb_coa_purchase.Text);

            if (!hasSales && !hasPurchase)
            {
                Helpers.ShowDialogMessage("error", "Please select at least one Chart of Accounts: Sales or Purchase.");
                return false;
            }

            // 5. Validate unique Tax Code (NEW MODE ONLY)
            if (_isNewMode)
            {
                string enteredCode = txt_code.Text.Trim();

                if (_taxTable != null &&
                    _taxTable.AsEnumerable().Any(r =>
                        r.Field<string>("code")?.Equals(
                            enteredCode,
                            StringComparison.OrdinalIgnoreCase) == true))
                {
                    Helpers.ShowDialogMessage(
                        "error",
                        $"Tax Code '{enteredCode}' is already in use."
                    );
                    txt_code.Focus();
                    return false;
                }
            }

            return true;
        }

        private bool ValidateDateFields(List<TaxDetailsModel> details, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (details == null || details.Count == 0)
            {
                errorMessage = "Tax details cannot be empty.";
                return true;
            }

            const string dateFormat = "MM/dd/yyyy";
            var ranges = new List<(DateTime From, DateTime? To)>(); // To can be null

            foreach (var d in details)
            {
                if (!DateTime.TryParseExact(
                    d.valid_from,
                    dateFormat,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out DateTime from))
                {
                    errorMessage = "Invalid Valid From date format.";
                    return true;
                }

                DateTime? to = null;
                if (!string.IsNullOrWhiteSpace(d.valid_to))
                {
                    if (!DateTime.TryParseExact(
                        d.valid_to,
                        dateFormat,
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out DateTime parsedTo))
                    {
                        errorMessage = "Invalid Valid To date format.";
                        return true;
                    }

                    // Check if valid_to < valid_from
                    if (parsedTo < from)
                    {
                        errorMessage =
                            $"Invalid date range detected:\n\n" +
                            $"Valid From: {from:MM/dd/yyyy}\n" +
                            $"Valid To: {parsedTo:MM/dd/yyyy}\n\n" +
                            $"Valid To date cannot be earlier than Valid From date.";
                        return true;
                    }

                    to = parsedTo;
                }

                ranges.Add((from, to));
            }

            // Sort by Valid From
            ranges = ranges.OrderBy(r => r.From).ToList();

            // Overlap check
            for (int i = 1; i < ranges.Count; i++)
            {
                var previous = ranges[i - 1];
                var current = ranges[i];

                // If previous To is null (open-ended), any subsequent From overlaps
                if (previous.To.HasValue && current.From <= previous.To.Value)
                {
                    errorMessage =
                        $"Overlapping tax period detected:\n\n" +
                        $"Previous: {previous.From:MM/dd/yyyy} - {(previous.To.HasValue ? previous.To.Value.ToString("MM/dd/yyyy") : "Open")}\n" +
                        $"Current: {current.From:MM/dd/yyyy} - {(current.To.HasValue ? current.To.Value.ToString("MM/dd/yyyy") : "Open")}";

                    return true;
                }
            }

            return false; // All date fields valid
        }

        private async void InputVatPage_Load(object sender, EventArgs e)
        {
            try
            {
                Helpers.Loading.ShowLoading(dgv_tax_code_list, "Fetching data...");
                Helpers.Loading.ShowLoading(dgv_tax_details, "Fetching data...");
                await LoadTaxSetups();
                await FetchChartOfAccountClass();
                LoadSelectedTaxSetup();
            }
            catch (Exception ex)
            {
                Helpers.ShowDialogMessage("error", $"Failed to load: {ex.Message}");
            }
            finally
            {
                Helpers.Loading.HideLoading(dgv_tax_code_list);
                Helpers.Loading.HideLoading(dgv_tax_details);
            }
        }

        private async Task LoadTaxSetups()
        {
            _taxdata = await taxSetupService.GetAsModel();

            _taxTable = Helpers.ToDataTable(_taxdata.tax_setup_view);

            if (_taxTable != null)
            {
                dgv_tax_code_list.AutoGenerateColumns = false;

                dgv_tax_code_list.DataSource = _taxTable;
            }
            else
            {
                dgv_tax_code_list.DataSource = null;
            }
        }

        private async Task FetchChartOfAccountClass()
        {
            serviceSetup = new GeneralService<ChartOfAccountsViewModel>(ApiEndPoints.CHART_OF_ACCOUNT_VIEW);
            _coaTable = await serviceSetup.GetAsDatatable();

            if (_coaTable == null || _coaTable.Rows.Count == 0)
                return;

            // PURCHASE (EXPENSE + ASSET)
            var purchaseFilter = _coaTable.AsEnumerable()
                .Where(r =>
                    r.Field<string>("type") == "EXPENSE" ||
                    r.Field<string>("type") == "ASSET"
                );

            DataTable purchaseTable = purchaseFilter.Any()
                ? purchaseFilter.CopyToDataTable()
                : _coaTable.Clone();

            cmb_coa_purchase.DataSource = purchaseTable;
            cmb_coa_purchase.ValueMember = "id";
            cmb_coa_purchase.DisplayMember = "name";
            cmb_coa_purchase.SelectedIndex = -1;
            cmb_coa_purchase.Text = "";

            // SALES (LIABILITY + EQUITY + REVENUE)
            var salesFilter = _coaTable.AsEnumerable()
                .Where(r =>
                    r.Field<string>("type") == "LIABILITY" ||
                    r.Field<string>("type") == "EQUITY" ||
                    r.Field<string>("type") == "REVENUE"
                );

            DataTable salesTable = salesFilter.Any()
                ? salesFilter.CopyToDataTable()
                : _coaTable.Clone();

            cmb_coa_sales.DataSource = salesTable;
            cmb_coa_sales.ValueMember = "id";
            cmb_coa_sales.DisplayMember = "name";
            cmb_coa_sales.SelectedIndex = -1;
            cmb_coa_sales.Text = "";
        }

        private void LoadSelectedTaxSetup()
        {
            if (dgv_tax_code_list.SelectedRows.Count == 0)
                return;

            if (_taxdata == null || _taxdata.tax_setup == null)
                return;

            if (!dgv_tax_code_list.Columns.Contains("view_id"))
                return;

            var selectedRow = dgv_tax_code_list.SelectedRows[0];

            int viewId = Convert.ToInt32(selectedRow.Cells["view_id"].Value);

            var selectedTaxSetup = _taxdata.tax_setup
                .FirstOrDefault(x => x.id == viewId);

            if (selectedTaxSetup == null)
                return;

            var taxSetupTable = Helpers.ToDataTable(
                new List<TaxSetupModel> { selectedTaxSetup }
            );

            string coaPurchId = taxSetupTable.Rows[0]["coa_purchase_id"]?.ToString();
            string coaSalesId = taxSetupTable.Rows[0]["coa_sales_id"]?.ToString();

            if (int.TryParse(coaPurchId, out int coaPurchidValue))
                cmb_coa_purchase.SelectedValue = coaPurchidValue;
            else
                cmb_coa_purchase.SelectedIndex = -1;

            if (int.TryParse(coaSalesId, out int coaSalesidValue))
                cmb_coa_sales.SelectedValue = coaSalesidValue;
            else
                cmb_coa_sales.SelectedIndex = -1;

            dgv_tax_details.AutoGenerateColumns = false;

            //Bind child details
            if (_taxdata?.tax_setup_details != null)
            {
                var detailsForCurrent = new BindingList<TaxDetailsModel>(_taxdata.tax_setup_details.Where(d => d.tax_code_id == viewId).ToList());

                dgv_tax_details.DataSource = detailsForCurrent;
            }
            else
            {
                dgv_tax_details.DataSource = null;
            }

            Panel[] pnlList = { pnl_content };
            Helpers.BindControls(pnlList, taxSetupTable, 0);
        }

        private void dgv_tax_code_list_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ignore header and invalid clicks
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            LoadSelectedTaxSetup();
        }

        private void dgv_tax_code_list_SelectionChanged(object sender, EventArgs e)
        {
            LoadSelectedTaxSetup();
        }

        private void dgv_tax_details_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            Helpers.HandleNumericColumns(dgv_tax_details, e, new[] { "tax_rate", "valid_from", "valid_to" }, '/' );
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            if (_taxTable == null || _taxTable.Rows.Count == 0)
                return;

            string searchText = txt_search.Text.Trim();

            if (string.IsNullOrEmpty(searchText) || searchText == placeHolderText)
            {
                dgv_tax_code_list.DataSource = _taxTable;
            }
            else
            {
                var searchedData = Helpers.FilterDataTable(_taxTable, searchText, "code", "tax_desc", "input_tax_account", "output_tax_account", "tax_rate", "effective_period");
                dgv_tax_code_list.DataSource = searchedData;
            }
        }

        private void cmb_coa_sales_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            {
                cmb_coa_sales.SelectedIndex = -1;
                cmb_coa_sales.Text = "";
                e.Handled = true;

                UpdateCoaComboState();
            }
        }

        private void cmb_coa_purchase_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            {
                cmb_coa_purchase.SelectedIndex = -1;
                cmb_coa_purchase.Text = "";
                e.Handled = true;

                UpdateCoaComboState();
            }
        }

        private void UpdateCoaComboState()
        {
            bool hasSales = cmb_coa_sales.SelectedIndex != -1 && !string.IsNullOrWhiteSpace(cmb_coa_sales.Text);
            bool hasPurchase = cmb_coa_purchase.SelectedIndex != -1 && !string.IsNullOrWhiteSpace(cmb_coa_purchase.Text);

            // If sales is selected, disable purchase
            cmb_coa_purchase.Enabled = !hasSales;

            // If purchase is selected, disable sales
            cmb_coa_sales.Enabled = !hasPurchase;
        }

        private void cmb_coa_sales_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCoaComboState();
        }

        private void cmb_coa_purchase_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCoaComboState();
        }
    }
}
