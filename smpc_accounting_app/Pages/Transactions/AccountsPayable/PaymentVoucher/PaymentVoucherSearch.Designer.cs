
namespace smpc_accounting_app.Pages.Transactions.AccountsPayable.PaymentVoucher
{
    partial class PaymentVoucherSearch
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
            this.dgv_pv_search = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.supplier = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.supplier_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reference_apv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.currency = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.doc_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.doc_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.transaction_amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txt_search = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_pv_search)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_pv_search
            // 
            this.dgv_pv_search.AllowUserToAddRows = false;
            this.dgv_pv_search.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_pv_search.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_pv_search.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_pv_search.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.supplier,
            this.supplier_code,
            this.reference_apv,
            this.currency,
            this.doc_no,
            this.doc_date,
            this.transaction_amount});
            this.dgv_pv_search.Location = new System.Drawing.Point(-1, 31);
            this.dgv_pv_search.Name = "dgv_pv_search";
            this.dgv_pv_search.Size = new System.Drawing.Size(802, 389);
            this.dgv_pv_search.TabIndex = 8;
            this.dgv_pv_search.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_pv_search_CellClick);
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
            this.supplier.HeaderText = "SUPPLIER";
            this.supplier.Name = "supplier";
            this.supplier.ReadOnly = true;
            // 
            // supplier_code
            // 
            this.supplier_code.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.supplier_code.DataPropertyName = "supplier_code";
            this.supplier_code.HeaderText = "SUPPLIER CODE";
            this.supplier_code.Name = "supplier_code";
            this.supplier_code.ReadOnly = true;
            // 
            // reference_apv
            // 
            this.reference_apv.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.reference_apv.DataPropertyName = "reference_apv";
            this.reference_apv.HeaderText = "REFERENCE APV";
            this.reference_apv.Name = "reference_apv";
            this.reference_apv.ReadOnly = true;
            // 
            // currency
            // 
            this.currency.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.currency.DataPropertyName = "currency";
            this.currency.HeaderText = "CURRENCY";
            this.currency.MinimumWidth = 150;
            this.currency.Name = "currency";
            this.currency.ReadOnly = true;
            // 
            // doc_no
            // 
            this.doc_no.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.doc_no.DataPropertyName = "doc_no";
            this.doc_no.HeaderText = "DOC NO";
            this.doc_no.Name = "doc_no";
            this.doc_no.ReadOnly = true;
            // 
            // doc_date
            // 
            this.doc_date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.doc_date.DataPropertyName = "doc_date";
            this.doc_date.HeaderText = "DOC DATE";
            this.doc_date.Name = "doc_date";
            this.doc_date.ReadOnly = true;
            // 
            // transaction_amount
            // 
            this.transaction_amount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.transaction_amount.DataPropertyName = "transaction_amount";
            this.transaction_amount.HeaderText = "TRANSACTION AMOUNT";
            this.transaction_amount.Name = "transaction_amount";
            this.transaction_amount.ReadOnly = true;
            // 
            // txt_search
            // 
            this.txt_search.Location = new System.Drawing.Point(350, 215);
            this.txt_search.Name = "txt_search";
            this.txt_search.Size = new System.Drawing.Size(100, 20);
            this.txt_search.TabIndex = 9;
            // 
            // PaymentVoucherSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgv_pv_search);
            this.Controls.Add(this.txt_search);
            this.Name = "PaymentVoucherSearch";
            this.Text = "PaymentVoucherSearch";
            this.Load += new System.EventHandler(this.PaymentVoucherSearch_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_pv_search)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_pv_search;
        private System.Windows.Forms.TextBox txt_search;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn supplier;
        private System.Windows.Forms.DataGridViewTextBoxColumn supplier_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn reference_apv;
        private System.Windows.Forms.DataGridViewTextBoxColumn currency;
        private System.Windows.Forms.DataGridViewTextBoxColumn doc_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn doc_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn transaction_amount;
    }
}