using System.Collections.Generic;

namespace smpc_accounting_app.Models
{
    // One row of the A/R Record of Transactions (spec 12.3): one per Sales
    // Invoice, with the balance derived from what Payment Receipts applied to it.
    public class ARRecordModel
    {
        public int sales_invoice_id { get; set; }
        public string tag { get; set; }            // PAID | ONGOING
        public string customer { get; set; }
        public string customer_code { get; set; }
        public string sales_invoice_no { get; set; }
        public string doc_date { get; set; }
        public string so_no { get; set; }
        public string payment_term { get; set; }
        public double total_amount_due { get; set; }
        public double paid { get; set; }
        public double balance { get; set; }
        public string next_due { get; set; }
    }

    // One row of the A/P Record of Transactions, the mirror of ARRecordModel.
    //
    // Read-only throughout. INVOICE DUE is typed on the Invoice Receipt and
    // carried forward unchanged (5.14), so unlike A/R's NEXT DUE there is nothing
    // to edit from this screen - editing it here would let a due date diverge from
    // the document every downstream voucher reads it off.
    public class APRecordModel
    {
        public int invoice_receipt_id { get; set; }
        public string tag { get; set; }            // PAID | ONGOING
        public string supplier { get; set; }
        public string supplier_code { get; set; }
        public string invoice_receipt_no { get; set; }
        public string receipt_type { get; set; }   // INVOICE RECEIPT | BULK INVOICE RECEIPT
        public string doc_date { get; set; }
        public string reference_po { get; set; }
        public string payment_term { get; set; }
        public double net_amount { get; set; }
        public double applied { get; set; }
        public double balance { get; set; }
        public string invoice_due { get; set; }
    }

    // NEXT DUE is typed by A/R (spec 12.3) - nothing converts a payment term
    // into a date.
    public class ARNextDueModel
    {
        public int sales_invoice_id { get; set; }
        public string next_due { get; set; }
    }

    // One row of the Billing list (spec 12.5), keyed on the SO.
    public class BillingRowModel
    {
        public string so_no { get; set; }
        public string date { get; set; }
        public string project_name { get; set; }
        public string sales_executive { get; set; }
        public string customer { get; set; }
        public double total_due { get; set; }
        public double adjustments { get; set; }
        public double paid { get; set; }
        public double balance { get; set; }
        public string next_due { get; set; }
        public string status { get; set; }         // ACTIVE | CLOSED
        public List<BillingReceiptModel> receipts { get; set; }
    }

    // An Official Receipt recorded against the SO's invoices (spec 5.22).
    public class BillingReceiptModel
    {
        public string so_no { get; set; }
        public string or_no { get; set; }
        public string date { get; set; }
        public double amount { get; set; }
        public int doc_no { get; set; }
    }

    // A row of the SO's hand-keyed billing ledger (spec 12.4). BEGINING BALANCE
    // and ADDITIONAL are positive; CLIENT PAY is negative.
    public class SoBillingTransactionModel
    {
        public int id { get; set; }
        public string so_no { get; set; }
        public string date { get; set; }
        public string transaction_type { get; set; }
        public string description { get; set; }
        public double amount { get; set; }
        public string reference { get; set; }
        public string remarks { get; set; }
        public string created_by { get; set; }
    }
}
