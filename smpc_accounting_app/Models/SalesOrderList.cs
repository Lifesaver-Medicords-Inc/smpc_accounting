using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smpc_accounting_app.Models
{
    class SalesOrderList 
    {
        public List<SalesOrderWithDeliverReceipt> orders {get;set; }
        public List<SalesOrderDrDetails> order_details { get; set; }
    }
}
