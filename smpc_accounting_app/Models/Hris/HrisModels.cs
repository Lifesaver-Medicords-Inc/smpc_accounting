using Newtonsoft.Json;
using System.Collections.Generic;

namespace smpc_accounting_app.Models.Hris
{
    // DTOs for the HRIS GraphQL API. Property names mirror the GraphQL schema
    // (camelCase), unlike the ERP REST models (snake_case).

    public class HrisEmployeeModel
    {
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("employeeNo")] public string EmployeeNo { get; set; }
        [JsonProperty("userId")] public int? UserId { get; set; }
        [JsonProperty("user")] public HrisErpUserModel User { get; set; }
        [JsonProperty("firstName")] public string FirstName { get; set; }
        [JsonProperty("middleName")] public string MiddleName { get; set; }
        [JsonProperty("lastName")] public string LastName { get; set; }
        [JsonProperty("suffix")] public string Suffix { get; set; }
        [JsonProperty("birthDate")] public string BirthDate { get; set; }
        [JsonProperty("gender")] public string Gender { get; set; }
        [JsonProperty("civilStatus")] public string CivilStatus { get; set; }
        [JsonProperty("department")] public string Department { get; set; }
        [JsonProperty("positionId")] public int? PositionId { get; set; }
        [JsonProperty("position")] public HrisPositionModel Position { get; set; }
        [JsonProperty("employmentStatus")] public string EmploymentStatus { get; set; }
        [JsonProperty("scheduleType")] public string ScheduleType { get; set; }
        [JsonProperty("payFrequency")] public string PayFrequency { get; set; }
        [JsonProperty("workStartTime")] public string WorkStartTime { get; set; }
        [JsonProperty("workEndTime")] public string WorkEndTime { get; set; }
        [JsonProperty("hireDate")] public string HireDate { get; set; }
        [JsonProperty("regularizationDate")] public string RegularizationDate { get; set; }
        [JsonProperty("endDate")] public string EndDate { get; set; }
        [JsonProperty("isActive")] public bool IsActive { get; set; }
        [JsonProperty("sssNo")] public string SssNo { get; set; }
        [JsonProperty("philhealthNo")] public string PhilhealthNo { get; set; }
        [JsonProperty("pagibigNo")] public string PagibigNo { get; set; }
        [JsonProperty("tin")] public string Tin { get; set; }
        [JsonProperty("taxStatus")] public string TaxStatus { get; set; }
        [JsonProperty("mobileNo")] public string MobileNo { get; set; }
        [JsonProperty("email")] public string Email { get; set; }
        [JsonProperty("addresses")] public List<HrisAddressModel> Addresses { get; set; } = new List<HrisAddressModel>();
        [JsonProperty("compensations")] public List<HrisCompensationModel> Compensations { get; set; } = new List<HrisCompensationModel>();
        [JsonProperty("allowances")] public List<HrisAllowanceModel> Allowances { get; set; } = new List<HrisAllowanceModel>();
        [JsonProperty("emergencyContacts")] public List<HrisEmergencyContactModel> EmergencyContacts { get; set; } = new List<HrisEmergencyContactModel>();
        [JsonProperty("dependents")] public List<HrisDependentModel> Dependents { get; set; } = new List<HrisDependentModel>();
        [JsonProperty("educations")] public List<HrisEducationModel> Educations { get; set; } = new List<HrisEducationModel>();
        [JsonProperty("workHistories")] public List<HrisWorkHistoryModel> WorkHistories { get; set; } = new List<HrisWorkHistoryModel>();
        [JsonProperty("files")] public List<HrisEmployeeFileModel> Files { get; set; } = new List<HrisEmployeeFileModel>();
    }

    public class HrisAddressModel
    {
        [JsonProperty("addressType")] public string AddressType { get; set; }
        [JsonProperty("unitNo")] public string UnitNo { get; set; }
        [JsonProperty("streetName")] public string StreetName { get; set; }
        [JsonProperty("barangay")] public string Barangay { get; set; }
        [JsonProperty("city")] public string City { get; set; }
        [JsonProperty("province")] public string Province { get; set; }
        [JsonProperty("region")] public string Region { get; set; }
        [JsonProperty("country")] public string Country { get; set; }
        [JsonProperty("postalCode")] public string PostalCode { get; set; }
    }

    public class HrisCompensationModel
    {
        [JsonProperty("basicPay")] public decimal BasicPay { get; set; }
        [JsonProperty("rateType")] public string RateType { get; set; }
        [JsonProperty("bankName")] public string BankName { get; set; }
        [JsonProperty("bankAccountNo")] public string BankAccountNo { get; set; }
        [JsonProperty("effectiveDate")] public string EffectiveDate { get; set; }
        [JsonProperty("isCurrent")] public bool IsCurrent { get; set; }
    }

    public class HrisAllowanceModel
    {
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("amount")] public decimal Amount { get; set; }
        [JsonProperty("isTaxable")] public bool IsTaxable { get; set; }
        [JsonProperty("effectiveDate")] public string EffectiveDate { get; set; }
    }

    public class HrisEmergencyContactModel
    {
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("relationship")] public string Relationship { get; set; }
        [JsonProperty("contactNo")] public string ContactNo { get; set; }
        [JsonProperty("address")] public string Address { get; set; }
    }

    public class HrisDependentModel
    {
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("relationship")] public string Relationship { get; set; }
        [JsonProperty("birthDate")] public string BirthDate { get; set; }
    }

    public class HrisEducationModel
    {
        [JsonProperty("level")] public string Level { get; set; }
        [JsonProperty("school")] public string School { get; set; }
        [JsonProperty("course")] public string Course { get; set; }
        [JsonProperty("yearFrom")] public int? YearFrom { get; set; }
        [JsonProperty("yearTo")] public int? YearTo { get; set; }
    }

    public class HrisWorkHistoryModel
    {
        [JsonProperty("employer")] public string Employer { get; set; }
        [JsonProperty("position")] public string Position { get; set; }
        [JsonProperty("dateFrom")] public string DateFrom { get; set; }
        [JsonProperty("dateTo")] public string DateTo { get; set; }
        [JsonProperty("reasonForLeaving")] public string ReasonForLeaving { get; set; }
    }

    public class HrisEmployeeFileModel
    {
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("fileName")] public string FileName { get; set; }
        [JsonProperty("originalName")] public string OriginalName { get; set; }
        [JsonProperty("type")] public string Type { get; set; }
        [JsonProperty("size")] public int Size { get; set; }
        [JsonProperty("category")] public string Category { get; set; }
    }

    public class HrisPositionModel
    {
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("code")] public string Code { get; set; }
        [JsonProperty("name")] public string Name { get; set; }
    }

    public class HrisErpUserModel
    {
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("employeeId")] public string EmployeeId { get; set; }
        [JsonProperty("firstName")] public string FirstName { get; set; }
        [JsonProperty("lastName")] public string LastName { get; set; }
        [JsonProperty("department")] public string Department { get; set; }
    }

    public class HrisEmployeePageModel
    {
        [JsonProperty("items")] public List<HrisEmployeeModel> Items { get; set; } = new List<HrisEmployeeModel>();
        [JsonProperty("totalCount")] public int TotalCount { get; set; }
    }

    // Wrappers matching each operation's response shape ({"data":{"employees":...}}).
    public class EmployeesData
    {
        [JsonProperty("employees")] public HrisEmployeePageModel Employees { get; set; }
    }
    public class EmployeeData
    {
        [JsonProperty("employee")] public HrisEmployeeModel Employee { get; set; }
    }
    public class CreateEmployeeData
    {
        [JsonProperty("createEmployee")] public HrisEmployeeModel CreateEmployee { get; set; }
    }
    public class UpdateEmployeeData
    {
        [JsonProperty("updateEmployee")] public HrisEmployeeModel UpdateEmployee { get; set; }
    }
    public class SetEmployeeStatusData
    {
        [JsonProperty("setEmployeeStatus")] public HrisEmployeeModel SetEmployeeStatus { get; set; }
    }
    public class LinkUserAccountData
    {
        [JsonProperty("linkUserAccount")] public HrisEmployeeModel LinkUserAccount { get; set; }
    }
    public class AddEmployeeFileData
    {
        [JsonProperty("addEmployeeFile")] public HrisEmployeeFileModel AddEmployeeFile { get; set; }
    }
    public class DeleteEmployeeFileData
    {
        [JsonProperty("deleteEmployeeFile")] public bool DeleteEmployeeFile { get; set; }
    }
    public class PositionsData
    {
        [JsonProperty("positions")] public List<HrisPositionModel> Positions { get; set; }
    }
    public class ErpUsersData
    {
        [JsonProperty("erpUsers")] public List<HrisErpUserModel> ErpUsers { get; set; }
    }
}
