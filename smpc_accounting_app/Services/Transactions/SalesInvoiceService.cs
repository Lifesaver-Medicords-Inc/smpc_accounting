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
    class SalesInvoiceService : ServiceBase<SalesInvoiceList>
    {
        public SalesInvoiceService() : base(ApiEndPoints.SALES_INVOICE) { }

        // CREATE
        public async Task<ApiResponseModel<object>> CreateSalesInvoiceRecord(SalesInvoicePayload payload)
        {
            var response = await ApiService<ApiResponseModel<object>>.Post(ApiEndPoints.SALES_INVOICE, new Dictionary<string, dynamic>
                {
                    { "sales_invoice", payload.sales_invoice },
                    { "sales_invoice_details", payload.sales_invoice_details }
                }
            );

            return response;
        }
    }
}
