using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using smpc_accounting_app.Models;
using smpc_accounting_app.Services.Helpers;
using smpc_accounting_app.Shared;

namespace smpc_accounting_app.Services.Transactions
{
    class APVoucherService : ServiceBase<APVoucherList>
    {
        public APVoucherService() : base(ApiEndPoints.AP_VOUCHER) { }

        // CREATE
        public async Task<ApiResponseModel<object>> CreateAPVoucherRecord(APVoucherPayload payload)
        {
            var response = await ApiService<ApiResponseModel<object>>.Post(ApiEndPoints.AP_VOUCHER, new Dictionary<string, dynamic>
                {
                    { "ap_voucher", payload.ap_voucher },
                    { "ap_voucher_details", payload.ap_voucher_details }
                }
            );

            return response;
        }
    }
}
