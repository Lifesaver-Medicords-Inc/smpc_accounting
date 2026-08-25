
namespace smpc_accounting_app.Pages.Transactions.Journal
{
    partial class DebitMemo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DebitMemo));
            this.panel6 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_status = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btn_new = new System.Windows.Forms.ToolStripButton();
            this.btn_search = new System.Windows.Forms.ToolStripButton();
            this.btn_prev = new System.Windows.Forms.ToolStripButton();
            this.btn_next = new System.Windows.Forms.ToolStripButton();
            this.btn_print = new System.Windows.Forms.ToolStripButton();
            this.btn_save = new System.Windows.Forms.ToolStripButton();
            this.btn_cancel = new System.Windows.Forms.ToolStripButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.txt_supplier_code = new System.Windows.Forms.TextBox();
            this.txt_supplier_id = new System.Windows.Forms.TextBox();
            this.label_supplier_name = new System.Windows.Forms.Label();
            this.txt_supplier_name = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_document_no = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_trans_amount = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmb_reason_code = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dtp_date = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_currency = new System.Windows.Forms.TextBox();
            this.label_location_group = new System.Windows.Forms.Label();
            this.txt_location_group = new System.Windows.Forms.TextBox();
            this.label_sales_period = new System.Windows.Forms.Label();
            this.txt_sales_period = new System.Windows.Forms.TextBox();
            this.label_ref_doc_no = new System.Windows.Forms.Label();
            this.txt_ref_doc_no = new System.Windows.Forms.TextBox();
            this.label_ref_po_no = new System.Windows.Forms.Label();
            this.txt_ref_po_no = new System.Windows.Forms.TextBox();
            this.label_unapplied_amount = new System.Windows.Forms.Label();
            this.txt_unapplied_amount = new System.Windows.Forms.TextBox();
            this.pnl_grid_actions = new System.Windows.Forms.Panel();
            this.btn_remove_line = new System.Windows.Forms.Button();
            this.btn_add_credit_memo = new System.Windows.Forms.Button();
            this.btn_add_invoice = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.aPPLYDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dOCNODataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dUEDATEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tOTALDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oPENAMOUNTDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aMOUNTAPPLIEDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bALANCEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel6.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnl_grid_actions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
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
            this.label1.Size = new System.Drawing.Size(145, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "DEBIT MEMO";
            //
            // lbl_status
            //
            // Inline "saving.../saved" text beside the module name - CLAUDE.md's
            // convention for new UI, no "saved successfully" modal.
            this.lbl_status.AutoSize = true;
            this.lbl_status.ForeColor = System.Drawing.Color.Gray;
            this.lbl_status.Location = new System.Drawing.Point(170, 18);
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
            this.btn_save,
            this.btn_cancel});
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
            // btn_save
            //
            // A Debit Memo commits entirely on this click (Sec12.6.3/Sec14.57) - there is
            // no draft state and no approval step, unlike Credit Memo's customer side.
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
            // panel3
            //
            // Field values marked read-only below (DOC NO., UNAPPLIED AMOUNT) are
            // system-set/computed, never typed.
            this.panel3.Controls.Add(this.label11);
            this.panel3.Controls.Add(this.txt_supplier_code);
            this.panel3.Controls.Add(this.txt_supplier_id);
            this.panel3.Controls.Add(this.label_supplier_name);
            this.panel3.Controls.Add(this.txt_supplier_name);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.txt_document_no);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.txt_trans_amount);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.cmb_reason_code);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.dtp_date);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.txt_currency);
            this.panel3.Controls.Add(this.label_location_group);
            this.panel3.Controls.Add(this.txt_location_group);
            this.panel3.Controls.Add(this.label_sales_period);
            this.panel3.Controls.Add(this.txt_sales_period);
            this.panel3.Controls.Add(this.label_ref_doc_no);
            this.panel3.Controls.Add(this.txt_ref_doc_no);
            this.panel3.Controls.Add(this.label_ref_po_no);
            this.panel3.Controls.Add(this.txt_ref_po_no);
            this.panel3.Controls.Add(this.label_unapplied_amount);
            this.panel3.Controls.Add(this.txt_unapplied_amount);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 72);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1028, 150);
            this.panel3.TabIndex = 18;
            //
            // label11
            //
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 15);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(78, 13);
            this.label11.TabIndex = 235;
            this.label11.Text = "SUPPLIER CODE";
            //
            // txt_supplier_code
            //
            this.txt_supplier_code.Location = new System.Drawing.Point(140, 12);
            this.txt_supplier_code.Name = "txt_supplier_code";
            this.txt_supplier_code.ReadOnly = true;
            this.txt_supplier_code.Size = new System.Drawing.Size(180, 20);
            this.txt_supplier_code.TabIndex = 237;
            //
            // txt_supplier_id
            //
            // Hidden - the resolved BPI id backing txt_supplier_code.
            this.txt_supplier_id.Location = new System.Drawing.Point(140, 12);
            this.txt_supplier_id.Name = "txt_supplier_id";
            this.txt_supplier_id.Size = new System.Drawing.Size(180, 20);
            this.txt_supplier_id.TabIndex = 238;
            this.txt_supplier_id.Visible = false;
            //
            // label_supplier_name
            //
            this.label_supplier_name.AutoSize = true;
            this.label_supplier_name.Location = new System.Drawing.Point(340, 15);
            this.label_supplier_name.Name = "label_supplier_name";
            this.label_supplier_name.Size = new System.Drawing.Size(80, 13);
            this.label_supplier_name.TabIndex = 239;
            this.label_supplier_name.Text = "SUPPLIER NAME";
            //
            // txt_supplier_name
            //
            this.txt_supplier_name.Location = new System.Drawing.Point(468, 12);
            this.txt_supplier_name.Name = "txt_supplier_name";
            this.txt_supplier_name.ReadOnly = true;
            this.txt_supplier_name.Size = new System.Drawing.Size(180, 20);
            this.txt_supplier_name.TabIndex = 240;
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
            // label9
            //
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 41);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(137, 13);
            this.label9.TabIndex = 245;
            this.label9.Text = "TRANS. AMOUNT";
            //
            // txt_trans_amount
            //
            this.txt_trans_amount.Location = new System.Drawing.Point(140, 38);
            this.txt_trans_amount.Name = "txt_trans_amount";
            this.txt_trans_amount.Size = new System.Drawing.Size(180, 20);
            this.txt_trans_amount.TabIndex = 244;
            this.txt_trans_amount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            //
            // label4
            //
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(340, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 13);
            this.label4.TabIndex = 255;
            this.label4.Text = "REASON CODE";
            //
            // cmb_reason_code
            //
            // Fixed 5-value list per Sec5.19 (shared with Credit Memo) - not present in
            // SEC17 despite SEC17 being described as authoritative; kept as a fixed inline
            // list per the spec text rather than promoted to an editable Setup list on our
            // own authority. Required (Sec14.58).
            this.cmb_reason_code.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_reason_code.FormattingEnabled = true;
            this.cmb_reason_code.Items.AddRange(new object[] {
            "--Select--",
            "pur return",
            "adj twas",
            "cancel chq",
            "pur disc",
            "exp cancel"});
            this.cmb_reason_code.Location = new System.Drawing.Point(468, 38);
            this.cmb_reason_code.Name = "cmb_reason_code";
            this.cmb_reason_code.Size = new System.Drawing.Size(180, 21);
            this.cmb_reason_code.TabIndex = 254;
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
            // label3
            //
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 240;
            this.label3.Text = "CURRENCY";
            //
            // txt_currency
            //
            this.txt_currency.Location = new System.Drawing.Point(140, 64);
            this.txt_currency.Name = "txt_currency";
            this.txt_currency.Size = new System.Drawing.Size(180, 20);
            this.txt_currency.TabIndex = 256;
            //
            // label_location_group
            //
            this.label_location_group.AutoSize = true;
            this.label_location_group.Location = new System.Drawing.Point(340, 67);
            this.label_location_group.Name = "label_location_group";
            this.label_location_group.Size = new System.Drawing.Size(76, 13);
            this.label_location_group.TabIndex = 257;
            this.label_location_group.Text = "LOCATION GROUP";
            //
            // txt_location_group
            //
            this.txt_location_group.Location = new System.Drawing.Point(468, 64);
            this.txt_location_group.Name = "txt_location_group";
            this.txt_location_group.Size = new System.Drawing.Size(180, 20);
            this.txt_location_group.TabIndex = 258;
            //
            // label_sales_period
            //
            this.label_sales_period.AutoSize = true;
            this.label_sales_period.Location = new System.Drawing.Point(668, 67);
            this.label_sales_period.Name = "label_sales_period";
            this.label_sales_period.Size = new System.Drawing.Size(64, 13);
            this.label_sales_period.TabIndex = 259;
            this.label_sales_period.Text = "SALES PERIOD";
            //
            // txt_sales_period
            //
            this.txt_sales_period.Location = new System.Drawing.Point(796, 64);
            this.txt_sales_period.Name = "txt_sales_period";
            this.txt_sales_period.Size = new System.Drawing.Size(180, 20);
            this.txt_sales_period.TabIndex = 260;
            //
            // label_ref_doc_no
            //
            this.label_ref_doc_no.AutoSize = true;
            this.label_ref_doc_no.Location = new System.Drawing.Point(12, 93);
            this.label_ref_doc_no.Name = "label_ref_doc_no";
            this.label_ref_doc_no.Size = new System.Drawing.Size(68, 13);
            this.label_ref_doc_no.TabIndex = 261;
            this.label_ref_doc_no.Text = "REF. DOC. NO.";
            //
            // txt_ref_doc_no
            //
            this.txt_ref_doc_no.Location = new System.Drawing.Point(140, 90);
            this.txt_ref_doc_no.Name = "txt_ref_doc_no";
            this.txt_ref_doc_no.Size = new System.Drawing.Size(180, 20);
            this.txt_ref_doc_no.TabIndex = 262;
            //
            // label_ref_po_no
            //
            this.label_ref_po_no.AutoSize = true;
            this.label_ref_po_no.Location = new System.Drawing.Point(340, 93);
            this.label_ref_po_no.Name = "label_ref_po_no";
            this.label_ref_po_no.Size = new System.Drawing.Size(60, 13);
            this.label_ref_po_no.TabIndex = 263;
            this.label_ref_po_no.Text = "REF. PO NO.";
            //
            // txt_ref_po_no
            //
            this.txt_ref_po_no.Location = new System.Drawing.Point(468, 90);
            this.txt_ref_po_no.Name = "txt_ref_po_no";
            this.txt_ref_po_no.Size = new System.Drawing.Size(180, 20);
            this.txt_ref_po_no.TabIndex = 264;
            //
            // label_unapplied_amount
            //
            // Computed client-side (TRANS. AMOUNT minus every ticked apply line's AMOUNT
            // APPLIED) - Sec14.43 requires this to reach exactly 0 before Save is allowed.
            this.label_unapplied_amount.AutoSize = true;
            this.label_unapplied_amount.Location = new System.Drawing.Point(668, 93);
            this.label_unapplied_amount.Name = "label_unapplied_amount";
            this.label_unapplied_amount.Size = new System.Drawing.Size(121, 13);
            this.label_unapplied_amount.TabIndex = 265;
            this.label_unapplied_amount.Text = "UNAPPLIED AMOUNT";
            //
            // txt_unapplied_amount
            //
            this.txt_unapplied_amount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_unapplied_amount.Location = new System.Drawing.Point(796, 90);
            this.txt_unapplied_amount.Name = "txt_unapplied_amount";
            this.txt_unapplied_amount.ReadOnly = true;
            this.txt_unapplied_amount.Size = new System.Drawing.Size(180, 20);
            this.txt_unapplied_amount.TabIndex = 266;
            this.txt_unapplied_amount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            //
            // pnl_grid_actions
            //
            this.pnl_grid_actions.Controls.Add(this.btn_remove_line);
            this.pnl_grid_actions.Controls.Add(this.btn_add_credit_memo);
            this.pnl_grid_actions.Controls.Add(this.btn_add_invoice);
            this.pnl_grid_actions.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_grid_actions.Location = new System.Drawing.Point(0, 222);
            this.pnl_grid_actions.Name = "pnl_grid_actions";
            this.pnl_grid_actions.Padding = new System.Windows.Forms.Padding(8);
            this.pnl_grid_actions.Size = new System.Drawing.Size(1028, 44);
            this.pnl_grid_actions.TabIndex = 20;
            //
            // btn_remove_line
            //
            this.btn_remove_line.Location = new System.Drawing.Point(340, 8);
            this.btn_remove_line.Name = "btn_remove_line";
            this.btn_remove_line.Size = new System.Drawing.Size(100, 28);
            this.btn_remove_line.TabIndex = 2;
            this.btn_remove_line.Text = "Remove Line";
            this.btn_remove_line.UseVisualStyleBackColor = true;
            //
            // btn_add_credit_memo
            //
            this.btn_add_credit_memo.Location = new System.Drawing.Point(190, 8);
            this.btn_add_credit_memo.Name = "btn_add_credit_memo";
            this.btn_add_credit_memo.Size = new System.Drawing.Size(140, 28);
            this.btn_add_credit_memo.TabIndex = 1;
            this.btn_add_credit_memo.Text = "+ Credit Memo";
            this.btn_add_credit_memo.UseVisualStyleBackColor = true;
            //
            // btn_add_invoice
            //
            this.btn_add_invoice.Location = new System.Drawing.Point(12, 8);
            this.btn_add_invoice.Name = "btn_add_invoice";
            this.btn_add_invoice.Size = new System.Drawing.Size(168, 28);
            this.btn_add_invoice.TabIndex = 0;
            this.btn_add_invoice.Text = "+ Invoice / Bulk Invoice";
            this.btn_add_invoice.UseVisualStyleBackColor = true;
            //
            // dataGridView1
            //
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.aPPLYDataGridViewTextBoxColumn,
            this.dOCNODataGridViewTextBoxColumn,
            this.dUEDATEDataGridViewTextBoxColumn,
            this.tOTALDataGridViewTextBoxColumn,
            this.oPENAMOUNTDataGridViewTextBoxColumn,
            this.aMOUNTAPPLIEDDataGridViewTextBoxColumn,
            this.bALANCEDataGridViewTextBoxColumn});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 266);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1028, 934);
            this.dataGridView1.TabIndex = 19;
            //
            // aPPLYDataGridViewTextBoxColumn
            //
            this.aPPLYDataGridViewTextBoxColumn.DataPropertyName = "APPLY";
            this.aPPLYDataGridViewTextBoxColumn.HeaderText = "APPLY";
            this.aPPLYDataGridViewTextBoxColumn.Name = "aPPLYDataGridViewTextBoxColumn";
            this.aPPLYDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.aPPLYDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.aPPLYDataGridViewTextBoxColumn.Width = 60;
            //
            // dOCNODataGridViewTextBoxColumn
            //
            this.dOCNODataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dOCNODataGridViewTextBoxColumn.DataPropertyName = "DOC NO.";
            this.dOCNODataGridViewTextBoxColumn.HeaderText = "DOC NO.";
            this.dOCNODataGridViewTextBoxColumn.Name = "dOCNODataGridViewTextBoxColumn";
            this.dOCNODataGridViewTextBoxColumn.ReadOnly = true;
            //
            // dUEDATEDataGridViewTextBoxColumn
            //
            this.dUEDATEDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dUEDATEDataGridViewTextBoxColumn.DataPropertyName = "DUE DATE";
            this.dUEDATEDataGridViewTextBoxColumn.HeaderText = "DUE DATE";
            this.dUEDATEDataGridViewTextBoxColumn.Name = "dUEDATEDataGridViewTextBoxColumn";
            this.dUEDATEDataGridViewTextBoxColumn.ReadOnly = true;
            //
            // tOTALDataGridViewTextBoxColumn
            //
            this.tOTALDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.tOTALDataGridViewTextBoxColumn.DataPropertyName = "TOTAL";
            this.tOTALDataGridViewTextBoxColumn.HeaderText = "TOTAL";
            this.tOTALDataGridViewTextBoxColumn.Name = "tOTALDataGridViewTextBoxColumn";
            this.tOTALDataGridViewTextBoxColumn.ReadOnly = true;
            //
            // oPENAMOUNTDataGridViewTextBoxColumn
            //
            this.oPENAMOUNTDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.oPENAMOUNTDataGridViewTextBoxColumn.DataPropertyName = "OPEN AMOUNT";
            this.oPENAMOUNTDataGridViewTextBoxColumn.HeaderText = "OPEN AMOUNT";
            this.oPENAMOUNTDataGridViewTextBoxColumn.Name = "oPENAMOUNTDataGridViewTextBoxColumn";
            this.oPENAMOUNTDataGridViewTextBoxColumn.ReadOnly = true;
            //
            // aMOUNTAPPLIEDDataGridViewTextBoxColumn
            //
            this.aMOUNTAPPLIEDDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.aMOUNTAPPLIEDDataGridViewTextBoxColumn.DataPropertyName = "AMOUNT APPLIED";
            this.aMOUNTAPPLIEDDataGridViewTextBoxColumn.HeaderText = "AMOUNT APPLIED";
            this.aMOUNTAPPLIEDDataGridViewTextBoxColumn.Name = "aMOUNTAPPLIEDDataGridViewTextBoxColumn";
            //
            // bALANCEDataGridViewTextBoxColumn
            //
            this.bALANCEDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.bALANCEDataGridViewTextBoxColumn.DataPropertyName = "BALANCE";
            this.bALANCEDataGridViewTextBoxColumn.HeaderText = "BALANCE";
            this.bALANCEDataGridViewTextBoxColumn.Name = "bALANCEDataGridViewTextBoxColumn";
            this.bALANCEDataGridViewTextBoxColumn.ReadOnly = true;
            //
            // DebitMemo
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.pnl_grid_actions);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panel6);
            this.Name = "DebitMemo";
            this.Size = new System.Drawing.Size(1028, 1200);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.pnl_grid_actions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
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
        private System.Windows.Forms.ToolStripButton btn_save;
        private System.Windows.Forms.ToolStripButton btn_cancel;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txt_supplier_code;
        private System.Windows.Forms.TextBox txt_supplier_id;
        private System.Windows.Forms.Label label_supplier_name;
        private System.Windows.Forms.TextBox txt_supplier_name;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_document_no;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_trans_amount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmb_reason_code;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtp_date;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_currency;
        private System.Windows.Forms.Label label_location_group;
        private System.Windows.Forms.TextBox txt_location_group;
        private System.Windows.Forms.Label label_sales_period;
        private System.Windows.Forms.TextBox txt_sales_period;
        private System.Windows.Forms.Label label_ref_doc_no;
        private System.Windows.Forms.TextBox txt_ref_doc_no;
        private System.Windows.Forms.Label label_ref_po_no;
        private System.Windows.Forms.TextBox txt_ref_po_no;
        private System.Windows.Forms.Label label_unapplied_amount;
        private System.Windows.Forms.TextBox txt_unapplied_amount;
        private System.Windows.Forms.Panel pnl_grid_actions;
        private System.Windows.Forms.Button btn_remove_line;
        private System.Windows.Forms.Button btn_add_credit_memo;
        private System.Windows.Forms.Button btn_add_invoice;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn aPPLYDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dOCNODataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dUEDATEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tOTALDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn oPENAMOUNTDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn aMOUNTAPPLIEDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn bALANCEDataGridViewTextBoxColumn;
    }
}
