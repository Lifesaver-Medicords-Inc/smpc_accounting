using Microsoft.Reporting.WinForms;
using smpc_accounting_app.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace smpc_accounting_app.Printing
{
    // The Class A house template (spec 2.10): company block, centred title,
    // two-column header, ruled line table, remarks, signature footer. One layout,
    // Printing/HouseTemplate.rdlc, shared by every document and list printed on
    // it - a screen supplies only its content, so the look is set in one place.
    // The same layout file is used by the inventory app.
    //
    // Holds up to 10 columns and 4 signature blocks; a column shows only when it
    // has a header, a signature block only when it has a label.
    public class HouseTemplateReport
    {
        public const int ColumnCount = 10;
        public const int SignatureCount = 4;

        public string Title { get; set; } = "";
        public string Remarks { get; set; } = "";

        public List<KeyValuePair<string, string>> LeftBlock { get; } = new List<KeyValuePair<string, string>>();
        public List<KeyValuePair<string, string>> RightBlock { get; } = new List<KeyValuePair<string, string>>();
        public List<HouseTemplateColumn> Columns { get; } = new List<HouseTemplateColumn>();
        public List<string[]> Rows { get; } = new List<string[]>();
        public List<KeyValuePair<string, string>> Signatures { get; } = new List<KeyValuePair<string, string>>();

        public string ReportPath =>
            Path.Combine(Application.StartupPath, "Printing", "HouseTemplate.rdlc");

        // Opens the preview. Every print on this template goes through here.
        public void ShowPreview()
        {
            using (var preview = new PrintPreview(ReportPath, DataSources(), Parameters()))
            {
                preview.ShowDialog();
            }
        }

        // Renders the report to a file's bytes without opening the preview.
        // "EXCELOPENXML" is .xlsx, "PDF" is PDF, "WORDOPENXML" is .docx.
        //
        // Only Petty Cash calls this with Excel: 2.10.2 forbids Excel on documents
        // and grants that one exception, so the caller - not this class - decides
        // which formats a screen offers.
        public byte[] Render(string format)
        {
            var report = new LocalReport { ReportPath = ReportPath };
            report.EnableExternalImages = false;

            foreach (var source in DataSources())
                report.DataSources.Add(source);
            report.SetParameters(Parameters());

            string mimeType, encoding, extension;
            string[] streams;
            Warning[] warnings;
            return report.Render(format, null, out mimeType, out encoding, out extension, out streams, out warnings);
        }

        public List<ReportDataSource> DataSources()
        {
            var table = new DataTable("Lines");
            for (int i = 1; i <= ColumnCount; i++)
                table.Columns.Add("C" + i, typeof(string));

            foreach (var row in Rows)
            {
                var r = table.NewRow();
                for (int i = 0; i < ColumnCount; i++)
                    r[i] = row != null && i < row.Length ? (row[i] ?? "") : "";
                table.Rows.Add(r);
            }

            return new List<ReportDataSource> { new ReportDataSource("Lines", table) };
        }

        public List<ReportParameter> Parameters()
        {
            if (Columns.Count > ColumnCount)
                throw new InvalidOperationException("The house template holds at most " + ColumnCount + " columns.");
            if (Signatures.Count > SignatureCount)
                throw new InvalidOperationException("The house template holds at most " + SignatureCount + " signatures.");

            // The company name comes from Company Setup when it is filled in. Company
            // Setup holds no address or contact fields, so those are the same constants
            // the Purchase Order print carries.
            var company = CacheData.CompanySetup;
            var list = new List<ReportParameter>
            {
                Param("CompanyName", Value(company?.company_name, "SUNSHINE MULTI PLUS CORPORATION")),
                Param("CompanyAddress", "644 RAJA MATANDA STREET, TONDO, MANILA PHILIPPINES 1012"),
                Param("CompanyContact", "Tel #: +63282477015 to 17 Fax #: +63282450287"),
                Param("Title", Title),
                Param("LeftBlock", Block(LeftBlock)),
                Param("RightBlock", Block(RightBlock)),
                Param("Remarks", Remarks),
            };

            string align = "";
            for (int i = 0; i < ColumnCount; i++)
            {
                HouseTemplateColumn col = i < Columns.Count ? Columns[i] : null;
                list.Add(Param("H" + (i + 1), col?.Header));
                align += col == null ? 'L' : col.Align;
            }
            list.Add(Param("Align", align));

            for (int i = 0; i < SignatureCount; i++)
            {
                bool has = i < Signatures.Count;
                list.Add(Param("Sig" + (i + 1) + "Label", has ? Signatures[i].Key : ""));
                list.Add(Param("Sig" + (i + 1) + "Name", has ? Signatures[i].Value : ""));
            }

            return list;
        }

        // A list print of exactly what the user sees: the grid's visible columns,
        // in their displayed order, with the rows as displayed (so a search that
        // narrowed the grid prints narrowed too).
        public static HouseTemplateReport FromGrid(DataGridView grid, string title)
        {
            var report = new HouseTemplateReport { Title = title };
            if (grid == null) return report;

            var columns = grid.Columns.Cast<DataGridViewColumn>()
                              .Where(c => c.Visible)
                              .OrderBy(c => c.DisplayIndex)
                              .Take(ColumnCount)
                              .ToList();

            foreach (var c in columns)
            {
                bool numeric = c.ValueType == typeof(decimal) || c.ValueType == typeof(double)
                            || c.ValueType == typeof(float) || c.ValueType == typeof(int)
                            || c.ValueType == typeof(long);
                report.Columns.Add(new HouseTemplateColumn(
                    string.IsNullOrWhiteSpace(c.HeaderText) ? c.Name : c.HeaderText, numeric ? 'R' : 'L'));
            }

            foreach (DataGridViewRow row in grid.Rows)
            {
                if (row.IsNewRow) continue;
                report.Rows.Add(columns.Select(c => row.Cells[c.Index].FormattedValue?.ToString() ?? "").ToArray());
            }

            return report;
        }

        public static KeyValuePair<string, string> Pair(string label, string value)
        {
            return new KeyValuePair<string, string>(label, value ?? "");
        }

        private static string Value(string fromSetup, string fallback)
        {
            return string.IsNullOrWhiteSpace(fromSetup) ? fallback : fromSetup.Trim();
        }

        private static ReportParameter Param(string name, string value)
        {
            return new ReportParameter(name, value ?? "");
        }

        private static string Block(IEnumerable<KeyValuePair<string, string>> lines)
        {
            return string.Join(Environment.NewLine, lines.Select(l => l.Key + ": " + (l.Value ?? "")));
        }
    }

    public class HouseTemplateColumn
    {
        public string Header { get; }

        // 'L', 'R' (numbers) or 'C'.
        public char Align { get; }

        public HouseTemplateColumn(string header, char align = 'L')
        {
            Header = header ?? "";
            Align = align;
        }
    }
}
