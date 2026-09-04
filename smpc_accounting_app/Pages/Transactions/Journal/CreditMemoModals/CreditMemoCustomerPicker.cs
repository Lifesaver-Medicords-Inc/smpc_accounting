using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using smpc_accounting_app.Models;
using smpc_accounting_app.Services;
using smpc_accounting_app.Services.Helpers;
using smpc_accounting_app.Shared;

namespace smpc_accounting_app.Pages.Transactions.Journal.CreditMemoModals
{
    // Partner picker for the CUSTOMER side of the Credit Memo screen (§5.18).
    //
    // Exists because reusing SalesInvoiceCustomer here was silently wrong: that
    // modal is backed by vw_get_customer, which exposes the PARENT tbl_bpi.id as
    // customer_id. Credit Memo's own server-side guard (partnerHasEntityType)
    // checks tbl_bpi_entity for a "CUS" row, and that table keys on
    // tbl_bpi_general.id (the branch) - so the parent id could never satisfy it,
    // and every customer Credit Memo failed with "partner <n> is not registered
    // as a Customer" even for customers that ARE correctly registered.
    //
    // This one is backed by vw_get_credit_memo_customer, which returns the branch
    // id as partner_id and is itself already filtered to partners holding "CUS" -
    // so nothing offered here can fail that guard. Supplier CMs were never
    // affected: vw_get_supplier_trade already exposes the branch id.
    //
    // Same shape as SalesInvoiceCustomer / DebitMemoCreditMemoPicker: search box
    // via Helpers.CreateSearchBox, single click selects and closes.
    public partial class CreditMemoCustomerPicker : Form
    {
        public CreditMemoCustomerViewModel Selected { get; private set; }

        private DataTable _customerTable;
        private const string PlaceholderText = "Customer Search...";

        public CreditMemoCustomerPicker()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterParent;
            dgv_customer_search.AutoGenerateColumns = false;

            InitializeSearchBox();
        }

        private void InitializeSearchBox()
        {
            txt_search = Helpers.CreateSearchBox(PlaceholderText, txt_search_TextChanged);
            this.Controls.Add(txt_search);
        }

        private async void CreditMemoCustomerPicker_Load(object sender, EventArgs e)
        {
            try
            {
                Helpers.Loading.ShowLoading(dgv_customer_search, "Fetching data...");
                await LoadCustomers();
            }
            catch (Exception ex)
            {
                Helpers.ShowDialogMessage("error", $"Failed to load: {ex.Message}");
            }
            finally
            {
                Helpers.Loading.HideLoading(dgv_customer_search);
            }
        }

        private async Task LoadCustomers()
        {
            var service = new GeneralService<CreditMemoCustomerViewModel>(ApiEndPoints.CREDIT_MEMO_CUSTOMERS);
            _customerTable = await service.GetAsDatatable();

            if (_customerTable != null && _customerTable.Rows.Count > 0)
            {
                dgv_customer_search.DataSource = _customerTable;
            }
            else
            {
                dgv_customer_search.DataSource = null;
                Helpers.ShowDialogMessage("error", "No customer found.");
            }
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            if (_customerTable == null || _customerTable.Rows.Count == 0) return;

            string searchText = txt_search.Text.Trim();

            if (string.IsNullOrEmpty(searchText) || searchText == PlaceholderText)
            {
                dgv_customer_search.DataSource = _customerTable;
            }
            else
            {
                var searched = Helpers.FilterDataTable(_customerTable, searchText,
                    "customer_code", "customer", "payment_term", "tax_code", "customer_address", "tin");
                dgv_customer_search.DataSource = searched;
            }
        }

        private void dgv_customer_search_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgv_customer_search.Rows[e.RowIndex];

            var idValue = row.Cells["partner_id"].Value;
            if (idValue == null || !int.TryParse(idValue.ToString(), out int partnerId) || partnerId == 0)
                return;

            Selected = new CreditMemoCustomerViewModel
            {
                partner_id = partnerId,
                customer = CellText(row, "customer"),
                customer_code = CellText(row, "customer_code"),
                payment_term = CellText(row, "payment_term"),
                tax_code = CellText(row, "tax_code"),
                customer_address = CellText(row, "customer_address"),
                tin = CellText(row, "tin")
            };

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private static string CellText(DataGridViewRow row, string columnName)
        {
            if (!row.DataGridView.Columns.Contains(columnName)) return string.Empty;
            return row.Cells[columnName].Value?.ToString() ?? string.Empty;
        }
    }
}
