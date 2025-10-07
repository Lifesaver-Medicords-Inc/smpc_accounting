using smpc_accounting_app.Models;
using smpc_accounting_app.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smpc_accounting_app.Services.Accounting
{
    class SalesInvoiceService : ServiceBase<dynamic>
    {
        public SalesInvoiceService() : base(ApiEndPoints.SALES_INVOICE) { }
    }
}
