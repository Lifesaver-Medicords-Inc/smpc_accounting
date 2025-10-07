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
    public partial class ChartOfAccountPage : UserControl
    {

        readonly ChartOfAccountService chartOfAccountService = new ChartOfAccountService();
        public ChartOfAccountPage()
        {
            InitializeComponent();
        }
        private void Toggle(bool isEditable = false)
        {
            try
            {
                pnl_content.Enabled = isEditable;
                btn_save.Visible = isEditable;
                btn_cancel.Visible = isEditable;

                btn_new_subchart.Visible = !isEditable;
                btn_edit.Visible = !isEditable;
                btn_delete.Visible = !isEditable;
                btn_print.Visible = !isEditable;
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

                var cmb = dt.Rows[e.RowIndex]["account_type"].ToString();
                cmb_account_type.Text = cmb.Trim();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
           
        }

        private void cmb_type_SelectedValueChanged(object sender, EventArgs e)
        {
            if (pnl_content.Enabled)
            {
                string selectedValue = cmb_account_type.SelectedItem?.ToString();

                if (!string.IsNullOrEmpty(selectedValue))
                {
                    txt_account_code.Text = chartOfAccountService.GenerateNewAccountCode(selectedValue, (DataTable)bindingSource.DataSource);
                }
            } 
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            try
            {
                Toggle(false);
                Helpers.ResetControls(pnl_content);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void ChartOfAccountPage_Load(object sender, EventArgs e)
        {
            _ = GetAllAccounts();
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

                    Boolean isEmpty = data.TryGetValue("account_code", out dynamic _code);

                    if (!isEmpty) return;

                    var isCodeExist = list.AsEnumerable().Where(datas => "'" + datas.Field<string>("account_code").ToString() + "'" == _code).ToList();

                    if (isCodeExist.Count > 0 && String.IsNullOrEmpty(txt_id.Text))
                    { 
                        Helpers.ShowDialogMessage("error", "Account Code is already exist!");
                        Helpers.ResetControls(pnl_content);
                        return; 
                    }
                }

                if (String.IsNullOrEmpty(txt_id.Text))
                {
                    data.Remove("id");
                    await chartOfAccountService.Insert(data);
                    Helpers.ResetControls(pnl_content);
                }
                else
                {
                    data["id"] = int.Parse(data["id"]);
                    await chartOfAccountService.Update(data);
                    Helpers.ResetControls(pnl_content);
                }

                await GetAllAccounts();
                Toggle(false);
            }
            catch (Exception)
            {

                throw;
            }
        } 
        private async Task GetAllAccounts()
        {
            try
            {
                var charts = await chartOfAccountService.GetAsDatatable();
                bindingSource.DataSource = charts;

                treeView.Nodes.Clear();

                TreeNode assetRootNode = new TreeNode("Asset(100000)");
                TreeNode liabilityRootNode = new TreeNode("Liability(200000)");
                TreeNode capitalRootNode = new TreeNode("Capital(300000)");
                TreeNode incomeRootNode = new TreeNode("Income(400000)");
                TreeNode expenseRootNode = new TreeNode("Expense(500000)");

                TreeNode emp1 = new TreeNode("Alice");
                emp1.Nodes.Add("Age: 30");
                emp1.Nodes.Add("Position: Manager");

                foreach (DataRow chartRow in charts.Rows)
                {
                    var account_type = chartRow["account_type"].ToString();

                    if (account_type == "ASSET")
                    {
                        assetRootNode.Nodes.Add($"{chartRow["account_code"].ToString()} : {chartRow["account_name"].ToString()}");
                    }

                    if (account_type == "LIABILITY")
                    {
                        liabilityRootNode.Nodes.Add($"{chartRow["account_code"].ToString()} : {chartRow["account_name"].ToString()}");
                    }

                    if (account_type == "CAPITAL")
                    {
                        capitalRootNode.Nodes.Add($"{chartRow["account_code"].ToString()} : {chartRow["account_name"].ToString()}");
                    }

                    if (account_type == "INCOME")
                    {
                        incomeRootNode.Nodes.Add($"{chartRow["account_code"].ToString()} : {chartRow["account_name"].ToString()}");
                    }

                    if (account_type == "EXPENSE")
                    {
                        expenseRootNode.Nodes.Add($"{chartRow["account_code"].ToString()} : {chartRow["account_name"].ToString()}");
                    }
                }

                treeView.Nodes.Add(assetRootNode);
                treeView.Nodes.Add(liabilityRootNode);
                treeView.Nodes.Add(capitalRootNode);
                treeView.Nodes.Add(incomeRootNode);
                treeView.Nodes.Add(expenseRootNode);

                treeView.ExpandAll(); // Optional: expand all nodes    
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_id.Text)) return;

            Toggle(true);
        }

        private async void btn_delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txt_id.Text)) return;

                var data = Helpers.GetControlsValues(pnl_content);
                data["id"] = int.Parse(data["id"]);

                await chartOfAccountService.Delete(data);

                //bindingSource.DataSource = await chartOfAccountService.GetAsDatatable(); 
                await GetAllAccounts();
                Helpers.ResetControls(pnl_content);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                if (!e.Node.Text.Contains(':')) return;

                txt_parent_account.Text = e.Node.Text;
                var listofchart = (DataTable)bindingSource.DataSource;
                var selectedChart = e.Node.Text.Split(':')[0].Trim();

                var selectedChartData = listofchart.AsEnumerable()
                    .Where(o => o.Field<string>("account_code") == selectedChart)
                    .CopyToDataTable();

                Helpers.BindControls(new Panel[] { pnl_content }, selectedChartData, 0);

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void contextMenuStrip2_Opening(object sender, CancelEventArgs e)
        {

        }

        private void btn_new_subchart_Click(object sender, EventArgs e)
        {
            Toggle(true);
            cmb_account_type.Text = "";
            txt_account_code.Text = "";
            txt_account_name.Text = "";
            txt_short_name.Text = "";
            txt_id.Text = "";

            cmb_account_type.Enabled = false;
            txt_parent_account.Enabled = false;
            
            txt_account_code.Text = chartOfAccountService.GenerateNewSubsidiaryAccountCode(txt_parent_account.Text, (DataTable)bindingSource.DataSource);
        }

        private void btn_new_Click_1(object sender, EventArgs e)
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
    }
}
