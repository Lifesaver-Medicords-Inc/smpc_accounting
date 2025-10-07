using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smpc_accounting_app.Models
{
    class SalesInvoice
    {
        public int id { get; set; }
        public int customer_id { get; set; }
        public int tax_id { get; set; }
        public string customer_name { get; set; }
        public string customer_code { get; set; }
        public int payment_terms_id { get; set; }
        public string tax_code { get; set; }
        public string journal_name { get; set; }
        public int journal_id { get; set; }
        public string address { get; set; }
        public string sales_id { get; set; }
        public string sales_executive { get; set; }
        public string currency_code { get; set; }
        public int base_rate { get; set; }
        public decimal exchange_rate { get; set; }
        public string doc_no { get; set; }
        public string doc_date { get; set; }
        public string posting_date { get; set; }
        public string reference_dr_no { get; set; }
        public string doc_so_no { get; set; }
        public string reference_po { get; set; }
        public decimal vatable_sales { get; set; }
        public decimal vatable_sales_fc { get; set; }
        public decimal vat_exempt_sales { get; set; }
        public decimal vat_exempt_sales_fc { get; set; }
        public decimal zero_rated_sales { get; set; }
        public decimal zero_rated_sales_fc { get; set; }
        public decimal total_sales_vat_inclusive { get; set; }
        public decimal total_sales_vat_inclusive_fc { get; set; }
        public decimal less_vat { get; set; }
        public decimal less_vat_fc { get; set; }
        public decimal amount_net_vat { get; set; }
        public decimal amount_net_vat_fc { get; set; }
        public decimal less_pwd_discount { get; set; }
        public decimal less_pwd_discount_fc { get; set; }
        public decimal amount_due { get; set; }
        public decimal amount_due_fc { get; set; }
        public decimal add_vat { get; set; }
        public decimal add_vat_fc { get; set; }
        public decimal total_amount_due { get; set; }
        public decimal total_amount_due_fc { get; set; }
    }
}
