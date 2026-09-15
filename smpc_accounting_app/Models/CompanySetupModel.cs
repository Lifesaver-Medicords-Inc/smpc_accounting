using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smpc_accounting_app.Models
{
    public class CompanySetupModel
    {
        public int id { get; set; }
        public string company_name { get; set; }
        public string company_code { get; set; }
        public string legal_name { get; set; }
        public string trade_name { get; set; }
        public string business_type { get; set; }
        public string sec_registration_no { get; set; }
        public string dti_registration_no { get; set; }
        public string tin { get; set; }
        public string bir_branch_code { get; set; }
        public string rdo_code { get; set; }
        public string industry { get; set; }
        public string status { get; set; }
        public bool? is_head_office { get; set; }
        public float beg_bal { get; set; }
        public float monthly_rate { get; set; }
        public string currency_code { get; set; }
        public float markup_multiplier_price { get; set; }
        public string start_fiscal_date { get; set; }
        public string end_fiscal_date { get; set; }
        public string inclusions_quotation_terms { get; set; }
        public string exclusions_quotation_terms { get; set; }
        public string term_and_conditions { get; set; }
        // Sales_Quotation_Bug_Report_2026-08-03.md #18 - whole-number percentage
        // (12 means 12%), matching how VAT is written throughout the spec
        // ("VAT (12%)"). Consumers divide by 100 rather than storing the raw
        // decimal fraction.
        public float vat_rate_percent { get; set; }

        // Spec 4.5.6: the restocking and cancellation fee percentages are live
        // defaults, not just contract wording. They autofill the Sales Order's
        // charges modal (5.4) and the Sales Return's credit computation (12.6.2),
        // and stay overridable at the point of charge.
        //
        // Whole-number percentages, same convention as vat_rate_percent: 10 means
        // 10%. Zero is a real value meaning "no fee" - 8.15 says a 0 fee produces
        // no invoice line - so nothing may substitute a default for an explicit 0.
        public float restocking_fee_percent { get; set; }
        public float cancellation_fee_percent { get; set; }
    }
}
