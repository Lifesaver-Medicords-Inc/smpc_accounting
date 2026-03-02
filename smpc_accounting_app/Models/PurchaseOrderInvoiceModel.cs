using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smpc_accounting_app.Models
{
    public class PurchaseOrderInvoiceModel
    {
        public int id { get; set; }
        public string po_number { get; set; }
        public string doc_date { get; set; }
        public string supplier_name { get; set; }
        public float total_amount_po { get; set; }
    }

    public class PurchaseOrderDetailsInvoiceModel
    {
        public int purchase_order_details_id { get; set; }
        public int based_id { get; set; }
        public string item_code { get; set; }
        public string item_description { get; set; }
        public string order_uom { get; set; }
        public int order_qty { get; set; }
        public string req_uom { get; set; }
        public int req_qty { get; set; }
        public float unit_price { get; set; }
        public float total_cost { get; set; }
    }

    public class PurchaseOrderInvoiceList
    {
        public List<PurchaseOrderInvoiceModel> purchase_order_view { get; set; }
        public List<PurchaseOrderDetailsInvoiceModel> purchase_order_details_view { get; set; }
    }
}
