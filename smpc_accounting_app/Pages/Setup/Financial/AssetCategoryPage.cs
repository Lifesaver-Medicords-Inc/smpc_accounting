using smpc_accounting_app.Models;
using smpc_accounting_app.Services.Helpers;
using smpc_accounting_app.Services.Setup;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace smpc_accounting_app.Pages.Setup.Financial
{
    // PP&E register's category level - not in the spec, see ERP_API's
    // accounting_asset_category_model.go. Same list+detail-panel shape as
    // ChartClassPage, simplified: no type-based code-prefix rule, and a
    // single GetAsList() load rather than cursor pagination, since this
    // list will only ever hold a handful of rows (LAND, BUILDING,
    // MACHINERY, ...).
    public partial class AssetCategoryPage : UserControl
    {
        readonly AssetCategoryService _service = new AssetCategoryService();
        private bool _isNewMode = false;
        private DataTable _data;
        private string _placeHolderText = "Asset Category Search...";

        public AssetCategoryPage()
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

                int? currentId = !_isNewMode && int.TryParse(txt_id.Text, out int idValue) ? idValue : (int?)null;

                if (IsDuplicateCode(txt_code.Text.Trim(), currentId))
                {
                    Helpers.ShowDialogMessage("error", $"Code '{txt_code.Text}' already exists.");
                    return;
                }

                var payload = Helpers.BuildModelFromPanels<AssetCategoryModel>(new Panel[] { pnl_content });

                Helpers.Loading.ShowLoading(dgv_list, "Saving data...");

                var result = _isNewMode
                    ? await _service.Insert(payload)
                    : await _service.Update(payload);

                if (!result.Success)
                {
                    Helpers.ShowDialogMessage("error", _isNewMode ? "Asset Category not created." : "Asset Category not updated.");
                    return;
                }

                Helpers.ShowDialogMessage("success", _isNewMode ? "Asset Category created successfully." : "Asset Category updated successfully.");

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
                Helpers.ShowDialogMessage("error", "Please select an Asset Category to delete.");
                return;
            }

            var confirm = MessageBox.Show("Are you sure you want to delete this Asset Category?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            try
            {
                Helpers.Loading.ShowLoading(dgv_list, "Deleting data...");

                var result = await _service.Delete(new AssetCategoryModel { id = int.Parse(txt_id.Text) });

                if (!result)
                {
                    Helpers.ShowDialogMessage("error", "Asset Category not deleted.");
                    return;
                }

                Helpers.ShowDialogMessage("success", "Asset Category deleted successfully.");
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

        private async void AssetCategoryPage_Load(object sender, EventArgs e)
        {
            try
            {
                Helpers.Loading.ShowLoading(dgv_list, "Fetching data...");
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

            Helpers.BindControls(new Panel[] { pnl_content }, _data, rowIndex);
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            string searchText = txt_search.Text.Trim();
            if (string.IsNullOrEmpty(searchText) || searchText == _placeHolderText || _data == null)
            {
                dgv_list.DataSource = _data;
                return;
            }

            dgv_list.DataSource = Helpers.FilterDataTable(_data, searchText, "code", "name");
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
