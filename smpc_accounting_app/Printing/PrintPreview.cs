using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace smpc_accounting_app.Printing
{
    public partial class PrintPreview : Form
    {
        public PrintPreview(string reportPath, List<ReportDataSource> dataSources, List<ReportParameter> parameters = null)
        {
            InitializeComponent();

            reportViewer1.LocalReport.ReportPath = reportPath;
            reportViewer1.LocalReport.DataSources.Clear();

            // Add datasets
            foreach (var ds in dataSources)
            {
                reportViewer1.LocalReport.DataSources.Add(ds);
            }

            // Add parameters
            if (parameters != null && parameters.Count > 0)
            {
                reportViewer1.LocalReport.SetParameters(parameters);
            }

            // Set default zoom to Page Width
            reportViewer1.ZoomMode = ZoomMode.PageWidth;

            reportViewer1.RefreshReport();
        }
    }
}
