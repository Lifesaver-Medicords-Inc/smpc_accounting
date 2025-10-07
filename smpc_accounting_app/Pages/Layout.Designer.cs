
namespace smpc_accounting_app
{
    partial class Layout
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("AP Voucher");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Sales Return");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Payment Receipt");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Account Receivables", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Invoice Receipt");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("AP Voucher");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Payment Voucher");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("AP Voucher");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Accounts Payables", new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode6,
            treeNode7,
            treeNode8});
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Journal Voucher");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Credit Memo");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Sales Invoice");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Transactions", new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode9,
            treeNode10,
            treeNode11,
            treeNode12});
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Tax Reports");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Reports");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("Book Setup");
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("Bank Setup");
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("Currency Setup");
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("Chart Of Account Setup");
            System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("GL Mapper Setup");
            System.Windows.Forms.TreeNode treeNode21 = new System.Windows.Forms.TreeNode("Chart Class Setup");
            System.Windows.Forms.TreeNode treeNode22 = new System.Windows.Forms.TreeNode("Chart Account Setup");
            System.Windows.Forms.TreeNode treeNode23 = new System.Windows.Forms.TreeNode("Expanded Tax Setup");
            System.Windows.Forms.TreeNode treeNode24 = new System.Windows.Forms.TreeNode("Final Tax Setup");
            System.Windows.Forms.TreeNode treeNode25 = new System.Windows.Forms.TreeNode("Input Vat Setup");
            System.Windows.Forms.TreeNode treeNode26 = new System.Windows.Forms.TreeNode("Output Vat Setup");
            System.Windows.Forms.TreeNode treeNode27 = new System.Windows.Forms.TreeNode("Tax Setup", new System.Windows.Forms.TreeNode[] {
            treeNode23,
            treeNode24,
            treeNode25,
            treeNode26});
            System.Windows.Forms.TreeNode treeNode28 = new System.Windows.Forms.TreeNode("Invoice Receipt");
            System.Windows.Forms.TreeNode treeNode29 = new System.Windows.Forms.TreeNode("System Configuration", new System.Windows.Forms.TreeNode[] {
            treeNode16,
            treeNode17,
            treeNode18,
            treeNode19,
            treeNode20,
            treeNode21,
            treeNode22,
            treeNode27,
            treeNode28});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Layout));
            this.panel1 = new System.Windows.Forms.Panel();
            this.Sidebar = new System.Windows.Forms.TreeView();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.container = new System.Windows.Forms.Panel();
            this.tabContainer = new System.Windows.Forms.TabControl();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.container.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Sidebar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 450);
            this.panel1.TabIndex = 0;
            // 
            // Sidebar
            // 
            this.Sidebar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Sidebar.Location = new System.Drawing.Point(0, 0);
            this.Sidebar.Name = "Sidebar";
            treeNode1.Name = "APVoucher";
            treeNode1.Text = "AP Voucher";
            treeNode2.Name = "Node14";
            treeNode2.Text = "Sales Return";
            treeNode3.Name = "Node12";
            treeNode3.Text = "Payment Receipt";
            treeNode4.Name = "parent_2";
            treeNode4.Text = "Account Receivables";
            treeNode5.Name = "Invoice Receipt";
            treeNode5.Text = "Invoice Receipt";
            treeNode6.Name = "Node1";
            treeNode6.Text = "AP Voucher";
            treeNode7.Name = "Node2";
            treeNode7.Text = "Payment Voucher";
            treeNode8.Name = "AP Voucher";
            treeNode8.Text = "AP Voucher";
            treeNode9.Name = "parent_3";
            treeNode9.Text = "Accounts Payables";
            treeNode10.Name = "Journal Voucher";
            treeNode10.Text = "Journal Voucher";
            treeNode11.Name = "Credit Memo";
            treeNode11.Text = "Credit Memo";
            treeNode12.Name = "Sales Invoice";
            treeNode12.Text = "Sales Invoice";
            treeNode13.Name = "parent_1";
            treeNode13.Text = "Transactions";
            treeNode14.Name = "parent_4";
            treeNode14.Text = "Tax Reports";
            treeNode15.Name = "parent_5";
            treeNode15.Text = "Reports";
            treeNode16.Name = "Book Setup";
            treeNode16.Text = "Book Setup";
            treeNode17.Name = "Bank Setup";
            treeNode17.Text = "Bank Setup";
            treeNode18.Name = "Currency Setup";
            treeNode18.Text = "Currency Setup";
            treeNode19.Name = "Chart Of Account Setup";
            treeNode19.Text = "Chart Of Account Setup";
            treeNode20.Name = "GL Mapper Setup";
            treeNode20.Text = "GL Mapper Setup";
            treeNode21.Name = "Chart Class Setup";
            treeNode21.Text = "Chart Class Setup";
            treeNode22.Name = "Chart Account Setup";
            treeNode22.Text = "Chart Account Setup";
            treeNode23.Name = "Expanded Tax Setup";
            treeNode23.Text = "Expanded Tax Setup";
            treeNode24.Name = "Final Tax Setup";
            treeNode24.Text = "Final Tax Setup";
            treeNode25.Name = "Input Vat Setup";
            treeNode25.Text = "Input Vat Setup";
            treeNode26.Name = "Output Vat Setup";
            treeNode26.Text = "Output Vat Setup";
            treeNode27.Name = "parent_6";
            treeNode27.Text = "Tax Setup";
            treeNode28.Name = "Invoice Receipt";
            treeNode28.Text = "Invoice Receipt";
            treeNode29.Name = "parent_6";
            treeNode29.Text = "System Configuration";
            this.Sidebar.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode13,
            treeNode14,
            treeNode15,
            treeNode29});
            this.Sidebar.Size = new System.Drawing.Size(200, 450);
            this.Sidebar.TabIndex = 0;
            this.Sidebar.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.Sidebar_AfterSelect);
            this.Sidebar.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.Sidebar_NodeMouseClick);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.LightCoral;
            this.panel5.Controls.Add(this.label1);
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel5.Location = new System.Drawing.Point(944, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(313, 450);
            this.panel5.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold);
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(108, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "RED BOX";
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.DarkRed;
            this.panel6.Location = new System.Drawing.Point(1, 39);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(312, 1);
            this.panel6.TabIndex = 0;
            // 
            // container
            // 
            this.container.Controls.Add(this.tabContainer);
            this.container.Dock = System.Windows.Forms.DockStyle.Fill;
            this.container.Location = new System.Drawing.Point(200, 0);
            this.container.Name = "container";
            this.container.Size = new System.Drawing.Size(744, 450);
            this.container.TabIndex = 3;
            // 
            // tabContainer
            // 
            this.tabContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabContainer.Location = new System.Drawing.Point(0, 0);
            this.tabContainer.Multiline = true;
            this.tabContainer.Name = "tabContainer";
            this.tabContainer.SelectedIndex = 0;
            this.tabContainer.Size = new System.Drawing.Size(744, 450);
            this.tabContainer.TabIndex = 0;
            this.tabContainer.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabContainer_DrawItem);
            this.tabContainer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tabContainer_MouseDown);
            // 
            // Layout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1257, 450);
            this.Controls.Add(this.container);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Layout";
            this.Text = "SMPC - ACCOUNTING";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.container.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TreeView Sidebar;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel container;
        private System.Windows.Forms.TabControl tabContainer;
    }
}

