namespace smpc_accounting_app.Pages.Shared
{
    partial class RedBoxAR
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

        // Slim refresh/status strip on top, then a single AutoScroll card list
        // filling the rest - this control is mounted below the existing
        // "RED BOX" title/divider in Layout's panel5, so it doesn't repeat its
        // own title. Same skeleton as smpc_sales_system's and smpc_dispatching's
        // RedBox controls, just with one section instead of two.
        private void InitializeComponent()
        {
            this.pnl_top = new System.Windows.Forms.Panel();
            this.lbl_status = new System.Windows.Forms.Label();
            this.btn_refresh = new System.Windows.Forms.Button();
            this.pnl_cards = new System.Windows.Forms.FlowLayoutPanel();
            this.pnl_top.SuspendLayout();
            this.SuspendLayout();
            //
            // pnl_top
            //
            this.pnl_top.Controls.Add(this.lbl_status);
            this.pnl_top.Controls.Add(this.btn_refresh);
            this.pnl_top.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_top.Location = new System.Drawing.Point(0, 0);
            this.pnl_top.Name = "pnl_top";
            this.pnl_top.Size = new System.Drawing.Size(300, 24);
            this.pnl_top.TabIndex = 0;
            //
            // lbl_status
            //
            this.lbl_status.AutoSize = true;
            this.lbl_status.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.lbl_status.ForeColor = System.Drawing.Color.DimGray;
            this.lbl_status.Location = new System.Drawing.Point(4, 6);
            this.lbl_status.Name = "lbl_status";
            this.lbl_status.Size = new System.Drawing.Size(0, 12);
            this.lbl_status.TabIndex = 1;
            //
            // btn_refresh
            //
            this.btn_refresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_refresh.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.btn_refresh.Location = new System.Drawing.Point(230, 1);
            this.btn_refresh.Name = "btn_refresh";
            this.btn_refresh.Size = new System.Drawing.Size(65, 21);
            this.btn_refresh.TabIndex = 0;
            this.btn_refresh.Text = "Refresh";
            this.btn_refresh.UseVisualStyleBackColor = true;
            this.btn_refresh.Click += new System.EventHandler(this.btn_refresh_Click);
            //
            // pnl_cards
            //
            this.pnl_cards.AutoScroll = true;
            this.pnl_cards.BackColor = System.Drawing.Color.LightCoral;
            this.pnl_cards.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_cards.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnl_cards.Location = new System.Drawing.Point(0, 24);
            this.pnl_cards.Name = "pnl_cards";
            this.pnl_cards.Size = new System.Drawing.Size(300, 376);
            this.pnl_cards.TabIndex = 1;
            this.pnl_cards.WrapContents = false;
            //
            // RedBoxAR
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnl_cards);
            this.Controls.Add(this.pnl_top);
            this.Name = "RedBoxAR";
            this.Size = new System.Drawing.Size(300, 400);
            this.pnl_top.ResumeLayout(false);
            this.pnl_top.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_top;
        private System.Windows.Forms.Label lbl_status;
        private System.Windows.Forms.Button btn_refresh;
        private System.Windows.Forms.FlowLayoutPanel pnl_cards;
    }
}
