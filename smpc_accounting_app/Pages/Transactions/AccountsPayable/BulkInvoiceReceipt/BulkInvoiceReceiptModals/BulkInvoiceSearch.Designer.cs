
namespace smpc_accounting_app.Pages.Transactions.AccountsPayable.BulkInvoiceReceipt.BulkInvoiceReceiptModals
{
    partial class BulkInvoiceSearch
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgv_ir_search = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.supplier = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.supplier_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tax_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.invoice_due = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.doc_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.doc_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.net_amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ir_search)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_ir_search
            // 
            this.dgv_ir_search.AllowUserToAddRows = false;
            this.dgv_ir_search.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_ir_search.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_ir_search.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_ir_search.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.supplier,
            this.supplier_code,
            this.tax_code,
            this.invoice_due,
            this.doc_no,
            this.doc_date,
            this.net_amount});
            this.dgv_ir_search.Location = new System.Drawing.Point(-1, 31);
            this.dgv_ir_search.Name = "dgv_ir_search";
            this.dgv_ir_search.Size = new System.Drawing.Size(802, 389);
            this.dgv_ir_search.TabIndex = 8;
            this.dgv_ir_search.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_ir_search_CellClick);
            // 
            // id
            // 
            this.id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "ID";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            this.id.Width = 80;
            // 
            // supplier
            // 
            this.supplier.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.supplier.DataPropertyName = "supplier";
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Gainsboro;
            this.supplier.DefaultCellStyle = dataGridViewCellStyle2;
            this.supplier.HeaderText = "SUPPLIER";
            this.supplier.Name = "supplier";
            this.supplier.ReadOnly = true;
            // 
            // supplier_code
            // 
            this.supplier_code.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.supplier_code.DataPropertyName = "supplier_code";
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Gainsboro;
            this.supplier_code.DefaultCellStyle = dataGridViewCellStyle3;
            this.supplier_code.HeaderText = "SUPPLIER CODE";
            this.supplier_code.Name = "supplier_code";
            this.supplier_code.ReadOnly = true;
            // 
            // tax_code
            // 
            this.tax_code.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.tax_code.DataPropertyName = "tax_code";
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Gainsboro;
            this.tax_code.DefaultCellStyle = dataGridViewCellStyle4;
            this.tax_code.HeaderText = "TAX CODE";
            this.tax_code.Name = "tax_code";
            this.tax_code.ReadOnly = true;
            // 
            // invoice_due
            // 
            this.invoice_due.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.invoice_due.DataPropertyName = "invoice_due";
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Gainsboro;
            this.invoice_due.DefaultCellStyle = dataGridViewCellStyle5;
            this.invoice_due.HeaderText = "INVOICE DUE";
            this.invoice_due.MinimumWidth = 150;
            this.invoice_due.Name = "invoice_due";
            this.invoice_due.ReadOnly = true;
            // 
            // doc_no
            // 
            this.doc_no.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.doc_no.DataPropertyName = "doc_no";
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.Gainsboro;
            this.doc_no.DefaultCellStyle = dataGridViewCellStyle6;
            this.doc_no.HeaderText = "DOC NO";
            this.doc_no.Name = "doc_no";
            this.doc_no.ReadOnly = true;
            // 
            // doc_date
            // 
            this.doc_date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.doc_date.DataPropertyName = "doc_date";
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.Gainsboro;
            this.doc_date.DefaultCellStyle = dataGridViewCellStyle7;
            this.doc_date.HeaderText = "DOC DATE";
            this.doc_date.Name = "doc_date";
            this.doc_date.ReadOnly = true;
            // 
            // net_amount
            // 
            this.net_amount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.net_amount.DataPropertyName = "net_amount";
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.Gainsboro;
            this.net_amount.DefaultCellStyle = dataGridViewCellStyle8;
            this.net_amount.HeaderText = "NET AMOUNT";
            this.net_amount.Name = "net_amount";
            this.net_amount.ReadOnly = true;
            // 
            // BulkInvoiceSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgv_ir_search);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "BulkInvoiceSearch";
            this.Text = "BulkInvoiceSearch";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BulkInvoiceSearch_FormClosing);
            this.Load += new System.EventHandler(this.BulkInvoiceSearch_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ir_search)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_ir_search;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn supplier;
        private System.Windows.Forms.DataGridViewTextBoxColumn supplier_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn tax_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn invoice_due;
        private System.Windows.Forms.DataGridViewTextBoxColumn doc_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn doc_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn net_amount;
    }
}