
namespace smpc_accounting_app.Pages.Transactions
{
    partial class PaymentReceipt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PaymentReceipt));
            this.pnl_header = new System.Windows.Forms.Panel();
            this.dtp_posting_date = new System.Windows.Forms.DateTimePicker();
            this.txt_posting_datess = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.txt_customer_id = new System.Windows.Forms.TextBox();
            this.cmb_currency = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txt_exchange_rate = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txt_base_rate = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.cmb_journal = new System.Windows.Forms.ComboBox();
            this.cmb_tax = new System.Windows.Forms.ComboBox();
            this.cmb_payment_terms = new System.Windows.Forms.ComboBox();
            this.txt_doc_date = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_reference_dr_no = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_customer_name = new System.Windows.Forms.TextBox();
            this.txt_customer_code = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_doc_no = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btn_new = new System.Windows.Forms.ToolStripButton();
            this.btn_search = new System.Windows.Forms.ToolStripButton();
            this.btn_prev = new System.Windows.Forms.ToolStripButton();
            this.btn_next = new System.Windows.Forms.ToolStripButton();
            this.btn_print = new System.Windows.Forms.ToolStripButton();
            this.btn_save = new System.Windows.Forms.ToolStripButton();
            this.btn_cancel = new System.Windows.Forms.ToolStripButton();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.pnl_check = new System.Windows.Forms.Panel();
            this.pnl_payment_details = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel_header_check = new System.Windows.Forms.Panel();
            this.label20 = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.pnl_header_payment_details = new System.Windows.Forms.Panel();
            this.pnl_header.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.pnl_check.SuspendLayout();
            this.pnl_payment_details.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel_header_check.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // pnl_header
            // 
            this.pnl_header.Controls.Add(this.textBox1);
            this.pnl_header.Controls.Add(this.label19);
            this.pnl_header.Controls.Add(this.label18);
            this.pnl_header.Controls.Add(this.dtp_posting_date);
            this.pnl_header.Controls.Add(this.txt_posting_datess);
            this.pnl_header.Controls.Add(this.label21);
            this.pnl_header.Controls.Add(this.txt_customer_id);
            this.pnl_header.Controls.Add(this.cmb_currency);
            this.pnl_header.Controls.Add(this.label13);
            this.pnl_header.Controls.Add(this.txt_exchange_rate);
            this.pnl_header.Controls.Add(this.label14);
            this.pnl_header.Controls.Add(this.txt_base_rate);
            this.pnl_header.Controls.Add(this.label15);
            this.pnl_header.Controls.Add(this.cmb_journal);
            this.pnl_header.Controls.Add(this.cmb_tax);
            this.pnl_header.Controls.Add(this.cmb_payment_terms);
            this.pnl_header.Controls.Add(this.txt_doc_date);
            this.pnl_header.Controls.Add(this.label12);
            this.pnl_header.Controls.Add(this.label7);
            this.pnl_header.Controls.Add(this.txt_reference_dr_no);
            this.pnl_header.Controls.Add(this.label6);
            this.pnl_header.Controls.Add(this.label16);
            this.pnl_header.Controls.Add(this.label2);
            this.pnl_header.Controls.Add(this.label3);
            this.pnl_header.Controls.Add(this.txt_customer_name);
            this.pnl_header.Controls.Add(this.txt_customer_code);
            this.pnl_header.Controls.Add(this.label4);
            this.pnl_header.Controls.Add(this.txt_doc_no);
            this.pnl_header.Controls.Add(this.label11);
            this.pnl_header.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_header.Enabled = false;
            this.pnl_header.Location = new System.Drawing.Point(0, 72);
            this.pnl_header.Name = "pnl_header";
            this.pnl_header.Size = new System.Drawing.Size(1121, 211);
            this.pnl_header.TabIndex = 84;
            this.pnl_header.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl_header_Paint);
            // 
            // dtp_posting_date
            // 
            this.dtp_posting_date.CustomFormat = "MM/dd/yyyy";
            this.dtp_posting_date.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_posting_date.Location = new System.Drawing.Point(889, 58);
            this.dtp_posting_date.Name = "dtp_posting_date";
            this.dtp_posting_date.Size = new System.Drawing.Size(182, 20);
            this.dtp_posting_date.TabIndex = 305;
            this.dtp_posting_date.Value = new System.DateTime(2025, 7, 4, 14, 46, 2, 0);
            // 
            // txt_posting_datess
            // 
            this.txt_posting_datess.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txt_posting_datess.Location = new System.Drawing.Point(540, 107);
            this.txt_posting_datess.Name = "txt_posting_datess";
            this.txt_posting_datess.Size = new System.Drawing.Size(182, 20);
            this.txt_posting_datess.TabIndex = 304;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(797, 60);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(90, 13);
            this.label21.TabIndex = 303;
            this.label21.Text = "POSTING DATE:";
            // 
            // txt_customer_id
            // 
            this.txt_customer_id.Location = new System.Drawing.Point(370, 18);
            this.txt_customer_id.Name = "txt_customer_id";
            this.txt_customer_id.Size = new System.Drawing.Size(21, 20);
            this.txt_customer_id.TabIndex = 301;
            this.txt_customer_id.Visible = false;
            // 
            // cmb_currency
            // 
            this.cmb_currency.FormattingEnabled = true;
            this.cmb_currency.Items.AddRange(new object[] {
            "USD",
            "PHP"});
            this.cmb_currency.Location = new System.Drawing.Point(540, 36);
            this.cmb_currency.Name = "cmb_currency";
            this.cmb_currency.Size = new System.Drawing.Size(182, 21);
            this.cmb_currency.TabIndex = 300;
            this.cmb_currency.Tag = "";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(433, 61);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(101, 13);
            this.label13.TabIndex = 296;
            this.label13.Text = "EXCHANGE RATE:";
            // 
            // txt_exchange_rate
            // 
            this.txt_exchange_rate.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txt_exchange_rate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_exchange_rate.Location = new System.Drawing.Point(540, 58);
            this.txt_exchange_rate.Name = "txt_exchange_rate";
            this.txt_exchange_rate.Size = new System.Drawing.Size(182, 20);
            this.txt_exchange_rate.TabIndex = 297;
            this.txt_exchange_rate.Text = "57.00";
            this.txt_exchange_rate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(435, 39);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(70, 13);
            this.label14.TabIndex = 295;
            this.label14.Text = "CURRENCY:";
            // 
            // txt_base_rate
            // 
            this.txt_base_rate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_base_rate.Location = new System.Drawing.Point(540, 16);
            this.txt_base_rate.Name = "txt_base_rate";
            this.txt_base_rate.Size = new System.Drawing.Size(182, 20);
            this.txt_base_rate.TabIndex = 294;
            this.txt_base_rate.Text = "1";
            this.txt_base_rate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(435, 19);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(80, 13);
            this.label15.TabIndex = 293;
            this.label15.Text = "BASE RATE:";
            // 
            // cmb_journal
            // 
            this.cmb_journal.FormattingEnabled = true;
            this.cmb_journal.Location = new System.Drawing.Point(132, 81);
            this.cmb_journal.Name = "cmb_journal";
            this.cmb_journal.Size = new System.Drawing.Size(182, 21);
            this.cmb_journal.TabIndex = 292;
            this.cmb_journal.Tag = "DYNAMIC";
            // 
            // cmb_tax
            // 
            this.cmb_tax.FormattingEnabled = true;
            this.cmb_tax.Location = new System.Drawing.Point(132, 101);
            this.cmb_tax.Name = "cmb_tax";
            this.cmb_tax.Size = new System.Drawing.Size(182, 21);
            this.cmb_tax.TabIndex = 291;
            this.cmb_tax.Tag = "DYNAMIC";
            // 
            // cmb_payment_terms
            // 
            this.cmb_payment_terms.FormattingEnabled = true;
            this.cmb_payment_terms.Location = new System.Drawing.Point(132, 60);
            this.cmb_payment_terms.Name = "cmb_payment_terms";
            this.cmb_payment_terms.Size = new System.Drawing.Size(182, 21);
            this.cmb_payment_terms.TabIndex = 290;
            this.cmb_payment_terms.Tag = "DYNAMIC";
            // 
            // txt_doc_date
            // 
            this.txt_doc_date.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txt_doc_date.Location = new System.Drawing.Point(889, 37);
            this.txt_doc_date.Name = "txt_doc_date";
            this.txt_doc_date.Size = new System.Drawing.Size(182, 20);
            this.txt_doc_date.TabIndex = 289;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(20, 106);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(113, 13);
            this.label12.TabIndex = 288;
            this.label12.Text = "REFERENCE OR NO:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(435, 85);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 13);
            this.label7.TabIndex = 279;
            this.label7.Text = "CASH AMOUNT:";
            // 
            // txt_reference_dr_no
            // 
            this.txt_reference_dr_no.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txt_reference_dr_no.BackColor = System.Drawing.Color.White;
            this.txt_reference_dr_no.Location = new System.Drawing.Point(540, 82);
            this.txt_reference_dr_no.Name = "txt_reference_dr_no";
            this.txt_reference_dr_no.ReadOnly = true;
            this.txt_reference_dr_no.Size = new System.Drawing.Size(182, 20);
            this.txt_reference_dr_no.TabIndex = 280;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(797, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 13);
            this.label6.TabIndex = 277;
            this.label6.Text = "DOC DATE:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(20, 61);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(118, 13);
            this.label16.TabIndex = 271;
            this.label16.Text = "REFERENCE CR NO : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 266;
            this.label2.Text = "CUSTOMER:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 267;
            this.label3.Text = "CODE:";
            // 
            // txt_customer_name
            // 
            this.txt_customer_name.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txt_customer_name.BackColor = System.Drawing.Color.White;
            this.txt_customer_name.Location = new System.Drawing.Point(132, 18);
            this.txt_customer_name.Name = "txt_customer_name";
            this.txt_customer_name.ReadOnly = true;
            this.txt_customer_name.Size = new System.Drawing.Size(233, 20);
            this.txt_customer_name.TabIndex = 268;
            // 
            // txt_customer_code
            // 
            this.txt_customer_code.BackColor = System.Drawing.Color.White;
            this.txt_customer_code.Location = new System.Drawing.Point(132, 39);
            this.txt_customer_code.Name = "txt_customer_code";
            this.txt_customer_code.ReadOnly = true;
            this.txt_customer_code.Size = new System.Drawing.Size(182, 20);
            this.txt_customer_code.TabIndex = 269;
            this.txt_customer_code.Text = "sdf";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.CausesValidation = false;
            this.label4.Location = new System.Drawing.Point(20, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 13);
            this.label4.TabIndex = 270;
            this.label4.Text = "DATE COLLECTED: ";
            // 
            // txt_doc_no
            // 
            this.txt_doc_no.BackColor = System.Drawing.Color.White;
            this.txt_doc_no.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_doc_no.Location = new System.Drawing.Point(889, 16);
            this.txt_doc_no.Name = "txt_doc_no";
            this.txt_doc_no.ReadOnly = true;
            this.txt_doc_no.Size = new System.Drawing.Size(182, 20);
            this.txt_doc_no.TabIndex = 68;
            this.txt_doc_no.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(797, 19);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 13);
            this.label11.TabIndex = 66;
            this.label11.Text = "DOC NO:";
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
            this.toolStrip1.Size = new System.Drawing.Size(1121, 25);
            this.toolStrip1.TabIndex = 83;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btn_new
            // 
            this.btn_new.Image = ((System.Drawing.Image)(resources.GetObject("btn_new.Image")));
            this.btn_new.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_new.Name = "btn_new";
            this.btn_new.Size = new System.Drawing.Size(51, 22);
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
            this.btn_save.Image = ((System.Drawing.Image)(resources.GetObject("btn_save.Image")));
            this.btn_save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(51, 22);
            this.btn_save.Text = "Save";
            this.btn_save.Visible = false;
            // 
            // btn_cancel
            // 
            this.btn_cancel.Image = ((System.Drawing.Image)(resources.GetObject("btn_cancel.Image")));
            this.btn_cancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(63, 22);
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.Visible = false;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.label1);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1121, 47);
            this.panel6.TabIndex = 82;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(18, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(211, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "PAYMENT RECEIPT";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(387, 114);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(137, 13);
            this.label18.TabIndex = 306;
            this.label18.Text = "TRANSACTION AMOUNT:";
            this.label18.Click += new System.EventHandler(this.label18_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(387, 137);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(121, 13);
            this.label19.TabIndex = 307;
            this.label19.Text = "UNAPPLIED AMOUNT:";
            // 
            // textBox1
            // 
            this.textBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBox1.Location = new System.Drawing.Point(540, 130);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(182, 20);
            this.textBox1.TabIndex = 308;
            // 
            // pnl_check
            // 
            this.pnl_check.Controls.Add(this.dataGridView2);
            this.pnl_check.Controls.Add(this.panel_header_check);
            this.pnl_check.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_check.Location = new System.Drawing.Point(0, 283);
            this.pnl_check.Name = "pnl_check";
            this.pnl_check.Size = new System.Drawing.Size(1121, 187);
            this.pnl_check.TabIndex = 85;
            // 
            // pnl_payment_details
            // 
            this.pnl_payment_details.Controls.Add(this.pnl_header_payment_details);
            this.pnl_payment_details.Controls.Add(this.dataGridView1);
            this.pnl_payment_details.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_payment_details.Location = new System.Drawing.Point(0, 470);
            this.pnl_payment_details.Name = "pnl_payment_details";
            this.pnl_payment_details.Size = new System.Drawing.Size(1121, 612);
            this.pnl_payment_details.TabIndex = 86;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1121, 612);
            this.dataGridView1.TabIndex = 0;
            // 
            // panel_header_check
            // 
            this.panel_header_check.Controls.Add(this.label20);
            this.panel_header_check.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_header_check.Location = new System.Drawing.Point(0, 0);
            this.panel_header_check.Name = "panel_header_check";
            this.panel_header_check.Size = new System.Drawing.Size(1121, 39);
            this.panel_header_check.TabIndex = 0;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(8, 11);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(62, 16);
            this.label20.TabIndex = 267;
            this.label20.Text = "CHECK:";
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.Location = new System.Drawing.Point(0, 39);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(1121, 148);
            this.dataGridView2.TabIndex = 1;
            // 
            // pnl_header_payment_details
            // 
            this.pnl_header_payment_details.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_header_payment_details.Location = new System.Drawing.Point(0, 0);
            this.pnl_header_payment_details.Name = "pnl_header_payment_details";
            this.pnl_header_payment_details.Size = new System.Drawing.Size(1121, 60);
            this.pnl_header_payment_details.TabIndex = 1;
            // 
            // PaymentReceipt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnl_payment_details);
            this.Controls.Add(this.pnl_check);
            this.Controls.Add(this.pnl_header);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panel6);
            this.Name = "PaymentReceipt";
            this.Size = new System.Drawing.Size(1121, 1082);
            this.pnl_header.ResumeLayout(false);
            this.pnl_header.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.pnl_check.ResumeLayout(false);
            this.pnl_payment_details.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel_header_check.ResumeLayout(false);
            this.panel_header_check.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnl_header;
        private System.Windows.Forms.DateTimePicker dtp_posting_date;
        private System.Windows.Forms.TextBox txt_posting_datess;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txt_customer_id;
        private System.Windows.Forms.ComboBox cmb_currency;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txt_exchange_rate;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txt_base_rate;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cmb_journal;
        private System.Windows.Forms.ComboBox cmb_tax;
        private System.Windows.Forms.ComboBox cmb_payment_terms;
        private System.Windows.Forms.TextBox txt_doc_date;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_reference_dr_no;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_customer_name;
        private System.Windows.Forms.TextBox txt_customer_code;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_doc_no;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btn_new;
        private System.Windows.Forms.ToolStripButton btn_search;
        private System.Windows.Forms.ToolStripButton btn_prev;
        private System.Windows.Forms.ToolStripButton btn_next;
        private System.Windows.Forms.ToolStripButton btn_print;
        private System.Windows.Forms.ToolStripButton btn_save;
        private System.Windows.Forms.ToolStripButton btn_cancel;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Panel pnl_check;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Panel panel_header_check;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Panel pnl_payment_details;
        private System.Windows.Forms.Panel pnl_header_payment_details;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}
