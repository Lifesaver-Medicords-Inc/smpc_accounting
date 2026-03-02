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
    class PaymentVoucherService : ServiceBase<PaymentVoucherList>
    {
        public PaymentVoucherService() : base(ApiEndPoints.PAYMENT_VOUCHER) { }

        // CREATE
        public async Task<ApiResponseModel<object>> CreatePaymentVoucherRecord(PaymentVoucherPayload payload)
        {
            var response = await ApiService<ApiResponseModel<object>>.Post(ApiEndPoints.PAYMENT_VOUCHER, new Dictionary<string, dynamic>
                {
                    { "payment_voucher", payload.payment_voucher },
                    { "payment_voucher_details", payload.payment_voucher_details }
                }
            );

            return response;
        }
    }
}
