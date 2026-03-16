using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smpc_accounting_app.Models
{
    public class BulkInvoiceReceiptModel
    {
        public int id { get; set; }
        public string supplier { get; set; }
        public string supplier_code { get; set; }
        public int supplier_id { get; set; }
        public string supplier_address { get; set; }
        public string reference_doc { get; set; }
        public string payment_term { get; set; }
        public string currency { get; set; }
        public string tax_code { get; set; }
        public int tax_code_id { get; set; }
        public string invoice_due { get; set; }
        public int doc_no { get; set; }
        public string doc_date { get; set; }
        public string invoice_type { get; set; }
        public float other_charges { get; set; }
        public float net_amount { get; set; }
        public float twas_amount { get; set; }
        public bool? ap_voucher { get; set; }
        public string type { get; set; }
        public string remarks { get; set; }
        public string prepared_by { get; set; }
    }

    public class BulkInvoiceReceiptDetailsModel
    {
        public int id { get; set; }
        public int bulk_invoice_receipt_id { get; set; }
        public string payment_charge_code { get; set; }
        public string charge_description { get; set; }
        public int account_id { get; set; }
        public string account_code { get; set; }
        public float line_amount { get; set; }
    }

    public class BulkInvoiceReceiptList
    {
        public List<BulkInvoiceReceiptModel> bulk_invoice_receipt { get; set; }
        public List<BulkInvoiceReceiptDetailsModel> bulk_invoice_receipt_details { get; set; }
    }

    public class BulkInvoiceReceiptPayload
    {
        public BulkInvoiceReceiptModel bulk_invoice_receipt { get; set; }
        public List<BulkInvoiceReceiptDetailsModel> bulk_invoice_receipt_details { get; set; }
    }
}
