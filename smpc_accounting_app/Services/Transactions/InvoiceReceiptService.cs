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
    class InvoiceReceiptService : ServiceBase<InvoiceReceiptList>
    {
        public InvoiceReceiptService() : base(ApiEndPoints.INVOICE_RECEIPT) { }

        // CREATE
        public async Task<object> CreateInvoiceReceiptRecord(InvoiceReceiptPayload payload)
        {
            var response = await ApiService<ApiResponseModel<object>>.Post(ApiEndPoints.INVOICE_RECEIPT, new Dictionary<string, dynamic>
                {
                    { "invoice_receipt", payload.invoice_receipt },
                    { "invoice_receipt_details", payload.invoice_receipt_details }
                }
            );

            return response.data;
        }

        // UPDATE
        public async Task<object> UpdateInvoiceReceiptRecord(InvoiceReceiptPayload payload)
        {
            var response = await ApiService<ApiResponseModel<object>>.Put(ApiEndPoints.INVOICE_RECEIPT, new Dictionary<string, dynamic>
                {
                    { "invoice_receipt", payload.invoice_receipt },
                    { "invoice_receipt_details", payload.invoice_receipt_details }
                }
            );

            return response.data;
        }

        // DELETE
        public async Task<object> DeleteInvoiceReceiptRecord(InvoiceReceiptPayload payload)
        {
            var response = await ApiService<ApiResponseModel<object>>.Delete(ApiEndPoints.INVOICE_RECEIPT, new Dictionary<string, dynamic>
                {
                    { "invoice_receipt", payload.invoice_receipt },
                    { "invoice_receipt_details", payload.invoice_receipt_details }
                }
            );

            return response.data;
        }
    }
}
