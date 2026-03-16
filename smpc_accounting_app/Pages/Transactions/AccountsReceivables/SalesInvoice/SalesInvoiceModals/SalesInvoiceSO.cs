using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using smpc_accounting_app.Services;
using smpc_accounting_app.Models;
using smpc_accounting_app.Services.Helpers;
using smpc_accounting_app.Shared;
              
namespace smpc_accounting_app.Pages.Transactions.AccountsReceivables.SalesInvoice.SalesInvoiceModals
{
    public partial class SalesInvoiceSO : Form
    {
        public DataTable SelectedSOs { get; private set; } = null;
        public float? OverpaymentAmount { get; private set; } = null;
        private string placeHolderText = "Sales Order Search...";
        GeneralService<SalesOrderViewList> serviceSetup;
        private DataTable customerTable;
        private int _customerID;
        public HashSet<string> SelectedSOLabels { get; private set; } = new HashSet<string>();
        public HashSet<string> SelectedSalesPersonLabels { get; private set; } = new HashSet<string>();
        public DataTable ExistingSOs { get; set; }

        private List<SalesOrderViewModel> _parentdata;
        private List<SalesOrderDetailsViewModel> _childdata;
        private SalesOrderViewList _sodata;
        public decimal TotalAmount { get; private set; } = 0;

        public SalesInvoiceSO(int customerId)
        {
            InitializeComponent();

            // Center the modal relative to its parent form
            this.StartPosition = FormStartPosition.CenterParent;

            dgv_main_search.AutoGenerateColumns = false;
            InitializeSearchBox();

            _customerID = customerId;
            Helpers.DataGridViewFormatter.DataGridViewDecimalFormat(dgv_main_search, new[] { "total_sales" });
            Helpers.DataGridViewDocumentFormatter.DataGridViewDocumentFormat(dgv_main_search, "so_number", "SO");
        }

        private void InitializeSearchBox()
        {
            txt_search = Helpers.CreateSearchBox(placeHolderText, txt_search_TextChanged);
            this.Controls.Add(txt_search);
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            if (customerTable == null || customerTable.Rows.Count == 0)
                return;

            string searchText = txt_search.Text.Trim();

            if (string.IsNullOrEmpty(searchText) || searchText == placeHolderText)
            {
                dgv_main_search.DataSource = customerTable;
            }
            else
            {
                var searchedData = Helpers.FilterDataTable(customerTable, searchText,
                    "so_number", "doc_date", "customer_name", "net_amount");
                dgv_main_search.DataSource = searchedData;
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void SalesInvoiceSO_Load(object sender, EventArgs e)
        {
            try
            {
                Helpers.Loading.ShowLoading(dgv_main_search, "Fetching data...");
                await LoadSalesInvoice();
            }
            catch (Exception ex)
            {
                Helpers.ShowDialogMessage("error", $"Failed to load: {ex.Message}");
            }
            finally
            {
                Helpers.Loading.HideLoading(dgv_main_search);
            }
        }

        private async Task LoadSalesInvoice()
        {
            try
            {
                serviceSetup = new GeneralService<SalesOrderViewList>(ApiEndPoints.SALES_INVOICE_SO + _customerID);
                _sodata = await serviceSetup.GetAsModel();

                _parentdata = _sodata?.sales_order_view ;
                _childdata = _sodata?.sales_order_details_view;

                if (_childdata == null || !_childdata.Any())
                    throw new InvalidOperationException("No Sales Order found.");

                if (_parentdata == null || !_parentdata.Any())
                    throw new InvalidOperationException("No Sales Orders found.");

                customerTable = Helpers.ToDataTable(_parentdata);

                // If there are existing IRs, exclude them
                if (ExistingSOs != null && ExistingSOs.Rows.Count > 0)
                {
                    var usedIds = ExistingSOs.AsEnumerable()
                        .Where(r => r["sales_order_id"] != DBNull.Value)
                        .Select(r => Convert.ToInt32(r["sales_order_id"]))
                        .ToHashSet();

                    var filteredRows = customerTable.AsEnumerable()
                        .Where(r => !usedIds.Contains(Convert.ToInt32(r["sales_order_id"])));

                    customerTable = filteredRows.Any()
                        ? filteredRows.CopyToDataTable()
                        : customerTable.Clone(); // empty table with same structure
                }

                if (customerTable?.Rows.Count > 0)
                {
                    dgv_main_search.DataSource = customerTable;
                }
                else
                {
                    dgv_main_search.DataSource = null;
                    Helpers.ShowDialogMessage("error", "No Sales Order found.");
                    this.Close();
                }
            }
            catch (InvalidOperationException)
            {
                // Catch real unexpected errors
                Helpers.ShowDialogMessage("error", "No Sales Order found.");
                this.Close();
            }
            catch (Exception)
            {
                // Catch real unexpected errors
                Helpers.ShowDialogMessage("error", "No Sales Order found.");
                this.Close();
            }
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            SelectedSOLabels.Clear();

            if (customerTable == null || customerTable.Rows.Count == 0)
                return;

            //Collect selected sales order id
            var selectedIds = new HashSet<int>();
            decimal total = 0m;

            foreach (DataGridViewRow row in dgv_main_search.Rows)
            {
                if (row.IsNewRow) continue;

                bool isChecked = false;

                if (row.Cells["isSelected"].Value != null)
                {
                    bool.TryParse(row.Cells["isSelected"].Value.ToString(), out isChecked);
                }

                if (isChecked)
                {
                    // Get sales_order_id
                    if (row.Cells["sales_order_id"].Value != null)
                    {
                        int id = Convert.ToInt32(row.Cells["sales_order_id"].Value);
                        selectedIds.Add(id);
                    }

                    //SUM total_sales
                    if (row.Cells["total_sales"].Value != null &&
                        decimal.TryParse(row.Cells["total_sales"].Value.ToString(), out decimal rowAmount))
                    {
                        total += rowAmount;
                    }

                    // still collect doc_no if needed
                    var docNo = row.Cells["so_number"].Value?.ToString();
                    if (!string.IsNullOrEmpty(docNo))
                    {
                        SelectedSOLabels.Add(docNo); // HashSet automatically prevents duplicates
                    }

                    // still collect sales person if needed
                    var salesPerson = row.Cells["sales_person"].Value?.ToString();
                    if (!string.IsNullOrEmpty(salesPerson))
                    {
                        SelectedSalesPersonLabels.Add(salesPerson); // HashSet automatically prevents duplicates
                    }
                }
            }

            if (!selectedIds.Any())
            {
                Helpers.ShowDialogMessage("error", "Please select at least one Sales Order.");
                return;
            }

            // Store the computed total
            TotalAmount = total;

            // Filter child data based on selected sales order id
            var filteredChildData = _childdata
                ?.Where(c => selectedIds.Contains(c.sales_order_id))
                .ToList();

            if (filteredChildData == null || !filteredChildData.Any())
            {
                Helpers.ShowDialogMessage("error", "No matching Sales Order details found.");
                return;
            }

            // Convert filtered child data to DataTable
            SelectedSOs = Helpers.ToDataTable(filteredChildData);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
