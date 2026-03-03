using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smpc_accounting_app.Models
{
    public class SalesInvoiceReceiptView
    {
        public int sales_invoice_id { get; set; }
        public string doc_no { get; set; }
        public string doc_date { get; set; }
        public string due_date { get; set; }
        public float twas_applied { get; set; }
        public float open_amount { get; set; }
    }
}
