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

namespace smpc_accounting_app.Pages.Setup.Others
{
    public partial class CurrencyPage : UserControl
    {

        CurrencyService currencyService = new CurrencyService();

        public CurrencyPage()
        {
            InitializeComponent();
        }

        private async void Currency_Load(object sender, EventArgs e)
        {
            await GetAll();
        }

        private void Toggle(bool isEditable = false)
        {
            pnl_content.Enabled = isEditable;
            btn_save.Visible = isEditable;
            btn_cancel.Visible = isEditable; 
            btn_new.Visible = !isEditable;
            btn_edit.Visible = !isEditable;
            btn_delete.Visible = !isEditable;
            btn_print.Visible = !isEditable;
        }

        private async Task GetAll()
        {
            var data = await currencyService.GetAsDatatable();

            bindingSource.DataSource = data;
        }

        private async void btn_save_Click(object sender, EventArgs e)
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
                await currencyService.Insert(data);
            }

            else
            {
                data["id"] = int.Parse(data["id"]);
                await currencyService.Update(data);
            }


            await GetAll();
            Toggle(false);
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Toggle(false);
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            Toggle(true);
            Helpers.ResetControls(pnl_content);
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            Toggle(true);
        }

        private async void btn_delete_Click(object sender, EventArgs e)
        {
            var data = Helpers.GetControlsValues(pnl_content);

            data["id"] = int.Parse(data["id"]);
            await currencyService.Delete(data);
            await GetAll();
        }
    }
}
