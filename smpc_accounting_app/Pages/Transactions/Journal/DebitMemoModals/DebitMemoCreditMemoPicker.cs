using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using smpc_accounting_app.Models;
using smpc_accounting_app.Services.Helpers;

namespace smpc_accounting_app.Pages.Transactions.Journal.DebitMemoModals
{
    // Debit Memo's picker for the "Credit Memo" apply-line target type (spec
    // §5.19) - a supplier Credit Memo being applied against what's owed.
    // Same shape as InvoiceSearchSupplier.cs: search box added via
    // Helpers.CreateSearchBox, single click selects and closes. Takes an
    // already-filtered list (caller scopes to partner_type == "Supplier" and
    // this memo's supplier - see DebitMemo.cs's btn_add_credit_memo_Click),
    // this modal doesn't fetch anything itself.
    //
    // CreditMemoModel.applied_by_dm now gates a fully-consumed CM out of the
    // list DebitMemo.cs passes in here (server sets it on full application,
    // §12.6.3). Remaining, real gap: a PARTIAL application still leaves this
    // false, since nothing in this codebase tracks a running balance for any
    // of the three DM apply-target types - see applyToTargetDocuments in the
    // Go service for the full explanation.
    public partial class DebitMemoCreditMemoPicker : Form
    {
        public CreditMemoModel Selected { get; private set; }

        private readonly List<CreditMemoModel> _creditMemos;
        private DataTable _table;
        private const string PlaceholderText = "Credit Memo Search...";

        public DebitMemoCreditMemoPicker(List<CreditMemoModel> creditMemos)
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterParent;
            _creditMemos = creditMemos ?? new List<CreditMemoModel>();

            _table = Helpers.ToDataTable(_creditMemos);
            dgv_credit_memo_search.DataSource = _table;

            InitializeSearchBox();
        }

        private void InitializeSearchBox()
        {
            txt_search = Helpers.CreateSearchBox(PlaceholderText, txt_search_TextChanged);
            this.Controls.Add(txt_search);
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            if (_table == null || _table.Rows.Count == 0) return;

            string searchText = txt_search.Text.Trim();

            if (string.IsNullOrEmpty(searchText) || searchText == PlaceholderText)
            {
                dgv_credit_memo_search.DataSource = _table;
            }
            else
            {
                var searched = Helpers.FilterDataTable(_table, searchText, "doc_no", "reason_code", "trans_amount", "doc_date");
                dgv_credit_memo_search.DataSource = searched;
            }
        }

        private void dgv_credit_memo_search_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var idValue = dgv_credit_memo_search.Rows[e.RowIndex].Cells["id"].Value;
            if (idValue == null || !int.TryParse(idValue.ToString(), out int id)) return;

            Selected = _creditMemos.FirstOrDefault(c => c.id == id);
            if (Selected == null) return;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
