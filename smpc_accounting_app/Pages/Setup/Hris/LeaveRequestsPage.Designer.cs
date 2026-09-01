
namespace smpc_accounting_app.Pages.Setup.Hris
{
    partial class LeaveRequestsPage
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

        // Skeleton: header, toolstrip (status filter + Load + Approve/Reject),
        // detail panel (built in code-behind), list grid.
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.lbl_status = new System.Windows.Forms.ToolStripLabel();
            this.cmb_statusFilter = new System.Windows.Forms.ToolStripComboBox();
            this.btn_load = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btn_approve = new System.Windows.Forms.ToolStripButton();
            this.btn_reject = new System.Windows.Forms.ToolStripButton();
            this.pnl_form = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgv_list = new System.Windows.Forms.DataGridView();
            this.col_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_employee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_from = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_to = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_decidedBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_list)).BeginInit();
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
            this.label1.Size = new System.Drawing.Size(180, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Leave Requests";
            //
            // toolStrip1
            //
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbl_status,
            this.cmb_statusFilter,
            this.btn_load,
            this.toolStripSeparator1,
            this.btn_approve,
            this.btn_reject});
            this.toolStrip1.Location = new System.Drawing.Point(0, 47);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1400, 25);
            this.toolStrip1.TabIndex = 10;
            //
            // items
            //
            this.lbl_status.Name = "lbl_status";
            this.lbl_status.Size = new System.Drawing.Size(40, 22);
            this.lbl_status.Text = "STATUS";
            this.cmb_statusFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_statusFilter.Name = "cmb_statusFilter";
            this.cmb_statusFilter.Size = new System.Drawing.Size(120, 25);
            this.btn_load.Name = "btn_load";
            this.btn_load.Size = new System.Drawing.Size(42, 22);
            this.btn_load.Text = "Load";
            this.btn_load.Click += new System.EventHandler(this.btn_load_Click);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            this.btn_approve.Name = "btn_approve";
            this.btn_approve.Size = new System.Drawing.Size(70, 22);
            this.btn_approve.Text = "Approve";
            this.btn_approve.Click += new System.EventHandler(this.btn_approve_Click);
            this.btn_reject.Name = "btn_reject";
            this.btn_reject.Size = new System.Drawing.Size(60, 22);
            this.btn_reject.Text = "Reject";
            this.btn_reject.Click += new System.EventHandler(this.btn_reject_Click);
            //
            // pnl_form
            //
            this.pnl_form.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_form.Location = new System.Drawing.Point(0, 72);
            this.pnl_form.Name = "pnl_form";
            this.pnl_form.Size = new System.Drawing.Size(1400, 150);
            this.pnl_form.TabIndex = 64;
            //
            // panel2
            //
            this.panel2.Controls.Add(this.dgv_list);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 222);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(6);
            this.panel2.Size = new System.Drawing.Size(1400, 728);
            this.panel2.TabIndex = 2;
            //
            // dgv_list
            //
            this.dgv_list.AllowUserToAddRows = false;
            this.dgv_list.AllowUserToDeleteRows = false;
            this.dgv_list.AllowUserToResizeRows = false;
            this.dgv_list.AutoGenerateColumns = false;
            this.dgv_list.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgv_list.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_id,
            this.col_employee,
            this.col_type,
            this.col_from,
            this.col_to,
            this.col_status,
            this.col_decidedBy});
            this.dgv_list.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_list.Location = new System.Drawing.Point(6, 6);
            this.dgv_list.Name = "dgv_list";
            this.dgv_list.ReadOnly = true;
            this.dgv_list.RowHeadersVisible = false;
            this.dgv_list.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_list.MultiSelect = false;
            this.dgv_list.Size = new System.Drawing.Size(1388, 716);
            this.dgv_list.TabIndex = 4;
            this.dgv_list.SelectionChanged += new System.EventHandler(this.dgv_list_SelectionChanged);
            //
            // columns
            //
            this.col_id.Name = "col_id";
            this.col_id.Visible = false;
            this.col_employee.HeaderText = "EMPLOYEE";
            this.col_employee.Name = "col_employee";
            this.col_employee.ReadOnly = true;
            this.col_employee.Width = 220;
            this.col_type.HeaderText = "TYPE";
            this.col_type.Name = "col_type";
            this.col_type.ReadOnly = true;
            this.col_type.Width = 80;
            this.col_from.HeaderText = "FROM";
            this.col_from.Name = "col_from";
            this.col_from.ReadOnly = true;
            this.col_from.Width = 100;
            this.col_to.HeaderText = "TO";
            this.col_to.Name = "col_to";
            this.col_to.ReadOnly = true;
            this.col_to.Width = 100;
            this.col_status.HeaderText = "STATUS";
            this.col_status.Name = "col_status";
            this.col_status.ReadOnly = true;
            this.col_status.Width = 100;
            this.col_decidedBy.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.col_decidedBy.HeaderText = "DECIDED BY";
            this.col_decidedBy.Name = "col_decidedBy";
            this.col_decidedBy.ReadOnly = true;
            //
            // LeaveRequestsPage
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnl_form);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panel1);
            this.Name = "LeaveRequestsPage";
            this.Size = new System.Drawing.Size(1400, 950);
            this.Load += new System.EventHandler(this.LeaveRequestsPage_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_list)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel lbl_status;
        private System.Windows.Forms.ToolStripComboBox cmb_statusFilter;
        private System.Windows.Forms.ToolStripButton btn_load;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btn_approve;
        private System.Windows.Forms.ToolStripButton btn_reject;
        private System.Windows.Forms.Panel pnl_form;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgv_list;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_employee;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_from;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_to;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_status;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_decidedBy;
    }
}
