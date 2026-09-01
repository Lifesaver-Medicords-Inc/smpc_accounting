using smpc_accounting_app.Models.Hris;
using smpc_accounting_app.Services.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace smpc_accounting_app.Services.Hris
{
    // Typed operations for the HRIS Payroll module. Same contract as the other
    // Hris services: check HasErrors, never an HTTP status.
    static class HrisPayrollService
    {
        private const string RunFields = @"
            id periodStart periodEnd payFrequency cutoffYear cutoffMonth periodNo payDate status notes generationNotes
            employeeCount totalGross totalDeductions totalNet
            items {
                id employeeId employee { id employeeNo firstName middleName lastName }
                rateType basicRate daysWorked daysPaidLeave totalHours otHours ndHours lateUtMinutes regHolidayHours specialHolidayHours regHolidayUnworkedDays
                basicPayAmount otPay ndPay holidayPremiumPay allowanceAmount otherEarnings
                sssEe philhealthEe pagibigEe withholdingTax tardinessDeduction otherDeductions
                grossPay deductionsTotal netPay remarks
            }";

        public static Task<GraphQLResponse<PayrollRunsData>> GetPayrollRunsAsync()
        {
            string query = "{ payrollRuns(page: 1, pageSize: 200) { totalCount items { " + RunFields + " } } }";
            return HrisApiService.ExecuteAsync<PayrollRunsData>(query);
        }

        public static Task<GraphQLResponse<CreatePayrollRunData>> CreateAsync(Dictionary<string, object> input)
        {
            string query = "mutation($input: PayrollRunInput!) { createPayrollRun(input: $input) { " + RunFields + " } }";
            return HrisApiService.ExecuteAsync<CreatePayrollRunData>(query, new Dictionary<string, object> { { "input", input } });
        }

        public static Task<GraphQLResponse<UpdatePayrollRunData>> UpdateAsync(int id, Dictionary<string, object> input)
        {
            string query = "mutation($id: ID!, $input: PayrollRunInput!) { updatePayrollRun(id: $id, input: $input) { " + RunFields + " } }";
            return HrisApiService.ExecuteAsync<UpdatePayrollRunData>(query, new Dictionary<string, object> { { "id", id }, { "input", input } });
        }

        public static Task<GraphQLResponse<RegeneratePayrollRunData>> RegenerateAsync(int id)
        {
            string query = "mutation($id: ID!) { regeneratePayrollRun(id: $id) { " + RunFields + " } }";
            return HrisApiService.ExecuteAsync<RegeneratePayrollRunData>(query, new Dictionary<string, object> { { "id", id } });
        }

        public static Task<GraphQLResponse<UpdatePayrollItemsData>> UpdateItemsAsync(int runId, List<object> items)
        {
            string query = "mutation($runId: ID!, $items: [PayrollItemOverrideInput!]!) { updatePayrollItems(runId: $runId, items: $items) { " + RunFields + " } }";
            return HrisApiService.ExecuteAsync<UpdatePayrollItemsData>(query, new Dictionary<string, object> { { "runId", runId }, { "items", items } });
        }

        public static Task<GraphQLResponse<ApprovePayrollRunData>> ApproveAsync(int id)
        {
            string query = "mutation($id: ID!) { approvePayrollRun(id: $id) { " + RunFields + " } }";
            return HrisApiService.ExecuteAsync<ApprovePayrollRunData>(query, new Dictionary<string, object> { { "id", id } });
        }

        public static Task<GraphQLResponse<ReopenPayrollRunData>> ReopenAsync(int id)
        {
            string query = "mutation($id: ID!) { reopenPayrollRun(id: $id) { " + RunFields + " } }";
            return HrisApiService.ExecuteAsync<ReopenPayrollRunData>(query, new Dictionary<string, object> { { "id", id } });
        }
    }
}
