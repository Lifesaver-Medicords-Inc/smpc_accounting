using Newtonsoft.Json;
using System.Collections.Generic;

namespace smpc_accounting_app.Models.Hris
{
    // DTOs for the HRIS Payroll module (GraphQL, camelCase like HrisModels.cs).

    public class HrisPayrollRunModel
    {
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("periodStart")] public string PeriodStart { get; set; }
        [JsonProperty("periodEnd")] public string PeriodEnd { get; set; }
        [JsonProperty("payFrequency")] public string PayFrequency { get; set; }
        [JsonProperty("cutoffYear")] public int CutoffYear { get; set; }
        [JsonProperty("cutoffMonth")] public int CutoffMonth { get; set; }
        [JsonProperty("periodNo")] public int PeriodNo { get; set; }
        [JsonProperty("payDate")] public string PayDate { get; set; }
        [JsonProperty("status")] public string Status { get; set; }
        [JsonProperty("notes")] public string Notes { get; set; }
        [JsonProperty("generationNotes")] public string GenerationNotes { get; set; }
        [JsonProperty("employeeCount")] public int EmployeeCount { get; set; }
        [JsonProperty("totalGross")] public decimal TotalGross { get; set; }
        [JsonProperty("totalDeductions")] public decimal TotalDeductions { get; set; }
        [JsonProperty("totalNet")] public decimal TotalNet { get; set; }
        [JsonProperty("items")] public List<HrisPayrollItemModel> Items { get; set; } = new List<HrisPayrollItemModel>();
    }

    public class HrisPayrollItemModel
    {
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("employeeId")] public int EmployeeId { get; set; }
        [JsonProperty("employee")] public HrisEmployeeModel Employee { get; set; }
        [JsonProperty("rateType")] public string RateType { get; set; }
        [JsonProperty("basicRate")] public decimal BasicRate { get; set; }
        [JsonProperty("daysWorked")] public int DaysWorked { get; set; }
        [JsonProperty("daysPaidLeave")] public int DaysPaidLeave { get; set; }
        [JsonProperty("totalHours")] public decimal TotalHours { get; set; }
        [JsonProperty("otHours")] public decimal OtHours { get; set; }
        [JsonProperty("ndHours")] public decimal NdHours { get; set; }
        [JsonProperty("lateUtMinutes")] public int LateUtMinutes { get; set; }
        [JsonProperty("regHolidayHours")] public decimal RegHolidayHours { get; set; }
        [JsonProperty("specialHolidayHours")] public decimal SpecialHolidayHours { get; set; }
        [JsonProperty("regHolidayUnworkedDays")] public int RegHolidayUnworkedDays { get; set; }
        [JsonProperty("basicPayAmount")] public decimal BasicPayAmount { get; set; }
        [JsonProperty("otPay")] public decimal OtPay { get; set; }
        [JsonProperty("ndPay")] public decimal NdPay { get; set; }
        [JsonProperty("holidayPremiumPay")] public decimal HolidayPremiumPay { get; set; }
        [JsonProperty("allowanceAmount")] public decimal AllowanceAmount { get; set; }
        [JsonProperty("otherEarnings")] public decimal OtherEarnings { get; set; }
        [JsonProperty("sssEe")] public decimal SssEe { get; set; }
        [JsonProperty("philhealthEe")] public decimal PhilhealthEe { get; set; }
        [JsonProperty("pagibigEe")] public decimal PagibigEe { get; set; }
        [JsonProperty("withholdingTax")] public decimal WithholdingTax { get; set; }
        [JsonProperty("tardinessDeduction")] public decimal TardinessDeduction { get; set; }
        [JsonProperty("otherDeductions")] public decimal OtherDeductions { get; set; }
        [JsonProperty("grossPay")] public decimal GrossPay { get; set; }
        [JsonProperty("deductionsTotal")] public decimal DeductionsTotal { get; set; }
        [JsonProperty("netPay")] public decimal NetPay { get; set; }
        [JsonProperty("remarks")] public string Remarks { get; set; }
    }

    public class HrisPayrollRunPageModel
    {
        [JsonProperty("items")] public List<HrisPayrollRunModel> Items { get; set; } = new List<HrisPayrollRunModel>();
        [JsonProperty("totalCount")] public int TotalCount { get; set; }
    }

    public class PayrollRunsData
    {
        [JsonProperty("payrollRuns")] public HrisPayrollRunPageModel PayrollRuns { get; set; }
    }
    public class CreatePayrollRunData
    {
        [JsonProperty("createPayrollRun")] public HrisPayrollRunModel CreatePayrollRun { get; set; }
    }
    public class UpdatePayrollRunData
    {
        [JsonProperty("updatePayrollRun")] public HrisPayrollRunModel UpdatePayrollRun { get; set; }
    }
    public class RegeneratePayrollRunData
    {
        [JsonProperty("regeneratePayrollRun")] public HrisPayrollRunModel RegeneratePayrollRun { get; set; }
    }
    public class UpdatePayrollItemsData
    {
        [JsonProperty("updatePayrollItems")] public HrisPayrollRunModel UpdatePayrollItems { get; set; }
    }
    public class ApprovePayrollRunData
    {
        [JsonProperty("approvePayrollRun")] public HrisPayrollRunModel ApprovePayrollRun { get; set; }
    }
    public class ReopenPayrollRunData
    {
        [JsonProperty("reopenPayrollRun")] public HrisPayrollRunModel ReopenPayrollRun { get; set; }
    }
}
