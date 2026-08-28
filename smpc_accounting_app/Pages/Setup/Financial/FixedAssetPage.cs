using smpc_accounting_app.Models;
using smpc_accounting_app.Services.Helpers;
using smpc_accounting_app.Services.Setup;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace smpc_accounting_app.Pages.Setup.Financial
{
    // One PP&E item (§ not in the spec - see ERP_API's accounting_fixed_asset_model.go).
    // cmb_category and cmb_status are handled manually rather than through
    // Helpers.BuildModelFromPanels/BindControls, same as ChartOfAccountsPage's
    // own cmb_account_class does - those two generic helpers use conflicting
    // naming conventions for a DYNAMIC combo (BuildModelFromPanels wants
    // "cmb_" + the exact property name "category_id"; BindControls wants the
    // control named "cmb_category" and reads column "category_id" from
    // "cmb_" + name + "_id") that can't both be satisfied by one control name.
    public partial class FixedAssetPage : UserControl
    {
        readonly FixedAssetService _service = new FixedAssetService();
        readonly AssetCategoryService _categoryService = new AssetCategoryService();
        private bool _isNewMode = false;
        private DataTable _data;
        private List<AssetCategoryModel> _categories = new List<AssetCategoryModel>();
        private string _placeHolderText = "Fixed Asset Search...";

        public FixedAssetPage()
        {
            InitializeComponent();

            Helpers.Placeholder.SetPlaceholder(txt_search, _placeHolderText);
            dgv_list.AutoGenerateColumns = false;
        }

        private void SetEditMode(bool enable, bool isNewMode = false)
        {
            _isNewMode = isNewMode;

            string[] editButtons = { "btn_save", "btn_cancel" };
            string[] navButtons = { "btn_new", "btn_edit", "btn_delete" };

            Helpers.SetButtonVisibility(
                toolStrip1,
                pnl_content,
                visibleButtons: enable ? editButtons : navButtons,
                hiddenButtons: enable ? navButtons : editButtons
            );

            Helpers.SetChildControlsEnabled(new[] { pnl_content }, !enable, new string[] { });

            // Disposed Date only matters (and is only enabled) once Status is
            // actually DISPOSED - see cmb_status_SelectedIndexChanged.
            ToggleDisposedDate();
        }

        private void ToggleDisposedDate()
        {
            bool isDisposed = cmb_status.SelectedItem?.ToString() == "DISPOSED";
            dtp_disposed_date.Enabled = isDisposed && dtp_disposed_date.Parent != null && pnl_content.Enabled;
        }

        private void cmb_status_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToggleDisposedDate();
        }

        private async void btn_new_Click(object sender, EventArgs e)
        {
            SetEditMode(true, isNewMode: true);
            Helpers.ResetControls(new Panel[] { pnl_content });
            await LoadCategoriesIntoCombo();
            cmb_status.SelectedItem = "ACTIVE";
            dtp_acquired_date.Value = DateTime.Today;
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            SetEditMode(true);
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            SetEditMode(false);
            LoadSelected();
        }

        private async void btn_save_Click(object sender, EventArgs e)
        {
            btn_save.Enabled = false;
            btn_cancel.Enabled = false;

            try
            {
                bool hasError = Helpers.ValidateControlsValues(pnl_content);
                if (hasError)
                {
                    Helpers.ShowDialogMessage("error", "Please fill in all required fields.");
                    return;
                }

                if (cmb_category.SelectedValue == null)
                {
                    Helpers.ShowDialogMessage("error", "Please select a Category.");
                    return;
                }

                int? currentId = !_isNewMode && int.TryParse(txt_id.Text, out int idValue) ? idValue : (int?)null;

                if (IsDuplicateCode(txt_code.Text.Trim(), currentId))
                {
                    Helpers.ShowDialogMessage("error", $"Asset Tag '{txt_code.Text}' already exists.");
                    return;
                }

                var payload = Helpers.BuildModelFromPanels<FixedAssetModel>(new Panel[] { pnl_content });

                // Manual fields - see this class's own doc comment for why.
                payload.category_id = Convert.ToInt32(cmb_category.SelectedValue);
                payload.category_name = cmb_category.Text;
                payload.status = cmb_status.SelectedItem?.ToString() ?? "ACTIVE";
                payload.disposed_date = payload.status == "DISPOSED"
                    ? dtp_disposed_date.Value.ToString("MM/dd/yyyy")
                    : "";

                if (!_isNewMode)
                    payload.id = int.Parse(txt_id.Text);

                Helpers.Loading.ShowLoading(dgv_list, "Saving data...");

                var result = _isNewMode
                    ? await _service.Insert(payload)
                    : await _service.Update(payload);

                if (!result.Success)
                {
                    Helpers.ShowDialogMessage("error", _isNewMode ? "Fixed Asset not created." : "Fixed Asset not updated.");
                    return;
                }

                Helpers.ShowDialogMessage("success", _isNewMode ? "Fixed Asset created successfully." : "Fixed Asset updated successfully.");

                SetEditMode(false);
                await LoadData();
                LoadSelected();
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
                Helpers.ShowDialogMessage("error", "Please select a Fixed Asset to delete.");
                return;
            }

            var confirm = MessageBox.Show("Are you sure you want to delete this Fixed Asset?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            try
            {
                Helpers.Loading.ShowLoading(dgv_list, "Deleting data...");

                var result = await _service.Delete(new FixedAssetModel { id = int.Parse(txt_id.Text) });

                if (!result)
                {
                    Helpers.ShowDialogMessage("error", "Fixed Asset not deleted.");
                    return;
                }

                Helpers.ShowDialogMessage("success", "Fixed Asset deleted successfully.");
            }
            catch (Exception ex)
            {
                Helpers.ShowDialogMessage("error", $"Failed to delete: {ex.Message}");
            }
            finally
            {
                await LoadData();
                Helpers.Loading.HideLoading(dgv_list);
            }
        }

        private bool IsDuplicateCode(string code, int? currentId)
        {
            if (_data == null || _data.Rows.Count == 0) return false;

            foreach (DataRow row in _data.Rows)
            {
                string existingCode = row["code"]?.ToString();
                int existingId = Convert.ToInt32(row["id"]);

                if (string.Equals(existingCode, code, StringComparison.OrdinalIgnoreCase))
                {
                    if (currentId == null) return true;
                    if (existingId != currentId.Value) return true;
                }
            }
            return false;
        }

        private async void FixedAssetPage_Load(object sender, EventArgs e)
        {
            try
            {
                Helpers.Loading.ShowLoading(dgv_list, "Fetching data...");
                await LoadCategoriesIntoCombo();
                await LoadData();
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

        private async System.Threading.Tasks.Task LoadCategoriesIntoCombo()
        {
            _categories = await _categoryService.GetAsList();

            cmb_category.DataSource = null;
            cmb_category.DataSource = _categories;
            cmb_category.ValueMember = "id";
            cmb_category.DisplayMember = "name";
            cmb_category.SelectedIndex = -1;
        }

        private async System.Threading.Tasks.Task LoadData()
        {
            var records = await _service.GetAsList();
            _data = Helpers.ToDataTable(records);
            dgv_list.DataSource = _data;
        }

        private void LoadSelected()
        {
            if (dgv_list.SelectedRows.Count == 0 || _data == null) return;

            int rowIndex = dgv_list.SelectedRows[0].Index;
            if (rowIndex < 0 || rowIndex >= _data.Rows.Count) return;

            // txt_cost/txt_salvage_value/txt_useful_life_years/txt_code/txt_name
            // and dtp_acquired_date all go through the generic helper; only the
            // two combos are set by hand below.
            Helpers.BindControls(new Panel[] { pnl_content }, _data, rowIndex);

            var row = _data.Rows[rowIndex];

            object categoryId = row["category_id"];
            if (categoryId != null && categoryId != DBNull.Value)
                cmb_category.SelectedValue = Convert.ToInt32(categoryId);
            else
                cmb_category.SelectedIndex = -1;

            string status = row["status"]?.ToString();
            cmb_status.SelectedItem = string.IsNullOrWhiteSpace(status) ? "ACTIVE" : status;

            string disposedDate = row["disposed_date"]?.ToString();
            if (DateTime.TryParse(disposedDate, out DateTime parsedDisposed))
                dtp_disposed_date.Value = parsedDisposed;

            ToggleDisposedDate();
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            string searchText = txt_search.Text.Trim();
            if (string.IsNullOrEmpty(searchText) || searchText == _placeHolderText || _data == null)
            {
                dgv_list.DataSource = _data;
                return;
            }

            dgv_list.DataSource = Helpers.FilterDataTable(_data, searchText, "code", "name", "category_name", "status");
        }

        private void dgv_list_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            LoadSelected();
        }

        private void dgv_list_SelectionChanged(object sender, EventArgs e)
        {
            LoadSelected();
        }
    }
}
