using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smpc_accounting_app.Models
{
    public class SalesInvoice2Model
    {
        public int id { get; set; }
        public string customer { get; set; }
        public string customer_code { get; set; }
        public int customer_id { get; set; }
        public string journal { get; set; }
        public string payment_term { get; set; }
        public string customer_address { get; set; }
        public string tax_code { get; set; }
        public float base_rate { get; set; }
        public string currency { get; set; }
        public float exchange_rate { get; set; }
        public string doc_no { get; set; }
        public string doc_date { get; set; }
        public string posting_date { get; set; }
        public string reference_doc_dr { get; set; }
        public string reference_doc_so { get; set; }
        public string reference_doc_po { get; set; }
        public string sales_person { get; set; }
        public string prepared_by { get; set; }
        public float vat_sales { get; set; }
        public float vat_exempt_sales { get; set; }
        public float zero_sales { get; set; }
        public float total_sales { get; set; }
        public float less_vat { get; set; }
        public float net_vat { get; set; }
        public float pwd_discount { get; set; }
        public float amount_due { get; set; }
        public float add_vat { get; set; }
        public float total_amount_due { get; set; }
    }

    public class SalesInvoiceDetails2Model
    {
        public int id { get; set; }
        public int sales_invoice_id { get; set; }
        public int sales_order_details_id { get; set; }
        public int delivery_receipt_details_id { get; set; }
        public int item_id { get; set; }
        public string item_code { get; set; }
        public string item_description { get; set; }
        public int item_qty { get; set; }
        public string item_uom { get; set; }
        public float unit_price { get; set; }
        public float discount { get; set; }
        public float total_cost { get; set; }
        public string date_deliver { get; set; }
    }

    public class SalesInvoice2List
    {
        public List<SalesInvoice2Model> sales_invoice2 { get; set; }
        public List<SalesInvoiceDetails2Model> sales_invoice_details2 { get; set; }
    }

    public class SalesInvoice2Payload
    {
        public SalesInvoice2Model sales_invoice2 { get; set; }
        public List<SalesInvoiceDetails2Model> sales_invoice_details2 { get; set; }
    }
}
