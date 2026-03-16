
namespace smpc_accounting_app.Pages.Transactions.AccountsReceivables.PaymentReceipt.PaymentReceiptModals
{
    partial class PaymentReceiptSearch
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
            this.txt_search = new System.Windows.Forms.TextBox();
            this.dgv_pr_search = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customer_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reference_cr_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.date_collect = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reference_or_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.doc_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.doc_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.transaction_amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_pr_search)).BeginInit();
            this.SuspendLayout();
            // 
            // txt_search
            // 
            this.txt_search.Location = new System.Drawing.Point(350, 215);
            this.txt_search.Name = "txt_search";
            this.txt_search.Size = new System.Drawing.Size(100, 20);
            this.txt_search.TabIndex = 9;
            // 
            // dgv_pr_search
            // 
            this.dgv_pr_search.AllowUserToAddRows = false;
            this.dgv_pr_search.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_pr_search.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_pr_search.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_pr_search.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.customer,
            this.customer_code,
            this.reference_cr_no,
            this.date_collect,
            this.reference_or_no,
            this.doc_no,
            this.doc_date,
            this.transaction_amount});
            this.dgv_pr_search.Location = new System.Drawing.Point(-1, 31);
            this.dgv_pr_search.Name = "dgv_pr_search";
            this.dgv_pr_search.Size = new System.Drawing.Size(802, 389);
            this.dgv_pr_search.TabIndex = 10;
            this.dgv_pr_search.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_pr_search_CellClick);
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
            // customer
            // 
            this.customer.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.customer.DataPropertyName = "customer";
            this.customer.HeaderText = "CUSTOMER";
            this.customer.Name = "customer";
            this.customer.ReadOnly = true;
            // 
            // customer_code
            // 
            this.customer_code.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.customer_code.DataPropertyName = "customer_code";
            this.customer_code.HeaderText = "CUSTOMER CODE";
            this.customer_code.Name = "customer_code";
            this.customer_code.ReadOnly = true;
            // 
            // reference_cr_no
            // 
            this.reference_cr_no.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.reference_cr_no.DataPropertyName = "reference_cr_no";
            this.reference_cr_no.HeaderText = "REF CR NO";
            this.reference_cr_no.Name = "reference_cr_no";
            this.reference_cr_no.ReadOnly = true;
            // 
            // date_collect
            // 
            this.date_collect.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.date_collect.DataPropertyName = "date_collect";
            this.date_collect.HeaderText = "DATE COLLECTED";
            this.date_collect.MinimumWidth = 150;
            this.date_collect.Name = "date_collect";
            this.date_collect.ReadOnly = true;
            // 
            // reference_or_no
            // 
            this.reference_or_no.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.reference_or_no.DataPropertyName = "reference_or_no";
            this.reference_or_no.HeaderText = "REF OR NO";
            this.reference_or_no.Name = "reference_or_no";
            this.reference_or_no.ReadOnly = true;
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
            // PaymentReceiptSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgv_pr_search);
            this.Controls.Add(this.txt_search);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "PaymentReceiptSearch";
            this.Text = "PaymentReceiptSearch";
            this.Load += new System.EventHandler(this.PaymentReceiptSearch_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_pr_search)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txt_search;
        private System.Windows.Forms.DataGridView dgv_pr_search;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn customer;
        private System.Windows.Forms.DataGridViewTextBoxColumn customer_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn reference_cr_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn date_collect;
        private System.Windows.Forms.DataGridViewTextBoxColumn reference_or_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn doc_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn doc_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn transaction_amount;
    }
}