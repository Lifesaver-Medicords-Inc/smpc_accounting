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
    class TaxSetupService : ServiceBase<TaxSetup>
    {
        public TaxSetupService() : base(ApiEndPoints.TAX_SETUP) {}
    }

    class TaxClassificationService : ServiceBase<TaxClassification>
    {
        public TaxClassificationService() : base(ApiEndPoints.TAX_CODE_SETUP) {}

        public async Task<List<TaxClassification>> GetTaxClassfication(string code)
        {
            var response = await ApiService<ApiResponseModel<List<TaxClassification>>>.Get(ApiEndPoints.TAX_CODE_SETUP + code);

            return response.data;
        }
    }
}
