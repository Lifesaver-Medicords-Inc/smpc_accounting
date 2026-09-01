using smpc_accounting_app.Models.Hris;
using smpc_accounting_app.Services.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace smpc_accounting_app.Services.Hris
{
    // Typed operations for HR's leave decision queue. Same contract as the
    // other Hris services: check HasErrors, never an HTTP status.
    static class HrisLeaveService
    {
        private const string LeaveFields = @"
            id employeeId employee { id employeeNo firstName middleName lastName department }
            leaveType dateFrom dateTo reason status decisionNote decidedBy decidedAt";

        public static Task<GraphQLResponse<LeaveRequestsData>> GetLeaveRequestsAsync(string status)
        {
            string query;
            object variables = null;
            if (string.IsNullOrEmpty(status) || status == "ALL")
            {
                query = "{ leaveRequests { " + LeaveFields + " } }";
            }
            else
            {
                query = "query($status: LeaveStatus) { leaveRequests(filter: { status: $status }) { " + LeaveFields + " } }";
                variables = new Dictionary<string, object> { { "status", status } };
            }
            return HrisApiService.ExecuteAsync<LeaveRequestsData>(query, variables);
        }

        public static Task<GraphQLResponse<ApproveLeaveRequestData>> ApproveAsync(int id, string note)
        {
            string query = "mutation($id: ID!, $note: String) { approveLeaveRequest(id: $id, note: $note) { " + LeaveFields + " } }";
            return HrisApiService.ExecuteAsync<ApproveLeaveRequestData>(query, new Dictionary<string, object> { { "id", id }, { "note", note } });
        }

        public static Task<GraphQLResponse<RejectLeaveRequestData>> RejectAsync(int id, string note)
        {
            string query = "mutation($id: ID!, $note: String) { rejectLeaveRequest(id: $id, note: $note) { " + LeaveFields + " } }";
            return HrisApiService.ExecuteAsync<RejectLeaveRequestData>(query, new Dictionary<string, object> { { "id", id }, { "note", note } });
        }
    }
}
