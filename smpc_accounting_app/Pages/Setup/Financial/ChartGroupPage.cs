using smpc_accounting_app.Services.Helpers;
using smpc_accounting_app.Services.Setup;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace smpc_accounting_app.Pages.Setup.Financial
{
    public partial class ChartGroupPage : UserControl
    { 
        readonly ChartGroupService chartGroupServices = new ChartGroupService();

        public ChartGroupPage()
        {
            InitializeComponent();
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

        private async void btn_delete_Click(object sender, EventArgs e)
        {
            try
            {
                var data = Helpers.GetControlsValues(pnl_content);

                data["id"] = int.Parse(data["id"]);

                await chartGroupServices.Delete(data);

                await GetAll();

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

                if (bindingSource.DataSource is DataTable chartGroups)
                {
                    var list = chartGroups.Copy();

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
                    await chartGroupServices.Insert(data);
                }
                else
                {
                    data["id"] = int.Parse(data["id"]);
                    await chartGroupServices.Update(data);
                }


                await GetAll();
                Toggle(false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async void ChartGroupPage_Load(object sender, EventArgs e)
        {
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
                var data = await chartGroupServices.GetAsDatatable();

                bindingSource.DataSource = data;
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

        private void dgv_list_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
