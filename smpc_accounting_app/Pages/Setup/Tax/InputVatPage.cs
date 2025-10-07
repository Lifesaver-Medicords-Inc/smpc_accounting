using smpc_accounting_app.Models;
using smpc_accounting_app.Services.Helpers;
using smpc_accounting_app.Services.Setup;
using smpc_accounting_app.Shared;
using smpc_sales_app.Services.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace smpc_accounting_app.Pages.Setup.Tax
{
    public partial class InputVatPage : UserControl
    {
        public static DataTable ChartOfAccountPurchasing { get; set; } = new DataTable();
        public static DataTable ChartOfAccountSales { get; set; } = new DataTable();
        DataTable taxDetails;
        DataTable tax;
        DataTable taxView;
        TaxSetupList setupList = new TaxSetupList();


        readonly ChartOfAccountsService chartOfAccountService = new ChartOfAccountsService();
        readonly TaxSetupService taxSetupService = new TaxSetupService();
        TaxSetupList records;
        int selectedRecords;
        string dateFormat = "yyyy-MM-dd";

        public InputVatPage()
        {
            InitializeComponent();
           
        }
        private void InputVatPage_Load(object sender, EventArgs e)
        {
            FetchChartOfAccountSales();
            FetchChartOfAccountPurchasing();
            TaxRecords();
           
        }

        private async Task<TaxSetupList> FetchTaxRecords()
        {
            var response = await ApiService<ApiResponseModel<TaxSetupList>>.Get(ApiEndPoints.TAX_SETUP);
            records = response.data;
            return records;
        }

      
        private async Task<List<TaxSetup>> FetchTaxRecordsss()
        {
            var response1 = await ApiService<ApiResponseModel<TaxSetupList>>.Get(ApiEndPoints.TAX_SETUP);
            var view = response1.data;
            return response1.data.Tax;
        }
        private async void TaxRecords()
        {
            var taxRecords = await FetchTaxRecords();
            var view = await FetchTaxRecordsss();
            if (taxRecords == null  || taxRecords.Tax.Count == 0)
            {
                MessageBox.Show("No tax setup available !", "SMPC SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
          
            BindRecordsToDataTable(taxRecords);
            BindTaxRecords(true);
           
            
        }

        private void BindRecordsToDataTable(TaxSetupList records)
        {

            tax = JsonHelper.ToDataTable(records.Tax);
            taxDetails = JsonHelper.ToDataTable(records.Tax_Details);
            taxView = JsonHelper.ToDataTable(records.Tax_View);
        }
        private void BindTaxRecords(bool isBind = false)
        {
            if (isBind)
            {
                BindDataToPanel();
                BindDataToDataGrid();
                //cmb_coa_sales.SelectedValue = records.Tax[this.selectedRecords].coa_sales_id;
                //cmb_coa_sales.SelectedItem = records.Tax[this.selectedRecords].coa_sales_id;
                //cmb_coa_purchase.SelectedValue = records.Tax[this.selectedRecords].coa_purchase_id;
                //cmb_coa_purchase.SelectedItem = records.Tax[this.selectedRecords].coa_purchase_id;
            }
        }
        private void BindDataToPanel()
        {
            Panel[] panels = { pnl_content };
            if (tax != null)
            {
                Helpers.BindControls(panels, tax, selectedRecords);

            }
        }
        private void BindDataToDataGrid()
        {
            DataView dataViewValidityDetails = new DataView(taxDetails);
            DataTable filteredRecord = new DataTable();
            if (dataViewValidityDetails.Count != 0)
            {
                dataViewValidityDetails.RowFilter = "tax_code_id = '" + tax.Rows[this.selectedRecords]["id"].ToString() + "'";
                DataTable filterTaxDetails = dataViewValidityDetails.ToTable();
                
                if (filterTaxDetails.Rows.Count > 0)
                {
                    if (!filterTaxDetails.Columns.Contains("valid_status"))
                    {
                        filterTaxDetails.Columns.Add("valid_status", typeof(string));
                    }
                     filteredRecord =  ValidateTaxDetailStatus(filterTaxDetails);           
                }           
                dataBindingTaxDetails.DataSource = filteredRecord;
            }

            foreach (DataColumn column in taxView.Columns)
            {
                column.ColumnName = column.ColumnName.ToLower();
            }
            dataBindingTaxList.DataSource = taxView;
        }
   
        private string GetDateStatus(string currentRow, string prevRow)
        {
            DateTime currentDate = DateTime.Parse(currentRow);
            DateTime prevDates = DateTime.Parse(prevRow);
            if (currentDate > prevDates)
            {
                return "FUTURE";
            }
            else if (currentDate < prevDates)
            {
                return "ACTIVE";

            }
            return "INACTIVE";
        } 

        private DataTable ValidateTaxDetailStatus(DataTable records)
        {
            for (int i = 0; i < records.Rows.Count; i++)
            {
                var currentRow = records.Rows[i];
                var currentValidFrom = currentRow["valid_from"].ToString();
                currentRow["valid_status"] = "ACTIVE";

                if (i > 0)
                {
                    var prevRow = records.Rows[i - 1];
                    var prevValidTo = prevRow["valid_to"].ToString();

                    var status = GetDateStatus(currentValidFrom, prevValidTo);
                    currentRow["valid_status"] = status;
                }
            } 
            return records;
        }
        private bool TaxHeaderValidation(out string messages)
        {
           messages = string.Empty;
           var records = Helpers.GetControlsValues(pnl_content);
           var errors = new List<string>();
          
            if (string.IsNullOrEmpty(records["code"]) || string.IsNullOrEmpty(records["tax_desc"]))
            {
               errors.Add("Tax code and Tax Description is required");
            }
            if (records["coa_sales_id"] == 0 && records["coa_purchase_id"] == 0)
            {
                errors.Add("Linking at least one tax account is required");
            }
            messages = string.Join("\n", errors);

            return errors.Count == 0;
        }
        private async Task<DataTable> FetchChartOfAccountClassification(string chartClass)
        {
            var responseData = await chartOfAccountService.GetChartOfAccountsClassfication(chartClass);
            return  responseData == null ? null : JsonHelper.ToDataTable(responseData);
        }
        private async void FetchChartOfAccountSales()
        {
            var records = await FetchChartOfAccountClassification("CL");
            if (records == null)
            {
                return;
            }

            ChartOfAccountSales = records;

            DataRow newRow = ChartOfAccountSales.NewRow();
            newRow["id"] = 0;
            newRow["name"] = "-- Select --";

            ChartOfAccountSales.Rows.InsertAt(newRow, 0);
            cmb_coa_sales.DataSource = ChartOfAccountSales;
            cmb_coa_sales.DisplayMember = "name";
            cmb_coa_sales.ValueMember = "id";
        }
        private async void FetchChartOfAccountPurchasing()
        {
            var records = await FetchChartOfAccountClassification("CA");
            if (records == null)
            {
                return;
            }
            ChartOfAccountPurchasing = records;

            DataRow newRow = ChartOfAccountPurchasing.NewRow();
            newRow["id"] = 0;
            newRow["name"] = "-- Select --";

            ChartOfAccountPurchasing.Rows.InsertAt(newRow, 0);
            cmb_coa_purchase.DataSource = ChartOfAccountPurchasing;
            cmb_coa_purchase.DisplayMember = "name";
            cmb_coa_purchase.ValueMember = "id";
        }

        private void ResetData()
        {
            Helpers.ResetControls(pnl_content);
            DataTable cloneTaxDetails = null;

            if (taxDetails != null)
            {
                cloneTaxDetails = taxDetails.Clone(); // Clone structure
            }
            else
            {
                cloneTaxDetails = new DataTable();
                cloneTaxDetails.Columns.Add("valid_status", typeof(string));
            }

            DataRow newRow = cloneTaxDetails.NewRow();
            newRow["valid_status"] = "ACTIVE";
            cloneTaxDetails.Rows.Add(newRow);

            dataBindingTaxDetails.DataSource = cloneTaxDetails;
            dgv_tax_details.DataSource = dataBindingTaxDetails; // Ensure rebind
            dgv_tax_details.Refresh(); // Redraw

         
        }
        private void ValidityPeriodStatus()
        {
            //var combobox = (DataGridViewComboBoxColumn)dgv_tax_details.Columns["valid_status"];

            //combobox.DataSource = ENUM_TAX_CODE.STATUS_LIST();
            //combobox.DisplayMember = "title";
            //combobox.ValueMember = "value";
        }
        private void BtnToggle(bool isEditable = false)
        {
            pnl_content.Enabled = isEditable;
            btn_save.Visible = isEditable;
            btn_cancel.Visible = isEditable;
            btn_new.Visible = !isEditable;
            btn_edit.Visible = !isEditable;
            btn_delete.Visible = !isEditable;
            btn_prev.Visible = !isEditable;
            btn_next.Visible = !isEditable;
        }
    
        private List<TaxDetails> SaveValidityDetails(bool isUpdate)
        {
            var taxDetailsRecords = Helpers.ConvertDataGridViewToDataTable(dgv_tax_details);
            List<TaxDetails> taxDetailList = new List<TaxDetails>();

            TaxDetails taxDetails = null;
            int id = 0;
            int tax_code_id = 0;
            string valid_from;
            string valid_to;
            string valid_status;
            decimal tax_rate = 0m;
            foreach(DataRow row in taxDetailsRecords.Rows)
            {

                if (isUpdate)
                {

                    if (row.IsNull("id") || string.IsNullOrWhiteSpace(row["tax_code_id"].ToString())) { 
                        id = 0;
                        tax_code_id = 0;
                    }
                    else
                    {
                        id = int.Parse(row["id"].ToString());
                        tax_code_id = int.Parse(row["tax_code_id"].ToString());
                    }
                  
                }

                valid_from   = row["valid_from"].ToString();
                valid_to     = row["valid_to"].ToString();
                valid_status = "";
                if (!decimal.TryParse(row["tax_rate"]?.ToString(), out tax_rate))
                {
                    tax_rate = 0;
                }
                taxDetails = new TaxDetails(id,tax_code_id,valid_from,valid_to,valid_status,tax_rate);
                taxDetailList.Add(taxDetails);
            }

            return taxDetailList;
        }

        private async void btn_save_Click(object sender, EventArgs e)
        {
            ApiResponseModel response = new ApiResponseModel();
            string message;
            try
            {
                string taxHeaderMessage = "";
                var data = Helpers.GetControlsValues(pnl_content);
                string[] taxFieldToInt = { "coa_sales_id", "coa_purchase_id", };

                bool isIntFieldsValid = Helpers.ConvertFieldPropertiesType<int>(data, taxFieldToInt);
                bool isCreditable = data.TryGetValue("input_tax_creditable", out var value) && value?.ToString() == "1";
                bool isTaxRecordValid = TaxHeaderValidation( out taxHeaderMessage);
                data["input_tax_creditable"] = isCreditable;
                
                if (!isIntFieldsValid) {
                   return; 
                }
                
                if (!isTaxRecordValid) {
                    Helpers.ShowDialogMessage("error", taxHeaderMessage);
                    return;
                }


                if (txt_id.Text.Equals("")) {
                    var taxDetails = SaveValidityDetails(false);
                    data.Add("tax_details", taxDetails);
                    data.Remove("id");

                    response = await taxSetupService.Insert(data);
                    message = response.Success ? "Insert Data Succesfully" : "Fail to add chart of accounts \n" + response.message;
                }


                else {

                    var taxDetails = SaveValidityDetails(true);
                    data.Add("tax_details", taxDetails);
                    var datanew = data;
                    data["id"] = int.Parse(data["id"]);

                    response = await taxSetupService.Update(data);
                    message = response.Success ? "Update Data Succefully" : "Fail to update chart of accounts \n" + response.message;
                }

                if (!response.Success) {
                    Helpers.ShowDialogMessage("error", message);
                    return;
                }

                ResetData();
                BtnToggle(false);

                TaxRecords();

            } catch(Exception ex)
            {
                MessageBox.Show("Failed in Saving Data \n", ex.Message);
                throw;
            }
          
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            BtnToggle(true);
            ResetData();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Helpers.ResetControls(pnl_content);
            BtnToggle(false);

            if(records != null)
            {
                BindTaxRecords(true);
            }
           
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            BtnToggle(true);
        }

        private void dgv_tax_details_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

               string col = dgv_tax_details.Columns[e.ColumnIndex].Name;

            // Only trigger if editing valid_from or valid_to
            if (col == "valid_from" || col == "valid_to")
            {
                ValidateChainedDates();
                AutoFillActiveValidTo();
                AutoFillPrevValidToWhenCurrentValidFromEntered();
            }

            //string futureValue = dgv_tax_details.Rows[e.RowIndex].Cells["valid_from"].Value.ToString();

            //var _previousCellValueValidTo = dgv_tax_details.Rows[0].Cells["valid_to"].Value.ToString();
            //var _previousCellValueValidFrom = dgv_tax_details.Rows[0].Cells["valid_from"].Value.ToString();


            //if (_previousCellValueValidTo != "" && _previousCellValueValidFrom != "")
            //{
            //    dgv_tax_details.Rows[0].Cells["status"].Value = "ACTIVE";


            //    for (int i = 1; i < dgv_tax_details.Rows.Count; i++)
            //    {

            //        var prevRow = dgv_tax_details.Rows[i - 1];
            //        var currentRow = dgv_tax_details.Rows[i];
            //        var prevRowValidFrom = prevRow.Cells["valid_from"].Value.ToString();
            //        var prevRowValidTo = prevRow.Cells["valid_to"].Value.ToString();

            ////        var currentValidFrom = currentRow.Cells["valid_from"].Value.ToString();

            //        if (prevRowValidTo == "" && prevRowValidFrom != "")
            //            {
            //                DateTime date = DateTime.Parse(futureValue).AddDays(-1);
            //                dgv_tax_details.Rows[0].Cells["valid_to"].Value = date.ToString("MM/dd/yy");
            //                dgv_tax_details.Rows[0].Cells["status"].Value = "ACTIVE";

            //            }






            //    }

            //}

            //if (e.RowIndex >= 1)
            //{
            //    if (_previousCellValueValidTo == "" && _previousCellValueValidFrom != "")
            //    {
            //        DateTime date = DateTime.Parse(futureValue).AddDays(-1);
            //        dgv_tax_details.Rows[0].Cells["valid_to"].Value = date.ToString("MM/dd/yy");
            //        dgv_tax_details.Rows[0].Cells["status"].Value = "ACTIVE";

            //    }
            //    else if (_previousCellValueValidFrom != "")
            //    {
            //        DateTime futureDate = DateTime.Parse(futureValue);
            //        DateTime prevDate = DateTime.Parse(_previousCellValueValidTo);
            //        if (futureDate <= prevDate)
            //        {
            //            MessageBox.Show("Invalid future date");
            //            dgv_tax_details.Rows[e.RowIndex].Cells["valid_from"].Value = "";
            //            return;
            //        }
            //        else
            //        {
            //            dgv_tax_details.Rows[e.RowIndex].Cells["status"].Value = "FUTURE";
            //            dgv_tax_details.Rows[0].Cells["status"].Value = "ACTIVE";
            //        }
            //    }

            //    else
            //    {
            //        MessageBox.Show("Valid From is required");
            //        dgv_tax_details.Rows[e.RowIndex].Cells["valid_from"].Value = "";
            //        return;
            //    }
            //}


            if (e.ColumnIndex == 2)
            {
                var cell = dgv_tax_details.Rows[e.RowIndex].Cells[e.ColumnIndex];
                decimal result;

                if (cell.Value != null && !decimal.TryParse(cell.Value.ToString(), out result))
                {
                    MessageBox.Show($"Invalid decimal value in row {e.RowIndex + 1}. Please enter a valid number.");
                    // Reset to previous value or default
                    cell.Value = 0;
                    // Set error text for the specific row
                    dgv_tax_details.Rows[e.RowIndex].ErrorText = "Invalid decimal value";
                }
                else
                {
                    dgv_tax_details.Rows[e.RowIndex].ErrorText = string.Empty;
                }
            }
        }

        private void ValidateChainedDates()
        {
            for (int i = 0; i < dgv_tax_details.Rows.Count; i++)
            {
                var row = dgv_tax_details.Rows[i];

                // Get user inputs
                string validFromStr = row.Cells["valid_from"].Value?.ToString();
                string validToStr = row.Cells["valid_to"].Value?.ToString();

           

                if (!DateTime.TryParse(validFromStr, out DateTime validFrom) ||
                    !DateTime.TryParse(validToStr, out DateTime validTo))
                {
                    // Skip rows with incomplete date entries
                    continue;
                }

                // Rule 1: valid_to must be after valid_from
                if (validTo <= validFrom)
                {
                    MessageBox.Show($"Row {i + 1}: 'VALID TO' must be after 'VALID FROM'.");
                    row.Cells["valid_to"].Value = "";
                    continue;
                }

                // Rule 2: From 2nd row onwards, valid_from must be after previous valid_to
                if (i > 0)
                {
                    var prevRow = dgv_tax_details.Rows[i - 1];
                    string prevValidToStr = prevRow.Cells["valid_to"].Value?.ToString();

                    if (!DateTime.TryParse(prevValidToStr, out DateTime prevValidTo))
                    {
                        MessageBox.Show($"Row {i + 1}: Previous row's 'VALID TO' is invalid or missing.");
                        continue;
                    }

                    if (validFrom <= prevValidTo)
                    {
                        MessageBox.Show($"Row {i + 1}: 'VALID FROM' must be after previous 'VALID TO' ({prevValidTo:yyyy-MM-dd}).");
                        row.Cells["valid_from"].Value = "";
                        continue;
                    }

                    // Set FUTURE status for valid future record
                    row.Cells["valid_status"].Value = "FUTURE";
                }
                else
                {
                    // First row is always ACTIVE
                    row.Cells["valid_status"].Value = "ACTIVE";
                }
            }
        }

        private void AutoFillActiveValidTo()
        {
            DateTime? firstFutureValidFrom = null;

            // Step 1: Find the first FUTURE row with a valid valid_from
            foreach (DataGridViewRow row in dgv_tax_details.Rows)
            {
                string status = row.Cells["valid_status"].Value?.ToString();
                if (status == "FUTURE")
                {
                    string futureFromStr = row.Cells["valid_from"].Value?.ToString();
                    if (DateTime.TryParse(futureFromStr, out DateTime futureFrom))
                    {
                        firstFutureValidFrom = futureFrom;
                        break; // Use the first future row only
                    }
                }
            }

            // If there's no future record yet, skip
            if (firstFutureValidFrom == null)
                return;

            // Step 2: Fill the missing valid_to of the ACTIVE row (if any)
            foreach (DataGridViewRow row in dgv_tax_details.Rows)
            {
                string status = row.Cells["valid_status"].Value?.ToString();
                string validToStr = row.Cells["valid_to"].Value?.ToString();

                if (status == "ACTIVE" && string.IsNullOrWhiteSpace(validToStr))
                {
                    row.Cells["valid_to"].Value = firstFutureValidFrom.Value.AddDays(-1).ToString(dateFormat);
                    break; // Only the first active row with blank valid_to is handled
                }
            }
        }

        private bool isValidDateFormat(string input)
        {
            DateTime parsedDate;
            bool isValid = false;

            isValid = DateTime.TryParseExact(input,dateFormat,CultureInfo.InvariantCulture,DateTimeStyles.None,out parsedDate );
            return isValid;
        }
        private void AutoFillPrevValidToWhenCurrentValidFromEntered()
        {
            for (int i = 1; i < dgv_tax_details.Rows.Count; i++) // Start from 2nd row
            {
                var currentRow = dgv_tax_details.Rows[i];
                var prevRow = dgv_tax_details.Rows[i - 1];

                // Get valid_from of current row
                string currValidFromStr = currentRow.Cells["valid_from"].Value?.ToString();
                string prevValidToStr = prevRow.Cells["valid_to"].Value?.ToString();

                if (DateTime.TryParse(currValidFromStr, out DateTime currValidFrom) &&
                    string.IsNullOrWhiteSpace(prevValidToStr))
                {
                    // Set valid_to of previous row = current valid_from - 1 day
                    prevRow.Cells["valid_to"].Value = currValidFrom.AddDays(-1).ToString(dateFormat);
                }
            }
        }
        private void pnl_content_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel_tax_code_list_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            dataBindingTaxList.Filter = $"code LIKE '{txt_search.Text}%' OR tax_desc LIKE '%{txt_search.Text}%'";


        }

        private void dgv_tax_code_list_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            if (this.tax.Rows.Count - 1 > this.selectedRecords)
            {
                this.selectedRecords++;
                BindTaxRecords(true);

            }
            else
            {
                MessageBox.Show("No record found", "SMPC SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void btn_prev_Click(object sender, EventArgs e)
        {
            if (this.selectedRecords > 0)
            {
                this.selectedRecords--;
                BindTaxRecords(true);
            }
            else
            {
                MessageBox.Show("No record found", "SMPC SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgv_tax_details_CellClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void dgv_tax_details_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
           


        }

        private void dgv_tax_details_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                // Column name or index you want to validate
                var columnName = dgv_tax_details.Columns[e.ColumnIndex].Name;

                // Only validate specific column
                if (columnName == "valid_from" || columnName == "valid_to")
                {
                    string input = e.FormattedValue?.ToString()?.Trim();

                    // Allow blank input if needed
                    if (string.IsNullOrEmpty(input))
                        return;

                    // Try to parse with strict format
                    if (!DateTime.TryParseExact(input, dateFormat,
                        System.Globalization.CultureInfo.InvariantCulture,
                        System.Globalization.DateTimeStyles.None, out _))
                    {
                        MessageBox.Show("Invalid date. Please use yyyy-MM-dd.",
                                        "Date Format Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        e.Cancel = true; // Cancel editing
                        dgv_tax_details.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = ""; // Reset cell
                    }
                }
            }
            catch(Exception ex)
            {
                throw;
            }
         
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {

        }
    }

}
