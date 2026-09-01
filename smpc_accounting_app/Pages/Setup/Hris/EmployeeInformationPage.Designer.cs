
namespace smpc_accounting_app.Pages.Setup.Hris
{
    partial class EmployeeInformationPage
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

        #region Component Designer generated code

        // Skeleton only: header, nav toolstrip, detail TabControl (fills the page).
        // Document-style navigation per the suite convention: NEW - SEARCH -
        // << PREV - NEXT >> stepping through one employee at a time; SEARCH opens
        // a popup list (no permanent list grid on the form). The field controls
        // inside each tab are built in code-behind (BuildDetailTabs).
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btn_new = new System.Windows.Forms.ToolStripButton();
            this.btn_search = new System.Windows.Forms.ToolStripButton();
            this.btn_prev = new System.Windows.Forms.ToolStripButton();
            this.btn_next = new System.Windows.Forms.ToolStripButton();
            this.btn_edit = new System.Windows.Forms.ToolStripButton();
            this.btn_save = new System.Windows.Forms.ToolStripButton();
            this.btn_cancel = new System.Windows.Forms.ToolStripButton();
            this.btn_status = new System.Windows.Forms.ToolStripButton();
            this.btn_link = new System.Windows.Forms.ToolStripButton();
            this.lbl_record = new System.Windows.Forms.ToolStripLabel();
            this.tabControlDetail = new System.Windows.Forms.TabControl();
            this.tab_identity = new System.Windows.Forms.TabPage();
            this.tab_gov = new System.Windows.Forms.TabPage();
            this.tab_comp = new System.Windows.Forms.TabPage();
            this.tab_contacts = new System.Windows.Forms.TabPage();
            this.tab_records = new System.Windows.Forms.TabPage();
            this.tab_files = new System.Windows.Forms.TabPage();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tabControlDetail.SuspendLayout();
            this.SuspendLayout();
            //
            // panel1
            //
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1400, 47);
            this.panel1.TabIndex = 1;
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F);
            this.label1.Location = new System.Drawing.Point(12, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(216, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Employee Information";
            //
            // toolStrip1
            //
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_new,
            this.btn_search,
            this.btn_prev,
            this.btn_next,
            this.btn_edit,
            this.btn_save,
            this.btn_cancel,
            this.btn_status,
            this.btn_link,
            this.lbl_record});
            this.toolStrip1.Location = new System.Drawing.Point(0, 47);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1400, 25);
            this.toolStrip1.TabIndex = 10;
            //
            // btn_new
            //
            this.btn_new.Name = "btn_new";
            this.btn_new.Size = new System.Drawing.Size(51, 22);
            this.btn_new.Text = "New";
            this.btn_new.Click += new System.EventHandler(this.btn_new_Click);
            //
            // btn_search
            //
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(58, 22);
            this.btn_search.Text = "Search";
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            //
            // btn_prev
            //
            this.btn_prev.Name = "btn_prev";
            this.btn_prev.Size = new System.Drawing.Size(62, 22);
            this.btn_prev.Text = "<< Prev";
            this.btn_prev.Click += new System.EventHandler(this.btn_prev_Click);
            //
            // btn_next
            //
            this.btn_next.Name = "btn_next";
            this.btn_next.Size = new System.Drawing.Size(62, 22);
            this.btn_next.Text = "Next >>";
            this.btn_next.Click += new System.EventHandler(this.btn_next_Click);
            //
            // btn_edit
            //
            this.btn_edit.Name = "btn_edit";
            this.btn_edit.Size = new System.Drawing.Size(47, 22);
            this.btn_edit.Text = "Edit";
            this.btn_edit.Click += new System.EventHandler(this.btn_edit_Click);
            //
            // btn_save
            //
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(51, 22);
            this.btn_save.Text = "Save";
            this.btn_save.Visible = false;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            //
            // btn_cancel
            //
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(63, 22);
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.Visible = false;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            //
            // btn_status
            //
            this.btn_status.Name = "btn_status";
            this.btn_status.Size = new System.Drawing.Size(78, 22);
            this.btn_status.Text = "Set Status";
            this.btn_status.Click += new System.EventHandler(this.btn_status_Click);
            //
            // btn_link
            //
            this.btn_link.Name = "btn_link";
            this.btn_link.Size = new System.Drawing.Size(96, 22);
            this.btn_link.Text = "Link ERP User";
            this.btn_link.Click += new System.EventHandler(this.btn_link_Click);
            //
            // lbl_record
            //
            this.lbl_record.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.lbl_record.Name = "lbl_record";
            this.lbl_record.Size = new System.Drawing.Size(30, 22);
            this.lbl_record.Text = "0 / 0";
            //
            // tabControlDetail
            //
            this.tabControlDetail.Controls.Add(this.tab_identity);
            this.tabControlDetail.Controls.Add(this.tab_gov);
            this.tabControlDetail.Controls.Add(this.tab_comp);
            this.tabControlDetail.Controls.Add(this.tab_contacts);
            this.tabControlDetail.Controls.Add(this.tab_records);
            this.tabControlDetail.Controls.Add(this.tab_files);
            this.tabControlDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlDetail.Location = new System.Drawing.Point(0, 72);
            this.tabControlDetail.Name = "tabControlDetail";
            this.tabControlDetail.SelectedIndex = 0;
            this.tabControlDetail.Size = new System.Drawing.Size(1400, 878);
            this.tabControlDetail.TabIndex = 64;
            //
            // tab pages
            //
            this.tab_identity.Name = "tab_identity";
            this.tab_identity.Text = "Identity && Employment";
            this.tab_identity.UseVisualStyleBackColor = true;
            this.tab_gov.Name = "tab_gov";
            this.tab_gov.Text = "Government IDs";
            this.tab_gov.UseVisualStyleBackColor = true;
            this.tab_comp.Name = "tab_comp";
            this.tab_comp.Text = "Compensation";
            this.tab_comp.UseVisualStyleBackColor = true;
            this.tab_contacts.Name = "tab_contacts";
            this.tab_contacts.Text = "Contacts && Address";
            this.tab_contacts.UseVisualStyleBackColor = true;
            this.tab_records.Name = "tab_records";
            this.tab_records.Text = "201 Records";
            this.tab_records.UseVisualStyleBackColor = true;
            this.tab_files.Name = "tab_files";
            this.tab_files.Text = "Files";
            this.tab_files.UseVisualStyleBackColor = true;
            //
            // EmployeeInformationPage
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControlDetail);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panel1);
            this.Name = "EmployeeInformationPage";
            this.Size = new System.Drawing.Size(1400, 950);
            this.Load += new System.EventHandler(this.EmployeeInformationPage_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControlDetail.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btn_new;
        private System.Windows.Forms.ToolStripButton btn_search;
        private System.Windows.Forms.ToolStripButton btn_prev;
        private System.Windows.Forms.ToolStripButton btn_next;
        private System.Windows.Forms.ToolStripButton btn_edit;
        private System.Windows.Forms.ToolStripButton btn_save;
        private System.Windows.Forms.ToolStripButton btn_cancel;
        private System.Windows.Forms.ToolStripButton btn_status;
        private System.Windows.Forms.ToolStripButton btn_link;
        private System.Windows.Forms.ToolStripLabel lbl_record;
        private System.Windows.Forms.TabControl tabControlDetail;
        private System.Windows.Forms.TabPage tab_identity;
        private System.Windows.Forms.TabPage tab_gov;
        private System.Windows.Forms.TabPage tab_comp;
        private System.Windows.Forms.TabPage tab_contacts;
        private System.Windows.Forms.TabPage tab_records;
        private System.Windows.Forms.TabPage tab_files;
    }
}
