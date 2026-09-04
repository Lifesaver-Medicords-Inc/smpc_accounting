using smpc_accounting_app.Models.Hris;
using smpc_accounting_app.Services.Helpers;
using smpc_accounting_app.Services.Hris;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace smpc_accounting_app.Pages.Setup.Hris
{
    // HRIS People Analytics - read-only dashboards over the employee master,
    // timesheets, leave requests, and payroll runs. Every number here is
    // recomputed live by the HRIS API on each Refresh; nothing is cached or
    // stored - this page never writes anything back.
    //
    // Three independent views sharing one tab strip, each built in code
    // (BuildHeadcountTab / BuildTrendsTab / BuildAttendanceLeaveTab) - same
    // reasoning BenefitsPage.cs uses for its two tabs.
    //
    // Constructor stays I/O-free (RoutesService instantiates pages eagerly).
    public partial class PeopleAnalyticsPage : UserControl
    {
        private bool _loaded;

        // ── Headcount tab ────────────────────────────────────────────────
        private Label lbl_totalActive, lbl_hired, lbl_exited, lbl_turnover, lbl_tenure;
        private Chart chart_department;
        private DataGridView dgv_byStatus, dgv_bySchedule, dgv_byFrequency;

        // ── Trends tab ───────────────────────────────────────────────────
        private NumericUpDown num_trendMonths;
        private Chart chart_turnover, chart_payrollCost;

        // ── Attendance & Leave tab ───────────────────────────────────────
        private NumericUpDown num_alYear;
        private ComboBox cmb_alMonth;
        private Label lbl_tsCount, lbl_daysWorked, lbl_daysAbsent, lbl_otHours, lbl_lateMin, lbl_utMin, lbl_avgTardy;
        private DataGridView dgv_leaveByType;
        private Label lbl_leavePending, lbl_leaveRejected, lbl_leaveCancelled;

        public PeopleAnalyticsPage()
        {
            InitializeComponent();

            var tabs = new TabControl { Dock = DockStyle.Fill };
            var tabHeadcount = new TabPage("Headcount");
            var tabTrends = new TabPage("Trends");
            var tabAttendanceLeave = new TabPage("Attendance && Leave");
            tabs.TabPages.Add(tabHeadcount);
            tabs.TabPages.Add(tabTrends);
            tabs.TabPages.Add(tabAttendanceLeave);
            pnl_body.Controls.Add(tabs);

            BuildHeadcountTab(tabHeadcount);
            BuildTrendsTab(tabTrends);
            BuildAttendanceLeaveTab(tabAttendanceLeave);
        }

        // ================================================================
        // Shared helpers
        // ================================================================

        private static Panel MakeStatCard(string caption, out Label valueLabel)
        {
            var card = new Panel
            {
                Width = 190,
                Height = 80,
                Margin = new Padding(6),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.WhiteSmoke,
            };
            valueLabel = new Label
            {
                Dock = DockStyle.Top,
                Height = 40,
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Text = "—",
            };
            var captionLabel = new Label
            {
                Dock = DockStyle.Top,
                Height = 24,
                Font = new Font("Segoe UI", 8.5F),
                ForeColor = Color.DimGray,
                TextAlign = ContentAlignment.TopCenter,
                Text = caption,
            };
            card.Controls.Add(captionLabel);
            card.Controls.Add(valueLabel);
            return card;
        }

        private static FlowLayoutPanel MakeCardRow()
        {
            return new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 100,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(10),
                AutoScroll = true,
            };
        }

        private static Chart MakeChart(string title)
        {
            var chart = new Chart { Dock = DockStyle.Fill };
            var area = new ChartArea("main");
            area.AxisX.MajorGrid.LineColor = Color.Gainsboro;
            area.AxisY.MajorGrid.LineColor = Color.Gainsboro;
            chart.ChartAreas.Add(area);
            chart.Legends.Add(new Legend("legend"));
            chart.Titles.Add(new Title(title, Docking.Top, new Font("Segoe UI", 10, FontStyle.Bold), Color.Black));
            return chart;
        }

        private static DataGridView MakeGrid(params (string name, string header, int width)[] columns)
        {
            var dgv = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                AutoGenerateColumns = false,
                BackgroundColor = SystemColors.Window,
                ReadOnly = true,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
            };
            foreach (var (name, header, width) in columns)
            {
                var col = new DataGridViewTextBoxColumn { Name = name, HeaderText = header };
                if (width > 0) col.Width = width; else col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgv.Columns.Add(col);
            }
            return dgv;
        }

        // ================================================================
        // Headcount tab
        // ================================================================

        private void BuildHeadcountTab(TabPage tab)
        {
            var toolStrip = new ToolStrip { GripStyle = ToolStripGripStyle.Hidden };
            var btnRefresh = new ToolStripButton("Refresh");
            btnRefresh.Click += async (s, e) => await LoadHeadcount();
            toolStrip.Items.Add(btnRefresh);
            tab.Controls.Add(toolStrip);

            var cardRow = MakeCardRow();
            cardRow.Controls.Add(MakeStatCard("TOTAL ACTIVE", out lbl_totalActive));
            cardRow.Controls.Add(MakeStatCard("HIRED THIS YEAR", out lbl_hired));
            cardRow.Controls.Add(MakeStatCard("EXITED THIS YEAR", out lbl_exited));
            cardRow.Controls.Add(MakeStatCard("TURNOVER RATE", out lbl_turnover));
            cardRow.Controls.Add(MakeStatCard("AVG TENURE (YRS)", out lbl_tenure));
            tab.Controls.Add(cardRow);

            // Plain percentage-based TableLayoutPanel, not SplitContainer -
            // SplitContainer's own internal layout code re-validates the
            // splitter position against the control's CURRENT size on every
            // layout pass, not just when this code explicitly sets it, and
            // throws "Height/Width must be greater than 0px" if that happens
            // while it's still 0-sized (e.g. sitting in a TabPage that isn't
            // the initially-selected one, which never gets a real layout
            // pass until clicked). A row/column percentage split has no
            // equivalent runtime check, so it can't hit this class of bug.
            var pnl_main = new Panel { Dock = DockStyle.Fill, Padding = new Padding(10) };
            var mainSplit = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 1, ColumnCount = 2 };
            mainSplit.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            mainSplit.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            mainSplit.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            chart_department = MakeChart("Active Employees by Department");
            mainSplit.Controls.Add(chart_department, 0, 0);

            var breakdowns = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 3, ColumnCount = 1 };
            breakdowns.RowStyles.Add(new RowStyle(SizeType.Percent, 33F));
            breakdowns.RowStyles.Add(new RowStyle(SizeType.Percent, 33F));
            breakdowns.RowStyles.Add(new RowStyle(SizeType.Percent, 34F));

            Panel LabeledGrid(string title, out DataGridView grid)
            {
                var p = new Panel { Dock = DockStyle.Fill, Padding = new Padding(4) };
                grid = MakeGrid(("label", "", 0), ("count", title.ToUpperInvariant(), 90));
                p.Controls.Add(grid);
                return p;
            }
            breakdowns.Controls.Add(LabeledGrid("Employment Status", out dgv_byStatus), 0, 0);
            breakdowns.Controls.Add(LabeledGrid("Schedule Type", out dgv_bySchedule), 0, 1);
            breakdowns.Controls.Add(LabeledGrid("Pay Frequency", out dgv_byFrequency), 0, 2);
            mainSplit.Controls.Add(breakdowns, 1, 0);

            pnl_main.Controls.Add(mainSplit);
            tab.Controls.Add(pnl_main);

            tab.Controls.SetChildIndex(pnl_main, 0);
            tab.Controls.SetChildIndex(cardRow, 1);
            tab.Controls.SetChildIndex(toolStrip, 2);
        }

        private async System.Threading.Tasks.Task LoadHeadcount()
        {
            try
            {
                Helpers.Loading.ShowLoading(dgv_byStatus, "Fetching data...");
                var result = await HrisAnalyticsService.GetHeadcountSummaryAsync();
                if (result.HasErrors)
                {
                    Helpers.ShowDialogMessage("error", result.ErrorMessage);
                    return;
                }
                var d = result.Data.HeadcountSummary;
                lbl_totalActive.Text = d.TotalActive.ToString();
                lbl_hired.Text = d.HiredThisYear.ToString();
                lbl_exited.Text = d.ExitedThisYear.ToString();
                lbl_turnover.Text = $"{d.TurnoverRatePercent:0.0}%";
                lbl_tenure.Text = d.AverageTenureYears.ToString("0.0");

                chart_department.Series.Clear();
                var series = new Series("Department") { ChartType = SeriesChartType.Pie, IsValueShownAsLabel = true };
                foreach (var row in d.ByDepartment)
                    series.Points.AddXY(string.IsNullOrWhiteSpace(row.Label) ? "(none)" : row.Label, row.Count);
                chart_department.Series.Add(series);

                void FillGrid(DataGridView grid, System.Collections.Generic.List<HrisLabelCountModel> rows)
                {
                    grid.Rows.Clear();
                    foreach (var row in rows) grid.Rows.Add(row.Label, row.Count);
                }
                FillGrid(dgv_byStatus, d.ByEmploymentStatus);
                FillGrid(dgv_bySchedule, d.ByScheduleType);
                FillGrid(dgv_byFrequency, d.ByPayFrequency);
            }
            catch (Exception ex)
            {
                Helpers.ShowDialogMessage("error", $"Failed to load: {ex.Message}");
            }
            finally
            {
                Helpers.Loading.HideLoading(dgv_byStatus);
            }
        }

        // ================================================================
        // Trends tab
        // ================================================================

        private void BuildTrendsTab(TabPage tab)
        {
            var toolStrip = new ToolStrip { GripStyle = ToolStripGripStyle.Hidden };
            toolStrip.Items.Add(new ToolStripLabel("Months:"));
            num_trendMonths = new NumericUpDown { Minimum = 3, Maximum = 36, Value = 12, Width = 60 };
            var hostMonths = new ToolStripControlHost(num_trendMonths);
            toolStrip.Items.Add(hostMonths);
            var btnRefresh = new ToolStripButton("Refresh");
            btnRefresh.Click += async (s, e) => await LoadTrends();
            toolStrip.Items.Add(btnRefresh);
            tab.Controls.Add(toolStrip);

            // TableLayoutPanel, not SplitContainer - see the comment on the
            // Headcount tab's mainSplit above for why.
            var trendsSplit = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 2, ColumnCount = 1 };
            trendsSplit.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            trendsSplit.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            chart_turnover = MakeChart("Turnover Trend (Hires vs Exits)");
            chart_payrollCost = MakeChart("Payroll Cost Trend (Approved Runs, Total Net)");
            trendsSplit.Controls.Add(chart_turnover, 0, 0);
            trendsSplit.Controls.Add(chart_payrollCost, 0, 1);
            tab.Controls.Add(trendsSplit);

            tab.Controls.SetChildIndex(trendsSplit, 0);
            tab.Controls.SetChildIndex(toolStrip, 1);
        }

        private async System.Threading.Tasks.Task LoadTrends()
        {
            int months = (int)num_trendMonths.Value;
            try
            {
                Helpers.Loading.ShowLoading(chart_turnover, "Fetching data...");

                var turnover = await HrisAnalyticsService.GetTurnoverTrendAsync(months);
                if (turnover.HasErrors)
                {
                    Helpers.ShowDialogMessage("error", turnover.ErrorMessage);
                    return;
                }
                chart_turnover.Series.Clear();
                var hires = new Series("Hires") { ChartType = SeriesChartType.Column, Color = Color.SeaGreen };
                var exits = new Series("Exits") { ChartType = SeriesChartType.Column, Color = Color.IndianRed };
                foreach (var row in turnover.Data.TurnoverTrend)
                {
                    hires.Points.AddXY(row.Month, row.Hires);
                    exits.Points.AddXY(row.Month, row.Exits);
                }
                chart_turnover.Series.Add(hires);
                chart_turnover.Series.Add(exits);

                var payroll = await HrisAnalyticsService.GetPayrollCostTrendAsync(months);
                if (payroll.HasErrors)
                {
                    Helpers.ShowDialogMessage("error", payroll.ErrorMessage);
                    return;
                }
                chart_payrollCost.Series.Clear();
                var net = new Series("Total Net") { ChartType = SeriesChartType.Line, Color = Color.SteelBlue, BorderWidth = 3 };
                foreach (var row in payroll.Data.PayrollCostTrend)
                    net.Points.AddXY(row.Month, (double)row.TotalNet);
                chart_payrollCost.Series.Add(net);
            }
            catch (Exception ex)
            {
                Helpers.ShowDialogMessage("error", $"Failed to load: {ex.Message}");
            }
            finally
            {
                Helpers.Loading.HideLoading(chart_turnover);
            }
        }

        // ================================================================
        // Attendance & Leave tab
        // ================================================================

        private void BuildAttendanceLeaveTab(TabPage tab)
        {
            var toolStrip = new ToolStrip { GripStyle = ToolStripGripStyle.Hidden };
            toolStrip.Items.Add(new ToolStripLabel("Year:"));
            num_alYear = new NumericUpDown { Minimum = 2000, Maximum = 2100, Value = DateTime.Now.Year, Width = 70 };
            toolStrip.Items.Add(new ToolStripControlHost(num_alYear));
            toolStrip.Items.Add(new ToolStripLabel("Month:"));
            cmb_alMonth = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 90 };
            cmb_alMonth.Items.Add("ALL");
            for (int m = 1; m <= 12; m++) cmb_alMonth.Items.Add(m.ToString());
            cmb_alMonth.SelectedIndex = 0;
            toolStrip.Items.Add(new ToolStripControlHost(cmb_alMonth));
            var btnRefresh = new ToolStripButton("Refresh");
            btnRefresh.Click += async (s, e) => await LoadAttendanceLeave();
            toolStrip.Items.Add(btnRefresh);
            tab.Controls.Add(toolStrip);

            var cardRow = MakeCardRow();
            cardRow.Height = 100;
            cardRow.Controls.Add(MakeStatCard("TIMESHEETS", out lbl_tsCount));
            cardRow.Controls.Add(MakeStatCard("DAYS WORKED", out lbl_daysWorked));
            cardRow.Controls.Add(MakeStatCard("DAYS ABSENT", out lbl_daysAbsent));
            cardRow.Controls.Add(MakeStatCard("OT HOURS", out lbl_otHours));
            cardRow.Controls.Add(MakeStatCard("LATE (MIN)", out lbl_lateMin));
            cardRow.Controls.Add(MakeStatCard("UNDERTIME (MIN)", out lbl_utMin));
            cardRow.Controls.Add(MakeStatCard("AVG TARDINESS (MIN)", out lbl_avgTardy));
            tab.Controls.Add(cardRow);

            var pnl_leave = new Panel { Dock = DockStyle.Fill, Padding = new Padding(10) };
            var leaveTable = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2 };
            leaveTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            leaveTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));

            var gridPanel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(4) };
            var lbl = new Label { Dock = DockStyle.Top, Text = "APPROVED LEAVE BY TYPE (selected year)", Height = 24, Font = new Font("Segoe UI", 8.5F, FontStyle.Bold) };
            dgv_leaveByType = MakeGrid(("type", "TYPE", 100), ("requests", "REQUESTS", 90), ("days", "DAYS TAKEN", 100));
            gridPanel.Controls.Add(dgv_leaveByType);
            gridPanel.Controls.Add(lbl);
            leaveTable.Controls.Add(gridPanel, 0, 0);

            var otherStatuses = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.TopDown, Padding = new Padding(10), AutoScroll = true };
            otherStatuses.Controls.Add(MakeStatCard("PENDING", out lbl_leavePending));
            otherStatuses.Controls.Add(MakeStatCard("REJECTED", out lbl_leaveRejected));
            otherStatuses.Controls.Add(MakeStatCard("CANCELLED", out lbl_leaveCancelled));
            leaveTable.Controls.Add(otherStatuses, 1, 0);

            pnl_leave.Controls.Add(leaveTable);
            tab.Controls.Add(pnl_leave);

            tab.Controls.SetChildIndex(pnl_leave, 0);
            tab.Controls.SetChildIndex(cardRow, 1);
            tab.Controls.SetChildIndex(toolStrip, 2);
        }

        private async System.Threading.Tasks.Task LoadAttendanceLeave()
        {
            int year = (int)num_alYear.Value;
            int? month = cmb_alMonth.SelectedIndex > 0 ? cmb_alMonth.SelectedIndex : (int?)null;
            try
            {
                Helpers.Loading.ShowLoading(dgv_leaveByType, "Fetching data...");

                var attendance = await HrisAnalyticsService.GetAttendanceSummaryAsync(year, month);
                if (attendance.HasErrors)
                {
                    Helpers.ShowDialogMessage("error", attendance.ErrorMessage);
                    return;
                }
                var a = attendance.Data.AttendanceSummary;
                lbl_tsCount.Text = a.TimesheetCount.ToString();
                lbl_daysWorked.Text = a.TotalDaysWorked.ToString();
                lbl_daysAbsent.Text = a.TotalDaysAbsent.ToString();
                lbl_otHours.Text = a.TotalOtHours.ToString("0.0");
                lbl_lateMin.Text = a.TotalLateMinutes.ToString();
                lbl_utMin.Text = a.TotalUndertimeMinutes.ToString();
                lbl_avgTardy.Text = a.AverageTardinessMinutes.ToString("0.0");

                var leave = await HrisAnalyticsService.GetLeaveUtilizationAsync(year);
                if (leave.HasErrors)
                {
                    Helpers.ShowDialogMessage("error", leave.ErrorMessage);
                    return;
                }
                var l = leave.Data.LeaveUtilization;
                dgv_leaveByType.Rows.Clear();
                foreach (var row in l.ByType) dgv_leaveByType.Rows.Add(row.LeaveType, row.Requests, row.DaysTaken);
                lbl_leavePending.Text = l.PendingCount.ToString();
                lbl_leaveRejected.Text = l.RejectedCount.ToString();
                lbl_leaveCancelled.Text = l.CancelledCount.ToString();
            }
            catch (Exception ex)
            {
                Helpers.ShowDialogMessage("error", $"Failed to load: {ex.Message}");
            }
            finally
            {
                Helpers.Loading.HideLoading(dgv_leaveByType);
            }
        }

        // ================================================================

        private async void PeopleAnalyticsPage_Load(object sender, EventArgs e)
        {
            if (_loaded) return;
            _loaded = true;
            await LoadHeadcount();
            await LoadTrends();
            await LoadAttendanceLeave();
        }
    }
}
