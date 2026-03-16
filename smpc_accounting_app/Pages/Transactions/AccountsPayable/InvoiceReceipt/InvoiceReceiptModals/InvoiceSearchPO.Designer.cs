
namespace smpc_accounting_app.Pages.Transactions.AccountsPayable.InvoiceReceipt.InvoiceReceiptModals
{
    partial class InvoiceSearchPO
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_select_all = new System.Windows.Forms.Button();
            this.btn_unselect_all = new System.Windows.Forms.Button();
            this.btn_ok = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.treev_main = new System.Windows.Forms.TreeView();
            this.flow_main = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_select_all);
            this.panel1.Controls.Add(this.btn_unselect_all);
            this.panel1.Controls.Add(this.btn_ok);
            this.panel1.Controls.Add(this.btn_cancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(186, 401);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(614, 49);
            this.panel1.TabIndex = 2;
            // 
            // btn_select_all
            // 
            this.btn_select_all.Location = new System.Drawing.Point(191, 14);
            this.btn_select_all.Name = "btn_select_all";
            this.btn_select_all.Size = new System.Drawing.Size(75, 23);
            this.btn_select_all.TabIndex = 3;
            this.btn_select_all.Text = "Select All";
            this.btn_select_all.UseVisualStyleBackColor = true;
            this.btn_select_all.Click += new System.EventHandler(this.btn_select_all_Click);
            // 
            // btn_unselect_all
            // 
            this.btn_unselect_all.Location = new System.Drawing.Point(301, 14);
            this.btn_unselect_all.Name = "btn_unselect_all";
            this.btn_unselect_all.Size = new System.Drawing.Size(75, 23);
            this.btn_unselect_all.TabIndex = 2;
            this.btn_unselect_all.Text = "UnSelect All";
            this.btn_unselect_all.UseVisualStyleBackColor = true;
            this.btn_unselect_all.Click += new System.EventHandler(this.btn_unselect_all_Click);
            // 
            // btn_ok
            // 
            this.btn_ok.Location = new System.Drawing.Point(412, 14);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(75, 23);
            this.btn_ok.TabIndex = 1;
            this.btn_ok.Text = "OK";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(521, 14);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 0;
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // treev_main
            // 
            this.treev_main.Dock = System.Windows.Forms.DockStyle.Left;
            this.treev_main.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.treev_main.Location = new System.Drawing.Point(0, 0);
            this.treev_main.Name = "treev_main";
            this.treev_main.Size = new System.Drawing.Size(186, 450);
            this.treev_main.TabIndex = 1;
            this.treev_main.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treev_main_AfterSelect);
            // 
            // flow_main
            // 
            this.flow_main.AutoScroll = true;
            this.flow_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flow_main.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flow_main.Location = new System.Drawing.Point(186, 0);
            this.flow_main.Name = "flow_main";
            this.flow_main.Size = new System.Drawing.Size(614, 401);
            this.flow_main.TabIndex = 3;
            this.flow_main.WrapContents = false;
            // 
            // InvoiceSearchPO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.flow_main);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.treev_main);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "InvoiceSearchPO";
            this.Text = "InvoiceSearchPO";
            this.Load += new System.EventHandler(this.InvoiceSearchPO_Load);
            this.Resize += new System.EventHandler(this.InvoiceSearchPO_Resize);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_select_all;
        private System.Windows.Forms.Button btn_unselect_all;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.TreeView treev_main;
        private System.Windows.Forms.FlowLayoutPanel flow_main;
    }
}