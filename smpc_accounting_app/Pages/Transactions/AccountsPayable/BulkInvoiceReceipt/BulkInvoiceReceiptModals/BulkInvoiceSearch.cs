using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using smpc_accounting_app.Models;
using smpc_accounting_app.Services.Transactions;
using smpc_accounting_app.Services.Helpers;
using smpc_accounting_app.Services;
using smpc_accounting_app.Shared;

namespace smpc_accounting_app.Pages.Transactions.AccountsPayable.BulkInvoiceReceipt.BulkInvoiceReceiptModals
{
    public partial class BulkInvoiceSearch : Form
    {
        public string SelectedBIRId { get; private set; } = null;

        private string _placeHolderText = "Bulk Invoice Receipt Search...";
        private string _lastSearchText = "";

        // Cursor = id of the last record in the current page
        private int? _currentCursor = null;
        private bool _hasNext = false;
        private bool _isLoading = false;
        private const int _maxRows = 200;

        private DataTable _birTable;
        private System.Windows.Forms.Timer _searchDebounceTimer;
        private System.Windows.Forms.TextBox _txt_search;

        GeneralService<BulkInvoiceReceiptModel> bulkInvoiceSearchService;

        public BulkInvoiceSearch()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterParent;

            dgv_ir_search.AutoGenerateColumns = false;
            Helpers.DataGridViewDocumentFormatter.DataGridViewDocumentFormat(dgv_ir_search, "doc_no", "IR");
            Helpers.DataGridViewFormatter.DataGridViewDecimalFormat(dgv_ir_search, new[] { "net_amount" });

            InitializeSearchBox();
            InitializeDebounceTimer();

            dgv_ir_search.Scroll += dgv_ir_search_Scroll;
        }

        private void InitializeSearchBox()
        {
            _txt_search = Helpers.CreateSearchBox(_placeHolderText, txt_search_TextChanged);
            this.Controls.Add(_txt_search);
        }

        private void InitializeDebounceTimer()
        {
            _searchDebounceTimer = new System.Windows.Forms.Timer();
            _searchDebounceTimer.Interval = 400;
            _searchDebounceTimer.Tick += async (s, e) =>
            {
                _searchDebounceTimer.Stop();
                await ResetAndSearch();
            };
        }

        // ─── Keystroke → debounce → server search ────────────────────────────────
        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            _searchDebounceTimer.Stop();
            _searchDebounceTimer.Start();
        }

        // ─── Reset cursor + table, then fetch first page of new search ───────────
        private async Task ResetAndSearch()
        {
            string searchText = _txt_search.Text.Trim().Replace(" ", "_");

            if (searchText == _placeHolderText)
                searchText = "";

            if (searchText == _lastSearchText)
                return;

            _lastSearchText = searchText;

            _currentCursor = null;
            _hasNext = false;

            dgv_ir_search.DataSource = null;
            _birTable = _birTable != null ? _birTable.Clone() : null;

            await LoadNextPage();
        }

        // ─── Scroll: trigger next page when near the bottom ──────────────────────
        private async void dgv_ir_search_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation != ScrollOrientation.VerticalScroll)
                return;

            int lastVisible = dgv_ir_search.FirstDisplayedScrollingRowIndex
                              + dgv_ir_search.DisplayedRowCount(false);

            bool nearBottom = lastVisible >= dgv_ir_search.Rows.Count - 3;

            if (nearBottom && _hasNext && !_isLoading)
                await LoadNextPage();
        }

        // ─── Core loader: passes cursor (last id) + search term to the API ───────
        private async Task LoadNextPage()
        {
            if (_isLoading) return;
            _isLoading = true;

            try
            {
                Helpers.Loading.ShowLoading(dgv_ir_search, "Fetching data...");

                bulkInvoiceSearchService = new GeneralService<BulkInvoiceReceiptModel>(
                    ApiEndPoints.BULK_INVOICE_RECEIPT_SEARCH);

                // Pass cursor (last record id of previous page) and current search term
                var result = await bulkInvoiceSearchService.GetAsListSearch(
                    id: _currentCursor,
                    search: _lastSearchText);

                var newRecords = result.Data;
                _hasNext = result.Pagination?.has_next ?? false;

                if (newRecords == null || newRecords.Count == 0)
                {
                    if (_birTable == null || _birTable.Rows.Count == 0)
                        Helpers.ShowDialogMessage("error", "No bulk invoice receipts found.");

                    return;
                }

                // Advance cursor to the id of the last record in this batch
                _currentCursor = newRecords.LastOrDefault()?.id;

                // First page: build table from scratch. Subsequent pages: append rows.
                if (_birTable == null || _birTable.Rows.Count == 0)
                {
                    _birTable = Helpers.ToDataTable(newRecords);
                    dgv_ir_search.DataSource = _birTable; // bind once
                }
                else if (_birTable.Rows.Count < _maxRows)
                {
                    var tempTable = Helpers.ToDataTable(newRecords);
                    foreach (DataRow row in tempTable.Rows)
                        _birTable.ImportRow(row);
                }
            }
            catch (NullReferenceException)
            {
                Helpers.ShowDialogMessage("error", "No bulk invoice receipts found.");
            }
            catch (Exception ex)
            {
                Helpers.ShowDialogMessage("error", $"Failed to load: {ex.Message}");
            }
            finally
            {
                _isLoading = false;
                Helpers.Loading.HideLoading(dgv_ir_search);
            }
        }

        // ─── Initial load on form open ────────────────────────────────────────────
        private async void BulkInvoiceSearch_Load(object sender, EventArgs e)
        {
            try
            {
                await LoadNextPage();
            }
            catch (Exception ex)
            {
                Helpers.ShowDialogMessage("error", $"Failed to load: {ex.Message}");
            }
        }

        // ─── Row click: return selected record id to parent ───────────────────────
        private void dgv_ir_search_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgv_ir_search.Rows[e.RowIndex];
            var idValue = row.Cells["id"].Value;

            if (idValue != null)
            {
                SelectedBIRId = idValue.ToString();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void BulkInvoiceSearch_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_searchDebounceTimer != null)
            {
                _searchDebounceTimer.Stop();
                _searchDebounceTimer.Dispose();
                _searchDebounceTimer = null;
            }
        }
    }
}