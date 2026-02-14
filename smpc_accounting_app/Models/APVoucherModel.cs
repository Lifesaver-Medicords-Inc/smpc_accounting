using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smpc_accounting_app.Models
{
    public class APVoucherModel
    {
        public int id { get; set; }
        public string supplier { get; set; }
        public string supplier_code { get; set; }
        public int supplier_id { get; set; }
        public string currency { get; set; }
        public string tax_code { get; set; }
        public int tax_code_id { get; set; }
        public string doc_no { get; set; }
        public string doc_date { get; set; }
        public float transaction_amount { get; set; }
        public string prepared_by { get; set; }
    }

    public class APVoucherDetailsModel
    {
        public int id { get; set; }
        public int ap_voucher_id { get; set; }
        public int invoice_receipt_id { get; set; }
        public string receipt_no { get; set; }
        public string ir_doc_date { get; set; }
        public float line_amount { get; set; }
        public string receipt_type { get; set; }
    }

    public class APVoucherList
    {
        public List<APVoucherModel> ap_voucher { get; set; }
        public List<APVoucherDetailsModel> ap_voucher_details { get; set; }
    }

    public class APVoucherPayload
    {
        public APVoucherModel ap_voucher { get; set; }
        public List<APVoucherDetailsModel> ap_voucher_details { get; set; }
    }
}
