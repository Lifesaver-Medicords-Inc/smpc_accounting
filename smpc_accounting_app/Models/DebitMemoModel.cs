using System.Collections.Generic;

namespace smpc_accounting_app.Models
{
    // Mirrors ERP_API's models.DebitMemoContent (spec §5.19/§12.6) field for
    // field - snake_case to match the Go JSON tags directly, same convention
    // as CreditMemoModel.cs. A/P only - SupplierId/Code/Name, never
    // "customer" (the spec explicitly flags the old mockup's CUSTOMER label
    // as the error, not the data - see DebitMemo.Designer.cs's label fix).
    public class DebitMemoModel
    {
        public int id { get; set; }
        public int doc_no { get; set; }

        public int supplier_id { get; set; }
        public string supplier_code { get; set; }
        public string supplier_name { get; set; }

        public double trans_amount { get; set; }

        // Required (§14.58). Fixed 5-value list, same as CreditMemoModel's
        // reason_code (pur return / adj twas / cancel chq / pur disc / exp
        // cancel) - not in §17 despite §17 being authoritative; kept fixed
        // per the spec text, not promoted to Setup on our own authority.
        public string reason_code { get; set; }

        public string currency { get; set; }
        public string location_group { get; set; }
        public string doc_date { get; set; }
        public string sales_period { get; set; }
        public string ref_doc_no { get; set; }
        public string ref_po_no { get; set; }

        // Computed client-side before every save (trans_amount minus the sum
        // of every ticked line's amount_applied) - §14.43 requires this to
        // be 0 before CreateDebitMemo will accept the request.
        public double unapplied_amount { get; set; }

        public List<DebitMemoDetailsModel> debit_memo_details { get; set; }
    }

    // Mirrors models.DebitMemoDetailsContent - the apply table (spec §5.19).
    // target_doc_type is one of "Invoice Receipt" / "Bulk Invoice Receipt" /
    // "Credit Memo" - never "Miscellaneous Receiving" (out of scope, §15).
    public class DebitMemoDetailsModel
    {
        public int id { get; set; }
        public int debit_memo_id { get; set; }

        public bool apply { get; set; }
        public string target_doc_type { get; set; }
        public int target_doc_id { get; set; }
        public string target_doc_no { get; set; }
        public string due_date { get; set; }
        public double total { get; set; }
        public double open_amount { get; set; }
        public double amount_applied { get; set; }
        public double balance { get; set; }
    }

    public class DebitMemoBody
    {
        public DebitMemoModel debit_memo { get; set; }
        public List<DebitMemoDetailsModel> debit_memo_details { get; set; }
    }

    public class DebitMemoGetResponse
    {
        public List<DebitMemoModel> debit_memo { get; set; }
        public List<DebitMemoDetailsModel> debit_memo_details { get; set; }
    }
}
