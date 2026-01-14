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

namespace smpc_accounting_app.Pages.Transactions.Journal.JournalEntry
{
    public partial class JournalEntrySearch : Form
    {
        public string SelectedJEId { get; private set; } = null;
        private string placeHolderText = "Journal Entry Search...";
        private JournalEntryList JournalEntry;
        readonly JournalEntryService journalEntryService = new JournalEntryService();
        private DataTable jeTable;

        public JournalEntrySearch()
        {
            InitializeComponent();

            // Center the modal relative to its parent form
            this.StartPosition = FormStartPosition.CenterParent;

            InitializeSearchBox();
        }

        private void InitializeSearchBox()
        {
            txt_search = Helpers.CreateSearchBox(placeHolderText, txt_search_TextChanged);
            this.Controls.Add(txt_search);
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            if (jeTable == null || jeTable.Rows.Count == 0)
                return;

            string searchText = txt_search.Text.Trim();

            if (string.IsNullOrEmpty(searchText) || searchText == placeHolderText)
            {
                dgv_je_search.DataSource = jeTable;
            }
            else
            {
                var searchedData = Helpers.FilterDataTable(jeTable, searchText, "journal_name", "journal_description", "doc_no", "currency", "created_by");
                dgv_je_search.DataSource = searchedData;
            }
        }

        private async void JournalEntrySearch_Load(object sender, EventArgs e)
        {
            try
            {
                Helpers.Loading.ShowLoading(dgv_je_search, "Fetching data...");
                await JournalEntries();
            }
            catch (Exception ex)
            {
                Helpers.ShowDialogMessage("error", $"Failed to load: {ex.Message}");
            }
            finally
            {
                Helpers.Loading.HideLoading(dgv_je_search);
            }
        }

        private async Task JournalEntries()
        {
            JournalEntry = await journalEntryService.GetAsModel();

            JournalEntry.journal_entry.Reverse();

            // Convert journal entry list to DataTable using helper
            jeTable = Helpers.ToDataTable(JournalEntry.journal_entry);

            if (jeTable?.Rows.Count > 0)
            {
                dgv_je_search.DataSource = jeTable;
            }
            else
            {
                dgv_je_search.DataSource = null;
                Helpers.ShowDialogMessage("info", "No journal entry found.");
            }
        }

        private void dgv_je_search_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            var row = dgv_je_search.Rows[e.RowIndex];

            // Always get the id value from the row, regardless of which column was clicked
            var idValue = row.Cells["id"].Value;

            if (idValue != null)
            {
                SelectedJEId = idValue.ToString();

                this.DialogResult = DialogResult.OK; // close the modal with OK
                this.Close();
            }
        }
    }
}
