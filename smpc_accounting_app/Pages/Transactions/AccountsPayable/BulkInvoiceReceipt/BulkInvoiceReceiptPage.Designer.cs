
namespace smpc_accounting_app.Pages.Transactions.AccountsPayable.BulkInvoiceReceipt
{
    partial class BulkInvoiceReceiptPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BulkInvoiceReceiptPage));
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
            this.btn_ap_voucher = new System.Windows.Forms.Button();
            this.pnl_main = new System.Windows.Forms.Panel();
            this.dtp_doc_date = new System.Windows.Forms.DateTimePicker();
            this.txt_other_charges = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.cmb_tax_code = new System.Windows.Forms.ComboBox();
            this.txt_prepared_by = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.txt_supplier_id = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txt_id = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.btn_supplier = new System.Windows.Forms.Button();
            this.dtp_invoice_due = new System.Windows.Forms.DateTimePicker();
            this.txt_remarks = new System.Windows.Forms.TextBox();
            this.txt_type = new System.Windows.Forms.TextBox();
            this.txt_ap_voucher = new System.Windows.Forms.TextBox();
            this.txt_twas_amount = new System.Windows.Forms.TextBox();
            this.txt_net_amount = new System.Windows.Forms.TextBox();
            this.txt_invoice_type = new System.Windows.Forms.TextBox();
            this.txt_doc_no = new System.Windows.Forms.TextBox();
            this.txt_currency = new System.Windows.Forms.TextBox();
            this.txt_payment_term = new System.Windows.Forms.TextBox();
            this.txt_reference_doc = new System.Windows.Forms.TextBox();
            this.txt_supplier_code = new System.Windows.Forms.TextBox();
            this.txt_supplier = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.dgv_main = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.payment_charge_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.charge_description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmb_account_code = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.account_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.line_amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel6.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnl_main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_main)).BeginInit();
            this.SuspendLayout();
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.label1);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1400, 47);
            this.panel6.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(18, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(207, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Bulk Invoice Receipt";
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
            this.toolStrip1.TabIndex = 16;
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
            this.panel1.Controls.Add(this.btn_ap_voucher);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 850);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1400, 100);
            this.panel1.TabIndex = 18;
            // 
            // btn_ap_voucher
            // 
            this.btn_ap_voucher.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_ap_voucher.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(218)))), ((int)(((byte)(248)))));
            this.btn_ap_voucher.Location = new System.Drawing.Point(136, 50);
            this.btn_ap_voucher.Name = "btn_ap_voucher";
            this.btn_ap_voucher.Size = new System.Drawing.Size(108, 25);
            this.btn_ap_voucher.TabIndex = 36;
            this.btn_ap_voucher.Text = "AP VOUCHER";
            this.btn_ap_voucher.UseVisualStyleBackColor = false;
            this.btn_ap_voucher.Click += new System.EventHandler(this.btn_ap_voucher_Click);
            // 
            // pnl_main
            // 
            this.pnl_main.Controls.Add(this.dtp_doc_date);
            this.pnl_main.Controls.Add(this.txt_other_charges);
            this.pnl_main.Controls.Add(this.label20);
            this.pnl_main.Controls.Add(this.cmb_tax_code);
            this.pnl_main.Controls.Add(this.txt_prepared_by);
            this.pnl_main.Controls.Add(this.label21);
            this.pnl_main.Controls.Add(this.txt_supplier_id);
            this.pnl_main.Controls.Add(this.label22);
            this.pnl_main.Controls.Add(this.txt_id);
            this.pnl_main.Controls.Add(this.label23);
            this.pnl_main.Controls.Add(this.btn_supplier);
            this.pnl_main.Controls.Add(this.dtp_invoice_due);
            this.pnl_main.Controls.Add(this.txt_remarks);
            this.pnl_main.Controls.Add(this.txt_type);
            this.pnl_main.Controls.Add(this.txt_ap_voucher);
            this.pnl_main.Controls.Add(this.txt_twas_amount);
            this.pnl_main.Controls.Add(this.txt_net_amount);
            this.pnl_main.Controls.Add(this.txt_invoice_type);
            this.pnl_main.Controls.Add(this.txt_doc_no);
            this.pnl_main.Controls.Add(this.txt_currency);
            this.pnl_main.Controls.Add(this.txt_payment_term);
            this.pnl_main.Controls.Add(this.txt_reference_doc);
            this.pnl_main.Controls.Add(this.txt_supplier_code);
            this.pnl_main.Controls.Add(this.txt_supplier);
            this.pnl_main.Controls.Add(this.label24);
            this.pnl_main.Controls.Add(this.label25);
            this.pnl_main.Controls.Add(this.label26);
            this.pnl_main.Controls.Add(this.label27);
            this.pnl_main.Controls.Add(this.label28);
            this.pnl_main.Controls.Add(this.label29);
            this.pnl_main.Controls.Add(this.label30);
            this.pnl_main.Controls.Add(this.label31);
            this.pnl_main.Controls.Add(this.label32);
            this.pnl_main.Controls.Add(this.label33);
            this.pnl_main.Controls.Add(this.label34);
            this.pnl_main.Controls.Add(this.label35);
            this.pnl_main.Controls.Add(this.label36);
            this.pnl_main.Controls.Add(this.label37);
            this.pnl_main.Controls.Add(this.label38);
            this.pnl_main.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_main.Location = new System.Drawing.Point(0, 72);
            this.pnl_main.Name = "pnl_main";
            this.pnl_main.Size = new System.Drawing.Size(1400, 242);
            this.pnl_main.TabIndex = 76;
            // 
            // dtp_doc_date
            // 
            this.dtp_doc_date.Enabled = false;
            this.dtp_doc_date.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_doc_date.Location = new System.Drawing.Point(995, 39);
            this.dtp_doc_date.Name = "dtp_doc_date";
            this.dtp_doc_date.Size = new System.Drawing.Size(289, 20);
            this.dtp_doc_date.TabIndex = 305;
            this.dtp_doc_date.Tag = "REQUIRED";
            // 
            // txt_other_charges
            // 
            this.txt_other_charges.Enabled = false;
            this.txt_other_charges.Location = new System.Drawing.Point(995, 81);
            this.txt_other_charges.Name = "txt_other_charges";
            this.txt_other_charges.Size = new System.Drawing.Size(289, 20);
            this.txt_other_charges.TabIndex = 304;
            this.txt_other_charges.Tag = "MONEY";
            this.txt_other_charges.TextChanged += new System.EventHandler(this.txt_other_charges_TextChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(883, 84);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(106, 13);
            this.label20.TabIndex = 303;
            this.label20.Text = "OTHER CHARGES :";
            // 
            // cmb_tax_code
            // 
            this.cmb_tax_code.BackColor = System.Drawing.Color.White;
            this.cmb_tax_code.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_tax_code.Enabled = false;
            this.cmb_tax_code.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmb_tax_code.FormattingEnabled = true;
            this.cmb_tax_code.Location = new System.Drawing.Point(136, 119);
            this.cmb_tax_code.MaxLength = 50;
            this.cmb_tax_code.MinimumSize = new System.Drawing.Size(200, 0);
            this.cmb_tax_code.Name = "cmb_tax_code";
            this.cmb_tax_code.Size = new System.Drawing.Size(289, 21);
            this.cmb_tax_code.TabIndex = 302;
            this.cmb_tax_code.TabStop = false;
            this.cmb_tax_code.Tag = "REQUIRED";
            this.cmb_tax_code.SelectedIndexChanged += new System.EventHandler(this.cmb_tax_code_SelectedIndexChanged);
            // 
            // txt_prepared_by
            // 
            this.txt_prepared_by.Location = new System.Drawing.Point(595, 140);
            this.txt_prepared_by.Name = "txt_prepared_by";
            this.txt_prepared_by.Size = new System.Drawing.Size(289, 20);
            this.txt_prepared_by.TabIndex = 301;
            this.txt_prepared_by.Tag = "";
            this.txt_prepared_by.Visible = false;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(493, 147);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(86, 13);
            this.label21.TabIndex = 300;
            this.label21.Text = "PREPARED BY:";
            this.label21.Visible = false;
            // 
            // txt_supplier_id
            // 
            this.txt_supplier_id.Enabled = false;
            this.txt_supplier_id.Location = new System.Drawing.Point(595, 105);
            this.txt_supplier_id.Name = "txt_supplier_id";
            this.txt_supplier_id.Size = new System.Drawing.Size(289, 20);
            this.txt_supplier_id.TabIndex = 299;
            this.txt_supplier_id.Tag = "";
            this.txt_supplier_id.Visible = false;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(499, 108);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(80, 13);
            this.label22.TabIndex = 298;
            this.label22.Text = "SUPPLIER ID :";
            this.label22.Visible = false;
            // 
            // txt_id
            // 
            this.txt_id.Enabled = false;
            this.txt_id.Location = new System.Drawing.Point(595, 173);
            this.txt_id.Name = "txt_id";
            this.txt_id.Size = new System.Drawing.Size(289, 20);
            this.txt_id.TabIndex = 295;
            this.txt_id.Tag = "";
            this.txt_id.Visible = false;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(555, 176);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(24, 13);
            this.label23.TabIndex = 294;
            this.label23.Text = "ID :";
            this.label23.Visible = false;
            // 
            // btn_supplier
            // 
            this.btn_supplier.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_supplier.BackgroundImage")));
            this.btn_supplier.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_supplier.Enabled = false;
            this.btn_supplier.Location = new System.Drawing.Point(431, 14);
            this.btn_supplier.Name = "btn_supplier";
            this.btn_supplier.Size = new System.Drawing.Size(30, 23);
            this.btn_supplier.TabIndex = 293;
            this.btn_supplier.UseVisualStyleBackColor = true;
            this.btn_supplier.Click += new System.EventHandler(this.btn_supplier_Click);
            // 
            // dtp_invoice_due
            // 
            this.dtp_invoice_due.Enabled = false;
            this.dtp_invoice_due.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_invoice_due.Location = new System.Drawing.Point(136, 141);
            this.dtp_invoice_due.Name = "dtp_invoice_due";
            this.dtp_invoice_due.Size = new System.Drawing.Size(289, 20);
            this.dtp_invoice_due.TabIndex = 290;
            this.dtp_invoice_due.Tag = "REQUIRED";
            // 
            // txt_remarks
            // 
            this.txt_remarks.Enabled = false;
            this.txt_remarks.Location = new System.Drawing.Point(136, 206);
            this.txt_remarks.Name = "txt_remarks";
            this.txt_remarks.Size = new System.Drawing.Size(1069, 20);
            this.txt_remarks.TabIndex = 289;
            this.txt_remarks.Tag = "";
            // 
            // txt_type
            // 
            this.txt_type.Enabled = false;
            this.txt_type.Location = new System.Drawing.Point(995, 165);
            this.txt_type.Name = "txt_type";
            this.txt_type.Size = new System.Drawing.Size(289, 20);
            this.txt_type.TabIndex = 288;
            this.txt_type.Tag = "REQUIRED";
            // 
            // txt_ap_voucher
            // 
            this.txt_ap_voucher.Enabled = false;
            this.txt_ap_voucher.Location = new System.Drawing.Point(995, 144);
            this.txt_ap_voucher.Name = "txt_ap_voucher";
            this.txt_ap_voucher.Size = new System.Drawing.Size(289, 20);
            this.txt_ap_voucher.TabIndex = 287;
            this.txt_ap_voucher.Tag = "";
            // 
            // txt_twas_amount
            // 
            this.txt_twas_amount.Enabled = false;
            this.txt_twas_amount.Location = new System.Drawing.Point(995, 123);
            this.txt_twas_amount.Name = "txt_twas_amount";
            this.txt_twas_amount.Size = new System.Drawing.Size(289, 20);
            this.txt_twas_amount.TabIndex = 286;
            this.txt_twas_amount.Tag = "MONEY REQUIRED";
            // 
            // txt_net_amount
            // 
            this.txt_net_amount.Enabled = false;
            this.txt_net_amount.Location = new System.Drawing.Point(995, 102);
            this.txt_net_amount.Name = "txt_net_amount";
            this.txt_net_amount.Size = new System.Drawing.Size(289, 20);
            this.txt_net_amount.TabIndex = 285;
            this.txt_net_amount.Tag = "MONEY REQUIRED";
            // 
            // txt_invoice_type
            // 
            this.txt_invoice_type.Enabled = false;
            this.txt_invoice_type.Location = new System.Drawing.Point(995, 60);
            this.txt_invoice_type.Name = "txt_invoice_type";
            this.txt_invoice_type.Size = new System.Drawing.Size(289, 20);
            this.txt_invoice_type.TabIndex = 284;
            this.txt_invoice_type.Tag = "REQUIRED";
            // 
            // txt_doc_no
            // 
            this.txt_doc_no.Enabled = false;
            this.txt_doc_no.Location = new System.Drawing.Point(995, 18);
            this.txt_doc_no.Name = "txt_doc_no";
            this.txt_doc_no.Size = new System.Drawing.Size(289, 20);
            this.txt_doc_no.TabIndex = 282;
            this.txt_doc_no.Tag = "";
            // 
            // txt_currency
            // 
            this.txt_currency.Enabled = false;
            this.txt_currency.Location = new System.Drawing.Point(136, 98);
            this.txt_currency.Name = "txt_currency";
            this.txt_currency.Size = new System.Drawing.Size(289, 20);
            this.txt_currency.TabIndex = 279;
            this.txt_currency.Tag = "REQUIRED";
            // 
            // txt_payment_term
            // 
            this.txt_payment_term.Enabled = false;
            this.txt_payment_term.Location = new System.Drawing.Point(136, 77);
            this.txt_payment_term.Name = "txt_payment_term";
            this.txt_payment_term.Size = new System.Drawing.Size(289, 20);
            this.txt_payment_term.TabIndex = 278;
            this.txt_payment_term.Tag = "REQUIRED";
            // 
            // txt_reference_doc
            // 
            this.txt_reference_doc.Enabled = false;
            this.txt_reference_doc.Location = new System.Drawing.Point(136, 56);
            this.txt_reference_doc.Name = "txt_reference_doc";
            this.txt_reference_doc.Size = new System.Drawing.Size(289, 20);
            this.txt_reference_doc.TabIndex = 277;
            this.txt_reference_doc.Tag = "REQUIRED";
            // 
            // txt_supplier_code
            // 
            this.txt_supplier_code.Enabled = false;
            this.txt_supplier_code.Location = new System.Drawing.Point(136, 35);
            this.txt_supplier_code.Name = "txt_supplier_code";
            this.txt_supplier_code.Size = new System.Drawing.Size(289, 20);
            this.txt_supplier_code.TabIndex = 276;
            this.txt_supplier_code.Tag = "REQUIRED";
            // 
            // txt_supplier
            // 
            this.txt_supplier.Enabled = false;
            this.txt_supplier.Location = new System.Drawing.Point(136, 14);
            this.txt_supplier.Name = "txt_supplier";
            this.txt_supplier.ReadOnly = true;
            this.txt_supplier.Size = new System.Drawing.Size(289, 20);
            this.txt_supplier.TabIndex = 275;
            this.txt_supplier.Tag = "REQUIRED";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(949, 168);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(41, 13);
            this.label24.TabIndex = 274;
            this.label24.Tag = "";
            this.label24.Text = "TYPE :";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(900, 147);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(90, 13);
            this.label25.TabIndex = 272;
            this.label25.Text = "APV VOUCHER :";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(895, 126);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(95, 13);
            this.label26.TabIndex = 270;
            this.label26.Text = "TWAS AMOUNT :";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(905, 105);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(85, 13);
            this.label27.TabIndex = 268;
            this.label27.Text = "NET AMOUNT :";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(903, 63);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(87, 13);
            this.label28.TabIndex = 266;
            this.label28.Text = "INVOICE TYPE :";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(935, 21);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(55, 13);
            this.label29.TabIndex = 264;
            this.label29.Text = "DOC NO :";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(64, 209);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(66, 13);
            this.label30.TabIndex = 262;
            this.label30.Text = "REMARKS :";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(48, 144);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(82, 13);
            this.label31.TabIndex = 260;
            this.label31.Text = "INVOICE DUE :";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(63, 123);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(67, 13);
            this.label32.TabIndex = 258;
            this.label32.Text = "TAX CODE :";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(60, 101);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(70, 13);
            this.label33.TabIndex = 256;
            this.label33.Text = "CURRENCY:";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(31, 80);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(99, 13);
            this.label34.TabIndex = 254;
            this.label34.Text = "PAYMENT TERM :";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(922, 42);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(68, 13);
            this.label35.TabIndex = 245;
            this.label35.Text = "DOC DATE :";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(26, 59);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(104, 13);
            this.label36.TabIndex = 240;
            this.label36.Text = "REFERENCE DOC :";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(87, 38);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(43, 13);
            this.label37.TabIndex = 238;
            this.label37.Text = "CODE :";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(64, 17);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(66, 13);
            this.label38.TabIndex = 235;
            this.label38.Text = "SUPPLIER :";
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
            this.payment_charge_code,
            this.charge_description,
            this.cmb_account_code,
            this.account_code,
            this.line_amount});
            this.dgv_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_main.EnableHeadersVisualStyles = false;
            this.dgv_main.Location = new System.Drawing.Point(0, 314);
            this.dgv_main.Name = "dgv_main";
            this.dgv_main.Size = new System.Drawing.Size(1400, 536);
            this.dgv_main.TabIndex = 77;
            this.dgv_main.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_main_CellValueChanged);
            this.dgv_main.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgv_main_CurrentCellDirtyStateChanged);
            this.dgv_main.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgv_main_EditingControlShowing);
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "ID";
            this.id.Name = "id";
            this.id.Visible = false;
            // 
            // payment_charge_code
            // 
            this.payment_charge_code.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.payment_charge_code.DataPropertyName = "payment_charge_code";
            this.payment_charge_code.HeaderText = "PAYMENT CHARGE CODE";
            this.payment_charge_code.MinimumWidth = 200;
            this.payment_charge_code.Name = "payment_charge_code";
            this.payment_charge_code.ReadOnly = true;
            // 
            // charge_description
            // 
            this.charge_description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.charge_description.DataPropertyName = "charge_description";
            this.charge_description.HeaderText = "CHARGE DESCRIPTION";
            this.charge_description.Name = "charge_description";
            this.charge_description.ReadOnly = true;
            // 
            // cmb_account_code
            // 
            this.cmb_account_code.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cmb_account_code.HeaderText = "ACCOUNT CODE";
            this.cmb_account_code.Name = "cmb_account_code";
            this.cmb_account_code.ReadOnly = true;
            this.cmb_account_code.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // account_code
            // 
            this.account_code.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.account_code.DataPropertyName = "account_code";
            this.account_code.HeaderText = "ACCOUNT CODE";
            this.account_code.Name = "account_code";
            this.account_code.ReadOnly = true;
            this.account_code.Visible = false;
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
            // BulkInvoiceReceiptPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgv_main);
            this.Controls.Add(this.pnl_main);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panel6);
            this.Name = "BulkInvoiceReceiptPage";
            this.Size = new System.Drawing.Size(1400, 950);
            this.Load += new System.EventHandler(this.BulkInvoiceReceiptPage_Load);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.pnl_main.ResumeLayout(false);
            this.pnl_main.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_main)).EndInit();
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
        private System.Windows.Forms.Button btn_ap_voucher;
        private System.Windows.Forms.Panel pnl_main;
        private System.Windows.Forms.TextBox txt_other_charges;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ComboBox cmb_tax_code;
        private System.Windows.Forms.TextBox txt_prepared_by;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txt_supplier_id;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txt_id;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Button btn_supplier;
        private System.Windows.Forms.DateTimePicker dtp_invoice_due;
        private System.Windows.Forms.TextBox txt_remarks;
        private System.Windows.Forms.TextBox txt_type;
        private System.Windows.Forms.TextBox txt_ap_voucher;
        private System.Windows.Forms.TextBox txt_twas_amount;
        private System.Windows.Forms.TextBox txt_net_amount;
        private System.Windows.Forms.TextBox txt_invoice_type;
        private System.Windows.Forms.TextBox txt_doc_no;
        private System.Windows.Forms.TextBox txt_currency;
        private System.Windows.Forms.TextBox txt_payment_term;
        private System.Windows.Forms.TextBox txt_reference_doc;
        private System.Windows.Forms.TextBox txt_supplier_code;
        private System.Windows.Forms.TextBox txt_supplier;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.DataGridView dgv_main;
        private System.Windows.Forms.DateTimePicker dtp_doc_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn payment_charge_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn charge_description;
        private System.Windows.Forms.DataGridViewComboBoxColumn cmb_account_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn account_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn line_amount;
    }
}
