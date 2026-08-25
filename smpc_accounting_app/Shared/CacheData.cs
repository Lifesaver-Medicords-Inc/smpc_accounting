using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using smpc_accounting_app.Models;

namespace smpc_accounting_app.Shared
{
    static class CacheData
    {
        //Setup
        public static DataTable ChartOfAccountClass { get; set; } = new DataTable();
        public static DataTable ChartOfAccountGroup { get; set; } = new DataTable();
        public static DataTable ChartOfAccountClassification { get; set; } = new DataTable();
        public static DataTable TaxClassification { get; set; } = new DataTable();


        public static DataTable PaymentTerms { get; set; } = new DataTable();

        // Login only ever delivered the auth token as a Set-Cookie header
        // (utils.CreateAuthToken on the Go side) - ApiService.cs never read it,
        // so every authenticated call after login failed with "Missing
        // authentication token" (RequireAuth). Mirrors
        // smpc_inventory_app's RequestToApi.cs, the app where this already
        // works correctly.
        public static string SessionToken { get; set; } = "";

        public static CurrentUserModel CurrentUser { get; set; } = null;
        public static CompanySetupModel CompanySetup { get; set; } = null;
        public static JournalEntryModel CurrentJournal { get; set; } = null;
        public static ExchangeRateModel CurrencyRate { get; set; } = null;
    }
}
