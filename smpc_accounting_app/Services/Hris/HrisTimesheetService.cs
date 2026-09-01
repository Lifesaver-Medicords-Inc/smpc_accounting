using smpc_accounting_app.Models.Hris;
using smpc_accounting_app.Services.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace smpc_accounting_app.Services.Hris
{
    // Typed operations for the HRIS Timesheet module. Same contract as
    // HrisEmployeeService: check HasErrors, never an HTTP status.
    static class HrisTimesheetService
    {
        private const string TimesheetFields = @"
            id employeeId
            employee { id employeeNo firstName middleName lastName }
            periodStart periodEnd cutoffYear cutoffMonth periodNo status notes
            daysWorked daysAbsent daysPaidLeave daysUnpaidLeave totalHours totalOtHours totalNdHours totalLateMinutes totalUndertimeMinutes regHolidayWorkedHours specialHolidayWorkedHours regHolidayUnworkedDays
            entries { entryDate dayType timeIn timeOut breakMinutes otHours hoursWorked ndHours lateMinutes undertimeMinutes holidayType timeInLat timeInLng timeInLocation timeOutLat timeOutLng timeOutLocation remarks }";

        public static Task<GraphQLResponse<TimesheetsData>> GetTimesheetsAsync()
        {
            string query = "{ timesheets(page: 1, pageSize: 200) { totalCount items { " + TimesheetFields + " } } }";
            return HrisApiService.ExecuteAsync<TimesheetsData>(query);
        }

        public static Task<GraphQLResponse<CreateTimesheetData>> CreateAsync(Dictionary<string, object> input)
        {
            string query = "mutation($input: TimesheetInput!) { createTimesheet(input: $input) { " + TimesheetFields + " } }";
            return HrisApiService.ExecuteAsync<CreateTimesheetData>(query, new Dictionary<string, object> { { "input", input } });
        }

        public static Task<GraphQLResponse<UpdateTimesheetData>> UpdateAsync(int id, Dictionary<string, object> input)
        {
            string query = "mutation($id: ID!, $input: TimesheetInput!) { updateTimesheet(id: $id, input: $input) { " + TimesheetFields + " } }";
            return HrisApiService.ExecuteAsync<UpdateTimesheetData>(query, new Dictionary<string, object> { { "id", id }, { "input", input } });
        }

        public static Task<GraphQLResponse<ApproveTimesheetData>> ApproveAsync(int id)
        {
            string query = "mutation($id: ID!) { approveTimesheet(id: $id) { " + TimesheetFields + " } }";
            return HrisApiService.ExecuteAsync<ApproveTimesheetData>(query, new Dictionary<string, object> { { "id", id } });
        }

        public static Task<GraphQLResponse<CutoffBoardData>> GetCutoffBoardAsync(string frequency, int year, int month, int periodNo)
        {
            string query = "query($f: PayFrequency!, $y: Int!, $m: Int!, $p: Int!) { cutoffBoard(frequency: $f, year: $y, month: $m, periodNo: $p) { employee { id employeeNo firstName middleName lastName payFrequency } timesheet { " + TimesheetFields + " } } }";
            return HrisApiService.ExecuteAsync<CutoffBoardData>(query, new Dictionary<string, object> { { "f", frequency }, { "y", year }, { "m", month }, { "p", periodNo } });
        }

        public static Task<GraphQLResponse<CreateCutoffTimesheetsData>> CreateCutoffTimesheetsAsync(string frequency, int year, int month, int periodNo)
        {
            string query = "mutation($f: PayFrequency!, $y: Int!, $m: Int!, $p: Int!) { createCutoffTimesheets(frequency: $f, year: $y, month: $m, periodNo: $p) { created notes } }";
            return HrisApiService.ExecuteAsync<CreateCutoffTimesheetsData>(query, new Dictionary<string, object> { { "f", frequency }, { "y", year }, { "m", month }, { "p", periodNo } });
        }

        public static Task<GraphQLResponse<ReopenTimesheetData>> ReopenAsync(int id)
        {
            string query = "mutation($id: ID!) { reopenTimesheet(id: $id) { " + TimesheetFields + " } }";
            return HrisApiService.ExecuteAsync<ReopenTimesheetData>(query, new Dictionary<string, object> { { "id", id } });
        }
    }
}
