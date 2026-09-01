using smpc_accounting_app.Models.Hris;
using smpc_accounting_app.Services.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace smpc_accounting_app.Services.Hris
{
    // Typed operations for the HRIS Holiday Calendar. Same contract as the
    // other Hris services: check HasErrors, never an HTTP status.
    static class HrisHolidayService
    {
        private const string HolidayFields = "id holidayDate name type";

        public static Task<GraphQLResponse<HolidaysData>> GetHolidaysAsync(int year)
        {
            string query = "query($year: Int!) { holidays(year: $year) { " + HolidayFields + " } }";
            return HrisApiService.ExecuteAsync<HolidaysData>(query, new Dictionary<string, object> { { "year", year } });
        }

        public static Task<GraphQLResponse<CreateHolidayData>> CreateAsync(string date, string name, string type)
        {
            string query = "mutation($input: HolidayInput!) { createHoliday(input: $input) { " + HolidayFields + " } }";
            return HrisApiService.ExecuteAsync<CreateHolidayData>(query, new Dictionary<string, object>
            {
                { "input", new Dictionary<string, object> { { "holidayDate", date }, { "name", name }, { "type", type } } }
            });
        }

        public static Task<GraphQLResponse<UpdateHolidayData>> UpdateAsync(int id, string date, string name, string type)
        {
            string query = "mutation($id: ID!, $input: HolidayInput!) { updateHoliday(id: $id, input: $input) { " + HolidayFields + " } }";
            return HrisApiService.ExecuteAsync<UpdateHolidayData>(query, new Dictionary<string, object>
            {
                { "id", id },
                { "input", new Dictionary<string, object> { { "holidayDate", date }, { "name", name }, { "type", type } } }
            });
        }

        private const string SetupFields = "id name type rule month day isActive";

        public static Task<GraphQLResponse<HolidaySetupsData>> GetSetupsAsync()
        {
            return HrisApiService.ExecuteAsync<HolidaySetupsData>("{ holidaySetups { " + SetupFields + " } }");
        }

        private static Dictionary<string, object> SetupInput(string name, string type, string rule, int month, int day, bool isActive)
        {
            return new Dictionary<string, object>
            {
                { "name", name }, { "type", type }, { "rule", rule },
                { "month", month }, { "day", day }, { "isActive", isActive },
            };
        }

        public static Task<GraphQLResponse<CreateHolidaySetupData>> CreateSetupAsync(string name, string type, string rule, int month, int day, bool isActive)
        {
            string query = "mutation($input: HolidaySetupInput!) { createHolidaySetup(input: $input) { " + SetupFields + " } }";
            return HrisApiService.ExecuteAsync<CreateHolidaySetupData>(query, new Dictionary<string, object> { { "input", SetupInput(name, type, rule, month, day, isActive) } });
        }

        public static Task<GraphQLResponse<UpdateHolidaySetupData>> UpdateSetupAsync(int id, string name, string type, string rule, int month, int day, bool isActive)
        {
            string query = "mutation($id: ID!, $input: HolidaySetupInput!) { updateHolidaySetup(id: $id, input: $input) { " + SetupFields + " } }";
            return HrisApiService.ExecuteAsync<UpdateHolidaySetupData>(query, new Dictionary<string, object> { { "id", id }, { "input", SetupInput(name, type, rule, month, day, isActive) } });
        }

        public static Task<GraphQLResponse<DeleteHolidaySetupData>> DeleteSetupAsync(int id)
        {
            string query = "mutation($id: ID!) { deleteHolidaySetup(id: $id) }";
            return HrisApiService.ExecuteAsync<DeleteHolidaySetupData>(query, new Dictionary<string, object> { { "id", id } });
        }

        public static Task<GraphQLResponse<GenerateHolidayYearData>> GenerateYearAsync(int year)
        {
            string query = "mutation($year: Int!) { generateHolidayYear(year: $year) { created skipped notes } }";
            return HrisApiService.ExecuteAsync<GenerateHolidayYearData>(query, new Dictionary<string, object> { { "year", year } });
        }

        public static Task<GraphQLResponse<DeleteHolidayData>> DeleteAsync(int id)
        {
            string query = "mutation($id: ID!) { deleteHoliday(id: $id) }";
            return HrisApiService.ExecuteAsync<DeleteHolidayData>(query, new Dictionary<string, object> { { "id", id } });
        }
    }
}
