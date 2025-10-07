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

namespace smpc_accounting_app.Pages.Setup.Financial
{
    public partial class ChartClassPage : UserControl
    {
        readonly ChartClassService chartClassServices = new ChartClassService();
        public ChartClassPage()
        {
            try
            {

                InitializeComponent();

                bindingSource.DataSource = chartClassServices.GetAsDatatable();

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

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            try
            {
                Toggle(false);
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

        private async void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                var data = Helpers.GetControlsValues(pnl_content);

                if (bindingSource.DataSource is DataTable chartClasses)
                {
                    var list = chartClasses.Copy();

                    //TEMPORARY CODE CONTAINER 

                    Boolean isEmpty = data.TryGetValue("code", out dynamic _code);

                    if (!isEmpty) return;

                    var isCodeExist = list.AsEnumerable().Where(datas => "'" + datas.Field<string>("code").ToString() + "'" == _code).ToList();

                    if (isCodeExist.Count > 0 && String.IsNullOrEmpty(txt_id.Text))
                    {

                        Helpers.ShowDialogMessage("error", "Code is already exist!");

                        return;

                    }
                }

                if (String.IsNullOrEmpty(txt_id.Text))
                {
                    data.Remove("id");
                    await chartClassServices.Insert(data);
                }
                else
                {
                    data["id"] = int.Parse(data["id"]);
                    await chartClassServices.Update(data);
                }

                await GetAll();
                Toggle(false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async void btn_delete_Click(object sender, EventArgs e)
        {
            try
            {
                var data = Helpers.GetControlsValues(pnl_content);
                data["id"] = int.Parse(data["id"]);

                await chartClassServices.Delete(data);

                bindingSource.DataSource = await chartClassServices.GetAsDatatable();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async void ChartClassPage_Load(object sender, EventArgs e)
        {

            //cmb_type.DataSource = Enum.GetValues(typeof(ChartType)); 
            //cmb_type.ValueMember = "value";
            //cmb_type.DisplayMember = "title";

            try
            {
                await GetAll();
            }
            catch (Exception)
            {

                throw;
            }

        }

        private async Task GetAll()
        {
            try
            {
                bindingSource.DataSource = await chartClassServices.GetAsDatatable();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void dgv_list_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Panel[] pnl = new Panel[1];
                pnl[0] = pnl_content;
                DataTable dt = (DataTable)bindingSource.DataSource;

                Helpers.BindControls(pnl, (DataTable)bindingSource.DataSource, e.RowIndex);

                var cmb = dt.Rows[e.RowIndex]["type"].ToString();
                cmb_type.Text = cmb.Trim();
            }
            catch (Exception)
            {

                throw;
            }
            //cmb_type.SelectedIndex = e.RowIndex;
            //cmb_type.SelectedValue = dt.Rows[e.RowIndex]["type"].ToString();
            //cmb_type.SelectedText = dt.Rows[e.RowIndex]["type"].ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                bindingSource.Filter = $"Name LIKE '{txt_search.Text}%' and Code LIKE '{txt_search.Text}%'";
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void dgv_list_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgv_list_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Panel[] pnl = new Panel[1];
                pnl[0] = pnl_content;
                DataTable dt = (DataTable)bindingSource.DataSource;
            }
            catch (Exception)
            {

                throw;
            } 
        }
    }
}
