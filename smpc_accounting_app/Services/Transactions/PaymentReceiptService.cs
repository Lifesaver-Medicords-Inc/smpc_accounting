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
    class PaymentReceiptService : ServiceBase<PaymentReceiptList>
    {
        public PaymentReceiptService() : base(ApiEndPoints.PAYMENT_RECEIPT) { }

        // CREATE
        public async Task<ApiResponseModel<object>> CreatePaymentReceiptRecord(PaymentReceiptPayload payload)
        {
            var response = await ApiService<ApiResponseModel<object>>.Post(ApiEndPoints.PAYMENT_RECEIPT, new Dictionary<string, dynamic>
                {
                    { "payment_receipt", payload.payment_receipt },
                    { "payment_receipt_details", payload.payment_receipt_details }
                }
            );

            return response;
        }
    }
}
