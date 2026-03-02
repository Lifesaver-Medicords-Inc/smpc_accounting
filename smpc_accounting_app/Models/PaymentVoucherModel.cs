using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smpc_accounting_app.Models
{
    public class PaymentVoucherModel
    {
        public int id { get; set; }
        public string supplier { get; set; }
        public string supplier_code { get; set; }
        public int supplier_id { get; set; }
        public string reference_apv { get; set; }
        public string currency { get; set; }
        public float transaction_amount { get; set; }
        public float unapplied_amount { get; set; }
        public string doc_no { get; set; }
        public string doc_date { get; set; }
        public string remarks { get; set; }
        public float cash_amount { get; set; }
        public float check_amount { get; set; }
        public string check_bank { get; set; }
        public string check_account_no { get; set; }
        public string ref_check_no { get; set; }
        public string check_date { get; set; }
        public float transfer_amount { get; set; }
        public string transfer_type { get; set; }
        public string transfer_bank { get; set; }
        public string transfer_account_no { get; set; }
        public string ref_doc_no { get; set; }
        public string ref_doc_date { get; set; }
        public string prepared_by { get; set; }
        public float overpayment_amount { get; set; }
    }

    public class PaymentVoucherDetailsModel
    {
        public int id { get; set; }
        public int payment_voucher_id { get; set; }
        public int ap_voucher_details_id { get; set; }
        public string doc_no { get; set; }
        public string due_date { get; set; }
        public float trans_amount { get; set; }
        public float open_amount { get; set; }
        public float amount_applied { get; set; }
        public float twas_applied { get; set; }
        public float balance { get; set; }
        public string receipt_type { get; set; }
    }

    public class PaymentVoucherList
    {
        public List<PaymentVoucherModel> payment_voucher { get; set; }
        public List<PaymentVoucherDetailsModel> payment_voucher_details { get; set; }
    }

    public class PaymentVoucherPayload
    {
        public PaymentVoucherModel payment_voucher { get; set; }
        public List<PaymentVoucherDetailsModel> payment_voucher_details { get; set; }
    }
}
