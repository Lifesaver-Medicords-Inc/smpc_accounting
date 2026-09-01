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
    // HRIS Employee Information setup (201 file master) - data comes from the
    // HRIS GraphQL API (D:\HRIS, port 4001), NOT the ERP REST API.
    //
    // Document-style form per the suite convention: NEW - SEARCH - << PREV -
    // NEXT >> step through one employee at a time; SEARCH opens a popup list
    // (searchable by first / middle / last name). There is no permanent list
    // grid on the form.
    //
    // The field controls inside each tab are built in code (BuildDetailTabs)
    // rather than in the Designer - the Designer holds only the page skeleton.
    //
    // IMPORTANT (RoutesService instantiates every page eagerly): the constructor
    // does no I/O; all API calls start in the Load event.
    public partial class EmployeeInformationPage : UserControl
    {
        private static readonly string[] Departments =
            { "Admin", "HR", "Sales", "Engineering", "Purchasing", "Dispatch", "Warehouse", "Accounting" };
        private static readonly string[] EmploymentStatuses =
            { "PROBATIONARY", "REGULAR", "CONTRACTUAL", "PROJECT_BASED", "RESIGNED", "TERMINATED", "AWOL", "RETIRED" };
        private static readonly string[] CivilStatuses = { "", "SINGLE", "MARRIED", "WIDOWED", "SEPARATED" };
        private static readonly string[] RateTypes = { "MONTHLY", "DAILY", "HOURLY" };
        private static readonly string[] ScheduleTypes = { "FIXED", "FLEXIBLE" };
        private static readonly string[] PayFrequencies = { "SEMI_MONTHLY", "WEEKLY", "MONTHLY" };
        private static readonly string[] FileCategories = { "CONTRACT", "GOV_ID", "MEMO", "OTHER" };

        private List<HrisEmployeeModel> _employees = new List<HrisEmployeeModel>();
        private List<HrisPositionModel> _positions = new List<HrisPositionModel>();
        private int _currentIndex = -1;
        private bool _isNewMode;
        private bool _editMode;
        private bool _loaded;

        private HrisEmployeeModel Current =>
            _currentIndex >= 0 && _currentIndex < _employees.Count ? _employees[_currentIndex] : null;

        // Identity & Employment
        private TextBox txt_employeeNo, txt_firstName, txt_middleName, txt_lastName, txt_suffix,
            txt_birthDate, txt_hireDate, txt_regularizationDate, txt_endDate;
        private ComboBox cmb_gender, cmb_civilStatus, cmb_department, cmb_position, cmb_employmentStatus, cmb_scheduleType, cmb_payFrequency;
        private TextBox txt_workStart, txt_workEnd;
        private CheckBox chk_isActive;
        // Government IDs
        private TextBox txt_sssNo, txt_philhealthNo, txt_pagibigNo, txt_tin, txt_taxStatus;
        // Compensation
        private TextBox txt_basicPay, txt_bankName, txt_bankAccountNo, txt_compEffectiveDate;
        private ComboBox cmb_rateType;
        private DataGridView dgv_allowances;
        // Contacts & Address
        private TextBox txt_mobileNo, txt_email, txt_unitNo, txt_streetName, txt_barangay,
            txt_city, txt_province, txt_postalCode, txt_ecName, txt_ecRelationship, txt_ecContactNo;
        // 201 Records
        private DataGridView dgv_dependents, dgv_educations, dgv_work;
        // Files
        private DataGridView dgv_files;
        private ComboBox cmb_fileCategory;
        private Button btn_uploadFile, btn_deleteFile;

        public EmployeeInformationPage()
        {
            InitializeComponent();
            BuildDetailTabs();
            SetEditMode(false);
        }

        // ------------------------------------------------------------ layout

        private void BuildDetailTabs()
        {
            // Identity & Employment
            var t = NewFieldTable();
            txt_employeeNo = AddText(t, "EMPLOYEE NO *");
            txt_firstName = AddText(t, "FIRST NAME *");
            txt_middleName = AddText(t, "MIDDLE NAME");
            txt_lastName = AddText(t, "LAST NAME *");
            txt_suffix = AddText(t, "SUFFIX");
            txt_birthDate = AddText(t, "BIRTH DATE (YYYY-MM-DD)");
            cmb_gender = AddCombo(t, "GENDER", new[] { "", "Male", "Female" });
            cmb_civilStatus = AddCombo(t, "CIVIL STATUS", CivilStatuses);
            cmb_department = AddCombo(t, "DEPARTMENT", Departments);
            cmb_position = AddCombo(t, "POSITION", null);
            cmb_employmentStatus = AddCombo(t, "EMPLOYMENT STATUS", EmploymentStatuses);
            cmb_scheduleType = AddCombo(t, "SCHEDULE TYPE", ScheduleTypes);
            cmb_payFrequency = AddCombo(t, "PAY FREQUENCY", PayFrequencies);
            txt_workStart = AddText(t, "WORK START (HH:MM)");
            txt_workEnd = AddText(t, "WORK END (HH:MM)");
            txt_hireDate = AddText(t, "HIRE DATE (YYYY-MM-DD)");
            txt_regularizationDate = AddText(t, "REGULARIZATION DATE");
            txt_endDate = AddText(t, "END DATE");
            chk_isActive = AddCheck(t, "ACTIVE");
            tab_identity.Controls.Add(t);

            // Government IDs
            t = NewFieldTable();
            txt_sssNo = AddText(t, "SSS NO");
            txt_philhealthNo = AddText(t, "PHILHEALTH NO");
            txt_pagibigNo = AddText(t, "PAG-IBIG NO");
            txt_tin = AddText(t, "TIN");
            txt_taxStatus = AddText(t, "TAX STATUS");
            tab_gov.Controls.Add(t);

            // Compensation (current record + allowances grid)
            t = NewFieldTable();
            t.Dock = DockStyle.Top;
            t.Height = 170; // 5 single-column rows
            txt_basicPay = AddText(t, "BASIC PAY");
            cmb_rateType = AddCombo(t, "RATE TYPE", RateTypes);
            txt_bankName = AddText(t, "BANK NAME");
            txt_bankAccountNo = AddText(t, "BANK ACCOUNT NO");
            txt_compEffectiveDate = AddText(t, "EFFECTIVE DATE (YYYY-MM-DD)");
            dgv_allowances = MakeChildGrid();
            AddTextColumn(dgv_allowances, "name", "ALLOWANCE", 200);
            AddTextColumn(dgv_allowances, "amount", "AMOUNT", 100);
            AddCheckColumn(dgv_allowances, "isTaxable", "TAXABLE");
            AddTextColumn(dgv_allowances, "effectiveDate", "EFFECTIVE DATE", 130);
            tab_comp.Controls.Add(WrapGrid("ALLOWANCES", dgv_allowances));
            tab_comp.Controls.Add(t);

            // Contacts & Address
            t = NewFieldTable();
            txt_mobileNo = AddText(t, "MOBILE NO");
            txt_email = AddText(t, "EMAIL");
            txt_unitNo = AddText(t, "UNIT NO");
            txt_streetName = AddText(t, "STREET");
            txt_barangay = AddText(t, "BARANGAY");
            txt_city = AddText(t, "CITY");
            txt_province = AddText(t, "PROVINCE");
            txt_postalCode = AddText(t, "POSTAL CODE");
            txt_ecName = AddText(t, "EMERGENCY CONTACT");
            txt_ecRelationship = AddText(t, "EC RELATIONSHIP");
            txt_ecContactNo = AddText(t, "EC CONTACT NO");
            tab_contacts.Controls.Add(t);

            // 201 Records: three editable grids
            dgv_dependents = MakeChildGrid();
            AddTextColumn(dgv_dependents, "name", "DEPENDENT", 200);
            AddTextColumn(dgv_dependents, "relationship", "RELATIONSHIP", 120);
            AddTextColumn(dgv_dependents, "birthDate", "BIRTH DATE", 110);

            dgv_educations = MakeChildGrid();
            AddTextColumn(dgv_educations, "level", "LEVEL", 100);
            AddTextColumn(dgv_educations, "school", "SCHOOL", 220);
            AddTextColumn(dgv_educations, "course", "COURSE", 220);
            AddTextColumn(dgv_educations, "yearFrom", "FROM", 70);
            AddTextColumn(dgv_educations, "yearTo", "TO", 70);

            dgv_work = MakeChildGrid();
            AddTextColumn(dgv_work, "employer", "EMPLOYER", 220);
            AddTextColumn(dgv_work, "position", "POSITION", 150);
            AddTextColumn(dgv_work, "dateFrom", "FROM", 100);
            AddTextColumn(dgv_work, "dateTo", "TO", 100);
            AddTextColumn(dgv_work, "reasonForLeaving", "REASON FOR LEAVING", 220);

            var records = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 3 };
            records.RowStyles.Add(new RowStyle(SizeType.Percent, 33F));
            records.RowStyles.Add(new RowStyle(SizeType.Percent, 33F));
            records.RowStyles.Add(new RowStyle(SizeType.Percent, 34F));
            records.Controls.Add(WrapGrid("DEPENDENTS", dgv_dependents), 0, 0);
            records.Controls.Add(WrapGrid("EDUCATION", dgv_educations), 0, 1);
            records.Controls.Add(WrapGrid("WORK HISTORY", dgv_work), 0, 2);
            tab_records.Controls.Add(records);

            // Files
            dgv_files = MakeChildGrid();
            dgv_files.ReadOnly = true;
            dgv_files.AllowUserToAddRows = false;
            dgv_files.AllowUserToDeleteRows = false;
            AddTextColumn(dgv_files, "id", "ID", 50, visible: false);
            AddTextColumn(dgv_files, "originalName", "FILE", 300);
            AddTextColumn(dgv_files, "category", "CATEGORY", 110);
            AddTextColumn(dgv_files, "type", "TYPE", 160);
            AddTextColumn(dgv_files, "size", "SIZE", 90);

            var fileBar = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 36, Padding = new Padding(6, 6, 6, 0) };
            cmb_fileCategory = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 130 };
            cmb_fileCategory.Items.AddRange(FileCategories);
            cmb_fileCategory.SelectedIndex = 0;
            btn_uploadFile = new Button { Text = "Upload File...", Width = 110 };
            btn_uploadFile.Click += btn_uploadFile_Click;
            btn_deleteFile = new Button { Text = "Delete File", Width = 100 };
            btn_deleteFile.Click += btn_deleteFile_Click;
            fileBar.Controls.Add(new Label { Text = "CATEGORY", AutoSize = true, Padding = new Padding(0, 6, 4, 0) });
            fileBar.Controls.Add(cmb_fileCategory);
            fileBar.Controls.Add(btn_uploadFile);
            fileBar.Controls.Add(btn_deleteFile);
            tab_files.Controls.Add(dgv_files);
            tab_files.Controls.Add(fileBar);
        }

        // Single-column form: one LABEL + FIELD pair per row, rows auto-sized so
        // there is no stretch gap between the last fields.
        private TableLayoutPanel NewFieldTable()
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
            return t;
        }

        private void PlaceField(TableLayoutPanel t, string label, Control control)
        {
            t.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            t.Controls.Add(new Label { Text = label, AutoSize = true, Anchor = AnchorStyles.Left, Padding = new Padding(0, 4, 0, 0) });
            control.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            t.Controls.Add(control);
        }

        private TextBox AddText(TableLayoutPanel t, string label)
        {
            var txt = new TextBox { BackColor = System.Drawing.Color.Gainsboro };
            PlaceField(t, label, txt);
            return txt;
        }

        private ComboBox AddCombo(TableLayoutPanel t, string label, string[] items)
        {
            var cmb = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
            if (items != null) cmb.Items.AddRange(items);
            PlaceField(t, label, cmb);
            return cmb;
        }

        private CheckBox AddCheck(TableLayoutPanel t, string label)
        {
            var chk = new CheckBox { AutoSize = true };
            PlaceField(t, label, chk);
            return chk;
        }

        private DataGridView MakeChildGrid()
        {
            return new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToResizeRows = false,
                RowHeadersVisible = false,
                AutoGenerateColumns = false,
                BackgroundColor = System.Drawing.SystemColors.Window
            };
        }

        private void AddTextColumn(DataGridView grid, string name, string header, int width, bool visible = true)
        {
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = name, HeaderText = header, Width = width, Visible = visible });
        }

        private void AddCheckColumn(DataGridView grid, string name, string header)
        {
            grid.Columns.Add(new DataGridViewCheckBoxColumn { Name = name, HeaderText = header, Width = 70 });
        }

        private Panel WrapGrid(string title, DataGridView grid)
        {
            var panel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(6, 20, 6, 6) };
            panel.Controls.Add(grid);
            panel.Controls.Add(new Label { Text = title, Dock = DockStyle.Top, Height = 16, Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold) });
            return panel;
        }

        // ------------------------------------------------------------ loading

        // Same look as Helpers.Loading, but that helper is typed to DataGridView;
        // this page has no permanent grid, so the overlay covers the detail tabs.
        private UserControl _busyOverlay;

        private void ShowBusy(string message)
        {
            if (_busyOverlay != null) return;
            _busyOverlay = new UserControl
            {
                BackColor = System.Drawing.Color.FromArgb(180, System.Drawing.Color.Gray),
                Dock = DockStyle.Fill
            };
            _busyOverlay.Controls.Add(new Label
            {
                Dock = DockStyle.Fill,
                Text = message,
                ForeColor = System.Drawing.Color.White,
                Font = new System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Bold),
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            });
            tabControlDetail.Parent.Controls.Add(_busyOverlay);
            _busyOverlay.BringToFront();
        }

        private void HideBusy()
        {
            if (_busyOverlay == null) return;
            tabControlDetail.Parent.Controls.Remove(_busyOverlay);
            _busyOverlay.Dispose();
            _busyOverlay = null;
        }

        private async void EmployeeInformationPage_Load(object sender, EventArgs e)
        {
            if (_loaded) return;
            _loaded = true;
            try
            {
                ShowBusy("Fetching data...");
                var positions = await HrisEmployeeService.GetPositionsAsync();
                if (positions.HasErrors)
                {
                    Helpers.ShowDialogMessage("error", positions.ErrorMessage);
                    return;
                }
                _positions = positions.Data.Positions ?? new List<HrisPositionModel>();
                cmb_position.Items.Clear();
                cmb_position.Items.Add(""); // no position
                foreach (var p in _positions) cmb_position.Items.Add(p.Name);

                await LoadData();
            }
            catch (Exception ex)
            {
                Helpers.ShowDialogMessage("error", $"Failed to load: {ex.Message}");
            }
            finally
            {
                HideBusy();
            }
        }

        // Reloads all employees and repositions on keepId (or the first record).
        private async System.Threading.Tasks.Task LoadData(int? keepId = null)
        {
            var result = await HrisEmployeeService.GetEmployeesAsync();
            if (result.HasErrors)
            {
                Helpers.ShowDialogMessage("error", result.ErrorMessage);
                return;
            }
            _employees = result.Data.Employees.Items ?? new List<HrisEmployeeModel>();

            if (keepId.HasValue)
            {
                _currentIndex = _employees.FindIndex(x => x.Id == keepId.Value);
                if (_currentIndex < 0 && _employees.Count > 0) _currentIndex = 0;
            }
            else
            {
                _currentIndex = _employees.Count > 0 ? 0 : -1;
            }
            ShowCurrent();
        }

        private void ShowCurrent()
        {
            if (Current != null) BindDetail(Current);
            else ClearDetail();
            UpdateRecordLabel();
            UpdateNavButtons();
        }

        private void UpdateRecordLabel()
        {
            lbl_record.Text = _employees.Count == 0
                ? "0 / 0"
                : $"{_currentIndex + 1} / {_employees.Count}";
        }

        private void UpdateNavButtons()
        {
            btn_prev.Enabled = !_editMode && _currentIndex > 0;
            btn_next.Enabled = !_editMode && _currentIndex >= 0 && _currentIndex < _employees.Count - 1;
            btn_search.Enabled = !_editMode;

            bool filesEnabled = !_editMode && Current != null;
            btn_uploadFile.Enabled = filesEnabled;
            btn_deleteFile.Enabled = filesEnabled;
            cmb_fileCategory.Enabled = filesEnabled;
        }

        private void BindDetail(HrisEmployeeModel emp)
        {
            txt_employeeNo.Text = emp.EmployeeNo;
            txt_firstName.Text = emp.FirstName;
            txt_middleName.Text = emp.MiddleName;
            txt_lastName.Text = emp.LastName;
            txt_suffix.Text = emp.Suffix;
            txt_birthDate.Text = emp.BirthDate;
            cmb_gender.Text = emp.Gender ?? "";
            cmb_civilStatus.Text = emp.CivilStatus ?? "";
            cmb_department.Text = emp.Department ?? "";
            cmb_position.Text = emp.Position != null ? emp.Position.Name : "";
            cmb_employmentStatus.Text = emp.EmploymentStatus ?? "";
            cmb_scheduleType.Text = emp.ScheduleType ?? "";
            cmb_payFrequency.Text = string.IsNullOrEmpty(emp.PayFrequency) ? "SEMI_MONTHLY" : emp.PayFrequency;
            txt_workStart.Text = emp.WorkStartTime;
            txt_workEnd.Text = emp.WorkEndTime;
            txt_hireDate.Text = emp.HireDate;
            txt_regularizationDate.Text = emp.RegularizationDate;
            txt_endDate.Text = emp.EndDate;
            chk_isActive.Checked = emp.IsActive;

            txt_sssNo.Text = emp.SssNo;
            txt_philhealthNo.Text = emp.PhilhealthNo;
            txt_pagibigNo.Text = emp.PagibigNo;
            txt_tin.Text = emp.Tin;
            txt_taxStatus.Text = emp.TaxStatus;

            var comp = emp.Compensations.FirstOrDefault(c => c.IsCurrent) ?? emp.Compensations.FirstOrDefault();
            txt_basicPay.Text = comp != null ? comp.BasicPay.ToString(CultureInfo.InvariantCulture) : "";
            cmb_rateType.Text = comp != null ? comp.RateType : "";
            txt_bankName.Text = comp != null ? comp.BankName : "";
            txt_bankAccountNo.Text = comp != null ? comp.BankAccountNo : "";
            txt_compEffectiveDate.Text = comp != null ? comp.EffectiveDate : "";

            dgv_allowances.Rows.Clear();
            foreach (var a in emp.Allowances)
                dgv_allowances.Rows.Add(a.Name, a.Amount.ToString(CultureInfo.InvariantCulture), a.IsTaxable, a.EffectiveDate);

            txt_mobileNo.Text = emp.MobileNo;
            txt_email.Text = emp.Email;
            var addr = emp.Addresses.FirstOrDefault();
            txt_unitNo.Text = addr != null ? addr.UnitNo : "";
            txt_streetName.Text = addr != null ? addr.StreetName : "";
            txt_barangay.Text = addr != null ? addr.Barangay : "";
            txt_city.Text = addr != null ? addr.City : "";
            txt_province.Text = addr != null ? addr.Province : "";
            txt_postalCode.Text = addr != null ? addr.PostalCode : "";
            var ec = emp.EmergencyContacts.FirstOrDefault();
            txt_ecName.Text = ec != null ? ec.Name : "";
            txt_ecRelationship.Text = ec != null ? ec.Relationship : "";
            txt_ecContactNo.Text = ec != null ? ec.ContactNo : "";

            dgv_dependents.Rows.Clear();
            foreach (var d in emp.Dependents)
                dgv_dependents.Rows.Add(d.Name, d.Relationship, d.BirthDate);
            dgv_educations.Rows.Clear();
            foreach (var ed in emp.Educations)
                dgv_educations.Rows.Add(ed.Level, ed.School, ed.Course,
                    ed.YearFrom.HasValue ? ed.YearFrom.ToString() : "",
                    ed.YearTo.HasValue ? ed.YearTo.ToString() : "");
            dgv_work.Rows.Clear();
            foreach (var w in emp.WorkHistories)
                dgv_work.Rows.Add(w.Employer, w.Position, w.DateFrom, w.DateTo, w.ReasonForLeaving);

            dgv_files.Rows.Clear();
            foreach (var f in emp.Files)
                dgv_files.Rows.Add(f.Id, f.OriginalName, f.Category, f.Type, f.Size);
        }

        private void ClearDetail()
        {
            foreach (var txt in new[] { txt_employeeNo, txt_firstName, txt_middleName, txt_lastName, txt_suffix,
                txt_birthDate, txt_workStart, txt_workEnd, txt_hireDate, txt_regularizationDate, txt_endDate,
                txt_sssNo, txt_philhealthNo, txt_pagibigNo, txt_tin, txt_taxStatus,
                txt_basicPay, txt_bankName, txt_bankAccountNo, txt_compEffectiveDate,
                txt_mobileNo, txt_email, txt_unitNo, txt_streetName, txt_barangay,
                txt_city, txt_province, txt_postalCode, txt_ecName, txt_ecRelationship, txt_ecContactNo })
            {
                txt.Text = "";
            }
            foreach (var cmb in new[] { cmb_gender, cmb_civilStatus, cmb_department, cmb_position, cmb_rateType })
            {
                cmb.SelectedIndex = -1;
            }
            cmb_employmentStatus.Text = "PROBATIONARY";
            cmb_scheduleType.Text = "FIXED";
            cmb_payFrequency.Text = "SEMI_MONTHLY";
            txt_workStart.Text = "08:00";
            txt_workEnd.Text = "17:00";
            chk_isActive.Checked = true;
            foreach (var grid in new[] { dgv_allowances, dgv_dependents, dgv_educations, dgv_work, dgv_files })
            {
                grid.Rows.Clear();
            }
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
            btn_status.Visible = !enable;
            btn_link.Visible = !enable;

            foreach (var txt in new[] { txt_employeeNo, txt_firstName, txt_middleName, txt_lastName, txt_suffix,
                txt_birthDate, txt_workStart, txt_workEnd, txt_hireDate, txt_regularizationDate, txt_endDate,
                txt_sssNo, txt_philhealthNo, txt_pagibigNo, txt_tin, txt_taxStatus,
                txt_basicPay, txt_bankName, txt_bankAccountNo, txt_compEffectiveDate,
                txt_mobileNo, txt_email, txt_unitNo, txt_streetName, txt_barangay,
                txt_city, txt_province, txt_postalCode, txt_ecName, txt_ecRelationship, txt_ecContactNo })
            {
                txt.ReadOnly = !enable;
            }
            foreach (var cmb in new[] { cmb_gender, cmb_civilStatus, cmb_department, cmb_position, cmb_employmentStatus, cmb_scheduleType, cmb_payFrequency, cmb_rateType })
            {
                cmb.Enabled = enable;
            }
            chk_isActive.Enabled = enable;
            foreach (var grid in new[] { dgv_allowances, dgv_dependents, dgv_educations, dgv_work })
            {
                grid.ReadOnly = !enable;
                grid.AllowUserToAddRows = enable;
                grid.AllowUserToDeleteRows = enable;
            }

            UpdateNavButtons();
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            ClearDetail();
            SetEditMode(true, isNewMode: true);
            tabControlDetail.SelectedTab = tab_identity;
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            if (Current == null)
            {
                Helpers.ShowDialogMessage("error", "No employee loaded. Use Search or New.");
                return;
            }
            SetEditMode(true);
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            SetEditMode(false);
            ShowCurrent();
        }

        // ------------------------------------------------------------ navigation

        private void btn_prev_Click(object sender, EventArgs e)
        {
            if (_currentIndex <= 0) return;
            _currentIndex--;
            ShowCurrent();
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            if (_currentIndex >= _employees.Count - 1) return;
            _currentIndex++;
            ShowCurrent();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            if (_employees.Count == 0)
            {
                Helpers.ShowDialogMessage("error", "No employees yet. Use New to create one.");
                return;
            }
            using (var dialog = new SearchEmployeeDialog(_employees))
            {
                if (dialog.ShowDialog(FindForm()) != DialogResult.OK || !dialog.SelectedEmployeeId.HasValue) return;
                int index = _employees.FindIndex(x => x.Id == dialog.SelectedEmployeeId.Value);
                if (index >= 0)
                {
                    _currentIndex = index;
                    ShowCurrent();
                }
            }
        }

        // ------------------------------------------------------------ save

        private async void btn_save_Click(object sender, EventArgs e)
        {
            btn_save.Enabled = false;
            btn_cancel.Enabled = false;
            try
            {
                if (string.IsNullOrWhiteSpace(txt_employeeNo.Text)
                    || string.IsNullOrWhiteSpace(txt_firstName.Text)
                    || string.IsNullOrWhiteSpace(txt_lastName.Text))
                {
                    Helpers.ShowDialogMessage("error", "Employee No, First Name and Last Name are required.");
                    return;
                }

                var input = BuildInput();
                if (input == null) return; // validation error already shown

                ShowBusy("Saving data...");

                string error;
                int? savedId = null;
                if (_isNewMode)
                {
                    var result = await HrisEmployeeService.CreateAsync(input);
                    error = result.HasErrors ? result.ErrorMessage : null;
                    if (error == null) savedId = result.Data.CreateEmployee.Id;
                }
                else
                {
                    var result = await HrisEmployeeService.UpdateAsync(Current.Id, input);
                    error = result.HasErrors ? result.ErrorMessage : null;
                    if (error == null) savedId = result.Data.UpdateEmployee.Id;
                }

                if (error != null)
                {
                    Helpers.ShowDialogMessage("error", error);
                    return;
                }

                Helpers.ShowDialogMessage("success", _isNewMode ? "Employee created successfully." : "Employee updated successfully.");
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
                HideBusy();
            }
        }

        private Dictionary<string, object> BuildInput()
        {
            var input = new Dictionary<string, object>
            {
                { "employeeNo", txt_employeeNo.Text.Trim() },
                { "firstName", txt_firstName.Text.Trim() },
                { "middleName", txt_middleName.Text.Trim() },
                { "lastName", txt_lastName.Text.Trim() },
                { "suffix", txt_suffix.Text.Trim() },
                { "gender", cmb_gender.Text },
                { "department", cmb_department.Text },
                { "sssNo", txt_sssNo.Text.Trim() },
                { "philhealthNo", txt_philhealthNo.Text.Trim() },
                { "pagibigNo", txt_pagibigNo.Text.Trim() },
                { "tin", txt_tin.Text.Trim() },
                { "taxStatus", txt_taxStatus.Text.Trim() },
                { "mobileNo", txt_mobileNo.Text.Trim() },
                { "email", txt_email.Text.Trim() },
                { "isActive", chk_isActive.Checked },
            };
            AddIfNotBlank(input, "birthDate", txt_birthDate.Text);
            AddIfNotBlank(input, "hireDate", txt_hireDate.Text);
            AddIfNotBlank(input, "regularizationDate", txt_regularizationDate.Text);
            AddIfNotBlank(input, "endDate", txt_endDate.Text);
            AddIfNotBlank(input, "civilStatus", cmb_civilStatus.Text);
            AddIfNotBlank(input, "employmentStatus", cmb_employmentStatus.Text);
            AddIfNotBlank(input, "scheduleType", cmb_scheduleType.Text);
            AddIfNotBlank(input, "payFrequency", cmb_payFrequency.Text);
            AddIfNotBlank(input, "workStartTime", txt_workStart.Text);
            AddIfNotBlank(input, "workEndTime", txt_workEnd.Text);

            var position = _positions.FirstOrDefault(p => p.Name == cmb_position.Text);
            if (position != null) input["positionId"] = position.Id;
            if (!_isNewMode && Current != null && Current.UserId.HasValue)
                input["userId"] = Current.UserId.Value; // preserve the ERP-login link on update

            // Single CURRENT address, if anything was typed
            if (!AllBlank(txt_unitNo, txt_streetName, txt_barangay, txt_city, txt_province, txt_postalCode))
            {
                input["addresses"] = new List<object> { new Dictionary<string, object>
                {
                    { "addressType", "CURRENT" },
                    { "unitNo", txt_unitNo.Text.Trim() },
                    { "streetName", txt_streetName.Text.Trim() },
                    { "barangay", txt_barangay.Text.Trim() },
                    { "city", txt_city.Text.Trim() },
                    { "province", txt_province.Text.Trim() },
                    { "country", "Philippines" },
                    { "postalCode", txt_postalCode.Text.Trim() },
                } };
            }

            // Single current compensation record, if a basic pay was typed
            if (!string.IsNullOrWhiteSpace(txt_basicPay.Text))
            {
                if (!decimal.TryParse(txt_basicPay.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal basicPay))
                {
                    Helpers.ShowDialogMessage("error", "Basic Pay must be a number.");
                    return null;
                }
                var comp = new Dictionary<string, object>
                {
                    { "basicPay", basicPay },
                    { "rateType", string.IsNullOrWhiteSpace(cmb_rateType.Text) ? "MONTHLY" : cmb_rateType.Text },
                    { "bankName", txt_bankName.Text.Trim() },
                    { "bankAccountNo", txt_bankAccountNo.Text.Trim() },
                    { "isCurrent", true },
                };
                AddIfNotBlank(comp, "effectiveDate", txt_compEffectiveDate.Text);
                input["compensations"] = new List<object> { comp };
            }

            if (!AllBlank(txt_ecName))
            {
                input["emergencyContacts"] = new List<object> { new Dictionary<string, object>
                {
                    { "name", txt_ecName.Text.Trim() },
                    { "relationship", txt_ecRelationship.Text.Trim() },
                    { "contactNo", txt_ecContactNo.Text.Trim() },
                } };
            }

            var allowances = new List<object>();
            foreach (DataGridViewRow row in dgv_allowances.Rows)
            {
                if (row.IsNewRow || AllBlankCells(row, "name")) continue;
                if (!decimal.TryParse(CellText(row, "amount"), NumberStyles.Number, CultureInfo.InvariantCulture, out decimal amount))
                {
                    Helpers.ShowDialogMessage("error", "Allowance amount must be a number.");
                    return null;
                }
                var item = new Dictionary<string, object>
                {
                    { "name", CellText(row, "name") },
                    { "amount", amount },
                    { "isTaxable", row.Cells["isTaxable"].Value is bool b && b },
                };
                AddIfNotBlank(item, "effectiveDate", CellText(row, "effectiveDate"));
                allowances.Add(item);
            }
            if (allowances.Count > 0) input["allowances"] = allowances;

            var dependents = new List<object>();
            foreach (DataGridViewRow row in dgv_dependents.Rows)
            {
                if (row.IsNewRow || AllBlankCells(row, "name")) continue;
                var item = new Dictionary<string, object>
                {
                    { "name", CellText(row, "name") },
                    { "relationship", CellText(row, "relationship") },
                };
                AddIfNotBlank(item, "birthDate", CellText(row, "birthDate"));
                dependents.Add(item);
            }
            if (dependents.Count > 0) input["dependents"] = dependents;

            var educations = new List<object>();
            foreach (DataGridViewRow row in dgv_educations.Rows)
            {
                if (row.IsNewRow || AllBlankCells(row, "school")) continue;
                var item = new Dictionary<string, object>
                {
                    { "level", CellText(row, "level") },
                    { "school", CellText(row, "school") },
                    { "course", CellText(row, "course") },
                };
                if (int.TryParse(CellText(row, "yearFrom"), out int yearFrom)) item["yearFrom"] = yearFrom;
                if (int.TryParse(CellText(row, "yearTo"), out int yearTo)) item["yearTo"] = yearTo;
                educations.Add(item);
            }
            if (educations.Count > 0) input["educations"] = educations;

            var workHistories = new List<object>();
            foreach (DataGridViewRow row in dgv_work.Rows)
            {
                if (row.IsNewRow || AllBlankCells(row, "employer")) continue;
                var item = new Dictionary<string, object>
                {
                    { "employer", CellText(row, "employer") },
                    { "position", CellText(row, "position") },
                    { "reasonForLeaving", CellText(row, "reasonForLeaving") },
                };
                AddIfNotBlank(item, "dateFrom", CellText(row, "dateFrom"));
                AddIfNotBlank(item, "dateTo", CellText(row, "dateTo"));
                workHistories.Add(item);
            }
            if (workHistories.Count > 0) input["workHistories"] = workHistories;

            return input;
        }

        private static void AddIfNotBlank(Dictionary<string, object> target, string key, string value)
        {
            if (!string.IsNullOrWhiteSpace(value)) target[key] = value.Trim();
        }

        private static bool AllBlank(params TextBox[] boxes)
        {
            return boxes.All(b => string.IsNullOrWhiteSpace(b.Text));
        }

        private static string CellText(DataGridViewRow row, string column)
        {
            var value = row.Cells[column].Value;
            return value == null ? "" : value.ToString().Trim();
        }

        private static bool AllBlankCells(DataGridViewRow row, string keyColumn)
        {
            return string.IsNullOrWhiteSpace(CellText(row, keyColumn));
        }

        // ------------------------------------------------------------ status / link

        private async void btn_status_Click(object sender, EventArgs e)
        {
            if (Current == null)
            {
                Helpers.ShowDialogMessage("error", "No employee loaded. Use Search or New.");
                return;
            }
            using (var dialog = new StatusDialog(Current.EmploymentStatus))
            {
                if (dialog.ShowDialog(FindForm()) != DialogResult.OK) return;
                try
                {
                    ShowBusy("Saving data...");
                    var result = await HrisEmployeeService.SetStatusAsync(Current.Id, dialog.SelectedStatus, dialog.EndDate);
                    if (result.HasErrors)
                    {
                        Helpers.ShowDialogMessage("error", result.ErrorMessage);
                        return;
                    }
                    Helpers.ShowDialogMessage("success", "Employment status updated.");
                    await LoadData(Current.Id);
                }
                catch (Exception ex)
                {
                    Helpers.ShowDialogMessage("error", $"Failed to update status: {ex.Message}");
                }
                finally
                {
                    HideBusy();
                }
            }
        }

        private async void btn_link_Click(object sender, EventArgs e)
        {
            if (Current == null)
            {
                Helpers.ShowDialogMessage("error", "No employee loaded. Use Search or New.");
                return;
            }
            using (var dialog = new LinkUserDialog())
            {
                if (dialog.ShowDialog(FindForm()) != DialogResult.OK) return;
                try
                {
                    ShowBusy("Saving data...");
                    var result = await HrisEmployeeService.LinkUserAsync(Current.Id, dialog.SelectedUserId);
                    if (result.HasErrors)
                    {
                        Helpers.ShowDialogMessage("error", result.ErrorMessage);
                        return;
                    }
                    Helpers.ShowDialogMessage("success", dialog.SelectedUserId.HasValue ? "ERP user linked." : "ERP user unlinked.");
                    await LoadData(Current.Id);
                }
                catch (Exception ex)
                {
                    Helpers.ShowDialogMessage("error", $"Failed to link user: {ex.Message}");
                }
                finally
                {
                    HideBusy();
                }
            }
        }

        // ------------------------------------------------------------ files

        private async void btn_uploadFile_Click(object sender, EventArgs e)
        {
            if (Current == null) return;
            using (var picker = new OpenFileDialog { Title = "Select employee document" })
            {
                if (picker.ShowDialog(FindForm()) != DialogResult.OK) return;
                try
                {
                    ShowBusy("Uploading file...");
                    var uploaded = await HrisApiService.UploadFileAsync(picker.FileName);
                    var result = await HrisEmployeeService.AddFileAsync(Current.Id, uploaded, cmb_fileCategory.Text);
                    if (result.HasErrors)
                    {
                        Helpers.ShowDialogMessage("error", result.ErrorMessage);
                        return;
                    }
                    Helpers.ShowDialogMessage("success", "File uploaded.");
                    await LoadData(Current.Id);
                }
                catch (Exception ex)
                {
                    Helpers.ShowDialogMessage("error", $"Failed to upload: {ex.Message}");
                }
                finally
                {
                    HideBusy();
                }
            }
        }

        private async void btn_deleteFile_Click(object sender, EventArgs e)
        {
            if (Current == null || dgv_files.SelectedRows.Count == 0) return;
            var row = dgv_files.SelectedRows[0];
            int fileId = Convert.ToInt32(row.Cells["id"].Value);

            var confirm = MessageBox.Show("Remove this file from the employee's 201 file? (It is retained for audit.)",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;
            try
            {
                ShowBusy("Deleting file...");
                var result = await HrisEmployeeService.DeleteFileAsync(fileId);
                if (result.HasErrors)
                {
                    Helpers.ShowDialogMessage("error", result.ErrorMessage);
                    return;
                }
                Helpers.ShowDialogMessage("success", "File deleted.");
                await LoadData(Current.Id);
            }
            catch (Exception ex)
            {
                Helpers.ShowDialogMessage("error", $"Failed to delete: {ex.Message}");
            }
            finally
            {
                HideBusy();
            }
        }

        // ------------------------------------------------------------ dialogs

        // Popup employee search - filters across FIRST / MIDDLE / LAST name
        // (and employee no) as you type. Double-click or Select loads the record.
        private class SearchEmployeeDialog : Form
        {
            private readonly TextBox _search = new TextBox { Width = 300 };
            private readonly DataGridView _grid;
            private readonly List<HrisEmployeeModel> _all;

            public int? SelectedEmployeeId { get; private set; }

            public SearchEmployeeDialog(List<HrisEmployeeModel> employees)
            {
                _all = employees;

                Text = "Employee Search";
                FormBorderStyle = FormBorderStyle.FixedDialog;
                StartPosition = FormStartPosition.CenterParent;
                MaximizeBox = false;
                MinimizeBox = false;
                ClientSize = new System.Drawing.Size(640, 430);

                _grid = new DataGridView
                {
                    Location = new System.Drawing.Point(15, 50),
                    Size = new System.Drawing.Size(610, 320),
                    ReadOnly = true,
                    AllowUserToAddRows = false,
                    RowHeadersVisible = false,
                    SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                    MultiSelect = false,
                    AutoGenerateColumns = false
                };
                _grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "id", Visible = false });
                _grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "employeeNo", HeaderText = "EMPLOYEE NO", Width = 130 });
                _grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "firstName", HeaderText = "FIRST NAME", Width = 150 });
                _grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "middleName", HeaderText = "MIDDLE NAME", Width = 140 });
                _grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "lastName", HeaderText = "LAST NAME", Width = 150 });
                _grid.CellDoubleClick += (s, e) => { if (e.RowIndex >= 0) Choose(); };

                var searchLabel = new Label { Text = "SEARCH", Location = new System.Drawing.Point(15, 20), AutoSize = true };
                _search.Location = new System.Drawing.Point(80, 17);
                _search.TextChanged += (s, e) => Refill();

                var select = new Button { Text = "Select", Width = 90, Location = new System.Drawing.Point(430, 385) };
                select.Click += (s, e) => Choose();
                var cancel = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel, Width = 90, Location = new System.Drawing.Point(535, 385) };

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
                SelectedEmployeeId = Convert.ToInt32(_grid.SelectedRows[0].Cells["id"].Value);
                DialogResult = DialogResult.OK;
            }

            private void Refill()
            {
                string term = _search.Text.Trim();
                var matches = string.IsNullOrEmpty(term)
                    ? _all
                    : _all.Where(x =>
                        Has(x.FirstName, term) || Has(x.MiddleName, term) || Has(x.LastName, term)
                        || Has(x.EmployeeNo, term)).ToList();

                _grid.Rows.Clear();
                foreach (var emp in matches)
                {
                    _grid.Rows.Add(emp.Id, emp.EmployeeNo, emp.FirstName, emp.MiddleName, emp.LastName);
                }
            }

            private static bool Has(string value, string term)
            {
                return value != null && value.IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0;
            }
        }

        private class StatusDialog : Form
        {
            private readonly ComboBox _status = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 220 };
            private readonly TextBox _endDate = new TextBox { Width = 220 };

            public string SelectedStatus => _status.Text;
            public string EndDate => _endDate.Text.Trim();

            public StatusDialog(string current)
            {
                Text = "Set Employment Status";
                FormBorderStyle = FormBorderStyle.FixedDialog;
                StartPosition = FormStartPosition.CenterParent;
                MaximizeBox = false;
                MinimizeBox = false;
                ClientSize = new System.Drawing.Size(370, 140);

                _status.Items.AddRange(EmploymentStatuses);
                _status.Text = current;

                var ok = new Button { Text = "Save", DialogResult = DialogResult.OK, Location = new System.Drawing.Point(180, 100) };
                var cancel = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel, Location = new System.Drawing.Point(270, 100) };
                Controls.Add(new Label { Text = "STATUS", Location = new System.Drawing.Point(20, 23), AutoSize = true });
                _status.Location = new System.Drawing.Point(130, 20);
                Controls.Add(_status);
                Controls.Add(new Label { Text = "END DATE", Location = new System.Drawing.Point(20, 58), AutoSize = true });
                _endDate.Location = new System.Drawing.Point(130, 55);
                Controls.Add(new Label { Text = "(YYYY-MM-DD, optional)", Location = new System.Drawing.Point(130, 78), AutoSize = true });
                Controls.Add(_endDate);
                Controls.Add(ok);
                Controls.Add(cancel);
                AcceptButton = ok;
                CancelButton = cancel;
            }
        }

        private class LinkUserDialog : Form
        {
            private readonly TextBox _search = new TextBox { Width = 250 };
            private readonly DataGridView _grid;
            private List<HrisErpUserModel> _users = new List<HrisErpUserModel>();

            // null = unlink; set when a row is chosen.
            public int? SelectedUserId { get; private set; }

            public LinkUserDialog()
            {
                Text = "Link ERP User Account";
                FormBorderStyle = FormBorderStyle.FixedDialog;
                StartPosition = FormStartPosition.CenterParent;
                MaximizeBox = false;
                MinimizeBox = false;
                ClientSize = new System.Drawing.Size(520, 400);

                _grid = new DataGridView
                {
                    Location = new System.Drawing.Point(15, 50),
                    Size = new System.Drawing.Size(490, 290),
                    ReadOnly = true,
                    AllowUserToAddRows = false,
                    RowHeadersVisible = false,
                    SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                    MultiSelect = false,
                    AutoGenerateColumns = false
                };
                _grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "id", Visible = false });
                _grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "employeeId", HeaderText = "EMPLOYEE ID", Width = 140 });
                _grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "name", HeaderText = "NAME", Width = 200 });
                _grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "department", HeaderText = "DEPARTMENT", Width = 130 });

                var searchLabel = new Label { Text = "SEARCH", Location = new System.Drawing.Point(15, 20), AutoSize = true };
                _search.Location = new System.Drawing.Point(80, 17);
                _search.TextChanged += async (s, e) => await LoadUsers(_search.Text.Trim());

                var link = new Button { Text = "Link Selected", Width = 100, Location = new System.Drawing.Point(190, 355) };
                link.Click += (s, e) =>
                {
                    if (_grid.SelectedRows.Count == 0) return;
                    SelectedUserId = Convert.ToInt32(_grid.SelectedRows[0].Cells["id"].Value);
                    DialogResult = DialogResult.OK;
                };
                var unlink = new Button { Text = "Unlink", Width = 80, Location = new System.Drawing.Point(300, 355) };
                unlink.Click += (s, e) => { SelectedUserId = null; DialogResult = DialogResult.OK; };
                var cancel = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel, Width = 80, Location = new System.Drawing.Point(390, 355) };

                Controls.Add(searchLabel);
                Controls.Add(_search);
                Controls.Add(_grid);
                Controls.Add(link);
                Controls.Add(unlink);
                Controls.Add(cancel);
                CancelButton = cancel;

                Load += async (s, e) => await LoadUsers(null);
            }

            private async System.Threading.Tasks.Task LoadUsers(string search)
            {
                try
                {
                    var result = await HrisEmployeeService.GetErpUsersAsync(search);
                    if (result.HasErrors) return;
                    _users = result.Data.ErpUsers ?? new List<HrisErpUserModel>();
                    _grid.Rows.Clear();
                    foreach (var u in _users)
                    {
                        _grid.Rows.Add(u.Id, u.EmployeeId, (u.FirstName + " " + u.LastName).Trim(), u.Department);
                    }
                }
                catch
                {
                    // ignore transient search errors while typing
                }
            }
        }
    }
}
