
namespace smpc_accounting_app.Pages.Transactions.Journal
{
    partial class CreditMemo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreditMemo));
            this.panel6 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_status = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btn_new = new System.Windows.Forms.ToolStripButton();
            this.btn_search = new System.Windows.Forms.ToolStripButton();
            this.btn_prev = new System.Windows.Forms.ToolStripButton();
            this.btn_next = new System.Windows.Forms.ToolStripButton();
            this.btn_print = new System.Windows.Forms.ToolStripButton();
            this.btn_edit = new System.Windows.Forms.ToolStripButton();
            this.btn_save = new System.Windows.Forms.ToolStripButton();
            this.btn_cancel = new System.Windows.Forms.ToolStripButton();
            this.btn_approve = new System.Windows.Forms.ToolStripButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.txt_partner_code = new System.Windows.Forms.TextBox();
            this.txt_partner_id = new System.Windows.Forms.TextBox();
            this.label_partner_type = new System.Windows.Forms.Label();
            this.txt_partner_type = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_document_no = new System.Windows.Forms.TextBox();
            this.label_partner_name = new System.Windows.Forms.Label();
            this.txt_partner_name = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_trans_amount = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dtp_date = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.cmb_reason_code = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_currency = new System.Windows.Forms.TextBox();
            this.label_location_group = new System.Windows.Forms.Label();
            this.txt_location_group = new System.Windows.Forms.TextBox();
            this.label_sales_period = new System.Windows.Forms.Label();
            this.txt_sales_period = new System.Windows.Forms.TextBox();
            this.label_ref_srt_no = new System.Windows.Forms.Label();
            this.txt_ref_srt_no = new System.Windows.Forms.TextBox();
            this.label_ref_si_no = new System.Windows.Forms.Label();
            this.txt_ref_si_no = new System.Windows.Forms.TextBox();
            this.chk_dm_refund = new System.Windows.Forms.CheckBox();
            this.label_ref_dm_no = new System.Windows.Forms.Label();
            this.txt_ref_dm_no = new System.Windows.Forms.TextBox();
            this.label_approved_by = new System.Windows.Forms.Label();
            this.txt_approved_by = new System.Windows.Forms.TextBox();
            this.label_approval_date = new System.Windows.Forms.Label();
            this.txt_approval_date = new System.Windows.Forms.TextBox();
            this.panel6.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            //
            // panel6
            //
            this.panel6.Controls.Add(this.label1);
            this.panel6.Controls.Add(this.lbl_status);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1028, 47);
            this.panel6.TabIndex = 16;
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(18, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "CREDIT MEMO";
            //
            // lbl_status
            //
            // Inline "saving.../saved" text beside the module name - CLAUDE.md's
            // convention for new UI, no "saved successfully" modal.
            this.lbl_status.AutoSize = true;
            this.lbl_status.ForeColor = System.Drawing.Color.Gray;
            this.lbl_status.Location = new System.Drawing.Point(185, 18);
            this.lbl_status.Name = "lbl_status";
            this.lbl_status.Size = new System.Drawing.Size(0, 13);
            this.lbl_status.TabIndex = 2;
            //
            // toolStrip1
            //
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_new,
            this.btn_search,
            this.btn_prev,
            this.btn_next,
            this.btn_print,
            this.btn_edit,
            this.btn_save,
            this.btn_cancel,
            this.btn_approve});
            this.toolStrip1.Location = new System.Drawing.Point(0, 47);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(1028, 25);
            this.toolStrip1.TabIndex = 17;
            this.toolStrip1.Text = "toolStrip1";
            //
            // btn_new
            //
            this.btn_new.Image = ((System.Drawing.Image)(resources.GetObject("btn_new.Image")));
            this.btn_new.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_new.Name = "btn_new";
            this.btn_new.Size = new System.Drawing.Size(35, 22);
            this.btn_new.Text = "New";
            //
            // btn_search
            //
            this.btn_search.Image = ((System.Drawing.Image)(resources.GetObject("btn_search.Image")));
            this.btn_search.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(62, 22);
            this.btn_search.Text = "Search";
            //
            // btn_prev
            //
            this.btn_prev.Image = ((System.Drawing.Image)(resources.GetObject("btn_prev.Image")));
            this.btn_prev.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_prev.Name = "btn_prev";
            this.btn_prev.Size = new System.Drawing.Size(72, 22);
            this.btn_prev.Text = "Previous";
            //
            // btn_next
            //
            this.btn_next.Image = ((System.Drawing.Image)(resources.GetObject("btn_next.Image")));
            this.btn_next.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_next.Name = "btn_next";
            this.btn_next.Size = new System.Drawing.Size(52, 22);
            this.btn_next.Text = "Next";
            //
            // btn_print
            //
            this.btn_print.Image = ((System.Drawing.Image)(resources.GetObject("btn_print.Image")));
            this.btn_print.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_print.Name = "btn_print";
            this.btn_print.Size = new System.Drawing.Size(52, 22);
            this.btn_print.Text = "Print";
            //
            // btn_edit
            //
            this.btn_edit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btn_edit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btn_edit.Name = "btn_edit";
            this.btn_edit.Size = new System.Drawing.Size(31, 22);
            this.btn_edit.Text = "Edit";
            //
            // btn_save
            //
            // Supplier side commits on this click (Sec12.6.3, no draft); customer side
            // instead stores a draft and enables btn_approve for the COO (Sec14.99).
            this.btn_save.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(35, 22);
            this.btn_save.Text = "Save";
            this.btn_save.Visible = false;
            //
            // btn_cancel
            //
            this.btn_cancel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(46, 22);
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.Visible = false;
            //
            // btn_approve
            //
            // Sec5.18/Sec3.3/Sec14.99 - COO only, customer-side Credit Memos only.
            // Visible only on a saved, unapproved customer memo - never for a supplier
            // memo, which already committed on save and has nothing left to approve.
            this.btn_approve.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btn_approve.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btn_approve.Name = "btn_approve";
            this.btn_approve.Size = new System.Drawing.Size(55, 22);
            this.btn_approve.Text = "Approve";
            this.btn_approve.Visible = false;
            //
            // panel3
            //
            // Field values marked read-only below (PARTNER TYPE, DOC NO., APPROVED BY,
            // APPROVAL DATE) are system-set/derived, never typed - Sec5.18, Sec14.98.
            this.panel3.Controls.Add(this.label11);
            this.panel3.Controls.Add(this.txt_partner_code);
            this.panel3.Controls.Add(this.txt_partner_id);
            this.panel3.Controls.Add(this.label_partner_type);
            this.panel3.Controls.Add(this.txt_partner_type);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.txt_document_no);
            this.panel3.Controls.Add(this.label_partner_name);
            this.panel3.Controls.Add(this.txt_partner_name);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.txt_trans_amount);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.dtp_date);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.cmb_reason_code);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.txt_currency);
            this.panel3.Controls.Add(this.label_location_group);
            this.panel3.Controls.Add(this.txt_location_group);
            this.panel3.Controls.Add(this.label_sales_period);
            this.panel3.Controls.Add(this.txt_sales_period);
            this.panel3.Controls.Add(this.label_ref_srt_no);
            this.panel3.Controls.Add(this.txt_ref_srt_no);
            this.panel3.Controls.Add(this.label_ref_si_no);
            this.panel3.Controls.Add(this.txt_ref_si_no);
            this.panel3.Controls.Add(this.chk_dm_refund);
            this.panel3.Controls.Add(this.label_ref_dm_no);
            this.panel3.Controls.Add(this.txt_ref_dm_no);
            this.panel3.Controls.Add(this.label_approved_by);
            this.panel3.Controls.Add(this.txt_approved_by);
            this.panel3.Controls.Add(this.label_approval_date);
            this.panel3.Controls.Add(this.txt_approval_date);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 72);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1028, 230);
            this.panel3.TabIndex = 18;
            //
            // label11
            //
            // Renamed from the shell's "SUPPIER:" (typo) - this form serves both A/P
            // and A/R (Sec5.18's "Module path"), so PARTNER covers either, not just
            // supplier. txt_partner_code was previously mis-paired with the doc-no
            // field here; both are corrected in this pass.
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 15);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(78, 13);
            this.label11.TabIndex = 235;
            this.label11.Text = "PARTNER CODE";
            //
            // txt_partner_code
            //
            this.txt_partner_code.Location = new System.Drawing.Point(140, 12);
            this.txt_partner_code.Name = "txt_partner_code";
            this.txt_partner_code.ReadOnly = true;
            this.txt_partner_code.Size = new System.Drawing.Size(180, 20);
            this.txt_partner_code.TabIndex = 237;
            //
            // txt_partner_id
            //
            // Hidden - the resolved BPI id backing txt_partner_code.
            this.txt_partner_id.Location = new System.Drawing.Point(140, 12);
            this.txt_partner_id.Name = "txt_partner_id";
            this.txt_partner_id.Size = new System.Drawing.Size(180, 20);
            this.txt_partner_id.TabIndex = 238;
            this.txt_partner_id.Visible = false;
            //
            // label_partner_type
            //
            this.label_partner_type.AutoSize = true;
            this.label_partner_type.Location = new System.Drawing.Point(340, 15);
            this.label_partner_type.Name = "label_partner_type";
            this.label_partner_type.Size = new System.Drawing.Size(72, 13);
            this.label_partner_type.TabIndex = 239;
            this.label_partner_type.Text = "PARTNER TYPE";
            //
            // txt_partner_type
            //
            // Derived, never a user choice (Sec14.98) - fixed by which module opened
            // this screen (A/P => Supplier, A/R => Customer), not editable here.
            this.txt_partner_type.Location = new System.Drawing.Point(468, 12);
            this.txt_partner_type.Name = "txt_partner_type";
            this.txt_partner_type.ReadOnly = true;
            this.txt_partner_type.Size = new System.Drawing.Size(180, 20);
            this.txt_partner_type.TabIndex = 240;
            //
            // label8
            //
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(668, 15);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 13);
            this.label8.TabIndex = 246;
            this.label8.Text = "DOC NO.";
            //
            // txt_document_no
            //
            this.txt_document_no.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_document_no.Location = new System.Drawing.Point(796, 12);
            this.txt_document_no.Name = "txt_document_no";
            this.txt_document_no.ReadOnly = true;
            this.txt_document_no.Size = new System.Drawing.Size(180, 20);
            this.txt_document_no.TabIndex = 241;
            this.txt_document_no.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            //
            // label_partner_name
            //
            this.label_partner_name.AutoSize = true;
            this.label_partner_name.Location = new System.Drawing.Point(12, 41);
            this.label_partner_name.Name = "label_partner_name";
            this.label_partner_name.Size = new System.Drawing.Size(80, 13);
            this.label_partner_name.TabIndex = 242;
            this.label_partner_name.Text = "PARTNER NAME";
            //
            // txt_partner_name
            //
            this.txt_partner_name.Location = new System.Drawing.Point(140, 38);
            this.txt_partner_name.Name = "txt_partner_name";
            this.txt_partner_name.ReadOnly = true;
            this.txt_partner_name.Size = new System.Drawing.Size(180, 20);
            this.txt_partner_name.TabIndex = 243;
            //
            // label9
            //
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(340, 41);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(137, 13);
            this.label9.TabIndex = 245;
            this.label9.Text = "TRANS. AMOUNT";
            //
            // txt_trans_amount
            //
            this.txt_trans_amount.Location = new System.Drawing.Point(468, 38);
            this.txt_trans_amount.Name = "txt_trans_amount";
            this.txt_trans_amount.Size = new System.Drawing.Size(180, 20);
            this.txt_trans_amount.TabIndex = 244;
            this.txt_trans_amount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            //
            // label7
            //
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(668, 41);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 13);
            this.label7.TabIndex = 247;
            this.label7.Text = "DOC DATE";
            //
            // dtp_date
            //
            this.dtp_date.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_date.Location = new System.Drawing.Point(796, 38);
            this.dtp_date.Name = "dtp_date";
            this.dtp_date.Size = new System.Drawing.Size(180, 20);
            this.dtp_date.TabIndex = 248;
            //
            // label4
            //
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 13);
            this.label4.TabIndex = 255;
            this.label4.Text = "REASON CODE";
            //
            // cmb_reason_code
            //
            // Fixed 5-value list per Sec5.19 (shared with Debit Memo) - not present in
            // SEC17 despite SEC17 being described as authoritative; kept as a fixed inline
            // list per the spec text rather than promoted to an editable Setup list on
            // our own authority. Required on both sides (Sec14.58).
            this.cmb_reason_code.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_reason_code.FormattingEnabled = true;
            this.cmb_reason_code.Items.AddRange(new object[] {
            "--Select--",
            "pur return",
            "adj twas",
            "cancel chq",
            "pur disc",
            "exp cancel"});
            this.cmb_reason_code.Location = new System.Drawing.Point(140, 64);
            this.cmb_reason_code.Name = "cmb_reason_code";
            this.cmb_reason_code.Size = new System.Drawing.Size(180, 21);
            this.cmb_reason_code.TabIndex = 254;
            //
            // label3
            //
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(340, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 240;
            this.label3.Text = "CURRENCY";
            //
            // txt_currency
            //
            this.txt_currency.Location = new System.Drawing.Point(468, 64);
            this.txt_currency.Name = "txt_currency";
            this.txt_currency.Size = new System.Drawing.Size(180, 20);
            this.txt_currency.TabIndex = 256;
            //
            // label_location_group
            //
            this.label_location_group.AutoSize = true;
            this.label_location_group.Location = new System.Drawing.Point(668, 67);
            this.label_location_group.Name = "label_location_group";
            this.label_location_group.Size = new System.Drawing.Size(76, 13);
            this.label_location_group.TabIndex = 257;
            this.label_location_group.Text = "LOCATION GROUP";
            //
            // txt_location_group
            //
            this.txt_location_group.Location = new System.Drawing.Point(796, 64);
            this.txt_location_group.Name = "txt_location_group";
            this.txt_location_group.Size = new System.Drawing.Size(180, 20);
            this.txt_location_group.TabIndex = 258;
            //
            // label_sales_period
            //
            this.label_sales_period.AutoSize = true;
            this.label_sales_period.Location = new System.Drawing.Point(12, 93);
            this.label_sales_period.Name = "label_sales_period";
            this.label_sales_period.Size = new System.Drawing.Size(64, 13);
            this.label_sales_period.TabIndex = 259;
            this.label_sales_period.Text = "SALES PERIOD";
            //
            // txt_sales_period
            //
            this.txt_sales_period.Location = new System.Drawing.Point(140, 90);
            this.txt_sales_period.Name = "txt_sales_period";
            this.txt_sales_period.Size = new System.Drawing.Size(180, 20);
            this.txt_sales_period.TabIndex = 260;
            //
            // label_ref_srt_no
            //
            // Customer side only - the originating Sales Return, if any (Sec5.18).
            // Blank for a credit raised with no return at all (Sec12.6.4).
            this.label_ref_srt_no.AutoSize = true;
            this.label_ref_srt_no.Location = new System.Drawing.Point(340, 93);
            this.label_ref_srt_no.Name = "label_ref_srt_no";
            this.label_ref_srt_no.Size = new System.Drawing.Size(68, 13);
            this.label_ref_srt_no.TabIndex = 261;
            this.label_ref_srt_no.Text = "REF. SRT NO.";
            //
            // txt_ref_srt_no
            //
            this.txt_ref_srt_no.Location = new System.Drawing.Point(468, 90);
            this.txt_ref_srt_no.Name = "txt_ref_srt_no";
            this.txt_ref_srt_no.ReadOnly = true;
            this.txt_ref_srt_no.Size = new System.Drawing.Size(180, 20);
            this.txt_ref_srt_no.TabIndex = 262;
            //
            // label_ref_si_no
            //
            // Customer side only - the SI being credited, inherited from the SRT's own
            // reference doc (Sec5.18).
            this.label_ref_si_no.AutoSize = true;
            this.label_ref_si_no.Location = new System.Drawing.Point(668, 93);
            this.label_ref_si_no.Name = "label_ref_si_no";
            this.label_ref_si_no.Size = new System.Drawing.Size(60, 13);
            this.label_ref_si_no.TabIndex = 263;
            this.label_ref_si_no.Text = "REF. SI NO.";
            //
            // txt_ref_si_no
            //
            this.txt_ref_si_no.Location = new System.Drawing.Point(796, 90);
            this.txt_ref_si_no.Name = "txt_ref_si_no";
            this.txt_ref_si_no.ReadOnly = true;
            this.txt_ref_si_no.Size = new System.Drawing.Size(180, 20);
            this.txt_ref_si_no.TabIndex = 264;
            //
            // chk_dm_refund
            //
            // Supplier side only (Sec14.100 - a customer Credit Memo MUST NOT offer
            // this). Ticking + REF. DM NO. refunds that DM's unapplied debit.
            this.chk_dm_refund.AutoSize = true;
            this.chk_dm_refund.Location = new System.Drawing.Point(12, 120);
            this.chk_dm_refund.Name = "chk_dm_refund";
            this.chk_dm_refund.Size = new System.Drawing.Size(88, 17);
            this.chk_dm_refund.TabIndex = 265;
            this.chk_dm_refund.Text = "DM REFUND";
            this.chk_dm_refund.UseVisualStyleBackColor = true;
            //
            // label_ref_dm_no
            //
            this.label_ref_dm_no.AutoSize = true;
            this.label_ref_dm_no.Location = new System.Drawing.Point(340, 119);
            this.label_ref_dm_no.Name = "label_ref_dm_no";
            this.label_ref_dm_no.Size = new System.Drawing.Size(64, 13);
            this.label_ref_dm_no.TabIndex = 266;
            this.label_ref_dm_no.Text = "REF. DM NO.";
            //
            // txt_ref_dm_no
            //
            this.txt_ref_dm_no.Location = new System.Drawing.Point(468, 116);
            this.txt_ref_dm_no.Name = "txt_ref_dm_no";
            this.txt_ref_dm_no.Size = new System.Drawing.Size(180, 20);
            this.txt_ref_dm_no.TabIndex = 267;
            //
            // label_approved_by
            //
            // Customer side only - Sec3.4/Sec3.3, the approver's name MUST be
            // displayed once approved.
            this.label_approved_by.AutoSize = true;
            this.label_approved_by.Location = new System.Drawing.Point(668, 119);
            this.label_approved_by.Name = "label_approved_by";
            this.label_approved_by.Size = new System.Drawing.Size(66, 13);
            this.label_approved_by.TabIndex = 268;
            this.label_approved_by.Text = "APPROVED BY";
            //
            // txt_approved_by
            //
            this.txt_approved_by.Location = new System.Drawing.Point(796, 116);
            this.txt_approved_by.Name = "txt_approved_by";
            this.txt_approved_by.ReadOnly = true;
            this.txt_approved_by.Size = new System.Drawing.Size(180, 20);
            this.txt_approved_by.TabIndex = 269;
            //
            // label_approval_date
            //
            this.label_approval_date.AutoSize = true;
            this.label_approval_date.Location = new System.Drawing.Point(12, 145);
            this.label_approval_date.Name = "label_approval_date";
            this.label_approval_date.Size = new System.Drawing.Size(78, 13);
            this.label_approval_date.TabIndex = 270;
            this.label_approval_date.Text = "APPROVAL DATE";
            //
            // txt_approval_date
            //
            this.txt_approval_date.Location = new System.Drawing.Point(140, 142);
            this.txt_approval_date.Name = "txt_approval_date";
            this.txt_approval_date.ReadOnly = true;
            this.txt_approval_date.Size = new System.Drawing.Size(180, 20);
            this.txt_approval_date.TabIndex = 271;
            //
            // CreditMemo
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panel6);
            this.Name = "CreditMemo";
            this.Size = new System.Drawing.Size(1028, 1200);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_status;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btn_new;
        private System.Windows.Forms.ToolStripButton btn_search;
        private System.Windows.Forms.ToolStripButton btn_prev;
        private System.Windows.Forms.ToolStripButton btn_next;
        private System.Windows.Forms.ToolStripButton btn_print;
        private System.Windows.Forms.ToolStripButton btn_edit;
        private System.Windows.Forms.ToolStripButton btn_save;
        private System.Windows.Forms.ToolStripButton btn_cancel;
        private System.Windows.Forms.ToolStripButton btn_approve;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txt_partner_code;
        private System.Windows.Forms.TextBox txt_partner_id;
        private System.Windows.Forms.Label label_partner_type;
        private System.Windows.Forms.TextBox txt_partner_type;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_document_no;
        private System.Windows.Forms.Label label_partner_name;
        private System.Windows.Forms.TextBox txt_partner_name;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_trans_amount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtp_date;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmb_reason_code;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_currency;
        private System.Windows.Forms.Label label_location_group;
        private System.Windows.Forms.TextBox txt_location_group;
        private System.Windows.Forms.Label label_sales_period;
        private System.Windows.Forms.TextBox txt_sales_period;
        private System.Windows.Forms.Label label_ref_srt_no;
        private System.Windows.Forms.TextBox txt_ref_srt_no;
        private System.Windows.Forms.Label label_ref_si_no;
        private System.Windows.Forms.TextBox txt_ref_si_no;
        private System.Windows.Forms.CheckBox chk_dm_refund;
        private System.Windows.Forms.Label label_ref_dm_no;
        private System.Windows.Forms.TextBox txt_ref_dm_no;
        private System.Windows.Forms.Label label_approved_by;
        private System.Windows.Forms.TextBox txt_approved_by;
        private System.Windows.Forms.Label label_approval_date;
        private System.Windows.Forms.TextBox txt_approval_date;
    }
}
