using System.Collections.Generic;

namespace smpc_accounting_app.Models
{
    // Petty Cash Replenishment (spec 5.26). An imprest fund: one cycle is one
    // request, and the next opens as a copy of the last with the balance carried
    // forward. ACCOUNTABILITY is computed from a counted CASH ON HAND, never the
    // other way round.
    public class PettyCashModel
    {
        public int id { get; set; }
        public int doc_no { get; set; }
        public string cut_off_date { get; set; }
        public double opening_fund { get; set; }
        public double accountable_fund { get; set; }
        public double cash_on_hand { get; set; }
        public double deductions { get; set; }
        public double total_expense { get; set; }
        public double encashment { get; set; }
        public double for_reimbursement { get; set; }
        public double accountability { get; set; }
        public string status { get; set; }          // DRAFT | SUBMITTED | APPROVED
        public string prepared_by { get; set; }
        public string approved_by { get; set; }
        public string approved_date { get; set; }
        public string remarks { get; set; }
        public List<PettyCashDetailModel> details { get; set; } = new List<PettyCashDetailModel>();
    }

    // One disbursement. PARTICULAR prints as "<requester>- <description>
    // <references>". A row with no reference is accepted: the form prompts, it
    // never blocks (spec 5.26).
    public class PettyCashDetailModel
    {
        public int id { get; set; }
        public int petty_cash_id { get; set; }
        public string date { get; set; }
        public string requester { get; set; }
        public string description { get; set; }
        public string reference { get; set; }
        public double amount { get; set; }
        public bool is_encashment { get; set; }
    }

    public class PettyCashApproveModel
    {
        public int id { get; set; }
        public string approved_by { get; set; }
        public string approved_date { get; set; }
    }
}
