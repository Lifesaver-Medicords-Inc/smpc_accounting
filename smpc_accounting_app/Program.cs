using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Serilog;

namespace smpc_accounting_app
{
    static class Program
    {
        public static string ApiBaseUrl { get; private set; }
        // HRIS GraphQL API base URL. Optional key: null when unconfigured, and only
        // the Employee Information module needs it (HrisApiService guards for null),
        // so a missing key never blocks the rest of the app from starting.
        public static string HrisApiBaseUrl { get; private set; }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Configure Serilog
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("logs\\accounting-logs-.log", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            try
            {
                string env = System.Configuration.ConfigurationManager.AppSettings["Environment"] ?? "Development";

                // Resolve the correct API URL
                ApiBaseUrl = System.Configuration.ConfigurationManager.AppSettings[$"ApiBaseUrl.{env}"]
                             ?? throw new ConfigurationErrorsException($"No API URL configured for environment: {env}");

                HrisApiBaseUrl = System.Configuration.ConfigurationManager.AppSettings[$"HrisApiBaseUrl.{env}"];

                // Global crash guard (mirrors dispatching's Program.cs). The try/catch below
                // only covers STARTUP - once the message loop is pumping, a UI-thread
                // exception (e.g. "Index out of range" on a grid click) bypasses it and hit
                // the raw .NET Continue/Quit dialog. CatchException routes those here so the
                // app keeps running; the full stack is logged and the user sees a clean message.
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                Application.ThreadException += (s, e) =>
                {
                    try { Serilog.Log.Error(e.Exception, "Unhandled UI-thread exception"); } catch { }
                    MessageBox.Show(
                        "Something went wrong and that action could not be completed." + Environment.NewLine + Environment.NewLine
                        + e.Exception.Message + Environment.NewLine + Environment.NewLine
                        + "The app will keep running. Full details were saved to the log.",
                        "Unexpected Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                };
                AppDomain.CurrentDomain.UnhandledException += (s, e) =>
                {
                    var uex = e.ExceptionObject as Exception;
                    try { Serilog.Log.Error(uex, "Unhandled non-UI exception"); } catch { }
                    MessageBox.Show(
                        "A serious error occurred." + Environment.NewLine + Environment.NewLine
                        + (uex?.Message ?? "Unknown error") + Environment.NewLine + Environment.NewLine
                        + "Details were saved to the log.",
                        "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                };

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Layout());
            }
            catch (ConfigurationErrorsException ex)
            {
                Log.Error("Configuration Error: {Exception}", ex.Message);
                MessageBox.Show($"Configuration error: {ex.Message}", "Config Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (NullReferenceException ex)
            {
                Log.Error("Null Reference Error: {Exception}", ex.Message);
                Log.Error("StackTrace: {Exception}", ex.StackTrace);
                MessageBox.Show($"A null reference error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                Log.Error("Exception Message: {Exception}", ex.Message);
                Log.Error("Exception: {Exception}", ex.StackTrace);
                Log.Debug("=============================================");
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
