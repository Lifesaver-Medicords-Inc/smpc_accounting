using smpc_accounting_app.Models;
using smpc_accounting_app.Services;
using smpc_accounting_app.Services.Accounting;
using smpc_accounting_app.Services.Helpers;
using smpc_accounting_app.Services.Sales;
using smpc_accounting_app.Services.Setup;
using smpc_accounting_app.Shared;
using smpc_sales_app.Services.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace smpc_accounting_app.Pages.Transactions.AccountsReceivables
{
   
    public partial class SalesInvoicePage : UserControl
    {
        readonly SalesOrderServices salesOrderServices = new SalesOrderServices();
        readonly ChartOfAccountsService chartOfAccountsServices = new ChartOfAccountsService();
        SalesInvoiceService salesInvoiceService = new SalesInvoiceService();
        TaxClassificationService taxService = new TaxClassificationService();
        GeneralSetupService serviceSetup;
        DataTable salesOrderWithDr;
        DataTable salesOrderHead;
        DataTable salesOrderDetails;
        DataTable salesInvoice;
        DataTable salesInvoiceDetails;
        SaleInvoiceList records;
        int salesInvoiceDocNo;
        int selectedRecords = 0;
        public SalesInvoicePage()
        {
            InitializeComponent();
            dg_sales_invoice.CellPainting += dg_sales_invoice_CellPainting;
            dg_sales_invoice.Paint += dg_sales_invoice_Paint;
        }

        private void SalesInvoice_Load(object sender, EventArgs e)
        {
            GroupColumnBuffer();
            GetPanelWithDigitValidationFields(pnl_footer);
            FetchSalesOrderWithDr();
            FetchPaymentTerms();
            //FetchChartOfAccounts();
            FetchSalesInvoiceDocNo();
            //FetchTaxCode();
            cmb_currency.SelectedIndex = 1;
            SalesInvoiceRecords();

        }
        private async Task<SaleInvoiceList> FetchSalesInvoice()
        {
            try
            {

                var response = await ApiService<ApiResponseModel<SaleInvoiceList>>.Get(ApiEndPoints.SALES_INVOICE);
                records = response.data;
                return records;
            }
            catch(Exception)
            {
                throw;
            }

        }
        private async void SalesInvoiceRecords()
        {
          var salesInvoiceRecord =  await FetchSalesInvoice();

          if (salesInvoiceRecord == null)
            {
                MessageBox.Show("Application failed, try again!", "SMPC SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            BindRecordsToDataTable(salesInvoiceRecord);
            BindSaleInvoiceRecords(true);
              
        }

        private async void FetchSalesOrderWithDr()
        {
            var responseData = await salesOrderServices.GetAsDatatable();
            salesOrderWithDr = responseData;

        }
        private async void FetchPaymentTerms()
        {
            serviceSetup =  new GeneralSetupService(ApiEndPoints.PAYMENT_TERMS_SETUP);
            CacheData.PaymentTerms = await serviceSetup.GetAsDatatable();

            DataRow newRow = CacheData.PaymentTerms.NewRow();
            newRow["id"] = DBNull.Value;
            newRow["name"] = "--Select--";

            CacheData.PaymentTerms.Rows.InsertAt(newRow, 0);

            cmb_payment_terms.DataSource = CacheData.PaymentTerms;
            cmb_payment_terms.DisplayMember = "name";
            cmb_payment_terms.ValueMember = "id";
        }
        private async void FetchChartOfAccounts()
        {
            var responseData = await chartOfAccountsServices.GetChartOfAccountsClassfication("SALES");
            CacheData.ChartOfAccountClassification = JsonHelper.ToDataTable(responseData);


            cmb_journal.DataSource = CacheData.ChartOfAccountClassification;
            cmb_journal.DisplayMember = "name";
            cmb_journal.ValueMember = "id";

        }
        private async void FetchTaxCode()
        {

            var responseData = await taxService.GetTaxClassfication("CL");
            CacheData.TaxClassification = JsonHelper.ToDataTable(responseData);

            cmb_tax.DataSource = CacheData.TaxClassification;
            cmb_tax.DisplayMember = "code";
            cmb_tax.ValueMember = "id";
        }

        private async void FetchSalesInvoiceDocNo()
        {
            var response = await ApiService<ApiResponseModel<int>>.Get(ApiEndPoints.SALES_INVOICE_DOC_NO);
            salesInvoiceDocNo = response.data;
        }

        private void BindDataToPanel()
        {
            Panel[] panels = { pnl_header, pnl_footer };

            Helpers.BindControls(panels, salesInvoice, this.selectedRecords);
        }


        private void BindSaleInvoiceRecords(bool isBind = false)
        {
            if (isBind)
            {
                BindDataToPanel();
                BindDataToDataGrid();
                if (records.sales_invoice.Count > 0)
                {
                    cmb_tax.SelectedValue = records.sales_invoice[this.selectedRecords].tax_id;
                }
            }
        }

        private void BindRecordsToDataTable(SaleInvoiceList records)
        {
            if (records.sales_invoice != null)
            {
                salesInvoice = JsonHelper.ToDataTable(records.sales_invoice);
                salesInvoiceDetails = JsonHelper.ToDataTable(records.sales_invoice_details);
            }
        }

    
        private void BindDataToDataGrid()
        {
            DataView dataViewSalesInvoiceDetails = new DataView(salesInvoiceDetails);

            if (dataViewSalesInvoiceDetails.Count != 0)
            {
                dataViewSalesInvoiceDetails.RowFilter = "sales_invoice_id = '" + salesInvoice.Rows[this.selectedRecords]["id"].ToString() + "'";
                dataBindingSalesInvoice.DataSource = dataViewSalesInvoiceDetails.ToTable();
            }

        }
        private void BtnToggle(bool isEditable = false)
        {

            pnl_header.Enabled = isEditable;
            btn_save.Visible = isEditable;
            btn_cancel.Visible = isEditable;
            btn_new.Visible = !isEditable;
            btn_print.Visible = !isEditable;
            btn_search.Visible = !isEditable;
            btn_prev.Visible = !isEditable;
            btn_next.Visible = !isEditable;

        }
        private void DocumentCodeIncrementor()
        {
            txt_doc_no.Text = $"SI#{salesInvoiceDocNo + 1}";  
        }

        private async void LoadSalesOrderById(string orderId)
        {
            var response = await salesOrderServices.GetSalesOrderId(orderId);

            salesOrderHead = JsonHelper.ToDataTable(response.orders);
            salesOrderDetails = JsonHelper.ToDataTable(response.order_details);

            Panel[] pnlHeader = { pnl_header };
            Helpers.BindControls(pnlHeader, salesOrderHead);
            dg_sales_invoice.DataSource = salesOrderDetails;

            decimal netAmount = decimal.Parse(salesOrderHead.Rows[0]["net_amount"].ToString());
            int paymentTermsId = int.Parse(salesOrderHead.Rows[0]["payment_terms_id"].ToString()); 
            string taxCode = salesOrderHead.Rows[0]["tax_code"].ToString();

            cmb_payment_terms.SelectedIndex = paymentTermsId;
            cmb_tax.Text = taxCode;

            DocumentCodeIncrementor();
            
            txt_doc_so_no.Text = $"SO#{salesOrderHead.Rows[0]["doc_so_no"]}";
            txt_ref_po.Text = $"PO#{salesOrderHead.Rows[0]["ref_po"]}";
            txt_reference_dr_no.Text = $"DR#{salesOrderHead.Rows[0]["ref_po"]}";
            txt_base_rate.Text = "1";
            txt_sales_id.Text = txt_sales_executive.Text;  // this is for the meantime since S.O is not fix

            //Computation for the mean time and it adjust if S.O data is alredy Done
            Taxation tax = new Taxation(netAmount, "NON-VAT", 0);
            var value = tax.ComputeTaxValue();
            var amountNetVat = value.vatable - value.vat_amount;
            var vatAmount =cmb_tax.Text != "VAT" ? 0 : value.vat_amount;
            var vatable = value.vatable;
            var nonTaxable = cmb_tax.Text != "VAT" ? value.vatable : 0;

            Helpers.FormatAsCurrency(txt_vatable_sales, vatable);
            Helpers.FormatAsCurrency(txt_less_vat, vatAmount);
            Helpers.FormatAsCurrency(txt_total_sales_vat_inclusive, vatable);
            Helpers.FormatAsCurrency(txt_amount_net_vat, amountNetVat);
            Helpers.FormatAsCurrency(txt_amount_due, amountNetVat);
            Helpers.FormatAsCurrency(txt_add_vat, vatAmount);
            Helpers.FormatAsCurrency(txt_total_amount_due, vatable);
            Helpers.FormatAsCurrency(txt_zero_rated_sales, nonTaxable);
            Helpers.FormatAsCurrency(txt_vat_exempt_sales, nonTaxable);
            Helpers.FormatAsCurrency(txt_pwd_discount, 0);
        }

        private void ResetData()
        {
            Helpers.ResetControls(pnl_header);
            Helpers.ResetControls(pnl_footer);

            cmb_payment_terms.SelectedIndex = 1;
            cmb_currency.SelectedIndex = 1;
            cmb_tax.SelectedIndex = 0;


            DataTable cloneSalesInvoice = salesInvoiceDetails.Clone();
            dataBindingSalesInvoice.DataSource = cloneSalesInvoice;
            dg_sales_invoice.DataSource = dataBindingSalesInvoice; // Ensure rebind
            dg_sales_invoice.Refresh(); // Redraw
        }

        private Dictionary<string, dynamic> AddForeignCurrencyInSalesInvoice(Dictionary<string, dynamic> data , string[] selectedKey)
        {

            var records = new Dictionary<string, dynamic>(data);

            foreach (var value in selectedKey)
            {
                if (data.ContainsKey(value))
                {
                    string newFcKey = $"{value}_fc";

                    if (!records.ContainsKey(newFcKey))
                    {
                        if (decimal.TryParse(data[value]?.ToString(), out decimal baseAmount))
                        {
                            records[newFcKey] = ConvertToSelectedCurrency(baseAmount);
                        }

                    }

                }

            }
          
            return records;
        }
        private decimal ConvertToSelectedCurrency(decimal value)
        {
            decimal results;
            string currency = cmb_currency.Text;
            string exchangeRate = txt_exchange_rate.Text;
            results = currency == "PHP" ? value : value * decimal.Parse(exchangeRate) ;
            if (results == 0)
            {
                results = 0.00m;
            }
            return results;
        }
        private void AllowDigitsOnTxtField(object sender, KeyPressEventArgs e)
        {
            TextBox txt = sender as TextBox;
            // Allow control keys (e.g., Backspace)
            if (char.IsControl(e.KeyChar))
                return;

            // Allow only digits
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
                return;
            }

            if (e.KeyChar == '.' && txt.Text.Contains("."))
            {
                e.Handled = true;
            }

        }

        private void GetPanelWithDigitValidationFields(Panel pnl)
        {
            foreach (Control controls in pnl.Controls)
            {
                if (controls is TextBox txtBox)
                {
                    txtBox.KeyPress += AllowDigitsOnTxtField;
                }
            }
        }
     
        private void GroupColumnBuffer()
        {
            typeof(DataGridView).InvokeMember("DoubleBuffered",
              System.Reflection.BindingFlags.NonPublic |
              System.Reflection.BindingFlags.Instance |
              System.Reflection.BindingFlags.SetProperty,
              null, dg_sales_invoice, new object[] { true });
        }

        private List<SalesInvoiceDetails> SaveSalesInvoiceDetails()
        {
            var salesInvoiceDetails = Helpers.ConvertDataGridViewToDataTable(dg_sales_invoice);
            List<SalesInvoiceDetails> listSalesInvoiceDetails =  new List<SalesInvoiceDetails>();
            SalesInvoiceDetails salesInvoiceDetailsModel;

            int id = 0;
            int sales_invoice_id;
            int order_details_id;
            int based_id;
            int item_id;
            int qty;
            string item_code;
            string item_desc;
            string uom_name;
            string deliver_date;
            string status;
            decimal unit_price;
            decimal total_cost;
            decimal discount;

            foreach (DataRow row in salesInvoiceDetails.Rows)
            {
                sales_invoice_id = 0;
                order_details_id = int.Parse(row["order_details_id"].ToString());
                based_id = int.Parse(row["based_id"].ToString());
                item_id  = int.Parse(row["item_id"].ToString());
                qty    = int.Parse(row["qty"].ToString());
                status = row["status"].ToString();
                item_code = row["item_code"].ToString();
                item_desc = row["item_desc"].ToString();
                uom_name  = row["uom_name"].ToString();
                deliver_date = row["delivery_date"].ToString();
                unit_price = decimal.Parse(row["unit_price"].ToString());
                total_cost = decimal.Parse(row["total_cost"].ToString());
                discount   = decimal.Parse(row["discount"].ToString());

                salesInvoiceDetailsModel = new SalesInvoiceDetails(id, sales_invoice_id, order_details_id, based_id, item_id, item_code, item_desc, qty, unit_price, uom_name,total_cost,discount,status,deliver_date);
                listSalesInvoiceDetails.Add(salesInvoiceDetailsModel);
            }

            return listSalesInvoiceDetails;
        }
        private void btn_add_customer_Click(object sender, EventArgs e)
        {
            string modaltitle = "Completely Delivered S.O";
            Dictionary<string, string> columnMappings = new Dictionary<string, string>
                {
                    {"order_id","ORDER ID" },
                    { "document_no", "Document No" },
                    { "company_name", "Company Name" },
                    { "sales_executive", "Sales Executive" },
                };

            SetupSelectionModal salesOrderModal = new SetupSelectionModal(modaltitle, ApiEndPoints.SALES_ORDER_DR, salesOrderWithDr, columnMappings, 0);
            DialogResult result = salesOrderModal.ShowDialog();

            if (result == DialogResult.OK)
            {
                Dictionary<string, string> salesOrderModalResult = salesOrderModal.GetResult();

                string orderId = "";
                bool hasSalesOrderID = salesOrderModalResult.TryGetValue("id", out orderId);
                if (!hasSalesOrderID)
                {
                    MessageBox.Show("No data found try again\n\n• " + "", "SMPC SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                FetchSalesInvoiceDocNo();
                LoadSalesOrderById(orderId);
            }
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            BtnToggle(true);
            ResetData();
        }

        private async void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                Panel[] pnlList = { pnl_header, pnl_footer };
                string[] salesInvoiceFieldToInt = { "customer_id", "payment_terms_id", "journal_id", "base_rate","tax_id" };
                string[] saleInvoiceFieldToFloat = { "exchange_rate" };

                var panelSalesInvoiceFooterData = Helpers.GetControlsValues(pnl_footer);
                var selectSalesInvoiceFooterKey = panelSalesInvoiceFooterData.Keys.ToArray();

                var records = Helpers.GetControlsValues(pnlList);
                bool isIntFieldsValid = Helpers.ConvertFieldPropertiesType<int>(records, salesInvoiceFieldToInt);
                bool isDecimalFieldsValid = Helpers.ConvertFieldPropertiesType<decimal>(records, saleInvoiceFieldToFloat);

                if (!isIntFieldsValid || !isDecimalFieldsValid)
                {
                    return;
                }
                var salesInvoicePayload = AddForeignCurrencyInSalesInvoice(records, selectSalesInvoiceFooterKey);
                salesInvoicePayload.Add("currency_code", cmb_currency.Text);
                salesInvoicePayload.Add("journal_name", cmb_journal.Text);
               
                var salesInvoiceDetails = SaveSalesInvoiceDetails();
                salesInvoicePayload.Add("sales_invoice_details", salesInvoiceDetails);
                
                var response = await salesInvoiceService.Insert(salesInvoicePayload);
                
                if (!response.Success)
                {
                    MessageBox.Show("ERROR IN SALES INVOICE");
                    return;
                }
                else
                {
                    SalesInvoiceRecords();
                    BtnToggle(false);
                    ResetData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in Button save ", ex.Message);
            }
           
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            BtnToggle(false);
            Helpers.ResetControls(pnl_header);
            Helpers.ResetControls(pnl_footer);
            ResetData();

        }

        private void dg_sales_invoice_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dg_sales_invoice_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && (e.ColumnIndex == 3 || e.ColumnIndex == 4)) // Qty & UOM
            {
                e.PaintBackground(e.CellBounds, true);
                e.PaintContent(e.CellBounds);
                e.Handled = true;
            }
        }

        private void dg_sales_invoice_Paint(object sender, PaintEventArgs e)
        {
            // Get header cell bounds for Qty and UOM
            Rectangle qtyHeader = dg_sales_invoice.GetCellDisplayRectangle(3, -1, true);
            Rectangle uomHeader = dg_sales_invoice.GetCellDisplayRectangle(4, -1, true);

            Rectangle groupHeader = Rectangle.Union(qtyHeader, uomHeader);

            // Draw group header
            using (SolidBrush backColorBrush = new SolidBrush(dg_sales_invoice.ColumnHeadersDefaultCellStyle.BackColor))
            {
                e.Graphics.FillRectangle(backColorBrush, groupHeader);
            }

            using (Pen borderPen = new Pen(dg_sales_invoice.GridColor))
            {
                e.Graphics.DrawRectangle(borderPen, groupHeader);
            }

            using (StringFormat format = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            })
            {
                e.Graphics.DrawString("Quantity", dg_sales_invoice.ColumnHeadersDefaultCellStyle.Font,
                    Brushes.Black, groupHeader, format);
            }
        }

    
        private void panel_footer_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txt_pwd_discount_Leave(object sender, EventArgs e)
        {

            string pwdDiscount = Helpers.GetCleanedPriceValue(txt_pwd_discount.Text);
            string amountNetVat = Helpers.GetCleanedPriceValue(txt_amount_net_vat.Text);
            string addVat = Helpers.GetCleanedPriceValue(txt_add_vat.Text);
            
            decimal amountDueResult =  decimal.Parse(amountNetVat) - decimal.Parse(pwdDiscount);
            decimal totalAmountDue = amountDueResult + decimal.Parse(addVat) ;
            Helpers.FormatAsCurrency(txt_amount_due, amountDueResult);
            Helpers.FormatAsCurrency(txt_total_amount_due, totalAmountDue);
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            if (this.salesInvoice.Rows.Count - 1 > this.selectedRecords)
            {
                this.selectedRecords++;
                BindSaleInvoiceRecords(true);
            }
            else
            {
                MessageBox.Show("No record found", "SMPC SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btn_prev_Click(object sender, EventArgs e)
        {
            if (this.selectedRecords > 0)
            {
                this.selectedRecords--;
                BindSaleInvoiceRecords(true);
            }
            else
            {
                MessageBox.Show("No record found", "SMPC SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void cmb_tax_code_TextChanged(object sender, EventArgs e)
        {
            

        }

        private void cmb_tax_code_SelectedIndexChanged(object sender, EventArgs e)
        {
            var rowSelected =cmb_tax.SelectedItem as DataRowView;
            if (rowSelected != null)
            {
                string name = rowSelected["code"].ToString();

                var tax_account = CacheData.TaxClassification.AsEnumerable().
                             FirstOrDefault(row => row.Field<string>("code").Equals(name));
                if (tax_account != null)
                {
                    txt_tax_account_id.Text = tax_account.Field<int>("account_id").ToString();
                }
                else
                {
                    MessageBox.Show("No matching tax account found. You need to bind a chart of account", "SMPC SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }      
        }
    }
}
