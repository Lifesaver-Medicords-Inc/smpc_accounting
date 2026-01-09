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
    class TaxSetupService : ServiceBase<TaxList>
    {
        public TaxSetupService() : base(ApiEndPoints.TAX_SETUP) {}

        // CREATE
        public async Task<object> CreateTaxRecord(TaxSetupPayload payload)
        {
            var response = await ApiService<ApiResponseModel<object>>.Post(ApiEndPoints.TAX_SETUP, new Dictionary<string, dynamic>
                {
                    { "tax_setup", payload.tax_setup },
                    { "tax_setup_details", payload.tax_setup_details }
                }
            );

            return response.data;
        }

        // UPDATE
        public async Task<object> UpdateTaxRecord(TaxSetupPayload payload)
        {
            var response = await ApiService<ApiResponseModel<object>>.Put(ApiEndPoints.TAX_SETUP, new Dictionary<string, dynamic>
                {
                    { "tax_setup", payload.tax_setup },
                    { "tax_setup_details", payload.tax_setup_details }
                }
            );

            return response.data;
        }

        // DELETE
        public async Task<object> DeleteTaxRecord(TaxSetupPayload payload)
        {
            var response = await ApiService<ApiResponseModel<object>>.Delete(ApiEndPoints.TAX_SETUP, new Dictionary<string, dynamic>
                {
                    { "tax_setup", payload.tax_setup },
                    { "tax_setup_details", payload.tax_setup_details }
                }
            );

            return response.data;
        }
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
