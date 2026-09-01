using smpc_accounting_app.Models.Hris;
using smpc_accounting_app.Services.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace smpc_accounting_app.Services.Hris
{
    // Typed operations against the HRIS GraphQL API. Every method returns
    // GraphQLResponse<T>; callers must check HasErrors (GraphQL business errors
    // arrive with HTTP 200 - there is no Success flag).
    static class HrisEmployeeService
    {
        // Full selection set - the detail panel needs everything.
        private const string EmployeeFields = @"
            id employeeNo userId
            user { id employeeId firstName lastName department }
            firstName middleName lastName suffix birthDate gender civilStatus
            department positionId position { id code name }
            employmentStatus scheduleType payFrequency workStartTime workEndTime hireDate regularizationDate endDate isActive
            sssNo philhealthNo pagibigNo tin taxStatus mobileNo email
            addresses { addressType unitNo streetName barangay city province region country postalCode }
            compensations { basicPay rateType bankName bankAccountNo effectiveDate isCurrent }
            allowances { name amount isTaxable effectiveDate }
            emergencyContacts { name relationship contactNo address }
            dependents { name relationship birthDate }
            educations { level school course yearFrom yearTo }
            workHistories { employer position dateFrom dateTo reasonForLeaving }
            files { id fileName originalName type size category }";

        public static Task<GraphQLResponse<EmployeesData>> GetEmployeesAsync(string search = null)
        {
            string query = "query($search: String) { employees(filter: { search: $search }, page: 1, pageSize: 200) { totalCount items { " + EmployeeFields + " } } }";
            return HrisApiService.ExecuteAsync<EmployeesData>(query, new Dictionary<string, object> { { "search", search } });
        }

        public static Task<GraphQLResponse<CreateEmployeeData>> CreateAsync(Dictionary<string, object> input)
        {
            string query = "mutation($input: EmployeeInput!) { createEmployee(input: $input) { " + EmployeeFields + " } }";
            return HrisApiService.ExecuteAsync<CreateEmployeeData>(query, new Dictionary<string, object> { { "input", input } });
        }

        public static Task<GraphQLResponse<UpdateEmployeeData>> UpdateAsync(int id, Dictionary<string, object> input)
        {
            string query = "mutation($id: ID!, $input: EmployeeInput!) { updateEmployee(id: $id, input: $input) { " + EmployeeFields + " } }";
            return HrisApiService.ExecuteAsync<UpdateEmployeeData>(query, new Dictionary<string, object> { { "id", id }, { "input", input } });
        }

        public static Task<GraphQLResponse<SetEmployeeStatusData>> SetStatusAsync(int id, string status, string endDate)
        {
            string query = "mutation($id: ID!, $status: EmploymentStatus!, $endDate: String) { setEmployeeStatus(id: $id, status: $status, endDate: $endDate) { " + EmployeeFields + " } }";
            return HrisApiService.ExecuteAsync<SetEmployeeStatusData>(query, new Dictionary<string, object> { { "id", id }, { "status", status }, { "endDate", string.IsNullOrWhiteSpace(endDate) ? null : endDate } });
        }

        public static Task<GraphQLResponse<LinkUserAccountData>> LinkUserAsync(int employeeId, int? userId)
        {
            string query = "mutation($employeeId: ID!, $userId: ID) { linkUserAccount(employeeId: $employeeId, userId: $userId) { " + EmployeeFields + " } }";
            return HrisApiService.ExecuteAsync<LinkUserAccountData>(query, new Dictionary<string, object> { { "employeeId", employeeId }, { "userId", userId } });
        }

        public static Task<GraphQLResponse<AddEmployeeFileData>> AddFileAsync(int employeeId, HrisUploadResult upload, string category)
        {
            string query = "mutation($employeeId: ID!, $meta: FileMetaInput!) { addEmployeeFile(employeeId: $employeeId, meta: $meta) { id fileName originalName type size category } }";
            var meta = new Dictionary<string, object>
            {
                { "fileName", upload.FileName },
                { "originalName", upload.OriginalName },
                { "type", upload.Type },
                { "size", upload.Size },
                { "category", category },
            };
            return HrisApiService.ExecuteAsync<AddEmployeeFileData>(query, new Dictionary<string, object> { { "employeeId", employeeId }, { "meta", meta } });
        }

        public static Task<GraphQLResponse<DeleteEmployeeFileData>> DeleteFileAsync(int fileId)
        {
            string query = "mutation($id: ID!) { deleteEmployeeFile(id: $id) }";
            return HrisApiService.ExecuteAsync<DeleteEmployeeFileData>(query, new Dictionary<string, object> { { "id", fileId } });
        }

        public static Task<GraphQLResponse<PositionsData>> GetPositionsAsync()
        {
            return HrisApiService.ExecuteAsync<PositionsData>("{ positions { id code name } }");
        }

        public static Task<GraphQLResponse<ErpUsersData>> GetErpUsersAsync(string search = null)
        {
            string query = "query($search: String) { erpUsers(search: $search) { id employeeId firstName lastName department } }";
            return HrisApiService.ExecuteAsync<ErpUsersData>(query, new Dictionary<string, object> { { "search", search } });
        }
    }
}
