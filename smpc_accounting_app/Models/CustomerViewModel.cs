using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smpc_accounting_app.Models
{
    public class CustomerViewModel
    {
        public int customer_id { get; set; }
        public string customer { get; set; }
        public string customer_code { get; set; }
        public string payment_term { get; set; }
        public string tax_code { get; set; }
        public string tax { get; set; }
        public string tin { get; set; }
        public string customer_address { get; set; }
        public float overpayment_amount { get; set; }
    }
}
