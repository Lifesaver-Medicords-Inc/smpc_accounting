using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smpc_accounting_app.Models
{
    public class SalesOrderViewModel
    {
        public int id { get; set; }
        public int sales_order_id { get; set; }
        public string so_number { get; set; }
        public string doc_date { get; set; }
        public string customer_name { get; set; }
        public string sales_person { get; set; }
        public float total_sales { get; set; }
    }

    public class SalesOrderDetailsViewModel
    {
        public int sales_order_details_id { get; set; }
        public int sales_order_id { get; set; }
        public int item_id { get; set; }
        public string item_code { get; set; }
        public string item_description { get; set; }
        public string item_uom { get; set; }
        public int item_qty { get; set; }
        public float discount { get; set; }
        public float unit_price { get; set; }
        public float total_cost { get; set; }
        public string date_deliver { get; set; }
    }

    public class SalesOrderViewList
    {
        public List<SalesOrderViewModel> sales_order_view { get; set; }
        public List<SalesOrderDetailsViewModel> sales_order_details_view { get; set; }
    }
}
