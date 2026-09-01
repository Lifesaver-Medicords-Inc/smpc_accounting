using Newtonsoft.Json;
using System.Collections.Generic;

namespace smpc_accounting_app.Models.Hris
{
    // DTOs for the HRIS Leave Requests module (GraphQL, camelCase).

    public class HrisLeaveRequestModel
    {
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("employeeId")] public int EmployeeId { get; set; }
        [JsonProperty("employee")] public HrisEmployeeModel Employee { get; set; }
        [JsonProperty("leaveType")] public string LeaveType { get; set; }
        [JsonProperty("dateFrom")] public string DateFrom { get; set; }
        [JsonProperty("dateTo")] public string DateTo { get; set; }
        [JsonProperty("reason")] public string Reason { get; set; }
        [JsonProperty("status")] public string Status { get; set; }
        [JsonProperty("decisionNote")] public string DecisionNote { get; set; }
        [JsonProperty("decidedBy")] public string DecidedBy { get; set; }
        [JsonProperty("decidedAt")] public string DecidedAt { get; set; }
    }

    public class LeaveRequestsData
    {
        [JsonProperty("leaveRequests")] public List<HrisLeaveRequestModel> LeaveRequests { get; set; } = new List<HrisLeaveRequestModel>();
    }
    public class ApproveLeaveRequestData
    {
        [JsonProperty("approveLeaveRequest")] public HrisLeaveRequestModel ApproveLeaveRequest { get; set; }
    }
    public class RejectLeaveRequestData
    {
        [JsonProperty("rejectLeaveRequest")] public HrisLeaveRequestModel RejectLeaveRequest { get; set; }
    }
}
