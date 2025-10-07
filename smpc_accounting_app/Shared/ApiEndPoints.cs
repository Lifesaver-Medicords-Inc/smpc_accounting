using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smpc_accounting_app.Shared
{
    public static class ApiEndPoints
    {

        public const string CHART_CLASS_SETUP = "/setup/chart_class";
        public const string CHART_GROUP_SETUP = "/setup/chart_group";
        public const string CHART_OF_ACCOUNT_SETUP = "/setup/chart_of_account";
        public const string CHART_OF_ACCOUNT_CLASSIFCATION_SETUP = "/setup/chart_of_account_classification/";
        public const string GENERAL_LEDGER_MAPPER_SETUP = "/setup/general_ledger";
        public const string BANK_SETUP = "/setup/bank";   
        public const string BOOK_SETUP = "/setup/book";
        public const string CURRENCY_SETUP = "/setup/currency";

        public const string PAYMENT_TERMS_SETUP = "/setup/payment_terms";

        //Tax Setup
        public const string TAX_SETUP = "/setup/tax";
        public const string TAX_CODE_SETUP = "/setup/tax_setup/";

        public const string EXPANDED_TAX_SETUP = "/setup/expanded_tax";
        public const string FINAL_TAX_SETUP = "/setup/final_tax";

        //Sales Invoice
        public const string SALES_ORDER_DR = "/sales/order_dr/";
        public const string SALES_INVOICE_DOC_NO = "/accounting/sales_invoice_doc_no";
        public const string SALES_INVOICE = "/accounting/sales_invoice";


    }
}