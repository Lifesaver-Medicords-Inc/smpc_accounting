using smpc_accounting_app.Pages;
using smpc_accounting_app.Pages.Setup;
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
using smpc_accounting_app.Pages.Transactions.AccountsReceivables.SalesInvoice;
using smpc_accounting_app.Pages.Transactions.AccountsReceivables.PaymentReceipt;

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
            { "GL Mapper Setup", new GeneralLedgerMapperPage() },        
            { "Tax Setup", new InputVatPage() },
            {"Chart Of Accounts Setup", new ChartOfAccountsPage() },
            // Phase 3 items 3.2-3.4. Output VAT/Final Tax are the same generic §4.5.3
            // Tax Code Setup screen InputVatPage already is (that page shows every
            // code unfiltered - "Input VAT" is just its current menu label) narrowed
            // to a real subset: Output VAT to codes with a Sales COA assigned, Final
            // Tax to the single "FINAL-TAX" code (confirmed with the user: just
            // another Tax Code Setup entry, not a separate concept).
            { "Output VAT Setup", new OutputVatPage() },
            { "Final Tax Setup", new FinalTaxPage() },
            { "Company Setup", new CompanySetupPage() },
            // PP&E register - not in the spec, see accounting_asset_category_model.go.
            { "Asset Category Setup", new AssetCategoryPage() },
            { "Fixed Asset Setup", new FixedAssetPage() },

            //========================================================================
            // TRANSACTIONS
            // "Journal Voucher" retired (Phase 3 item 3.1) - it was a permanent 20-line
            // shell with no sidebar node of its own (unreachable in the UI either way).
            // JournalEntryPage already implements everything §4.5.5 asks for (header +
            // editable debit/credit lines, full New/Edit/Save/Delete/Print/Search)
            // against the same complete API - confirmed with the user rather than
            // building a second copy of the same feature.
            {"Journal Entry", new JournalEntryPage() },

            //========================================================================
            // ACCOUNTS PAYABLES
            { "Payment Voucher", new PaymentVoucherPage() },
            { "AP Voucher", new APVoucherPage() },
            { "Invoice Receipt", new InvoiceReceiptPage() },
            { "Bulk Invoice Receipt", new BulkInvoiceReceiptPage() },
            // Sec5.18: direction is fixed by which menu entry constructs this -
            // never a choice made inside the form itself (Sec14.98).
            { "Credit Memo", new CreditMemo("Supplier") },
            { "Debit Memo", new DebitMemo() },

            //========================================================================
            // ACCOUNTS RECEIVABLES
            {"Sales Invoice", new SalesInvoicePage()},
            {"Payment Receipt", new PaymentReceiptPage()},
            {"Customer Credit Memo", new CreditMemo("Customer") },
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
