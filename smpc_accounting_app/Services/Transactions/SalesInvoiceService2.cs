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
    class SalesInvoiceService2 : ServiceBase<SalesInvoice2List>
    {
        public SalesInvoiceService2() : base(ApiEndPoints.SALES_INVOICE2) { }

        // CREATE
        public async Task<ApiResponseModel<object>> CreateSalesInvoiceRecord(SalesInvoice2Payload payload)
        {
            var response = await ApiService<ApiResponseModel<object>>.Post(ApiEndPoints.SALES_INVOICE2, new Dictionary<string, dynamic>
                {
                    { "sales_invoice2", payload.sales_invoice2 },
                    { "sales_invoice_details2", payload.sales_invoice_details2 }
                }
            );

            return response;
        }
    }
}
