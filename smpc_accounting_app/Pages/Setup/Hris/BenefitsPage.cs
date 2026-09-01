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
    // HRIS Benefits Administration - the plan catalog (HMO, insurance,
    // allowances, loans) HR maintains, and each employee's enrollment in
    // them. An enrolled employee/employer share is picked up automatically
    // every payroll cutoff (see PayrollItem.benefitsEe/benefitsEr on the
    // Payroll page) - there is nothing to enter on the Payroll side. A LOAN
    // enrollment's balanceRemaining is system-managed: it only moves when
    // the payroll run carrying its deduction is actually Approved (reverses
    // on Reopen), so it is read-only here.
    //
    // Two independent CRUD surfaces sharing one tab strip. Both build their
    // toolstrip/form/grid in code (BuildPlansTab / BuildEnrollmentsTab) -
    // Designer.cs only carries the page header.
    //
    // Constructor stays I/O-free (RoutesService instantiates pages eagerly).
    public partial class BenefitsPage : UserControl
    {
        private static readonly string[] Categories = { "HMO", "INSURANCE", "ALLOWANCE", "LOAN", "OTHER" };
        private static readonly string[] EnrollmentStatusFilters = { "ACTIVE", "ENDED", "ALL" };

        private bool _loaded;

        // ── Plans tab ────────────────────────────────────────────────────
        private List<HrisBenefitPlanModel> _plans = new List<HrisBenefitPlanModel>();
        private bool _planEditMode, _planIsNew;
        private DataGridView dgv_plans;
        private ToolStripButton btn_plan_new, btn_plan_edit, btn_plan_delete, btn_plan_save, btn_plan_cancel, btn_plan_load;
        private TextBox txt_planName, txt_planDescription, txt_planEmployerShare, txt_planEmployeeShare;
        private ComboBox cmb_planCategory;
        private CheckBox chk_planActive;

        private HrisBenefitPlanModel SelectedPlan
        {
            get
            {
                if (dgv_plans.SelectedRows.Count == 0) return null;
                var idValue = dgv_plans.SelectedRows[0].Cells["col_plan_id"].Value;
                if (idValue == null) return null;
                return _plans.FirstOrDefault(x => x.Id == Convert.ToInt32(idValue));
            }
        }

        // ── Enrollments tab ──────────────────────────────────────────────
        private List<HrisEmployeeModel> _employees = new List<HrisEmployeeModel>();
        private List<HrisBenefitPlanModel> _activePlans = new List<HrisBenefitPlanModel>();
        private List<HrisBenefitEnrollmentModel> _enrollments = new List<HrisBenefitEnrollmentModel>();
        private bool _enrollEditMode, _enrollIsNew;
        private DataGridView dgv_enrollments;
        private ToolStripComboBox cmb_enrollStatusFilter;
        private ToolStripButton btn_enroll_load, btn_enroll_new, btn_enroll_edit, btn_enroll_end, btn_enroll_save, btn_enroll_cancel;
        private ComboBox cmb_enrollEmployee, cmb_enrollPlan;
        private TextBox txt_enrollEffective, txt_enrollEnd, txt_enrollEmployerShare, txt_enrollEmployeeShare,
            txt_enrollPrincipal, txt_enrollBalance, txt_enrollStatus, txt_enrollNotes;

        private HrisBenefitEnrollmentModel SelectedEnrollment
        {
            get
            {
                if (dgv_enrollments.SelectedRows.Count == 0) return null;
                var idValue = dgv_enrollments.SelectedRows[0].Cells["col_enr_id"].Value;
                if (idValue == null) return null;
                return _enrollments.FirstOrDefault(x => x.Id == Convert.ToInt32(idValue));
            }
        }

        public BenefitsPage()
        {
            InitializeComponent();

            var tabs = new TabControl { Dock = DockStyle.Fill };
            var tabPlans = new TabPage("Benefit Plans");
            var tabEnrollments = new TabPage("Enrollments");
            tabs.TabPages.Add(tabPlans);
            tabs.TabPages.Add(tabEnrollments);
            pnl_body.Controls.Add(tabs);

            BuildPlansTab(tabPlans);
            BuildEnrollmentsTab(tabEnrollments);

            SetPlanEditMode(false);
            SetEnrollEditMode(false);
        }

        // ================================================================
        // Plans tab
        // ================================================================

        private void BuildPlansTab(TabPage tab)
        {
            var toolStrip = new ToolStrip { GripStyle = ToolStripGripStyle.Hidden };
            btn_plan_load = new ToolStripButton("Load");
            btn_plan_load.Click += async (s, e) => await LoadPlans();
            btn_plan_new = new ToolStripButton("New");
            btn_plan_new.Click += btn_plan_new_Click;
            btn_plan_edit = new ToolStripButton("Edit");
            btn_plan_edit.Click += btn_plan_edit_Click;
            btn_plan_delete = new ToolStripButton("Delete");
            btn_plan_delete.Click += btn_plan_delete_Click;
            btn_plan_save = new ToolStripButton("Save");
            btn_plan_save.Click += btn_plan_save_Click;
            btn_plan_cancel = new ToolStripButton("Cancel");
            btn_plan_cancel.Click += (s, e) => { SetPlanEditMode(false); BindSelectedPlan(); };
            toolStrip.Items.AddRange(new ToolStripItem[] {
                btn_plan_load, new ToolStripSeparator(),
                btn_plan_new, btn_plan_edit, btn_plan_delete, btn_plan_save, btn_plan_cancel });
            tab.Controls.Add(toolStrip);

            var pnl_form = new Panel { Dock = DockStyle.Top, Height = 190 };
            var t = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, Padding = new Padding(10) };
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

            txt_planName = AddText("PLAN NAME *");
            cmb_planCategory = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
            cmb_planCategory.Items.AddRange(Categories);
            Place("CATEGORY *", cmb_planCategory);
            txt_planDescription = AddText("DESCRIPTION");
            txt_planEmployerShare = AddText("DEFAULT EMPLOYER SHARE (monthly)");
            txt_planEmployeeShare = AddText("DEFAULT EMPLOYEE SHARE (monthly)");
            chk_planActive = new CheckBox { AutoSize = true };
            Place("ACTIVE (gates NEW enrollments only)", chk_planActive);

            pnl_form.Controls.Add(t);
            tab.Controls.Add(pnl_form);

            var pnl_grid = new Panel { Dock = DockStyle.Fill, Padding = new Padding(6) };
            dgv_plans = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                AutoGenerateColumns = false,
                BackgroundColor = System.Drawing.SystemColors.Window,
                ReadOnly = true,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
            };
            dgv_plans.Columns.Add(new DataGridViewTextBoxColumn { Name = "col_plan_id", Visible = false });
            dgv_plans.Columns.Add(new DataGridViewTextBoxColumn { Name = "col_plan_name", HeaderText = "NAME", Width = 220 });
            dgv_plans.Columns.Add(new DataGridViewTextBoxColumn { Name = "col_plan_category", HeaderText = "CATEGORY", Width = 100 });
            dgv_plans.Columns.Add(new DataGridViewTextBoxColumn { Name = "col_plan_er", HeaderText = "EMPLOYER SHARE", Width = 120 });
            dgv_plans.Columns.Add(new DataGridViewTextBoxColumn { Name = "col_plan_ee", HeaderText = "EMPLOYEE SHARE", Width = 120 });
            dgv_plans.Columns.Add(new DataGridViewTextBoxColumn { Name = "col_plan_active", HeaderText = "ACTIVE", Width = 70 });
            dgv_plans.Columns.Add(new DataGridViewTextBoxColumn { Name = "col_plan_desc", HeaderText = "DESCRIPTION", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgv_plans.SelectionChanged += (s, e) => { if (!_planEditMode) BindSelectedPlan(); };
            pnl_grid.Controls.Add(dgv_plans);
            tab.Controls.Add(pnl_grid);

            // Dock order: last-added is on top when using Fill+Top together,
            // so add Fill first, then Top panels, then the toolstrip last.
            tab.Controls.SetChildIndex(pnl_grid, 0);
            tab.Controls.SetChildIndex(pnl_form, 1);
            tab.Controls.SetChildIndex(toolStrip, 2);
        }

        private async System.Threading.Tasks.Task LoadPlans()
        {
            try
            {
                Helpers.Loading.ShowLoading(dgv_plans, "Fetching data...");
                var result = await HrisBenefitService.GetPlansAsync();
                if (result.HasErrors)
                {
                    Helpers.ShowDialogMessage("error", result.ErrorMessage);
                    return;
                }
                _plans = result.Data.BenefitPlans ?? new List<HrisBenefitPlanModel>();
                _activePlans = _plans.Where(p => p.IsActive).ToList();
                dgv_plans.Rows.Clear();
                foreach (var p in _plans)
                {
                    dgv_plans.Rows.Add(p.Id, p.Name, p.Category, p.DefaultEmployerShare, p.DefaultEmployeeShare,
                        p.IsActive ? "YES" : "NO", p.Description);
                }
                BindSelectedPlan();
                RefreshEnrollPlanCombo();
            }
            catch (Exception ex)
            {
                Helpers.ShowDialogMessage("error", $"Failed to load: {ex.Message}");
            }
            finally
            {
                Helpers.Loading.HideLoading(dgv_plans);
            }
        }

        private void BindSelectedPlan()
        {
            var plan = SelectedPlan;
            txt_planName.Text = plan?.Name ?? "";
            cmb_planCategory.Text = plan?.Category ?? "";
            txt_planDescription.Text = plan?.Description ?? "";
            txt_planEmployerShare.Text = plan != null ? plan.DefaultEmployerShare.ToString(CultureInfo.InvariantCulture) : "";
            txt_planEmployeeShare.Text = plan != null ? plan.DefaultEmployeeShare.ToString(CultureInfo.InvariantCulture) : "";
            chk_planActive.Checked = plan?.IsActive ?? false;
        }

        private void SetPlanEditMode(bool enable, bool isNew = false)
        {
            _planEditMode = enable;
            _planIsNew = isNew;
            btn_plan_save.Visible = enable;
            btn_plan_cancel.Visible = enable;
            btn_plan_new.Visible = !enable;
            btn_plan_edit.Visible = !enable;
            btn_plan_delete.Visible = !enable;
            txt_planName.ReadOnly = !enable;
            txt_planDescription.ReadOnly = !enable;
            txt_planEmployerShare.ReadOnly = !enable;
            txt_planEmployeeShare.ReadOnly = !enable;
            cmb_planCategory.Enabled = enable;
            chk_planActive.Enabled = enable;
            dgv_plans.Enabled = !enable;
        }

        private void btn_plan_new_Click(object sender, EventArgs e)
        {
            txt_planName.Text = "";
            cmb_planCategory.Text = "HMO";
            txt_planDescription.Text = "";
            txt_planEmployerShare.Text = "0";
            txt_planEmployeeShare.Text = "0";
            chk_planActive.Checked = true;
            SetPlanEditMode(true, isNew: true);
        }

        private void btn_plan_edit_Click(object sender, EventArgs e)
        {
            if (SelectedPlan == null)
            {
                Helpers.ShowDialogMessage("error", "Please select a benefit plan to edit.");
                return;
            }
            SetPlanEditMode(true);
        }

        private async void btn_plan_save_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_planName.Text) || string.IsNullOrWhiteSpace(cmb_planCategory.Text))
            {
                Helpers.ShowDialogMessage("error", "Name and category are required.");
                return;
            }
            if (!decimal.TryParse(txt_planEmployerShare.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal er)) er = 0;
            if (!decimal.TryParse(txt_planEmployeeShare.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal ee)) ee = 0;
            var input = new Dictionary<string, object>
            {
                { "name", txt_planName.Text.Trim() },
                { "category", cmb_planCategory.Text },
                { "description", txt_planDescription.Text.Trim() },
                { "defaultEmployerShare", er },
                { "defaultEmployeeShare", ee },
                { "isActive", chk_planActive.Checked },
            };
            try
            {
                Helpers.Loading.ShowLoading(dgv_plans, "Saving data...");
                string error = null;
                if (_planIsNew)
                {
                    var result = await HrisBenefitService.CreatePlanAsync(input);
                    if (result.HasErrors) error = result.ErrorMessage;
                }
                else
                {
                    var result = await HrisBenefitService.UpdatePlanAsync(SelectedPlan.Id, input);
                    if (result.HasErrors) error = result.ErrorMessage;
                }
                if (error != null)
                {
                    Helpers.ShowDialogMessage("error", error);
                    return;
                }
                Helpers.ShowDialogMessage("success", _planIsNew ? "Benefit plan added." : "Benefit plan updated.");
                SetPlanEditMode(false);
                await LoadPlans();
            }
            catch (Exception ex)
            {
                Helpers.ShowDialogMessage("error", $"Failed to save: {ex.Message}");
            }
            finally
            {
                Helpers.Loading.HideLoading(dgv_plans);
            }
        }

        private async void btn_plan_delete_Click(object sender, EventArgs e)
        {
            if (SelectedPlan == null)
            {
                Helpers.ShowDialogMessage("error", "Please select a benefit plan to delete.");
                return;
            }
            var confirm = MessageBox.Show(
                $"Delete \"{SelectedPlan.Name}\"? This only works if no employee has ever been enrolled in it - otherwise deactivate it instead.",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;
            try
            {
                Helpers.Loading.ShowLoading(dgv_plans, "Deleting...");
                var result = await HrisBenefitService.DeletePlanAsync(SelectedPlan.Id);
                if (result.HasErrors)
                {
                    Helpers.ShowDialogMessage("error", result.ErrorMessage);
                    return;
                }
                Helpers.ShowDialogMessage("success", "Benefit plan deleted.");
                await LoadPlans();
            }
            catch (Exception ex)
            {
                Helpers.ShowDialogMessage("error", $"Failed to delete: {ex.Message}");
            }
            finally
            {
                Helpers.Loading.HideLoading(dgv_plans);
            }
        }

        // ================================================================
        // Enrollments tab
        // ================================================================

        private void BuildEnrollmentsTab(TabPage tab)
        {
            var toolStrip = new ToolStrip { GripStyle = ToolStripGripStyle.Hidden };
            var lbl = new ToolStripLabel("STATUS");
            cmb_enrollStatusFilter = new ToolStripComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
            cmb_enrollStatusFilter.Items.AddRange(EnrollmentStatusFilters);
            cmb_enrollStatusFilter.SelectedIndex = 0; // ACTIVE
            btn_enroll_load = new ToolStripButton("Load");
            btn_enroll_load.Click += async (s, e) => await LoadEnrollments();
            btn_enroll_new = new ToolStripButton("New");
            btn_enroll_new.Click += btn_enroll_new_Click;
            btn_enroll_edit = new ToolStripButton("Edit");
            btn_enroll_edit.Click += btn_enroll_edit_Click;
            btn_enroll_end = new ToolStripButton("End");
            btn_enroll_end.Click += btn_enroll_end_Click;
            btn_enroll_save = new ToolStripButton("Save");
            btn_enroll_save.Click += btn_enroll_save_Click;
            btn_enroll_cancel = new ToolStripButton("Cancel");
            btn_enroll_cancel.Click += (s, e) => { SetEnrollEditMode(false); BindSelectedEnrollment(); };
            toolStrip.Items.AddRange(new ToolStripItem[] {
                lbl, cmb_enrollStatusFilter, btn_enroll_load, new ToolStripSeparator(),
                btn_enroll_new, btn_enroll_edit, btn_enroll_end, btn_enroll_save, btn_enroll_cancel });
            tab.Controls.Add(toolStrip);

            var pnl_form = new Panel { Dock = DockStyle.Top, Height = 280 };
            var t = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, Padding = new Padding(10) };
            t.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 190F));
            t.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 430F));

            void Place(string label, Control control)
            {
                t.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                t.Controls.Add(new Label { Text = label, AutoSize = true, Anchor = AnchorStyles.Left, Padding = new Padding(0, 4, 0, 0) });
                control.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                t.Controls.Add(control);
            }
            TextBox AddText(string label, bool readOnly = false)
            {
                var txt = new TextBox { BackColor = System.Drawing.Color.Gainsboro, ReadOnly = readOnly };
                Place(label, txt);
                return txt;
            }

            cmb_enrollEmployee = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
            Place("EMPLOYEE * (fixed after creation)", cmb_enrollEmployee);
            cmb_enrollPlan = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
            cmb_enrollPlan.SelectedIndexChanged += (s, e) => UpdatePrincipalRequirement();
            Place("BENEFIT PLAN * (fixed after creation)", cmb_enrollPlan);
            txt_enrollStatus = AddText("STATUS", readOnly: true);
            txt_enrollEffective = AddText("EFFECTIVE DATE (YYYY-MM-DD) *");
            txt_enrollEnd = AddText("END DATE (YYYY-MM-DD, blank = ongoing)");
            txt_enrollEmployerShare = AddText("EMPLOYER SHARE (monthly)");
            txt_enrollEmployeeShare = AddText("EMPLOYEE SHARE (monthly)");
            txt_enrollPrincipal = AddText("PRINCIPAL AMOUNT (LOAN only, at creation)");
            txt_enrollBalance = AddText("BALANCE REMAINING (system-managed)", readOnly: true);
            txt_enrollNotes = AddText("NOTES");

            pnl_form.Controls.Add(t);
            tab.Controls.Add(pnl_form);

            var pnl_grid = new Panel { Dock = DockStyle.Fill, Padding = new Padding(6) };
            dgv_enrollments = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                AutoGenerateColumns = false,
                BackgroundColor = System.Drawing.SystemColors.Window,
                ReadOnly = true,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
            };
            dgv_enrollments.Columns.Add(new DataGridViewTextBoxColumn { Name = "col_enr_id", Visible = false });
            dgv_enrollments.Columns.Add(new DataGridViewTextBoxColumn { Name = "col_enr_employee", HeaderText = "EMPLOYEE", Width = 200 });
            dgv_enrollments.Columns.Add(new DataGridViewTextBoxColumn { Name = "col_enr_plan", HeaderText = "PLAN", Width = 150 });
            dgv_enrollments.Columns.Add(new DataGridViewTextBoxColumn { Name = "col_enr_category", HeaderText = "CATEGORY", Width = 90 });
            dgv_enrollments.Columns.Add(new DataGridViewTextBoxColumn { Name = "col_enr_status", HeaderText = "STATUS", Width = 80 });
            dgv_enrollments.Columns.Add(new DataGridViewTextBoxColumn { Name = "col_enr_effective", HeaderText = "EFFECTIVE", Width = 90 });
            dgv_enrollments.Columns.Add(new DataGridViewTextBoxColumn { Name = "col_enr_end", HeaderText = "END", Width = 90 });
            dgv_enrollments.Columns.Add(new DataGridViewTextBoxColumn { Name = "col_enr_ee", HeaderText = "EE SHARE/CUT", Width = 100 });
            dgv_enrollments.Columns.Add(new DataGridViewTextBoxColumn { Name = "col_enr_balance", HeaderText = "BALANCE", Width = 100 });
            dgv_enrollments.SelectionChanged += (s, e) => { if (!_enrollEditMode) BindSelectedEnrollment(); };
            pnl_grid.Controls.Add(dgv_enrollments);
            tab.Controls.Add(pnl_grid);

            tab.Controls.SetChildIndex(pnl_grid, 0);
            tab.Controls.SetChildIndex(pnl_form, 1);
            tab.Controls.SetChildIndex(toolStrip, 2);
        }

        private async void BenefitsPage_Load(object sender, EventArgs e)
        {
            if (_loaded) return;
            _loaded = true;
            try
            {
                Helpers.Loading.ShowLoading(dgv_plans, "Fetching data...");
                var employees = await HrisEmployeeService.GetEmployeesAsync();
                if (employees.HasErrors)
                {
                    Helpers.ShowDialogMessage("error", employees.ErrorMessage);
                    return;
                }
                _employees = employees.Data.Employees?.Items ?? new List<HrisEmployeeModel>();
                cmb_enrollEmployee.Items.Clear();
                foreach (var emp in _employees) cmb_enrollEmployee.Items.Add(EmployeeLabel(emp));

                await LoadPlans();
                await LoadEnrollments();
            }
            catch (Exception ex)
            {
                Helpers.ShowDialogMessage("error", $"Failed to load: {ex.Message}");
            }
            finally
            {
                Helpers.Loading.HideLoading(dgv_plans);
            }
        }

        private static string EmployeeLabel(HrisEmployeeModel emp) => $"{emp.EmployeeNo} — {emp.FirstName} {emp.LastName}";

        private void RefreshEnrollPlanCombo()
        {
            cmb_enrollPlan.Items.Clear();
            foreach (var p in _activePlans) cmb_enrollPlan.Items.Add($"{p.Name} ({p.Category})");
        }

        private async System.Threading.Tasks.Task LoadEnrollments()
        {
            try
            {
                Helpers.Loading.ShowLoading(dgv_enrollments, "Fetching data...");
                var result = await HrisBenefitService.GetEnrollmentsAsync(status: cmb_enrollStatusFilter.Text);
                if (result.HasErrors)
                {
                    Helpers.ShowDialogMessage("error", result.ErrorMessage);
                    return;
                }
                _enrollments = result.Data.BenefitEnrollments ?? new List<HrisBenefitEnrollmentModel>();
                dgv_enrollments.Rows.Clear();
                foreach (var en in _enrollments)
                {
                    string employee = en.Employee != null ? EmployeeLabel(en.Employee) : en.EmployeeId.ToString();
                    dgv_enrollments.Rows.Add(en.Id, employee, en.Plan?.Name, en.Plan?.Category, en.Status,
                        en.EffectiveDate, en.EndDate, en.EmployeeShare, en.Plan?.Category == "LOAN" ? (object)en.BalanceRemaining : "—");
                }
                BindSelectedEnrollment();
            }
            catch (Exception ex)
            {
                Helpers.ShowDialogMessage("error", $"Failed to load: {ex.Message}");
            }
            finally
            {
                Helpers.Loading.HideLoading(dgv_enrollments);
            }
        }

        private void BindSelectedEnrollment()
        {
            var en = SelectedEnrollment;
            cmb_enrollEmployee.Text = en?.Employee != null ? EmployeeLabel(en.Employee) : "";
            cmb_enrollPlan.Text = en?.Plan != null ? $"{en.Plan.Name} ({en.Plan.Category})" : "";
            txt_enrollStatus.Text = en?.Status ?? "";
            txt_enrollEffective.Text = en?.EffectiveDate ?? "";
            txt_enrollEnd.Text = en?.EndDate ?? "";
            txt_enrollEmployerShare.Text = en != null ? en.EmployerShare.ToString(CultureInfo.InvariantCulture) : "";
            txt_enrollEmployeeShare.Text = en != null ? en.EmployeeShare.ToString(CultureInfo.InvariantCulture) : "";
            txt_enrollPrincipal.Text = en != null ? en.PrincipalAmount.ToString(CultureInfo.InvariantCulture) : "";
            txt_enrollBalance.Text = en != null && en.Plan?.Category == "LOAN" ? en.BalanceRemaining.ToString(CultureInfo.InvariantCulture) : "—";
        }

        private void UpdatePrincipalRequirement()
        {
            bool isLoan = cmb_enrollPlan.SelectedIndex >= 0 && cmb_enrollPlan.SelectedIndex < _activePlans.Count
                && _activePlans[cmb_enrollPlan.SelectedIndex].Category == "LOAN";
            txt_enrollPrincipal.BackColor = isLoan ? System.Drawing.Color.LightYellow : System.Drawing.Color.Gainsboro;
        }

        private void SetEnrollEditMode(bool enable, bool isNew = false)
        {
            _enrollEditMode = enable;
            _enrollIsNew = isNew;
            btn_enroll_save.Visible = enable;
            btn_enroll_cancel.Visible = enable;
            btn_enroll_new.Visible = !enable;
            btn_enroll_edit.Visible = !enable;
            btn_enroll_end.Visible = !enable;
            // Employee/plan/principal are fixed once created - only editable on New.
            cmb_enrollEmployee.Enabled = enable && isNew;
            cmb_enrollPlan.Enabled = enable && isNew;
            txt_enrollPrincipal.ReadOnly = !(enable && isNew);
            txt_enrollEffective.ReadOnly = !enable;
            txt_enrollEnd.ReadOnly = !enable;
            txt_enrollEmployerShare.ReadOnly = !enable;
            txt_enrollEmployeeShare.ReadOnly = !enable;
            txt_enrollNotes.ReadOnly = !enable;
            dgv_enrollments.Enabled = !enable;
        }

        private void btn_enroll_new_Click(object sender, EventArgs e)
        {
            if (_activePlans.Count == 0)
            {
                Helpers.ShowDialogMessage("error", "Add an active benefit plan first.");
                return;
            }
            cmb_enrollEmployee.SelectedIndex = -1;
            cmb_enrollPlan.SelectedIndex = -1;
            txt_enrollStatus.Text = "ACTIVE";
            txt_enrollEffective.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txt_enrollEnd.Text = "";
            txt_enrollEmployerShare.Text = "0";
            txt_enrollEmployeeShare.Text = "0";
            txt_enrollPrincipal.Text = "0";
            txt_enrollBalance.Text = "—";
            txt_enrollNotes.Text = "";
            SetEnrollEditMode(true, isNew: true);
            UpdatePrincipalRequirement();
        }

        private void btn_enroll_edit_Click(object sender, EventArgs e)
        {
            if (SelectedEnrollment == null)
            {
                Helpers.ShowDialogMessage("error", "Please select an enrollment to edit.");
                return;
            }
            SetEnrollEditMode(true);
        }

        private async void btn_enroll_save_Click(object sender, EventArgs e)
        {
            if (cmb_enrollEmployee.SelectedIndex < 0 && _enrollIsNew)
            {
                Helpers.ShowDialogMessage("error", "Please select an employee.");
                return;
            }
            if (cmb_enrollPlan.SelectedIndex < 0 && _enrollIsNew)
            {
                Helpers.ShowDialogMessage("error", "Please select a benefit plan.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txt_enrollEffective.Text))
            {
                Helpers.ShowDialogMessage("error", "Effective date is required (YYYY-MM-DD).");
                return;
            }
            decimal.TryParse(txt_enrollEmployerShare.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal er);
            decimal.TryParse(txt_enrollEmployeeShare.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal ee);
            decimal.TryParse(txt_enrollPrincipal.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal principal);

            int employeeId = _enrollIsNew ? _employees[cmb_enrollEmployee.SelectedIndex].Id : SelectedEnrollment.EmployeeId;
            int planId = _enrollIsNew ? _activePlans[cmb_enrollPlan.SelectedIndex].Id : SelectedEnrollment.BenefitPlanId;
            string planCategory = _enrollIsNew ? _activePlans[cmb_enrollPlan.SelectedIndex].Category : SelectedEnrollment.Plan?.Category;

            if (_enrollIsNew && planCategory == "LOAN" && principal <= 0)
            {
                Helpers.ShowDialogMessage("error", "A loan enrollment needs a principal amount greater than zero.");
                return;
            }

            var input = new Dictionary<string, object>
            {
                { "employeeId", employeeId },
                { "benefitPlanId", planId },
                { "effectiveDate", txt_enrollEffective.Text.Trim() },
                { "endDate", string.IsNullOrWhiteSpace(txt_enrollEnd.Text) ? null : txt_enrollEnd.Text.Trim() },
                { "employerShare", er },
                { "employeeShare", ee },
                { "notes", txt_enrollNotes.Text.Trim() },
            };
            if (_enrollIsNew) input["principalAmount"] = principal;

            try
            {
                Helpers.Loading.ShowLoading(dgv_enrollments, "Saving data...");
                string error = null;
                if (_enrollIsNew)
                {
                    var result = await HrisBenefitService.CreateEnrollmentAsync(input);
                    if (result.HasErrors) error = result.ErrorMessage;
                }
                else
                {
                    var result = await HrisBenefitService.UpdateEnrollmentAsync(SelectedEnrollment.Id, input);
                    if (result.HasErrors) error = result.ErrorMessage;
                }
                if (error != null)
                {
                    Helpers.ShowDialogMessage("error", error);
                    return;
                }
                Helpers.ShowDialogMessage("success", _enrollIsNew ? "Enrollment added." : "Enrollment updated.");
                SetEnrollEditMode(false);
                await LoadEnrollments();
            }
            catch (Exception ex)
            {
                Helpers.ShowDialogMessage("error", $"Failed to save: {ex.Message}");
            }
            finally
            {
                Helpers.Loading.HideLoading(dgv_enrollments);
            }
        }

        private async void btn_enroll_end_Click(object sender, EventArgs e)
        {
            if (SelectedEnrollment == null)
            {
                Helpers.ShowDialogMessage("error", "Please select an enrollment to end.");
                return;
            }
            if (SelectedEnrollment.Status == "ENDED")
            {
                Helpers.ShowDialogMessage("error", "This enrollment already ended.");
                return;
            }
            string balanceNote = SelectedEnrollment.Plan?.Category == "LOAN"
                ? $"\n\nRemaining loan balance ({SelectedEnrollment.BalanceRemaining:N2}) is NOT forgiven - this only stops further payroll deductions."
                : "";
            var confirm = MessageBox.Show(
                $"End {SelectedEnrollment.Employee?.FirstName} {SelectedEnrollment.Employee?.LastName}'s \"{SelectedEnrollment.Plan?.Name}\" enrollment?{balanceNote}",
                "Confirm End Enrollment", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;
            try
            {
                Helpers.Loading.ShowLoading(dgv_enrollments, "Saving data...");
                var result = await HrisBenefitService.EndEnrollmentAsync(SelectedEnrollment.Id, null);
                if (result.HasErrors)
                {
                    Helpers.ShowDialogMessage("error", result.ErrorMessage);
                    return;
                }
                Helpers.ShowDialogMessage("success", "Enrollment ended.");
                await LoadEnrollments();
            }
            catch (Exception ex)
            {
                Helpers.ShowDialogMessage("error", $"Failed: {ex.Message}");
            }
            finally
            {
                Helpers.Loading.HideLoading(dgv_enrollments);
            }
        }
    }
}
