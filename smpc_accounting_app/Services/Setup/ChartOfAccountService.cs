using smpc_accounting_app.Models;
using smpc_accounting_app.Services.Helpers;
using smpc_accounting_app.Shared;
using smpc_sales_app.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smpc_accounting_app.Services.Setup
{
    class ChartOfAccountService : ServiceBase<ChartOfAccount>
    {
        public enum CHART_ACCOUNT_TYPE
        {
            ASSET = 100001,
            LIABILITY = 200001,
            CAPITAL = 300001,
            INCOME = 400001,
            EXPENSE = 500001
        };

        public ChartOfAccountService() : base(ApiEndPoints.CHART_OF_ACCOUNT_SETUP) { }

        public Func<string, DataTable, string> GenerateNewAccountCode = (string account_type, DataTable list_of_chart) =>
         {
             DataTable dv = list_of_chart;
             var data = dv.AsEnumerable()
                .Where(o => o.Field<string>("account_type") == account_type)
                .OrderByDescending(o => o.Field<int>("id"))
                .FirstOrDefault();

             int response = 0;
             if (data == null)
             {
                 switch (account_type)
                 {
                     case "ASSET":
                         response = (int)CHART_ACCOUNT_TYPE.ASSET;
                         break;
                     case "LIABILITY":
                         response = (int)CHART_ACCOUNT_TYPE.LIABILITY;
                         break;
                     case "CAPITAL":
                         response = (int)CHART_ACCOUNT_TYPE.CAPITAL;
                         break;
                     case "INCOME":
                         response = (int)CHART_ACCOUNT_TYPE.INCOME;
                         break;
                     case "EXPENSE":
                         response = (int)CHART_ACCOUNT_TYPE.EXPENSE;
                         break;
                 }
                 return response.ToString();
             }
             else
             {
                 return (int.Parse(data.Field<string>("account_code")) + 1).ToString();
             }
         };
        public Func<string, DataTable, string> GenerateNewSubsidiaryAccountCode = (string account_code, DataTable list_of_chart) =>
        {
            DataTable dv = list_of_chart;
            var data = dv.AsEnumerable()
                .Where(o => o.Field<string>("account_code")
                    .Contains(account_code.Split(':')[0].Trim())) 
               .OrderByDescending(o => o.Field<int>("id"))
               .FirstOrDefault();

            if (data.Field<string>("account_code").Contains('-'))
            {
                int count = 0;
                int dashCount = data.Field<string>("account_code")
                                    .Count(o => o == '-');
                if (dashCount >= 1)
                { 
                      count = int.Parse(data.Field<string>("account_code").ToString()) + 1;
                }
                else
                {
                      count = int.Parse(data.Field<string>("account_code").ToString()) + 1;
                }

                var new_account_code = data.Field<string>("account_code").ToString()  ;
                return new_account_code + " - " + count;
            }
            else
            { 
                return (int.Parse(data.Field<string>("account_code"))).ToString() + " - 01";
            }
        };

    public async Task<DataTable> GetAsDatatableForCombobox()
    {
        try
        {
            DataTable newTable = new DataTable();
            newTable.Columns.Add("id", typeof(string));
            newTable.Columns.Add("account_code", typeof(string));
            newTable.Columns.Add("account_name", typeof(string));

            var response = await ApiService<ApiResponseModel<List<ChartOfAccount>>>.Get(ApiEndPoints.CHART_OF_ACCOUNT_SETUP);

            DataTable data = JsonHelper.ToDataTable(response.data);

            // Use LINQ to select and transform rows
            var query = from row in data.AsEnumerable()
                        select new
                        {
                            id = row.Field<int>("id"),
                            account_code = row.Field<string>("account_code") + " - " + row.Field<string>("account_name"),
                            account_name = row.Field<string>("account_name"),
                        };

            // Add rows to the new DataTable
            int rowCount = 0, isFirstRow = 0;
            foreach (var item in query)
            {
                if (rowCount == isFirstRow)
                {
                    newTable.Rows.Add(0, "", "");
                    rowCount++;
                }
                newTable.Rows.Add(item.id, item.account_code, item.account_name);
            }

            return newTable;

        }
        catch (Exception ex)
        {
            var error = ex.Message;
            throw;
        }
    }
}


}