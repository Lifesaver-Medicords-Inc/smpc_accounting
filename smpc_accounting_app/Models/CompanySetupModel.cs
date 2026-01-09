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
    }
}
