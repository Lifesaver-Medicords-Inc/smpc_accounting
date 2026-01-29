
namespace smpc_accounting_app.Pages.Transactions.AccountsPayable.InvoiceReceipt.InvoiceReceiptModals
{
    partial class InvoiceSearchSupplier
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
            this.dgv_suplier_search = new System.Windows.Forms.DataGridView();
            this.txt_search = new System.Windows.Forms.TextBox();
            this.supplier_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.supplier = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.supplier_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.invoice_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.payment_term = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_suplier_search)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_suplier_search
            // 
            this.dgv_suplier_search.AllowUserToAddRows = false;
            this.dgv_suplier_search.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_suplier_search.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_suplier_search.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_suplier_search.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.supplier_id,
            this.supplier,
            this.supplier_code,
            this.invoice_type,
            this.payment_term,
            this.type});
            this.dgv_suplier_search.Location = new System.Drawing.Point(-1, 31);
            this.dgv_suplier_search.Name = "dgv_suplier_search";
            this.dgv_suplier_search.Size = new System.Drawing.Size(802, 389);
            this.dgv_suplier_search.TabIndex = 8;
            this.dgv_suplier_search.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_suplier_search_CellClick);
            // 
            // txt_search
            // 
            this.txt_search.Location = new System.Drawing.Point(350, 215);
            this.txt_search.Name = "txt_search";
            this.txt_search.Size = new System.Drawing.Size(100, 20);
            this.txt_search.TabIndex = 9;
            // 
            // supplier_id
            // 
            this.supplier_id.DataPropertyName = "supplier_id";
            this.supplier_id.HeaderText = "SUPPLIER ID";
            this.supplier_id.Name = "supplier_id";
            this.supplier_id.Visible = false;
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
            // invoice_type
            // 
            this.invoice_type.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.invoice_type.DataPropertyName = "invoice_type";
            this.invoice_type.HeaderText = "INVOICE TYPE";
            this.invoice_type.Name = "invoice_type";
            this.invoice_type.ReadOnly = true;
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
            // type
            // 
            this.type.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.type.DataPropertyName = "type";
            this.type.HeaderText = "TYPE";
            this.type.Name = "type";
            this.type.ReadOnly = true;
            // 
            // InvoiceSearchSupplier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgv_suplier_search);
            this.Controls.Add(this.txt_search);
            this.Name = "InvoiceSearchSupplier";
            this.Text = "InvoiceSearchSupplier";
            this.Load += new System.EventHandler(this.InvoiceSearchSupplier_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_suplier_search)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_suplier_search;
        private System.Windows.Forms.TextBox txt_search;
        private System.Windows.Forms.DataGridViewTextBoxColumn supplier_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn supplier;
        private System.Windows.Forms.DataGridViewTextBoxColumn supplier_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn invoice_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn payment_term;
        private System.Windows.Forms.DataGridViewTextBoxColumn type;
    }
}