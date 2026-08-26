using System.Collections.Generic;
using System.Threading.Tasks;
using smpc_accounting_app.Models;
using smpc_accounting_app.Services.Helpers;
using smpc_accounting_app.Shared;

namespace smpc_accounting_app.Services.Setup
{
    // Phase 3 item 3.4. Company Setup (§4.5.6) always operates on the one real
    // company record (id=1, confirmed against the live DB) - there's no create/
    // delete flow here, just fetch it and let an authorized user edit it. Not a
    // ServiceBase<T> like most setup services here, since Get and Update genuinely
    // hit two different endpoints (the existing read-only COMPANY_SETUP fetch vs.
    // the full-CRUD COMPANIES group's PUT), which ServiceBase's single fixed url
    // can't express.
    class CompanySetupService
    {
        public async Task<CompanySetupModel> Get()
        {
            var response = await ApiService<ApiResponseModel<CompanySetupModel>>.Get(ApiEndPoints.COMPANY_SETUP);
            return response?.data;
        }

        public async Task<ApiResponseModel> Update(CompanySetupModel company)
        {
            var data = new Dictionary<string, dynamic>
            {
                { "company_code", company.company_code },
                { "company_name", company.company_name },
                { "legal_name", company.legal_name },
                { "trade_name", company.trade_name },
                { "business_type", company.business_type },
                { "sec_registration_no", company.sec_registration_no },
                { "dti_registration_no", company.dti_registration_no },
                { "tin", company.tin },
                { "bir_branch_code", company.bir_branch_code },
                { "rdo_code", company.rdo_code },
                { "industry", company.industry },
                { "status", company.status },
                { "is_head_office", company.is_head_office },
                { "currency_code", company.currency_code },
                { "beg_bal", company.beg_bal },
                { "monthly_rate", company.monthly_rate },
                { "markup_multiplier_price", company.markup_multiplier_price },
                { "start_fiscal_date", company.start_fiscal_date },
                { "end_fiscal_date", company.end_fiscal_date },
                { "inclusions_quotation_terms", company.inclusions_quotation_terms },
                { "exclusions_quotation_terms", company.exclusions_quotation_terms },
                { "term_and_conditions", company.term_and_conditions },
            };

            return await ApiService<ApiResponseModel>.Put(ApiEndPoints.COMPANIES + "/1", data);
        }
    }
}
