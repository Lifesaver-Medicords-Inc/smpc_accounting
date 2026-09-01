using Newtonsoft.Json;
using System.Collections.Generic;

namespace smpc_accounting_app.Models.Hris
{
    // DTOs for the HRIS Timesheet module (GraphQL, camelCase like HrisModels.cs).

    public class HrisTimesheetModel
    {
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("employeeId")] public int EmployeeId { get; set; }
        [JsonProperty("employee")] public HrisEmployeeModel Employee { get; set; }
        [JsonProperty("periodStart")] public string PeriodStart { get; set; }
        [JsonProperty("periodEnd")] public string PeriodEnd { get; set; }
        [JsonProperty("cutoffYear")] public int CutoffYear { get; set; }
        [JsonProperty("cutoffMonth")] public int CutoffMonth { get; set; }
        [JsonProperty("periodNo")] public int PeriodNo { get; set; }
        [JsonProperty("status")] public string Status { get; set; }
        [JsonProperty("notes")] public string Notes { get; set; }
        [JsonProperty("daysWorked")] public int DaysWorked { get; set; }
        [JsonProperty("daysAbsent")] public int DaysAbsent { get; set; }
        [JsonProperty("daysPaidLeave")] public int DaysPaidLeave { get; set; }
        [JsonProperty("daysUnpaidLeave")] public int DaysUnpaidLeave { get; set; }
        [JsonProperty("totalHours")] public decimal TotalHours { get; set; }
        [JsonProperty("totalOtHours")] public decimal TotalOtHours { get; set; }
        [JsonProperty("totalNdHours")] public decimal TotalNdHours { get; set; }
        [JsonProperty("totalLateMinutes")] public int TotalLateMinutes { get; set; }
        [JsonProperty("totalUndertimeMinutes")] public int TotalUndertimeMinutes { get; set; }
        [JsonProperty("regHolidayWorkedHours")] public decimal RegHolidayWorkedHours { get; set; }
        [JsonProperty("specialHolidayWorkedHours")] public decimal SpecialHolidayWorkedHours { get; set; }
        [JsonProperty("regHolidayUnworkedDays")] public int RegHolidayUnworkedDays { get; set; }
        [JsonProperty("entries")] public List<HrisTimesheetEntryModel> Entries { get; set; } = new List<HrisTimesheetEntryModel>();
    }

    public class HrisTimesheetEntryModel
    {
        [JsonProperty("entryDate")] public string EntryDate { get; set; }
        [JsonProperty("dayType")] public string DayType { get; set; }
        [JsonProperty("timeIn")] public string TimeIn { get; set; }
        [JsonProperty("timeOut")] public string TimeOut { get; set; }
        [JsonProperty("breakMinutes")] public int BreakMinutes { get; set; }
        [JsonProperty("otHours")] public decimal OtHours { get; set; }
        [JsonProperty("hoursWorked")] public decimal HoursWorked { get; set; }
        [JsonProperty("ndHours")] public decimal NdHours { get; set; }
        [JsonProperty("lateMinutes")] public int LateMinutes { get; set; }
        [JsonProperty("undertimeMinutes")] public int UndertimeMinutes { get; set; }
        [JsonProperty("holidayType")] public string HolidayType { get; set; }
        [JsonProperty("timeInLat")] public decimal? TimeInLat { get; set; }
        [JsonProperty("timeInLng")] public decimal? TimeInLng { get; set; }
        [JsonProperty("timeInLocation")] public string TimeInLocation { get; set; }
        [JsonProperty("timeOutLat")] public decimal? TimeOutLat { get; set; }
        [JsonProperty("timeOutLng")] public decimal? TimeOutLng { get; set; }
        [JsonProperty("timeOutLocation")] public string TimeOutLocation { get; set; }
        [JsonProperty("remarks")] public string Remarks { get; set; }
    }

    public class HrisTimesheetPageModel
    {
        [JsonProperty("items")] public List<HrisTimesheetModel> Items { get; set; } = new List<HrisTimesheetModel>();
        [JsonProperty("totalCount")] public int TotalCount { get; set; }
    }

    public class HrisCutoffRowModel
    {
        [JsonProperty("employee")] public HrisEmployeeModel Employee { get; set; }
        [JsonProperty("timesheet")] public HrisTimesheetModel Timesheet { get; set; }
    }

    public class HrisCutoffCreateResultModel
    {
        [JsonProperty("created")] public int Created { get; set; }
        [JsonProperty("notes")] public string Notes { get; set; }
    }

    public class CutoffBoardData
    {
        [JsonProperty("cutoffBoard")] public System.Collections.Generic.List<HrisCutoffRowModel> CutoffBoard { get; set; }
    }

    public class CreateCutoffTimesheetsData
    {
        [JsonProperty("createCutoffTimesheets")] public HrisCutoffCreateResultModel CreateCutoffTimesheets { get; set; }
    }

    public class TimesheetsData
    {
        [JsonProperty("timesheets")] public HrisTimesheetPageModel Timesheets { get; set; }
    }
    public class CreateTimesheetData
    {
        [JsonProperty("createTimesheet")] public HrisTimesheetModel CreateTimesheet { get; set; }
    }
    public class UpdateTimesheetData
    {
        [JsonProperty("updateTimesheet")] public HrisTimesheetModel UpdateTimesheet { get; set; }
    }
    public class ApproveTimesheetData
    {
        [JsonProperty("approveTimesheet")] public HrisTimesheetModel ApproveTimesheet { get; set; }
    }
    public class ReopenTimesheetData
    {
        [JsonProperty("reopenTimesheet")] public HrisTimesheetModel ReopenTimesheet { get; set; }
    }
}
