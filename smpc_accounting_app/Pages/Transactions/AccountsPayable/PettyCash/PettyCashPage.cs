using smpc_accounting_app.Models;
using smpc_accounting_app.Printing;
using smpc_accounting_app.Services;
using smpc_accounting_app.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace smpc_accounting_app.Pages.Transactions.AccountsPayable.PettyCash
{
    // Petty Cash Replenishment (spec 5.26). Kept by the custodian (the A/P
    // accountant) and approved by the COO. An imprest fund: one cycle is one
    // request, and New Cycle opens the next as a copy of the last with the
    // balance carried forward.
    //
    // CASH ON HAND is counted and typed; ACCOUNTABILITY is computed from it,
    // never the other way round. The request cannot be submitted until
    // CASH ON HAND + FOR REIMBURSEMENT equals the accountable fund - until it
    // does, the tally line reads red (2.9's exact allocation).
    //
    // A row with no document reference is accepted. Saving prompts about it and
    // carries on: a form that rejects those rows pushes real spending out of the
    // record entirely.
    public partial class PettyCashPage : UserControl
    {
        private List<PettyCashModel> _requests = new List<PettyCashModel>();
        private PettyCashModel _current = new PettyCashModel { status = "DRAFT" };
        private BindingList<PettyCashDetailModel> _details = new BindingList<PettyCashDetailModel>();
        private bool _loading;

        public PettyCashPage()
        {
            InitializeComponent();
            BuildColumns();

            btn_new.Click += async (s, e) => await NewCycleAsync();
            btn_save.Click += async (s, e) => await SaveAsync("DRAFT");
            btn_submit.Click += async (s, e) => await SaveAsync("SUBMITTED");
            btn_approve.Click += async (s, e) => await ApproveAsync();
            btn_delete.Click += async (s, e) => await DeleteAsync();
            btn_print.Click += (s, e) => Report()?.ShowPreview();
            btn_export_excel.Click += btn_export_excel_Click;
            btn_add_row.Click += (s, e) => AddRow(false);
            btn_add_encashment.Click += (s, e) => AddRow(true);
            btn_delete_row.Click += btn_delete_row_Click;
            cmb_request.SelectedIndexChanged += cmb_request_SelectedIndexChanged;
            dgv_details.CellEndEdit += (s, e) => Recompute();
            txt_cash_on_hand.TextChanged += (s, e) => Recompute();
            txt_deductions.TextChanged += (s, e) => Recompute();
            txt_accountable_fund.TextChanged += (s, e) => Recompute();
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await LoadAsync();
        }

        private void BuildColumns()
        {
            dgv_details.AutoGenerateColumns = false;
            dgv_details.Columns.Clear();
            dgv_details.Columns.Add(new DataGridViewTextBoxColumn
            { Name = "date", DataPropertyName = "date", HeaderText = "DATE", FillWeight = 60 });
            dgv_details.Columns.Add(new DataGridViewTextBoxColumn
            { Name = "requester", DataPropertyName = "requester", HeaderText = "REQUESTER", FillWeight = 90 });
            dgv_details.Columns.Add(new DataGridViewTextBoxColumn
            { Name = "description", DataPropertyName = "description", HeaderText = "DESCRIPTION", FillWeight = 180 });
            dgv_details.Columns.Add(new DataGridViewTextBoxColumn
            { Name = "reference", DataPropertyName = "reference", HeaderText = "DOCUMENT REFERENCE", FillWeight = 150 });

            var amount = new DataGridViewTextBoxColumn
            { Name = "amount", DataPropertyName = "amount", HeaderText = "AMOUNT", FillWeight = 70 };
            amount.DefaultCellStyle.Format = "N2";
            amount.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_details.Columns.Add(amount);

            dgv_details.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "is_encashment",
                DataPropertyName = "is_encashment",
                HeaderText = "ENCASHMENT OFFSET",
                FillWeight = 80,
            });
        }

        private async System.Threading.Tasks.Task LoadAsync()
        {
            var service = new GeneralService<PettyCashModel>(ApiEndPoints.PETTY_CASH);
            _requests = await service.GetAsList() ?? new List<PettyCashModel>();

            _loading = true;
            cmb_request.Items.Clear();
            foreach (var r in _requests)
                cmb_request.Items.Add("PC#" + r.doc_no.ToString("D4") + "  -  " + (r.cut_off_date ?? "") + "  (" + r.status + ")");
            _loading = false;

            if (_requests.Count > 0)
            {
                cmb_request.SelectedIndex = 0;
            }
            else
            {
                await NewCycleAsync();
            }
        }

        private void cmb_request_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_loading || cmb_request.SelectedIndex < 0 || cmb_request.SelectedIndex >= _requests.Count) return;
            _current = _requests[cmb_request.SelectedIndex];
            ShowCurrent();
        }

        private async System.Threading.Tasks.Task NewCycleAsync()
        {
            var service = new GeneralService<PettyCashModel>(ApiEndPoints.PETTY_CASH_NEXT_CYCLE);
            _current = await service.GetAsModel() ?? new PettyCashModel { status = "DRAFT" };
            _current.id = 0;
            _current.cut_off_date = DateTime.Now.ToString("yyyy-MM-dd");
            ShowCurrent();
        }

        private void ShowCurrent()
        {
            _loading = true;
            DateTime cutOff;
            dtp_cut_off.Value = DateTime.TryParse(_current.cut_off_date, out cutOff) ? cutOff : DateTime.Now;
            txt_accountable_fund.Text = _current.accountable_fund.ToString("N2");
            txt_opening_fund.Text = _current.opening_fund.ToString("N2");
            txt_cash_on_hand.Text = _current.cash_on_hand.ToString("N2");
            txt_deductions.Text = _current.deductions.ToString("N2");
            lbl_status.Text = _current.status ?? "DRAFT";
            _details = new BindingList<PettyCashDetailModel>(_current.details ?? new List<PettyCashDetailModel>());
            dgv_details.DataSource = _details;
            _loading = false;

            bool approved = (_current.status ?? "") == "APPROVED";
            pnl_header.Enabled = !approved;
            dgv_details.ReadOnly = approved;
            toolStrip2.Enabled = !approved;
            btn_save.Enabled = !approved;
            btn_submit.Enabled = !approved;
            btn_approve.Enabled = !approved;

            Recompute();
        }

        private static double Number(string text)
        {
            double value;
            return double.TryParse((text ?? "").Replace(",", ""), NumberStyles.Any, CultureInfo.InvariantCulture, out value) ? value : 0;
        }

        // TOTAL EXPENSE + ENCASHMENT (negative) + deductions = FOR REIMBURSEMENT;
        // plus the counted cash on hand = ACCOUNTABILITY (spec 5.26).
        private void Recompute()
        {
            if (_loading) return;

            double expense = _details.Where(d => !d.is_encashment).Sum(d => d.amount);
            double encashment = _details.Where(d => d.is_encashment).Sum(d => d.amount);
            double deductions = Number(txt_deductions.Text);
            double cashOnHand = Number(txt_cash_on_hand.Text);
            double accountable = Number(txt_accountable_fund.Text);

            double forReimbursement = expense + encashment + deductions;
            double accountability = forReimbursement + cashOnHand;

            lbl_total_expense.Text = expense.ToString("N2");
            lbl_encashment.Text = encashment.ToString("N2");
            lbl_for_reimbursement.Text = forReimbursement.ToString("N2");
            lbl_accountability.Text = accountability.ToString("N2");

            // The tally check: until cash on hand plus the request equals the fund
            // the custodian answers for, the sheet is unreconciled and reads red.
            double difference = cashOnHand + forReimbursement - accountable;
            if (Math.Abs(difference) <= 0.005)
            {
                lbl_tally.ForeColor = System.Drawing.SystemColors.ControlText;
                lbl_tally.Text = "Tally OK: cash on hand + for reimbursement = accountable fund.";
            }
            else
            {
                lbl_tally.ForeColor = System.Drawing.Color.Red;
                lbl_tally.Text = "Out by " + difference.ToString("N2") +
                    " - cash on hand + for reimbursement must equal the accountable fund before submitting.";
            }
        }

        private void AddRow(bool encashment)
        {
            if ((_current.status ?? "") == "APPROVED") return;

            _details.Add(new PettyCashDetailModel
            {
                date = DateTime.Now.ToString("yyyy-MM-dd"),
                is_encashment = encashment,
                // The offset is a single negative line citing the cheque and the POs
                // it covered (spec 5.26).
                description = encashment ? "Encashment offset - cheque no., cheque date, PO nos." : "",
            });
            Recompute();
        }

        private void btn_delete_row_Click(object sender, EventArgs e)
        {
            var row = dgv_details.CurrentRow?.DataBoundItem as PettyCashDetailModel;
            if (row == null || (_current.status ?? "") == "APPROVED") return;
            _details.Remove(row);
            Recompute();
        }

        private void Collect()
        {
            _current.cut_off_date = dtp_cut_off.Value.ToString("yyyy-MM-dd");
            _current.accountable_fund = Number(txt_accountable_fund.Text);
            _current.opening_fund = Number(txt_opening_fund.Text);
            _current.cash_on_hand = Number(txt_cash_on_hand.Text);
            _current.deductions = Number(txt_deductions.Text);
            _current.details = _details.ToList();

            // The derived figures too, so a print of an unsaved draft shows what the
            // screen shows. The API recomputes all four on save and its answer wins.
            _current.total_expense = _details.Where(d => !d.is_encashment).Sum(d => d.amount);
            _current.encashment = _details.Where(d => d.is_encashment).Sum(d => d.amount);
            _current.for_reimbursement = _current.total_expense + _current.encashment + _current.deductions;
            _current.accountability = _current.for_reimbursement + _current.cash_on_hand;

            if (string.IsNullOrWhiteSpace(_current.prepared_by))
            {
                var user = CacheData.CurrentUser;
                _current.prepared_by = user == null ? "" : (user.first_name + " " + user.last_name).Trim();
            }
        }

        private async System.Threading.Tasks.Task SaveAsync(string status)
        {
            dgv_details.EndEdit();
            Collect();

            // Prompt, never block: fares, meals and unrequisitioned purchases often
            // have no document at all (spec 5.26).
            int missing = _details.Count(d => string.IsNullOrWhiteSpace(d.reference));
            if (missing > 0)
            {
                var answer = MessageBox.Show(
                    missing + " row(s) have no document reference. Save anyway?",
                    "Petty Cash", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (answer != DialogResult.Yes) return;
            }

            _current.status = status;
            var service = new GeneralService<PettyCashModel>(ApiEndPoints.PETTY_CASH);
            var result = _current.id == 0 ? await service.Insert(_current) : await service.Update(_current);

            if (result == null || !result.Success)
            {
                MessageBox.Show(result?.message ?? "Saving the request failed.", "Petty Cash",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            await LoadAsync();
        }

        // The COO's step: the fund goes back to its ceiling.
        private async System.Threading.Tasks.Task ApproveAsync()
        {
            if (_current.id == 0)
            {
                MessageBox.Show("Save the request first.", "Petty Cash", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var user = CacheData.CurrentUser;
            var service = new GeneralService<PettyCashApproveModel>(ApiEndPoints.PETTY_CASH_APPROVE);
            var result = await service.Insert(new PettyCashApproveModel
            {
                id = _current.id,
                approved_by = user == null ? "" : (user.first_name + " " + user.last_name).Trim(),
                approved_date = DateTime.Now.ToString("yyyy-MM-dd"),
            });

            if (result == null || !result.Success)
            {
                MessageBox.Show(result?.message ?? "Approving the request failed.", "Petty Cash",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            await LoadAsync();
        }

        private async System.Threading.Tasks.Task DeleteAsync()
        {
            if (_current.id == 0) return;
            if (MessageBox.Show("Delete this request?", "Petty Cash",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            var service = new GeneralService<PettyCashModel>(ApiEndPoints.PETTY_CASH + "/" + _current.id);
            if (!await service.Delete(_current))
            {
                MessageBox.Show("Deleting the request failed.", "Petty Cash",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            await LoadAsync();
        }

        // Class A house template, with the sheet's own figures under the table and
        // the custodian / COO signature blocks (spec 5.26, 2.10).
        private HouseTemplateReport Report()
        {
            dgv_details.EndEdit();
            Collect();

            var report = new HouseTemplateReport { Title = "PETTY CASH REPLENISHMENT" };

            report.LeftBlock.Add(HouseTemplateReport.Pair("CUT-OFF DATE", _current.cut_off_date));
            report.LeftBlock.Add(HouseTemplateReport.Pair("OPENING FUND", _current.opening_fund.ToString("N2")));
            report.LeftBlock.Add(HouseTemplateReport.Pair("ACCOUNTABLE FUND", _current.accountable_fund.ToString("N2")));

            report.RightBlock.Add(HouseTemplateReport.Pair("DOC NO.", "PC#" + _current.doc_no.ToString("D4")));
            report.RightBlock.Add(HouseTemplateReport.Pair("STATUS", _current.status));
            report.RightBlock.Add(HouseTemplateReport.Pair("CASH ON HAND", _current.cash_on_hand.ToString("N2")));

            report.Columns.Add(new HouseTemplateColumn("DATE"));
            report.Columns.Add(new HouseTemplateColumn("PARTICULAR"));
            report.Columns.Add(new HouseTemplateColumn("AMOUNT", 'R'));

            foreach (var d in _details)
            {
                // PARTICULAR reads "<requester>- <description> <references>" (5.26).
                string particular = (d.requester ?? "").Trim() + "- " + (d.description ?? "").Trim();
                if (!string.IsNullOrWhiteSpace(d.reference)) particular += " " + d.reference.Trim();
                report.Rows.Add(new[] { d.date, particular, d.amount.ToString("N2") });
            }

            report.Rows.Add(new[] { "", "TOTAL EXPENSE", _current.total_expense.ToString("N2") });
            report.Rows.Add(new[] { "", "LESS: ENCASHMENT", _current.encashment.ToString("N2") });
            report.Rows.Add(new[] { "", "FOR REIMBURSEMENT", _current.for_reimbursement.ToString("N2") });
            report.Rows.Add(new[] { "", "CASH ON HAND", _current.cash_on_hand.ToString("N2") });
            report.Rows.Add(new[] { "", "ACCOUNTABILITY", _current.accountability.ToString("N2") });

            report.Signatures.Add(HouseTemplateReport.Pair("PREPARED BY", _current.prepared_by));
            report.Signatures.Add(HouseTemplateReport.Pair("APPROVED BY", _current.approved_by));
            return report;
        }

        // Petty cash is the one document allowed to export to Excel (spec 2.10.2).
        private void btn_export_excel_Click(object sender, EventArgs e)
        {
            var report = Report();
            if (report == null) return;

            using (var dialog = new SaveFileDialog
            {
                Filter = "Excel workbook (*.xlsx)|*.xlsx",
                FileName = "PettyCash_PC" + _current.doc_no.ToString("D4") + ".xlsx",
            })
            {
                if (dialog.ShowDialog() != DialogResult.OK) return;

                try
                {
                    File.WriteAllBytes(dialog.FileName, report.Render("EXCELOPENXML"));
                    MessageBox.Show("Exported to " + dialog.FileName, "Petty Cash",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Export failed: " + ex.Message, "Petty Cash",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
