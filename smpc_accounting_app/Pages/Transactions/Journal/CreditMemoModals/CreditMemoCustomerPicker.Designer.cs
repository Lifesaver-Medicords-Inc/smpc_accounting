namespace smpc_accounting_app.Pages.Transactions.Journal.CreditMemoModals
{
    partial class CreditMemoCustomerPicker
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.dgv_customer_search = new System.Windows.Forms.DataGridView();
            this.partner_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customer_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.payment_term = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tax_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customer_address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_customer_search)).BeginInit();
            this.SuspendLayout();
            //
            // dgv_customer_search
            //
            this.dgv_customer_search.AllowUserToAddRows = false;
            this.dgv_customer_search.AllowUserToDeleteRows = false;
            this.dgv_customer_search.AutoGenerateColumns = false;
            this.dgv_customer_search.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_customer_search.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.partner_id,
            this.customer_code,
            this.customer,
            this.payment_term,
            this.tax_code,
            this.customer_address,
            this.tin});
            this.dgv_customer_search.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgv_customer_search.Location = new System.Drawing.Point(0, 31);
            this.dgv_customer_search.MultiSelect = false;
            this.dgv_customer_search.Name = "dgv_customer_search";
            this.dgv_customer_search.ReadOnly = true;
            this.dgv_customer_search.RowHeadersVisible = false;
            this.dgv_customer_search.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_customer_search.Size = new System.Drawing.Size(800, 419);
            this.dgv_customer_search.TabIndex = 0;
            this.dgv_customer_search.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_customer_search_CellClick);
            //
            // partner_id
            //
            // Hidden, but present: this is tbl_bpi_general.id (the branch), which is
            // what Credit Memo's partner registration guard checks - NOT the parent
            // tbl_bpi.id that vw_get_customer exposes as customer_id.
            this.partner_id.DataPropertyName = "partner_id";
            this.partner_id.HeaderText = "partner_id";
            this.partner_id.Name = "partner_id";
            this.partner_id.ReadOnly = true;
            this.partner_id.Visible = false;
            //
            // customer_code
            //
            this.customer_code.DataPropertyName = "customer_code";
            this.customer_code.HeaderText = "CODE";
            this.customer_code.Name = "customer_code";
            this.customer_code.ReadOnly = true;
            this.customer_code.Width = 90;
            //
            // customer
            //
            this.customer.DataPropertyName = "customer";
            this.customer.HeaderText = "CUSTOMER";
            this.customer.Name = "customer";
            this.customer.ReadOnly = true;
            this.customer.Width = 200;
            //
            // payment_term
            //
            this.payment_term.DataPropertyName = "payment_term";
            this.payment_term.HeaderText = "PAYMENT TERM";
            this.payment_term.Name = "payment_term";
            this.payment_term.ReadOnly = true;
            this.payment_term.Width = 110;
            //
            // tax_code
            //
            this.tax_code.DataPropertyName = "tax_code";
            this.tax_code.HeaderText = "TAX CODE";
            this.tax_code.Name = "tax_code";
            this.tax_code.ReadOnly = true;
            this.tax_code.Width = 80;
            //
            // customer_address
            //
            this.customer_address.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.customer_address.DataPropertyName = "customer_address";
            this.customer_address.HeaderText = "ADDRESS";
            this.customer_address.Name = "customer_address";
            this.customer_address.ReadOnly = true;
            //
            // tin
            //
            this.tin.DataPropertyName = "tin";
            this.tin.HeaderText = "TIN";
            this.tin.Name = "tin";
            this.tin.ReadOnly = true;
            this.tin.Width = 120;
            //
            // CreditMemoCustomerPicker
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgv_customer_search);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreditMemoCustomerPicker";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Customer";
            this.Load += new System.EventHandler(this.CreditMemoCustomerPicker_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_customer_search)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_customer_search;
        private System.Windows.Forms.DataGridViewTextBoxColumn partner_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn customer_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn customer;
        private System.Windows.Forms.DataGridViewTextBoxColumn payment_term;
        private System.Windows.Forms.DataGridViewTextBoxColumn tax_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn customer_address;
        private System.Windows.Forms.DataGridViewTextBoxColumn tin;
        private System.Windows.Forms.TextBox txt_search;
    }
}
