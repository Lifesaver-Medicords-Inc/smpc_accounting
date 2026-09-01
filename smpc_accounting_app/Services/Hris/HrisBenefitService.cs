using smpc_accounting_app.Models.Hris;
using smpc_accounting_app.Services.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace smpc_accounting_app.Services.Hris
{
    // Typed operations for HRIS Benefits Administration (plan catalog +
    // employee enrollments). Same contract as the other Hris services: check
    // HasErrors, never an HTTP status.
    static class HrisBenefitService
    {
        private const string PlanFields = @"
            id name category description defaultEmployerShare defaultEmployeeShare isActive";

        private const string EnrollmentFields = @"
            id employeeId employee { id employeeNo firstName middleName lastName department }
            benefitPlanId plan { " + PlanFields + @" }
            status effectiveDate endDate employerShare employeeShare principalAmount balanceRemaining notes";

        // ── Plans ────────────────────────────────────────────────────────

        public static Task<GraphQLResponse<BenefitPlansData>> GetPlansAsync(bool activeOnly = false)
        {
            string query = "query($activeOnly: Boolean) { benefitPlans(activeOnly: $activeOnly) { " + PlanFields + " } }";
            return HrisApiService.ExecuteAsync<BenefitPlansData>(query, new Dictionary<string, object> { { "activeOnly", activeOnly } });
        }

        public static Task<GraphQLResponse<CreateBenefitPlanData>> CreatePlanAsync(Dictionary<string, object> input)
        {
            string query = "mutation($input: BenefitPlanInput!) { createBenefitPlan(input: $input) { " + PlanFields + " } }";
            return HrisApiService.ExecuteAsync<CreateBenefitPlanData>(query, new Dictionary<string, object> { { "input", input } });
        }

        public static Task<GraphQLResponse<UpdateBenefitPlanData>> UpdatePlanAsync(int id, Dictionary<string, object> input)
        {
            string query = "mutation($id: ID!, $input: BenefitPlanInput!) { updateBenefitPlan(id: $id, input: $input) { " + PlanFields + " } }";
            return HrisApiService.ExecuteAsync<UpdateBenefitPlanData>(query, new Dictionary<string, object> { { "id", id }, { "input", input } });
        }

        public static Task<GraphQLResponse<DeleteBenefitPlanData>> DeletePlanAsync(int id)
        {
            string query = "mutation($id: ID!) { deleteBenefitPlan(id: $id) }";
            return HrisApiService.ExecuteAsync<DeleteBenefitPlanData>(query, new Dictionary<string, object> { { "id", id } });
        }

        // ── Enrollments ──────────────────────────────────────────────────

        public static Task<GraphQLResponse<BenefitEnrollmentsData>> GetEnrollmentsAsync(int? employeeId = null, string status = null)
        {
            var filter = new Dictionary<string, object>();
            if (employeeId.HasValue) filter["employeeId"] = employeeId.Value;
            if (!string.IsNullOrEmpty(status) && status != "ALL") filter["status"] = status;
            string query = filter.Count == 0
                ? "{ benefitEnrollments { " + EnrollmentFields + " } }"
                : "query($filter: BenefitEnrollmentFilter) { benefitEnrollments(filter: $filter) { " + EnrollmentFields + " } }";
            object variables = filter.Count == 0 ? null : new Dictionary<string, object> { { "filter", filter } };
            return HrisApiService.ExecuteAsync<BenefitEnrollmentsData>(query, variables);
        }

        public static Task<GraphQLResponse<CreateBenefitEnrollmentData>> CreateEnrollmentAsync(Dictionary<string, object> input)
        {
            string query = "mutation($input: BenefitEnrollmentInput!) { createBenefitEnrollment(input: $input) { " + EnrollmentFields + " } }";
            return HrisApiService.ExecuteAsync<CreateBenefitEnrollmentData>(query, new Dictionary<string, object> { { "input", input } });
        }

        public static Task<GraphQLResponse<UpdateBenefitEnrollmentData>> UpdateEnrollmentAsync(int id, Dictionary<string, object> input)
        {
            string query = "mutation($id: ID!, $input: BenefitEnrollmentInput!) { updateBenefitEnrollment(id: $id, input: $input) { " + EnrollmentFields + " } }";
            return HrisApiService.ExecuteAsync<UpdateBenefitEnrollmentData>(query, new Dictionary<string, object> { { "id", id }, { "input", input } });
        }

        public static Task<GraphQLResponse<EndBenefitEnrollmentData>> EndEnrollmentAsync(int id, string endDate)
        {
            string query = "mutation($id: ID!, $endDate: String) { endBenefitEnrollment(id: $id, endDate: $endDate) { " + EnrollmentFields + " } }";
            return HrisApiService.ExecuteAsync<EndBenefitEnrollmentData>(query, new Dictionary<string, object> { { "id", id }, { "endDate", endDate } });
        }
    }
}
