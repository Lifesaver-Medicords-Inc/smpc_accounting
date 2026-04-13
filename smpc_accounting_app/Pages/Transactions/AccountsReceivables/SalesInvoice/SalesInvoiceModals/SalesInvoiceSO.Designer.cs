
namespace smpc_accounting_app.Pages.Transactions.AccountsReceivables.SalesInvoice.SalesInvoiceModals
{
    partial class SalesInvoiceSO
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
            this.dgv_main_search = new System.Windows.Forms.DataGridView();
            this.txt_search = new System.Windows.Forms.TextBox();
            this.btn_ok = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isSelected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.sales_order_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.so_number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dr_number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.doc_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customer_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sales_person = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.total_sales = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_main_search)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_main_search
            // 
            this.dgv_main_search.AllowUserToAddRows = false;
            this.dgv_main_search.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_main_search.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_main_search.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_main_search.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.isSelected,
            this.sales_order_id,
            this.so_number,
            this.dr_number,
            this.doc_date,
            this.customer_name,
            this.sales_person,
            this.total_sales});
            this.dgv_main_search.Location = new System.Drawing.Point(-1, 31);
            this.dgv_main_search.Name = "dgv_main_search";
            this.dgv_main_search.Size = new System.Drawing.Size(802, 328);
            this.dgv_main_search.TabIndex = 9;
            // 
            // txt_search
            // 
            this.txt_search.Location = new System.Drawing.Point(350, 215);
            this.txt_search.Name = "txt_search";
            this.txt_search.Size = new System.Drawing.Size(100, 20);
            this.txt_search.TabIndex = 10;
            // 
            // btn_ok
            // 
            this.btn_ok.Location = new System.Drawing.Point(590, 399);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(75, 23);
            this.btn_ok.TabIndex = 15;
            this.btn_ok.Text = "OK";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(699, 399);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 14;
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
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
            // isSelected
            // 
            this.isSelected.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle2.NullValue = false;
            this.isSelected.DefaultCellStyle = dataGridViewCellStyle2;
            this.isSelected.HeaderText = "";
            this.isSelected.MinimumWidth = 50;
            this.isSelected.Name = "isSelected";
            this.isSelected.Width = 50;
            // 
            // sales_order_id
            // 
            this.sales_order_id.DataPropertyName = "sales_order_id";
            this.sales_order_id.HeaderText = "SO ID";
            this.sales_order_id.Name = "sales_order_id";
            this.sales_order_id.ReadOnly = true;
            this.sales_order_id.Visible = false;
            // 
            // so_number
            // 
            this.so_number.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.so_number.DataPropertyName = "so_number";
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Gainsboro;
            this.so_number.DefaultCellStyle = dataGridViewCellStyle3;
            this.so_number.HeaderText = "REFERENCE DOC";
            this.so_number.Name = "so_number";
            this.so_number.ReadOnly = true;
            // 
            // dr_number
            // 
            this.dr_number.DataPropertyName = "dr_number";
            this.dr_number.HeaderText = "DR NUMBER";
            this.dr_number.Name = "dr_number";
            this.dr_number.ReadOnly = true;
            this.dr_number.Visible = false;
            // 
            // doc_date
            // 
            this.doc_date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.doc_date.DataPropertyName = "doc_date";
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Gainsboro;
            this.doc_date.DefaultCellStyle = dataGridViewCellStyle4;
            this.doc_date.HeaderText = "DOC DATE";
            this.doc_date.Name = "doc_date";
            this.doc_date.ReadOnly = true;
            // 
            // customer_name
            // 
            this.customer_name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.customer_name.DataPropertyName = "customer_name";
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Gainsboro;
            this.customer_name.DefaultCellStyle = dataGridViewCellStyle5;
            this.customer_name.HeaderText = "CUSTOMER NAME";
            this.customer_name.Name = "customer_name";
            this.customer_name.ReadOnly = true;
            // 
            // sales_person
            // 
            this.sales_person.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.sales_person.DataPropertyName = "sales_person";
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.Gainsboro;
            this.sales_person.DefaultCellStyle = dataGridViewCellStyle6;
            this.sales_person.HeaderText = "SALES PERSON";
            this.sales_person.Name = "sales_person";
            this.sales_person.ReadOnly = true;
            // 
            // total_sales
            // 
            this.total_sales.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.total_sales.DataPropertyName = "total_sales";
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.Gainsboro;
            this.total_sales.DefaultCellStyle = dataGridViewCellStyle7;
            this.total_sales.HeaderText = "TOTAL SALES";
            this.total_sales.Name = "total_sales";
            // 
            // SalesInvoiceSO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.dgv_main_search);
            this.Controls.Add(this.txt_search);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SalesInvoiceSO";
            this.Text = "SalesInvoiceSO";
            this.Load += new System.EventHandler(this.SalesInvoiceSO_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_main_search)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_main_search;
        private System.Windows.Forms.TextBox txt_search;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isSelected;
        private System.Windows.Forms.DataGridViewTextBoxColumn sales_order_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn so_number;
        private System.Windows.Forms.DataGridViewTextBoxColumn dr_number;
        private System.Windows.Forms.DataGridViewTextBoxColumn doc_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn customer_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn sales_person;
        private System.Windows.Forms.DataGridViewTextBoxColumn total_sales;
    }
}