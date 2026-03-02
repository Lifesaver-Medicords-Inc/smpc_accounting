using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smpc_accounting_app.Models
{
    public class InvoiceReceiptModel
    {
        public int id { get; set; }
        public string supplier { get; set; }
        public string supplier_code { get; set; }
        public int supplier_id { get; set; }
        public string payment_term { get; set; }
        public string tax_code { get; set; }
        public int tax_code_id { get; set; }
        public string invoice_due { get; set; }
        public string doc_no { get; set; }
        public string doc_date { get; set; }
        public string invoice_type { get; set; }
        public float net_amount { get; set; }
        public float twas_amount { get; set; }
        public bool? ap_voucher { get; set; }
        public string type { get; set; }
        public string remarks { get; set; }
        public string currency { get; set; }
        public string prepared_by { get; set; }
        public float other_charges { get; set; }
        public string reference_po { get; set; }  
    }

    public class InvoiceReceiptDetailsModel
    {
        public int id { get; set; }
        public int invoice_receipt_id { get; set; }
        public string item_code { get; set; }
        public string item_description { get; set; }
        public int req_qty { get; set; }
        public float unit_price { get; set; }
        public float total_cost { get; set; }
        public float discount { get; set; }
        public float line_amount { get; set; }
        public int purchase_order_details_id { get; set; }
    }

    public class InvoiceViewModel
    {
        public int invoice_receipt_id { get; set; }
        public string receipt_no { get; set; }
        public string ir_due_date { get; set; }
        public string ir_doc_date { get; set; }
        public float twas_amount { get; set; }
        public float line_amount { get; set; }
        public string receipt_type { get; set; }
    }

    public class InvoiceReceiptList
    {
        public List<InvoiceReceiptModel> invoice_receipt { get; set; }
        public List<InvoiceReceiptDetailsModel> invoice_receipt_details { get; set; }
    }

    public class InvoiceReceiptPayload
    {
        public InvoiceReceiptModel invoice_receipt { get; set; }
        public List<InvoiceReceiptDetailsModel> invoice_receipt_details { get; set; }
    }
}
