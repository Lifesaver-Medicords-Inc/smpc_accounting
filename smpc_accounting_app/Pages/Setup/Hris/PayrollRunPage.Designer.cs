
namespace smpc_accounting_app.Pages.Setup.Hris
{
    partial class PayrollRunPage
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

        #region Component Designer generated code

        // Skeleton only: header, nav toolstrip, header-fields panel, items grid.
        // Same document-style shape as TimesheetPage; header fields built in
        // code-behind (BuildHeaderFields).
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btn_new = new System.Windows.Forms.ToolStripButton();
            this.btn_search = new System.Windows.Forms.ToolStripButton();
            this.btn_prev = new System.Windows.Forms.ToolStripButton();
            this.btn_next = new System.Windows.Forms.ToolStripButton();
            this.btn_edit = new System.Windows.Forms.ToolStripButton();
            this.btn_save = new System.Windows.Forms.ToolStripButton();
            this.btn_cancel = new System.Windows.Forms.ToolStripButton();
            this.btn_regenerate = new System.Windows.Forms.ToolStripButton();
            this.btn_approve = new System.Windows.Forms.ToolStripButton();
            this.btn_reopen = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btn_printPayslips = new System.Windows.Forms.ToolStripButton();
            this.btn_printRegister = new System.Windows.Forms.ToolStripButton();
            this.btn_printRemittance = new System.Windows.Forms.ToolStripButton();
            this.lbl_record = new System.Windows.Forms.ToolStripLabel();
            this.pnl_header = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgv_items = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_items)).BeginInit();
            this.SuspendLayout();
            //
            // panel1
            //
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1400, 47);
            this.panel1.TabIndex = 1;
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F);
            this.label1.Location = new System.Drawing.Point(12, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Payroll";
            //
            // toolStrip1
            //
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_new,
            this.btn_search,
            this.btn_prev,
            this.btn_next,
            this.btn_edit,
            this.btn_save,
            this.btn_cancel,
            this.btn_regenerate,
            this.btn_approve,
            this.btn_reopen,
            this.toolStripSeparator1,
            this.btn_printPayslips,
            this.btn_printRegister,
            this.btn_printRemittance,
            this.lbl_record});
            this.toolStrip1.Location = new System.Drawing.Point(0, 47);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1400, 25);
            this.toolStrip1.TabIndex = 10;
            //
            // buttons
            //
            this.btn_new.Name = "btn_new";
            this.btn_new.Size = new System.Drawing.Size(51, 22);
            this.btn_new.Text = "New";
            this.btn_new.Click += new System.EventHandler(this.btn_new_Click);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(58, 22);
            this.btn_search.Text = "Search";
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            this.btn_prev.Name = "btn_prev";
            this.btn_prev.Size = new System.Drawing.Size(62, 22);
            this.btn_prev.Text = "<< Prev";
            this.btn_prev.Click += new System.EventHandler(this.btn_prev_Click);
            this.btn_next.Name = "btn_next";
            this.btn_next.Size = new System.Drawing.Size(62, 22);
            this.btn_next.Text = "Next >>";
            this.btn_next.Click += new System.EventHandler(this.btn_next_Click);
            this.btn_edit.Name = "btn_edit";
            this.btn_edit.Size = new System.Drawing.Size(47, 22);
            this.btn_edit.Text = "Edit";
            this.btn_edit.Click += new System.EventHandler(this.btn_edit_Click);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(51, 22);
            this.btn_save.Text = "Save";
            this.btn_save.Visible = false;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(63, 22);
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.Visible = false;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            this.btn_regenerate.Name = "btn_regenerate";
            this.btn_regenerate.Size = new System.Drawing.Size(86, 22);
            this.btn_regenerate.Text = "Regenerate";
            this.btn_regenerate.Click += new System.EventHandler(this.btn_regenerate_Click);
            this.btn_approve.Name = "btn_approve";
            this.btn_approve.Size = new System.Drawing.Size(70, 22);
            this.btn_approve.Text = "Approve";
            this.btn_approve.Click += new System.EventHandler(this.btn_approve_Click);
            this.btn_reopen.Name = "btn_reopen";
            this.btn_reopen.Size = new System.Drawing.Size(66, 22);
            this.btn_reopen.Text = "Reopen";
            this.btn_reopen.Click += new System.EventHandler(this.btn_reopen_Click);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            this.btn_printPayslips.Name = "btn_printPayslips";
            this.btn_printPayslips.Size = new System.Drawing.Size(90, 22);
            this.btn_printPayslips.Text = "Print Payslips";
            this.btn_printPayslips.Click += new System.EventHandler(this.btn_printPayslips_Click);
            this.btn_printRegister.Name = "btn_printRegister";
            this.btn_printRegister.Size = new System.Drawing.Size(94, 22);
            this.btn_printRegister.Text = "Print Register";
            this.btn_printRegister.Click += new System.EventHandler(this.btn_printRegister_Click);
            this.btn_printRemittance.Name = "btn_printRemittance";
            this.btn_printRemittance.Size = new System.Drawing.Size(112, 22);
            this.btn_printRemittance.Text = "Print Remittance";
            this.btn_printRemittance.Click += new System.EventHandler(this.btn_printRemittance_Click);
            this.lbl_record.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.lbl_record.Name = "lbl_record";
            this.lbl_record.Size = new System.Drawing.Size(30, 22);
            this.lbl_record.Text = "0 / 0";
            //
            // pnl_header
            //
            this.pnl_header.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_header.Location = new System.Drawing.Point(0, 72);
            this.pnl_header.Name = "pnl_header";
            this.pnl_header.Size = new System.Drawing.Size(1400, 235);
            this.pnl_header.TabIndex = 64;
            //
            // panel2
            //
            this.panel2.Controls.Add(this.dgv_items);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 307);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(6);
            this.panel2.Size = new System.Drawing.Size(1400, 643);
            this.panel2.TabIndex = 2;
            //
            // dgv_items
            //
            this.dgv_items.AllowUserToAddRows = false;
            this.dgv_items.AllowUserToDeleteRows = false;
            this.dgv_items.AllowUserToResizeRows = false;
            this.dgv_items.AutoGenerateColumns = false;
            this.dgv_items.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgv_items.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_items.Location = new System.Drawing.Point(6, 6);
            this.dgv_items.Name = "dgv_items";
            this.dgv_items.RowHeadersVisible = false;
            this.dgv_items.Size = new System.Drawing.Size(1388, 631);
            this.dgv_items.TabIndex = 4;
            //
            // PayrollRunPage
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnl_header);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panel1);
            this.Name = "PayrollRunPage";
            this.Size = new System.Drawing.Size(1400, 950);
            this.Load += new System.EventHandler(this.PayrollRunPage_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_items)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btn_new;
        private System.Windows.Forms.ToolStripButton btn_search;
        private System.Windows.Forms.ToolStripButton btn_prev;
        private System.Windows.Forms.ToolStripButton btn_next;
        private System.Windows.Forms.ToolStripButton btn_edit;
        private System.Windows.Forms.ToolStripButton btn_save;
        private System.Windows.Forms.ToolStripButton btn_cancel;
        private System.Windows.Forms.ToolStripButton btn_regenerate;
        private System.Windows.Forms.ToolStripButton btn_approve;
        private System.Windows.Forms.ToolStripButton btn_reopen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btn_printPayslips;
        private System.Windows.Forms.ToolStripButton btn_printRegister;
        private System.Windows.Forms.ToolStripButton btn_printRemittance;
        private System.Windows.Forms.ToolStripLabel lbl_record;
        private System.Windows.Forms.Panel pnl_header;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgv_items;
    }
}
