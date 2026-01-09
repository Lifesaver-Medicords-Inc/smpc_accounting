using smpc_accounting_app.Services;
using smpc_accounting_app.Services.Helpers;
using smpc_accounting_app.Services.Setup;
using smpc_accounting_app.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using smpc_accounting_app.Models;

namespace smpc_accounting_app.Pages.Setup.Financial
{
    public partial class ChartOfAccounts : UserControl
    {

        ChartOfAccountsService chartOfAccountService = new ChartOfAccountsService();
        GeneralService<ChartClassModel> serviceSetup;

        private bool _isNewMode = false;
        private bool _isEditMode = false;
        private DataTable _coadata;
        private string placeHolderText = "Chart of Account Search...";

        public ChartOfAccounts()
        {
            InitializeComponent();

            Helpers.Placeholder.SetPlaceholder(txt_search, placeHolderText);
            Helpers.AllowOnlyNumbers(txt_code, '-');
        }

        private void SetEditMode(bool enable, bool isNewMode = false)
        {
            _isNewMode = isNewMode;
            _isEditMode = !isNewMode && enable;

            // buttons
            string[] editButtons = { "btn_save", "btn_cancel" };
            string[] navButtons = { "btn_new", "btn_print", "btn_edit", "btn_delete" };

            Helpers.SetButtonVisibility(
                toolStrip1,
                visibleButtons: enable ? editButtons : navButtons,
                hiddenButtons: enable ? navButtons : editButtons
            );

            pnl_content.Enabled = enable;
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            SetEditMode(true, isNewMode: true);
            Helpers.ResetControls(new Panel[] { pnl_content });
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            SetEditMode(true);
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            SetEditMode(false);
            LoadSelectedChartOfAccount();
        }

        private async void btn_delete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_id.Text))
            {
                Helpers.ShowDialogMessage("error", "Please select a Chart of Account to delete.");
                return;
            }

            var confirm = MessageBox.Show($"Are you sure you want to delete this Chart of Account {txt_id.Text}?", 
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            try
            {
                Helpers.Loading.ShowLoading(dgv_chart_of_account, "Deleting data...");

                var coaModel = new ChartOfAccountsModel
                {
                    id = int.Parse(txt_id.Text),
                };

                await chartOfAccountService.Delete(coaModel);

                Helpers.ShowDialogMessage("success", "Chart of Account deleted successfully.");
            }
            catch (Exception ex)
            {
                Helpers.ShowDialogMessage("error", $"Failed to delete: {ex.Message}");
            }
            finally
            {
                await FetchChartOfAccount();

                Helpers.Loading.HideLoading(dgv_chart_of_account);
            }
        }

        private async void btn_save_Click(object sender, EventArgs e)
        {
            dgv_chart_of_account.EndEdit();

            // Validate required controls in selected panel
            bool hasError = Helpers.ValidateControlsValues(pnl_content);

            if (hasError)
            {
                Helpers.ShowDialogMessage("error", "Please fill in all required fields.");
                return;
            }

            if (txt_code.Text.Contains("--"))
            {
                Helpers.ShowDialogMessage("error", "Code cannot contain consecutive dashes (--).");
                return;
            }

            // Validate number of dashes should not exceed 5
            int dashCount = txt_code.Text.Count(c => c == '-');
            if (dashCount > 5)
            {
                Helpers.ShowDialogMessage("error", "Code cannot contain more than 5 dashes (-).");
                return;
            }

            if(cmb_group.Text == null || cmb_group.Text == "")
            {
                // Must be at least 6 characters
                if (txt_code.Text.Length < 6)
                {
                    Helpers.ShowDialogMessage("error", "Code must be at least 6 characters long.");
                    return;
                }
            }
            else
            {
                // Must be at least 7 characters
                if (txt_code.Text.Length < 7)
                {
                    Helpers.ShowDialogMessage("error", "Code must be at least 7 characters long.");
                    return;
                }
            }

            if(cmb_group.Text == null || cmb_group.Text == "")
            {
                // Validate code prefix based on selected Account Class
                if (cmb_account_class.SelectedItem != null)
                {
                    DataRowView selectedRow = cmb_account_class.SelectedItem as DataRowView;

                    if (selectedRow != null)
                    {
                        string accountType = selectedRow["type"]?.ToString().Replace(" ", "").ToUpper();
                        string expectedPrefix = "";

                        switch (accountType)
                        {
                            case "ASSET":
                                expectedPrefix = "10000";
                                break;
                            case "LIABILITY":
                                expectedPrefix = "20000";
                                break;
                            case "EQUITY":
                                expectedPrefix = "30000";
                                break;
                            case "REVENUE":
                                expectedPrefix = "40000";
                                break;
                            case "EXPENSE":
                                expectedPrefix = "50000";
                                break;
                        }

                        if (!string.IsNullOrEmpty(expectedPrefix) && !txt_code.Text.StartsWith(expectedPrefix))
                        {
                            Helpers.ShowDialogMessage("error", $"Code must start with '{expectedPrefix}' for {accountType} accounts.");
                            return;
                        }
                    }
                }
            }

            // Validate code prefix based on selected Group
            if (cmb_group.SelectedItem != null)
            {
                DataRowView selectedRow = cmb_group.SelectedItem as DataRowView;

                if (selectedRow != null)
                {
                    string groupCode = selectedRow["code"]?.ToString();

                    if (!string.IsNullOrEmpty(groupCode) && !txt_code.Text.StartsWith(groupCode))
                    {
                        Helpers.ShowDialogMessage("error", $"Code must start with '{groupCode}' for the selected group.");
                        return;
                    }
                }
            }

            int? currentId = null;

            if (!_isNewMode && int.TryParse(txt_id.Text, out int idValue))
            {
                currentId = idValue;
            }

            if (IsDuplicateCode(txt_code.Text.Trim(), currentId))
            {
                Helpers.ShowDialogMessage(
                    "error",
                    $"Code '{txt_code.Text}' already exists. Please use a unique Chart of Account code."
                );
                return;
            }

            var chartOfAccountPayload = Helpers.BuildModelFromPanels<ChartOfAccountsModel>(new Panel[] { pnl_content });

            if (cmb_account_class.SelectedValue != null)
            {
                chartOfAccountPayload.class_id = Convert.ToInt32(cmb_account_class.SelectedValue);
            }
            else
            {
                Helpers.ShowDialogMessage("error", "Please select a valid Account Class.");
                return;
            }

            try
            {
                Helpers.Loading.ShowLoading(dgv_chart_of_account, "Saving data...");

                if (_isNewMode && (txt_id.Text == null || txt_id.Text == ""))
                {
                    var result = await chartOfAccountService.Insert(chartOfAccountPayload);
                    Helpers.ShowDialogMessage("success", "Chart of Account created successfully.");
                }
                else
                {
                    var result = await chartOfAccountService.Update(chartOfAccountPayload);
                    Helpers.ShowDialogMessage("success", "Chart of Account updated successfully.");
                }
            }
            catch (Exception ex)
            {
                Helpers.ShowDialogMessage("error", $"Failed to save: {ex.Message}");
            }
            finally
            {
                SetEditMode(false);
                await FetchChartOfAccount();

                Helpers.Loading.HideLoading(dgv_chart_of_account);
            }
        }

        private bool IsDuplicateCode(string code, int? currentId = null)
        {
            if (_coadata == null || _coadata.Rows.Count == 0)
                return false;

            foreach (DataRow row in _coadata.Rows)
            {
                string existingCode = row["code"]?.ToString();
                int existingId = Convert.ToInt32(row["id"]);

                if (string.Equals(existingCode, code, StringComparison.OrdinalIgnoreCase))
                {
                    // New record → any match is invalid
                    if (currentId == null)
                        return true;

                    // Edit record → allow same record, block others
                    if (existingId != currentId.Value)
                        return true;
                }
            }

            return false;
        }

        private async void ChartOfAccounts_Load(object sender, EventArgs e)
        {
            try
            {
                Helpers.Loading.ShowLoading(dgv_chart_of_account, "Fetching data...");
                await FetchChartOfAccount();
                await FetchChartOfAccountClass();
                LoadSelectedChartOfAccount();
            }
            catch (Exception ex)
            {
                Helpers.ShowDialogMessage("error", $"Failed to load: {ex.Message}");
            }
            finally
            {
                Helpers.Loading.HideLoading(dgv_chart_of_account);
            }
        }

        private async Task FetchChartOfAccount()
        {
            _coadata = await chartOfAccountService.GetAsDatatable();

            if (_coadata != null)
            {
                dgv_chart_of_account.AutoGenerateColumns = false;

                dgv_chart_of_account.DataSource = _coadata;
            }
            else
            {
                dgv_chart_of_account.DataSource = null;
            }

            cmb_group.DataSource = _coadata.Copy();
            cmb_group.ValueMember = "id";
            cmb_group.DisplayMember = "name";

            cmb_group.SelectedIndex = -1;
            cmb_group.SelectedItem = null;
            cmb_group.Text = "";
        }

        private async Task FetchChartOfAccountClass()
        {
            serviceSetup = new GeneralService<ChartClassModel>(ApiEndPoints.CHART_CLASS_SETUP);
            CacheData.ChartOfAccountClass = await serviceSetup.GetAsDatatable();

            cmb_account_class.DataSource = CacheData.ChartOfAccountClass;
            cmb_account_class.ValueMember = "id";
            cmb_account_class.DisplayMember = "name";

            cmb_account_class.SelectedIndex = -1;
            cmb_account_class.SelectedItem = null;
            cmb_account_class.Text = "";
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            if (_coadata == null || _coadata.Rows.Count == 0)
                return;

            string searchText = txt_search.Text.Trim();

            if (string.IsNullOrEmpty(searchText) || searchText == placeHolderText)
            {
                dgv_chart_of_account.DataSource = _coadata;
            }
            else
            {
                var searchedData = Helpers.FilterDataTable(_coadata, searchText, "code", "name", "account_class");
                dgv_chart_of_account.DataSource = searchedData;
            }
        }

        private void LoadSelectedChartOfAccount()
        {
            if (dgv_chart_of_account.SelectedRows.Count == 0)
                return;

            int rowIndex = dgv_chart_of_account.SelectedRows[0].Index;

            if (_coadata == null || rowIndex < 0 || rowIndex >= _coadata.Rows.Count)
                return;

            Panel[] pnlList = { pnl_content };
            DataTable dt = Helpers.ConvertDataGridViewToDataTable(dgv_chart_of_account);

            string classId = dt.Rows[rowIndex]["class_id"]?.ToString();
            string groupId = dt.Rows[rowIndex]["group_id"]?.ToString();

            if (int.TryParse(classId, out int classidValue))
                cmb_account_class.SelectedValue = classidValue;
            else
                cmb_account_class.SelectedIndex = -1;

            if (int.TryParse(groupId, out int groupidValue))
                cmb_group.SelectedValue = groupidValue;
            else
                cmb_group.SelectedIndex = -1;

            // Bind the rest of the controls
            Helpers.BindControls(pnlList, dt, rowIndex);
        }

        private void dgv_chart_of_account_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ignore header and invalid clicks
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            LoadSelectedChartOfAccount();
        }

        private void dgv_chart_of_account_SelectionChanged(object sender, EventArgs e)
        {
            LoadSelectedChartOfAccount();
        }

        private void cmb_account_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_account_class.SelectedItem == null)
                return;

            if (cmb_group.SelectedItem != null)
                return;

            // Get the DataRowView of the selected item
            DataRowView selectedRow = cmb_account_class.SelectedItem as DataRowView;

            if (selectedRow == null)
                return;

            string accountType = selectedRow["type"]?.ToString().Replace(" ", "").ToUpper();

            // Set default code based on type
            switch (accountType)
            {
                case "ASSET":
                    txt_code.Text = "10000";
                    break;
                case "LIABILITY":
                    txt_code.Text = "20000";
                    break;
                case "EQUITY":
                    txt_code.Text = "30000";
                    break;
                case "REVENUE":
                    txt_code.Text = "40000";
                    break;
                case "EXPENSE":
                    txt_code.Text = "50000";
                    break;
                default:
                    txt_code.Text = "";
                    break;
            }
        }

        private void cmb_group_SelectedIndexChanged(object sender, EventArgs e)
        {
            // GROUP CLEARED
            if (cmb_group.SelectedItem == null)
            {
                cmb_account_class.Enabled = true;

                // Clear Account Class
                cmb_account_class.SelectedIndex = -1;
                cmb_account_class.SelectedItem = null;
                cmb_account_class.Text = "";

                // Clear Code
                txt_code.Text = "";

                return;
            }

            // GROUP SELECTED
            DataRowView selectedRow = cmb_group.SelectedItem as DataRowView;
            if (selectedRow == null)
                return;

            // Lock dependent controls
            cmb_account_class.Enabled = false;

            // Set account class from group
            if (selectedRow["class_id"] != null &&
                int.TryParse(selectedRow["class_id"].ToString(), out int classId))
            {
                cmb_account_class.SelectedValue = classId;
            }

            // Set code from group
            string groupCode = selectedRow["code"]?.ToString();
            if (!string.IsNullOrEmpty(groupCode))
            {
                txt_code.Text = groupCode;
            }
        }

        private void cmb_account_class_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            {
                cmb_account_class.SelectedIndex = -1;
                cmb_account_class.Text = "";
                e.Handled = true;

                txt_code.Text = "";
            }
        }

        private void cmb_group_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            {
                cmb_group.SelectedIndex = -1;
                cmb_group.Text = "";
                e.Handled = true;
            }
        }
    }
}
