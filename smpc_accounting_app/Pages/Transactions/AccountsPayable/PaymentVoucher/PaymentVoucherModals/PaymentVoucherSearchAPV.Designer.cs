
namespace smpc_accounting_app.Pages.Transactions.AccountsPayable.PaymentVoucher.PaymentVoucherModals
{
    partial class PaymentVoucherSearchAPV
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
            this.txt_search = new System.Windows.Forms.TextBox();
            this.dgv_apv_search = new System.Windows.Forms.DataGridView();
            this.btn_ok = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.ap_voucher_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isSelected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.supplier = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.supplier_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.currency = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.doc_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.doc_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.transaction_amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_apv_search)).BeginInit();
            this.SuspendLayout();
            // 
            // txt_search
            // 
            this.txt_search.Location = new System.Drawing.Point(350, 215);
            this.txt_search.Name = "txt_search";
            this.txt_search.Size = new System.Drawing.Size(100, 20);
            this.txt_search.TabIndex = 10;
            // 
            // dgv_apv_search
            // 
            this.dgv_apv_search.AllowUserToAddRows = false;
            this.dgv_apv_search.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_apv_search.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_apv_search.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_apv_search.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ap_voucher_id,
            this.isSelected,
            this.supplier,
            this.supplier_code,
            this.currency,
            this.doc_no,
            this.doc_date,
            this.transaction_amount});
            this.dgv_apv_search.Location = new System.Drawing.Point(-1, 31);
            this.dgv_apv_search.Name = "dgv_apv_search";
            this.dgv_apv_search.Size = new System.Drawing.Size(802, 328);
            this.dgv_apv_search.TabIndex = 11;
            // 
            // btn_ok
            // 
            this.btn_ok.Location = new System.Drawing.Point(580, 403);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(75, 23);
            this.btn_ok.TabIndex = 13;
            this.btn_ok.Text = "OK";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(689, 403);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 12;
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // ap_voucher_id
            // 
            this.ap_voucher_id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ap_voucher_id.DataPropertyName = "ap_voucher_id";
            this.ap_voucher_id.HeaderText = "ID";
            this.ap_voucher_id.Name = "ap_voucher_id";
            this.ap_voucher_id.ReadOnly = true;
            this.ap_voucher_id.Visible = false;
            this.ap_voucher_id.Width = 80;
            // 
            // isSelected
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle2.NullValue = false;
            this.isSelected.DefaultCellStyle = dataGridViewCellStyle2;
            this.isSelected.HeaderText = "";
            this.isSelected.MinimumWidth = 50;
            this.isSelected.Name = "isSelected";
            this.isSelected.Width = 50;
            // 
            // supplier
            // 
            this.supplier.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.supplier.DataPropertyName = "supplier";
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Gainsboro;
            this.supplier.DefaultCellStyle = dataGridViewCellStyle3;
            this.supplier.HeaderText = "SUPPLIER";
            this.supplier.Name = "supplier";
            this.supplier.ReadOnly = true;
            // 
            // supplier_code
            // 
            this.supplier_code.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.supplier_code.DataPropertyName = "supplier_code";
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Gainsboro;
            this.supplier_code.DefaultCellStyle = dataGridViewCellStyle4;
            this.supplier_code.HeaderText = "SUPPLIER CODE";
            this.supplier_code.Name = "supplier_code";
            this.supplier_code.ReadOnly = true;
            // 
            // currency
            // 
            this.currency.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.currency.DataPropertyName = "currency";
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Gainsboro;
            this.currency.DefaultCellStyle = dataGridViewCellStyle5;
            this.currency.HeaderText = "CURRENCY";
            this.currency.MinimumWidth = 150;
            this.currency.Name = "currency";
            this.currency.ReadOnly = true;
            // 
            // doc_no
            // 
            this.doc_no.DataPropertyName = "doc_no";
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.Gainsboro;
            this.doc_no.DefaultCellStyle = dataGridViewCellStyle6;
            this.doc_no.HeaderText = "DOC NO";
            this.doc_no.Name = "doc_no";
            this.doc_no.ReadOnly = true;
            // 
            // doc_date
            // 
            this.doc_date.DataPropertyName = "doc_date";
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.Gainsboro;
            this.doc_date.DefaultCellStyle = dataGridViewCellStyle7;
            this.doc_date.HeaderText = "DOC DATE";
            this.doc_date.Name = "doc_date";
            this.doc_date.ReadOnly = true;
            // 
            // transaction_amount
            // 
            this.transaction_amount.DataPropertyName = "transaction_amount";
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.Gainsboro;
            this.transaction_amount.DefaultCellStyle = dataGridViewCellStyle8;
            this.transaction_amount.HeaderText = "TRANSACTION AMOUNT";
            this.transaction_amount.Name = "transaction_amount";
            this.transaction_amount.ReadOnly = true;
            // 
            // PaymentVoucherSearchAPV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.dgv_apv_search);
            this.Controls.Add(this.txt_search);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "PaymentVoucherSearchAPV";
            this.Text = "PaymentVoucherSearchAPV";
            this.Load += new System.EventHandler(this.PaymentVoucherSearchAPV_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_apv_search)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txt_search;
        private System.Windows.Forms.DataGridView dgv_apv_search;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn ap_voucher_id;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isSelected;
        private System.Windows.Forms.DataGridViewTextBoxColumn supplier;
        private System.Windows.Forms.DataGridViewTextBoxColumn supplier_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn currency;
        private System.Windows.Forms.DataGridViewTextBoxColumn doc_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn doc_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn transaction_amount;
    }
}