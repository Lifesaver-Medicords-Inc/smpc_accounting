using smpc_accounting_app.Pages;
using smpc_accounting_app.Pages.Setup.Financial;
using smpc_accounting_app.Pages.Setup.Others;
using smpc_accounting_app.Pages.Setup.Tax;
using smpc_accounting_app.Pages.Transactions;
using smpc_accounting_app.Pages.Transactions.Journal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace smpc_accounting_app.Services
{
    class RoutesService
    {
        private Dictionary<string, Control> _pages = new Dictionary<string, Control>()
        {
            //========================================================================
            // SETUP   
            { "Chart Class Setup", new ChartClassPage() },
            { "Chart Group Setup", new ChartGroupPage() },
            { "Chart Of Account Setup", new ChartOfAccountPage() },
            { "Bank Setup", new BankPage() },
            { "Book Setup", new BookPage() },
            { "Currency Setup", new CurrencyPage() },
            { "Expanded Tax Setup", new ExpandedTaxPage() },
            { "GL Mapper Setup", new GeneralLedgerMapperPage() },
            { "Invoice Receipt", new InvoiceReceipt() },
            {"Chart Account Setup", new ChartOfAccounts() },


            //========================================================================
            // TRANSACTIONS   
            {"Journal Voucher", new JournalVoucher() },
            {"Credit Memo", new CreditMemo() },
            {"Debit Memo", new DebitMemo() },
             
            //========================================================================
            // TRANSACTIONS 
            //{ "AP Voucher", new APVoucher() },
            {"Sales Invoice", new SalesInvoice()},
            { "Input Vat Setup", new InputVatPage() }, 
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
