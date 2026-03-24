using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using smpc_accounting_app.Services.Helpers;
using smpc_accounting_app.Models;
using smpc_accounting_app.Services.Transactions;
using smpc_accounting_app.Shared;
using smpc_accounting_app.Printing;
using Microsoft.Reporting.WinForms;
using System.IO;
using smpc_accounting_app.Pages.Transactions.AccountsReceivables.SalesInvoice.SalesInvoiceModals;

namespace smpc_accounting_app.Pages.Transactions.AccountsReceivables.SalesInvoice
{
    public partial class SalesInvoicePage2 : UserControl
    {
        SalesInvoiceService2 salesInvoiceService = new SalesInvoiceService2();
        private int _currentSIIndex = -1;
        private int _previousSIIndex = -1;
        private SalesInvoice2List _sidata;
        private List<SalesInvoice2Model> _salesInvoice;
        private DataTable _siTable;
        private BindingList<SalesInvoiceDetails2Model> _currentDetails;
        private bool _isNewMode = false;
        private bool _isEditing = false;
        private string _userName;
        private string _currentJournal;
        private Panel[] _panels;
        private CurrencyRateModel _currencyChoice;

        //Dictionaries for the column grouping of datagridviews
        Dictionary<string, string[]> columnGroupsMain = new Dictionary<string, string[]>()
        {
            { "QTY", new string[] { "item_qty", "item_uom" } },
        };

        public SalesInvoicePage2()
        {
            InitializeComponent();

            Helpers.EnableGroupHeaders(dgv_main, columnGroupsMain);

            TextBox[] numberTextboxes = { txt_pwd_discount, txt_amount_due, txt_total_amount_due, txt_zero_sales,
            txt_vat_sales, txt_vat_exempt_sales, txt_total_sales, txt_less_vat, txt_net_vat, txt_add_vat, txt_overpayment_amount };

            Helpers.NumericTextBox.HandleNumericTextBox(numberTextboxes, '.');
            Helpers.TextboxFormatter.TextboxDecimalFormat(numberTextboxes);

            _panels = new Panel[] { pnl_main, pnl_footer };
            _userName = CacheData.CurrentUser.first_name + " " + CacheData.CurrentUser.last_name;
            _currentJournal = CacheData.CurrentJournal.journal_name;
            _currencyChoice = CacheData.CurrencyRate.rates;

            Helpers.DataGridViewFormatter.DataGridViewDecimalFormat(dgv_main, new[] { "unit_price", "discount", "total_cost" });
        }

        private void SetEditableColumns(bool isEdit)
        {
            var editableColumns = new[] { "item_qty" };

            foreach (var colName in editableColumns)
            {
                if (dgv_main.Columns.Contains(colName))
                {
                    var column = dgv_main.Columns[colName];

                    column.ReadOnly = !isEdit;

                    // Toggle background color based on readonly state
                    column.DefaultCellStyle.BackColor = column.ReadOnly ? Color.Gainsboro : Color.White;
                }
            }
        }

        private void SetEditMode(bool enable, bool isNewMode = false)
        {
            _isEditing = enable;
            _isNewMode = isNewMode;

            btn_ref_doc.Enabled = enable;
            btn_customer.Enabled = enable;

            SetEditableColumns(enable);

            // buttons
            string[] editButtons = { "btn_save", "btn_cancel", "btn_customer", "btn_ref_doc" };
            string[] navButtons = { "btn_new", "btn_print", "btn_edit", "btn_delete", "btn_next", "btn_prev", "btn_search" };

            Helpers.SetButtonVisibility(
                toolStrip1,
                pnl_main,
                visibleButtons: enable ? editButtons : navButtons,
                hiddenButtons: enable ? navButtons : editButtons
            );

            Helpers.SetChildControlsEnabledInclude(_panels, !enable, new string[] { "txt_reference_po", "cmb_currency" });

            if (enable)
            {
                LoadCurrencyChoices();
                cmb_currency.SelectedIndex = -1;
            }
            else
            {
                cmb_currency.DataSource = null;
                cmb_currency.Items.Clear();
            }
        }

        private void ChangeRecord(int step)
        {
            if (_salesInvoice == null || !_salesInvoice.Any()) return;

            int newIndex = _currentSIIndex + step;
            if (newIndex >= 0 && newIndex < _salesInvoice.Count)
            {
                _currentSIIndex = newIndex;
                ShowCurrentRecord();
            }
        }

        private void btn_prev_Click(object sender, EventArgs e)
        {
            ChangeRecord(-1);
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            ChangeRecord(1);
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            // Save current index before clearing
            _previousSIIndex = _currentSIIndex;
            SetEditMode(true, isNewMode: true);

            //Clear only the rows, keep columns
            _currentDetails = new BindingList<SalesInvoiceDetails2Model>();
            dgv_main.AutoGenerateColumns = false;
            dgv_main.DataSource = _currentDetails;
            Helpers.ResetControls(new Panel[] { pnl_main, pnl_footer });
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            if (_currentSIIndex < 0) return;

            var reportPath = Path.Combine(Application.StartupPath, "Printing", "AccountsReceivables", "SalesInvoiceReport.rdlc");

            // DEBUG CHECK
            if (!File.Exists(reportPath))
            {
                MessageBox.Show("RDLC file not found:\n" + reportPath);
                return;
            }

            var dataset2 = _currentDetails;
            SalesInvoice2Model dataset1 =  _salesInvoice[_currentSIIndex];

            // Format reference_doc_so the same way as in ShowCurrentRecord()
            if (!string.IsNullOrWhiteSpace(txt_reference_doc_so.Text))
            {
                var rawPOs = txt_reference_doc_so.Text;

                dataset1.reference_doc_so = string.Join(", ", rawPOs);
            }
            else
            {
                dataset1.reference_doc_so = string.Empty;
            }

            var dataSources = new List<ReportDataSource>()
            {
                new ReportDataSource("DataSet2", _currentDetails),
                new ReportDataSource("DataSet1", new List<SalesInvoice2Model> { _salesInvoice[_currentSIIndex] })
            };

            var preview = new PrintPreview(reportPath, dataSources);

            preview.ShowDialog();
        }

        private async void btn_cancel_Click(object sender, EventArgs e)
        {
            SetEditMode(false);

            // If no records exist, clear everything
            if (_salesInvoice == null || !_salesInvoice.Any())
            {
                ClearSalesInvoiceUI();
                return;
            }

            // Return to the previous record index if available
            if (_previousSIIndex >= 0 && _salesInvoice != null && _salesInvoice.Count > 0)
            {
                _currentSIIndex = _previousSIIndex;
                await LoadCurrencyChoices();
                await LoadSalesInvoices();
            }
        }

        private async void btn_search_Click(object sender, EventArgs e)
        {
            if (_salesInvoice == null || _salesInvoice.Count == 0)
            {
                await LoadSalesInvoices();
            }

            using (var searchForm = new SalesInvoiceSearch())
            {
                if (searchForm.ShowDialog(this) == DialogResult.OK && !string.IsNullOrEmpty(searchForm.SelectedSIId))
                {
                    if (int.TryParse(searchForm.SelectedSIId, out int selectedId))
                    {
                        int index = _salesInvoice.FindIndex(r => r.id == selectedId);
                        if (index >= 0)
                        {
                            _currentSIIndex = index;
                            await LoadSalesInvoices();
                        }
                    }
                    else
                    {
                        Helpers.ShowDialogMessage("error", "Invalid record ID selected.");
                    }
                }
            }
        }

        private async void btn_save_Click(object sender, EventArgs e)
        {
            btn_save.Enabled = false;
            btn_cancel.Enabled = false;

            try
            {
                dgv_main.EndEdit();

                // Compute totals first
                ComputeFooterAmounts();

                if (!string.IsNullOrWhiteSpace(txt_reference_doc_so.AccessibleDescription))
                {
                    txt_reference_doc_so.Text = txt_reference_doc_so.AccessibleDescription;
                }

                // VALIDATION moved here
                decimal totalAmountDue = decimal.TryParse(txt_total_amount_due.AccessibleDescription, out var totalDue) ? totalDue : 0;
                decimal addVatAmount = decimal.TryParse(txt_add_vat.AccessibleDescription, out var addVat) ? addVat : 0;
                decimal netVatAmount = decimal.TryParse(txt_net_vat.AccessibleDescription, out var netVat) ? netVat : 0;
                decimal pwdDiscountAmount = decimal.TryParse(txt_pwd_discount.Text, out var pwdDiscount) ? pwdDiscount : 0;

                if (Helpers.ZeroIfNearZero(addVatAmount) < 0)
                {
                    Helpers.ShowDialogMessage("error", "Additional VAT cannot be negative.");
                    return;
                }
                else if (Helpers.ZeroIfNearZero(totalAmountDue) <= 0)
                {
                    Helpers.ShowDialogMessage("error", "Total Amount Due must be greater than zero.");
                    return;
                }
                else if (Helpers.ZeroIfNearZero(pwdDiscountAmount - netVatAmount) > 0)
                {
                    Helpers.ShowDialogMessage("error", "PWD Discount cannot be greater than Net VAT.");
                    return;
                }

                // Validate required controls in selected panel
                bool hasError = Helpers.ValidateControlsValues(_panels);

                if (hasError) // if validation failed
                {
                    Helpers.ShowDialogMessage("error", "Please fill in all required fields.");
                    return;
                }

                string[] columnsToValidate = new[] { "item_code", "item_qty", "item_uom", "date_deliver", "unit_price" };

                if (await Helpers.ValidateDataGridViewCells(dgv_main, columnsToValidate))
                    return;

                var salesInvoiceParent = Helpers.BuildModelFromPanels<SalesInvoice2Model>(_panels);

                salesInvoiceParent.prepared_by = _userName;

                var salesInvoiceDetails = Helpers.DatagridviewMapper.BuildModelsFromData<SalesInvoiceDetails2Model>(dgv_main);

                //Check if sales invoice details is null or empty
                if (salesInvoiceDetails == null || salesInvoiceDetails.Count == 0)
                {
                    Helpers.ShowDialogMessage("error", "Please select at least one item.");
                    return;
                }

                // Wrap everything into Sales Invoice Payload
                var siPayload = new SalesInvoice2Payload
                {
                    sales_invoice2 = salesInvoiceParent,
                    sales_invoice_details2 = salesInvoiceDetails,
                };

                Helpers.Loading.ShowLoading(dgv_main, "Saving data...");

                var result = await salesInvoiceService.CreateSalesInvoiceRecord(siPayload);

                if (!result.success)
                {
                    Helpers.ShowDialogMessage("error", "Sales Invoice not created.");
                    return;
                }

                Helpers.ShowDialogMessage("success", "Sales Invoice created successfully.");

                SetEditMode(false);
                await LoadSalesInvoices();
            }
            catch (Exception ex)
            {
                Helpers.ShowDialogMessage("error", $"Failed to save: {ex.Message}");
            }
            finally
            {
                btn_save.Enabled = true;
                btn_cancel.Enabled = true;

                Helpers.Loading.HideLoading(dgv_main);
            }
        }

        private async void SalesInvoicePage2_Load(object sender, EventArgs e)
        {
            try
            {
                Helpers.Loading.ShowLoading(dgv_main, "Fetching data...");
                await LoadCurrencyChoices();
                await LoadSalesInvoices();
            }
            catch (Exception ex)
            {
                Helpers.ShowDialogMessage("error", $"Failed to load: {ex.Message}");
            }
            finally
            {
                Helpers.Loading.HideLoading(dgv_main);
            }
        }

        private async Task LoadSalesInvoices()
        {
            // save current index before reload
            int oldIndex = _currentSIIndex;

            //fill this declared value by the sales invoice data
            _sidata = await salesInvoiceService.GetAsModel();

            if (_sidata != null && _sidata.sales_invoice2 != null && _sidata.sales_invoice2.Count > 0)
            {
                //set this variable to the parent of the sales invoice
                _salesInvoice = _sidata.sales_invoice2;

                // restore old index if valid, otherwise fallback to 0
                if (oldIndex >= 0 && oldIndex < _salesInvoice.Count)
                    _currentSIIndex = oldIndex;
                else
                    _currentSIIndex = 0;

                ShowCurrentRecord();
            }
            else
            {
                ClearSalesInvoiceUI();
            }
        }

        private void ShowCurrentRecord()
        {
            if (_currentSIIndex < 0 || _sidata == null || _sidata.sales_invoice2 == null || !_sidata.sales_invoice2.Any())
                return;

            // Convert sales invoice list to DataTable using helper
            _siTable = Helpers.ToDataTable(_sidata.sales_invoice2);

            Helpers.BindControls(_panels, _siTable, _currentSIIndex);

            // Format txt_reference_doc_so from raw AccessibleDescription
            if (!string.IsNullOrWhiteSpace(txt_reference_doc_so.Text))
            {
                var rawPOs = txt_reference_doc_so.Text.Split(',')
                                .Select(p => p.Trim())
                                .Where(p => !string.IsNullOrEmpty(p))
                                .ToList();

                var formattedPOs = rawPOs.Select(p =>
                {
                    if (int.TryParse(p, out int poNumber))
                        return "SO" + poNumber.ToString("D8");
                    else
                        return "SO00000000";
                }).ToList();

                txt_reference_doc_so.Text = string.Join(", ", formattedPOs);
            }

            //Disable auto column generation before setting the data source
            dgv_main.AutoGenerateColumns = false;

            var current = _salesInvoice[_currentSIIndex];

            //Bind child details (grids)
            if (_sidata?.sales_invoice_details2 != null)
            {
                _currentDetails = new BindingList<SalesInvoiceDetails2Model>(
                    _sidata.sales_invoice_details2
                        .Where(d => d.sales_invoice_id == current.id)
                        .ToList()
                );

                dgv_main.DataSource = _currentDetails;
            }
            else
            {
                dgv_main.DataSource = null;
            }

            //Enable/disable navigation buttons
            btn_prev.Enabled = _currentSIIndex > 0;
            btn_next.Enabled = _currentSIIndex < _salesInvoice.Count - 1;
        }

        private void ClearSalesInvoiceUI()
        {
            _salesInvoice = new List<SalesInvoice2Model>();
            _currentSIIndex = -1;
            _previousSIIndex = -1;

            // Clear panel fields
            Helpers.ResetControls(_panels);

            // Clear grid
            dgv_main.DataSource = null;
            dgv_main.Rows.Clear();
            UpdatePwdDiscountState();

            // Disable navigation buttons
            btn_prev.Enabled = false;
            btn_next.Enabled = false;
        }

        private Task LoadCurrencyChoices()
        {
            if (_currencyChoice == null) return Task.CompletedTask;

            var currencyList = _currencyChoice
                .GetType()
                .GetProperties()
                .Select(p => p.Name)
                .ToList();

            cmb_currency.DataSource = currencyList;
            cmb_currency.SelectedIndex = -1;

            return Task.CompletedTask;
        }

        private void btn_customer_Click(object sender, EventArgs e)
        {
            using (var modal = new SalesInvoiceCustomer())
            {
                if (modal.ShowDialog(this) == DialogResult.OK && modal.SelectedCustomer != null)
                {
                    DataRow row = modal.SelectedCustomer.Rows[0];

                    // Assign values to controls
                    txt_customer_id.Text = row["customer_id"]?.ToString();
                    txt_customer.Text = row["customer"]?.ToString();
                    txt_customer_code.Text = row["customer_code"]?.ToString();
                    txt_payment_term.Text = row["payment_term"]?.ToString();
                    txt_tax_code.Text = row["tax_code"]?.ToString();
                    txt_customer_address.Text = row["customer_address"]?.ToString();
                    txt_overpayment_amount.Text = row["overpayment_amount"]?.ToString();
                    txt_tin.Text = row["tin"]?.ToString();

                    dgv_main.DataSource = null;
                    dgv_main.Rows.Clear();
                }
            }
        }

        private void cmb_currency_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_currency.SelectedIndex == -1 || _currencyChoice == null)
            {
                txt_exchange_rate.Clear();

                if(txt_base_rate.Text != null || txt_base_rate.Text != "")
                {
                    txt_base_rate.Clear();
                }

                return;
            }

            string selectedCurrency = cmb_currency.SelectedItem.ToString();

            var property = _currencyChoice.GetType().GetProperty(selectedCurrency);

            if (property != null)
            {
                var rateValue = property.GetValue(_currencyChoice);

                txt_exchange_rate.Text = rateValue?.ToString();
                txt_base_rate.Text = "1";
            }
        }

        private void btn_ref_doc_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_customer_id.Text))
            {
                Helpers.ShowDialogMessage("error", "Please select a customer first.");
                return;
            }

            if (!int.TryParse(txt_customer_id.Text, out int supplierId))
            {
                Helpers.ShowDialogMessage("error", "Invalid Customer selected.");
                return;
            }

            using (var modal = new SalesInvoiceSO(supplierId))
            {
                // Convert current dgv_main rows → DataTable
                modal.ExistingSOs = Helpers.ToDataTable(Helpers.DatagridviewMapper.BuildModelsFromData<SalesOrderDetailsViewModel>(dgv_main));

                if (modal.ShowDialog(this) == DialogResult.OK &&
                    modal.SelectedSOs != null &&
                    modal.SelectedSOs.Rows.Count > 0)
                {
                    //Bind SelectedRows to dgv_main
                    foreach (DataRow apvRow in modal.SelectedSOs.Rows)
                    {
                        int newRowIndex = dgv_main.Rows.Add();
                        var targetRow = dgv_main.Rows[newRowIndex];

                        targetRow.Cells["sales_order_details_id"].Value = apvRow["sales_order_details_id"];
                        targetRow.Cells["item_id"].Value = apvRow["item_id"];
                        targetRow.Cells["item_code"].Value = apvRow["item_code"];
                        targetRow.Cells["item_description"].Value = apvRow["item_description"];
                        targetRow.Cells["item_qty"].Value = apvRow["item_qty"];
                        targetRow.Cells["item_uom"].Value = apvRow["item_uom"];
                        targetRow.Cells["discount"].Value = apvRow["discount"];
                        targetRow.Cells["unit_price"].Value = apvRow["unit_price"];
                        targetRow.Cells["total_cost"].Value = apvRow["total_cost"];
                        targetRow.Cells["total_cost"].Value = apvRow["total_cost"];
                        targetRow.Cells["date_deliver"].Value = apvRow["date_deliver"];

                        txt_total_sales.Text = modal.TotalAmount.ToString("C2", System.Globalization.CultureInfo.GetCultureInfo("en-PH"));
                        txt_total_sales.AccessibleDescription = modal.TotalAmount.ToString(); // Store full precise value

                        // Check tax code before assigning VAT Sales
                        if (!string.IsNullOrWhiteSpace(txt_tax_code.Text) &&
                            txt_tax_code.Text.Equals("vat", StringComparison.OrdinalIgnoreCase))
                        {
                            txt_vat_sales.Text = modal.TotalAmount.ToString("C2",
                                System.Globalization.CultureInfo.GetCultureInfo("en-PH"));
                            txt_vat_sales.AccessibleDescription = modal.TotalAmount.ToString();
                        }
                        else
                        {
                            txt_vat_sales.Text = 0m.ToString("C2",
                                System.Globalization.CultureInfo.GetCultureInfo("en-PH"));
                            txt_vat_sales.AccessibleDescription = "0";
                        }
                    }

                    dgv_main.Refresh();
                    UpdatePwdDiscountState();

                    //Set Reference PO textbox
                    if (modal.SelectedSOLabels != null && modal.SelectedSOLabels.Any())
                    {
                        // Format each PO with prefix + 8 digits
                        var formattedPOs = modal.SelectedSOLabels.Select(po =>
                        {
                            if (int.TryParse(po, out int poNumber))
                                return "SO" + poNumber.ToString("D8"); // e.g., SO00000012
                            else
                                return "SO00000000"; // fallback for invalid numbers
                        }).ToList();

                        txt_reference_doc_so.Text = string.Join(", ", formattedPOs);
                        txt_reference_doc_so.AccessibleDescription = string.Join(", ", modal.SelectedSOLabels);
                    }
                    else
                    {
                        txt_reference_doc_so.Text = string.Empty;
                        txt_reference_doc_so.AccessibleDescription = string.Empty;
                    }

                    //Set Sales Person textbox
                    if (modal.SelectedSalesPersonLabels != null && modal.SelectedSalesPersonLabels.Any())
                    {
                        txt_sales_person.Text = string.Join(", ", modal.SelectedSalesPersonLabels);
                    }
                    else
                    {
                        txt_sales_person.Text = string.Empty;
                    }

                    txt_journal.Text = _currentJournal;

                    ComputeFooterAmounts();
                }
            }
        }

        private void dgv_main_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            Helpers.HandleNumericColumns(dgv_main, e, new[] { "item_qty" });
        }

        private void dgv_main_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // Prevent default crash dialog
            e.ThrowException = false;

            Helpers.ShowDialogMessage("error", "Invalid numeric value. Please enter a valid amount.");
        }

        private void txt_pwd_discount_Leave(object sender, EventArgs e)
        {
            ComputeFooterAmounts();
        }

        private void txt_pwd_discount_TextChanged(object sender, EventArgs e)
        {
            ComputeFooterAmounts();
        }

        private bool ComputeFooterAmounts()
        {
            if (!_isEditing)
                return true;

            // Use decimal for financial computations
            decimal pwdDiscountAmount = decimal.TryParse(txt_pwd_discount.Text, out var pwdDiscount) ? pwdDiscount : 0;
            decimal totalSalesAmount = decimal.TryParse(txt_total_sales.AccessibleDescription, out var totalSales) ? totalSales : 0;

            // Compute VAT depending on tax code
            decimal vatAmount = 0m;

            if (!string.IsNullOrWhiteSpace(txt_tax_code.Text) &&
                txt_tax_code.Text.Equals("vat", StringComparison.OrdinalIgnoreCase))
            {
                // VAT inclusive: extract VAT portion
                vatAmount = (totalSales / 1.12m) * 0.1m;
            }
            else
            {
                // Non-VAT: compute VAT directly
                vatAmount = totalSales * 0.1m;
            }


            // Display VAT with currency format
            txt_add_vat.Text = vatAmount.ToString("C2",System.Globalization.CultureInfo.GetCultureInfo("en-PH"));
            txt_add_vat.AccessibleDescription = vatAmount.ToString();

            txt_less_vat.Text = vatAmount.ToString("C2", System.Globalization.CultureInfo.GetCultureInfo("en-PH"));
            txt_less_vat.AccessibleDescription = vatAmount.ToString();

            // Compute Net VAT amount
            decimal netVatAmount = totalSalesAmount - vatAmount;

            // Display full precision
            txt_net_vat.Text = netVatAmount.ToString("C2", System.Globalization.CultureInfo.GetCultureInfo("en-PH"));
            txt_net_vat.AccessibleDescription = netVatAmount.ToString(); // Store full precise value

            // Compute Amount Due
            decimal amountDue =  netVatAmount - pwdDiscountAmount;

            // Display full precision
            txt_amount_due.Text = amountDue.ToString("C2", System.Globalization.CultureInfo.GetCultureInfo("en-PH"));
            txt_amount_due.AccessibleDescription = amountDue.ToString(); // Store full precise value

            // Compute Total Amount Due
            decimal totalAmountDue = amountDue + vatAmount;

            // Display full precision
            txt_total_amount_due.Text = totalAmountDue.ToString("C2", System.Globalization.CultureInfo.GetCultureInfo("en-PH"));
            txt_total_amount_due.AccessibleDescription = totalAmountDue.ToString(); // Store full precise value

            return true;
        }

        private void UpdatePwdDiscountState()
        {
            if (!_isEditing)
                return;

            // Enable only if there is at least one non-new row
            bool hasRows = dgv_main.Rows
                .Cast<DataGridViewRow>()
                .Any(r => !r.IsNewRow);

            txt_pwd_discount.Enabled = hasRows;

            if (!hasRows)
            {
                txt_pwd_discount.Text = null;
            }
        }
    }
}
