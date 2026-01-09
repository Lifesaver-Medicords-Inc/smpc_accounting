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
using smpc_accounting_app.Services;
using smpc_accounting_app.Models;
using smpc_accounting_app.Services.Transactions;
using System.Globalization;
using smpc_accounting_app.Shared;

namespace smpc_accounting_app.Pages.Transactions.Journal
{
    public partial class JournalEntry : UserControl
    {
        JournalEntryService journalEntryService = new JournalEntryService();
        GeneralService<ChartOfAccountsModel> serviceSetup;
        private string placeHolderText = "Journal Entry Search...";
        private int _currentJEIndex = -1;
        private int _previousJEIndex = -1;
        private List<ChartOfAccountsModel> _coadata;
        private JournalEntryList _jedata;
        private List<JournalEntryModel> _journalEntries;
        private DataTable _jeTable;
        private BindingList<JournalEntryDetailsModel> _currentDetails;
        private string _userName = CacheData.CurrentUser.first_name + " " + CacheData.CurrentUser.last_name;
        private CompanySetupModel _companySetup = CacheData.CompanySetup;

        private bool _isNewMode = false;
        private bool _isEditMode = false;

        public JournalEntry()
        {
            InitializeComponent();

            Helpers.Placeholder.SetPlaceholder(txt_search, placeHolderText);
        }

        private void SetEditableColumns(bool isEdit)
        {
            var editableColumns = new[] { "inserted_date", "debit", "credit", "line_memo", "cmb_posting_ref" };

            foreach (var colName in editableColumns)
            {
                if (dgv_journal_entry.Columns.Contains(colName))
                    dgv_journal_entry.Columns[colName].ReadOnly = !isEdit;
            }
        }

        private void SetEditMode(bool enable, bool isNewMode = false)
        {
            _isNewMode = isNewMode;
            _isEditMode = !isNewMode && enable;

            SetEditableColumns(enable);
            dgv_journal_entry.AllowUserToAddRows = enable;

            // buttons
            string[] editButtons = { "btn_save", "btn_cancel" };
            string[] navButtons = { "btn_new", "btn_print", "btn_edit", "btn_delete", "btn_next", "btn_prev" };

            Helpers.SetButtonVisibility(
                toolStrip1,
                visibleButtons: enable ? editButtons : navButtons,
                hiddenButtons: enable ? navButtons : editButtons
            );

            Helpers.SetChildControlsEnabled(new[] { pnl_content }, enable, new string[] { "txt_doc_no", "txt_currency", "txt_search", "txt_period" });
        }

        private void ChangeRecord(int step)
        {
            if (_journalEntries == null || !_journalEntries.Any()) return;

            int newIndex = _currentJEIndex + step;
            if (newIndex >= 0 && newIndex < _journalEntries.Count)
            {
                _currentJEIndex = newIndex;
                ShowCurrentRecord();
            }
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            // Save current index before clearing
            _previousJEIndex = _currentJEIndex;
            SetEditMode(true, isNewMode: true);

            //Clear only the rows, keep columns
            dgv_journal_entry.DataSource = null;
            dgv_journal_entry.Rows.Clear();
            Helpers.ResetControls(new Panel[] { pnl_content });
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            if (_currentJEIndex < 0 || _journalEntries == null || !_journalEntries.Any())
            {
                Helpers.ShowDialogMessage("error", "No record selected to edit.");
                return;
            }

            // Store last viewed record index
            _previousJEIndex = _currentJEIndex;

            SetEditMode(true);
        }

        private void btn_prev_Click(object sender, EventArgs e)
        {
            ChangeRecord(-1);
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            ChangeRecord(1);
        }

        private async void btn_cancel_Click(object sender, EventArgs e)
        {
            SetEditMode(false);

            // Return to the previous record index if available
            if (_previousJEIndex >= 0 && _journalEntries != null && _journalEntries.Count > 0)
            {
                _currentJEIndex = _previousJEIndex;
                await LoadJournalEntries();
            }
        }

        private async void btn_delete_Click(object sender, EventArgs e)
        {
            if (_currentJEIndex < 0)
            {
                Helpers.ShowDialogMessage("error", "No record selected to delete.");
                return;
            }

            var current = _journalEntries[_currentJEIndex];

            var confirm = MessageBox.Show($"Are you sure you want to delete Journal Entry #{current.id}?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            try
            {
                Helpers.Loading.ShowLoading(dgv_journal_entry, "Deleting data...");

                var jeModel = new JournalEntryModel
                {
                    id = string.IsNullOrWhiteSpace(txt_id.Text) ? current.id : int.Parse(txt_id.Text),
                };

                var jePayload = new JournalEntryPayload
                {
                    journal_entry = jeModel
                };

                await journalEntryService.DeleteJERecord(jePayload);

                Helpers.ShowDialogMessage("success", "Journal Entry deleted successfully.");
            }
            catch (Exception ex)
            {
                Helpers.ShowDialogMessage("error", $"Failed to delete: {ex.Message}");
            }
            finally
            {
                await LoadJournalEntries();

                Helpers.Loading.HideLoading(dgv_journal_entry);
            }
        }

        private void ClearJournalEntryUI()
        {
            _journalEntries = new List<JournalEntryModel>();
            _currentJEIndex = -1;
            _previousJEIndex = -1;

            // Clear panel fields
            Helpers.ResetControls(new Panel[] { pnl_content });

            // Clear grid
            dgv_journal_entry.DataSource = null;
            dgv_journal_entry.Rows.Clear();

            // Disable navigation buttons
            btn_prev.Enabled = false;
            btn_next.Enabled = false;
        }

        private async void btn_save_Click(object sender, EventArgs e)
        {
            dgv_journal_entry.EndEdit();

            //Fill missing posting_date values
            FillMissingPostingDates();

            //Validate required controls in the panel
            if (Helpers.ValidateControlsValues(pnl_content))
            {
                Helpers.ShowDialogMessage("error", "Please fill in all required fields.");
                return;
            }

            //Validate Datagridview columns
            string[] columnsToValidate = { "account_title", "posting_ref" };
            if (await Helpers.ValidateDataGridViewCells(dgv_journal_entry, columnsToValidate))
                return;

            //Validate that journal entry details are not empty
            var journalEntryDetails = Helpers.DatagridviewMapper.BuildModelsFromData<JournalEntryDetailsModel>(dgv_journal_entry);
            if (journalEntryDetails == null || journalEntryDetails.Count == 0)
            {
                Helpers.ShowDialogMessage("error", "Journal Entry cannot be empty.");
                return;
            }

            foreach (DataGridViewRow row in dgv_journal_entry.Rows)
            {
                if (row.IsNewRow) continue;

                //Posting Date format validation (only if not empty)
                string postingDateValue = row.Cells["inserted_date"].Value?.ToString();
                if (!string.IsNullOrWhiteSpace(postingDateValue))
                {
                    if (!DateTime.TryParseExact(
                            postingDateValue,
                            "MM/dd/yyyy",
                            CultureInfo.InvariantCulture,
                            DateTimeStyles.None,
                            out _))
                    {
                        Helpers.ShowDialogMessage(
                            "error",
                            "Posting Date must be in MM/dd/yyyy format."
                        );
                        return;
                    }
                }

                // Debit / Credit "/" validation
                string debitText = row.Cells["debit"].Value?.ToString();
                string creditText = row.Cells["credit"].Value?.ToString();

                if ((debitText != null && debitText.Contains("/")) ||
                    (creditText != null && creditText.Contains("/")))
                {
                    Helpers.ShowDialogMessage(
                        "error",
                        "Please insert appropriate Debit and Credit amounts."
                    );
                    return;
                }

                //Debit / Credit validation
                decimal debit = 0;
                decimal credit = 0;

                decimal.TryParse(row.Cells["debit"].Value?.ToString(), out debit);
                decimal.TryParse(row.Cells["credit"].Value?.ToString(), out credit);

                if (debit <= 0 && credit <= 0)
                {
                    Helpers.ShowDialogMessage(
                        "error",
                        "Each journal entry row must have a value in either Debit or Credit."
                    );
                    return;
                }
            }

            // Assign created_by and posting reference info for each detail
            foreach (DataGridViewRow row in dgv_journal_entry.Rows)
            {
                if (row.IsNewRow) continue;

                var detail = journalEntryDetails[row.Index];

                detail.created_by = _userName;

                // Get posting_ref_id and posting_ref from the ComboBox column
                var postingRefCell = row.Cells["cmb_posting_ref"];
                if (postingRefCell.Value != null)
                {
                    int postingRefId = 0;
                    if (int.TryParse(postingRefCell.Value.ToString(), out postingRefId))
                    {
                        detail.posting_ref_id = postingRefId;

                        var selectedCoa = _coadata.FirstOrDefault(c => c.id == postingRefId);
                        if (selectedCoa != null)
                        {
                            detail.posting_ref = selectedCoa.code; // DisplayMember
                        }
                    }
                }
            }

            if (!ValidateDebitCreditBalance())
            {
                return;
            }

            var journalEntryParent = Helpers.BuildModelFromPanels<JournalEntryModel>(new Panel[] { pnl_content });
            journalEntryParent.created_by = _userName;
            journalEntryParent.currency = _companySetup.currency_code;
            journalEntryParent.period = _companySetup.start_fiscal_date + " to " + _companySetup.end_fiscal_date;

            // Wrap everything into Journal Entry Payload
            var jePayload = new JournalEntryPayload
            {
                journal_entry = journalEntryParent,
                journal_entry_details = journalEntryDetails
            };

            try
            {
                Helpers.Loading.ShowLoading(dgv_journal_entry, "Saving data...");

                if (_isNewMode)
                {
                    var result = await journalEntryService.CreateJERecord(jePayload);
                    Helpers.ShowDialogMessage("success", "Item Request created successfully.");
                }
                else
                {
                    var result = await journalEntryService.UpdateJERecord(jePayload);
                    Helpers.ShowDialogMessage("success", "Item Request updated successfully.");
                }
            }
            catch (Exception ex)
            {
                Helpers.ShowDialogMessage("error", $"Failed to save: {ex.Message}");
            }
            finally
            {
                SetEditMode(false);
                await LoadJournalEntries();

                Helpers.Loading.HideLoading(dgv_journal_entry);
            }
        }

        private bool ValidateDebitCreditBalance()
        {
            decimal totalDebit = 0;
            decimal totalCredit = 0;

            foreach (DataGridViewRow row in dgv_journal_entry.Rows)
            {
                if (row.IsNewRow) continue;

                decimal debit = 0;
                decimal credit = 0;

                decimal.TryParse(row.Cells["debit"].Value?.ToString(), out debit);
                decimal.TryParse(row.Cells["credit"].Value?.ToString(), out credit);

                totalDebit += debit;
                totalCredit += credit;
            }

            // Prevent floating-point rounding issues
            totalDebit = Math.Round(totalDebit, 2);
            totalCredit = Math.Round(totalCredit, 2);

            if (totalDebit != totalCredit)
            {
                Helpers.ShowDialogMessage(
                    "error",
                    $"Journal Entry is not balanced.\n\n" +
                    $"Total Debit: {totalDebit:N2}\n" +
                    $"Total Credit: {totalCredit:N2}"
                );
                return false;
            }

            return true;
        }

        private void FillMissingPostingDates()
        {
            string lastPostingDate = null;

            foreach (DataGridViewRow row in dgv_journal_entry.Rows)
            {
                if (row.IsNewRow) continue;

                var cell = row.Cells["posting_date"];
                var value = cell.Value?.ToString();

                // If this row HAS a posting_date → update lastPostingDate
                if (!string.IsNullOrWhiteSpace(value))
                {
                    lastPostingDate = value;
                }
                // If this row is BLANK but we already have a previous date → copy it
                else if (!string.IsNullOrWhiteSpace(lastPostingDate))
                {
                    cell.Value = lastPostingDate;
                }
            }
        }

        private async void JournalEntry_Load(object sender, EventArgs e)
        {
            try
            {
                Helpers.Loading.ShowLoading(dgv_journal_entry, "Fetching data...");
                await LoadJournalEntries();
            }
            catch (Exception ex)
            {
                Helpers.ShowDialogMessage("error", $"Failed to load: {ex.Message}");
            }
            finally
            {
                Helpers.Loading.HideLoading(dgv_journal_entry);
            }
        }

        private async Task LoadJournalEntries()
        {
            // save current index before reload
            int oldIndex = _currentJEIndex;

            //fill this declared value by the journal entries data
            _jedata = await journalEntryService.GetAsModel();
            serviceSetup = new GeneralService<ChartOfAccountsModel>(ApiEndPoints.CHART_OF_ACCOUNT_SETUP);
            _coadata = await serviceSetup.GetAsList();

            // Populate datagridview combo box posting reference
            if (_coadata != null && _coadata.Count > 0)
            {
                var postingRefColumn =
                    (DataGridViewComboBoxColumn)dgv_journal_entry.Columns["cmb_posting_ref"];

                postingRefColumn.DataSource = _coadata;
                postingRefColumn.ValueMember = "id";
                postingRefColumn.DisplayMember = "code";
                postingRefColumn.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                postingRefColumn.FlatStyle = FlatStyle.Flat;

                // THIS allows null values
                postingRefColumn.DefaultCellStyle.NullValue = "";
            }

            // Reverse order so newest records appear first
            _jedata.journal_entry.Reverse();

            if (_jedata != null && _jedata.journal_entry != null && _jedata.journal_entry.Count > 0)
            {
                //set this variable to the parent of the journal entry
                _journalEntries = _jedata.journal_entry;

                // restore old index if valid, otherwise fallback to 0
                if (oldIndex >= 0 && oldIndex < _journalEntries.Count)
                    _currentJEIndex = oldIndex;
                else
                    _currentJEIndex = 0;

                ShowCurrentRecord();
            }
            else
            {
                ClearJournalEntryUI();
            }
        }

        private void ShowCurrentRecord()
        {
            if (_currentJEIndex < 0 || _jedata == null || _jedata.journal_entry == null || !_jedata.journal_entry.Any())
                return;

            // Convert receiving report list to DataTable using helper
            _jeTable = Helpers.ToDataTable(_jedata.journal_entry);

            Helpers.BindControls(new Panel[] { pnl_content }, _jeTable, _currentJEIndex);

            //Disable auto column generation before setting the data source
            dgv_journal_entry.AutoGenerateColumns = false;

            var current = _journalEntries[_currentJEIndex];

            //Bind child details (grids)
            if (_jedata?.journal_entry_details != null)
            {
                _currentDetails = new BindingList<JournalEntryDetailsModel>(
                    _jedata.journal_entry_details
                        .Where(d => d.journal_entry_id == current.id)
                        .ToList()
                );

                dgv_journal_entry.DataSource = _currentDetails;

                foreach (DataGridViewRow row in dgv_journal_entry.Rows)
                {
                    if (row.IsNewRow) continue;

                    var detail = _currentDetails[row.Index];
                    if (detail.posting_ref_id > 0)
                    {
                        var postingRefColumn = dgv_journal_entry.Columns["cmb_posting_ref"] as DataGridViewComboBoxColumn;
                        if (postingRefColumn != null)
                        {
                            var matched = _coadata.FirstOrDefault(c => c.id == detail.posting_ref_id);
                            if (matched != null)
                            {
                                row.Cells["cmb_posting_ref"].Value = matched.id;
                            }
                        }
                    }
                }
            }
            else
            {
                dgv_journal_entry.DataSource = null;
            }

            //Enable/disable navigation buttons
            btn_prev.Enabled = _currentJEIndex > 0;
            btn_next.Enabled = _currentJEIndex < _journalEntries.Count - 1;
        }

        private void dgv_journal_entry_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            Helpers.HandleNumericColumns(dgv_journal_entry, e, new[] { "inserted_date", "debit", "credit" }, '/', '.');

            // Handle posting_ref keydown
            if (dgv_journal_entry.CurrentCell.OwningColumn.Name == "cmb_posting_ref"
                && e.Control is ComboBox combo)
            {
                // Avoid multiple subscriptions
                combo.KeyDown -= PostingRef_KeyDown;
                combo.KeyDown += PostingRef_KeyDown;
            }
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            if (_currentDetails == null || _currentDetails.Count == 0)
                return;

            string searchText = txt_search.Text.Trim().ToLower();

            // Restore original data when empty or placeholder
            if (string.IsNullOrEmpty(searchText) || searchText == placeHolderText)
            {
                dgv_journal_entry.DataSource = _currentDetails;
            }
            else
            {
                // Filter BindingList
                var filteredList = new BindingList<JournalEntryDetailsModel>(
                    _currentDetails
                        .Where(d =>
                            (d.posting_date != null && d.posting_date.ToLower().Contains(searchText)) ||
                            (d.account_title != null && d.account_title.ToLower().Contains(searchText)) ||
                            (d.posting_ref != null && d.posting_ref.ToLower().Contains(searchText)) ||
                            (d.debit.ToString().Contains(searchText)) ||
                            (d.credit.ToString().Contains(searchText)) ||
                            (d.line_memo != null && d.line_memo.ToLower().Contains(searchText))
                        ).ToList()
                );

                dgv_journal_entry.DataSource = filteredList;
            }

            // Rebind posting_ref ComboBox column to preserve the values
            if (_coadata != null && _coadata.Count > 0)
            {
                var postingRefColumn = dgv_journal_entry.Columns["cmb_posting_ref"] as DataGridViewComboBoxColumn;
                if (postingRefColumn != null)
                {
                    postingRefColumn.DataSource = null; // reset first
                    postingRefColumn.DataSource = _coadata;
                    postingRefColumn.ValueMember = "id";
                    postingRefColumn.DisplayMember = "code";
                    postingRefColumn.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                    postingRefColumn.FlatStyle = FlatStyle.Flat;
                    postingRefColumn.DefaultCellStyle.NullValue = "";
                }
            }

            // Restore the selected values for posting_ref
            foreach (DataGridViewRow row in dgv_journal_entry.Rows)
            {
                if (row.IsNewRow) continue;

                var detail = row.DataBoundItem as JournalEntryDetailsModel;
                if (detail != null && detail.posting_ref_id > 0)
                {
                    row.Cells["cmb_posting_ref"].Value = detail.posting_ref_id;
                }
            }
        }

        private void PostingRef_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Delete &&
                e.KeyCode != Keys.Back &&
                e.KeyCode != Keys.Escape)
                return;

            if (!(sender is ComboBox combo))
                return;

            var row = dgv_journal_entry.CurrentRow;
            if (row == null)
                return;

            e.Handled = true;
            e.SuppressKeyPress = true;

            // Defer clearing until AFTER DataGridView commits
            BeginInvoke(new Action(() =>
            {
                row.Cells["cmb_posting_ref"].Value = DBNull.Value;

                row.Cells["account_title"].Value = null;

                combo.SelectedIndex = -1;
                combo.Text = string.Empty;

                dgv_journal_entry.EndEdit();
            }));
        }

        private void dgv_journal_entry_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            var row = dgv_journal_entry.Rows[e.RowIndex];
            var columnName = dgv_journal_entry.Columns[e.ColumnIndex].Name;

            if (columnName == "inserted_date")
            {
                CopyInsertedDateToPostingDate(e.RowIndex);
                return;
            }

            if (columnName == "cmb_posting_ref")
            {
                var postingRefValue = row.Cells["cmb_posting_ref"].Value;

                if (int.TryParse(postingRefValue.ToString(), out int selectedId))
                {
                    var coa = _coadata.FirstOrDefault(c => c.id == selectedId);
                    if (coa != null)
                    {
                        row.Cells["account_title"].Value = coa.name;
                        row.Cells["posting_ref"].Value = coa.code;
                    }
                }
            }

            if (columnName == "debit" || columnName == "credit")
            {
                decimal debit = 0;
                decimal credit = 0;

                decimal.TryParse(row.Cells["debit"].Value?.ToString(), out debit);
                decimal.TryParse(row.Cells["credit"].Value?.ToString(), out credit);

                // If DEBIT has value → lock CREDIT
                if (debit > 0)
                {
                    row.Cells["credit"].Value = null;
                    row.Cells["credit"].ReadOnly = true;
                }
                else
                {
                    row.Cells["credit"].ReadOnly = false;
                }

                // If CREDIT has value → lock DEBIT
                if (credit > 0)
                {
                    row.Cells["debit"].Value = null;
                    row.Cells["debit"].ReadOnly = true;
                }
                else
                {
                    row.Cells["debit"].ReadOnly = false;
                }

                // Force repaint (for indentation + visual updates)
                dgv_journal_entry.InvalidateRow(e.RowIndex);
            }
        }

        private void CopyInsertedDateToPostingDate(int rowIndex)
        {
            if (rowIndex < 0) return;

            var row = dgv_journal_entry.Rows[rowIndex];

            var insertedDateValue = row.Cells["inserted_date"].Value;

            if (insertedDateValue != null && !string.IsNullOrWhiteSpace(insertedDateValue.ToString()))
            {
                row.Cells["posting_date"].Value = insertedDateValue;
            }
            else
            {
                row.Cells["posting_date"].Value = null;
            }
        }

        private void dgv_journal_entry_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgv_journal_entry.IsCurrentCellDirty)
            {
                dgv_journal_entry.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgv_journal_entry_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            var grid = sender as DataGridView;

            // Only apply to account_title column
            if (grid.Columns[e.ColumnIndex].Name != "account_title")
                return;

            var row = grid.Rows[e.RowIndex];

            decimal credit = 0;
            decimal.TryParse(row.Cells["credit"].Value?.ToString(), out credit);

            // If CREDIT has value → indent
            if (credit > 0)
            {
                row.Cells["account_title"].Style.Padding = new Padding(20, 0, 0, 0);
            }
            else
            {
                // Reset padding if no credit
                row.Cells["account_title"].Style.Padding = new Padding(0);
            }
        }
    }
}
