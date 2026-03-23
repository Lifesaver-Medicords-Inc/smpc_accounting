using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smpc_accounting_app.Shared
{
    public static class ApiEndPoints
    {

        public const string CHART_CLASS_SETUP = "/accounting/chart_class";
        public const string CHART_GROUP_SETUP = "/setup/chart_group";
        public const string CHART_OF_ACCOUNT_SETUP = "/accounting/chart_of_account";
        public const string CHART_OF_ACCOUNT_VIEW = "/accounting/tax/coa";
        public const string CHART_OF_ACCOUNT_CLASSIFCATION_SETUP = "/setup/chart_of_account_classification/";
        public const string GENERAL_LEDGER_MAPPER_SETUP = "/setup/general_ledger";
        public const string BANK_SETUP = "/setup/bank";   
        public const string BOOK_SETUP = "/setup/book";
        public const string CURRENCY_SETUP = "/setup/currency";

        public const string COMPANY_SETUP = "/accounting/company_setup";
        public const string CURRENT_JOURNAL = "/accounting/current_journal";
        public const string CURRENCY_RATE = "/accounting/exchange_rate/";

        public const string PAYMENT_TERMS_SETUP = "/setup/payment_terms";

        //Tax Setup
        public const string TAX_SETUP = "/accounting/tax";
        public const string TAX_CODE_SETUP = "/setup/tax_setup/";

        public const string EXPANDED_TAX_SETUP = "/setup/expanded_tax";
        public const string FINAL_TAX_SETUP = "/setup/final_tax";

        //Sales Invoice
        public const string SALES_ORDER_DR = "/sales/order_dr/";
        public const string SALES_INVOICE_DOC_NO = "/accounting/sales_invoice_doc_no";
        public const string SALES_INVOICE = "/accounting/sales_invoice";

        //Transactions
        public const string JOURNAL_ENTRY = "/accounting/journal_entry2";
        public const string INVOICE_RECEIPT = "/accounting/invoice_receipt";
        public const string SALES_INVOICE2 = "/accounting/sales_invoice2";
        public const string AP_VOUCHER = "/accounting/ap_voucher";
        public const string PAYMENT_VOUCHER = "/accounting/payment_voucher";
        public const string PAYMENT_RECEIPT = "/accounting/payment_receipt";
        public const string AP_VOUCHER_INVOICE = "/accounting/ap_voucher/invoice/";
        public const string BULK_INVOICE_RECEIPT = "/accounting/bulk_invoice_receipt";
        public const string BULK_INVOICE_RECEIPT_SEARCH = "/accounting/bulk_invoice_receipt/search";
        public const string INVOICE_RECEIPT_TAX = "/accounting/invoice_receipt/tax_view";
        public const string SALES_INVOICE_RECEIPT = "/accounting/payment_receipt/sales_invoice/";
        public const string INVOICE_RECEIPT_SUPPLIER = "/accounting/invoice_receipt/supplier_trade";
        public const string CUSTOMER_VIEW = "/accounting/customer";
        public const string AP_VOUCHER_PAYMENT = "/accounting/payment_voucher/ap_voucher/";
        public const string SALES_INVOICE_SO = "/accounting/customer_so/";
        public const string INVOICE_RECEIPT_PO = "/accounting/invoice_receipt/supplier_po/";
    }
}