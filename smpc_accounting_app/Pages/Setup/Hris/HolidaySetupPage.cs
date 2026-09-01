using smpc_accounting_app.Models.Hris;
using smpc_accounting_app.Services.Helpers;
using smpc_accounting_app.Services.Hris;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace smpc_accounting_app.Pages.Setup.Hris
{
    // HRIS Holiday Setup - recurring holiday templates and the yearly
    // generator: type a year (2027, 2028, ...) and Generate stamps every
    // active template into that year's Holiday Calendar (existing dates are
    // skipped, so it is safe to re-run). Movable holidays (Holy Week, Eid,
    // yearly proclamations) are added by HR on the Holiday Calendar page.
    //
    // Rules: FIXED repeats a month/day; LAST_MONDAY takes the month's last
    // Monday (National Heroes Day).
    //
    // Constructor stays I/O-free (RoutesService instantiates pages eagerly).
    public partial class HolidaySetupPage : UserControl
    {
        private static readonly string[] HolidayTypes = { "REGULAR", "SPECIAL" };
        private static readonly string[] HolidayRules = { "FIXED", "LAST_MONDAY" };

        private List<HrisHolidaySetupModel> _setups = new List<HrisHolidaySetupModel>();
        private bool _isNewMode;
        private bool _editMode;
        private bool _loaded;

        private TextBox txt_name, txt_month, txt_day;
        private ComboBox cmb_type, cmb_rule;
        private CheckBox chk_active;

        private HrisHolidaySetupModel Selected
        {
            get
            {
                if (dgv_list.SelectedRows.Count == 0) return null;
                var idValue = dgv_list.SelectedRows[0].Cells["col_id"].Value;
                if (idValue == null) return null;
                int id = Convert.ToInt32(idValue);
                return _setups.FirstOrDefault(x => x.Id == id);
            }
        }

        public HolidaySetupPage()
        {
            InitializeComponent();
            BuildForm();
            txt_genYear.Text = (DateTime.Now.Year + 1).ToString();
            SetEditMode(false);
        }

        private void BuildForm()
        {
            var t = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                Padding = new Padding(10)
            };
            t.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 190F));
            t.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 430F));

            void Place(string label, Control control)
            {
                t.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                t.Controls.Add(new Label { Text = label, AutoSize = true, Anchor = AnchorStyles.Left, Padding = new Padding(0, 4, 0, 0) });
                control.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                t.Controls.Add(control);
            }
            TextBox AddText(string label)
            {
                var txt = new TextBox { BackColor = System.Drawing.Color.Gainsboro };
                Place(label, txt);
                return txt;
            }

            txt_name = AddText("HOLIDAY NAME *");
            cmb_type = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
            cmb_type.Items.AddRange(HolidayTypes);
            Place("TYPE *", cmb_type);
            cmb_rule = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
            cmb_rule.Items.AddRange(HolidayRules);
            Place("RULE (LAST_MONDAY ignores day)", cmb_rule);
            txt_month = AddText("MONTH (1-12) *");
            txt_day = AddText("DAY (1-31; FIXED only)");
            chk_active = new CheckBox { AutoSize = true };
            Place("ACTIVE", chk_active);

            pnl_form.Controls.Add(t);
        }

        private async void HolidaySetupPage_Load(object sender, EventArgs e)
        {
            if (_loaded) return;
            _loaded = true;
            await LoadData();
        }

        private async System.Threading.Tasks.Task LoadData()
        {
            try
            {
                Helpers.Loading.ShowLoading(dgv_list, "Fetching data...");
                var result = await HrisHolidayService.GetSetupsAsync();
                if (result.HasErrors)
                {
                    Helpers.ShowDialogMessage("error", result.ErrorMessage);
                    return;
                }
                _setups = result.Data.HolidaySetups ?? new List<HrisHolidaySetupModel>();
                dgv_list.Rows.Clear();
                foreach (var setup in _setups)
                {
                    dgv_list.Rows.Add(setup.Id, setup.Name, setup.Type, setup.Rule,
                        setup.Month, setup.Rule == "LAST_MONDAY" ? "—" : setup.Day.ToString(),
                        setup.IsActive ? "YES" : "NO");
                }
                BindSelected();
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

        private void BindSelected()
        {
            var setup = Selected;
            txt_name.Text = setup != null ? setup.Name : "";
            cmb_type.Text = setup != null ? setup.Type : "";
            cmb_rule.Text = setup != null ? setup.Rule : "";
            txt_month.Text = setup != null ? setup.Month.ToString() : "";
            txt_day.Text = setup != null ? setup.Day.ToString() : "";
            chk_active.Checked = setup != null && setup.IsActive;
        }

        private void SetEditMode(bool enable, bool isNewMode = false)
        {
            _editMode = enable;
            _isNewMode = isNewMode;
            btn_save.Visible = enable;
            btn_cancel.Visible = enable;
            btn_new.Visible = !enable;
            btn_edit.Visible = !enable;
            btn_delete.Visible = !enable;
            btn_generate.Enabled = !enable;
            txt_name.ReadOnly = !enable;
            txt_month.ReadOnly = !enable;
            txt_day.ReadOnly = !enable;
            cmb_type.Enabled = enable;
            cmb_rule.Enabled = enable;
            chk_active.Enabled = enable;
            dgv_list.Enabled = !enable;
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            txt_name.Text = "";
            cmb_type.Text = "REGULAR";
            cmb_rule.Text = "FIXED";
            txt_month.Text = "";
            txt_day.Text = "";
            chk_active.Checked = true;
            SetEditMode(true, isNewMode: true);
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            if (Selected == null)
            {
                Helpers.ShowDialogMessage("error", "Please select a holiday setup to edit.");
                return;
            }
            SetEditMode(true);
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            SetEditMode(false);
            BindSelected();
        }

        private async void btn_save_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_name.Text) || string.IsNullOrWhiteSpace(cmb_type.Text))
            {
                Helpers.ShowDialogMessage("error", "Name and type are required.");
                return;
            }
            if (!int.TryParse(txt_month.Text, out int month))
            {
                Helpers.ShowDialogMessage("error", "Month must be a number (1-12).");
                return;
            }
            int day = 0;
            if (cmb_rule.Text != "LAST_MONDAY" && !int.TryParse(txt_day.Text, out day))
            {
                Helpers.ShowDialogMessage("error", "Day must be a number for FIXED holidays.");
                return;
            }
            try
            {
                Helpers.Loading.ShowLoading(dgv_list, "Saving data...");
                string error;
                if (_isNewMode)
                {
                    var result = await HrisHolidayService.CreateSetupAsync(txt_name.Text.Trim(), cmb_type.Text, cmb_rule.Text, month, day, chk_active.Checked);
                    error = result.HasErrors ? result.ErrorMessage : null;
                }
                else
                {
                    var result = await HrisHolidayService.UpdateSetupAsync(Selected.Id, txt_name.Text.Trim(), cmb_type.Text, cmb_rule.Text, month, day, chk_active.Checked);
                    error = result.HasErrors ? result.ErrorMessage : null;
                }
                if (error != null)
                {
                    Helpers.ShowDialogMessage("error", error);
                    return;
                }
                Helpers.ShowDialogMessage("success", _isNewMode ? "Holiday setup added." : "Holiday setup updated.");
                SetEditMode(false);
                await LoadData();
            }
            catch (Exception ex)
            {
                Helpers.ShowDialogMessage("error", $"Failed to save: {ex.Message}");
            }
            finally
            {
                Helpers.Loading.HideLoading(dgv_list);
            }
        }

        private async void btn_delete_Click(object sender, EventArgs e)
        {
            if (Selected == null)
            {
                Helpers.ShowDialogMessage("error", "Please select a holiday setup to delete.");
                return;
            }
            var confirm = MessageBox.Show($"Delete the recurring template {Selected.Name}? (Already-generated calendar dates are kept.)",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;
            try
            {
                Helpers.Loading.ShowLoading(dgv_list, "Deleting...");
                var result = await HrisHolidayService.DeleteSetupAsync(Selected.Id);
                if (result.HasErrors)
                {
                    Helpers.ShowDialogMessage("error", result.ErrorMessage);
                    return;
                }
                Helpers.ShowDialogMessage("success", "Holiday setup deleted.");
                await LoadData();
            }
            catch (Exception ex)
            {
                Helpers.ShowDialogMessage("error", $"Failed to delete: {ex.Message}");
            }
            finally
            {
                Helpers.Loading.HideLoading(dgv_list);
            }
        }

        private async void btn_generate_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txt_genYear.Text, out int year))
            {
                Helpers.ShowDialogMessage("error", "Year must be a number.");
                return;
            }
            var confirm = MessageBox.Show(
                $"Generate the {year} holiday calendar from the active templates? Dates already on the calendar are skipped. Movable holidays (Holy Week, Eid) must still be added by HR.",
                "Generate Holiday Year", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;
            try
            {
                Helpers.Loading.ShowLoading(dgv_list, "Generating...");
                var result = await HrisHolidayService.GenerateYearAsync(year);
                if (result.HasErrors)
                {
                    Helpers.ShowDialogMessage("error", result.ErrorMessage);
                    return;
                }
                var generated = result.Data.GenerateHolidayYear;
                string message = $"{year}: created {generated.Created} holiday(s), skipped {generated.Skipped}.";
                if (!string.IsNullOrWhiteSpace(generated.Notes))
                    message += "\n" + generated.Notes;
                Helpers.ShowDialogMessage("success", message);
            }
            catch (Exception ex)
            {
                Helpers.ShowDialogMessage("error", $"Failed to generate: {ex.Message}");
            }
            finally
            {
                Helpers.Loading.HideLoading(dgv_list);
            }
        }

        private void dgv_list_SelectionChanged(object sender, EventArgs e)
        {
            if (!_editMode) BindSelected();
        }
    }
}
