using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using smpc_accounting_app.Models;
using smpc_accounting_app.Pages.Transactions.AccountsPayable.InvoiceReceipt.InvoiceReceiptModals;
using smpc_accounting_app.Pages.Transactions.AccountsReceivables.SalesInvoice.SalesInvoiceModals;
using smpc_accounting_app.Pages.Transactions.Journal.CreditMemoModals;
using smpc_accounting_app.Services.Helpers;
using smpc_accounting_app.Services.Transactions;

namespace smpc_accounting_app.Pages.Transactions.Journal
{
    // Credit Memo, spec §5.18/§12.6. One control serves both sides - PartnerType
    // is fixed by which menu entry constructed it (see RoutesService.cs: an
    // "Accounts Payables" entry passes "Supplier", an "Accounts Receivables"
    // one passes "Customer") and is never editable from within the form itself
    // (§14.98 - a control that lets someone pick is the one way this document
    // posts a customer credit as a payable).
    //
    // NOT YET WIRED: a dedicated search modal (btn_search is a no-op for now -
    // prev/next page through everything already loaded for this partner type)
    // and the ledger/receivable effect itself, which needs its own pass against
    // the live accounting code per CLAUDE.md's accounting-inverts-spec-wins
    // rule - approving here only flips the flag server-side, same as
    // ApproveCreditMemo's own doc comment on the Go side.
    public partial class CreditMemo : UserControl
    {
        private readonly string _partnerType;
        private readonly CreditMemoService _service = new CreditMemoService();
        private List<CreditMemoModel> _records = new List<CreditMemoModel>();
        private int _currentIndex = -1;

        // Fields that are always read-only regardless of edit mode - system-set
        // (DOC NO.), derived and never user-typed (PARTNER TYPE), or populated
        // only via the partner picker / approval flow rather than direct typing.
        private static readonly string[] AlwaysReadOnlyFields = {
            "txt_document_no", "txt_partner_type", "txt_partner_code",
            "txt_approved_by", "txt_approval_date"
        };

        public CreditMemo() : this("Supplier") { }

        public CreditMemo(string partnerType)
        {
            InitializeComponent();

            _partnerType = partnerType == "Customer" ? "Customer" : "Supplier";
            bool isCustomer = _partnerType == "Customer";

            txt_partner_type.Text = _partnerType;
            label1.Text = isCustomer ? "CUSTOMER CREDIT MEMO" : "CREDIT MEMO";

            // §14.100 - DM REFUND / REF. DM NO. are supplier-only.
            chk_dm_refund.Visible = !isCustomer;
            label_ref_dm_no.Visible = !isCustomer;
            txt_ref_dm_no.Visible = !isCustomer;

            // §5.18/§14.99 - the return references and the approval display are
            // customer-only.
            label_ref_srt_no.Visible = isCustomer;
            txt_ref_srt_no.Visible = isCustomer;
            label_ref_si_no.Visible = isCustomer;
            txt_ref_si_no.Visible = isCustomer;
            label_approved_by.Visible = isCustomer;
            txt_approved_by.Visible = isCustomer;
            label_approval_date.Visible = isCustomer;
            txt_approval_date.Visible = isCustomer;

            btn_new.Click += btn_new_Click;
            btn_search.Click += btn_search_Click;
            btn_prev.Click += btn_prev_Click;
            btn_next.Click += btn_next_Click;
            btn_save.Click += btn_save_Click;
            btn_cancel.Click += btn_cancel_Click;
            btn_approve.Click += btn_approve_Click;
            txt_partner_code.Click += txt_partner_code_Click;

            SetEditMode(false);
            this.Load += CreditMemo_Load;
        }

        private async void CreditMemo_Load(object sender, EventArgs e)
        {
            await LoadRecordsAsync();
        }

        private async Task LoadRecordsAsync()
        {
            lbl_status.Text = "loading...";
            try
            {
                var all = await _service.GetCreditMemos();

                // A Supplier Credit Memo screen has no business surfacing Customer
                // ones and vice versa - each menu entry only ever shows its own
                // side (§5.18's module-path split).
                _records = all.Where(r => r.partner_type == _partnerType)
                               .OrderByDescending(r => r.doc_no)
                               .ToList();
                _currentIndex = _records.Count > 0 ? 0 : -1;
                ShowCurrentRecord();
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to load credit memos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            var cm = _records[_currentIndex];
            txt_document_no.Text = cm.doc_no.ToString();
            txt_partner_id.Text = cm.partner_id.ToString();
            txt_partner_code.Text = cm.partner_code;
            txt_partner_name.Text = cm.partner_name;
            txt_trans_amount.Text = cm.trans_amount.ToString("N2");
            cmb_reason_code.Text = cm.reason_code;
            txt_currency.Text = cm.currency;
            txt_location_group.Text = cm.location_group;
            txt_sales_period.Text = cm.sales_period;
            if (DateTime.TryParse(cm.doc_date, out var docDate)) dtp_date.Value = docDate;

            if (_partnerType == "Customer")
            {
                txt_ref_srt_no.Text = cm.ref_srt_no;
                txt_ref_si_no.Text = cm.ref_si_no;
                txt_approved_by.Text = cm.approved_by_name;
                txt_approval_date.Text = cm.approval_date;
                btn_approve.Visible = !cm.is_approved;
            }
            else
            {
                chk_dm_refund.Checked = cm.dm_refund == true;
                txt_ref_dm_no.Text = cm.ref_dm_no;
                btn_approve.Visible = false;
            }
        }

        private void ClearForm()
        {
            txt_document_no.Text = "";
            txt_partner_id.Text = "";
            txt_partner_code.Text = "";
            txt_partner_name.Text = "";
            txt_trans_amount.Text = "";
            cmb_reason_code.SelectedIndex = -1;
            txt_currency.Text = "";
            txt_location_group.Text = "";
            txt_sales_period.Text = "";
            txt_ref_srt_no.Text = "";
            txt_ref_si_no.Text = "";
            chk_dm_refund.Checked = false;
            txt_ref_dm_no.Text = "";
            txt_approved_by.Text = "";
            txt_approval_date.Text = "";
            dtp_date.Value = DateTime.Now;
            btn_approve.Visible = false;
        }

        private void UpdateNavButtons()
        {
            btn_prev.Enabled = _currentIndex > 0;
            btn_next.Enabled = _currentIndex >= 0 && _currentIndex < _records.Count - 1;
        }

        private void SetEditMode(bool enable)
        {
            // btn_edit is deliberately never in the visible set. Neither Credit Memo
            // endpoint has an update route - a CM commits in full on Save (§14) - so
            // re-entering edit mode on an already-saved record and hitting Save would
            // silently call CreateCreditMemo again and produce a duplicate, not an
            // update. btn_new is the only way into edit mode, same as Debit Memo.
            Helpers.SetButtonVisibility(toolStrip1, panel3,
                visibleButtons: enable
                    ? new[] { "btn_save", "btn_cancel" }
                    : new[] { "btn_new", "btn_search", "btn_prev", "btn_next", "btn_print" },
                hiddenButtons: enable
                    ? new[] { "btn_new", "btn_search", "btn_prev", "btn_next", "btn_print", "btn_edit", "btn_approve" }
                    : new[] { "btn_save", "btn_cancel", "btn_edit" });

            Helpers.SetChildControlsEnabled(new Control[] { panel3 }, !enable, AlwaysReadOnlyFields);

            if (!enable && _currentIndex >= 0 && _currentIndex < _records.Count)
                btn_approve.Visible = _partnerType == "Customer" && !_records[_currentIndex].is_approved;
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
            // A dedicated search modal (by partner/doc no/date) is the natural next
            // step - not wired in this pass; PREV/NEXT already page through every
            // record loaded for this partner type.
        }

        private void txt_partner_code_Click(object sender, EventArgs e)
        {
            if (!btn_save.Visible) return; // only pickable while in edit mode

            if (_partnerType == "Supplier")
            {
                using (var modal = new InvoiceSearchSupplier())
                {
                    if (modal.ShowDialog(this.FindForm()) == DialogResult.OK
                        && modal.SelectedSupplier != null && modal.SelectedSupplier.Rows.Count > 0)
                    {
                        var row = modal.SelectedSupplier.Rows[0];
                        txt_partner_id.Text = row["supplier_id"].ToString();
                        txt_partner_code.Text = row["supplier_code"].ToString();
                        txt_partner_name.Text = row["supplier"].ToString();
                    }
                }
            }
            else
            {
                // CreditMemoCustomerPicker, NOT SalesInvoiceCustomer: the latter is
                // backed by vw_get_customer, whose customer_id is the PARENT
                // tbl_bpi.id. This screen's partner_id has to be the branch id
                // (tbl_bpi_general.id) because the server verifies it against
                // tbl_bpi_entity, which keys on bpi_general_id - feeding it the
                // parent id failed every customer Credit Memo with "partner <n> is
                // not registered as a Customer", even for correctly registered
                // customers (confirmed live on Bridge Inc: branch 40015 holds CUS,
                // parent 40026 holds nothing and never should). The supplier branch
                // above was never affected - vw_get_supplier_trade already returns
                // the branch id.
                using (var modal = new CreditMemoCustomerPicker())
                {
                    if (modal.ShowDialog(this.FindForm()) == DialogResult.OK
                        && modal.Selected != null)
                    {
                        txt_partner_id.Text = modal.Selected.partner_id.ToString();
                        txt_partner_code.Text = modal.Selected.customer_code;
                        txt_partner_name.Text = modal.Selected.customer;
                    }
                }
            }
        }

        private async void btn_save_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txt_partner_id.Text, out int partnerId) || partnerId == 0)
            {
                MessageBox.Show("Select a partner first.", "Missing Partner", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            var payload = new CreditMemoModel
            {
                partner_id = partnerId,
                partner_code = txt_partner_code.Text,
                partner_name = txt_partner_name.Text,
                partner_type = _partnerType,
                trans_amount = (double)transAmount,
                reason_code = cmb_reason_code.Text,
                currency = txt_currency.Text,
                location_group = txt_location_group.Text,
                doc_date = dtp_date.Value.ToString("MM/dd/yyyy"),
                sales_period = txt_sales_period.Text,
            };

            if (_partnerType == "Customer")
            {
                payload.ref_srt_no = txt_ref_srt_no.Text;
                payload.ref_si_no = txt_ref_si_no.Text;
            }
            else
            {
                payload.dm_refund = chk_dm_refund.Checked;
                payload.ref_dm_no = txt_ref_dm_no.Text;
            }

            lbl_status.Text = "saving...";
            btn_save.Enabled = false;
            btn_cancel.Enabled = false;
            try
            {
                var response = await _service.CreateCreditMemo(payload);
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

        private async void btn_approve_Click(object sender, EventArgs e)
        {
            if (_currentIndex < 0 || _currentIndex >= _records.Count) return;

            var confirm = MessageBox.Show(
                "Approve this Credit Memo? Only the COO may do this, and the receivable moves only once this runs (§14.99).",
                "Confirm Approval", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            btn_approve.Enabled = false;
            try
            {
                var response = await _service.ApproveCreditMemo(_records[_currentIndex].id);
                if (response == null || !response.success)
                {
                    MessageBox.Show(response?.message ?? "Failed to approve.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                await LoadRecordsAsync();
            }
            finally
            {
                btn_approve.Enabled = true;
            }
        }
    }
}
