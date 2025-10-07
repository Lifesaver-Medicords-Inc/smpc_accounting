using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smpc_accounting_app.Models
{
    class SalesInvoiceDetails
    {
        public int id { get; set; }
        public int sales_invoice_id { get; set; }
        public int order_details_id { get; set; }
        public int based_id { get; set; }
        public int item_id { get; set; }
        public string item_code { get; set; }
        public string item_desc { get; set; }
        public  int qty { get; set; }
        public decimal unit_price { get; set; }
        public string uom_name { get; set; }
        public decimal total_cost { get; set; }
        public decimal discount { get; set; }
        public string status { get; set; }
        public string delivery_date{ get; set; }
      
      public  SalesInvoiceDetails(int id, int sales_invoice_id, int order_details_id, int based_id, int item_id, string item_code, string item_desc, int qty, decimal unit_price, string uom_name, decimal total_cost, decimal discount, string status, string delivery_date)
        {
            this.id = id;
            this.sales_invoice_id = sales_invoice_id;
            this.order_details_id = order_details_id;
            this.based_id = based_id;
            this.item_id = item_id;
            this.item_code = item_code;
            this.item_desc = item_desc;
            this.qty = qty;
            this.unit_price = unit_price;
            this.uom_name = uom_name;
            this.total_cost = total_cost;
            this.discount = discount;
            this.status = status;
            this.delivery_date = delivery_date;

        }

    }
}
