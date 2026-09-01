using Newtonsoft.Json;
using System.Collections.Generic;

namespace smpc_accounting_app.Models.Hris
{
    // DTOs for the HRIS Benefits Administration module (GraphQL, camelCase).

    public class HrisBenefitPlanModel
    {
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("category")] public string Category { get; set; } // HMO / INSURANCE / ALLOWANCE / LOAN / OTHER
        [JsonProperty("description")] public string Description { get; set; }
        [JsonProperty("defaultEmployerShare")] public decimal DefaultEmployerShare { get; set; }
        [JsonProperty("defaultEmployeeShare")] public decimal DefaultEmployeeShare { get; set; }
        [JsonProperty("isActive")] public bool IsActive { get; set; }
    }

    public class HrisBenefitEnrollmentModel
    {
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("employeeId")] public int EmployeeId { get; set; }
        [JsonProperty("employee")] public HrisEmployeeModel Employee { get; set; }
        [JsonProperty("benefitPlanId")] public int BenefitPlanId { get; set; }
        [JsonProperty("plan")] public HrisBenefitPlanModel Plan { get; set; }
        [JsonProperty("status")] public string Status { get; set; } // ACTIVE / ENDED
        [JsonProperty("effectiveDate")] public string EffectiveDate { get; set; }
        [JsonProperty("endDate")] public string EndDate { get; set; }
        [JsonProperty("employerShare")] public decimal EmployerShare { get; set; }
        [JsonProperty("employeeShare")] public decimal EmployeeShare { get; set; }
        [JsonProperty("principalAmount")] public decimal PrincipalAmount { get; set; }
        [JsonProperty("balanceRemaining")] public decimal BalanceRemaining { get; set; }
        [JsonProperty("notes")] public string Notes { get; set; }
    }

    public class BenefitPlansData
    {
        [JsonProperty("benefitPlans")] public List<HrisBenefitPlanModel> BenefitPlans { get; set; } = new List<HrisBenefitPlanModel>();
    }
    public class CreateBenefitPlanData
    {
        [JsonProperty("createBenefitPlan")] public HrisBenefitPlanModel CreateBenefitPlan { get; set; }
    }
    public class UpdateBenefitPlanData
    {
        [JsonProperty("updateBenefitPlan")] public HrisBenefitPlanModel UpdateBenefitPlan { get; set; }
    }
    public class DeleteBenefitPlanData
    {
        [JsonProperty("deleteBenefitPlan")] public bool DeleteBenefitPlan { get; set; }
    }

    public class BenefitEnrollmentsData
    {
        [JsonProperty("benefitEnrollments")] public List<HrisBenefitEnrollmentModel> BenefitEnrollments { get; set; } = new List<HrisBenefitEnrollmentModel>();
    }
    public class CreateBenefitEnrollmentData
    {
        [JsonProperty("createBenefitEnrollment")] public HrisBenefitEnrollmentModel CreateBenefitEnrollment { get; set; }
    }
    public class UpdateBenefitEnrollmentData
    {
        [JsonProperty("updateBenefitEnrollment")] public HrisBenefitEnrollmentModel UpdateBenefitEnrollment { get; set; }
    }
    public class EndBenefitEnrollmentData
    {
        [JsonProperty("endBenefitEnrollment")] public HrisBenefitEnrollmentModel EndBenefitEnrollment { get; set; }
    }
}
