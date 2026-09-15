namespace smpc_accounting_app.Pages.Transactions.AccountsPayable.PettyCash
{
    partial class PettyCashPage
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btn_new = new System.Windows.Forms.ToolStripButton();
            this.btn_save = new System.Windows.Forms.ToolStripButton();
            this.btn_submit = new System.Windows.Forms.ToolStripButton();
            this.btn_approve = new System.Windows.Forms.ToolStripButton();
            this.btn_print = new System.Windows.Forms.ToolStripButton();
            this.btn_export_excel = new System.Windows.Forms.ToolStripButton();
            this.btn_delete = new System.Windows.Forms.ToolStripButton();
            this.sep1 = new System.Windows.Forms.ToolStripSeparator();
            this.lbl_request = new System.Windows.Forms.ToolStripLabel();
            this.cmb_request = new System.Windows.Forms.ToolStripComboBox();
            this.lbl_title = new System.Windows.Forms.Label();
            this.pnl_header = new System.Windows.Forms.Panel();
            this.lbl_cut_off = new System.Windows.Forms.Label();
            this.dtp_cut_off = new System.Windows.Forms.DateTimePicker();
            this.lbl_accountable_fund = new System.Windows.Forms.Label();
            this.txt_accountable_fund = new System.Windows.Forms.TextBox();
            this.lbl_opening_fund = new System.Windows.Forms.Label();
            this.txt_opening_fund = new System.Windows.Forms.TextBox();
            this.lbl_cash_on_hand = new System.Windows.Forms.Label();
            this.txt_cash_on_hand = new System.Windows.Forms.TextBox();
            this.lbl_deductions = new System.Windows.Forms.Label();
            this.txt_deductions = new System.Windows.Forms.TextBox();
            this.lbl_total_expense_c = new System.Windows.Forms.Label();
            this.lbl_total_expense = new System.Windows.Forms.Label();
            this.lbl_encashment_c = new System.Windows.Forms.Label();
            this.lbl_encashment = new System.Windows.Forms.Label();
            this.lbl_for_reimbursement_c = new System.Windows.Forms.Label();
            this.lbl_for_reimbursement = new System.Windows.Forms.Label();
            this.lbl_accountability_c = new System.Windows.Forms.Label();
            this.lbl_accountability = new System.Windows.Forms.Label();
            this.lbl_status = new System.Windows.Forms.Label();
            this.lbl_tally = new System.Windows.Forms.Label();
            this.dgv_details = new System.Windows.Forms.DataGridView();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.btn_add_row = new System.Windows.Forms.ToolStripButton();
            this.btn_add_encashment = new System.Windows.Forms.ToolStripButton();
            this.btn_delete_row = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.pnl_header.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_details)).BeginInit();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            //
            // toolStrip1
            //
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_new,
            this.btn_save,
            this.btn_submit,
            this.btn_approve,
            this.btn_print,
            this.btn_export_excel,
            this.btn_delete,
            this.sep1,
            this.lbl_request,
            this.cmb_request});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1100, 25);
            this.toolStrip1.TabIndex = 0;
            //
            // buttons
            //
            this.btn_new.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btn_new.Name = "btn_new";
            this.btn_new.Size = new System.Drawing.Size(80, 22);
            this.btn_new.Text = "New Cycle";
            this.btn_save.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(35, 22);
            this.btn_save.Text = "Save";
            this.btn_submit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btn_submit.Name = "btn_submit";
            this.btn_submit.Size = new System.Drawing.Size(50, 22);
            this.btn_submit.Text = "Submit";
            this.btn_approve.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btn_approve.Name = "btn_approve";
            this.btn_approve.Size = new System.Drawing.Size(58, 22);
            this.btn_approve.Text = "Approve";
            this.btn_print.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btn_print.Name = "btn_print";
            this.btn_print.Size = new System.Drawing.Size(40, 22);
            this.btn_print.Text = "Print";
            this.btn_export_excel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btn_export_excel.Name = "btn_export_excel";
            this.btn_export_excel.Size = new System.Drawing.Size(80, 22);
            this.btn_export_excel.Text = "Export Excel";
            this.btn_delete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.Size = new System.Drawing.Size(44, 22);
            this.btn_delete.Text = "Delete";
            this.sep1.Name = "sep1";
            this.sep1.Size = new System.Drawing.Size(6, 25);
            this.lbl_request.Name = "lbl_request";
            this.lbl_request.Size = new System.Drawing.Size(50, 22);
            this.lbl_request.Text = "Request:";
            this.cmb_request.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_request.Name = "cmb_request";
            this.cmb_request.Size = new System.Drawing.Size(260, 25);
            //
            // lbl_title
            //
            this.lbl_title.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_title.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lbl_title.Location = new System.Drawing.Point(0, 25);
            this.lbl_title.Name = "lbl_title";
            this.lbl_title.Padding = new System.Windows.Forms.Padding(8, 6, 0, 4);
            this.lbl_title.Size = new System.Drawing.Size(1100, 34);
            this.lbl_title.TabIndex = 1;
            this.lbl_title.Text = "Petty Cash Replenishment";
            //
            // pnl_header
            //
            this.pnl_header.Controls.Add(this.lbl_cut_off);
            this.pnl_header.Controls.Add(this.dtp_cut_off);
            this.pnl_header.Controls.Add(this.lbl_accountable_fund);
            this.pnl_header.Controls.Add(this.txt_accountable_fund);
            this.pnl_header.Controls.Add(this.lbl_opening_fund);
            this.pnl_header.Controls.Add(this.txt_opening_fund);
            this.pnl_header.Controls.Add(this.lbl_cash_on_hand);
            this.pnl_header.Controls.Add(this.txt_cash_on_hand);
            this.pnl_header.Controls.Add(this.lbl_deductions);
            this.pnl_header.Controls.Add(this.txt_deductions);
            this.pnl_header.Controls.Add(this.lbl_total_expense_c);
            this.pnl_header.Controls.Add(this.lbl_total_expense);
            this.pnl_header.Controls.Add(this.lbl_encashment_c);
            this.pnl_header.Controls.Add(this.lbl_encashment);
            this.pnl_header.Controls.Add(this.lbl_for_reimbursement_c);
            this.pnl_header.Controls.Add(this.lbl_for_reimbursement);
            this.pnl_header.Controls.Add(this.lbl_accountability_c);
            this.pnl_header.Controls.Add(this.lbl_accountability);
            this.pnl_header.Controls.Add(this.lbl_status);
            this.pnl_header.Controls.Add(this.lbl_tally);
            this.pnl_header.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_header.Location = new System.Drawing.Point(0, 59);
            this.pnl_header.Name = "pnl_header";
            this.pnl_header.Size = new System.Drawing.Size(1100, 128);
            this.pnl_header.TabIndex = 2;
            //
            // header fields (left column)
            //
            this.lbl_cut_off.AutoSize = true;
            this.lbl_cut_off.Location = new System.Drawing.Point(12, 12);
            this.lbl_cut_off.Name = "lbl_cut_off";
            this.lbl_cut_off.Size = new System.Drawing.Size(76, 13);
            this.lbl_cut_off.Text = "CUT-OFF DATE";
            this.dtp_cut_off.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_cut_off.CustomFormat = "yyyy-MM-dd";
            this.dtp_cut_off.Location = new System.Drawing.Point(140, 8);
            this.dtp_cut_off.Name = "dtp_cut_off";
            this.dtp_cut_off.Size = new System.Drawing.Size(140, 20);
            this.lbl_accountable_fund.AutoSize = true;
            this.lbl_accountable_fund.Location = new System.Drawing.Point(12, 38);
            this.lbl_accountable_fund.Name = "lbl_accountable_fund";
            this.lbl_accountable_fund.Size = new System.Drawing.Size(102, 13);
            this.lbl_accountable_fund.Text = "ACCOUNTABLE FUND";
            this.txt_accountable_fund.Location = new System.Drawing.Point(140, 35);
            this.txt_accountable_fund.Name = "txt_accountable_fund";
            this.txt_accountable_fund.Size = new System.Drawing.Size(140, 20);
            this.txt_accountable_fund.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.lbl_opening_fund.AutoSize = true;
            this.lbl_opening_fund.Location = new System.Drawing.Point(12, 64);
            this.lbl_opening_fund.Name = "lbl_opening_fund";
            this.lbl_opening_fund.Size = new System.Drawing.Size(79, 13);
            this.lbl_opening_fund.Text = "OPENING FUND";
            this.txt_opening_fund.Location = new System.Drawing.Point(140, 61);
            this.txt_opening_fund.Name = "txt_opening_fund";
            this.txt_opening_fund.Size = new System.Drawing.Size(140, 20);
            this.txt_opening_fund.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.lbl_cash_on_hand.AutoSize = true;
            this.lbl_cash_on_hand.Location = new System.Drawing.Point(12, 90);
            this.lbl_cash_on_hand.Name = "lbl_cash_on_hand";
            this.lbl_cash_on_hand.Size = new System.Drawing.Size(86, 13);
            this.lbl_cash_on_hand.Text = "CASH ON HAND";
            this.txt_cash_on_hand.Location = new System.Drawing.Point(140, 87);
            this.txt_cash_on_hand.Name = "txt_cash_on_hand";
            this.txt_cash_on_hand.Size = new System.Drawing.Size(140, 20);
            this.txt_cash_on_hand.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.lbl_deductions.AutoSize = true;
            this.lbl_deductions.Location = new System.Drawing.Point(300, 90);
            this.lbl_deductions.Name = "lbl_deductions";
            this.lbl_deductions.Size = new System.Drawing.Size(126, 13);
            this.lbl_deductions.Text = "DEDUCTIONS (negative)";
            this.txt_deductions.Location = new System.Drawing.Point(440, 87);
            this.txt_deductions.Name = "txt_deductions";
            this.txt_deductions.Size = new System.Drawing.Size(120, 20);
            this.txt_deductions.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            //
            // computed (right column)
            //
            this.lbl_total_expense_c.AutoSize = true;
            this.lbl_total_expense_c.Location = new System.Drawing.Point(640, 12);
            this.lbl_total_expense_c.Name = "lbl_total_expense_c";
            this.lbl_total_expense_c.Size = new System.Drawing.Size(84, 13);
            this.lbl_total_expense_c.Text = "TOTAL EXPENSE";
            this.lbl_total_expense.AutoSize = false;
            this.lbl_total_expense.Location = new System.Drawing.Point(800, 12);
            this.lbl_total_expense.Name = "lbl_total_expense";
            this.lbl_total_expense.Size = new System.Drawing.Size(140, 16);
            this.lbl_total_expense.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl_total_expense.Text = "0.00";
            this.lbl_encashment_c.AutoSize = true;
            this.lbl_encashment_c.Location = new System.Drawing.Point(640, 38);
            this.lbl_encashment_c.Name = "lbl_encashment_c";
            this.lbl_encashment_c.Size = new System.Drawing.Size(112, 13);
            this.lbl_encashment_c.Text = "LESS: ENCASHMENT";
            this.lbl_encashment.AutoSize = false;
            this.lbl_encashment.Location = new System.Drawing.Point(800, 38);
            this.lbl_encashment.Name = "lbl_encashment";
            this.lbl_encashment.Size = new System.Drawing.Size(140, 16);
            this.lbl_encashment.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl_encashment.Text = "0.00";
            this.lbl_for_reimbursement_c.AutoSize = true;
            this.lbl_for_reimbursement_c.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lbl_for_reimbursement_c.Location = new System.Drawing.Point(640, 64);
            this.lbl_for_reimbursement_c.Name = "lbl_for_reimbursement_c";
            this.lbl_for_reimbursement_c.Size = new System.Drawing.Size(122, 13);
            this.lbl_for_reimbursement_c.Text = "FOR REIMBURSEMENT";
            this.lbl_for_reimbursement.AutoSize = false;
            this.lbl_for_reimbursement.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lbl_for_reimbursement.Location = new System.Drawing.Point(800, 64);
            this.lbl_for_reimbursement.Name = "lbl_for_reimbursement";
            this.lbl_for_reimbursement.Size = new System.Drawing.Size(140, 16);
            this.lbl_for_reimbursement.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl_for_reimbursement.Text = "0.00";
            this.lbl_accountability_c.AutoSize = true;
            this.lbl_accountability_c.Location = new System.Drawing.Point(640, 90);
            this.lbl_accountability_c.Name = "lbl_accountability_c";
            this.lbl_accountability_c.Size = new System.Drawing.Size(88, 13);
            this.lbl_accountability_c.Text = "ACCOUNTABILITY";
            this.lbl_accountability.AutoSize = false;
            this.lbl_accountability.Location = new System.Drawing.Point(800, 90);
            this.lbl_accountability.Name = "lbl_accountability";
            this.lbl_accountability.Size = new System.Drawing.Size(140, 16);
            this.lbl_accountability.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl_accountability.Text = "0.00";
            this.lbl_status.AutoSize = true;
            this.lbl_status.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.lbl_status.Location = new System.Drawing.Point(300, 12);
            this.lbl_status.Name = "lbl_status";
            this.lbl_status.Size = new System.Drawing.Size(46, 15);
            this.lbl_status.Text = "DRAFT";
            this.lbl_tally.AutoSize = false;
            this.lbl_tally.Location = new System.Drawing.Point(300, 38);
            this.lbl_tally.Name = "lbl_tally";
            this.lbl_tally.Size = new System.Drawing.Size(320, 32);
            this.lbl_tally.Text = "";
            //
            // toolStrip2 (row actions)
            //
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_add_row,
            this.btn_add_encashment,
            this.btn_delete_row});
            this.toolStrip2.Location = new System.Drawing.Point(0, 187);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(1100, 25);
            this.toolStrip2.TabIndex = 3;
            this.btn_add_row.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btn_add_row.Name = "btn_add_row";
            this.btn_add_row.Size = new System.Drawing.Size(76, 22);
            this.btn_add_row.Text = "Add Expense";
            this.btn_add_encashment.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btn_add_encashment.Name = "btn_add_encashment";
            this.btn_add_encashment.Size = new System.Drawing.Size(130, 22);
            this.btn_add_encashment.Text = "Add Encashment Offset";
            this.btn_delete_row.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btn_delete_row.Name = "btn_delete_row";
            this.btn_delete_row.Size = new System.Drawing.Size(70, 22);
            this.btn_delete_row.Text = "Delete Row";
            //
            // dgv_details
            //
            this.dgv_details.AllowUserToAddRows = false;
            this.dgv_details.AllowUserToDeleteRows = false;
            this.dgv_details.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_details.BackgroundColor = System.Drawing.Color.White;
            this.dgv_details.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_details.Location = new System.Drawing.Point(0, 212);
            this.dgv_details.Name = "dgv_details";
            this.dgv_details.RowHeadersVisible = false;
            this.dgv_details.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_details.Size = new System.Drawing.Size(1100, 388);
            this.dgv_details.TabIndex = 4;
            //
            // PettyCashPage
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgv_details);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.pnl_header);
            this.Controls.Add(this.lbl_title);
            this.Controls.Add(this.toolStrip1);
            this.Name = "PettyCashPage";
            this.Size = new System.Drawing.Size(1100, 600);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.pnl_header.ResumeLayout(false);
            this.pnl_header.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_details)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btn_new;
        private System.Windows.Forms.ToolStripButton btn_save;
        private System.Windows.Forms.ToolStripButton btn_submit;
        private System.Windows.Forms.ToolStripButton btn_approve;
        private System.Windows.Forms.ToolStripButton btn_print;
        private System.Windows.Forms.ToolStripButton btn_export_excel;
        private System.Windows.Forms.ToolStripButton btn_delete;
        private System.Windows.Forms.ToolStripSeparator sep1;
        private System.Windows.Forms.ToolStripLabel lbl_request;
        private System.Windows.Forms.ToolStripComboBox cmb_request;
        private System.Windows.Forms.Label lbl_title;
        private System.Windows.Forms.Panel pnl_header;
        private System.Windows.Forms.Label lbl_cut_off;
        private System.Windows.Forms.DateTimePicker dtp_cut_off;
        private System.Windows.Forms.Label lbl_accountable_fund;
        private System.Windows.Forms.TextBox txt_accountable_fund;
        private System.Windows.Forms.Label lbl_opening_fund;
        private System.Windows.Forms.TextBox txt_opening_fund;
        private System.Windows.Forms.Label lbl_cash_on_hand;
        private System.Windows.Forms.TextBox txt_cash_on_hand;
        private System.Windows.Forms.Label lbl_deductions;
        private System.Windows.Forms.TextBox txt_deductions;
        private System.Windows.Forms.Label lbl_total_expense_c;
        private System.Windows.Forms.Label lbl_total_expense;
        private System.Windows.Forms.Label lbl_encashment_c;
        private System.Windows.Forms.Label lbl_encashment;
        private System.Windows.Forms.Label lbl_for_reimbursement_c;
        private System.Windows.Forms.Label lbl_for_reimbursement;
        private System.Windows.Forms.Label lbl_accountability_c;
        private System.Windows.Forms.Label lbl_accountability;
        private System.Windows.Forms.Label lbl_status;
        private System.Windows.Forms.Label lbl_tally;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton btn_add_row;
        private System.Windows.Forms.ToolStripButton btn_add_encashment;
        private System.Windows.Forms.ToolStripButton btn_delete_row;
        private System.Windows.Forms.DataGridView dgv_details;
    }
}
