using smpc_accounting_app.Models;
using smpc_accounting_app.Services.Helpers;
using smpc_accounting_app.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smpc_accounting_app.Services.Setup
{
    class ChartOfAccountsService : ServiceBase<ChartOfAccounts>
    {

        public ChartOfAccountsService() : base(ApiEndPoints.CHART_OF_ACCOUNT_SETUP) { }


        public async Task <List<ChartOfAccounts>> GetChartOfAccountsClassfication(string code)
        {
            var response = await ApiService<ApiResponseModel<List<ChartOfAccounts>>>.Get(ApiEndPoints.CHART_OF_ACCOUNT_CLASSIFCATION_SETUP + code);

            return response.data;
        }
    }
}
