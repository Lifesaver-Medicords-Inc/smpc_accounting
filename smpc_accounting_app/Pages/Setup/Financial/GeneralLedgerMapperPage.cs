
using smpc_accounting_app.Models;
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
    public partial class GeneralLedgerMapperPage : UserControl
    {

        readonly GeneralLedgerMapperService generalLedgerMapperService = new GeneralLedgerMapperService(); 

        readonly ChartOfAccountService chartOfAccountService = new ChartOfAccountService();

        DataTable ChartOfAccountList; 

        public GeneralLedgerMapperPage()
        {
            InitializeComponent();
        }

        private void ChartOfAccountMappingPage_Load(object sender, EventArgs e)
        {

        }

        private async Task GetAllAccounts()
        {
            try
            {
                ChartOfAccountList = await chartOfAccountService.GetAsDatatableForCombobox();
                bindingSource.DataSource = await generalLedgerMapperService.GetAsDatatable();

                chart_code.DataSource = ChartOfAccountList;
                chart_code.DisplayMember = "account_code";
                chart_code.ValueMember = "id";
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async void GeneralLedgerMapperPage_Load(object sender, EventArgs e)
        {
            await GetAllAccounts();

            await SetSavedChartAsync();
        }

        private async Task SetSavedChartAsync()
        {
            for (int rowIndex = 0; rowIndex < dgv_list.RowCount; rowIndex++)
            {
                var row = ((DataRowView)bindingSource[rowIndex]).Row;
                var chartCodeCell = row["account_id"].ToString();
                if (chartCodeCell != null && chartCodeCell != "")
                {
                    var selectedCellId = dgv_list.Rows[rowIndex].Cells["id"].Value?.ToString();
                    var selectedCellAccountId = dgv_list.Rows[rowIndex].Cells["account_id"].Value?.ToString();
                    if (string.IsNullOrEmpty(selectedCellId)) continue;

                    var matchingRow = ChartOfAccountList.AsEnumerable()
                        .FirstOrDefault(o => o.Field<string>("id") == selectedCellAccountId);

                    if (matchingRow != null)
                    {
                        dgv_list.Rows[rowIndex].Cells["chart_name"].Value = matchingRow.Field<string>("account_name");
                        dgv_list.Rows[rowIndex].Cells["chart_code"].Value = selectedCellAccountId;
                    }
                }
            }
        }

        private void dgv_list_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgv_list_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

        } 

        private void dgv_list_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            
            if (dgv_list.Columns[e.ColumnIndex].Name == "chart_code")
            {
                var cell = dgv_list.Rows[e.RowIndex].Cells["chart_name"];
                var cell_id = dgv_list.Rows[e.RowIndex].Cells["account_id"];

                if (dgv_list.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
                {
                    cell_id.Value = "0";
                    cell.Value = "";
                    return;
                }

                string selectedCellId = dgv_list.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                var value = ChartOfAccountList.AsEnumerable().FirstOrDefault(o => o.Field<string>("id") == selectedCellId);

                cell.Value = value.Field<string>("account_name").ToString();  

                cell_id.Value = selectedCellId;
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            var dialogResult = MessageBox.Show("Do you want to cancel changes?", "SMPC SOFTWARE", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.Yes)
            {
                Helpers.ShowDialogMessage("success", "Successfully saved!");
            } 

        }

        private async void btn_save_Click(object sender, EventArgs e)
        {
            var userResponse = MessageBox.Show(
               "Do you want to save changes?",
               "SMPC SOFTWARE",
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Warning);

            if (userResponse != DialogResult.Yes)

                return;

            DataTable dataFromDatagridview = (DataTable)bindingSource.DataSource;

            var updatedLedgerList = new List<Dictionary<string, dynamic>>();

            var payload = new Dictionary<string, dynamic>();

            foreach (DataRow item in dataFromDatagridview.Rows)
            {
                var ledgerId = int.Parse(
                        string.IsNullOrEmpty(item["id"].ToString()) ? "0" : item["id"].ToString()
                    );

                var accountId = int.Parse(
                        string.IsNullOrEmpty(item["account_id"].ToString()) ? "0" : item["account_id"].ToString()
                    );

                var pseudoAccount = item["pseudo_account"].ToString();

                Dictionary<string, dynamic> rowData = new Dictionary<string, dynamic>
                    {
                        {"id",ledgerId },
                        {"pseudo_account",pseudoAccount },
                        {"account_id",accountId }
                    };

                updatedLedgerList.Add(rowData);
            }

            payload.Add("Payload", updatedLedgerList);

            await generalLedgerMapperService.Update(payload);

            Helpers.ShowDialogMessage("success", "Successfully saved!");
        }
    }
}
