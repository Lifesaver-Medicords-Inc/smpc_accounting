
namespace smpc_accounting_app.Pages.Transactions.Journal.DebitMemoModals
{
    partial class DebitMemoCreditMemoPicker
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
            this.dgv_credit_memo_search = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.doc_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trans_amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reason_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.doc_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txt_search = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_credit_memo_search)).BeginInit();
            this.SuspendLayout();
            //
            // dgv_credit_memo_search
            //
            this.dgv_credit_memo_search.AllowUserToAddRows = false;
            this.dgv_credit_memo_search.AllowUserToDeleteRows = false;
            this.dgv_credit_memo_search.AutoGenerateColumns = false;
            this.dgv_credit_memo_search.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_credit_memo_search.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.doc_no,
            this.trans_amount,
            this.reason_code,
            this.doc_date});
            this.dgv_credit_memo_search.Location = new System.Drawing.Point(-1, 31);
            this.dgv_credit_memo_search.Name = "dgv_credit_memo_search";
            this.dgv_credit_memo_search.ReadOnly = true;
            this.dgv_credit_memo_search.RowHeadersVisible = false;
            this.dgv_credit_memo_search.Size = new System.Drawing.Size(802, 389);
            this.dgv_credit_memo_search.TabIndex = 8;
            this.dgv_credit_memo_search.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_credit_memo_search_CellClick);
            //
            // id
            //
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.Visible = false;
            //
            // doc_no
            //
            this.doc_no.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.doc_no.DataPropertyName = "doc_no";
            this.doc_no.HeaderText = "DOC NO.";
            this.doc_no.Name = "doc_no";
            //
            // trans_amount
            //
            this.trans_amount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.trans_amount.DataPropertyName = "trans_amount";
            this.trans_amount.HeaderText = "TRANS. AMOUNT";
            this.trans_amount.Name = "trans_amount";
            //
            // reason_code
            //
            this.reason_code.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.reason_code.DataPropertyName = "reason_code";
            this.reason_code.HeaderText = "REASON CODE";
            this.reason_code.Name = "reason_code";
            //
            // doc_date
            //
            this.doc_date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.doc_date.DataPropertyName = "doc_date";
            this.doc_date.HeaderText = "DOC DATE";
            this.doc_date.Name = "doc_date";
            //
            // txt_search
            //
            this.txt_search.Location = new System.Drawing.Point(350, 215);
            this.txt_search.Name = "txt_search";
            this.txt_search.Size = new System.Drawing.Size(100, 20);
            this.txt_search.TabIndex = 9;
            //
            // DebitMemoCreditMemoPicker
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgv_credit_memo_search);
            this.Controls.Add(this.txt_search);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "DebitMemoCreditMemoPicker";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Credit Memo";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_credit_memo_search)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_credit_memo_search;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn doc_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn trans_amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn reason_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn doc_date;
        private System.Windows.Forms.TextBox txt_search;
    }
}
