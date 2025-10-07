using smpc_accounting_app.Pages.Components;
using smpc_accounting_app.Services.Setup;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace smpc_accounting_app.Pages.Transactions
{
    public partial class APVoucher : UserControl
    {
        public APVoucher()
        {
            InitializeComponent();   
        }

        private void btn_supplier_Click(object sender, EventArgs e)
        {
            GetBpiModal modal = new GetBpiModal().GetModalName("List of Suppliers");
            modal.ShowDialog();
        }
    }
}
