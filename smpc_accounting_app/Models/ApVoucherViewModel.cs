using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smpc_accounting_app.Models
{
    public class ApVoucherViewModel
    {
        public int ap_voucher_id { get; set; }
        public string supplier { get; set; }
        public string supplier_code { get; set; }
        public string currency { get; set; }
        public string doc_no { get; set; }
        public string doc_date { get; set; }
        public float transaction_amount { get; set; }
    }

    public class ApVoucherDetailsViewModel
    {
        public int ap_voucher_details_id { get; set; }
        public int ap_voucher_id { get; set; }
        public string doc_no { get; set; }
        public string due_date { get; set; }
        public float trans_amount { get; set; }
        public float twas_applied { get; set; }
        public float open_amount { get; set; }
    }

    public class ApVoucherViewList
    {
        public List<ApVoucherViewModel> ap_voucher_view { get; set; }
        public List<ApVoucherDetailsViewModel> ap_voucher_details_view { get; set; }
    }
}
