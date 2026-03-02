using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smpc_accounting_app.Models
{
    public class SupplierTradeViewModel
    {
        public int supplier_id { get; set; }
        public string supplier { get; set; }
        public string supplier_code { get; set; }
        public string payment_term { get; set; }
        public string invoice_type { get; set; }
        public string type { get; set; }
        public float overpayment_amount { get; set; }
    }
}
