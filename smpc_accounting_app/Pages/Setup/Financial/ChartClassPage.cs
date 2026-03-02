using smpc_accounting_app.Services.Helpers;
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
using smpc_accounting_app.Models;

namespace smpc_accounting_app.Pages.Setup.Financial
{
    public partial class ChartClassPage : UserControl
    {
        readonly ChartClassService chartClassServices = new ChartClassService();
        private bool _isNewMode = false;
        private bool _isEditMode = false;
        private DataTable _cldata;
        private string placeHolderText = "Chart Class Search...";

        public ChartClassPage()
        {
            InitializeComponent();

            Helpers.AllowOnlyNumbers(txt_code, '-');
            Helpers.Placeholder.SetPlaceholder(txt_search, placeHolderText);
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

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            SetEditMode(false);
            LoadSelectedChartClass();
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            SetEditMode(true);
        }

        private async void btn_save_Click(object sender, EventArgs e)
        {
            btn_save.Enabled = false;
            btn_cancel.Enabled = false;

            try
            {
                dgv_list.EndEdit();

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

                // Validate code prefix based on selected type
                if (cmb_type.SelectedItem != null)
                {
                    string selectedType = cmb_type.SelectedItem.ToString().ToUpper();
                    string expectedPrefix = "";

                    switch (selectedType)
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
                        Helpers.ShowDialogMessage("error", $"Code must start with '{expectedPrefix}' for {selectedType} type.");
                        return;
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

                var chartClassPayload = Helpers.BuildModelFromPanels<ChartClassModel>(new Panel[] { pnl_content });

                Helpers.Loading.ShowLoading(dgv_list, "Saving data...");

                if (_isNewMode && (txt_id.Text == null || txt_id.Text == ""))
                {
                    var result = await chartClassServices.Insert(chartClassPayload);

                    if (!result.Success)
                    {
                        Helpers.ShowDialogMessage("error", "Chart Class not created.");
                        return;
                    }

                    Helpers.ShowDialogMessage("success", "Chart Class created successfully.");
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(txt_id.Text))
                    {
                        Helpers.ShowDialogMessage("error", "No Chart Class selected to update.");
                        return;
                    }

                    var result = await chartClassServices.Update(chartClassPayload);

                    if (!result.Success)
                    {
                        Helpers.ShowDialogMessage("error", "Chart Class not updated.");
                        return;
                    }

                    Helpers.ShowDialogMessage("success", "Chart Class updated successfully.");
                }

                SetEditMode(false);
                await GetChartClass();
                LoadSelectedChartClass();
            }
            catch (Exception ex)
            {
                Helpers.ShowDialogMessage("error", $"Failed to save: {ex.Message}");
            }
            finally
            {
                btn_save.Enabled = true;
                btn_cancel.Enabled = true;

                Helpers.Loading.HideLoading(dgv_list);
            }
        }

        private async void btn_delete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_id.Text))
            {
                Helpers.ShowDialogMessage("error", "Please select a Chart Class to delete.");
                return;
            }

            var confirm = MessageBox.Show($"Are you sure you want to delete this Chart Class {txt_id.Text}?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            try
            {
                Helpers.Loading.ShowLoading(dgv_list, "Deleting data...");

                var clModel = new ChartClassModel
                {
                    id = int.Parse(txt_id.Text),
                };

                var result = await chartClassServices.Delete(clModel);

                if (!result)
                {
                    Helpers.ShowDialogMessage("error", "Chart of Account not Deleted.");
                    return;
                }

                Helpers.ShowDialogMessage("success", "Chart Class deleted successfully.");
            }
            catch (Exception ex)
            {
                Helpers.ShowDialogMessage("error", $"Failed to delete: {ex.Message}");
            }
            finally
            {
                await GetChartClass();

                Helpers.Loading.HideLoading(dgv_list);
            }
        }

        private bool IsDuplicateCode(string code, int? currentId = null)
        {
            if (_cldata == null || _cldata.Rows.Count == 0)
                return false;

            foreach (DataRow row in _cldata.Rows)
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

        private async void ChartClassPage_Load(object sender, EventArgs e)
        {
            try
            {
                LoadTypes();
                Helpers.Loading.ShowLoading(dgv_list, "Fetching data...");
                await GetChartClass();
            }
            catch (Exception ex)
            {
                Helpers.ShowDialogMessage("error", $"Failed to load: {ex.Message}");
            }
            finally
            {
                Helpers.Loading.HideLoading(dgv_list);
            }
        }

        private void LoadTypes()
        {
            cmb_type.Items.Clear();

            cmb_type.Items.AddRange(new string[]
            {
                "ASSET",
                "LIABILITY",
                "EQUITY",
                "REVENUE",
                "EXPENSE"
            });

            cmb_type.SelectedIndex = -1; // default (no selection)
        }

        private async Task GetChartClass()
        {
            _cldata = await chartClassServices.GetAsDatatable();

            if (_cldata != null)
            {
                dgv_list.AutoGenerateColumns = false;

                dgv_list.DataSource = _cldata;
            }
            else
            {
                dgv_list.DataSource = null;
            }
        }

        private void LoadSelectedChartClass()
        {
            if (dgv_list.SelectedRows.Count == 0)
                return;

            int rowIndex = dgv_list.SelectedRows[0].Index;

            if (_cldata == null || rowIndex < 0 || rowIndex >= _cldata.Rows.Count)
                return;

            Panel[] pnlList = { pnl_content };
            DataTable dt = Helpers.ConvertDataGridViewToDataTable(dgv_list);

            // Get type (trim spaces)
            string type = dt.Rows[rowIndex]["type"]?.ToString()?.Replace(" ", "");

            // Match combo value
            int index = cmb_type.FindStringExact(type);
            cmb_type.SelectedIndex = index >= 0 ? index : -1;

            // Bind other controls
            Helpers.BindControls(pnlList, dt, rowIndex);
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            if (_cldata == null || _cldata.Rows.Count == 0)
                return;

            string searchText = txt_search.Text.Trim();

            if (string.IsNullOrEmpty(searchText) || searchText == placeHolderText)
            {
                dgv_list.DataSource = _cldata;
            }
            else
            {
                var searchedData = Helpers.FilterDataTable(_cldata, searchText, "code", "name", "type");
                dgv_list.DataSource = searchedData;
            }
        }

        private void dgv_list_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ignore header and invalid clicks
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            LoadSelectedChartClass();
        }

        private void dgv_list_SelectionChanged(object sender, EventArgs e)
        {
            LoadSelectedChartClass();
        }

        private void cmb_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_type.SelectedItem == null)
                return;

            string selected = cmb_type.SelectedItem.ToString().ToUpper();

            switch (selected)
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

        private void cmb_type_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            {
                cmb_type.SelectedIndex = -1;
                cmb_type.Text = "";
                e.Handled = true;

                txt_code.Text = "";
            }
        }
    }
}
