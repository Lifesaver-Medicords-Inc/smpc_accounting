
namespace smpc_accounting_app.Pages.Transactions.AccountsPayable.APVoucher
{
    partial class APVoucherPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(APVoucherPage));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btn_new = new System.Windows.Forms.ToolStripButton();
            this.btn_search = new System.Windows.Forms.ToolStripButton();
            this.btn_print = new System.Windows.Forms.ToolStripButton();
            this.btn_save = new System.Windows.Forms.ToolStripButton();
            this.btn_cancel = new System.Windows.Forms.ToolStripButton();
            this.btn_next = new System.Windows.Forms.ToolStripButton();
            this.btn_prev = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_payment_voucher = new System.Windows.Forms.Button();
            this.dgv_main = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.invoice_receipt_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.receipt_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ir_doc_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.line_amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.receipt_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnl_main = new System.Windows.Forms.Panel();
            this.txt_transaction_amount = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_prepared_by = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txt_supplier_id = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txt_id = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.btn_supplier = new System.Windows.Forms.Button();
            this.dtp_doc_date = new System.Windows.Forms.DateTimePicker();
            this.txt_doc_no = new System.Windows.Forms.TextBox();
            this.txt_currency = new System.Windows.Forms.TextBox();
            this.txt_supplier_code = new System.Windows.Forms.TextBox();
            this.txt_supplier = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.panel6.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_main)).BeginInit();
            this.pnl_main.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.label1);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1400, 47);
            this.panel6.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(18, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "AP Voucher";
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_new,
            this.btn_search,
            this.btn_print,
            this.btn_save,
            this.btn_cancel,
            this.btn_next,
            this.btn_prev});
            this.toolStrip1.Location = new System.Drawing.Point(0, 47);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(1400, 25);
            this.toolStrip1.TabIndex = 15;
            this.toolStrip1.Text = "toolStrip2";
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
            // btn_search
            // 
            this.btn_search.Image = ((System.Drawing.Image)(resources.GetObject("btn_search.Image")));
            this.btn_search.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(62, 22);
            this.btn_search.Text = "Search";
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
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
            // btn_next
            // 
            this.btn_next.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btn_next.Image = ((System.Drawing.Image)(resources.GetObject("btn_next.Image")));
            this.btn_next.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_next.Name = "btn_next";
            this.btn_next.Size = new System.Drawing.Size(52, 22);
            this.btn_next.Text = "Next";
            this.btn_next.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btn_next.Click += new System.EventHandler(this.btn_next_Click);
            // 
            // btn_prev
            // 
            this.btn_prev.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btn_prev.Image = ((System.Drawing.Image)(resources.GetObject("btn_prev.Image")));
            this.btn_prev.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_prev.Name = "btn_prev";
            this.btn_prev.Size = new System.Drawing.Size(72, 22);
            this.btn_prev.Text = "Previous";
            this.btn_prev.Click += new System.EventHandler(this.btn_prev_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_payment_voucher);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 850);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1400, 100);
            this.panel1.TabIndex = 19;
            // 
            // btn_payment_voucher
            // 
            this.btn_payment_voucher.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_payment_voucher.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(218)))), ((int)(((byte)(248)))));
            this.btn_payment_voucher.Location = new System.Drawing.Point(136, 50);
            this.btn_payment_voucher.Name = "btn_payment_voucher";
            this.btn_payment_voucher.Size = new System.Drawing.Size(151, 25);
            this.btn_payment_voucher.TabIndex = 36;
            this.btn_payment_voucher.Text = "PAYMENT VOUCHER";
            this.btn_payment_voucher.UseVisualStyleBackColor = false;
            // 
            // dgv_main
            // 
            this.dgv_main.AllowUserToAddRows = false;
            this.dgv_main.AllowUserToResizeColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_main.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_main.ColumnHeadersHeight = 50;
            this.dgv_main.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv_main.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.invoice_receipt_id,
            this.receipt_no,
            this.ir_doc_date,
            this.line_amount,
            this.receipt_type});
            this.dgv_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_main.EnableHeadersVisualStyles = false;
            this.dgv_main.Location = new System.Drawing.Point(0, 222);
            this.dgv_main.Name = "dgv_main";
            this.dgv_main.Size = new System.Drawing.Size(1400, 628);
            this.dgv_main.TabIndex = 78;
            this.dgv_main.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_main_CellClick);
            this.dgv_main.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_main_CellValueChanged);
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "ID";
            this.id.Name = "id";
            this.id.Visible = false;
            // 
            // invoice_receipt_id
            // 
            this.invoice_receipt_id.DataPropertyName = "invoice_receipt_id";
            this.invoice_receipt_id.HeaderText = "INVOICE ID";
            this.invoice_receipt_id.Name = "invoice_receipt_id";
            this.invoice_receipt_id.ReadOnly = true;
            this.invoice_receipt_id.Visible = false;
            // 
            // receipt_no
            // 
            this.receipt_no.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.receipt_no.DataPropertyName = "receipt_no";
            this.receipt_no.HeaderText = "RECEIPT NO.";
            this.receipt_no.MinimumWidth = 200;
            this.receipt_no.Name = "receipt_no";
            this.receipt_no.ReadOnly = true;
            // 
            // ir_doc_date
            // 
            this.ir_doc_date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ir_doc_date.DataPropertyName = "ir_doc_date";
            this.ir_doc_date.HeaderText = "DOC DATE";
            this.ir_doc_date.Name = "ir_doc_date";
            this.ir_doc_date.ReadOnly = true;
            // 
            // line_amount
            // 
            this.line_amount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.line_amount.DataPropertyName = "line_amount";
            this.line_amount.HeaderText = "AMOUNT";
            this.line_amount.MinimumWidth = 180;
            this.line_amount.Name = "line_amount";
            this.line_amount.ReadOnly = true;
            // 
            // receipt_type
            // 
            this.receipt_type.DataPropertyName = "receipt_type";
            this.receipt_type.HeaderText = "RECEIPT TYPE";
            this.receipt_type.Name = "receipt_type";
            this.receipt_type.ReadOnly = true;
            this.receipt_type.Visible = false;
            // 
            // pnl_main
            // 
            this.pnl_main.Controls.Add(this.txt_transaction_amount);
            this.pnl_main.Controls.Add(this.label3);
            this.pnl_main.Controls.Add(this.txt_prepared_by);
            this.pnl_main.Controls.Add(this.label19);
            this.pnl_main.Controls.Add(this.txt_supplier_id);
            this.pnl_main.Controls.Add(this.label18);
            this.pnl_main.Controls.Add(this.txt_id);
            this.pnl_main.Controls.Add(this.label17);
            this.pnl_main.Controls.Add(this.btn_supplier);
            this.pnl_main.Controls.Add(this.dtp_doc_date);
            this.pnl_main.Controls.Add(this.txt_doc_no);
            this.pnl_main.Controls.Add(this.txt_currency);
            this.pnl_main.Controls.Add(this.txt_supplier_code);
            this.pnl_main.Controls.Add(this.txt_supplier);
            this.pnl_main.Controls.Add(this.label13);
            this.pnl_main.Controls.Add(this.label5);
            this.pnl_main.Controls.Add(this.label9);
            this.pnl_main.Controls.Add(this.label2);
            this.pnl_main.Controls.Add(this.label11);
            this.pnl_main.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_main.Location = new System.Drawing.Point(0, 72);
            this.pnl_main.Name = "pnl_main";
            this.pnl_main.Size = new System.Drawing.Size(1400, 150);
            this.pnl_main.TabIndex = 16;
            // 
            // txt_transaction_amount
            // 
            this.txt_transaction_amount.Enabled = false;
            this.txt_transaction_amount.Location = new System.Drawing.Point(162, 76);
            this.txt_transaction_amount.Name = "txt_transaction_amount";
            this.txt_transaction_amount.Size = new System.Drawing.Size(289, 20);
            this.txt_transaction_amount.TabIndex = 320;
            this.txt_transaction_amount.Tag = "REQUIRED";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(137, 13);
            this.label3.TabIndex = 319;
            this.label3.Text = "TRANSACTION AMOUNT:";
            // 
            // txt_prepared_by
            // 
            this.txt_prepared_by.Location = new System.Drawing.Point(570, 76);
            this.txt_prepared_by.Name = "txt_prepared_by";
            this.txt_prepared_by.Size = new System.Drawing.Size(289, 20);
            this.txt_prepared_by.TabIndex = 318;
            this.txt_prepared_by.Tag = "";
            this.txt_prepared_by.Visible = false;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(468, 83);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(86, 13);
            this.label19.TabIndex = 317;
            this.label19.Text = "PREPARED BY:";
            this.label19.Visible = false;
            // 
            // txt_supplier_id
            // 
            this.txt_supplier_id.Enabled = false;
            this.txt_supplier_id.Location = new System.Drawing.Point(570, 41);
            this.txt_supplier_id.Name = "txt_supplier_id";
            this.txt_supplier_id.Size = new System.Drawing.Size(289, 20);
            this.txt_supplier_id.TabIndex = 316;
            this.txt_supplier_id.Tag = "";
            this.txt_supplier_id.Visible = false;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(474, 44);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(80, 13);
            this.label18.TabIndex = 315;
            this.label18.Text = "SUPPLIER ID :";
            this.label18.Visible = false;
            // 
            // txt_id
            // 
            this.txt_id.Enabled = false;
            this.txt_id.Location = new System.Drawing.Point(570, 109);
            this.txt_id.Name = "txt_id";
            this.txt_id.Size = new System.Drawing.Size(289, 20);
            this.txt_id.TabIndex = 314;
            this.txt_id.Tag = "";
            this.txt_id.Visible = false;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(530, 112);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(24, 13);
            this.label17.TabIndex = 313;
            this.label17.Text = "ID :";
            this.label17.Visible = false;
            // 
            // btn_supplier
            // 
            this.btn_supplier.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_supplier.BackgroundImage")));
            this.btn_supplier.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_supplier.Enabled = false;
            this.btn_supplier.Location = new System.Drawing.Point(458, 12);
            this.btn_supplier.Name = "btn_supplier";
            this.btn_supplier.Size = new System.Drawing.Size(30, 23);
            this.btn_supplier.TabIndex = 312;
            this.btn_supplier.UseVisualStyleBackColor = true;
            this.btn_supplier.Click += new System.EventHandler(this.btn_supplier_Click);
            // 
            // dtp_doc_date
            // 
            this.dtp_doc_date.Enabled = false;
            this.dtp_doc_date.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_doc_date.Location = new System.Drawing.Point(953, 38);
            this.dtp_doc_date.Name = "dtp_doc_date";
            this.dtp_doc_date.Size = new System.Drawing.Size(289, 20);
            this.dtp_doc_date.TabIndex = 311;
            this.dtp_doc_date.Tag = "REQUIRED";
            // 
            // txt_doc_no
            // 
            this.txt_doc_no.Enabled = false;
            this.txt_doc_no.Location = new System.Drawing.Point(953, 17);
            this.txt_doc_no.Name = "txt_doc_no";
            this.txt_doc_no.Size = new System.Drawing.Size(289, 20);
            this.txt_doc_no.TabIndex = 310;
            this.txt_doc_no.Tag = "";
            // 
            // txt_currency
            // 
            this.txt_currency.Enabled = false;
            this.txt_currency.Location = new System.Drawing.Point(162, 55);
            this.txt_currency.Name = "txt_currency";
            this.txt_currency.Size = new System.Drawing.Size(289, 20);
            this.txt_currency.TabIndex = 309;
            this.txt_currency.Tag = "REQUIRED";
            // 
            // txt_supplier_code
            // 
            this.txt_supplier_code.Enabled = false;
            this.txt_supplier_code.Location = new System.Drawing.Point(162, 34);
            this.txt_supplier_code.Name = "txt_supplier_code";
            this.txt_supplier_code.Size = new System.Drawing.Size(289, 20);
            this.txt_supplier_code.TabIndex = 308;
            this.txt_supplier_code.Tag = "REQUIRED";
            // 
            // txt_supplier
            // 
            this.txt_supplier.Enabled = false;
            this.txt_supplier.Location = new System.Drawing.Point(162, 13);
            this.txt_supplier.Name = "txt_supplier";
            this.txt_supplier.ReadOnly = true;
            this.txt_supplier.Size = new System.Drawing.Size(289, 20);
            this.txt_supplier.TabIndex = 307;
            this.txt_supplier.Tag = "REQUIRED";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(893, 20);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(55, 13);
            this.label13.TabIndex = 306;
            this.label13.Text = "DOC NO :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(85, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 305;
            this.label5.Text = "CURRENCY:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(880, 41);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(68, 13);
            this.label9.TabIndex = 304;
            this.label9.Text = "DOC DATE :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(111, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 303;
            this.label2.Text = "CODE :";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(88, 16);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(66, 13);
            this.label11.TabIndex = 302;
            this.label11.Text = "SUPPLIER :";
            // 
            // APVoucherPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgv_main);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnl_main);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panel6);
            this.Name = "APVoucherPage";
            this.Size = new System.Drawing.Size(1400, 950);
            this.Load += new System.EventHandler(this.APVoucherPage_Load);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_main)).EndInit();
            this.pnl_main.ResumeLayout(false);
            this.pnl_main.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btn_new;
        private System.Windows.Forms.ToolStripButton btn_search;
        private System.Windows.Forms.ToolStripButton btn_print;
        private System.Windows.Forms.ToolStripButton btn_save;
        private System.Windows.Forms.ToolStripButton btn_cancel;
        private System.Windows.Forms.ToolStripButton btn_next;
        private System.Windows.Forms.ToolStripButton btn_prev;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_payment_voucher;
        private System.Windows.Forms.DataGridView dgv_main;
        private System.Windows.Forms.Panel pnl_main;
        private System.Windows.Forms.TextBox txt_transaction_amount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_prepared_by;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txt_supplier_id;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txt_id;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button btn_supplier;
        private System.Windows.Forms.DateTimePicker dtp_doc_date;
        private System.Windows.Forms.TextBox txt_doc_no;
        private System.Windows.Forms.TextBox txt_currency;
        private System.Windows.Forms.TextBox txt_supplier_code;
        private System.Windows.Forms.TextBox txt_supplier;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn invoice_receipt_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn receipt_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn ir_doc_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn line_amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn receipt_type;
    }
}
