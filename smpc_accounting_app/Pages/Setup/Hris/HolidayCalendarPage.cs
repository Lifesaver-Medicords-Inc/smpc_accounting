using smpc_accounting_app.Models.Hris;
using smpc_accounting_app.Services.Helpers;
using smpc_accounting_app.Services.Hris;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace smpc_accounting_app.Pages.Setup.Hris
{
    // HRIS Holiday Calendar - the holiday source of truth (the ERP will sync
    // holidays from the HRIS later). Pay rules applied by timesheet/payroll:
    //   REGULAR  unworked -> 100% (daily/hourly), worked -> 200%
    //   SPECIAL  worked -> 130%, unworked -> no pay (company policy)
    // Timesheets pick holidays up automatically on their next save/regenerate.
    //
    // Constructor stays I/O-free (RoutesService instantiates pages eagerly).
    public partial class HolidayCalendarPage : UserControl
    {
        private static readonly string[] HolidayTypes = { "REGULAR", "SPECIAL" };

        private List<HrisHolidayModel> _holidays = new List<HrisHolidayModel>();
        private bool _isNewMode;
        private bool _editMode;
        private bool _loaded;

        private TextBox txt_date, txt_name;
        private ComboBox cmb_type;

        private HrisHolidayModel Selected
        {
            get
            {
                if (dgv_list.SelectedRows.Count == 0) return null;
                var idValue = dgv_list.SelectedRows[0].Cells["col_id"].Value;
                if (idValue == null) return null;
                int id = Convert.ToInt32(idValue);
                return _holidays.FirstOrDefault(x => x.Id == id);
            }
        }

        public HolidayCalendarPage()
        {
            InitializeComponent();
            BuildForm();
            txt_year.Text = DateTime.Now.Year.ToString();
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

            TextBox AddText(string label)
            {
                var txt = new TextBox { BackColor = System.Drawing.Color.Gainsboro };
                t.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                t.Controls.Add(new Label { Text = label, AutoSize = true, Anchor = AnchorStyles.Left, Padding = new Padding(0, 4, 0, 0) });
                txt.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                t.Controls.Add(txt);
                return txt;
            }

            txt_date = AddText("DATE (YYYY-MM-DD) *");
            txt_name = AddText("HOLIDAY NAME *");
            cmb_type = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
            cmb_type.Items.AddRange(HolidayTypes);
            t.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            t.Controls.Add(new Label { Text = "TYPE *", AutoSize = true, Anchor = AnchorStyles.Left, Padding = new Padding(0, 4, 0, 0) });
            cmb_type.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            t.Controls.Add(cmb_type);

            pnl_form.Controls.Add(t);
        }

        private async void HolidayCalendarPage_Load(object sender, EventArgs e)
        {
            if (_loaded) return;
            _loaded = true;
            await LoadData();
        }

        private async System.Threading.Tasks.Task LoadData()
        {
            if (!int.TryParse(txt_year.Text, out int year))
            {
                Helpers.ShowDialogMessage("error", "Year must be a number.");
                return;
            }
            try
            {
                Helpers.Loading.ShowLoading(dgv_list, "Fetching data...");
                var result = await HrisHolidayService.GetHolidaysAsync(year);
                if (result.HasErrors)
                {
                    Helpers.ShowDialogMessage("error", result.ErrorMessage);
                    return;
                }
                _holidays = result.Data.Holidays ?? new List<HrisHolidayModel>();
                dgv_list.Rows.Clear();
                foreach (var h in _holidays)
                {
                    dgv_list.Rows.Add(h.Id, h.HolidayDate, h.Name, h.Type);
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
            var holiday = Selected;
            txt_date.Text = holiday != null ? holiday.HolidayDate : "";
            txt_name.Text = holiday != null ? holiday.Name : "";
            cmb_type.Text = holiday != null ? holiday.Type : "";
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
            btn_load.Enabled = !enable;
            txt_date.ReadOnly = !enable;
            txt_name.ReadOnly = !enable;
            cmb_type.Enabled = enable;
            dgv_list.Enabled = !enable;
        }

        private async void btn_load_Click(object sender, EventArgs e)
        {
            await LoadData();
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            txt_date.Text = "";
            txt_name.Text = "";
            cmb_type.Text = "REGULAR";
            SetEditMode(true, isNewMode: true);
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            if (Selected == null)
            {
                Helpers.ShowDialogMessage("error", "Please select a holiday to edit.");
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
            if (string.IsNullOrWhiteSpace(txt_date.Text) || string.IsNullOrWhiteSpace(txt_name.Text) || string.IsNullOrWhiteSpace(cmb_type.Text))
            {
                Helpers.ShowDialogMessage("error", "Date, name, and type are required.");
                return;
            }
            try
            {
                Helpers.Loading.ShowLoading(dgv_list, "Saving data...");
                string error;
                if (_isNewMode)
                {
                    var result = await HrisHolidayService.CreateAsync(txt_date.Text.Trim(), txt_name.Text.Trim(), cmb_type.Text);
                    error = result.HasErrors ? result.ErrorMessage : null;
                }
                else
                {
                    var result = await HrisHolidayService.UpdateAsync(Selected.Id, txt_date.Text.Trim(), txt_name.Text.Trim(), cmb_type.Text);
                    error = result.HasErrors ? result.ErrorMessage : null;
                }
                if (error != null)
                {
                    Helpers.ShowDialogMessage("error", error);
                    return;
                }
                Helpers.ShowDialogMessage("success", _isNewMode ? "Holiday added." : "Holiday updated.");
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
                Helpers.ShowDialogMessage("error", "Please select a holiday to delete.");
                return;
            }
            var confirm = MessageBox.Show($"Delete {Selected.Name} ({Selected.HolidayDate})?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;
            try
            {
                Helpers.Loading.ShowLoading(dgv_list, "Deleting...");
                var result = await HrisHolidayService.DeleteAsync(Selected.Id);
                if (result.HasErrors)
                {
                    Helpers.ShowDialogMessage("error", result.ErrorMessage);
                    return;
                }
                Helpers.ShowDialogMessage("success", "Holiday deleted.");
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

        private void dgv_list_SelectionChanged(object sender, EventArgs e)
        {
            if (!_editMode) BindSelected();
        }
    }
}
