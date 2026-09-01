using Newtonsoft.Json;
using System.Collections.Generic;

namespace smpc_accounting_app.Models.Hris
{
    // DTOs for the HRIS Holiday Calendar (GraphQL, camelCase).

    public class HrisHolidayModel
    {
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("holidayDate")] public string HolidayDate { get; set; }
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("type")] public string Type { get; set; }
    }

    public class HrisHolidaySetupModel
    {
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("type")] public string Type { get; set; }
        [JsonProperty("rule")] public string Rule { get; set; }
        [JsonProperty("month")] public int Month { get; set; }
        [JsonProperty("day")] public int Day { get; set; }
        [JsonProperty("isActive")] public bool IsActive { get; set; }
    }

    public class HrisGenerateYearResultModel
    {
        [JsonProperty("created")] public int Created { get; set; }
        [JsonProperty("skipped")] public int Skipped { get; set; }
        [JsonProperty("notes")] public string Notes { get; set; }
    }

    public class HolidaySetupsData
    {
        [JsonProperty("holidaySetups")] public List<HrisHolidaySetupModel> HolidaySetups { get; set; } = new List<HrisHolidaySetupModel>();
    }
    public class CreateHolidaySetupData
    {
        [JsonProperty("createHolidaySetup")] public HrisHolidaySetupModel CreateHolidaySetup { get; set; }
    }
    public class UpdateHolidaySetupData
    {
        [JsonProperty("updateHolidaySetup")] public HrisHolidaySetupModel UpdateHolidaySetup { get; set; }
    }
    public class DeleteHolidaySetupData
    {
        [JsonProperty("deleteHolidaySetup")] public bool DeleteHolidaySetup { get; set; }
    }
    public class GenerateHolidayYearData
    {
        [JsonProperty("generateHolidayYear")] public HrisGenerateYearResultModel GenerateHolidayYear { get; set; }
    }

    public class HolidaysData
    {
        [JsonProperty("holidays")] public List<HrisHolidayModel> Holidays { get; set; } = new List<HrisHolidayModel>();
    }
    public class CreateHolidayData
    {
        [JsonProperty("createHoliday")] public HrisHolidayModel CreateHoliday { get; set; }
    }
    public class UpdateHolidayData
    {
        [JsonProperty("updateHoliday")] public HrisHolidayModel UpdateHoliday { get; set; }
    }
    public class DeleteHolidayData
    {
        [JsonProperty("deleteHoliday")] public bool DeleteHoliday { get; set; }
    }
}
