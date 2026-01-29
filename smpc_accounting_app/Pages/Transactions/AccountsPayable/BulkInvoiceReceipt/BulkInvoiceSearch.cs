using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using smpc_accounting_app.Models;
using smpc_accounting_app.Services.Transactions;
using smpc_accounting_app.Services.Helpers;

namespace smpc_accounting_app.Pages.Transactions.AccountsPayable.BulkInvoiceReceipt
{
    public partial class BulkInvoiceSearch : Form
    {
        public string SelectedBIRId { get; private set; } = null;
        private string placeHolderText = "Bulk Invoice Receipt Search...";
        private BulkInvoiceReceiptList BulkInvoiceReceipt;
        readonly BulkInvoiceReceiptService BulkinvoiceReceiptService = new BulkInvoiceReceiptService();
        private DataTable birTable;
        public BulkInvoiceSearch()
        {
            InitializeComponent();
        }
    }
}
