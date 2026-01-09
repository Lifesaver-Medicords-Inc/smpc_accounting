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
        public static CurrentUserModel CurrentUser { get; set; } = null;
        public static CompanySetupModel CompanySetup { get; set; } = null;
    }
}
