namespace smpc_accounting_app.Pages.Transactions.AccountsReceivables.Billing
{
    partial class BillingListPage
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
            this.btn_refresh = new System.Windows.Forms.ToolStripButton();
            this.btn_print = new System.Windows.Forms.ToolStripButton();
            this.sep1 = new System.Windows.Forms.ToolStripSeparator();
            this.lbl_search = new System.Windows.Forms.ToolStripLabel();
            this.txt_search = new System.Windows.Forms.ToolStripTextBox();
            this.lbl_title = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tab_status = new System.Windows.Forms.TabControl();
            this.tab_active = new System.Windows.Forms.TabPage();
            this.dgv_active = new System.Windows.Forms.DataGridView();
            this.tab_closed = new System.Windows.Forms.TabPage();
            this.dgv_closed = new System.Windows.Forms.DataGridView();
            this.tab_detail = new System.Windows.Forms.TabControl();
            this.tab_ledger = new System.Windows.Forms.TabPage();
            this.dgv_ledger = new System.Windows.Forms.DataGridView();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.btn_add_row = new System.Windows.Forms.ToolStripButton();
            this.btn_save_rows = new System.Windows.Forms.ToolStripButton();
            this.btn_delete_row = new System.Windows.Forms.ToolStripButton();
            this.lbl_ledger_so = new System.Windows.Forms.ToolStripLabel();
            this.tab_receipts = new System.Windows.Forms.TabPage();
            this.dgv_receipts = new System.Windows.Forms.DataGridView();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tab_status.SuspendLayout();
            this.tab_active.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_active)).BeginInit();
            this.tab_closed.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_closed)).BeginInit();
            this.tab_detail.SuspendLayout();
            this.tab_ledger.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ledger)).BeginInit();
            this.toolStrip2.SuspendLayout();
            this.tab_receipts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_receipts)).BeginInit();
            this.SuspendLayout();
            //
            // toolStrip1
            //
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_refresh,
            this.btn_print,
            this.sep1,
            this.lbl_search,
            this.txt_search});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1100, 25);
            this.toolStrip1.TabIndex = 0;
            //
            // btn_refresh
            //
            this.btn_refresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btn_refresh.Name = "btn_refresh";
            this.btn_refresh.Size = new System.Drawing.Size(56, 22);
            this.btn_refresh.Text = "Refresh";
            //
            // btn_print
            //
            this.btn_print.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btn_print.Name = "btn_print";
            this.btn_print.Size = new System.Drawing.Size(40, 22);
            this.btn_print.Text = "Print";
            //
            // sep1
            //
            this.sep1.Name = "sep1";
            this.sep1.Size = new System.Drawing.Size(6, 25);
            //
            // lbl_search
            //
            this.lbl_search.Name = "lbl_search";
            this.lbl_search.Size = new System.Drawing.Size(45, 22);
            this.lbl_search.Text = "Search:";
            //
            // txt_search
            //
            this.txt_search.Name = "txt_search";
            this.txt_search.Size = new System.Drawing.Size(200, 25);
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
            this.lbl_title.Text = "Billing";
            //
            // splitContainer1
            //
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 59);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.splitContainer1.Panel1.Controls.Add(this.tab_status);
            this.splitContainer1.Panel2.Controls.Add(this.tab_detail);
            this.splitContainer1.Size = new System.Drawing.Size(1100, 541);
            this.splitContainer1.SplitterDistance = 320;
            this.splitContainer1.TabIndex = 2;
            //
            // tab_status
            //
            this.tab_status.Controls.Add(this.tab_active);
            this.tab_status.Controls.Add(this.tab_closed);
            this.tab_status.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tab_status.Location = new System.Drawing.Point(0, 0);
            this.tab_status.Name = "tab_status";
            this.tab_status.SelectedIndex = 0;
            this.tab_status.Size = new System.Drawing.Size(1100, 320);
            this.tab_status.TabIndex = 0;
            //
            // tab_active
            //
            this.tab_active.Controls.Add(this.dgv_active);
            this.tab_active.Location = new System.Drawing.Point(4, 22);
            this.tab_active.Name = "tab_active";
            this.tab_active.Padding = new System.Windows.Forms.Padding(3);
            this.tab_active.Size = new System.Drawing.Size(1092, 294);
            this.tab_active.TabIndex = 0;
            this.tab_active.Text = "ACTIVE";
            this.tab_active.UseVisualStyleBackColor = true;
            //
            // dgv_active
            //
            this.dgv_active.AllowUserToAddRows = false;
            this.dgv_active.AllowUserToDeleteRows = false;
            this.dgv_active.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_active.BackgroundColor = System.Drawing.Color.White;
            this.dgv_active.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_active.Location = new System.Drawing.Point(3, 3);
            this.dgv_active.MultiSelect = false;
            this.dgv_active.Name = "dgv_active";
            this.dgv_active.ReadOnly = true;
            this.dgv_active.RowHeadersVisible = false;
            this.dgv_active.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_active.Size = new System.Drawing.Size(1086, 288);
            this.dgv_active.TabIndex = 0;
            //
            // tab_closed
            //
            this.tab_closed.Controls.Add(this.dgv_closed);
            this.tab_closed.Location = new System.Drawing.Point(4, 22);
            this.tab_closed.Name = "tab_closed";
            this.tab_closed.Padding = new System.Windows.Forms.Padding(3);
            this.tab_closed.Size = new System.Drawing.Size(1092, 294);
            this.tab_closed.TabIndex = 1;
            this.tab_closed.Text = "CLOSED";
            this.tab_closed.UseVisualStyleBackColor = true;
            //
            // dgv_closed
            //
            this.dgv_closed.AllowUserToAddRows = false;
            this.dgv_closed.AllowUserToDeleteRows = false;
            this.dgv_closed.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_closed.BackgroundColor = System.Drawing.Color.White;
            this.dgv_closed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_closed.Location = new System.Drawing.Point(3, 3);
            this.dgv_closed.MultiSelect = false;
            this.dgv_closed.Name = "dgv_closed";
            this.dgv_closed.ReadOnly = true;
            this.dgv_closed.RowHeadersVisible = false;
            this.dgv_closed.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_closed.Size = new System.Drawing.Size(1086, 288);
            this.dgv_closed.TabIndex = 0;
            //
            // tab_detail
            //
            this.tab_detail.Controls.Add(this.tab_ledger);
            this.tab_detail.Controls.Add(this.tab_receipts);
            this.tab_detail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tab_detail.Location = new System.Drawing.Point(0, 0);
            this.tab_detail.Name = "tab_detail";
            this.tab_detail.SelectedIndex = 0;
            this.tab_detail.Size = new System.Drawing.Size(1100, 217);
            this.tab_detail.TabIndex = 0;
            //
            // tab_ledger
            //
            this.tab_ledger.Controls.Add(this.dgv_ledger);
            this.tab_ledger.Controls.Add(this.toolStrip2);
            this.tab_ledger.Location = new System.Drawing.Point(4, 22);
            this.tab_ledger.Name = "tab_ledger";
            this.tab_ledger.Padding = new System.Windows.Forms.Padding(3);
            this.tab_ledger.Size = new System.Drawing.Size(1092, 191);
            this.tab_ledger.TabIndex = 0;
            this.tab_ledger.Text = "SO Transaction History";
            this.tab_ledger.UseVisualStyleBackColor = true;
            //
            // toolStrip2
            //
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_add_row,
            this.btn_save_rows,
            this.btn_delete_row,
            this.lbl_ledger_so});
            this.toolStrip2.Location = new System.Drawing.Point(3, 3);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(1086, 25);
            this.toolStrip2.TabIndex = 0;
            //
            // btn_add_row
            //
            this.btn_add_row.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btn_add_row.Name = "btn_add_row";
            this.btn_add_row.Size = new System.Drawing.Size(33, 22);
            this.btn_add_row.Text = "Add";
            //
            // btn_save_rows
            //
            this.btn_save_rows.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btn_save_rows.Name = "btn_save_rows";
            this.btn_save_rows.Size = new System.Drawing.Size(35, 22);
            this.btn_save_rows.Text = "Save";
            //
            // btn_delete_row
            //
            this.btn_delete_row.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btn_delete_row.Name = "btn_delete_row";
            this.btn_delete_row.Size = new System.Drawing.Size(44, 22);
            this.btn_delete_row.Text = "Delete";
            //
            // lbl_ledger_so
            //
            this.lbl_ledger_so.Name = "lbl_ledger_so";
            this.lbl_ledger_so.Size = new System.Drawing.Size(120, 22);
            this.lbl_ledger_so.Text = "   (no SO selected)";
            //
            // dgv_ledger
            //
            this.dgv_ledger.AllowUserToAddRows = false;
            this.dgv_ledger.AllowUserToDeleteRows = false;
            this.dgv_ledger.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_ledger.BackgroundColor = System.Drawing.Color.White;
            this.dgv_ledger.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_ledger.Location = new System.Drawing.Point(3, 28);
            this.dgv_ledger.Name = "dgv_ledger";
            this.dgv_ledger.RowHeadersVisible = false;
            this.dgv_ledger.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_ledger.Size = new System.Drawing.Size(1086, 160);
            this.dgv_ledger.TabIndex = 1;
            //
            // tab_receipts
            //
            this.tab_receipts.Controls.Add(this.dgv_receipts);
            this.tab_receipts.Location = new System.Drawing.Point(4, 22);
            this.tab_receipts.Name = "tab_receipts";
            this.tab_receipts.Padding = new System.Windows.Forms.Padding(3);
            this.tab_receipts.Size = new System.Drawing.Size(1092, 191);
            this.tab_receipts.TabIndex = 1;
            this.tab_receipts.Text = "Official Receipts";
            this.tab_receipts.UseVisualStyleBackColor = true;
            //
            // dgv_receipts
            //
            this.dgv_receipts.AllowUserToAddRows = false;
            this.dgv_receipts.AllowUserToDeleteRows = false;
            this.dgv_receipts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_receipts.BackgroundColor = System.Drawing.Color.White;
            this.dgv_receipts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_receipts.Location = new System.Drawing.Point(3, 3);
            this.dgv_receipts.Name = "dgv_receipts";
            this.dgv_receipts.ReadOnly = true;
            this.dgv_receipts.RowHeadersVisible = false;
            this.dgv_receipts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_receipts.Size = new System.Drawing.Size(1086, 185);
            this.dgv_receipts.TabIndex = 0;
            //
            // BillingListPage
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.lbl_title);
            this.Controls.Add(this.toolStrip1);
            this.Name = "BillingListPage";
            this.Size = new System.Drawing.Size(1100, 600);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tab_status.ResumeLayout(false);
            this.tab_active.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_active)).EndInit();
            this.tab_closed.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_closed)).EndInit();
            this.tab_detail.ResumeLayout(false);
            this.tab_ledger.ResumeLayout(false);
            this.tab_ledger.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ledger)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.tab_receipts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_receipts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btn_refresh;
        private System.Windows.Forms.ToolStripButton btn_print;
        private System.Windows.Forms.ToolStripSeparator sep1;
        private System.Windows.Forms.ToolStripLabel lbl_search;
        private System.Windows.Forms.ToolStripTextBox txt_search;
        private System.Windows.Forms.Label lbl_title;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tab_status;
        private System.Windows.Forms.TabPage tab_active;
        private System.Windows.Forms.DataGridView dgv_active;
        private System.Windows.Forms.TabPage tab_closed;
        private System.Windows.Forms.DataGridView dgv_closed;
        private System.Windows.Forms.TabControl tab_detail;
        private System.Windows.Forms.TabPage tab_ledger;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton btn_add_row;
        private System.Windows.Forms.ToolStripButton btn_save_rows;
        private System.Windows.Forms.ToolStripButton btn_delete_row;
        private System.Windows.Forms.ToolStripLabel lbl_ledger_so;
        private System.Windows.Forms.DataGridView dgv_ledger;
        private System.Windows.Forms.TabPage tab_receipts;
        private System.Windows.Forms.DataGridView dgv_receipts;
    }
}
