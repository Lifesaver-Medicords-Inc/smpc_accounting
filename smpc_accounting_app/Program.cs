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
