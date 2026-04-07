
namespace smpc_accounting_app.Pages.Transactions.Journal
{
    partial class JournalEntryPage
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JournalEntryPage));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle37 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle38 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle39 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle40 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle41 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle42 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle43 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle44 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle45 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btn_new = new System.Windows.Forms.ToolStripButton();
            this.btn_search = new System.Windows.Forms.ToolStripButton();
            this.btn_edit = new System.Windows.Forms.ToolStripButton();
            this.btn_delete = new System.Windows.Forms.ToolStripButton();
            this.btn_print = new System.Windows.Forms.ToolStripButton();
            this.btn_save = new System.Windows.Forms.ToolStripButton();
            this.btn_cancel = new System.Windows.Forms.ToolStripButton();
            this.btn_next = new System.Windows.Forms.ToolStripButton();
            this.btn_prev = new System.Windows.Forms.ToolStripButton();
            this.pnl_content = new System.Windows.Forms.Panel();
            this.txt_period = new System.Windows.Forms.TextBox();
            this.lbl_search = new System.Windows.Forms.Label();
            this.txt_search = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_doc_no = new System.Windows.Forms.TextBox();
            this.txt_id = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_currency = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_journal_name = new System.Windows.Forms.TextBox();
            this.txt_journal_description = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dgv_journal_entry = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.journal_entry_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.account_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.inserted_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.posting_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.account_title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmb_posting_ref = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.posting_ref = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.debit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.credit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.line_memo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.pnl_content.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_journal_entry)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1400, 47);
            this.panel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Journal Entry";
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_new,
            this.btn_search,
            this.btn_edit,
            this.btn_delete,
            this.btn_print,
            this.btn_save,
            this.btn_cancel,
            this.btn_next,
            this.btn_prev});
            this.toolStrip1.Location = new System.Drawing.Point(0, 47);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(1400, 25);
            this.toolStrip1.TabIndex = 11;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btn_new
            // 
            this.btn_new.Image = ((System.Drawing.Image)(resources.GetObject("btn_new.Image")));
            this.btn_new.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_new.Name = "btn_new";
            this.btn_new.Size = new System.Drawing.Size(51, 22);
            this.btn_new.Text = "New";
            this.btn_new.Click += new System.EventHandler(this.btn_new_Click);
            // 
            // btn_search
            // 
            this.btn_search.Image = ((System.Drawing.Image)(resources.GetObject("btn_search.Image")));
            this.btn_search.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(62, 22);
            this.btn_search.Text = "Search";
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // btn_edit
            // 
            this.btn_edit.Image = ((System.Drawing.Image)(resources.GetObject("btn_edit.Image")));
            this.btn_edit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_edit.Name = "btn_edit";
            this.btn_edit.Size = new System.Drawing.Size(47, 22);
            this.btn_edit.Text = "Edit";
            this.btn_edit.Click += new System.EventHandler(this.btn_edit_Click);
            // 
            // btn_delete
            // 
            this.btn_delete.Image = ((System.Drawing.Image)(resources.GetObject("btn_delete.Image")));
            this.btn_delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.Size = new System.Drawing.Size(60, 22);
            this.btn_delete.Text = "Delete";
            this.btn_delete.Click += new System.EventHandler(this.btn_delete_Click);
            // 
            // btn_print
            // 
            this.btn_print.Image = ((System.Drawing.Image)(resources.GetObject("btn_print.Image")));
            this.btn_print.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_print.Name = "btn_print";
            this.btn_print.Size = new System.Drawing.Size(52, 22);
            this.btn_print.Text = "Print";
            // 
            // btn_save
            // 
            this.btn_save.Image = ((System.Drawing.Image)(resources.GetObject("btn_save.Image")));
            this.btn_save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(51, 22);
            this.btn_save.Text = "Save";
            this.btn_save.Visible = false;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Image = ((System.Drawing.Image)(resources.GetObject("btn_cancel.Image")));
            this.btn_cancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(63, 22);
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.Visible = false;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_next
            // 
            this.btn_next.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btn_next.Image = ((System.Drawing.Image)(resources.GetObject("btn_next.Image")));
            this.btn_next.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_next.Name = "btn_next";
            this.btn_next.Size = new System.Drawing.Size(52, 22);
            this.btn_next.Text = "Next";
            this.btn_next.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btn_next.Click += new System.EventHandler(this.btn_next_Click);
            // 
            // btn_prev
            // 
            this.btn_prev.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btn_prev.Image = ((System.Drawing.Image)(resources.GetObject("btn_prev.Image")));
            this.btn_prev.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_prev.Name = "btn_prev";
            this.btn_prev.Size = new System.Drawing.Size(72, 22);
            this.btn_prev.Text = "Previous";
            this.btn_prev.Click += new System.EventHandler(this.btn_prev_Click);
            // 
            // pnl_content
            // 
            this.pnl_content.Controls.Add(this.txt_period);
            this.pnl_content.Controls.Add(this.lbl_search);
            this.pnl_content.Controls.Add(this.txt_search);
            this.pnl_content.Controls.Add(this.label7);
            this.pnl_content.Controls.Add(this.txt_doc_no);
            this.pnl_content.Controls.Add(this.txt_id);
            this.pnl_content.Controls.Add(this.label5);
            this.pnl_content.Controls.Add(this.label4);
            this.pnl_content.Controls.Add(this.txt_currency);
            this.pnl_content.Controls.Add(this.label6);
            this.pnl_content.Controls.Add(this.label3);
            this.pnl_content.Controls.Add(this.txt_journal_name);
            this.pnl_content.Controls.Add(this.txt_journal_description);
            this.pnl_content.Controls.Add(this.label2);
            this.pnl_content.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_content.Location = new System.Drawing.Point(0, 72);
            this.pnl_content.Name = "pnl_content";
            this.pnl_content.Size = new System.Drawing.Size(1400, 162);
            this.pnl_content.TabIndex = 69;
            // 
            // txt_period
            // 
            this.txt_period.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txt_period.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.txt_period.Location = new System.Drawing.Point(158, 71);
            this.txt_period.Name = "txt_period";
            this.txt_period.ReadOnly = true;
            this.txt_period.Size = new System.Drawing.Size(289, 20);
            this.txt_period.TabIndex = 102;
            this.txt_period.Tag = "";
            // 
            // lbl_search
            // 
            this.lbl_search.AutoSize = true;
            this.lbl_search.Location = new System.Drawing.Point(68, 122);
            this.lbl_search.Name = "lbl_search";
            this.lbl_search.Size = new System.Drawing.Size(51, 13);
            this.lbl_search.TabIndex = 101;
            this.lbl_search.Text = "SEARCH";
            // 
            // txt_search
            // 
            this.txt_search.Location = new System.Drawing.Point(158, 119);
            this.txt_search.Name = "txt_search";
            this.txt_search.Size = new System.Drawing.Size(180, 20);
            this.txt_search.TabIndex = 100;
            this.txt_search.TextChanged += new System.EventHandler(this.txt_search_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(931, 47);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 13);
            this.label7.TabIndex = 70;
            this.label7.Text = "DOC:";
            // 
            // txt_doc_no
            // 
            this.txt_doc_no.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.txt_doc_no.Location = new System.Drawing.Point(1021, 44);
            this.txt_doc_no.Name = "txt_doc_no";
            this.txt_doc_no.ReadOnly = true;
            this.txt_doc_no.Size = new System.Drawing.Size(289, 20);
            this.txt_doc_no.TabIndex = 71;
            this.txt_doc_no.Tag = "";
            // 
            // txt_id
            // 
            this.txt_id.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txt_id.Enabled = false;
            this.txt_id.Location = new System.Drawing.Point(1021, 84);
            this.txt_id.Name = "txt_id";
            this.txt_id.Size = new System.Drawing.Size(199, 20);
            this.txt_id.TabIndex = 69;
            this.txt_id.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(931, 87);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(18, 13);
            this.label5.TabIndex = 68;
            this.label5.Text = "ID";
            this.label5.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(931, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 66;
            this.label4.Text = "CURRENCY:";
            // 
            // txt_currency
            // 
            this.txt_currency.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.txt_currency.Location = new System.Drawing.Point(1021, 23);
            this.txt_currency.Name = "txt_currency";
            this.txt_currency.ReadOnly = true;
            this.txt_currency.Size = new System.Drawing.Size(289, 20);
            this.txt_currency.TabIndex = 67;
            this.txt_currency.Tag = "";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(68, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 13);
            this.label6.TabIndex = 64;
            this.label6.Text = "PERIOD:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(68, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 57;
            this.label3.Text = "JOURNAL:";
            // 
            // txt_journal_name
            // 
            this.txt_journal_name.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.txt_journal_name.Location = new System.Drawing.Point(158, 29);
            this.txt_journal_name.Name = "txt_journal_name";
            this.txt_journal_name.ReadOnly = true;
            this.txt_journal_name.Size = new System.Drawing.Size(289, 20);
            this.txt_journal_name.TabIndex = 59;
            this.txt_journal_name.Tag = "REQUIRED";
            // 
            // txt_journal_description
            // 
            this.txt_journal_description.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txt_journal_description.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.txt_journal_description.Location = new System.Drawing.Point(158, 50);
            this.txt_journal_description.Name = "txt_journal_description";
            this.txt_journal_description.ReadOnly = true;
            this.txt_journal_description.Size = new System.Drawing.Size(289, 20);
            this.txt_journal_description.TabIndex = 61;
            this.txt_journal_description.Tag = "REQUIRED";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(68, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 60;
            this.label2.Text = "DESCRIPTION:";
            // 
            // dgv_journal_entry
            // 
            this.dgv_journal_entry.AllowUserToAddRows = false;
            this.dgv_journal_entry.AllowUserToDeleteRows = false;
            this.dgv_journal_entry.AllowUserToResizeColumns = false;
            dataGridViewCellStyle37.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle37.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle37.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle37.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle37.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle37.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle37.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_journal_entry.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle37;
            this.dgv_journal_entry.ColumnHeadersHeight = 50;
            this.dgv_journal_entry.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv_journal_entry.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.journal_entry_id,
            this.account_id,
            this.inserted_date,
            this.posting_date,
            this.account_title,
            this.cmb_posting_ref,
            this.posting_ref,
            this.debit,
            this.credit,
            this.line_memo});
            this.dgv_journal_entry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_journal_entry.EnableHeadersVisualStyles = false;
            this.dgv_journal_entry.Location = new System.Drawing.Point(0, 234);
            this.dgv_journal_entry.Name = "dgv_journal_entry";
            this.dgv_journal_entry.Size = new System.Drawing.Size(1400, 716);
            this.dgv_journal_entry.TabIndex = 72;
            this.dgv_journal_entry.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgv_journal_entry_CellFormatting);
            this.dgv_journal_entry.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_journal_entry_CellValueChanged);
            this.dgv_journal_entry.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgv_journal_entry_CurrentCellDirtyStateChanged);
            this.dgv_journal_entry.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgv_journal_entry_DataError);
            this.dgv_journal_entry.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgv_journal_entry_EditingControlShowing);
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "ID";
            this.id.Name = "id";
            this.id.Visible = false;
            // 
            // journal_entry_id
            // 
            this.journal_entry_id.DataPropertyName = "journal_entry_id";
            this.journal_entry_id.HeaderText = "JOURNAL ENTRY ID";
            this.journal_entry_id.Name = "journal_entry_id";
            this.journal_entry_id.Visible = false;
            // 
            // account_id
            // 
            this.account_id.DataPropertyName = "account_id";
            this.account_id.HeaderText = "ACCOUNT ID";
            this.account_id.Name = "account_id";
            this.account_id.Visible = false;
            // 
            // inserted_date
            // 
            this.inserted_date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.inserted_date.DataPropertyName = "inserted_date";
            dataGridViewCellStyle38.BackColor = System.Drawing.Color.Gainsboro;
            this.inserted_date.DefaultCellStyle = dataGridViewCellStyle38;
            this.inserted_date.HeaderText = "DATE";
            this.inserted_date.MinimumWidth = 200;
            this.inserted_date.Name = "inserted_date";
            this.inserted_date.ReadOnly = true;
            // 
            // posting_date
            // 
            this.posting_date.DataPropertyName = "posting_date";
            dataGridViewCellStyle39.BackColor = System.Drawing.Color.Gainsboro;
            this.posting_date.DefaultCellStyle = dataGridViewCellStyle39;
            this.posting_date.HeaderText = "POSTING DATE";
            this.posting_date.Name = "posting_date";
            this.posting_date.ReadOnly = true;
            this.posting_date.Visible = false;
            // 
            // account_title
            // 
            this.account_title.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.account_title.DataPropertyName = "account_title";
            dataGridViewCellStyle40.BackColor = System.Drawing.Color.Gainsboro;
            this.account_title.DefaultCellStyle = dataGridViewCellStyle40;
            this.account_title.HeaderText = "ACCT TITLE & EXPLANATION";
            this.account_title.Name = "account_title";
            this.account_title.ReadOnly = true;
            // 
            // cmb_posting_ref
            // 
            this.cmb_posting_ref.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cmb_posting_ref.DataPropertyName = "serial_no";
            dataGridViewCellStyle41.BackColor = System.Drawing.Color.Gainsboro;
            this.cmb_posting_ref.DefaultCellStyle = dataGridViewCellStyle41;
            this.cmb_posting_ref.HeaderText = "POSTING REF.";
            this.cmb_posting_ref.MinimumWidth = 180;
            this.cmb_posting_ref.Name = "cmb_posting_ref";
            this.cmb_posting_ref.ReadOnly = true;
            this.cmb_posting_ref.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cmb_posting_ref.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // posting_ref
            // 
            this.posting_ref.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.posting_ref.DataPropertyName = "posting_ref";
            dataGridViewCellStyle42.BackColor = System.Drawing.Color.Gainsboro;
            this.posting_ref.DefaultCellStyle = dataGridViewCellStyle42;
            this.posting_ref.HeaderText = "POSTING REF.";
            this.posting_ref.Name = "posting_ref";
            this.posting_ref.ReadOnly = true;
            this.posting_ref.Visible = false;
            // 
            // debit
            // 
            this.debit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.debit.DataPropertyName = "debit";
            dataGridViewCellStyle43.BackColor = System.Drawing.Color.Gainsboro;
            this.debit.DefaultCellStyle = dataGridViewCellStyle43;
            this.debit.HeaderText = "DEBIT";
            this.debit.Name = "debit";
            this.debit.ReadOnly = true;
            // 
            // credit
            // 
            this.credit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.credit.DataPropertyName = "credit";
            dataGridViewCellStyle44.BackColor = System.Drawing.Color.Gainsboro;
            this.credit.DefaultCellStyle = dataGridViewCellStyle44;
            this.credit.HeaderText = "CREDIT";
            this.credit.MinimumWidth = 180;
            this.credit.Name = "credit";
            this.credit.ReadOnly = true;
            // 
            // line_memo
            // 
            this.line_memo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.line_memo.DataPropertyName = "line_memo";
            dataGridViewCellStyle45.BackColor = System.Drawing.Color.Gainsboro;
            this.line_memo.DefaultCellStyle = dataGridViewCellStyle45;
            this.line_memo.HeaderText = "LINE MEMO";
            this.line_memo.Name = "line_memo";
            this.line_memo.ReadOnly = true;
            // 
            // JournalEntryPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgv_journal_entry);
            this.Controls.Add(this.pnl_content);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panel1);
            this.Name = "JournalEntryPage";
            this.Size = new System.Drawing.Size(1400, 950);
            this.Load += new System.EventHandler(this.JournalEntry_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.pnl_content.ResumeLayout(false);
            this.pnl_content.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_journal_entry)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btn_new;
        private System.Windows.Forms.ToolStripButton btn_edit;
        private System.Windows.Forms.ToolStripButton btn_delete;
        private System.Windows.Forms.ToolStripButton btn_print;
        private System.Windows.Forms.ToolStripButton btn_save;
        private System.Windows.Forms.ToolStripButton btn_cancel;
        private System.Windows.Forms.Panel pnl_content;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_doc_no;
        private System.Windows.Forms.TextBox txt_id;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_currency;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_journal_name;
        private System.Windows.Forms.TextBox txt_journal_description;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripButton btn_next;
        private System.Windows.Forms.ToolStripButton btn_prev;
        private System.Windows.Forms.Label lbl_search;
        private System.Windows.Forms.TextBox txt_search;
        private System.Windows.Forms.TextBox txt_period;
        private System.Windows.Forms.ToolStripButton btn_search;
        private System.Windows.Forms.DataGridView dgv_journal_entry;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn journal_entry_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn account_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn inserted_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn posting_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn account_title;
        private System.Windows.Forms.DataGridViewComboBoxColumn cmb_posting_ref;
        private System.Windows.Forms.DataGridViewTextBoxColumn posting_ref;
        private System.Windows.Forms.DataGridViewTextBoxColumn debit;
        private System.Windows.Forms.DataGridViewTextBoxColumn credit;
        private System.Windows.Forms.DataGridViewTextBoxColumn line_memo;
    }
}
