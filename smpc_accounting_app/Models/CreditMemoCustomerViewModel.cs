namespace smpc_accounting_app.Models
{
    // Backs the Customer Credit Memo partner picker (vw_get_credit_memo_customer).
    // Distinct from CustomerViewModel on purpose - the id means something
    // different here:
    //
    //   CustomerViewModel.customer_id        = tbl_bpi.id         (parent company)
    //   CreditMemoCustomerViewModel.partner_id = tbl_bpi_general.id (branch)
    //
    // Credit Memo's own server-side guard verifies the partner holds the "CUS"
    // entity type, and that membership table keys on the branch id - so sending
    // the parent id failed every customer Credit Memo with "partner <n> is not
    // registered as a Customer" even for correctly registered customers.
    public class CreditMemoCustomerViewModel
    {
        public int partner_id { get; set; }
        public int parent_bpi_id { get; set; }
        public string customer { get; set; }
        public string customer_code { get; set; }
        public string payment_term { get; set; }
        public string tax_code { get; set; }
        public string customer_address { get; set; }
        public string tin { get; set; }
    }
}
