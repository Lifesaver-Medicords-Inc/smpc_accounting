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
    class BulkInvoiceReceiptService : ServiceBase<BulkInvoiceReceiptList>
    {
        public BulkInvoiceReceiptService() : base(ApiEndPoints.BULK_INVOICE_RECEIPT) { }

        // CREATE
        public async Task<ApiResponseModel<object>> CreateBulkInvoiceReceiptRecord(BulkInvoiceReceiptPayload payload)
        {
            var response = await ApiService<ApiResponseModel<object>>.Post(ApiEndPoints.BULK_INVOICE_RECEIPT, new Dictionary<string, dynamic>
                {
                    { "bulk_invoice_receipt", payload.bulk_invoice_receipt },
                    { "bulk_invoice_receipt_details", payload.bulk_invoice_receipt_details }
                }
            );

            return response;
        }
    }
}
