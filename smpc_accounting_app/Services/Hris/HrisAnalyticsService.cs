using smpc_accounting_app.Models.Hris;
using smpc_accounting_app.Services.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace smpc_accounting_app.Services.Hris
{
    // Typed operations for HRIS People Analytics - read-only aggregates,
    // no mutations. Same contract as the other Hris services: check
    // HasErrors, never an HTTP status.
    static class HrisAnalyticsService
    {
        public static Task<GraphQLResponse<HeadcountSummaryData>> GetHeadcountSummaryAsync()
        {
            const string query = @"{ headcountSummary {
                totalActive
                byDepartment { label count }
                byEmploymentStatus { label count }
                byScheduleType { label count }
                byPayFrequency { label count }
                averageTenureYears hiredThisYear exitedThisYear turnoverRatePercent
            } }";
            return HrisApiService.ExecuteAsync<HeadcountSummaryData>(query);
        }

        public static Task<GraphQLResponse<TurnoverTrendData>> GetTurnoverTrendAsync(int months)
        {
            const string query = @"query($months: Int) { turnoverTrend(months: $months) { month hires exits } }";
            return HrisApiService.ExecuteAsync<TurnoverTrendData>(query, new Dictionary<string, object> { { "months", months } });
        }

        public static Task<GraphQLResponse<AttendanceSummaryData>> GetAttendanceSummaryAsync(int? year, int? month)
        {
            const string query = @"query($year: Int, $month: Int) { attendanceSummary(year: $year, month: $month) {
                timesheetCount totalDaysWorked totalDaysAbsent totalOtHours totalLateMinutes totalUndertimeMinutes averageTardinessMinutes
            } }";
            return HrisApiService.ExecuteAsync<AttendanceSummaryData>(query, new Dictionary<string, object> { { "year", year }, { "month", month } });
        }

        public static Task<GraphQLResponse<LeaveUtilizationData>> GetLeaveUtilizationAsync(int year)
        {
            const string query = @"query($year: Int) { leaveUtilization(year: $year) {
                year byType { leaveType requests daysTaken } pendingCount rejectedCount cancelledCount
            } }";
            return HrisApiService.ExecuteAsync<LeaveUtilizationData>(query, new Dictionary<string, object> { { "year", year } });
        }

        public static Task<GraphQLResponse<PayrollCostTrendData>> GetPayrollCostTrendAsync(int months)
        {
            const string query = @"query($months: Int) { payrollCostTrend(months: $months) { month employeeCount totalGross totalDeductions totalNet } }";
            return HrisApiService.ExecuteAsync<PayrollCostTrendData>(query, new Dictionary<string, object> { { "months", months } });
        }
    }
}
