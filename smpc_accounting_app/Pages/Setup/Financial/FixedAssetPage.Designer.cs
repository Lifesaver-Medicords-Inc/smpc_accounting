
namespace smpc_accounting_app.Pages.Setup.Financial
{
    partial class FixedAssetPage
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

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btn_new = new System.Windows.Forms.ToolStripButton();
            this.btn_edit = new System.Windows.Forms.ToolStripButton();
            this.btn_delete = new System.Windows.Forms.ToolStripButton();
            this.btn_save = new System.Windows.Forms.ToolStripButton();
            this.btn_cancel = new System.Windows.Forms.ToolStripButton();
            this.pnl_content = new System.Windows.Forms.Panel();
            this.txt_id = new System.Windows.Forms.TextBox();
            this.lbl_code = new System.Windows.Forms.Label();
            this.txt_code = new System.Windows.Forms.TextBox();
            this.lbl_name = new System.Windows.Forms.Label();
            this.txt_name = new System.Windows.Forms.TextBox();
            this.lbl_category = new System.Windows.Forms.Label();
            this.cmb_category = new System.Windows.Forms.ComboBox();
            this.lbl_cost = new System.Windows.Forms.Label();
            this.txt_cost = new System.Windows.Forms.TextBox();
            this.lbl_acquired_date = new System.Windows.Forms.Label();
            this.dtp_acquired_date = new System.Windows.Forms.DateTimePicker();
            this.lbl_useful_life_years = new System.Windows.Forms.Label();
            this.txt_useful_life_years = new System.Windows.Forms.TextBox();
            this.lbl_salvage_value = new System.Windows.Forms.Label();
            this.txt_salvage_value = new System.Windows.Forms.TextBox();
            this.lbl_status = new System.Windows.Forms.Label();
            this.cmb_status = new System.Windows.Forms.ComboBox();
            this.lbl_disposed_date = new System.Windows.Forms.Label();
            this.dtp_disposed_date = new System.Windows.Forms.DateTimePicker();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_search = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgv_list = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.category_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.acquired_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.pnl_content.SuspendLayout();
            this.panel3.SuspendLayout();
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
            this.label1.Size = new System.Drawing.Size(206, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Fixed Asset Setup";
            //
            // toolStrip1
            //
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_new,
            this.btn_edit,
            this.btn_delete,
            this.btn_save,
            this.btn_cancel});
            this.toolStrip1.Location = new System.Drawing.Point(0, 47);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1400, 25);
            this.toolStrip1.TabIndex = 10;
            //
            // btn_new
            //
            this.btn_new.Name = "btn_new";
            this.btn_new.Size = new System.Drawing.Size(51, 22);
            this.btn_new.Text = "New";
            this.btn_new.Click += new System.EventHandler(this.btn_new_Click);
            //
            // btn_edit
            //
            this.btn_edit.Name = "btn_edit";
            this.btn_edit.Size = new System.Drawing.Size(47, 22);
            this.btn_edit.Text = "Edit";
            this.btn_edit.Click += new System.EventHandler(this.btn_edit_Click);
            //
            // btn_delete
            //
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.Size = new System.Drawing.Size(60, 22);
            this.btn_delete.Text = "Delete";
            this.btn_delete.Click += new System.EventHandler(this.btn_delete_Click);
            //
            // btn_save
            //
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(51, 22);
            this.btn_save.Text = "Save";
            this.btn_save.Visible = false;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            //
            // btn_cancel
            //
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(63, 22);
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.Visible = false;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            //
            // pnl_content
            //
            this.pnl_content.Controls.Add(this.txt_id);
            this.pnl_content.Controls.Add(this.lbl_code);
            this.pnl_content.Controls.Add(this.txt_code);
            this.pnl_content.Controls.Add(this.lbl_name);
            this.pnl_content.Controls.Add(this.txt_name);
            this.pnl_content.Controls.Add(this.lbl_category);
            this.pnl_content.Controls.Add(this.cmb_category);
            this.pnl_content.Controls.Add(this.lbl_cost);
            this.pnl_content.Controls.Add(this.txt_cost);
            this.pnl_content.Controls.Add(this.lbl_acquired_date);
            this.pnl_content.Controls.Add(this.dtp_acquired_date);
            this.pnl_content.Controls.Add(this.lbl_useful_life_years);
            this.pnl_content.Controls.Add(this.txt_useful_life_years);
            this.pnl_content.Controls.Add(this.lbl_salvage_value);
            this.pnl_content.Controls.Add(this.txt_salvage_value);
            this.pnl_content.Controls.Add(this.lbl_status);
            this.pnl_content.Controls.Add(this.cmb_status);
            this.pnl_content.Controls.Add(this.lbl_disposed_date);
            this.pnl_content.Controls.Add(this.dtp_disposed_date);
            this.pnl_content.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_content.Location = new System.Drawing.Point(0, 72);
            this.pnl_content.Name = "pnl_content";
            this.pnl_content.Size = new System.Drawing.Size(1400, 260);
            this.pnl_content.TabIndex = 64;
            //
            // txt_id
            //
            this.txt_id.Location = new System.Drawing.Point(1100, 20);
            this.txt_id.Name = "txt_id";
            this.txt_id.Size = new System.Drawing.Size(199, 20);
            this.txt_id.TabIndex = 63;
            this.txt_id.Visible = false;
            //
            // lbl_code
            //
            this.lbl_code.AutoSize = true;
            this.lbl_code.Location = new System.Drawing.Point(34, 23);
            this.lbl_code.Size = new System.Drawing.Size(80, 13);
            this.lbl_code.Text = "ASSET TAG";
            //
            // txt_code
            //
            this.txt_code.BackColor = System.Drawing.Color.Gainsboro;
            this.txt_code.Location = new System.Drawing.Point(150, 20);
            this.txt_code.Name = "txt_code";
            this.txt_code.ReadOnly = true;
            this.txt_code.Size = new System.Drawing.Size(289, 20);
            this.txt_code.TabIndex = 59;
            this.txt_code.Tag = "REQUIRED";
            //
            // lbl_name
            //
            this.lbl_name.AutoSize = true;
            this.lbl_name.Location = new System.Drawing.Point(34, 49);
            this.lbl_name.Size = new System.Drawing.Size(80, 13);
            this.lbl_name.Text = "NAME";
            //
            // txt_name
            //
            this.txt_name.BackColor = System.Drawing.Color.Gainsboro;
            this.txt_name.Location = new System.Drawing.Point(150, 46);
            this.txt_name.Name = "txt_name";
            this.txt_name.ReadOnly = true;
            this.txt_name.Size = new System.Drawing.Size(289, 20);
            this.txt_name.TabIndex = 61;
            this.txt_name.Tag = "REQUIRED";
            //
            // lbl_category
            //
            this.lbl_category.AutoSize = true;
            this.lbl_category.Location = new System.Drawing.Point(34, 75);
            this.lbl_category.Size = new System.Drawing.Size(80, 13);
            this.lbl_category.Text = "CATEGORY";
            //
            // cmb_category
            //
            this.cmb_category.BackColor = System.Drawing.Color.White;
            this.cmb_category.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_category.Enabled = false;
            this.cmb_category.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmb_category.Location = new System.Drawing.Point(150, 72);
            this.cmb_category.Name = "cmb_category";
            this.cmb_category.Size = new System.Drawing.Size(289, 21);
            this.cmb_category.TabIndex = 101;
            this.cmb_category.Tag = "REQUIRED";
            //
            // lbl_cost
            //
            this.lbl_cost.AutoSize = true;
            this.lbl_cost.Location = new System.Drawing.Point(34, 101);
            this.lbl_cost.Size = new System.Drawing.Size(80, 13);
            this.lbl_cost.Text = "COST";
            //
            // txt_cost
            //
            this.txt_cost.BackColor = System.Drawing.Color.Gainsboro;
            this.txt_cost.Location = new System.Drawing.Point(150, 98);
            this.txt_cost.Name = "txt_cost";
            this.txt_cost.ReadOnly = true;
            this.txt_cost.Size = new System.Drawing.Size(289, 20);
            this.txt_cost.TabIndex = 62;
            this.txt_cost.Tag = "MONEY REQUIRED";
            this.txt_cost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            //
            // lbl_acquired_date
            //
            this.lbl_acquired_date.AutoSize = true;
            this.lbl_acquired_date.Location = new System.Drawing.Point(34, 127);
            this.lbl_acquired_date.Size = new System.Drawing.Size(80, 13);
            this.lbl_acquired_date.Text = "ACQUIRED DATE";
            //
            // dtp_acquired_date
            //
            this.dtp_acquired_date.Enabled = false;
            this.dtp_acquired_date.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_acquired_date.Location = new System.Drawing.Point(150, 124);
            this.dtp_acquired_date.Name = "dtp_acquired_date";
            this.dtp_acquired_date.Size = new System.Drawing.Size(289, 20);
            this.dtp_acquired_date.TabIndex = 102;
            //
            // lbl_useful_life_years
            //
            this.lbl_useful_life_years.AutoSize = true;
            this.lbl_useful_life_years.Location = new System.Drawing.Point(34, 153);
            this.lbl_useful_life_years.Size = new System.Drawing.Size(120, 13);
            this.lbl_useful_life_years.Text = "USEFUL LIFE (YEARS)";
            //
            // txt_useful_life_years
            //
            this.txt_useful_life_years.BackColor = System.Drawing.Color.Gainsboro;
            this.txt_useful_life_years.Location = new System.Drawing.Point(150, 150);
            this.txt_useful_life_years.Name = "txt_useful_life_years";
            this.txt_useful_life_years.ReadOnly = true;
            this.txt_useful_life_years.Size = new System.Drawing.Size(289, 20);
            this.txt_useful_life_years.TabIndex = 63;
            this.txt_useful_life_years.Tag = "REQUIRED";
            this.txt_useful_life_years.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            //
            // lbl_salvage_value
            //
            this.lbl_salvage_value.AutoSize = true;
            this.lbl_salvage_value.Location = new System.Drawing.Point(34, 179);
            this.lbl_salvage_value.Size = new System.Drawing.Size(80, 13);
            this.lbl_salvage_value.Text = "SALVAGE VALUE";
            //
            // txt_salvage_value
            //
            this.txt_salvage_value.BackColor = System.Drawing.Color.Gainsboro;
            this.txt_salvage_value.Location = new System.Drawing.Point(150, 176);
            this.txt_salvage_value.Name = "txt_salvage_value";
            this.txt_salvage_value.ReadOnly = true;
            this.txt_salvage_value.Size = new System.Drawing.Size(289, 20);
            this.txt_salvage_value.TabIndex = 64;
            this.txt_salvage_value.Tag = "MONEY";
            this.txt_salvage_value.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            //
            // lbl_status
            //
            this.lbl_status.AutoSize = true;
            this.lbl_status.Location = new System.Drawing.Point(500, 23);
            this.lbl_status.Size = new System.Drawing.Size(80, 13);
            this.lbl_status.Text = "STATUS";
            //
            // cmb_status
            //
            this.cmb_status.BackColor = System.Drawing.Color.White;
            this.cmb_status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_status.Enabled = false;
            this.cmb_status.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmb_status.Items.AddRange(new object[] { "ACTIVE", "DISPOSED" });
            this.cmb_status.Location = new System.Drawing.Point(600, 20);
            this.cmb_status.Name = "cmb_status";
            this.cmb_status.Size = new System.Drawing.Size(220, 21);
            this.cmb_status.TabIndex = 103;
            this.cmb_status.SelectedIndexChanged += new System.EventHandler(this.cmb_status_SelectedIndexChanged);
            //
            // lbl_disposed_date
            //
            this.lbl_disposed_date.AutoSize = true;
            this.lbl_disposed_date.Location = new System.Drawing.Point(500, 49);
            this.lbl_disposed_date.Size = new System.Drawing.Size(80, 13);
            this.lbl_disposed_date.Text = "DISPOSED DATE";
            //
            // dtp_disposed_date
            //
            this.dtp_disposed_date.Enabled = false;
            this.dtp_disposed_date.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_disposed_date.Location = new System.Drawing.Point(600, 46);
            this.dtp_disposed_date.Name = "dtp_disposed_date";
            this.dtp_disposed_date.Size = new System.Drawing.Size(220, 20);
            this.dtp_disposed_date.TabIndex = 104;
            //
            // panel3
            //
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.txt_search);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 332);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1400, 51);
            this.panel3.TabIndex = 65;
            //
            // label5
            //
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 22);
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.Text = "SEARCH";
            //
            // txt_search
            //
            this.txt_search.Location = new System.Drawing.Point(86, 19);
            this.txt_search.Name = "txt_search";
            this.txt_search.Size = new System.Drawing.Size(289, 20);
            this.txt_search.TabIndex = 6;
            this.txt_search.TextChanged += new System.EventHandler(this.txt_search_TextChanged);
            //
            // panel2
            //
            this.panel2.Controls.Add(this.dgv_list);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 383);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1400, 567);
            this.panel2.TabIndex = 2;
            //
            // dgv_list
            //
            this.dgv_list.AllowUserToAddRows = false;
            this.dgv_list.AllowUserToDeleteRows = false;
            this.dgv_list.AllowUserToResizeColumns = false;
            this.dgv_list.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            this.dgv_list.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_list.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_list.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.code,
            this.name,
            this.category_name,
            this.cost,
            this.acquired_date,
            this.status});
            this.dgv_list.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_list.Location = new System.Drawing.Point(0, 0);
            this.dgv_list.Name = "dgv_list";
            this.dgv_list.ReadOnly = true;
            this.dgv_list.RowHeadersVisible = false;
            this.dgv_list.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_list.Size = new System.Drawing.Size(1400, 567);
            this.dgv_list.TabIndex = 4;
            this.dgv_list.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_list_CellClick);
            this.dgv_list.SelectionChanged += new System.EventHandler(this.dgv_list_SelectionChanged);
            //
            // id
            //
            this.id.DataPropertyName = "id";
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Gainsboro;
            this.id.DefaultCellStyle = dataGridViewCellStyle2;
            this.id.HeaderText = "ID";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            //
            // code
            //
            this.code.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.code.DataPropertyName = "code";
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Gainsboro;
            this.code.DefaultCellStyle = dataGridViewCellStyle3;
            this.code.HeaderText = "ASSET TAG";
            this.code.Name = "code";
            this.code.ReadOnly = true;
            //
            // name
            //
            this.name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.name.DataPropertyName = "name";
            this.name.HeaderText = "NAME";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            //
            // category_name
            //
            this.category_name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.category_name.DataPropertyName = "category_name";
            this.category_name.HeaderText = "CATEGORY";
            this.category_name.Name = "category_name";
            this.category_name.ReadOnly = true;
            //
            // cost
            //
            this.cost.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cost.DataPropertyName = "cost";
            this.cost.HeaderText = "COST";
            this.cost.Name = "cost";
            this.cost.ReadOnly = true;
            //
            // acquired_date
            //
            this.acquired_date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.acquired_date.DataPropertyName = "acquired_date";
            this.acquired_date.HeaderText = "ACQUIRED DATE";
            this.acquired_date.Name = "acquired_date";
            this.acquired_date.ReadOnly = true;
            //
            // status
            //
            this.status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.status.DataPropertyName = "status";
            this.status.HeaderText = "STATUS";
            this.status.Name = "status";
            this.status.ReadOnly = true;
            //
            // FixedAssetPage
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.pnl_content);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panel1);
            this.Name = "FixedAssetPage";
            this.Size = new System.Drawing.Size(1400, 950);
            this.Load += new System.EventHandler(this.FixedAssetPage_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.pnl_content.ResumeLayout(false);
            this.pnl_content.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_list)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btn_new;
        private System.Windows.Forms.ToolStripButton btn_edit;
        private System.Windows.Forms.ToolStripButton btn_delete;
        private System.Windows.Forms.ToolStripButton btn_save;
        private System.Windows.Forms.ToolStripButton btn_cancel;
        private System.Windows.Forms.Panel pnl_content;
        private System.Windows.Forms.TextBox txt_id;
        private System.Windows.Forms.Label lbl_code;
        private System.Windows.Forms.TextBox txt_code;
        private System.Windows.Forms.Label lbl_name;
        private System.Windows.Forms.TextBox txt_name;
        private System.Windows.Forms.Label lbl_category;
        private System.Windows.Forms.ComboBox cmb_category;
        private System.Windows.Forms.Label lbl_cost;
        private System.Windows.Forms.TextBox txt_cost;
        private System.Windows.Forms.Label lbl_acquired_date;
        private System.Windows.Forms.DateTimePicker dtp_acquired_date;
        private System.Windows.Forms.Label lbl_useful_life_years;
        private System.Windows.Forms.TextBox txt_useful_life_years;
        private System.Windows.Forms.Label lbl_salvage_value;
        private System.Windows.Forms.TextBox txt_salvage_value;
        private System.Windows.Forms.Label lbl_status;
        private System.Windows.Forms.ComboBox cmb_status;
        private System.Windows.Forms.Label lbl_disposed_date;
        private System.Windows.Forms.DateTimePicker dtp_disposed_date;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_search;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgv_list;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn code;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn category_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn cost;
        private System.Windows.Forms.DataGridViewTextBoxColumn acquired_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
    }
}
