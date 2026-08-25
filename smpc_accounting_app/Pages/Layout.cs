using smpc_accounting_app.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using smpc_accounting_app.Pages.Components;
using smpc_accounting_app.Pages.Shared;
using smpc_accounting_app.Shared;
using smpc_accounting_app.Models;
using smpc_accounting_app.Services.Helpers;

namespace smpc_accounting_app
{
    public partial class Layout : Form
    {
        private int tabCount = 0;
        GeneralService<CompanySetupModel> serviceCompanySetup;
        GeneralService<JournalEntryModel> serviceJournalSetup;
        GeneralService<ExchangeRateModel> serviceCurrencyRateSetup;
        private string _currencyCode;
        private readonly RedBoxAR _redBoxAR = new RedBoxAR { Dock = DockStyle.Fill };

        public Layout()
        {
            InitializeComponent();

            tabContainer.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabContainer.SizeMode = TabSizeMode.Fixed;
            tabContainer.ItemSize = new Size(150, 20); // Width, Height of tabs

            //tabContainer.DrawItem += tabContainer_DrawItem;
            //tabContainer.MouseDown += tabContainer_MouseDown;

            pnl_redbox_body.Controls.Add(_redBoxAR);
        }

        private void Sidebar_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                if (!e.Node.Name.Contains("parent"))
                {
                    RoutesService route = new RoutesService(e.Node.Name);
                    ShowForm(route.GetTitle(), route.GetForm());
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void OpenRoute(string routeName)
        {
            try
            {
                RoutesService route = new RoutesService(routeName);
                ShowForm(route.GetTitle(), route.GetForm());
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ShowForm(string tabTitle, Control control)
        {
            try
            {
                tabCount++;

                TabPage newTab = new TabPage(tabTitle); 

                //control.Width = this.Width - 235; 
                container.Height = this.Height * 2;
                //control.Height = this.Height;
                control.Width = this.Width - 570;
                newTab.Controls.Add(control);
                newTab.AutoScroll = true;
                tabContainer.TabPages.Add(newTab);
                tabContainer.SelectTab(newTab);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void removeTab(object sender, EventArgs e)
        {
            try
            {
                tabContainer.TabPages.Remove(tabContainer.SelectedTab);
            }
            catch (Exception)
            {
                throw;
            }  
        }

        private void tabContainer_DrawItem(object sender, DrawItemEventArgs e)
        {
            var tabPage = tabContainer.TabPages[e.Index];
            var tabRect = tabContainer.GetTabRect(e.Index); 
            bool isSelected = (e.Index == tabContainer.SelectedIndex);

            // Draw the tab title
            string title = tabPage.Text;
            Font font = isSelected ? new Font(e.Font, FontStyle.Bold) : e.Font;
            using (Brush textBrush = new SolidBrush(tabPage.ForeColor))
            {
                e.Graphics.DrawString(title, font, textBrush, tabRect.X + 2, tabRect.Y + 4);
            }

            // Define close button size and position
            int closeButtonSize = 16;
            Rectangle closeButton = new Rectangle(
                tabRect.Right - closeButtonSize - 5,  // Padding from right
                tabRect.Top + (tabRect.Height - 16) / 2,     // Vertically center
                closeButtonSize,
                closeButtonSize
            );

            // Draw a border box (optional)
            // e.Graphics.DrawRectangle(Pens.Gray, closeButton);

            // Draw "X" centered inside the rectangle
            using (Font closeFont = new Font("Arial", 9, FontStyle.Bold))
            {
                StringFormat sf = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };
                e.Graphics.DrawString("x", closeFont, Brushes.Black, closeButton, sf);
            }
        }

        private void tabContainer_MouseDown(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < tabContainer.TabPages.Count; i++)
            {
                Rectangle tabRect = tabContainer.GetTabRect(i);
                int closeButtonSize = 16;
                Rectangle closeButton = new Rectangle(
                    tabRect.Right - closeButtonSize - 5,  // Padding from right
                    tabRect.Top + (tabRect.Height - 16) / 2,     // Vertically center
                    closeButtonSize,
                    closeButtonSize
                );

                bool isSelected = (i == tabContainer.SelectedIndex);
                if (isSelected && closeButton.Contains(e.Location))
                {
                    TabPage tabToRemove = tabContainer.TabPages[i];
                    tabContainer.TabPages.Remove(tabToRemove);
                    break; //Break right after removing
                }
            }
            return;
        }

        private async void Layout_Load(object sender, EventArgs e)
        {
            this.Enabled = false;
            Sidebar.Enabled = false;

            Login login = new Login();
            if (DialogResult.OK == login.ShowDialog())
            {
                lbl_name.Text = CacheData.CurrentUser.first_name + " " + CacheData.CurrentUser.last_name;
                lbl_position.Text = CacheData.CurrentUser.position_id;
                lbl_department.Text = CacheData.CurrentUser.department;
                this.Enabled = true;

                serviceCompanySetup = new GeneralService<CompanySetupModel>(ApiEndPoints.COMPANY_SETUP);
                // GetAsModel() returns null on any failed/errored call (auth failure,
                // no matching row, etc.) - defaulting to an empty instance here means
                // every page that reads CacheData.CompanySetup.* later gets blank
                // fields instead of a NullReferenceException on CompanySetup itself.
                CacheData.CompanySetup = await serviceCompanySetup.GetAsModel() ?? new CompanySetupModel();
                _currencyCode = CacheData.CompanySetup.currency_code;

                serviceJournalSetup = new GeneralService<JournalEntryModel>(ApiEndPoints.CURRENT_JOURNAL);
                // GetCurrentJournal (Go) returns an error - not an empty result - when
                // no journal entry period covers today (confirmed: tbl_accounting_
                // journal_entry has zero rows in the test DB right now), so this comes
                // back null until someone sets one up. Same defensive default as
                // CompanySetup above - every page reading CacheData.CurrentJournal.*
                // (e.g. SalesInvoicePage) was crashing on this being null rather than
                // just having blank journal info.
                CacheData.CurrentJournal = await serviceJournalSetup.GetAsModel() ?? new JournalEntryModel();
                if (string.IsNullOrEmpty(CacheData.CurrentJournal.journal_name))
                {
                    // Helpers.ShowDialogMessage only handles "success"/"error" in this
                    // app (unlike smpc_inventory_app's version, which also has
                    // "warning") - anything else falls through to its own "Unknown
                    // status" dialog. Plain MessageBox.Show here instead of extending
                    // a shared helper other screens also rely on.
                    MessageBox.Show(
                        "No active journal entry period is configured for today. Set one up in Journal Entry before posting transactions that need one.",
                        "SMPC SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                this.Enabled = true;
            }
            else
            {
                Application.Exit();
            }

            await LoadCurrency();

            Sidebar.Enabled = true;

            // Loaded last, after login/currency are confirmed - RedBoxAR lives in
            // the always-visible panel5, so its constructor runs before login
            // resolves; kicking off its first data load here (rather than on its
            // own Load event) avoids hitting the API before a session exists.
            await _redBoxAR.RefreshData();
        }

        private async Task LoadCurrency()
        {
            serviceCurrencyRateSetup = new GeneralService<ExchangeRateModel>(ApiEndPoints.CURRENCY_RATE + "PHP");
            try
            {
                CacheData.CurrencyRate = await serviceCurrencyRateSetup.GetAsModel();

                if (CacheData.CurrencyRate == null)
                {
                    Helpers.ShowDialogMessage("error", "No exchange rate found for currency. Please connect to the internet: " + _currencyCode);

                    // Optional: stop further processing
                    Application.Exit();
                    return;
                }
            }
            catch (Exception ex)
            {
                Helpers.ShowDialogMessage("error", "Error retrieving currency rate.\n\nDetails: " + ex.Message);

                Application.Exit();
                return;
            }
        }
    }
}
