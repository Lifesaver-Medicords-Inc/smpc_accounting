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
    class ChartOfAccountsService : ServiceBase<ChartOfAccountsModel>
    {

        public ChartOfAccountsService() : base(ApiEndPoints.CHART_OF_ACCOUNT_SETUP) { }


        public async Task <List<ChartOfAccountsModel>> GetChartOfAccountsClassfication(string code)
        {
            var response = await ApiService<ApiResponseModel<List<ChartOfAccountsModel>>>.Get(ApiEndPoints.CHART_OF_ACCOUNT_CLASSIFCATION_SETUP + code);

            return response.data;
        }
    }
}
