using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using smpc_accounting_app.Services.Helpers;
using smpc_accounting_app.Shared;
using smpc_accounting_app.Services.Setup;

namespace smpc_accounting_app.Pages.Components
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Phase 4.6 (UI uniformity): required-field validation and the server's own
        // failure message (already modeled here via ApiResponseModel<T>.message - see
        // its comment - but never actually read until now), matching the other 5 apps'
        // Login.
        private async void btn_login_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_employee_id.Text))
            {
                Helpers.ShowDialogMessage("error", "Employee ID is required.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txt_password.Text))
            {
                Helpers.ShowDialogMessage("error", "Password is required.");
                return;
            }

            var data = Helpers.GetControlsValues(pnl_auth);
            data.Add("motherboard_serial_no", Helpers.GetSerialNumber());
            data.Add("machine_name", Environment.MachineName);

            // The standard loading screen (spec 2.1) stays over the login until signing in
            // finishes. It also blocks a second click from signing in twice.
            Helpers.Loading.ShowLoading(this);
            try
            {
                var currentUser = await AuthServices.Login(data);

                // Null-checked: an unreachable API comes back with no response at all, which
                // used to throw here instead of saying the login failed.
                if (currentUser != null && currentUser.success)
                {
                    CacheData.CurrentUser = currentUser.data;
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    string serverMessage = currentUser?.message;
                    Helpers.ShowDialogMessage("error", string.IsNullOrWhiteSpace(serverMessage) ? "Invalid Credentials" : serverMessage);
                }
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex, "Login failed");
                Helpers.ShowDialogMessage("error", "Something went wrong. Please try again.");
            }
            finally
            {
                Helpers.Loading.HideLoading(this);
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            //txt_employee_id.Text = "PURCH-PO-8";
            //txt_password.Text = "PURCH-PO-8";
        }
    }
}
