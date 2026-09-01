using Newtonsoft.Json;
using System.Collections.Generic;

namespace smpc_accounting_app.Models.Hris
{
    // DTOs for the HRIS People Analytics module (GraphQL, camelCase). Every
    // query recomputes live server-side - nothing here is a stored snapshot.

    public class HrisLabelCountModel
    {
        [JsonProperty("label")] public string Label { get; set; }
        [JsonProperty("count")] public int Count { get; set; }
    }

    public class HrisHeadcountSummaryModel
    {
        [JsonProperty("totalActive")] public int TotalActive { get; set; }
        [JsonProperty("byDepartment")] public List<HrisLabelCountModel> ByDepartment { get; set; } = new List<HrisLabelCountModel>();
        [JsonProperty("byEmploymentStatus")] public List<HrisLabelCountModel> ByEmploymentStatus { get; set; } = new List<HrisLabelCountModel>();
        [JsonProperty("byScheduleType")] public List<HrisLabelCountModel> ByScheduleType { get; set; } = new List<HrisLabelCountModel>();
        [JsonProperty("byPayFrequency")] public List<HrisLabelCountModel> ByPayFrequency { get; set; } = new List<HrisLabelCountModel>();
        [JsonProperty("averageTenureYears")] public double AverageTenureYears { get; set; }
        [JsonProperty("hiredThisYear")] public int HiredThisYear { get; set; }
        [JsonProperty("exitedThisYear")] public int ExitedThisYear { get; set; }
        [JsonProperty("turnoverRatePercent")] public double TurnoverRatePercent { get; set; }
    }

    public class HrisMonthMetricModel
    {
        [JsonProperty("month")] public string Month { get; set; }
        [JsonProperty("hires")] public int Hires { get; set; }
        [JsonProperty("exits")] public int Exits { get; set; }
        [JsonProperty("employeeCount")] public int EmployeeCount { get; set; }
        [JsonProperty("totalGross")] public decimal TotalGross { get; set; }
        [JsonProperty("totalDeductions")] public decimal TotalDeductions { get; set; }
        [JsonProperty("totalNet")] public decimal TotalNet { get; set; }
    }

    public class HrisAttendanceSummaryModel
    {
        [JsonProperty("timesheetCount")] public int TimesheetCount { get; set; }
        [JsonProperty("totalDaysWorked")] public int TotalDaysWorked { get; set; }
        [JsonProperty("totalDaysAbsent")] public int TotalDaysAbsent { get; set; }
        [JsonProperty("totalOtHours")] public double TotalOtHours { get; set; }
        [JsonProperty("totalLateMinutes")] public int TotalLateMinutes { get; set; }
        [JsonProperty("totalUndertimeMinutes")] public int TotalUndertimeMinutes { get; set; }
        [JsonProperty("averageTardinessMinutes")] public double AverageTardinessMinutes { get; set; }
    }

    public class HrisLeaveTypeUtilizationModel
    {
        [JsonProperty("leaveType")] public string LeaveType { get; set; }
        [JsonProperty("requests")] public int Requests { get; set; }
        [JsonProperty("daysTaken")] public int DaysTaken { get; set; }
    }

    public class HrisLeaveUtilizationModel
    {
        [JsonProperty("year")] public int Year { get; set; }
        [JsonProperty("byType")] public List<HrisLeaveTypeUtilizationModel> ByType { get; set; } = new List<HrisLeaveTypeUtilizationModel>();
        [JsonProperty("pendingCount")] public int PendingCount { get; set; }
        [JsonProperty("rejectedCount")] public int RejectedCount { get; set; }
        [JsonProperty("cancelledCount")] public int CancelledCount { get; set; }
    }

    public class HeadcountSummaryData
    {
        [JsonProperty("headcountSummary")] public HrisHeadcountSummaryModel HeadcountSummary { get; set; }
    }
    public class TurnoverTrendData
    {
        [JsonProperty("turnoverTrend")] public List<HrisMonthMetricModel> TurnoverTrend { get; set; } = new List<HrisMonthMetricModel>();
    }
    public class AttendanceSummaryData
    {
        [JsonProperty("attendanceSummary")] public HrisAttendanceSummaryModel AttendanceSummary { get; set; }
    }
    public class LeaveUtilizationData
    {
        [JsonProperty("leaveUtilization")] public HrisLeaveUtilizationModel LeaveUtilization { get; set; }
    }
    public class PayrollCostTrendData
    {
        [JsonProperty("payrollCostTrend")] public List<HrisMonthMetricModel> PayrollCostTrend { get; set; } = new List<HrisMonthMetricModel>();
    }
}
