
namespace smpc_accounting_app.Pages.Transactions.AccountsPayable.APVoucher.APVoucherModals
{
    partial class APVoucherInvoice
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
            this.dgv_ir_search = new System.Windows.Forms.DataGridView();
            this.invoice_receipt_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.receipt_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ir_doc_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ir_due_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.twas_amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.line_amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.receipt_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txt_search = new System.Windows.Forms.TextBox();
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
            this.invoice_receipt_id,
            this.receipt_no,
            this.ir_doc_date,
            this.ir_due_date,
            this.twas_amount,
            this.line_amount,
            this.receipt_type});
            this.dgv_ir_search.Location = new System.Drawing.Point(-1, 31);
            this.dgv_ir_search.Name = "dgv_ir_search";
            this.dgv_ir_search.Size = new System.Drawing.Size(802, 389);
            this.dgv_ir_search.TabIndex = 8;
            this.dgv_ir_search.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_ir_search_CellClick);
            // 
            // invoice_receipt_id
            // 
            this.invoice_receipt_id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.invoice_receipt_id.DataPropertyName = "invoice_receipt_id";
            this.invoice_receipt_id.HeaderText = "ID";
            this.invoice_receipt_id.Name = "invoice_receipt_id";
            this.invoice_receipt_id.ReadOnly = true;
            this.invoice_receipt_id.Visible = false;
            this.invoice_receipt_id.Width = 80;
            // 
            // receipt_no
            // 
            this.receipt_no.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.receipt_no.DataPropertyName = "receipt_no";
            this.receipt_no.HeaderText = "RECEIPT NO";
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
            // ir_due_date
            // 
            this.ir_due_date.DataPropertyName = "ir_due_date";
            this.ir_due_date.HeaderText = "DUE DATE";
            this.ir_due_date.Name = "ir_due_date";
            this.ir_due_date.ReadOnly = true;
            this.ir_due_date.Visible = false;
            // 
            // twas_amount
            // 
            this.twas_amount.DataPropertyName = "twas_amount";
            this.twas_amount.HeaderText = "TWAS AMOUNT";
            this.twas_amount.Name = "twas_amount";
            this.twas_amount.ReadOnly = true;
            this.twas_amount.Visible = false;
            // 
            // line_amount
            // 
            this.line_amount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.line_amount.DataPropertyName = "line_amount";
            this.line_amount.HeaderText = "AMOUNT";
            this.line_amount.Name = "line_amount";
            this.line_amount.ReadOnly = true;
            // 
            // receipt_type
            // 
            this.receipt_type.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.receipt_type.DataPropertyName = "receipt_type";
            this.receipt_type.HeaderText = "RECEIPT TYPE";
            this.receipt_type.Name = "receipt_type";
            this.receipt_type.ReadOnly = true;
            // 
            // txt_search
            // 
            this.txt_search.Location = new System.Drawing.Point(350, 215);
            this.txt_search.Name = "txt_search";
            this.txt_search.Size = new System.Drawing.Size(100, 20);
            this.txt_search.TabIndex = 9;
            // 
            // APVoucherInvoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgv_ir_search);
            this.Controls.Add(this.txt_search);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "APVoucherInvoice";
            this.Text = "APVoucherInvoice";
            this.Load += new System.EventHandler(this.APVoucherInvoice_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ir_search)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_ir_search;
        private System.Windows.Forms.TextBox txt_search;
        private System.Windows.Forms.DataGridViewTextBoxColumn invoice_receipt_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn receipt_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn ir_doc_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn ir_due_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn twas_amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn line_amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn receipt_type;
    }
}