
namespace smpc_accounting_app.Pages.Setup.Hris
{
    partial class HolidayCalendarPage
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

        // Skeleton: header, toolstrip (year picker + CRUD), form panel, grid.
        // Form fields are built in code-behind (BuildForm).
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.lbl_year = new System.Windows.Forms.ToolStripLabel();
            this.txt_year = new System.Windows.Forms.ToolStripTextBox();
            this.btn_load = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btn_new = new System.Windows.Forms.ToolStripButton();
            this.btn_edit = new System.Windows.Forms.ToolStripButton();
            this.btn_save = new System.Windows.Forms.ToolStripButton();
            this.btn_cancel = new System.Windows.Forms.ToolStripButton();
            this.btn_delete = new System.Windows.Forms.ToolStripButton();
            this.pnl_form = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgv_list = new System.Windows.Forms.DataGridView();
            this.col_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.label1.Text = "Holiday Calendar";
            //
            // toolStrip1
            //
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbl_year,
            this.txt_year,
            this.btn_load,
            this.toolStripSeparator1,
            this.btn_new,
            this.btn_edit,
            this.btn_save,
            this.btn_cancel,
            this.btn_delete});
            this.toolStrip1.Location = new System.Drawing.Point(0, 47);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1400, 25);
            this.toolStrip1.TabIndex = 10;
            //
            // items
            //
            this.lbl_year.Name = "lbl_year";
            this.lbl_year.Size = new System.Drawing.Size(38, 22);
            this.lbl_year.Text = "YEAR";
            this.txt_year.Name = "txt_year";
            this.txt_year.Size = new System.Drawing.Size(60, 25);
            this.btn_load.Name = "btn_load";
            this.btn_load.Size = new System.Drawing.Size(42, 22);
            this.btn_load.Text = "Load";
            this.btn_load.Click += new System.EventHandler(this.btn_load_Click);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            this.btn_new.Name = "btn_new";
            this.btn_new.Size = new System.Drawing.Size(51, 22);
            this.btn_new.Text = "New";
            this.btn_new.Click += new System.EventHandler(this.btn_new_Click);
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
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.Size = new System.Drawing.Size(60, 22);
            this.btn_delete.Text = "Delete";
            this.btn_delete.Click += new System.EventHandler(this.btn_delete_Click);
            //
            // pnl_form
            //
            this.pnl_form.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_form.Location = new System.Drawing.Point(0, 72);
            this.pnl_form.Name = "pnl_form";
            this.pnl_form.Size = new System.Drawing.Size(1400, 115);
            this.pnl_form.TabIndex = 64;
            //
            // panel2
            //
            this.panel2.Controls.Add(this.dgv_list);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 187);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(6);
            this.panel2.Size = new System.Drawing.Size(1400, 763);
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
            this.col_date,
            this.col_name,
            this.col_type});
            this.dgv_list.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_list.Location = new System.Drawing.Point(6, 6);
            this.dgv_list.Name = "dgv_list";
            this.dgv_list.ReadOnly = true;
            this.dgv_list.RowHeadersVisible = false;
            this.dgv_list.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_list.MultiSelect = false;
            this.dgv_list.Size = new System.Drawing.Size(1388, 751);
            this.dgv_list.TabIndex = 4;
            this.dgv_list.SelectionChanged += new System.EventHandler(this.dgv_list_SelectionChanged);
            //
            // columns
            //
            this.col_id.Name = "col_id";
            this.col_id.Visible = false;
            this.col_date.HeaderText = "DATE";
            this.col_date.Name = "col_date";
            this.col_date.ReadOnly = true;
            this.col_date.Width = 120;
            this.col_name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.col_name.HeaderText = "HOLIDAY";
            this.col_name.Name = "col_name";
            this.col_name.ReadOnly = true;
            this.col_type.HeaderText = "TYPE";
            this.col_type.Name = "col_type";
            this.col_type.ReadOnly = true;
            this.col_type.Width = 130;
            //
            // HolidayCalendarPage
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnl_form);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panel1);
            this.Name = "HolidayCalendarPage";
            this.Size = new System.Drawing.Size(1400, 950);
            this.Load += new System.EventHandler(this.HolidayCalendarPage_Load);
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
        private System.Windows.Forms.ToolStripLabel lbl_year;
        private System.Windows.Forms.ToolStripTextBox txt_year;
        private System.Windows.Forms.ToolStripButton btn_load;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btn_new;
        private System.Windows.Forms.ToolStripButton btn_edit;
        private System.Windows.Forms.ToolStripButton btn_save;
        private System.Windows.Forms.ToolStripButton btn_cancel;
        private System.Windows.Forms.ToolStripButton btn_delete;
        private System.Windows.Forms.Panel pnl_form;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgv_list;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_type;
    }
}
