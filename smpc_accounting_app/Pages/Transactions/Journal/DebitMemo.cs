using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using smpc_accounting_app.Models;
using smpc_accounting_app.Pages.Transactions.AccountsPayable.APVoucher.APVoucherModals;
using smpc_accounting_app.Pages.Transactions.AccountsPayable.InvoiceReceipt.InvoiceReceiptModals;
using smpc_accounting_app.Pages.Transactions.Journal.DebitMemoModals;
using smpc_accounting_app.Services.Helpers;
using smpc_accounting_app.Services.Transactions;

namespace smpc_accounting_app.Pages.Transactions.Journal
{
    // Debit Memo, spec §5.19/§12.6. A/P only - no customer counterpart
    // (§14.56/§14.100: charging a customer more is a new Sales Invoice, not
    // a memo). Commits entirely on SAVE - no draft, no approval workflow,
    // ever (§14.57, §12.6.3). Unlike Credit Memo there is no "Edit" here at
    // all: the Go route group has no update endpoint (only GET/GET-by-id/
    // POST), so an already-saved record is permanently read-only - New is
    // the only way into edit mode. (Credit Memo's own btn_edit_Click lets
    // you re-enter edit on an already-saved record even though it has no
    // update endpoint either, which would silently create a duplicate on
    // Save rather than update - a separate bug, not repeated here.)
    public partial class DebitMemo : UserControl
    {
        private readonly DebitMemoService _service = new DebitMemoService();
        private readonly CreditMemoService _creditMemoService = new CreditMemoService();
        private List<DebitMemoModel> _records = new List<DebitMemoModel>();
        private int _currentIndex = -1;
        private DataTable _detailsTable;
        private bool _suppressGridEvents = false;

        // Always read-only regardless of edit mode - system-set (DOC NO.),
        // populated only via the supplier picker (never typed), or computed
        // (UNAPPLIED AMOUNT).
        private static readonly string[] AlwaysReadOnlyFields = {
            "txt_document_no", "txt_supplier_id", "txt_supplier_code", "txt_supplier_name", "txt_unapplied_amount"
        };

        public DebitMemo()
        {
            InitializeComponent();

            _detailsTable = BuildDetailsTable();
            dataGridView1.DataSource = _detailsTable;

            btn_new.Click += btn_new_Click;
            btn_search.Click += btn_search_Click;
            btn_prev.Click += btn_prev_Click;
            btn_next.Click += btn_next_Click;
            btn_save.Click += btn_save_Click;
            btn_cancel.Click += btn_cancel_Click;
            txt_supplier_code.Click += txt_supplier_code_Click;
            btn_add_invoice.Click += btn_add_invoice_Click;
            btn_add_credit_memo.Click += btn_add_credit_memo_Click;
            btn_remove_line.Click += btn_remove_line_Click;
            dataGridView1.CellValueChanged += dataGridView1_CellValueChanged;
            dataGridView1.CurrentCellDirtyStateChanged += dataGridView1_CurrentCellDirtyStateChanged;
            txt_trans_amount.TextChanged += (s, e) => RecalculateUnapplied();

            SetEditMode(false);
            this.Load += DebitMemo_Load;
        }

        // The apply-table grid built in code rather than relying on the
        // Designer's own DataTable schema - that one left every column
        // typeless (defaults to string), which breaks the APPLY column's
        // DataGridViewCheckBoxColumn binding (needs a real bool source).
        // TARGET_TYPE/TARGET_ID are backend-only metadata - dataGridView1
        // has AutoGenerateColumns = false and only defines the seven
        // visible columns, so these never render.
        private DataTable BuildDetailsTable()
        {
            var table = new DataTable();
            table.Columns.Add("APPLY", typeof(bool));
            table.Columns.Add("DOC NO.", typeof(string));
            table.Columns.Add("DUE DATE", typeof(string));
            table.Columns.Add("TOTAL", typeof(double));
            table.Columns.Add("OPEN AMOUNT", typeof(double));
            table.Columns.Add("AMOUNT APPLIED", typeof(double));
            table.Columns.Add("BALANCE", typeof(double));
            table.Columns.Add("TARGET_TYPE", typeof(string));
            table.Columns.Add("TARGET_ID", typeof(int));
            return table;
        }

        private async void DebitMemo_Load(object sender, EventArgs e)
        {
            await LoadRecordsAsync();
        }

        private async Task LoadRecordsAsync()
        {
            lbl_status.Text = "loading...";
            try
            {
                _records = (await _service.GetDebitMemos()).OrderByDescending(r => r.doc_no).ToList();
                _currentIndex = _records.Count > 0 ? 0 : -1;
                ShowCurrentRecord();
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to load debit memos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                lbl_status.Text = "";
            }
        }

        private void ShowCurrentRecord()
        {
            ClearForm();
            UpdateNavButtons();

            if (_currentIndex < 0 || _currentIndex >= _records.Count) return;

            var dm = _records[_currentIndex];
            txt_document_no.Text = dm.doc_no.ToString();
            txt_supplier_id.Text = dm.supplier_id.ToString();
            txt_supplier_code.Text = dm.supplier_code;
            txt_supplier_name.Text = dm.supplier_name;
            txt_trans_amount.Text = dm.trans_amount.ToString("N2");
            cmb_reason_code.Text = dm.reason_code;
            txt_currency.Text = dm.currency;
            txt_location_group.Text = dm.location_group;
            txt_sales_period.Text = dm.sales_period;
            txt_ref_doc_no.Text = dm.ref_doc_no;
            txt_ref_po_no.Text = dm.ref_po_no;
            if (DateTime.TryParse(dm.doc_date, out var docDate)) dtp_date.Value = docDate;

            _suppressGridEvents = true;
            _detailsTable.Rows.Clear();
            foreach (var d in dm.debit_memo_details ?? new List<DebitMemoDetailsModel>())
            {
                var row = _detailsTable.NewRow();
                row["APPLY"] = d.apply;
                row["DOC NO."] = d.target_doc_no;
                row["DUE DATE"] = d.due_date;
                row["TOTAL"] = d.total;
                row["OPEN AMOUNT"] = d.open_amount;
                row["AMOUNT APPLIED"] = d.amount_applied;
                row["BALANCE"] = d.balance;
                row["TARGET_TYPE"] = d.target_doc_type;
                row["TARGET_ID"] = d.target_doc_id;
                _detailsTable.Rows.Add(row);
            }
            _suppressGridEvents = false;

            txt_unapplied_amount.Text = dm.unapplied_amount.ToString("N2");
        }

        private void ClearForm()
        {
            txt_document_no.Text = "";
            txt_supplier_id.Text = "";
            txt_supplier_code.Text = "";
            txt_supplier_name.Text = "";
            txt_trans_amount.Text = "";
            cmb_reason_code.SelectedIndex = -1;
            txt_currency.Text = "";
            txt_location_group.Text = "";
            txt_sales_period.Text = "";
            txt_ref_doc_no.Text = "";
            txt_ref_po_no.Text = "";
            dtp_date.Value = DateTime.Now;
            txt_unapplied_amount.Text = "0.00";
            txt_unapplied_amount.ForeColor = Color.Black;

            _suppressGridEvents = true;
            _detailsTable.Rows.Clear();
            _suppressGridEvents = false;
        }

        private void UpdateNavButtons()
        {
            btn_prev.Enabled = _currentIndex > 0;
            btn_next.Enabled = _currentIndex >= 0 && _currentIndex < _records.Count - 1;
        }

        private void SetEditMode(bool enable)
        {
            Helpers.SetButtonVisibility(toolStrip1, panel3,
                visibleButtons: enable
                    ? new[] { "btn_save", "btn_cancel" }
                    : new[] { "btn_new", "btn_search", "btn_prev", "btn_next", "btn_print" },
                hiddenButtons: enable
                    ? new[] { "btn_new", "btn_search", "btn_prev", "btn_next", "btn_print" }
                    : new[] { "btn_save", "btn_cancel" });

            Helpers.SetChildControlsEnabled(new Control[] { panel3 }, !enable, AlwaysReadOnlyFields);

            dataGridView1.ReadOnly = !enable;
            btn_add_invoice.Enabled = enable;
            btn_add_credit_memo.Enabled = enable;
            btn_remove_line.Enabled = enable;
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            _currentIndex = -1;
            ClearForm();
            SetEditMode(true);
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            SetEditMode(false);
            if (_currentIndex < 0 && _records.Count > 0) _currentIndex = 0;
            ShowCurrentRecord();
        }

        private void btn_prev_Click(object sender, EventArgs e)
        {
            if (_currentIndex > 0) { _currentIndex--; ShowCurrentRecord(); }
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            if (_currentIndex < _records.Count - 1) { _currentIndex++; ShowCurrentRecord(); }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            // Not wired this pass - same deferral already accepted for Credit
            // Memo; Prev/Next already page through everything loaded.
        }

        private void txt_supplier_code_Click(object sender, EventArgs e)
        {
            if (!btn_save.Visible) return; // only pickable while in edit mode

            using (var modal = new InvoiceSearchSupplier())
            {
                if (modal.ShowDialog(this.FindForm()) == DialogResult.OK
                    && modal.SelectedSupplier != null && modal.SelectedSupplier.Rows.Count > 0)
                {
                    var row = modal.SelectedSupplier.Rows[0];
                    string newSupplierId = row["supplier_id"].ToString();

                    if (txt_supplier_id.Text != newSupplierId && _detailsTable.Rows.Count > 0)
                    {
                        var confirm = MessageBox.Show(
                            "Changing the supplier will clear the apply list - its lines belong to the previous supplier. Continue?",
                            "Change Supplier", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (confirm != DialogResult.Yes) return;

                        _suppressGridEvents = true;
                        _detailsTable.Rows.Clear();
                        _suppressGridEvents = false;
                        RecalculateUnapplied();
                    }

                    txt_supplier_id.Text = newSupplierId;
                    txt_supplier_code.Text = row["supplier_code"].ToString();
                    txt_supplier_name.Text = row["supplier"].ToString();
                }
            }
        }

        // Reuses AP Voucher's own existing Invoice Receipt picker directly -
        // sp_GetInvoiceAPVoucher already unions Invoice Receipt and Bulk
        // Invoice Receipt for a supplier, open-only (ap_voucher = 0), which
        // is exactly what a Debit Memo's apply line needs for either of
        // those two target types. No new Go endpoint, no new IR picker.
        private void btn_add_invoice_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txt_supplier_id.Text, out int supplierId) || supplierId == 0)
            {
                MessageBox.Show("Select a supplier first.", "Missing Supplier", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Already-added IR/Bulk-IR lines excluded so the same document
            // can't be picked twice - APVoucherInvoice already supports this
            // via ExistingIRs, keyed on invoice_receipt_id.
            var existing = new DataTable();
            existing.Columns.Add("invoice_receipt_id", typeof(int));
            foreach (DataRow row in _detailsTable.Rows)
            {
                string targetType = row["TARGET_TYPE"]?.ToString();
                if (targetType == "Invoice Receipt" || targetType == "Bulk Invoice Receipt")
                {
                    existing.Rows.Add(Convert.ToInt32(row["TARGET_ID"]));
                }
            }

            using (var modal = new APVoucherInvoice())
            {
                modal.SupplierId = supplierId;
                modal.ExistingIRs = existing;

                if (modal.ShowDialog(this.FindForm()) == DialogResult.OK
                    && modal.SelectedIR != null && modal.SelectedIR.Rows.Count > 0)
                {
                    var row = modal.SelectedIR.Rows[0];
                    int targetId = Convert.ToInt32(row["invoice_receipt_id"]);
                    string docNo = row["receipt_no"]?.ToString();
                    string dueDate = row["ir_due_date"]?.ToString();
                    double total = Convert.ToDouble(row["line_amount"]);
                    string targetType = NormalizeTargetDocType(row["receipt_type"]?.ToString());

                    AddApplyLine(targetType, targetId, docNo, dueDate, total);
                }
            }
        }

        // sp_GetInvoiceAPVoucher's receipt_type comes back shouting-case
        // ("INVOICE RECEIPT" / "BULK INVOICE RECEIPT") - the Go debit memo
        // service's validTargetDocTypes expects Title Case ("Invoice
        // Receipt" / "Bulk Invoice Receipt"), so normalize before it's ever
        // sent.
        private static string NormalizeTargetDocType(string receiptType)
        {
            switch ((receiptType ?? "").Trim().ToUpperInvariant())
            {
                case "INVOICE RECEIPT": return "Invoice Receipt";
                case "BULK INVOICE RECEIPT": return "Bulk Invoice Receipt";
                default: return receiptType;
            }
        }

        private async void btn_add_credit_memo_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txt_supplier_id.Text, out int supplierId) || supplierId == 0)
            {
                MessageBox.Show("Select a supplier first.", "Missing Supplier", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<CreditMemoModel> supplierCreditMemos;
            try
            {
                var all = await _creditMemoService.GetCreditMemos();
                // applied_by_dm excluded: a CM a previous Debit Memo already fully
                // applied has nothing left to offer (§12.6.3 - the server now sets
                // this flag on full consumption; see applyToTargetDocuments).
                supplierCreditMemos = all
                    .Where(c => c.partner_type == "Supplier" && c.partner_id == supplierId && !c.applied_by_dm)
                    .ToList();
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to load credit memos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (supplierCreditMemos.Count == 0)
            {
                MessageBox.Show("This supplier has no Credit Memos to apply.", "None Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var picker = new DebitMemoCreditMemoPicker(supplierCreditMemos))
            {
                if (picker.ShowDialog(this.FindForm()) == DialogResult.OK && picker.Selected != null)
                {
                    var cm = picker.Selected;

                    foreach (DataRow existingRow in _detailsTable.Rows)
                    {
                        if (existingRow["TARGET_TYPE"]?.ToString() == "Credit Memo" && Convert.ToInt32(existingRow["TARGET_ID"]) == cm.id)
                        {
                            MessageBox.Show("This Credit Memo is already on the apply list.", "Already Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }

                    AddApplyLine("Credit Memo", cm.id, "CM#" + cm.doc_no, cm.doc_date, cm.trans_amount);
                }
            }
        }

        // TOTAL and OPEN AMOUNT start equal - this codebase has no partial-
        // payment/running-balance tracking for an IR or "already applied"
        // tracking for a CM anywhere yet (confirmed against
        // sp_GetInvoiceAPVoucher.sql: it's an all-or-nothing ap_voucher
        // flag, not a running balance), so the full face amount is the only
        // figure available - not invented, it's what the one source that
        // exists actually provides. AMOUNT APPLIED defaults to whatever's
        // still needed to zero out UNAPPLIED AMOUNT, capped at this line's
        // own total.
        private void AddApplyLine(string targetType, int targetId, string docNo, string dueDate, double total)
        {
            double remaining = GetRemainingUnapplied();
            double amountApplied = Math.Max(0, Math.Min(remaining, total));

            _suppressGridEvents = true;
            var newRow = _detailsTable.NewRow();
            newRow["APPLY"] = true;
            newRow["DOC NO."] = docNo;
            newRow["DUE DATE"] = dueDate;
            newRow["TOTAL"] = total;
            newRow["OPEN AMOUNT"] = total;
            newRow["AMOUNT APPLIED"] = amountApplied;
            newRow["BALANCE"] = total - amountApplied;
            newRow["TARGET_TYPE"] = targetType;
            newRow["TARGET_ID"] = targetId;
            _detailsTable.Rows.Add(newRow);
            _suppressGridEvents = false;

            RecalculateUnapplied();
        }

        private void btn_remove_line_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow?.DataBoundItem is DataRowView rowView)
            {
                rowView.Row.Delete();
                RecalculateUnapplied();
            }
            else
            {
                MessageBox.Show("Select a line to remove first.", "No Line Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView1.IsCurrentCellDirty)
            {
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        // Apply is a required checkbox, not decorative - Amount Applied is
        // only meant to be editable once ticked (models.DebitMemoDetailsContent's
        // own comment). Unticking zeroes and locks it; ticking bounds entry
        // to the line's own Open Amount.
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (_suppressGridEvents || e.RowIndex < 0) return;

            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            if (colName != "aPPLYDataGridViewTextBoxColumn" && colName != "aMOUNTAPPLIEDDataGridViewTextBoxColumn") return;

            if (!(dataGridView1.Rows[e.RowIndex].DataBoundItem is DataRowView rowView)) return;
            DataRow dataRow = rowView.Row;

            bool applied = dataRow["APPLY"] is bool b && b;
            double openAmount = Convert.ToDouble(dataRow["OPEN AMOUNT"]);
            double amountApplied = Convert.ToDouble(dataRow["AMOUNT APPLIED"]);

            _suppressGridEvents = true;
            if (!applied)
            {
                dataRow["AMOUNT APPLIED"] = 0d;
                amountApplied = 0;
            }
            else if (amountApplied > openAmount)
            {
                dataRow["AMOUNT APPLIED"] = openAmount;
                amountApplied = openAmount;
            }
            else if (amountApplied < 0)
            {
                dataRow["AMOUNT APPLIED"] = 0d;
                amountApplied = 0;
            }
            dataRow["BALANCE"] = openAmount - amountApplied;
            _suppressGridEvents = false;

            RecalculateUnapplied();
        }

        private double GetAppliedTotal()
        {
            double applied = 0;
            foreach (DataRow row in _detailsTable.Rows)
            {
                if (row.RowState == DataRowState.Deleted) continue;
                if (row["APPLY"] is bool b && b)
                {
                    applied += Convert.ToDouble(row["AMOUNT APPLIED"]);
                }
            }
            return applied;
        }

        private double GetRemainingUnapplied()
        {
            double.TryParse(txt_trans_amount.Text, out double transAmount);
            return Math.Max(0, transAmount - GetAppliedTotal());
        }

        // §14.43 - must reach exactly 0 before Save is allowed. Red is this
        // codebase's standing attention colour (CLAUDE.md §1.4) - fits an
        // outstanding, not-yet-allocated amount as well as it fits anything
        // else that colour is used for.
        private void RecalculateUnapplied()
        {
            double.TryParse(txt_trans_amount.Text, out double transAmount);
            double unapplied = transAmount - GetAppliedTotal();
            txt_unapplied_amount.Text = unapplied.ToString("N2");
            txt_unapplied_amount.ForeColor = Math.Abs(unapplied) > 0.005 ? Color.Red : Color.Black;
        }

        private async void btn_save_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txt_supplier_id.Text, out int supplierId) || supplierId == 0)
            {
                MessageBox.Show("Select a supplier first.", "Missing Supplier", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmb_reason_code.SelectedIndex <= 0)
            {
                MessageBox.Show("Reason code is required (§14.58).", "Missing Reason Code", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!decimal.TryParse(txt_trans_amount.Text, out decimal transAmount) || transAmount <= 0)
            {
                MessageBox.Show("Enter a valid transaction amount.", "Invalid Amount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            double unapplied = (double)transAmount - GetAppliedTotal();
            if (Math.Abs(unapplied) > 0.005)
            {
                MessageBox.Show(
                    $"UNAPPLIED AMOUNT must reach 0 before this can be saved (currently {unapplied:N2}) - §14.43.",
                    "Amount Not Fully Applied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var details = new List<DebitMemoDetailsModel>();
            foreach (DataRow row in _detailsTable.Rows)
            {
                if (row.RowState == DataRowState.Deleted) continue;
                bool apply = row["APPLY"] is bool b && b;
                if (!apply) continue;

                double openAmount = Convert.ToDouble(row["OPEN AMOUNT"]);
                double amountApplied = Convert.ToDouble(row["AMOUNT APPLIED"]);

                details.Add(new DebitMemoDetailsModel
                {
                    apply = true,
                    target_doc_type = row["TARGET_TYPE"].ToString(),
                    target_doc_id = Convert.ToInt32(row["TARGET_ID"]),
                    target_doc_no = row["DOC NO."]?.ToString(),
                    due_date = row["DUE DATE"]?.ToString(),
                    total = Convert.ToDouble(row["TOTAL"]),
                    open_amount = openAmount,
                    amount_applied = amountApplied,
                    balance = openAmount - amountApplied
                });
            }

            if (details.Count == 0)
            {
                MessageBox.Show("Add at least one ticked apply line.", "No Apply Lines", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var payload = new DebitMemoBody
            {
                debit_memo = new DebitMemoModel
                {
                    supplier_id = supplierId,
                    supplier_code = txt_supplier_code.Text,
                    supplier_name = txt_supplier_name.Text,
                    trans_amount = (double)transAmount,
                    reason_code = cmb_reason_code.Text,
                    currency = txt_currency.Text,
                    location_group = txt_location_group.Text,
                    doc_date = dtp_date.Value.ToString("MM/dd/yyyy"),
                    sales_period = txt_sales_period.Text,
                    ref_doc_no = txt_ref_doc_no.Text,
                    ref_po_no = txt_ref_po_no.Text,
                    unapplied_amount = 0
                },
                debit_memo_details = details
            };

            lbl_status.Text = "saving...";
            btn_save.Enabled = false;
            btn_cancel.Enabled = false;
            try
            {
                var response = await _service.CreateDebitMemo(payload);
                if (response == null || !response.success)
                {
                    MessageBox.Show(response?.message ?? "Failed to save.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lbl_status.Text = "";
                    return;
                }

                lbl_status.Text = "saved";
                SetEditMode(false);
                await LoadRecordsAsync();
            }
            finally
            {
                btn_save.Enabled = true;
                btn_cancel.Enabled = true;
            }
        }
    }
}
