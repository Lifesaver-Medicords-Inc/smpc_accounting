using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smpc_accounting_app.Models
{
    public class PaymentReceiptModel
    {
        public int id { get; set; }
        public string customer { get; set; }
        public string customer_code { get; set; }
        public int customer_id { get; set; }
        public string reference_cr_no { get; set; }
        public string date_collect { get; set; }
        public float transaction_amount { get; set; }
        public float unapplied_amount { get; set; }
        public float overpayment_amount { get; set; }
        public string reference_or_no { get; set; }
        public string currency { get; set; }
        public float cash_amount { get; set; }
        public string doc_no { get; set; }
        public string doc_date { get; set; }
        public string bank_code { get; set; }
        public string bank_branch { get; set; }
        public string check_no { get; set; }
        public string check_type { get; set; }
        public string check_date { get; set; }
        public float check_amount { get; set; }
        public string prepared_by { get; set; }
    }

    public class PaymentReceiptDetailsModel
    {
        public int id { get; set; }
        public int payment_receipt_id { get; set; }
        public int sales_invoice_id { get; set; }
        public int sales_invoice_details_id { get; set; }
        public string doc_no { get; set; }
        public string doc_date { get; set; }
        public string due_date { get; set; }
        public float open_amount { get; set; }
        public float amount_applied { get; set; }
        public float twas_applied { get; set; }
        public float balance { get; set; }
        public bool  isSelected { get; set; }
    }

    public class PaymentReceiptList
    {
        public List<PaymentReceiptModel> payment_receipt { get; set; }
        public List<PaymentReceiptDetailsModel> payment_receipt_details { get; set; }
    }

    public class PaymentReceiptPayload
    {
        public PaymentReceiptModel payment_receipt { get; set; }
        public List<PaymentReceiptDetailsModel> payment_receipt_details { get; set; }
    }
}
