using smpc_accounting_app.Services.Helpers;
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

namespace smpc_accounting_app.Pages.Setup.Tax
{
    public partial class ExpandedTaxPage : UserControl
    { 
        readonly ExpandedTaxService chartClassServices = new ExpandedTaxService();

        public ExpandedTaxPage()
        {
            InitializeComponent();
        }

        private   void ExpandedTaxPage_Load(object sender, EventArgs e)
        {
            try
            {
                //await GetAllExpandedTax();

                //bindingSource.DataSource = await chartClassServices.GetAsDatatable();
            }
            catch (Exception)
            { 

                throw;
            }
        }  

        private void Toggle(bool isEditable = false)
        {
            try
            {
                pnl_content.Enabled = isEditable;
                btn_save.Visible = isEditable;
                btn_cancel.Visible = isEditable;

                btn_new.Visible = !isEditable;
                btn_edit.Visible = !isEditable;
                btn_delete.Visible = !isEditable;
                btn_print.Visible = !isEditable;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            try
            {
                Toggle(true);
                Helpers.ResetControls(pnl_content);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            try
            {
                Toggle(true);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {

        }

        private void btn_save_Click(object sender, EventArgs e)
        {

        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {

        }

        private void dgv_list_Click(object sender, EventArgs e)
        {

        }

        private void dgv_list_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            //    Panel[] pnl = new Panel[1];
            //    pnl[0] = pnl_content;
            //    DataTable dt = (DataTable)bindingSource.DataSource;

            //    Helpers.BindControls(pnl, (DataTable)bindingSource.DataSource, e.RowIndex);

            //    var cmb = dt.Rows[e.RowIndex]["type"].ToString();
            //    cmb_type.Text = cmb.Trim();
            //}
            //catch (Exception)
            //{

            //    throw;
            //}
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    bindingSource.Filter = $"Name LIKE '{txt_search.Text}%' and Code LIKE '{txt_search.Text}%'";
            //}
            //catch (Exception)
            //{

            //    throw;
            //}
        }

        private void dgv_list_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            //    Panel[] pnl = new Panel[1];
            //    pnl[0] = pnl_content;
            //    DataTable dt = (DataTable)bindingSource.DataSource;
            //}
            //catch (Exception)
            //{

            //    throw;
            //}
        }
    }
}
