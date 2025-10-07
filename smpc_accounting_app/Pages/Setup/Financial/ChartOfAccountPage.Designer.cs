
namespace smpc_accounting_app.Pages.Setup.Financial
{
    partial class ChartOfAccountPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChartOfAccountPage));
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataset = new System.Data.DataSet();
            this.dataTable1 = new System.Data.DataTable();
            this.id = new System.Data.DataColumn();
            this.account_code = new System.Data.DataColumn();
            this.account_name = new System.Data.DataColumn();
            this.short_name = new System.Data.DataColumn();
            this.account_type = new System.Data.DataColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.treeView = new System.Windows.Forms.TreeView();
            this.pnl_content = new System.Windows.Forms.Panel();
            this.txt_short_name = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmb_account_type = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_id = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_account_code = new System.Windows.Forms.TextBox();
            this.txt_account_name = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btn_new_subchart = new System.Windows.Forms.ToolStripButton();
            this.btn_edit = new System.Windows.Forms.ToolStripButton();
            this.btn_delete = new System.Windows.Forms.ToolStripButton();
            this.btn_print = new System.Windows.Forms.ToolStripButton();
            this.btn_save = new System.Windows.Forms.ToolStripButton();
            this.btn_cancel = new System.Windows.Forms.ToolStripButton();
            this.btn_new = new System.Windows.Forms.ToolStripButton();
            this.txt_parent_id = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_parent_account = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).BeginInit();
            this.panel2.SuspendLayout();
            this.pnl_content.SuspendLayout();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bindingSource
            // 
            this.bindingSource.DataMember = "ds_table_setup";
            this.bindingSource.DataSource = this.dataset;
            // 
            // dataset
            // 
            this.dataset.DataSetName = "ds";
            this.dataset.Tables.AddRange(new System.Data.DataTable[] {
            this.dataTable1});
            // 
            // dataTable1
            // 
            this.dataTable1.Columns.AddRange(new System.Data.DataColumn[] {
            this.id,
            this.account_code,
            this.account_name,
            this.short_name,
            this.account_type});
            this.dataTable1.TableName = "ds_table_setup";
            // 
            // id
            // 
            this.id.ColumnName = "id";
            // 
            // account_code
            // 
            this.account_code.ColumnName = "account_code";
            // 
            // account_name
            // 
            this.account_name.ColumnName = "account_name";
            // 
            // short_name
            // 
            this.short_name.ColumnName = "short_name";
            // 
            // account_type
            // 
            this.account_type.ColumnName = "account_type";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.treeView);
            this.panel2.Controls.Add(this.pnl_content);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 72);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(823, 616);
            this.panel2.TabIndex = 9;
            // 
            // treeView
            // 
            this.treeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView.Location = new System.Drawing.Point(438, 3);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(382, 610);
            this.treeView.TabIndex = 5;
            this.treeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseClick);
            // 
            // pnl_content
            // 
            this.pnl_content.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pnl_content.Controls.Add(this.label9);
            this.pnl_content.Controls.Add(this.txt_parent_account);
            this.pnl_content.Controls.Add(this.txt_parent_id);
            this.pnl_content.Controls.Add(this.label8);
            this.pnl_content.Controls.Add(this.txt_short_name);
            this.pnl_content.Controls.Add(this.label7);
            this.pnl_content.Controls.Add(this.cmb_account_type);
            this.pnl_content.Controls.Add(this.label6);
            this.pnl_content.Controls.Add(this.txt_id);
            this.pnl_content.Controls.Add(this.label3);
            this.pnl_content.Controls.Add(this.label4);
            this.pnl_content.Controls.Add(this.txt_account_code);
            this.pnl_content.Controls.Add(this.txt_account_name);
            this.pnl_content.Controls.Add(this.label2);
            this.pnl_content.Enabled = false;
            this.pnl_content.Location = new System.Drawing.Point(3, 4);
            this.pnl_content.Name = "pnl_content";
            this.pnl_content.Size = new System.Drawing.Size(429, 227);
            this.pnl_content.TabIndex = 4;
            // 
            // txt_short_name
            // 
            this.txt_short_name.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txt_short_name.Location = new System.Drawing.Point(104, 137);
            this.txt_short_name.Name = "txt_short_name";
            this.txt_short_name.Size = new System.Drawing.Size(200, 20);
            this.txt_short_name.TabIndex = 77;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 140);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 13);
            this.label7.TabIndex = 76;
            this.label7.Text = "SHORT NAME";
            // 
            // cmb_account_type
            // 
            this.cmb_account_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_account_type.FormattingEnabled = true;
            this.cmb_account_type.Items.AddRange(new object[] {
            "ASSET",
            "LIABILITY",
            "CAPITAL",
            "INCOME",
            "EXPENSE"});
            this.cmb_account_type.Location = new System.Drawing.Point(104, 50);
            this.cmb_account_type.Name = "cmb_account_type";
            this.cmb_account_type.Size = new System.Drawing.Size(200, 21);
            this.cmb_account_type.TabIndex = 75;
            this.cmb_account_type.SelectedValueChanged += new System.EventHandler(this.cmb_type_SelectedValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 53);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 74;
            this.label6.Text = "TYPE";
            // 
            // txt_id
            // 
            this.txt_id.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txt_id.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource, "id", true));
            this.txt_id.Location = new System.Drawing.Point(288, 163);
            this.txt_id.Name = "txt_id";
            this.txt_id.Size = new System.Drawing.Size(104, 20);
            this.txt_id.TabIndex = 71;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 66;
            this.label3.Text = "CODE";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(254, 166);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(16, 13);
            this.label4.TabIndex = 70;
            this.label4.Text = "Id";
            // 
            // txt_account_code
            // 
            this.txt_account_code.Location = new System.Drawing.Point(104, 72);
            this.txt_account_code.Name = "txt_account_code";
            this.txt_account_code.ReadOnly = true;
            this.txt_account_code.Size = new System.Drawing.Size(200, 20);
            this.txt_account_code.TabIndex = 67;
            // 
            // txt_account_name
            // 
            this.txt_account_name.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txt_account_name.Location = new System.Drawing.Point(104, 93);
            this.txt_account_name.Multiline = true;
            this.txt_account_name.Name = "txt_account_name";
            this.txt_account_name.Size = new System.Drawing.Size(306, 43);
            this.txt_account_name.TabIndex = 69;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 68;
            this.label2.Text = "NAME";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(823, 47);
            this.panel1.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(229, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Chart of account setup";
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_new,
            this.btn_new_subchart,
            this.btn_edit,
            this.btn_delete,
            this.btn_print,
            this.btn_save,
            this.btn_cancel});
            this.toolStrip1.Location = new System.Drawing.Point(0, 47);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(823, 25);
            this.toolStrip1.TabIndex = 11;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btn_new_subchart
            // 
            this.btn_new_subchart.Image = ((System.Drawing.Image)(resources.GetObject("btn_new_subchart.Image")));
            this.btn_new_subchart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_new_subchart.Name = "btn_new_subchart";
            this.btn_new_subchart.Size = new System.Drawing.Size(101, 22);
            this.btn_new_subchart.Text = "New Subchart";
            this.btn_new_subchart.Click += new System.EventHandler(this.btn_new_subchart_Click);
            // 
            // btn_edit
            // 
            this.btn_edit.Image = ((System.Drawing.Image)(resources.GetObject("btn_edit.Image")));
            this.btn_edit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_edit.Name = "btn_edit";
            this.btn_edit.Size = new System.Drawing.Size(47, 22);
            this.btn_edit.Text = "Edit";
            this.btn_edit.Click += new System.EventHandler(this.btn_edit_Click);
            // 
            // btn_delete
            // 
            this.btn_delete.Image = ((System.Drawing.Image)(resources.GetObject("btn_delete.Image")));
            this.btn_delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.Size = new System.Drawing.Size(60, 22);
            this.btn_delete.Text = "Delete";
            this.btn_delete.Click += new System.EventHandler(this.btn_delete_Click);
            // 
            // btn_print
            // 
            this.btn_print.Image = ((System.Drawing.Image)(resources.GetObject("btn_print.Image")));
            this.btn_print.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_print.Name = "btn_print";
            this.btn_print.Size = new System.Drawing.Size(52, 22);
            this.btn_print.Text = "Print";
            // 
            // btn_save
            // 
            this.btn_save.Image = ((System.Drawing.Image)(resources.GetObject("btn_save.Image")));
            this.btn_save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(51, 22);
            this.btn_save.Text = "Save";
            this.btn_save.Visible = false;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Image = ((System.Drawing.Image)(resources.GetObject("btn_cancel.Image")));
            this.btn_cancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(63, 22);
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.Visible = false;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_new
            // 
            this.btn_new.Image = ((System.Drawing.Image)(resources.GetObject("btn_new.Image")));
            this.btn_new.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_new.Name = "btn_new";
            this.btn_new.Size = new System.Drawing.Size(51, 22);
            this.btn_new.Text = "New";
            this.btn_new.Click += new System.EventHandler(this.btn_new_Click_1);
            // 
            // txt_parent_id
            // 
            this.txt_parent_id.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txt_parent_id.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource, "id", true));
            this.txt_parent_id.Location = new System.Drawing.Point(288, 186);
            this.txt_parent_id.Name = "txt_parent_id";
            this.txt_parent_id.Size = new System.Drawing.Size(104, 20);
            this.txt_parent_id.TabIndex = 81;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(221, 189);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 13);
            this.label8.TabIndex = 80;
            this.label8.Text = "parent_id";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 32);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(91, 13);
            this.label9.TabIndex = 82;
            this.label9.Text = "PARENT CHART";
            // 
            // txt_parent_account
            // 
            this.txt_parent_account.Location = new System.Drawing.Point(104, 29);
            this.txt_parent_account.Name = "txt_parent_account";
            this.txt_parent_account.ReadOnly = true;
            this.txt_parent_account.Size = new System.Drawing.Size(200, 20);
            this.txt_parent_account.TabIndex = 83;
            // 
            // ChartOfAccountPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panel1);
            this.Name = "ChartOfAccountPage";
            this.Size = new System.Drawing.Size(823, 688);
            this.Load += new System.EventHandler(this.ChartOfAccountPage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.pnl_content.ResumeLayout(false);
            this.pnl_content.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource bindingSource;
        private System.Data.DataSet dataset;
        private System.Data.DataTable dataTable1;
        private System.Data.DataColumn id;
        private System.Data.DataColumn account_code;
        private System.Data.DataColumn account_name;
        private System.Data.DataColumn short_name;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btn_new_subchart;
        private System.Windows.Forms.ToolStripButton btn_edit;
        private System.Windows.Forms.ToolStripButton btn_delete;
        private System.Windows.Forms.ToolStripButton btn_print;
        private System.Windows.Forms.ToolStripButton btn_save;
        private System.Windows.Forms.ToolStripButton btn_cancel;
        private System.Data.DataColumn account_type;
        private System.Windows.Forms.Panel pnl_content;
        private System.Windows.Forms.TextBox txt_id;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_account_code;
        private System.Windows.Forms.TextBox txt_account_name;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmb_account_type;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_short_name;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.ToolStripButton btn_new;
        private System.Windows.Forms.TextBox txt_parent_id;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_parent_account;
    }
}
