
namespace smpc_accounting_app.Pages.Transactions.Journal.JournalEntry
{
    partial class JournalEntrySearch
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
            this.dgv_je_search = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.journal_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.journal_description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.doc_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.currency = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.created_by = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txt_search = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_je_search)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_je_search
            // 
            this.dgv_je_search.AllowUserToAddRows = false;
            this.dgv_je_search.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_je_search.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_je_search.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_je_search.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.journal_name,
            this.journal_description,
            this.doc_no,
            this.currency,
            this.created_by});
            this.dgv_je_search.Location = new System.Drawing.Point(-1, 31);
            this.dgv_je_search.Name = "dgv_je_search";
            this.dgv_je_search.Size = new System.Drawing.Size(802, 389);
            this.dgv_je_search.TabIndex = 5;
            this.dgv_je_search.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_je_search_CellClick);
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
            // journal_name
            // 
            this.journal_name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.journal_name.DataPropertyName = "journal_name";
            this.journal_name.HeaderText = "JOURNAL";
            this.journal_name.Name = "journal_name";
            this.journal_name.ReadOnly = true;
            // 
            // journal_description
            // 
            this.journal_description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.journal_description.DataPropertyName = "journal_description";
            this.journal_description.HeaderText = "DESCRIPTION";
            this.journal_description.Name = "journal_description";
            this.journal_description.ReadOnly = true;
            // 
            // doc_no
            // 
            this.doc_no.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.doc_no.DataPropertyName = "doc_no";
            this.doc_no.HeaderText = "DOC NO";
            this.doc_no.Name = "doc_no";
            this.doc_no.ReadOnly = true;
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
            // created_by
            // 
            this.created_by.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.created_by.DataPropertyName = "created_by";
            this.created_by.HeaderText = "CREATED BY";
            this.created_by.Name = "created_by";
            this.created_by.ReadOnly = true;
            // 
            // txt_search
            // 
            this.txt_search.Location = new System.Drawing.Point(350, 215);
            this.txt_search.Name = "txt_search";
            this.txt_search.Size = new System.Drawing.Size(100, 20);
            this.txt_search.TabIndex = 6;
            // 
            // JournalEntrySearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgv_je_search);
            this.Controls.Add(this.txt_search);
            this.Name = "JournalEntrySearch";
            this.Text = "JournalEntrySearch";
            this.Load += new System.EventHandler(this.JournalEntrySearch_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_je_search)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_je_search;
        private System.Windows.Forms.TextBox txt_search;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn journal_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn journal_description;
        private System.Windows.Forms.DataGridViewTextBoxColumn doc_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn currency;
        private System.Windows.Forms.DataGridViewTextBoxColumn created_by;
    }
}