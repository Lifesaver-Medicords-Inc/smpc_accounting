using smpc_accounting_app.Models.Hris;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;

namespace smpc_accounting_app.Services.Hris
{
    // GDI+ print/preview for the HRIS Payroll reports, same approach as
    // SMPC_Admin's ReportPrinter (hand-drawn Graphics + PrintPreviewDialog,
    // no .rdlc): three documents off one payroll run —
    //   Payslips   : one employee per page, portrait
    //   Register   : the run's items as a landscape table with totals
    //   Remittance : per-employee EE contributions + W/Tax for the calendar
    //                month (sums every run whose period ends in that month)
    // A non-APPROVED run prints with a DRAFT banner. Page unit: 1/100 inch.
    static class HrisPayrollPrinter
    {
        private static readonly Font FontTitle = new Font("Microsoft Sans Serif", 14, FontStyle.Bold);
        private static readonly Font FontHead = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);
        private static readonly Font FontBody = new Font("Microsoft Sans Serif", 9);
        private static readonly Font FontBodyBold = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);
        private static readonly Font FontSmall = new Font("Microsoft Sans Serif", 7);
        private static readonly Font FontSmallBold = new Font("Microsoft Sans Serif", 7, FontStyle.Bold);
        private static readonly Font FontNet = new Font("Microsoft Sans Serif", 11, FontStyle.Bold);

        private static string Money(decimal v) { return v.ToString("#,##0.00"); }

        private static string CompanyName()
        {
            var name = Shared.CacheData.CompanySetup != null ? Shared.CacheData.CompanySetup.company_name : null;
            return string.IsNullOrWhiteSpace(name) ? "SMPC" : name;
        }

        private static string EmployeeLabel(HrisPayrollItemModel item)
        {
            if (item.Employee == null) return "Employee #" + item.EmployeeId;
            return $"{item.Employee.EmployeeNo} — {item.Employee.FirstName} {item.Employee.MiddleName} {item.Employee.LastName}"
                .Replace("  ", " ").Trim();
        }

        private static void ShowPreview(PrintDocument doc, IWin32Window owner)
        {
            using (var preview = new PrintPreviewDialog
            {
                Document = doc,
                Width = 1000,
                Height = 750,
                StartPosition = FormStartPosition.CenterParent
            })
            {
                preview.ShowDialog(owner);
            }
        }

        // ------------------------------------------------------------ payslips

        public static void PreviewPayslips(HrisPayrollRunModel run, IWin32Window owner)
        {
            if (run.Items.Count == 0)
            {
                MessageBox.Show("This payroll run has no items to print.", "Payslips");
                return;
            }
            int index = 0;
            var doc = new PrintDocument { DocumentName = $"Payslips {run.PeriodStart} to {run.PeriodEnd}" };
            doc.PrintPage += (s, e) =>
            {
                DrawPayslip(e.Graphics, e.MarginBounds, run, run.Items[index]);
                index++;
                e.HasMorePages = index < run.Items.Count;
                if (!e.HasMorePages) index = 0; // preview may render the document twice
            };
            ShowPreview(doc, owner);
        }

        private static void DrawPayslip(Graphics g, Rectangle area, HrisPayrollRunModel run, HrisPayrollItemModel item)
        {
            float x = area.Left, right = area.Right, y = area.Top;
            float mid = x + (right - x) / 2f + 20;

            g.DrawString(CompanyName(), FontTitle, Brushes.Black, x, y);
            var slipSize = g.MeasureString("PAYSLIP", FontTitle);
            g.DrawString("PAYSLIP", FontTitle, Brushes.Black, right - slipSize.Width, y);
            y += 30;
            g.DrawString($"Pay Period: {run.PeriodStart} to {run.PeriodEnd}", FontBody, Brushes.Black, x, y);
            if (!string.IsNullOrWhiteSpace(run.PayDate))
            {
                g.DrawString($"Pay Date: {run.PayDate}", FontBody, Brushes.Black, mid, y);
            }
            y += 16;
            if (run.Status != "APPROVED")
            {
                g.DrawString("*** DRAFT — NOT FINAL ***", FontBodyBold, Brushes.Black, x, y);
                y += 16;
            }
            y += 4;
            g.DrawLine(Pens.Black, x, y, right, y);
            y += 10;

            g.DrawString(EmployeeLabel(item), FontHead, Brushes.Black, x, y);
            y += 16;
            string department = item.Employee != null ? item.Employee.Department : "";
            if (!string.IsNullOrWhiteSpace(department))
            {
                g.DrawString("Department: " + department, FontBody, Brushes.Black, x, y);
                y += 16;
            }
            g.DrawString($"Rate: {item.RateType} {Money(item.BasicRate)}   Days: {item.DaysWorked}   Paid Leave: {item.DaysPaidLeave}   Hours: {item.TotalHours:0.##}   OT: {item.OtHours:0.##}   ND: {item.NdHours:0.##}   Late/UT min: {item.LateUtMinutes}",
                FontBody, Brushes.Black, x, y);
            y += 22;

            float leftValueRight = mid - 40;
            float rightValueRight = right;
            float yLeft = y, yRight = y;

            void Left(string label, decimal value, bool bold = false)
            {
                var font = bold ? FontBodyBold : FontBody;
                g.DrawString(label, font, Brushes.Black, x, yLeft);
                var text = Money(value);
                var size = g.MeasureString(text, font);
                g.DrawString(text, font, Brushes.Black, leftValueRight - size.Width, yLeft);
                yLeft += 17;
            }
            void Right(string label, decimal value, bool bold = false)
            {
                var font = bold ? FontBodyBold : FontBody;
                g.DrawString(label, font, Brushes.Black, mid, yRight);
                var text = Money(value);
                var size = g.MeasureString(text, font);
                g.DrawString(text, font, Brushes.Black, rightValueRight - size.Width, yRight);
                yRight += 17;
            }

            g.DrawString("EARNINGS", FontHead, Brushes.Black, x, yLeft);
            yLeft += 18;
            g.DrawString("DEDUCTIONS", FontHead, Brushes.Black, mid, yRight);
            yRight += 18;

            Left("Basic Pay", item.BasicPayAmount);
            Left("Overtime Pay", item.OtPay);
            Left("Night Differential", item.NdPay);
            Left("Holiday Premium", item.HolidayPremiumPay);
            Left("Allowance", item.AllowanceAmount);
            Left("Other Earnings", item.OtherEarnings);
            Right("SSS", item.SssEe);
            Right("PhilHealth", item.PhilhealthEe);
            Right("Pag-IBIG", item.PagibigEe);
            Right("Withholding Tax", item.WithholdingTax);
            Right("Tardiness", item.TardinessDeduction);
            Right("Other Deductions", item.OtherDeductions);

            yLeft += 4;
            Left("GROSS PAY", item.GrossPay, bold: true);
            yRight += 4;
            Right("TOTAL DEDUCTIONS", item.DeductionsTotal, bold: true);

            y = Math.Max(yLeft, yRight) + 12;
            g.DrawLine(Pens.Black, x, y, right, y);
            y += 10;
            g.DrawString("NET PAY:  PHP " + Money(item.NetPay), FontNet, Brushes.Black, x, y);
            y += 26;
            if (!string.IsNullOrWhiteSpace(item.Remarks))
            {
                g.DrawString("Remarks: " + item.Remarks, FontBody, Brushes.Black, x, y);
                y += 18;
            }
            y += 30;
            g.DrawString("Received by: ____________________________          Date: ______________", FontBody, Brushes.Black, x, y);
        }

        // ------------------------------------------------------------ register

        private class Col
        {
            public string Header;
            public float Width;
            public Func<HrisPayrollItemModel, string> Value;
            public Func<HrisPayrollRunModel, string> Total;
            public bool AlignLeft;
        }

        public static void PreviewRegister(HrisPayrollRunModel run, IWin32Window owner)
        {
            if (run.Items.Count == 0)
            {
                MessageBox.Show("This payroll run has no items to print.", "Payroll Register");
                return;
            }

            var cols = new List<Col>
            {
                new Col { Header = "EMPLOYEE", Width = 150, AlignLeft = true, Value = EmployeeLabel, Total = r => "TOTALS (" + r.EmployeeCount + ")" },
                new Col { Header = "BASIC", Width = 68, Value = i => Money(i.BasicPayAmount), Total = r => Money(r.Items.Sum(i => i.BasicPayAmount)) },
                new Col { Header = "OT PAY", Width = 60, Value = i => Money(i.OtPay), Total = r => Money(r.Items.Sum(i => i.OtPay)) },
                new Col { Header = "ND PAY", Width = 60, Value = i => Money(i.NdPay), Total = r => Money(r.Items.Sum(i => i.NdPay)) },
                new Col { Header = "HOL PAY", Width = 62, Value = i => Money(i.HolidayPremiumPay), Total = r => Money(r.Items.Sum(i => i.HolidayPremiumPay)) },
                new Col { Header = "ALLOW", Width = 64, Value = i => Money(i.AllowanceAmount), Total = r => Money(r.Items.Sum(i => i.AllowanceAmount)) },
                new Col { Header = "OTHER", Width = 58, Value = i => Money(i.OtherEarnings), Total = r => Money(r.Items.Sum(i => i.OtherEarnings)) },
                new Col { Header = "GROSS", Width = 72, Value = i => Money(i.GrossPay), Total = r => Money(r.TotalGross) },
                new Col { Header = "SSS", Width = 58, Value = i => Money(i.SssEe), Total = r => Money(r.Items.Sum(i => i.SssEe)) },
                new Col { Header = "PHIC", Width = 60, Value = i => Money(i.PhilhealthEe), Total = r => Money(r.Items.Sum(i => i.PhilhealthEe)) },
                new Col { Header = "HDMF", Width = 54, Value = i => Money(i.PagibigEe), Total = r => Money(r.Items.Sum(i => i.PagibigEe)) },
                new Col { Header = "W/TAX", Width = 62, Value = i => Money(i.WithholdingTax), Total = r => Money(r.Items.Sum(i => i.WithholdingTax)) },
                new Col { Header = "TARDY", Width = 62, Value = i => Money(i.TardinessDeduction), Total = r => Money(r.Items.Sum(i => i.TardinessDeduction)) },
                new Col { Header = "OTH DED", Width = 62, Value = i => Money(i.OtherDeductions), Total = r => Money(r.Items.Sum(i => i.OtherDeductions)) },
                new Col { Header = "NET PAY", Width = 76, Value = i => Money(i.NetPay), Total = r => Money(r.TotalNet) },
            };

            int index = 0;
            var doc = new PrintDocument { DocumentName = $"Payroll Register {run.PeriodStart} to {run.PeriodEnd}" };
            doc.DefaultPageSettings.Landscape = true;
            doc.DefaultPageSettings.Margins = new Margins(40, 40, 50, 50);
            doc.PrintPage += (s, e) =>
            {
                var g = e.Graphics;
                var area = e.MarginBounds;
                float y = area.Top;

                g.DrawString(CompanyName() + " — Payroll Register", FontTitle, Brushes.Black, area.Left, y);
                y += 26;
                string status = run.Status == "APPROVED" ? "APPROVED" : "DRAFT — NOT FINAL";
                g.DrawString($"Period: {run.PeriodStart} to {run.PeriodEnd}    Pay Date: {run.PayDate}    Status: {status}    Amounts in PHP",
                    FontSmall, Brushes.Black, area.Left, y);
                y += 18;

                DrawTableHeader(g, cols, area.Left, ref y);

                while (index < run.Items.Count && y < area.Bottom - 40)
                {
                    DrawTableRow(g, cols, run.Items[index], area.Left, ref y);
                    index++;
                }

                if (index >= run.Items.Count)
                {
                    y += 4;
                    g.DrawLine(Pens.Black, area.Left, y, area.Left + cols.Sum(c => c.Width), y);
                    y += 4;
                    float x = area.Left;
                    foreach (var col in cols)
                    {
                        string text = col.Total(run);
                        DrawCell(g, text, FontSmallBold, x, y, col.Width, col.AlignLeft);
                        x += col.Width;
                    }
                    e.HasMorePages = false;
                    index = 0; // preview may render twice
                }
                else
                {
                    e.HasMorePages = true;
                }
            };
            ShowPreview(doc, owner);
        }

        private static void DrawTableHeader(Graphics g, List<Col> cols, float left, ref float y)
        {
            float x = left;
            foreach (var col in cols)
            {
                DrawCell(g, col.Header, FontSmallBold, x, y, col.Width, col.AlignLeft);
                x += col.Width;
            }
            y += 14;
            g.DrawLine(Pens.Black, left, y, left + cols.Sum(c => c.Width), y);
            y += 4;
        }

        private static void DrawTableRow(Graphics g, List<Col> cols, HrisPayrollItemModel item, float left, ref float y)
        {
            float x = left;
            foreach (var col in cols)
            {
                DrawCell(g, col.Value(item), FontSmall, x, y, col.Width, col.AlignLeft);
                x += col.Width;
            }
            y += 13;
        }

        private static void DrawCell(Graphics g, string text, Font font, float x, float y, float width, bool alignLeft)
        {
            if (string.IsNullOrEmpty(text)) return;
            if (alignLeft)
            {
                var rect = new RectangleF(x, y, width - 4, 13);
                g.DrawString(text, font, Brushes.Black, rect, new StringFormat { Trimming = StringTrimming.EllipsisCharacter, FormatFlags = StringFormatFlags.NoWrap });
            }
            else
            {
                var size = g.MeasureString(text, font);
                g.DrawString(text, font, Brushes.Black, x + width - size.Width - 4, y);
            }
        }

        // ------------------------------------------------------------ remittance

        private class RemitRow
        {
            public string Employee;
            public decimal Sss, Phic, Hdmf, Wtax;
        }

        // Sums EE contributions + W/Tax per employee across EVERY run whose
        // period ends in the same calendar month as the given run (both
        // cutoffs), since contributions land on the second cutoff only.
        public static void PreviewRemittance(HrisPayrollRunModel run, List<HrisPayrollRunModel> allRuns, IWin32Window owner)
        {
            string month = run.PeriodEnd != null && run.PeriodEnd.Length >= 7 ? run.PeriodEnd.Substring(0, 7) : "";
            var monthRuns = allRuns
                .Where(r => r.PeriodEnd != null && r.PeriodEnd.StartsWith(month))
                .OrderBy(r => r.PeriodStart)
                .ToList();

            var rows = monthRuns
                .SelectMany(r => r.Items)
                .GroupBy(i => i.EmployeeId)
                .Select(grp => new RemitRow
                {
                    Employee = EmployeeLabel(grp.First()),
                    Sss = grp.Sum(i => i.SssEe),
                    Phic = grp.Sum(i => i.PhilhealthEe),
                    Hdmf = grp.Sum(i => i.PagibigEe),
                    Wtax = grp.Sum(i => i.WithholdingTax),
                })
                .OrderBy(r => r.Employee)
                .ToList();

            if (rows.Count == 0)
            {
                MessageBox.Show("No payroll items found for month " + month + ".", "Remittance Summary");
                return;
            }

            bool anyDraft = monthRuns.Any(r => r.Status != "APPROVED");
            string periods = string.Join(", ", monthRuns.Select(r => r.PeriodStart + " to " + r.PeriodEnd));

            float[] widths = { 300, 90, 90, 90, 90 };
            string[] headers = { "EMPLOYEE", "SSS EE", "PHILHEALTH EE", "PAG-IBIG EE", "W/TAX" };

            int index = 0;
            var doc = new PrintDocument { DocumentName = "Statutory Remittance Summary " + month };
            doc.PrintPage += (s, e) =>
            {
                var g = e.Graphics;
                var area = e.MarginBounds;
                float y = area.Top;

                g.DrawString(CompanyName() + " — Statutory Remittance Summary", FontTitle, Brushes.Black, area.Left, y);
                y += 26;
                g.DrawString($"Month: {month}    Runs covered: {periods}    Amounts in PHP", FontSmall, Brushes.Black, area.Left, y);
                y += 14;
                g.DrawString("Employee shares only — employer shares are not yet computed by the HRIS."
                    + (anyDraft ? "    *** INCLUDES DRAFT RUNS — NOT FINAL ***" : ""), FontSmall, Brushes.Black, area.Left, y);
                y += 20;

                float x = area.Left;
                for (int c = 0; c < headers.Length; c++)
                {
                    DrawCell(g, headers[c], FontSmallBold, x, y, widths[c], c == 0);
                    x += widths[c];
                }
                y += 14;
                g.DrawLine(Pens.Black, area.Left, y, area.Left + widths.Sum(), y);
                y += 4;

                while (index < rows.Count && y < area.Bottom - 40)
                {
                    var row = rows[index];
                    string[] values = { row.Employee, Money(row.Sss), Money(row.Phic), Money(row.Hdmf), Money(row.Wtax) };
                    x = area.Left;
                    for (int c = 0; c < values.Length; c++)
                    {
                        DrawCell(g, values[c], FontSmall, x, y, widths[c], c == 0);
                        x += widths[c];
                    }
                    y += 13;
                    index++;
                }

                if (index >= rows.Count)
                {
                    y += 4;
                    g.DrawLine(Pens.Black, area.Left, y, area.Left + widths.Sum(), y);
                    y += 4;
                    string[] totals =
                    {
                        "TOTALS (" + rows.Count + " employees)",
                        Money(rows.Sum(r => r.Sss)), Money(rows.Sum(r => r.Phic)),
                        Money(rows.Sum(r => r.Hdmf)), Money(rows.Sum(r => r.Wtax)),
                    };
                    x = area.Left;
                    for (int c = 0; c < totals.Length; c++)
                    {
                        DrawCell(g, totals[c], FontSmallBold, x, y, widths[c], c == 0);
                        x += widths[c];
                    }
                    e.HasMorePages = false;
                    index = 0; // preview may render twice
                }
                else
                {
                    e.HasMorePages = true;
                }
            };
            ShowPreview(doc, owner);
        }
    }
}
