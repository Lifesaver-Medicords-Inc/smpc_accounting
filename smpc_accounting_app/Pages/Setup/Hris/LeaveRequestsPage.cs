using smpc_accounting_app.Models.Hris;
using smpc_accounting_app.Services.Helpers;
using smpc_accounting_app.Services.Hris;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace smpc_accounting_app.Pages.Setup.Hris
{
    // HRIS Leave Requests - HR's decision queue for employee leave filed on
    // mobile (or desktop) self-service. Approving a request stamps
    // LEAVE/LEAVE_UNPAID onto the employee's timesheet automatically (an
    // already-APPROVED timesheet is left locked; the server reports that in
    // the decision note so HR knows to fix it by hand).
    //
    // Constructor stays I/O-free (RoutesService instantiates pages eagerly).
    public partial class LeaveRequestsPage : UserControl
    {
        private static readonly string[] StatusFilters = { "PENDING", "APPROVED", "REJECTED", "CANCELLED", "ALL" };

        private List<HrisLeaveRequestModel> _requests = new List<HrisLeaveRequestModel>();
        private bool _loaded;

        private TextBox txt_employee, txt_type, txt_dates, txt_reason, txt_decidedBy, txt_decidedAt;
        private TextBox txt_note;

        private HrisLeaveRequestModel Selected
        {
            get
            {
                if (dgv_list.SelectedRows.Count == 0) return null;
                var idValue = dgv_list.SelectedRows[0].Cells["col_id"].Value;
                if (idValue == null) return null;
                int id = Convert.ToInt32(idValue);
                return _requests.FirstOrDefault(x => x.Id == id);
            }
        }

        public LeaveRequestsPage()
        {
            InitializeComponent();
            cmb_statusFilter.Items.AddRange(StatusFilters);
            cmb_statusFilter.SelectedIndex = 0; // PENDING — the actionable queue
            BuildForm();
        }

        private void BuildForm()
        {
            var t = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                Padding = new Padding(10)
            };
            t.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            t.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 470F));

            TextBox AddReadOnly(string label)
            {
                var txt = new TextBox { BackColor = System.Drawing.Color.Gainsboro, ReadOnly = true };
                t.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                t.Controls.Add(new Label { Text = label, AutoSize = true, Anchor = AnchorStyles.Left, Padding = new Padding(0, 4, 0, 0) });
                txt.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                t.Controls.Add(txt);
                return txt;
            }

            txt_employee = AddReadOnly("EMPLOYEE");
            txt_type = AddReadOnly("LEAVE TYPE");
            txt_dates = AddReadOnly("DATES");
            txt_reason = AddReadOnly("REASON");
            txt_decidedBy = AddReadOnly("DECIDED BY");
            txt_decidedAt = AddReadOnly("DECIDED AT");

            txt_note = new TextBox();
            t.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            t.Controls.Add(new Label { Text = "DECISION NOTE", AutoSize = true, Anchor = AnchorStyles.Left, Padding = new Padding(0, 4, 0, 0) });
            txt_note.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            t.Controls.Add(txt_note);

            pnl_form.Controls.Add(t);
        }

        private async void LeaveRequestsPage_Load(object sender, EventArgs e)
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
                var result = await HrisLeaveService.GetLeaveRequestsAsync(cmb_statusFilter.Text);
                if (result.HasErrors)
                {
                    Helpers.ShowDialogMessage("error", result.ErrorMessage);
                    return;
                }
                _requests = result.Data.LeaveRequests ?? new List<HrisLeaveRequestModel>();
                dgv_list.Rows.Clear();
                foreach (var req in _requests)
                {
                    string employee = req.Employee != null
                        ? $"{req.Employee.EmployeeNo} — {req.Employee.FirstName} {req.Employee.LastName}"
                        : req.EmployeeId.ToString();
                    dgv_list.Rows.Add(req.Id, employee, req.LeaveType, req.DateFrom, req.DateTo, req.Status, req.DecidedBy);
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
            var req = Selected;
            if (req == null)
            {
                txt_employee.Text = txt_type.Text = txt_dates.Text = txt_reason.Text =
                    txt_decidedBy.Text = txt_decidedAt.Text = txt_note.Text = "";
                btn_approve.Enabled = btn_reject.Enabled = false;
                return;
            }
            txt_employee.Text = req.Employee != null ? $"{req.Employee.EmployeeNo} — {req.Employee.FirstName} {req.Employee.LastName}" : "";
            txt_type.Text = req.LeaveType;
            txt_dates.Text = $"{req.DateFrom} to {req.DateTo}";
            txt_reason.Text = req.Reason;
            txt_decidedBy.Text = req.DecidedBy;
            txt_decidedAt.Text = req.DecidedAt;
            txt_note.Text = req.Status == "PENDING" ? "" : req.DecisionNote;
            txt_note.ReadOnly = req.Status != "PENDING";

            bool pending = req.Status == "PENDING";
            btn_approve.Enabled = pending;
            btn_reject.Enabled = pending;
        }

        private async void btn_load_Click(object sender, EventArgs e)
        {
            await LoadData();
        }

        private async void btn_approve_Click(object sender, EventArgs e)
        {
            if (Selected == null) return;
            var confirm = MessageBox.Show(
                $"Approve {Selected.Employee?.FirstName} {Selected.Employee?.LastName}'s {Selected.LeaveType} leave, {Selected.DateFrom} to {Selected.DateTo}?\n\nThis stamps their timesheet(s) automatically.",
                "Confirm Approve", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;
            await Decide(HrisLeaveService.ApproveAsync(Selected.Id, txt_note.Text.Trim()), "Leave request approved.");
        }

        private async void btn_reject_Click(object sender, EventArgs e)
        {
            if (Selected == null) return;
            if (string.IsNullOrWhiteSpace(txt_note.Text))
            {
                Helpers.ShowDialogMessage("error", "Please give a reason in the decision note before rejecting.");
                return;
            }
            var confirm = MessageBox.Show(
                $"Reject {Selected.Employee?.FirstName} {Selected.Employee?.LastName}'s {Selected.LeaveType} leave, {Selected.DateFrom} to {Selected.DateTo}?",
                "Confirm Reject", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;
            await Decide(HrisLeaveService.RejectAsync(Selected.Id, txt_note.Text.Trim()), "Leave request rejected.");
        }

        private async System.Threading.Tasks.Task Decide(System.Threading.Tasks.Task<GraphQLResponse<ApproveLeaveRequestData>> approveTask, string successMessage)
        {
            // Approve/Reject share the same response shape (HrisLeaveRequestModel);
            // callers pass whichever typed task applies. This overload handles Approve.
            try
            {
                Helpers.Loading.ShowLoading(dgv_list, "Saving data...");
                var result = await approveTask;
                if (result.HasErrors)
                {
                    Helpers.ShowDialogMessage("error", result.ErrorMessage);
                    return;
                }
                Helpers.ShowDialogMessage("success", successMessage);
                await LoadData();
            }
            catch (Exception ex)
            {
                Helpers.ShowDialogMessage("error", $"Failed: {ex.Message}");
            }
            finally
            {
                Helpers.Loading.HideLoading(dgv_list);
            }
        }

        private async System.Threading.Tasks.Task Decide(System.Threading.Tasks.Task<GraphQLResponse<RejectLeaveRequestData>> rejectTask, string successMessage)
        {
            try
            {
                Helpers.Loading.ShowLoading(dgv_list, "Saving data...");
                var result = await rejectTask;
                if (result.HasErrors)
                {
                    Helpers.ShowDialogMessage("error", result.ErrorMessage);
                    return;
                }
                Helpers.ShowDialogMessage("success", successMessage);
                await LoadData();
            }
            catch (Exception ex)
            {
                Helpers.ShowDialogMessage("error", $"Failed: {ex.Message}");
            }
            finally
            {
                Helpers.Loading.HideLoading(dgv_list);
            }
        }

        private void dgv_list_SelectionChanged(object sender, EventArgs e)
        {
            BindSelected();
        }
    }
}
