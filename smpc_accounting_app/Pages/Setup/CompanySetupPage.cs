using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using smpc_accounting_app.Models;
using smpc_accounting_app.Services.Helpers;
using smpc_accounting_app.Services.Setup;

namespace smpc_accounting_app.Pages.Setup
{
    // Phase 3 item 3.4. §4.5.6 describes a single, always-existing company record
    // ("Fetched at login") - confirmed against the live DB (tbl_company has exactly
    // one row, id=1, already populated with SMPC's real data). This screen edits
    // that one row: no New/Delete, just Edit -> Save/Cancel against GET/PUT.
    //
    // Scoped to CompanySetupModel's flat fields only (company profile, currency,
    // fiscal calendar, quotation T&C text) - not Address/Contacts (no UI need
    // identified yet, and sending an empty one on Save is safe: the Go update path
    // uses the same DbUpdate that already skips zero-valued fields, so leaving
    // those blank in this form never overwrites the real address/contacts, doesn't
    // matter that this form has no fields for them). Restocking-fee %/cancellation-
    // fee % from §4.5.6's text are deliberately NOT included - §15.1 is still open
    // on what they even mean, and there is no backing column for them on tbl_company
    // yet, so there is nothing here to bind them to.
    public partial class CompanySetupPage : UserControl
    {
        private readonly CompanySetupService _service = new CompanySetupService();
        private readonly Dictionary<string, Control> _fields = new Dictionary<string, Control>();
        private bool _isEditing = false;

        private static readonly (string Prop, string Label, FieldKind Kind)[] FieldDefs = new[]
        {
            ("company_code", "COMPANY CODE", FieldKind.Text),
            ("company_name", "COMPANY NAME", FieldKind.Text),
            ("legal_name", "LEGAL NAME", FieldKind.Text),
            ("trade_name", "TRADE NAME", FieldKind.Text),
            ("business_type", "BUSINESS TYPE", FieldKind.Text),
            ("sec_registration_no", "SEC REGISTRATION NO.", FieldKind.Text),
            ("dti_registration_no", "DTI REGISTRATION NO.", FieldKind.Text),
            ("tin", "TIN", FieldKind.Text),
            ("bir_branch_code", "BIR BRANCH CODE", FieldKind.Text),
            ("rdo_code", "RDO CODE", FieldKind.Text),
            ("industry", "INDUSTRY", FieldKind.Text),
            ("status", "STATUS", FieldKind.Text),
            ("is_head_office", "HEAD OFFICE", FieldKind.Check),
            ("currency_code", "CURRENCY", FieldKind.Text),
            ("beg_bal", "BEGINNING BALANCE", FieldKind.Text),
            ("monthly_rate", "EXCHANGE RATE", FieldKind.Text),
            ("markup_multiplier_price", "MARKUP MULTIPLIER (PRICE)", FieldKind.Text),
            // Sales_Quotation_Bug_Report_2026-08-03.md #18 - was a hardcoded 1.186 in
            // Quotation.cs's own markup computation, contradicting the separate
            // VAT_RATE = 0.12m constant used elsewhere. Both are configurable here now.
            ("vat_rate_percent", "VAT RATE (%)", FieldKind.Text),
            // These used to be FieldKind.Text - a bare TextBox a person could type
            // anything into. JournalEntryPage.cs copies these two strings verbatim
            // into every Journal Entry's period_from/period_to, and the API parses
            // that period against a fixed month/day/year layout - a free-typed date
            // (US day/month order or not, any AM/PM casing, any punctuation) could
            // silently fail that parse and made "no active journal entry" fire no
            // matter what was typed. A DateTimePicker guarantees a real, parseable
            // date every time.
            ("start_fiscal_date", "FISCAL YEAR START", FieldKind.Date),
            ("end_fiscal_date", "FISCAL YEAR END", FieldKind.Date),
            ("inclusions_quotation_terms", "QUOTATION T&C - INCLUSIONS", FieldKind.Multiline),
            ("exclusions_quotation_terms", "QUOTATION T&C - EXCLUSIONS", FieldKind.Multiline),
            ("term_and_conditions", "QUOTATION T&C - GENERAL", FieldKind.Multiline),
        };

        private enum FieldKind { Text, Multiline, Check, Date }

        // Must match the layout the API actually parses this against - see the
        // comment on `layout` in ERP_API's journal_entry_service.go GetCurrentJournal,
        // and the identical parse in sales_invoice_service.go / invoice_receipt_service.go
        // / bulk_invoice_receipt_service.go / payment_receipt_service.go /
        // payment_voucher_service.go (all six match a Journal Entry's period against
        // this exact shape). InvariantCulture so this never drifts with the
        // workstation's regional settings (PH locale still defaults to day/month for
        // short dates in some builds - this format string sidesteps that entirely).
        private const string FiscalDateFormat = "M/d/yyyy h:mm:ss tt";

        public CompanySetupPage()
        {
            InitializeComponent();
            BuildFields();
        }

        private void BuildFields()
        {
            pnl_fields.RowCount = FieldDefs.Length;

            for (int i = 0; i < FieldDefs.Length; i++)
            {
                var (prop, label, kind) = FieldDefs[i];

                pnl_fields.RowStyles.Add(new RowStyle(SizeType.AutoSize));

                var lbl = new Label
                {
                    Text = label,
                    AutoSize = true,
                    Anchor = AnchorStyles.Left,
                    Margin = new Padding(3, 8, 3, 3),
                };
                pnl_fields.Controls.Add(lbl, 0, i);

                Control field;
                if (kind == FieldKind.Check)
                {
                    field = new CheckBox { Margin = new Padding(3, 6, 3, 3), Enabled = false };
                }
                else if (kind == FieldKind.Date)
                {
                    field = new DateTimePicker
                    {
                        Width = 380,
                        Format = DateTimePickerFormat.Short,
                        Margin = new Padding(3, 3, 3, 3),
                        Enabled = false,
                    };
                }
                else
                {
                    field = new TextBox
                    {
                        Name = "txt_" + prop,
                        Width = 380,
                        ReadOnly = true,
                        BackColor = Color.FromArgb(235, 235, 235),
                        Margin = new Padding(3, 3, 3, 3),
                        Multiline = kind == FieldKind.Multiline,
                        Height = kind == FieldKind.Multiline ? 80 : 20,
                    };
                }
                field.Name = "fld_" + prop;
                pnl_fields.Controls.Add(field, 1, i);
                _fields[prop] = field;
            }
        }

        private async void CompanySetupPage_Load(object sender, EventArgs e)
        {
            await LoadCompany();
        }

        // Helpers.Loading.ShowLoading/HideLoading only accept a DataGridView - this
        // singleton edit form has none, so this just disables the panel for the
        // duration of the call instead (CLAUDE.md's "no saved successfully modals"
        // convention doesn't require a specific overlay, just no blocking dialog).
        private async System.Threading.Tasks.Task LoadCompany()
        {
            pnl_scroll.Enabled = false;
            try
            {
                var company = await _service.Get();
                if (company == null)
                {
                    Helpers.ShowDialogMessage("error", "Company setup could not be loaded.");
                    return;
                }
                BindCompany(company);
            }
            catch (Exception ex)
            {
                Helpers.ShowDialogMessage("error", $"Failed to load: {ex.Message}");
            }
            finally
            {
                pnl_scroll.Enabled = true;
            }
        }

        private void BindCompany(CompanySetupModel c)
        {
            SetValue("company_code", c.company_code);
            SetValue("company_name", c.company_name);
            SetValue("legal_name", c.legal_name);
            SetValue("trade_name", c.trade_name);
            SetValue("business_type", c.business_type);
            SetValue("sec_registration_no", c.sec_registration_no);
            SetValue("dti_registration_no", c.dti_registration_no);
            SetValue("tin", c.tin);
            SetValue("bir_branch_code", c.bir_branch_code);
            SetValue("rdo_code", c.rdo_code);
            SetValue("industry", c.industry);
            SetValue("status", c.status);
            ((CheckBox)_fields["is_head_office"]).Checked = c.is_head_office ?? false;
            SetValue("currency_code", c.currency_code);
            SetValue("beg_bal", c.beg_bal.ToString("0.00"));
            SetValue("monthly_rate", c.monthly_rate.ToString("0.0000"));
            SetValue("markup_multiplier_price", c.markup_multiplier_price.ToString("0.0000"));
            SetDateValue("start_fiscal_date", c.start_fiscal_date);
            SetDateValue("end_fiscal_date", c.end_fiscal_date);
            SetValue("inclusions_quotation_terms", c.inclusions_quotation_terms);
            SetValue("exclusions_quotation_terms", c.exclusions_quotation_terms);
            SetValue("term_and_conditions", c.term_and_conditions);
        }

        private void SetValue(string prop, string value)
        {
            if (_fields.TryGetValue(prop, out var control) && control is TextBox txt)
            {
                txt.Text = value ?? "";
            }
        }

        private string GetValue(string prop)
        {
            return _fields.TryGetValue(prop, out var control) && control is TextBox txt ? txt.Text : "";
        }

        // Existing rows may still hold whatever a person previously free-typed into
        // what used to be a plain text box - parse leniently (current culture first,
        // since that's what a human most likely typed, then invariant as a fallback)
        // and fall back to today rather than let a stray old value crash the page load.
        private void SetDateValue(string prop, string value)
        {
            if (!_fields.TryGetValue(prop, out var control) || !(control is DateTimePicker dtp)) return;

            if (!DateTime.TryParse(value, CultureInfo.CurrentCulture, DateTimeStyles.None, out var parsed) &&
                !DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsed))
            {
                parsed = DateTime.Today;
            }
            dtp.Value = parsed.Date;
        }

        // startOfDay pins the time to midnight (fiscal year START); the end date
        // gets pushed to the last instant of that day so a document dated on the
        // fiscal year's last calendar day still falls inside the range - see the
        // FiscalDateFormat comment for why this exact shape matters to the API.
        private string GetDateValue(string prop, bool startOfDay)
        {
            if (!_fields.TryGetValue(prop, out var control) || !(control is DateTimePicker dtp)) return "";

            DateTime value = startOfDay ? dtp.Value.Date : dtp.Value.Date.AddDays(1).AddSeconds(-1);
            return value.ToString(FiscalDateFormat, CultureInfo.InvariantCulture);
        }

        private void SetEditMode(bool enable)
        {
            _isEditing = enable;

            foreach (var kvp in _fields)
            {
                // company_code is the record's own identity - never user-editable,
                // same convention as every other document's read-only DOC NO./CODE.
                if (kvp.Key == "company_code") continue;

                if (kvp.Value is TextBox txt)
                {
                    txt.ReadOnly = !enable;
                    txt.BackColor = enable ? Color.White : Color.FromArgb(235, 235, 235);
                }
                else if (kvp.Value is CheckBox chk)
                {
                    chk.Enabled = enable;
                }
                else if (kvp.Value is DateTimePicker dtp)
                {
                    dtp.Enabled = enable;
                }
            }

            string[] editButtons = { "btn_save", "btn_cancel" };
            string[] navButtons = { "btn_edit" };
            Helpers.SetButtonVisibility(
                toolStrip1,
                pnl_scroll,
                visibleButtons: enable ? editButtons : navButtons,
                hiddenButtons: enable ? navButtons : editButtons
            );
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            SetEditMode(true);
        }

        private async void btn_cancel_Click(object sender, EventArgs e)
        {
            SetEditMode(false);
            await LoadCompany();
        }

        private async void btn_save_Click(object sender, EventArgs e)
        {
            btn_save.Enabled = false;
            btn_cancel.Enabled = false;

            try
            {
                var payload = new CompanySetupModel
                {
                    id = 1,
                    company_code = GetValue("company_code"),
                    company_name = GetValue("company_name"),
                    legal_name = GetValue("legal_name"),
                    trade_name = GetValue("trade_name"),
                    business_type = GetValue("business_type"),
                    sec_registration_no = GetValue("sec_registration_no"),
                    dti_registration_no = GetValue("dti_registration_no"),
                    tin = GetValue("tin"),
                    bir_branch_code = GetValue("bir_branch_code"),
                    rdo_code = GetValue("rdo_code"),
                    industry = GetValue("industry"),
                    status = GetValue("status"),
                    is_head_office = ((CheckBox)_fields["is_head_office"]).Checked,
                    currency_code = GetValue("currency_code"),
                    start_fiscal_date = GetDateValue("start_fiscal_date", startOfDay: true),
                    end_fiscal_date = GetDateValue("end_fiscal_date", startOfDay: false),
                    inclusions_quotation_terms = GetValue("inclusions_quotation_terms"),
                    exclusions_quotation_terms = GetValue("exclusions_quotation_terms"),
                    term_and_conditions = GetValue("term_and_conditions"),
                };

                if (!float.TryParse(GetValue("beg_bal"), out float begBal) ||
                    !float.TryParse(GetValue("monthly_rate"), out float monthlyRate) ||
                    !float.TryParse(GetValue("markup_multiplier_price"), out float markup))
                {
                    Helpers.ShowDialogMessage("error", "Beginning balance, exchange rate, and markup multiplier must be numbers.");
                    return;
                }
                payload.beg_bal = begBal;
                payload.monthly_rate = monthlyRate;
                payload.markup_multiplier_price = markup;

                pnl_scroll.Enabled = false;
                var result = await _service.Update(payload);

                if (result == null || !result.Success)
                {
                    Helpers.ShowDialogMessage("error", "Company setup not updated.");
                    return;
                }

                Helpers.ShowDialogMessage("success", "Company setup updated successfully.");
                SetEditMode(false);
                await LoadCompany();
            }
            catch (Exception ex)
            {
                Helpers.ShowDialogMessage("error", $"Failed to save: {ex.Message}");
            }
            finally
            {
                btn_save.Enabled = true;
                btn_cancel.Enabled = true;
                pnl_scroll.Enabled = true;
            }
        }
    }
}
