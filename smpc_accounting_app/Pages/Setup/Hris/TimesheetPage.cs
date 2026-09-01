using smpc_accounting_app.Models.Hris;
using smpc_accounting_app.Services.Helpers;
using smpc_accounting_app.Services.Hris;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace smpc_accounting_app.Pages.Setup.Hris
{
    // HRIS Timesheet (Payroll group) - per-employee, per-pay-period DTR served
    // by the HRIS GraphQL API. Document-style form like EmployeeInformationPage:
    // NEW - SEARCH - << PREV - NEXT >>, one timesheet at a time.
    //
    // DRAFT is editable; APPROVED is locked as a payroll input (Approve /
    // Reopen enforced server-side via HRIS_TIMESHEET_APPROVAL). Hours per day
    // and header totals are computed by the API, never typed here.
    //
    // Constructor stays I/O-free (RoutesService instantiates pages eagerly).
    public partial class TimesheetPage : UserControl
    {
        private static readonly string[] DayTypes = { "WORKED", "REST_DAY", "LEAVE", "LEAVE_UNPAID", "ABSENT", "HOLIDAY" };

        private List<HrisTimesheetModel> _timesheets = new List<HrisTimesheetModel>();
        private List<HrisEmployeeModel> _employees = new List<HrisEmployeeModel>();
        private int _currentIndex = -1;
        private bool _isNewMode;
        private bool _editMode;
        private bool _loaded;

        private HrisTimesheetModel Current =>
            _currentIndex >= 0 && _currentIndex < _timesheets.Count ? _timesheets[_currentIndex] : null;

        private ComboBox cmb_employee;
        private TextBox txt_periodStart, txt_periodEnd, txt_cutYear, txt_cutMonth, txt_cutPeriod,
            txt_notes, txt_status, txt_totals;

        public TimesheetPage()
        {
            InitializeComponent();
            BuildHeaderFields();
            BuildEntriesGrid();
            SetEditMode(false);
        }

        // ------------------------------------------------------------ layout

        private void BuildHeaderFields()
        {
            var t = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                AutoScroll = true,
                Padding = new Padding(10)
            };
            t.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 190F));
            t.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 430F));

            cmb_employee = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
            PlaceField(t, "EMPLOYEE *", cmb_employee);
            txt_cutYear = PlaceText(t, "CUTOFF YEAR");
            txt_cutMonth = PlaceText(t, "CUTOFF MONTH (1-12)");
            txt_cutPeriod = PlaceText(t, "PERIOD NO (wk 1-4 / semi 1-2 / mo 1)");
            txt_periodStart = PlaceText(t, "PERIOD START (or type dates)");
            txt_periodEnd = PlaceText(t, "PERIOD END (YYYY-MM-DD)");
            txt_notes = PlaceText(t, "NOTES");
            txt_status = PlaceText(t, "STATUS");
            txt_status.ReadOnly = true;
            txt_totals = PlaceText(t, "TOTALS");
            txt_totals.ReadOnly = true;

            pnl_header.Controls.Add(t);
        }

        private void PlaceField(TableLayoutPanel t, string label, Control control)
        {
            t.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            t.Controls.Add(new Label { Text = label, AutoSize = true, Anchor = AnchorStyles.Left, Padding = new Padding(0, 4, 0, 0) });
            control.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            t.Controls.Add(control);
        }

        private TextBox PlaceText(TableLayoutPanel t, string label)
        {
            var txt = new TextBox { BackColor = System.Drawing.Color.Gainsboro };
            PlaceField(t, label, txt);
            return txt;
        }

        private void BuildEntriesGrid()
        {
            dgv_entries.Columns.Add(new DataGridViewTextBoxColumn { Name = "entryDate", HeaderText = "DATE (YYYY-MM-DD)", Width = 140 });
            var dayType = new DataGridViewComboBoxColumn { Name = "dayType", HeaderText = "DAY TYPE", Width = 110, FlatStyle = FlatStyle.Flat };
            dayType.Items.AddRange(DayTypes);
            dgv_entries.Columns.Add(dayType);
            dgv_entries.Columns.Add(new DataGridViewTextBoxColumn { Name = "timeIn", HeaderText = "TIME IN (HH:MM)", Width = 110 });
            dgv_entries.Columns.Add(new DataGridViewTextBoxColumn { Name = "timeOut", HeaderText = "TIME OUT (HH:MM)", Width = 115 });
            dgv_entries.Columns.Add(new DataGridViewTextBoxColumn { Name = "breakMinutes", HeaderText = "BREAK (MINS)", Width = 100 });
            dgv_entries.Columns.Add(new DataGridViewTextBoxColumn { Name = "otHours", HeaderText = "OT HOURS", Width = 90 });
            var hours = new DataGridViewTextBoxColumn { Name = "hoursWorked", HeaderText = "HOURS (COMPUTED)", Width = 130, ReadOnly = true };
            hours.DefaultCellStyle.BackColor = System.Drawing.Color.Gainsboro;
            dgv_entries.Columns.Add(hours);
            foreach (var computed in new[] { new { N = "ndHours", H = "ND HRS", W = 70 },
                                             new { N = "lateMinutes", H = "LATE (MIN)", W = 85 },
                                             new { N = "undertimeMinutes", H = "UT (MIN)", W = 75 },
                                             new { N = "holidayType", H = "HOLIDAY", W = 75 },
                                             new { N = "timeInLocation", H = "IN LOCATION", W = 130 },
                                             new { N = "timeOutLocation", H = "OUT LOCATION", W = 130 } })
            {
                var col = new DataGridViewTextBoxColumn { Name = computed.N, HeaderText = computed.H, Width = computed.W, ReadOnly = true };
                col.DefaultCellStyle.BackColor = System.Drawing.Color.Gainsboro;
                dgv_entries.Columns.Add(col);
            }
            dgv_entries.Columns.Add(new DataGridViewTextBoxColumn { Name = "remarks", HeaderText = "REMARKS", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });

            // ComboBox cells throw a DataError dialog for values not in the list;
            // swallow it so a stale/empty cell never crashes the page.
            dgv_entries.DataError += (s, e) => { e.ThrowException = false; };
        }

        // ------------------------------------------------------------ loading

        private async void TimesheetPage_Load(object sender, EventArgs e)
        {
            if (_loaded) return;
            _loaded = true;
            try
            {
                Helpers.Loading.ShowLoading(dgv_entries, "Fetching data...");
                var employees = await HrisEmployeeService.GetEmployeesAsync();
                if (employees.HasErrors)
                {
                    Helpers.ShowDialogMessage("error", employees.ErrorMessage);
                    return;
                }
                _employees = employees.Data.Employees.Items ?? new List<HrisEmployeeModel>();
                cmb_employee.Items.Clear();
                foreach (var emp in _employees)
                {
                    cmb_employee.Items.Add(EmployeeLabel(emp));
                }

                await LoadData();
            }
            catch (Exception ex)
            {
                Helpers.ShowDialogMessage("error", $"Failed to load: {ex.Message}");
            }
            finally
            {
                Helpers.Loading.HideLoading(dgv_entries);
            }
        }

        private static string EmployeeLabel(HrisEmployeeModel emp)
        {
            return $"{emp.EmployeeNo} — {emp.FirstName} {emp.LastName}".Trim();
        }

        private async System.Threading.Tasks.Task LoadData(int? keepId = null)
        {
            var result = await HrisTimesheetService.GetTimesheetsAsync();
            if (result.HasErrors)
            {
                Helpers.ShowDialogMessage("error", result.ErrorMessage);
                return;
            }
            _timesheets = result.Data.Timesheets.Items ?? new List<HrisTimesheetModel>();

            if (keepId.HasValue)
            {
                _currentIndex = _timesheets.FindIndex(x => x.Id == keepId.Value);
                if (_currentIndex < 0 && _timesheets.Count > 0) _currentIndex = 0;
            }
            else
            {
                _currentIndex = _timesheets.Count > 0 ? 0 : -1;
            }
            ShowCurrent();
        }

        private void ShowCurrent()
        {
            if (Current != null) BindDetail(Current);
            else ClearDetail();
            lbl_record.Text = _timesheets.Count == 0 ? "0 / 0" : $"{_currentIndex + 1} / {_timesheets.Count}";
            UpdateNavButtons();
        }

        private void UpdateNavButtons()
        {
            btn_prev.Enabled = !_editMode && _currentIndex > 0;
            btn_next.Enabled = !_editMode && _currentIndex >= 0 && _currentIndex < _timesheets.Count - 1;
            btn_search.Enabled = !_editMode;
            btn_approve.Enabled = !_editMode && Current != null && Current.Status == "DRAFT";
            btn_reopen.Enabled = !_editMode && Current != null && Current.Status == "APPROVED";
        }

        private void BindDetail(HrisTimesheetModel ts)
        {
            cmb_employee.Text = ts.Employee != null ? EmployeeLabel(ts.Employee) : "";
            txt_cutYear.Text = ts.CutoffYear > 0 ? ts.CutoffYear.ToString() : "";
            txt_cutMonth.Text = ts.CutoffMonth > 0 ? ts.CutoffMonth.ToString() : "";
            txt_cutPeriod.Text = ts.PeriodNo > 0 ? ts.PeriodNo.ToString() : "";
            txt_periodStart.Text = ts.PeriodStart;
            txt_periodEnd.Text = ts.PeriodEnd;
            txt_notes.Text = ts.Notes;
            txt_status.Text = ts.Status;
            txt_totals.Text = $"Worked: {ts.DaysWorked}   Absent: {ts.DaysAbsent}   Paid leave: {ts.DaysPaidLeave}   Unpaid: {ts.DaysUnpaidLeave}   Hrs: {ts.TotalHours}   OT: {ts.TotalOtHours}   ND: {ts.TotalNdHours}   Late/UT min: {ts.TotalLateMinutes}/{ts.TotalUndertimeMinutes}   Hol reg/spec hrs: {ts.RegHolidayWorkedHours}/{ts.SpecialHolidayWorkedHours}   Unworked reg hol: {ts.RegHolidayUnworkedDays}";

            dgv_entries.Rows.Clear();
            foreach (var entry in ts.Entries)
            {
                dgv_entries.Rows.Add(entry.EntryDate, entry.DayType, entry.TimeIn, entry.TimeOut,
                    entry.BreakMinutes.ToString(CultureInfo.InvariantCulture),
                    entry.OtHours.ToString(CultureInfo.InvariantCulture),
                    entry.HoursWorked.ToString(CultureInfo.InvariantCulture),
                    entry.NdHours.ToString(CultureInfo.InvariantCulture),
                    entry.LateMinutes.ToString(CultureInfo.InvariantCulture),
                    entry.UndertimeMinutes.ToString(CultureInfo.InvariantCulture),
                    entry.HolidayType,
                    entry.TimeInLocation,
                    entry.TimeOutLocation,
                    entry.Remarks);
            }
        }

        private void ClearDetail()
        {
            cmb_employee.SelectedIndex = -1;
            txt_cutYear.Text = DateTime.Now.Year.ToString();
            txt_cutMonth.Text = DateTime.Now.Month.ToString();
            txt_cutPeriod.Text = "";
            txt_periodStart.Text = "";
            txt_periodEnd.Text = "";
            txt_notes.Text = "";
            txt_status.Text = "DRAFT";
            txt_totals.Text = "";
            dgv_entries.Rows.Clear();
        }

        // ------------------------------------------------------------ edit mode

        private void SetEditMode(bool enable, bool isNewMode = false)
        {
            _editMode = enable;
            _isNewMode = isNewMode;

            btn_save.Visible = enable;
            btn_cancel.Visible = enable;
            btn_new.Visible = !enable;
            btn_edit.Visible = !enable;
            btn_approve.Visible = !enable;
            btn_reopen.Visible = !enable;

            cmb_employee.Enabled = enable;
            txt_cutYear.ReadOnly = !enable;
            txt_cutMonth.ReadOnly = !enable;
            txt_cutPeriod.ReadOnly = !enable;
            txt_periodStart.ReadOnly = !enable;
            txt_periodEnd.ReadOnly = !enable;
            txt_notes.ReadOnly = !enable;

            dgv_entries.ReadOnly = !enable;
            dgv_entries.AllowUserToAddRows = enable;
            dgv_entries.AllowUserToDeleteRows = enable;

            UpdateNavButtons();
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            ClearDetail();
            SetEditMode(true, isNewMode: true);
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            if (Current == null)
            {
                Helpers.ShowDialogMessage("error", "No timesheet loaded. Use Search or New.");
                return;
            }
            if (Current.Status == "APPROVED")
            {
                Helpers.ShowDialogMessage("error", "This timesheet is approved and locked. Reopen it first.");
                return;
            }
            SetEditMode(true);
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            SetEditMode(false);
            ShowCurrent();
        }

        private void btn_prev_Click(object sender, EventArgs e)
        {
            if (_currentIndex <= 0) return;
            _currentIndex--;
            ShowCurrent();
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            if (_currentIndex >= _timesheets.Count - 1) return;
            _currentIndex++;
            ShowCurrent();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            if (_timesheets.Count == 0)
            {
                Helpers.ShowDialogMessage("error", "No timesheets yet. Use New to create one.");
                return;
            }
            using (var dialog = new SearchTimesheetDialog(_timesheets))
            {
                if (dialog.ShowDialog(FindForm()) != DialogResult.OK || !dialog.SelectedTimesheetId.HasValue) return;
                int index = _timesheets.FindIndex(x => x.Id == dialog.SelectedTimesheetId.Value);
                if (index >= 0)
                {
                    _currentIndex = index;
                    ShowCurrent();
                }
            }
        }

        // ------------------------------------------------------------ save / approve

        private async void btn_save_Click(object sender, EventArgs e)
        {
            btn_save.Enabled = false;
            btn_cancel.Enabled = false;
            try
            {
                var input = BuildInput();
                if (input == null) return; // validation error already shown

                Helpers.Loading.ShowLoading(dgv_entries, "Saving data...");

                string error;
                int? savedId = null;
                if (_isNewMode)
                {
                    var result = await HrisTimesheetService.CreateAsync(input);
                    error = result.HasErrors ? result.ErrorMessage : null;
                    if (error == null) savedId = result.Data.CreateTimesheet.Id;
                }
                else
                {
                    var result = await HrisTimesheetService.UpdateAsync(Current.Id, input);
                    error = result.HasErrors ? result.ErrorMessage : null;
                    if (error == null) savedId = result.Data.UpdateTimesheet.Id;
                }

                if (error != null)
                {
                    Helpers.ShowDialogMessage("error", error);
                    return;
                }

                Helpers.ShowDialogMessage("success", _isNewMode ? "Timesheet created successfully." : "Timesheet updated successfully.");
                SetEditMode(false);
                await LoadData(savedId);
            }
            catch (Exception ex)
            {
                Helpers.ShowDialogMessage("error", $"Failed to save: {ex.Message}");
            }
            finally
            {
                btn_save.Enabled = true;
                btn_cancel.Enabled = true;
                Helpers.Loading.HideLoading(dgv_entries);
            }
        }

        private Dictionary<string, object> BuildInput()
        {
            if (cmb_employee.SelectedIndex < 0 || cmb_employee.SelectedIndex >= _employees.Count)
            {
                Helpers.ShowDialogMessage("error", "Please select an employee.");
                return null;
            }
            bool hasCutoff = int.TryParse(txt_cutYear.Text, out int cutYear)
                & int.TryParse(txt_cutMonth.Text, out int cutMonth)
                & int.TryParse(txt_cutPeriod.Text, out int cutPeriod);
            if (!hasCutoff && (string.IsNullOrWhiteSpace(txt_periodStart.Text) || string.IsNullOrWhiteSpace(txt_periodEnd.Text)))
            {
                Helpers.ShowDialogMessage("error", "Give a cutoff (year + month + period) or type the period dates.");
                return null;
            }

            var input = new Dictionary<string, object>
            {
                { "employeeId", _employees[cmb_employee.SelectedIndex].Id },
                { "notes", txt_notes.Text.Trim() },
            };
            if (hasCutoff)
            {
                // Dates are derived server-side from the employee's pay frequency.
                input["cutoffYear"] = cutYear;
                input["cutoffMonth"] = cutMonth;
                input["periodNo"] = cutPeriod;
            }
            else
            {
                input["periodStart"] = txt_periodStart.Text.Trim();
                input["periodEnd"] = txt_periodEnd.Text.Trim();
            }

            var entries = new List<object>();
            foreach (DataGridViewRow row in dgv_entries.Rows)
            {
                if (row.IsNewRow) continue;
                string date = CellText(row, "entryDate");
                if (string.IsNullOrWhiteSpace(date)) continue;

                var entry = new Dictionary<string, object> { { "entryDate", date } };
                string dayType = CellText(row, "dayType");
                if (!string.IsNullOrWhiteSpace(dayType)) entry["dayType"] = dayType;
                string timeIn = CellText(row, "timeIn");
                if (!string.IsNullOrWhiteSpace(timeIn)) entry["timeIn"] = timeIn;
                string timeOut = CellText(row, "timeOut");
                if (!string.IsNullOrWhiteSpace(timeOut)) entry["timeOut"] = timeOut;
                string remarks = CellText(row, "remarks");
                if (!string.IsNullOrWhiteSpace(remarks)) entry["remarks"] = remarks;

                string breakText = CellText(row, "breakMinutes");
                if (!string.IsNullOrWhiteSpace(breakText))
                {
                    if (!int.TryParse(breakText, out int breakMins))
                    {
                        Helpers.ShowDialogMessage("error", $"Break minutes must be a whole number ({date}).");
                        return null;
                    }
                    entry["breakMinutes"] = breakMins;
                }
                string otText = CellText(row, "otHours");
                if (!string.IsNullOrWhiteSpace(otText))
                {
                    if (!decimal.TryParse(otText, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal ot))
                    {
                        Helpers.ShowDialogMessage("error", $"OT hours must be a number ({date}).");
                        return null;
                    }
                    entry["otHours"] = ot;
                }
                entries.Add(entry);
            }
            if (entries.Count > 0) input["entries"] = entries;

            return input;
        }

        private static string CellText(DataGridViewRow row, string column)
        {
            var value = row.Cells[column].Value;
            return value == null ? "" : value.ToString().Trim();
        }

        private async void btn_approve_Click(object sender, EventArgs e)
        {
            if (Current == null) return;
            var confirm = MessageBox.Show(
                "Approve this timesheet? It becomes locked and ready for payroll.",
                "Confirm Approve", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;
            await RunStatusAction(() => HrisTimesheetService.ApproveAsync(Current.Id), "Timesheet approved.");
        }

        private async void btn_reopen_Click(object sender, EventArgs e)
        {
            if (Current == null) return;
            var confirm = MessageBox.Show(
                "Reopen this approved timesheet for correction?",
                "Confirm Reopen", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;
            await RunStatusAction(async () =>
            {
                var r = await HrisTimesheetService.ReopenAsync(Current.Id);
                return new GraphQLResponse<ApproveTimesheetData> { Errors = r.Errors };
            }, "Timesheet reopened.");
        }

        private async System.Threading.Tasks.Task RunStatusAction(
            Func<System.Threading.Tasks.Task<GraphQLResponse<ApproveTimesheetData>>> action, string successMessage)
        {
            try
            {
                Helpers.Loading.ShowLoading(dgv_entries, "Saving data...");
                var result = await action();
                if (result.HasErrors)
                {
                    Helpers.ShowDialogMessage("error", result.ErrorMessage);
                    return;
                }
                Helpers.ShowDialogMessage("success", successMessage);
                await LoadData(Current.Id);
            }
            catch (Exception ex)
            {
                Helpers.ShowDialogMessage("error", $"Failed: {ex.Message}");
            }
            finally
            {
                Helpers.Loading.HideLoading(dgv_entries);
            }
        }

        // ------------------------------------------------------------ cutoff board

        private async void btn_board_Click(object sender, EventArgs e)
        {
            using (var dialog = new CutoffBoardDialog())
            {
                dialog.ShowDialog(FindForm());
                if (dialog.OpenTimesheetId.HasValue)
                {
                    await LoadData(dialog.OpenTimesheetId.Value);
                }
                else if (dialog.DataChanged)
                {
                    await LoadData(Current != null ? Current.Id : (int?)null);
                }
            }
        }

        // One page for every employee of a cutoff: pick frequency + year +
        // month + period, see who has a sheet, bulk-create the missing ones,
        // and jump into any employee's timesheet.
        private class CutoffBoardDialog : Form
        {
            private readonly ComboBox _frequency = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 120 };
            private readonly TextBox _year = new TextBox { Width = 60, Text = DateTime.Now.Year.ToString() };
            private readonly TextBox _month = new TextBox { Width = 40, Text = DateTime.Now.Month.ToString() };
            private readonly ComboBox _period = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 50 };
            private readonly DataGridView _grid;
            private List<HrisCutoffRowModel> _rows = new List<HrisCutoffRowModel>();

            public int? OpenTimesheetId { get; private set; }
            public bool DataChanged { get; private set; }

            public CutoffBoardDialog()
            {
                Text = "Cutoff Board — timesheets per cutoff";
                FormBorderStyle = FormBorderStyle.FixedDialog;
                StartPosition = FormStartPosition.CenterParent;
                MaximizeBox = false;
                MinimizeBox = false;
                ClientSize = new System.Drawing.Size(860, 480);

                _frequency.Items.AddRange(new object[] { "SEMI_MONTHLY", "WEEKLY", "MONTHLY" });
                _frequency.SelectedIndex = 0;
                _frequency.SelectedIndexChanged += (s, e) => FillPeriods();
                FillPeriods();

                var bar = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 34, Padding = new Padding(8, 6, 0, 0) };
                void Lbl(string t) { bar.Controls.Add(new Label { Text = t, AutoSize = true, Padding = new Padding(6, 5, 2, 0) }); }
                Lbl("FREQUENCY"); bar.Controls.Add(_frequency);
                Lbl("YEAR"); bar.Controls.Add(_year);
                Lbl("MONTH"); bar.Controls.Add(_month);
                Lbl("PERIOD"); bar.Controls.Add(_period);
                var load = new Button { Text = "Load", Width = 70 };
                load.Click += async (s, e) => await LoadBoard();
                bar.Controls.Add(load);
                var create = new Button { Text = "Create Missing", Width = 110 };
                create.Click += async (s, e) => await CreateMissing();
                bar.Controls.Add(create);
                var open = new Button { Text = "Open Selected", Width = 110 };
                open.Click += (s, e) => OpenSelected();
                bar.Controls.Add(open);

                _grid = new DataGridView
                {
                    Dock = DockStyle.Fill,
                    ReadOnly = true,
                    AllowUserToAddRows = false,
                    RowHeadersVisible = false,
                    SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                    MultiSelect = false,
                    AutoGenerateColumns = false
                };
                _grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "tsId", Visible = false });
                _grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "employeeNo", HeaderText = "EMPLOYEE NO", Width = 120 });
                _grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "name", HeaderText = "NAME", Width = 220 });
                _grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "status", HeaderText = "STATUS", Width = 110 });
                _grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "days", HeaderText = "DAYS", Width = 55 });
                _grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "hours", HeaderText = "HOURS", Width = 65 });
                _grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "ot", HeaderText = "OT", Width = 50 });
                _grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "nd", HeaderText = "ND", Width = 50 });
                _grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "late", HeaderText = "LATE/UT", Width = 70 });
                _grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "leave", HeaderText = "LEAVE P/U", Width = 75 });
                _grid.CellDoubleClick += (s, e) => { if (e.RowIndex >= 0) OpenSelected(); };

                Controls.Add(_grid);
                Controls.Add(bar);
            }

            private void FillPeriods()
            {
                int max = _frequency.Text == "WEEKLY" ? 4 : _frequency.Text == "MONTHLY" ? 1 : 2;
                _period.Items.Clear();
                for (int i = 1; i <= max; i++) _period.Items.Add(i.ToString());
                _period.SelectedIndex = 0;
            }

            private bool ReadCutoff(out int year, out int month, out int period)
            {
                period = _period.SelectedIndex + 1;
                bool ok = int.TryParse(_year.Text, out year) & int.TryParse(_month.Text, out month);
                if (!ok) Helpers.ShowDialogMessage("error", "Year and month must be numbers.");
                return ok;
            }

            private async System.Threading.Tasks.Task LoadBoard()
            {
                if (!ReadCutoff(out int year, out int month, out int period)) return;
                try
                {
                    var result = await HrisTimesheetService.GetCutoffBoardAsync(_frequency.Text, year, month, period);
                    if (result.HasErrors)
                    {
                        Helpers.ShowDialogMessage("error", result.ErrorMessage);
                        return;
                    }
                    _rows = result.Data.CutoffBoard ?? new List<HrisCutoffRowModel>();
                    _grid.Rows.Clear();
                    foreach (var row in _rows)
                    {
                        var ts = row.Timesheet;
                        _grid.Rows.Add(
                            ts != null ? (object)ts.Id : null,
                            row.Employee.EmployeeNo,
                            ($"{row.Employee.FirstName} {row.Employee.LastName}").Trim(),
                            ts != null ? ts.Status : "NO TIMESHEET",
                            ts != null ? ts.DaysWorked.ToString() : "",
                            ts != null ? ts.TotalHours.ToString("0.##") : "",
                            ts != null ? ts.TotalOtHours.ToString("0.##") : "",
                            ts != null ? ts.TotalNdHours.ToString("0.##") : "",
                            ts != null ? (ts.TotalLateMinutes + ts.TotalUndertimeMinutes).ToString() : "",
                            ts != null ? $"{ts.DaysPaidLeave}/{ts.DaysUnpaidLeave}" : "");
                    }
                }
                catch (Exception ex)
                {
                    Helpers.ShowDialogMessage("error", $"Failed to load board: {ex.Message}");
                }
            }

            private async System.Threading.Tasks.Task CreateMissing()
            {
                if (!ReadCutoff(out int year, out int month, out int period)) return;
                var confirm = MessageBox.Show(
                    "Create DRAFT timesheets (pre-filled schedule days, Sundays as rest days) for every listed employee without one?",
                    "Create Missing Timesheets", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm != DialogResult.Yes) return;
                try
                {
                    var result = await HrisTimesheetService.CreateCutoffTimesheetsAsync(_frequency.Text, year, month, period);
                    if (result.HasErrors)
                    {
                        Helpers.ShowDialogMessage("error", result.ErrorMessage);
                        return;
                    }
                    DataChanged = DataChanged || result.Data.CreateCutoffTimesheets.Created > 0;
                    string message = $"Created {result.Data.CreateCutoffTimesheets.Created} timesheet(s).";
                    if (!string.IsNullOrWhiteSpace(result.Data.CreateCutoffTimesheets.Notes))
                        message += "\nSkipped: " + result.Data.CreateCutoffTimesheets.Notes;
                    Helpers.ShowDialogMessage("success", message);
                    await LoadBoard();
                }
                catch (Exception ex)
                {
                    Helpers.ShowDialogMessage("error", $"Failed to create: {ex.Message}");
                }
            }

            private void OpenSelected()
            {
                if (_grid.SelectedRows.Count == 0) return;
                var idValue = _grid.SelectedRows[0].Cells["tsId"].Value;
                if (idValue == null)
                {
                    Helpers.ShowDialogMessage("error", "No timesheet yet for this employee — use Create Missing first.");
                    return;
                }
                OpenTimesheetId = Convert.ToInt32(idValue);
                DialogResult = DialogResult.OK;
            }
        }

        // ------------------------------------------------------------ search dialog

        // Filters live across employee no / first / middle / last name and period.
        private class SearchTimesheetDialog : Form
        {
            private readonly TextBox _search = new TextBox { Width = 300 };
            private readonly DataGridView _grid;
            private readonly List<HrisTimesheetModel> _all;

            public int? SelectedTimesheetId { get; private set; }

            public SearchTimesheetDialog(List<HrisTimesheetModel> timesheets)
            {
                _all = timesheets;

                Text = "Timesheet Search";
                FormBorderStyle = FormBorderStyle.FixedDialog;
                StartPosition = FormStartPosition.CenterParent;
                MaximizeBox = false;
                MinimizeBox = false;
                ClientSize = new System.Drawing.Size(700, 430);

                _grid = new DataGridView
                {
                    Location = new System.Drawing.Point(15, 50),
                    Size = new System.Drawing.Size(670, 320),
                    ReadOnly = true,
                    AllowUserToAddRows = false,
                    RowHeadersVisible = false,
                    SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                    MultiSelect = false,
                    AutoGenerateColumns = false
                };
                _grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "id", Visible = false });
                _grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "employee", HeaderText = "EMPLOYEE", Width = 220 });
                _grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "periodStart", HeaderText = "PERIOD START", Width = 110 });
                _grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "periodEnd", HeaderText = "PERIOD END", Width = 110 });
                _grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "status", HeaderText = "STATUS", Width = 90 });
                _grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "hours", HeaderText = "HOURS", Width = 80 });
                _grid.CellDoubleClick += (s, e) => { if (e.RowIndex >= 0) Choose(); };

                var searchLabel = new Label { Text = "SEARCH", Location = new System.Drawing.Point(15, 20), AutoSize = true };
                _search.Location = new System.Drawing.Point(80, 17);
                _search.TextChanged += (s, e) => Refill();

                var select = new Button { Text = "Select", Width = 90, Location = new System.Drawing.Point(490, 385) };
                select.Click += (s, e) => Choose();
                var cancel = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel, Width = 90, Location = new System.Drawing.Point(595, 385) };

                Controls.Add(searchLabel);
                Controls.Add(_search);
                Controls.Add(_grid);
                Controls.Add(select);
                Controls.Add(cancel);
                AcceptButton = select;
                CancelButton = cancel;

                Refill();
            }

            private void Choose()
            {
                if (_grid.SelectedRows.Count == 0) return;
                SelectedTimesheetId = Convert.ToInt32(_grid.SelectedRows[0].Cells["id"].Value);
                DialogResult = DialogResult.OK;
            }

            private void Refill()
            {
                string term = _search.Text.Trim();
                var matches = string.IsNullOrEmpty(term)
                    ? _all
                    : _all.Where(x =>
                        Has(x.Employee != null ? x.Employee.FirstName : null, term)
                        || Has(x.Employee != null ? x.Employee.MiddleName : null, term)
                        || Has(x.Employee != null ? x.Employee.LastName : null, term)
                        || Has(x.Employee != null ? x.Employee.EmployeeNo : null, term)
                        || Has(x.PeriodStart, term) || Has(x.PeriodEnd, term)
                        || Has(x.Status, term)).ToList();

                _grid.Rows.Clear();
                foreach (var ts in matches)
                {
                    string employee = ts.Employee != null
                        ? $"{ts.Employee.EmployeeNo} — {ts.Employee.FirstName} {ts.Employee.LastName}"
                        : ts.EmployeeId.ToString();
                    _grid.Rows.Add(ts.Id, employee, ts.PeriodStart, ts.PeriodEnd, ts.Status, ts.TotalHours);
                }
            }

            private static bool Has(string value, string term)
            {
                return value != null && value.IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0;
            }
        }
    }
}
