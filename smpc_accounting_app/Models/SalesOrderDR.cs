using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smpc_accounting_app.Models
{
    class SalesOrderDR
    {
        public int order_id { get; set; }
        public string document_no { get; set; }
      //  public string project_name { get; set; }
        public string company_name { get; set; }
        public string sales_executive { get; set; }

    }
    
    class SalesOrderWithDeliverReceipt 
    {
        public int order_id { get; set; }
        public int customer_id { get; set; }
        public int payment_terms_id  { get; set; }
        public int ref_po { get; set; }
        public string customer_name { get; set; }
        public string customer_code { get; set; }
        public string tax_code { get; set; }
        public int tax_id { get; set; }
        public string address { get; set; }
        public string doc_so_no { get; set; }
        public string doc_date { get; set; }
        public decimal net_amount { get; set; }
        public string sales_executive { get; set; }
    }

    class SalesOrderDrDetails
    {
        public int order_details_id { get; set; }
        public int based_id { get; set; }
        public int item_id { get; set; }
        public string item_code { get; set; }
        public string item_desc { get; set; }
        public int qty { get; set; }
        public decimal unit_price { get; set; }
        public string uom_name { get; set; }
        public decimal total_cost { get; set; }
        public decimal discount { get; set; }
        public string status { get; set; }
        public string delivery_date { get; set; }

    }
}
