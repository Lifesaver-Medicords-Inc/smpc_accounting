using smpc_accounting_app.Services;
using smpc_accounting_app.Services.Helpers;
using smpc_accounting_app.Services.Setup;
using smpc_accounting_app.Shared;
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
    public partial class ChartOfAccounts : UserControl
    {

        ChartOfAccountsService chartOfAccountService = new ChartOfAccountsService();
        GeneralSetupService serviceSetup;

        public ChartOfAccounts()
        {
            InitializeComponent();
        }


        private void ChartOfAccounts_Load(object sender, EventArgs e)
        {
            FetchChartOfAccountClass();
            FetchChartsOfAccountGroup();
            FetchChartOfAccount();

        }

        private void BtnToggle(bool isEditable = false)
        {
            pnl_content.Enabled = isEditable;
            btn_save.Visible = isEditable;
            btn_cancel.Visible = isEditable;
            btn_new.Visible = !isEditable;
            btn_edit.Visible = !isEditable;
            btn_delete.Visible = !isEditable;
            btn_print.Visible = !isEditable;
        }

        private async void FetchChartOfAccount()
        {
            var responseData = await chartOfAccountService.GetAsDatatable();
            dataBindingChartAccount.DataSource = responseData;
        }
        private async void FetchChartOfAccountClass()
        {
            serviceSetup = new GeneralSetupService(ApiEndPoints.CHART_CLASS_SETUP);
            CacheData.ChartOfAccountClass = await serviceSetup.GetAsDatatable();

            DataRow newRow = CacheData.ChartOfAccountClass.NewRow();
            newRow["id"] = 0;
            newRow["name"] = "-- Select --";

            CacheData.ChartOfAccountClass.Rows.InsertAt(newRow, 0);
            cmb_class.DataSource = CacheData.ChartOfAccountClass;
            cmb_class.ValueMember = "id";
            cmb_class.DisplayMember = "name";
        }
        private async void FetchChartsOfAccountGroup()
        {
            serviceSetup = new GeneralSetupService(ApiEndPoints.CHART_GROUP_SETUP);
            CacheData.ChartOfAccountGroup = await serviceSetup.GetAsDatatable();

            DataRow newRow = CacheData.ChartOfAccountGroup.NewRow();
            newRow["id"] = 0;
            newRow["name"] = "-- Select --";

            CacheData.ChartOfAccountGroup.Rows.InsertAt(newRow, 0);
            cmb_group.DataSource = CacheData.ChartOfAccountGroup;
            cmb_group.ValueMember = "id";
            cmb_group.DisplayMember = "name";
        }


        private bool IsFormValid(out string message)
        {
            var records = Helpers.GetControlsValues(pnl_content);
            var errors = new List<string>();
            message = string.Empty;

            if (string.IsNullOrEmpty(records["code"]))
                errors.Add("Code cannot be empty");

            if (string.IsNullOrEmpty(records["name"]))
                errors.Add("Name cannot be empty");

            if (records["group_id"] == 0)
                errors.Add("Group name cannot be empty");

            if (records["class_id"] == 0)
                errors.Add("Class name cannot be empty");

            message = string.Join("\n", errors);

            return errors.Count == 0;
        }

       
        private void ResetData()
        {

            Helpers.ResetControls(pnl_content);
            BtnToggle(false);
            cmb_class.SelectedIndex = 0;
            cmb_group.SelectedIndex = 0;
        }
        private void TryException(Action actionToggle)
        {

            try
            {
                actionToggle();
                Helpers.ResetControls(pnl_content);

            }
            catch (Exception)
            {
                throw;
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dg_chart_of_account_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            Panel[] pnlList = { pnl_content };
            DataTable dt = Helpers.ConvertDataGridViewToDataTable(dg_chart_of_account);

            string class_id = dt.Rows[e.RowIndex]["class_id"].ToString();
            string group_id = dt.Rows[e.RowIndex]["group_id"].ToString();

            cmb_class.SelectedValue = int.Parse(class_id);
            cmb_group.SelectedValue = int.Parse(group_id);

            Helpers.BindControls(pnlList, dt, e.RowIndex);
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            TryException(() => BtnToggle(true));

        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            BtnToggle(true);

        }

        private async void btn_delete_Click(object sender, EventArgs e)
        {

            ApiResponseModel response = new ApiResponseModel();
            var data = Helpers.GetControlsValues(pnl_content);
            DialogResult result = MessageBox.Show("Are you sure you want to delete this data? ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                data["id"] = int.Parse(data["id"]);
                bool isSuccess = await chartOfAccountService.Delete(data);

                if (!isSuccess)
                {
                    Helpers.ShowDialogMessage("error", "Operation failed. Please try again");
                    return;
                }
                Helpers.ShowDialogMessage("success", "Delete Chart of Account Succesfully");
                ResetData();
                FetchChartOfAccount();
            }

        }

        private async void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                ApiResponseModel response = new ApiResponseModel();
                string message;

                bool isFormValid = IsFormValid(out message);

                if (!isFormValid)
                {
                    Helpers.ShowDialogMessage("error", message);
                    return;
                }
                var data = Helpers.GetControlsValues(pnl_content);
                data["group_id"] = int.Parse(data["group_id"].ToString());
                data["class_id"] = int.Parse(data["class_id"].ToString());

                if (txt_id.Text.Equals(""))
                {
                    data.Remove("id");
                    response = await chartOfAccountService.Insert(data);
                    message = response.Success ? "Insert Data Succesfully" : "Fail to add chart of accounts \n" + response.message;
                }
                else
                {
                    data["id"] = int.Parse(data["id"].ToString());
                    response = await chartOfAccountService.Update(data);
                    message = response.Success ? "Update Data Succefully" : "Fail to update chart of accounts \n" + response.message;

                }

                if (!response.Success)
                {
                    Helpers.ShowDialogMessage("error", message);
                    return;
                }

                Helpers.ShowDialogMessage("success", message);
                ResetData();
                FetchChartOfAccount();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            TryException(() => BtnToggle(false));
            ResetData();
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            dataBindingChartAccount.Filter = $"Name LIKE '{txt_search.Text}%' AND Code LIKE '{txt_search.Text}%'";  
        }
    }
}
