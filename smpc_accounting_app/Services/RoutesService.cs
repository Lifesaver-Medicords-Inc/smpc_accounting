using smpc_accounting_app.Pages;
using smpc_accounting_app.Pages.Setup.Financial;
using smpc_accounting_app.Pages.Setup.Others;
using smpc_accounting_app.Pages.Setup.Tax;
using smpc_accounting_app.Pages.Transactions;
using smpc_accounting_app.Pages.Transactions.Journal;
using smpc_accounting_app.Pages.Transactions.Journal.JournalEntry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using smpc_accounting_app.Pages.Transactions.AccountsPayable;
using smpc_accounting_app.Pages.Transactions.AccountsPayable.InvoiceReceipt;
using smpc_accounting_app.Pages.Transactions.AccountsPayable.PaymentVoucher;
using smpc_accounting_app.Pages.Transactions.AccountsPayable.BulkInvoiceReceipt;
using smpc_accounting_app.Pages.Transactions.AccountsPayable.APVoucher;
using smpc_accounting_app.Pages.Transactions.AccountsReceivables;

namespace smpc_accounting_app.Services
{
    class RoutesService
    {
        private Dictionary<string, Control> _pages = new Dictionary<string, Control>()
        {
            //========================================================================
            // SETUP   
            { "Chart Class Setup", new ChartClassPage() },
            { "Bank Setup", new BankPage() },
            { "Book Setup", new BookPage() },
            { "Currency Setup", new CurrencyPage() },
            { "Expanded Tax Setup", new ExpandedTaxPage() },
            { "GL Mapper Setup", new GeneralLedgerMapperPage() },        
            {"Chart Of Accounts Setup", new ChartOfAccountsPage() },
            { "Tax Setup", new InputVatPage() },


            //========================================================================
            // TRANSACTIONS   
            {"Journal Voucher", new JournalVoucher() },
            {"Journal Entry", new JournalEntryPage() },
            {"Credit Memo", new CreditMemo() },
            {"Debit Memo", new DebitMemo() },
             
            //========================================================================
            // ACCOUNTS PAYABLES
            { "Payment Voucher", new PaymentVoucherPage2() },
            { "AP Voucher", new APVoucherPage() },
            { "Invoice Receipt", new InvoiceReceiptPage() },
            { "Bulk Invoice Receipt", new BulkInvoiceReceiptPage() },

            //========================================================================
            // ACCOUNTS RECEIVABLES
            {"Sales Invoice", new SalesInvoicePage()},
            {"Payment Receipt", new PaymentReceiptPage()},
        };

        private string _selectedRoute;
        public RoutesService(string selectedRoute)
        {
            this._selectedRoute = selectedRoute;
        }

        public Control GetForm()
        {
            return _pages.First(v => v.Key == this._selectedRoute).Value;
        }

        public String GetTitle()
        {
                return _pages.First(v => v.Key == this._selectedRoute).Key;
        }
    }
}
