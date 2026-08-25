using System.Collections.Generic;

namespace smpc_accounting_app.Models
{
    // Mirrors ERP_API's models.CreditMemoContent (spec §5.18/§12.6) field for
    // field - snake_case to match the Go JSON tags directly, same convention as
    // every other accounting model in this project.
    public class CreditMemoModel
    {
        public int id { get; set; }
        public int doc_no { get; set; }

        public int partner_id { get; set; }
        public string partner_code { get; set; }
        public string partner_name { get; set; }

        // "Supplier" | "Customer" - never edited from the client; fixed by which
        // menu entry opened this screen (§14.98).
        public string partner_type { get; set; }

        public double trans_amount { get; set; }
        public string reason_code { get; set; }

        public string currency { get; set; }
        public string location_group { get; set; }
        public string doc_date { get; set; }
        public string sales_period { get; set; }

        // Customer side only.
        public int ref_srt_id { get; set; }
        public string ref_srt_no { get; set; }
        public int ref_si_id { get; set; }
        public string ref_si_no { get; set; }

        // Supplier side only.
        public bool? dm_refund { get; set; }
        public int ref_dm_id { get; set; }
        public string ref_dm_no { get; set; }

        // Customer side only - approval gate (§14.99).
        public bool is_approved { get; set; }
        public int approved_by_id { get; set; }
        public string approved_by_name { get; set; }
        public string approval_date { get; set; }
    }

    public class CreditMemoBody
    {
        public CreditMemoModel credit_memo { get; set; }
    }

    public class CreditMemoGetResponse
    {
        public List<CreditMemoModel> credit_memo { get; set; }
    }
}
