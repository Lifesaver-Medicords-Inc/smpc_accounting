
namespace smpc_accounting_app.Pages.Setup.Tax
{
    partial class InputVatPage
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InputVatPage));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_id = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_code = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pnl_content = new System.Windows.Forms.Panel();
            this.chk_input_tax_creditable = new System.Windows.Forms.CheckBox();
            this.cmb_coa_purchase = new System.Windows.Forms.ComboBox();
            this.cmb_coa_sales = new System.Windows.Forms.ComboBox();
            this.dgv_tax_details = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tax_code_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valid_from = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valid_to = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tax_rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valid_status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label10 = new System.Windows.Forms.Label();
            this.txt_remarks = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_tax_desc = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btn_new = new System.Windows.Forms.ToolStripButton();
            this.btn_edit = new System.Windows.Forms.ToolStripButton();
            this.btn_delete = new System.Windows.Forms.ToolStripButton();
            this.btn_save = new System.Windows.Forms.ToolStripButton();
            this.btn_cancel = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel_tax_list = new System.Windows.Forms.Panel();
            this.dgv_tax_code_list = new System.Windows.Forms.DataGridView();
            this.view_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tax_desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.effective_period = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.coa_purch_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.input_tax_account = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.coa_sales_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.output_tax_account = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.account_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel_tax_code_list = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_search = new System.Windows.Forms.TextBox();
            this.panel3.SuspendLayout();
            this.pnl_content.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_tax_details)).BeginInit();
            this.panel5.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel_tax_list.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_tax_code_list)).BeginInit();
            this.panel_tax_code_list.SuspendLayout();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 99);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(177, 13);
            this.label6.TabIndex = 64;
            this.label6.Text = "OUTPUT TAX ACCOUNT (SALES):";
            // 
            // txt_id
            // 
            this.txt_id.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txt_id.Location = new System.Drawing.Point(387, 25);
            this.txt_id.Name = "txt_id";
            this.txt_id.Size = new System.Drawing.Size(38, 20);
            this.txt_id.TabIndex = 63;
            this.txt_id.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 57;
            this.label3.Text = "TAX CODE";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(365, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(16, 13);
            this.label4.TabIndex = 62;
            this.label4.Text = "Id";
            this.label4.Visible = false;
            // 
            // txt_code
            // 
            this.txt_code.Location = new System.Drawing.Point(119, 22);
            this.txt_code.Name = "txt_code";
            this.txt_code.Size = new System.Drawing.Size(235, 20);
            this.txt_code.TabIndex = 59;
            this.txt_code.Tag = "REQUIRED";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.pnl_content);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 47);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1090, 311);
            this.panel3.TabIndex = 7;
            // 
            // pnl_content
            // 
            this.pnl_content.Controls.Add(this.chk_input_tax_creditable);
            this.pnl_content.Controls.Add(this.cmb_coa_purchase);
            this.pnl_content.Controls.Add(this.cmb_coa_sales);
            this.pnl_content.Controls.Add(this.dgv_tax_details);
            this.pnl_content.Controls.Add(this.label10);
            this.pnl_content.Controls.Add(this.txt_remarks);
            this.pnl_content.Controls.Add(this.label9);
            this.pnl_content.Controls.Add(this.label8);
            this.pnl_content.Controls.Add(this.label6);
            this.pnl_content.Controls.Add(this.txt_id);
            this.pnl_content.Controls.Add(this.label3);
            this.pnl_content.Controls.Add(this.label4);
            this.pnl_content.Controls.Add(this.txt_code);
            this.pnl_content.Controls.Add(this.txt_tax_desc);
            this.pnl_content.Controls.Add(this.label2);
            this.pnl_content.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_content.Enabled = false;
            this.pnl_content.Location = new System.Drawing.Point(0, 28);
            this.pnl_content.Name = "pnl_content";
            this.pnl_content.Size = new System.Drawing.Size(1090, 283);
            this.pnl_content.TabIndex = 64;
            // 
            // chk_input_tax_creditable
            // 
            this.chk_input_tax_creditable.AutoSize = true;
            this.chk_input_tax_creditable.Location = new System.Drawing.Point(119, 69);
            this.chk_input_tax_creditable.Name = "chk_input_tax_creditable";
            this.chk_input_tax_creditable.Size = new System.Drawing.Size(153, 17);
            this.chk_input_tax_creditable.TabIndex = 66;
            this.chk_input_tax_creditable.Text = "INPUT TAX CREDITABLE";
            this.chk_input_tax_creditable.UseVisualStyleBackColor = true;
            // 
            // cmb_coa_purchase
            // 
            this.cmb_coa_purchase.BackColor = System.Drawing.Color.White;
            this.cmb_coa_purchase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_coa_purchase.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmb_coa_purchase.FormattingEnabled = true;
            this.cmb_coa_purchase.Location = new System.Drawing.Point(213, 119);
            this.cmb_coa_purchase.MaxLength = 50;
            this.cmb_coa_purchase.MinimumSize = new System.Drawing.Size(200, 0);
            this.cmb_coa_purchase.Name = "cmb_coa_purchase";
            this.cmb_coa_purchase.Size = new System.Drawing.Size(235, 21);
            this.cmb_coa_purchase.TabIndex = 103;
            this.cmb_coa_purchase.TabStop = false;
            this.cmb_coa_purchase.Tag = "";
            this.cmb_coa_purchase.SelectedIndexChanged += new System.EventHandler(this.cmb_coa_purchase_SelectedIndexChanged);
            this.cmb_coa_purchase.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmb_coa_purchase_KeyDown);
            // 
            // cmb_coa_sales
            // 
            this.cmb_coa_sales.BackColor = System.Drawing.Color.White;
            this.cmb_coa_sales.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_coa_sales.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmb_coa_sales.FormattingEnabled = true;
            this.cmb_coa_sales.Location = new System.Drawing.Point(213, 96);
            this.cmb_coa_sales.MaxLength = 50;
            this.cmb_coa_sales.MinimumSize = new System.Drawing.Size(200, 0);
            this.cmb_coa_sales.Name = "cmb_coa_sales";
            this.cmb_coa_sales.Size = new System.Drawing.Size(235, 21);
            this.cmb_coa_sales.TabIndex = 102;
            this.cmb_coa_sales.TabStop = false;
            this.cmb_coa_sales.Tag = "";
            this.cmb_coa_sales.SelectedIndexChanged += new System.EventHandler(this.cmb_coa_sales_SelectedIndexChanged);
            this.cmb_coa_sales.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmb_coa_sales_KeyDown);
            // 
            // dgv_tax_details
            // 
            this.dgv_tax_details.AllowUserToAddRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_tax_details.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_tax_details.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_tax_details.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.tax_code_id,
            this.valid_from,
            this.valid_to,
            this.tax_rate,
            this.valid_status});
            this.dgv_tax_details.Location = new System.Drawing.Point(547, 38);
            this.dgv_tax_details.Name = "dgv_tax_details";
            this.dgv_tax_details.Size = new System.Drawing.Size(520, 204);
            this.dgv_tax_details.TabIndex = 73;
            this.dgv_tax_details.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgv_tax_details_EditingControlShowing);
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "ID";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // tax_code_id
            // 
            this.tax_code_id.DataPropertyName = "tax_code_id";
            this.tax_code_id.HeaderText = "TAX CODE ID";
            this.tax_code_id.Name = "tax_code_id";
            this.tax_code_id.ReadOnly = true;
            this.tax_code_id.Visible = false;
            // 
            // valid_from
            // 
            this.valid_from.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.valid_from.DataPropertyName = "valid_from";
            this.valid_from.HeaderText = "VALID FROM";
            this.valid_from.Name = "valid_from";
            this.valid_from.ReadOnly = true;
            // 
            // valid_to
            // 
            this.valid_to.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.valid_to.DataPropertyName = "valid_to";
            this.valid_to.HeaderText = "VALID TO";
            this.valid_to.Name = "valid_to";
            this.valid_to.ReadOnly = true;
            // 
            // tax_rate
            // 
            this.tax_rate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.tax_rate.DataPropertyName = "tax_rate";
            this.tax_rate.HeaderText = "TAX RATE (%)";
            this.tax_rate.Name = "tax_rate";
            this.tax_rate.ReadOnly = true;
            // 
            // valid_status
            // 
            this.valid_status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.valid_status.DataPropertyName = "valid_status";
            this.valid_status.HeaderText = "STATUS";
            this.valid_status.Name = "valid_status";
            this.valid_status.ReadOnly = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(544, 22);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(99, 13);
            this.label10.TabIndex = 72;
            this.label10.Text = "VALIDITY PERIOD";
            // 
            // txt_remarks
            // 
            this.txt_remarks.Location = new System.Drawing.Point(77, 166);
            this.txt_remarks.Multiline = true;
            this.txt_remarks.Name = "txt_remarks";
            this.txt_remarks.Size = new System.Drawing.Size(371, 107);
            this.txt_remarks.TabIndex = 70;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 169);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 13);
            this.label9.TabIndex = 69;
            this.label9.Text = "REMARKS:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 124);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(202, 13);
            this.label8.TabIndex = 67;
            this.label8.Text = "INPUT TAX ACCOUNT (PURCHASING):";
            // 
            // txt_tax_desc
            // 
            this.txt_tax_desc.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txt_tax_desc.Location = new System.Drawing.Point(119, 43);
            this.txt_tax_desc.Name = "txt_tax_desc";
            this.txt_tax_desc.Size = new System.Drawing.Size(235, 20);
            this.txt_tax_desc.TabIndex = 61;
            this.txt_tax_desc.Tag = "REQUIRED";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 13);
            this.label2.TabIndex = 60;
            this.label2.Text = "TAX DESCRIPTION";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.toolStrip1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1090, 28);
            this.panel5.TabIndex = 65;
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
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(1090, 25);
            this.toolStrip1.TabIndex = 10;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btn_new
            // 
            this.btn_new.Image = ((System.Drawing.Image)(resources.GetObject("btn_new.Image")));
            this.btn_new.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_new.Name = "btn_new";
            this.btn_new.Size = new System.Drawing.Size(51, 22);
            this.btn_new.Text = "New";
            this.btn_new.Click += new System.EventHandler(this.btn_new_Click);
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
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1090, 47);
            this.panel1.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tax Code Setup";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 358);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1090, 507);
            this.panel2.TabIndex = 6;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel_tax_list);
            this.groupBox1.Controls.Add(this.panel_tax_code_list);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1090, 507);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // panel_tax_list
            // 
            this.panel_tax_list.Controls.Add(this.dgv_tax_code_list);
            this.panel_tax_list.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_tax_list.Location = new System.Drawing.Point(3, 94);
            this.panel_tax_list.Name = "panel_tax_list";
            this.panel_tax_list.Size = new System.Drawing.Size(1084, 410);
            this.panel_tax_list.TabIndex = 9;
            // 
            // dgv_tax_code_list
            // 
            this.dgv_tax_code_list.AllowUserToAddRows = false;
            this.dgv_tax_code_list.AllowUserToDeleteRows = false;
            this.dgv_tax_code_list.AllowUserToResizeColumns = false;
            this.dgv_tax_code_list.AllowUserToResizeRows = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_tax_code_list.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_tax_code_list.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_tax_code_list.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.view_id,
            this.code,
            this.tax_desc,
            this.effective_period,
            this.dataGridViewTextBoxColumn1,
            this.coa_purch_id,
            this.input_tax_account,
            this.coa_sales_id,
            this.output_tax_account,
            this.account_type});
            this.dgv_tax_code_list.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_tax_code_list.Location = new System.Drawing.Point(0, 0);
            this.dgv_tax_code_list.Name = "dgv_tax_code_list";
            this.dgv_tax_code_list.ReadOnly = true;
            this.dgv_tax_code_list.RowHeadersVisible = false;
            this.dgv_tax_code_list.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_tax_code_list.Size = new System.Drawing.Size(1084, 410);
            this.dgv_tax_code_list.TabIndex = 9;
            this.dgv_tax_code_list.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_tax_code_list_CellClick);
            this.dgv_tax_code_list.SelectionChanged += new System.EventHandler(this.dgv_tax_code_list_SelectionChanged);
            // 
            // view_id
            // 
            this.view_id.DataPropertyName = "view_id";
            this.view_id.HeaderText = "ID";
            this.view_id.Name = "view_id";
            this.view_id.ReadOnly = true;
            this.view_id.Visible = false;
            // 
            // code
            // 
            this.code.DataPropertyName = "code";
            this.code.HeaderText = "TAX CODE";
            this.code.Name = "code";
            this.code.ReadOnly = true;
            // 
            // tax_desc
            // 
            this.tax_desc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.tax_desc.DataPropertyName = "tax_desc";
            this.tax_desc.HeaderText = "TAX DESCRIPTION";
            this.tax_desc.Name = "tax_desc";
            this.tax_desc.ReadOnly = true;
            // 
            // effective_period
            // 
            this.effective_period.DataPropertyName = "effective_period";
            this.effective_period.HeaderText = "EFFECTIVE PERIOD";
            this.effective_period.Name = "effective_period";
            this.effective_period.ReadOnly = true;
            this.effective_period.Width = 150;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "tax_rate";
            this.dataGridViewTextBoxColumn1.HeaderText = "TAX RATE";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 150;
            // 
            // coa_purch_id
            // 
            this.coa_purch_id.DataPropertyName = "coa_purch_id";
            this.coa_purch_id.HeaderText = "COA PURCH ID";
            this.coa_purch_id.Name = "coa_purch_id";
            this.coa_purch_id.ReadOnly = true;
            this.coa_purch_id.Visible = false;
            // 
            // input_tax_account
            // 
            this.input_tax_account.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.input_tax_account.DataPropertyName = "input_tax_account";
            this.input_tax_account.HeaderText = "INPUT TAX ACCOUNT (PURCHASE)";
            this.input_tax_account.Name = "input_tax_account";
            this.input_tax_account.ReadOnly = true;
            this.input_tax_account.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // coa_sales_id
            // 
            this.coa_sales_id.DataPropertyName = "coa_sales_id";
            this.coa_sales_id.HeaderText = "COA SALES ID";
            this.coa_sales_id.Name = "coa_sales_id";
            this.coa_sales_id.ReadOnly = true;
            this.coa_sales_id.Visible = false;
            // 
            // output_tax_account
            // 
            this.output_tax_account.DataPropertyName = "output_tax_account";
            this.output_tax_account.HeaderText = "OUTPUT TAX ACCOUNT (SALES)";
            this.output_tax_account.Name = "output_tax_account";
            this.output_tax_account.ReadOnly = true;
            this.output_tax_account.Width = 200;
            // 
            // account_type
            // 
            this.account_type.DataPropertyName = "account_type";
            this.account_type.HeaderText = "ACCOUNT TYPE";
            this.account_type.Name = "account_type";
            this.account_type.ReadOnly = true;
            this.account_type.Visible = false;
            // 
            // panel_tax_code_list
            // 
            this.panel_tax_code_list.Controls.Add(this.label7);
            this.panel_tax_code_list.Controls.Add(this.label5);
            this.panel_tax_code_list.Controls.Add(this.txt_search);
            this.panel_tax_code_list.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_tax_code_list.Location = new System.Drawing.Point(3, 16);
            this.panel_tax_code_list.Name = "panel_tax_code_list";
            this.panel_tax_code_list.Size = new System.Drawing.Size(1084, 78);
            this.panel_tax_code_list.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(9, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(145, 25);
            this.label7.TabIndex = 8;
            this.label7.Text = "Tax Code List";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "SEARCH";
            // 
            // txt_search
            // 
            this.txt_search.Location = new System.Drawing.Point(61, 53);
            this.txt_search.Name = "txt_search";
            this.txt_search.Size = new System.Drawing.Size(180, 20);
            this.txt_search.TabIndex = 6;
            this.txt_search.TextChanged += new System.EventHandler(this.txt_search_TextChanged);
            // 
            // InputVatPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "InputVatPage";
            this.Size = new System.Drawing.Size(1090, 865);
            this.Load += new System.EventHandler(this.InputVatPage_Load);
            this.panel3.ResumeLayout(false);
            this.pnl_content.ResumeLayout(false);
            this.pnl_content.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_tax_details)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panel_tax_list.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_tax_code_list)).EndInit();
            this.panel_tax_code_list.ResumeLayout(false);
            this.panel_tax_code_list.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_id;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_code;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel pnl_content;
        private System.Windows.Forms.TextBox txt_tax_desc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btn_new;
        private System.Windows.Forms.ToolStripButton btn_edit;
        private System.Windows.Forms.ToolStripButton btn_delete;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel_tax_list;
        private System.Windows.Forms.DataGridView dgv_tax_code_list;
        private System.Windows.Forms.Panel panel_tax_code_list;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_search;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_remarks;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chk_input_tax_creditable;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataGridView dgv_tax_details;
        private System.Windows.Forms.ToolStripButton btn_cancel;
        private System.Windows.Forms.ToolStripButton btn_save;
        private System.Windows.Forms.ComboBox cmb_coa_purchase;
        private System.Windows.Forms.ComboBox cmb_coa_sales;
        private System.Windows.Forms.DataGridViewTextBoxColumn view_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn code;
        private System.Windows.Forms.DataGridViewTextBoxColumn tax_desc;
        private System.Windows.Forms.DataGridViewTextBoxColumn effective_period;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn coa_purch_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn input_tax_account;
        private System.Windows.Forms.DataGridViewTextBoxColumn coa_sales_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn output_tax_account;
        private System.Windows.Forms.DataGridViewTextBoxColumn account_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn tax_code_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn valid_from;
        private System.Windows.Forms.DataGridViewTextBoxColumn valid_to;
        private System.Windows.Forms.DataGridViewTextBoxColumn tax_rate;
        private System.Windows.Forms.DataGridViewTextBoxColumn valid_status;
    }
}
