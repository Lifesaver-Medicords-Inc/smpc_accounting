
namespace smpc_accounting_app.Pages.Transactions.AccountsReceivables.SalesInvoice
{
    partial class SalesInvoiceCustomer
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
            this.dgv_customer_search = new System.Windows.Forms.DataGridView();
            this.txt_search = new System.Windows.Forms.TextBox();
            this.customer_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customer_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tax_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.payment_term = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customer_address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.overpayment_amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_customer_search)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_customer_search
            // 
            this.dgv_customer_search.AllowUserToAddRows = false;
            this.dgv_customer_search.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_customer_search.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_customer_search.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_customer_search.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.customer_id,
            this.customer,
            this.customer_code,
            this.tax_code,
            this.tax,
            this.tin,
            this.payment_term,
            this.customer_address,
            this.overpayment_amount});
            this.dgv_customer_search.Location = new System.Drawing.Point(-1, 31);
            this.dgv_customer_search.Name = "dgv_customer_search";
            this.dgv_customer_search.Size = new System.Drawing.Size(802, 389);
            this.dgv_customer_search.TabIndex = 9;
            this.dgv_customer_search.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_customer_search_CellClick);
            // 
            // txt_search
            // 
            this.txt_search.Location = new System.Drawing.Point(350, 215);
            this.txt_search.Name = "txt_search";
            this.txt_search.Size = new System.Drawing.Size(100, 20);
            this.txt_search.TabIndex = 10;
            // 
            // customer_id
            // 
            this.customer_id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.customer_id.DataPropertyName = "customer_id";
            this.customer_id.HeaderText = "CUSTOMER ID";
            this.customer_id.Name = "customer_id";
            this.customer_id.ReadOnly = true;
            this.customer_id.Visible = false;
            this.customer_id.Width = 80;
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
            // tax_code
            // 
            this.tax_code.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.tax_code.DataPropertyName = "tax_code";
            this.tax_code.HeaderText = "TAX CODE";
            this.tax_code.Name = "tax_code";
            this.tax_code.ReadOnly = true;
            // 
            // tax
            // 
            this.tax.DataPropertyName = "tax";
            this.tax.HeaderText = "TAX";
            this.tax.Name = "tax";
            this.tax.ReadOnly = true;
            this.tax.Visible = false;
            // 
            // tin
            // 
            this.tin.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.tin.DataPropertyName = "tin";
            this.tin.HeaderText = "TIN";
            this.tin.Name = "tin";
            this.tin.ReadOnly = true;
            // 
            // payment_term
            // 
            this.payment_term.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.payment_term.DataPropertyName = "payment_term";
            this.payment_term.HeaderText = "PAYMENT TERM";
            this.payment_term.MinimumWidth = 150;
            this.payment_term.Name = "payment_term";
            this.payment_term.ReadOnly = true;
            // 
            // customer_address
            // 
            this.customer_address.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.customer_address.DataPropertyName = "customer_address";
            this.customer_address.HeaderText = "ADDRESS";
            this.customer_address.Name = "customer_address";
            this.customer_address.ReadOnly = true;
            // 
            // overpayment_amount
            // 
            this.overpayment_amount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.overpayment_amount.DataPropertyName = "overpayment_amount";
            this.overpayment_amount.HeaderText = "OVERPAYMENT AMOUNT";
            this.overpayment_amount.Name = "overpayment_amount";
            this.overpayment_amount.ReadOnly = true;
            // 
            // SalesInvoiceCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgv_customer_search);
            this.Controls.Add(this.txt_search);
            this.Name = "SalesInvoiceCustomer";
            this.Text = "SalesInvoiceCustomer";
            this.Load += new System.EventHandler(this.SalesInvoiceCustomer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_customer_search)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_customer_search;
        private System.Windows.Forms.TextBox txt_search;
        private System.Windows.Forms.DataGridViewTextBoxColumn customer_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn customer;
        private System.Windows.Forms.DataGridViewTextBoxColumn customer_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn tax_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn tax;
        private System.Windows.Forms.DataGridViewTextBoxColumn tin;
        private System.Windows.Forms.DataGridViewTextBoxColumn payment_term;
        private System.Windows.Forms.DataGridViewTextBoxColumn customer_address;
        private System.Windows.Forms.DataGridViewTextBoxColumn overpayment_amount;
    }
}