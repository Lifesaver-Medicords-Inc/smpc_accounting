using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smpc_accounting_app.Models
{
    class SaleInvoiceList
    {
       public List<SalesInvoice> sales_invoice { get; set; }
       public List<SalesInvoiceDetails> sales_invoice_details { get; set; }

    }
}
