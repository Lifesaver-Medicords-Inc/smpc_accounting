using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Data; 
using System.Data.SqlTypes;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Globalization;

namespace smpc_accounting_app.Services.Helpers
{
    public static class Helpers
    {
        public static decimal ZeroIfNearZero(decimal value, decimal tolerance = 0.01m)
        {
            return (value > -tolerance && value < tolerance) ? 0m : value;
        }

        public static void EnableGroupHeaders(DataGridView dgv, Dictionary<string, string[]> columnGroups)
        {
            if (dgv == null || columnGroups == null || columnGroups.Count == 0)
                return;

            // Double buffer to reduce flickering
            typeof(DataGridView).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.SetProperty,
                null, dgv, new object[] { true });

            // Redraw on scroll/resize
            dgv.Scroll += (s, e) => dgv.Invalidate();
            dgv.ColumnWidthChanged += (s, e) => dgv.Invalidate();

            // Paint group headers
            dgv.Paint += (s, e) => DrawGroupHeaders(dgv, e, columnGroups);

            // Override column header painting
            dgv.CellPainting += (s, e) => DrawGroupedHeaderCells(dgv, e);
        }

        private static void DrawGroupHeaders(DataGridView dgv, PaintEventArgs e, Dictionary<string, string[]> groups)
        {
            foreach (var group in groups)
            {
                string groupName = group.Key;
                string[] cols = group.Value;

                if (!cols.All(c => dgv.Columns.Contains(c)))
                    continue;

                DataGridViewColumn firstCol = dgv.Columns[cols.First()];
                DataGridViewColumn lastCol = dgv.Columns[cols.Last()];

                Rectangle r1 = dgv.GetCellDisplayRectangle(firstCol.Index, -1, true);
                Rectangle r2 = dgv.GetCellDisplayRectangle(lastCol.Index, -1, true);

                if (r1.IsEmpty || r2.IsEmpty) continue;

                Rectangle headerRect = new Rectangle(r1.X, r1.Y, r2.Right - r1.X, r1.Height / 2);

                using (Brush b = new SolidBrush(SystemColors.Control))
                    e.Graphics.FillRectangle(b, headerRect);

                e.Graphics.DrawRectangle(Pens.Gray, headerRect);

                TextRenderer.DrawText(e.Graphics, groupName,
                    dgv.ColumnHeadersDefaultCellStyle.Font,
                    headerRect, Color.Black,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
        }

        private static void DrawGroupedHeaderCells(DataGridView dgv, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);

                Rectangle fullRect = e.CellBounds;

                // Bottom half for column text
                Rectangle textRect = fullRect;
                textRect.Y += textRect.Height / 2;
                textRect.Height /= 2;

                TextRenderer.DrawText(e.Graphics,
                    e.FormattedValue?.ToString() ?? "",
                    e.CellStyle.Font, textRect,
                    e.CellStyle.ForeColor,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

                e.Handled = true;
            }
        }

        public static class DataGridViewFormatter
        {
            private static readonly Dictionary<DataGridView, string[]> _moneyColumns
                = new Dictionary<DataGridView, string[]>();

            // Re-entrancy guard, per grid. Dgv_CellValueChanged ends by assigning to the
            // very cell whose change invoked it, and that assignment raises the event
            // again. Where the assigned decimal does not round-trip to an identical
            // stored value - a bound column of a different underlying type, for one -
            // every pass counts as a fresh change and the handler recurses until the
            // stack is gone. StackOverflowException cannot be caught in .NET, so the app
            // dies outright, mid-edit.
            private static readonly HashSet<DataGridView> _formatting
                = new HashSet<DataGridView>();

            public static void DataGridViewDecimalFormat(DataGridView dgv, IEnumerable<string> moneyColumns)
            {
                if (dgv == null) return;

                _moneyColumns[dgv] = moneyColumns.ToArray();

                dgv.DataBindingComplete -= Dgv_DataBindingComplete;
                dgv.DataBindingComplete += Dgv_DataBindingComplete;

                dgv.CellValueChanged -= Dgv_CellValueChanged;
                dgv.CellValueChanged += Dgv_CellValueChanged;

                dgv.CurrentCellDirtyStateChanged -= Dgv_CurrentCellDirtyStateChanged;
                dgv.CurrentCellDirtyStateChanged += Dgv_CurrentCellDirtyStateChanged;
            }

            private static void Dgv_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
            {
                var dgv = sender as DataGridView;
                if (dgv == null || !_moneyColumns.ContainsKey(dgv)) return;

                var cols = _moneyColumns[dgv];

                foreach (var colName in cols)
                {
                    if (!dgv.Columns.Contains(colName)) continue;

                    dgv.Columns[colName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgv.Columns[colName].DefaultCellStyle.Format = "C2";
                    dgv.Columns[colName].DefaultCellStyle.FormatProvider = CultureInfo.GetCultureInfo("en-PH");
                }
            }

            private static void Dgv_CurrentCellDirtyStateChanged(object sender, EventArgs e)
            {
                var dgv = sender as DataGridView;
                if (dgv?.IsCurrentCellDirty == true)
                    dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }

            private static void Dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
            {
                var dgv = sender as DataGridView;
                if (dgv == null || e.RowIndex < 0 || e.ColumnIndex < 0) return;
                if (!_moneyColumns.ContainsKey(dgv)) return;

                var cols = _moneyColumns[dgv];
                var columnName = dgv.Columns[e.ColumnIndex].Name;

                if (!cols.Contains(columnName)) return;

                var cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (cell.Value == null) return;

                if (_formatting.Contains(dgv)) return;

                string rawValue = cell.Value.ToString().Trim();

                if (!decimal.TryParse(rawValue, NumberStyles.Currency,
                    CultureInfo.GetCultureInfo("en-PH"), out decimal moneyValue))
                {
                    var cleaned = rawValue.Replace("₱", "").Replace(",", "").Trim();

                    if (!decimal.TryParse(cleaned, NumberStyles.Number | NumberStyles.AllowDecimalPoint,
                        CultureInfo.InvariantCulture, out moneyValue))
                        moneyValue = 0m;
                }

                // Already the parsed decimal - writing it back would change nothing but
                // still risk another pass. This alone handles the common case; the guard
                // below covers the rest.
                if (cell.Value is decimal current && current == moneyValue) return;

                _formatting.Add(dgv);
                try
                {
                    cell.Value = moneyValue;
                }
                finally
                {
                    _formatting.Remove(dgv);
                }
            }
        }

        public static class DataGridViewDocumentFormatter
        {
            private static readonly Dictionary<DataGridView, Tuple<string, string, int>> _docConfigs
                = new Dictionary<DataGridView, Tuple<string, string, int>>();

            public static void DataGridViewDocumentFormat(DataGridView dgv, string columnName, string prefix, int digits = 8)
            {
                if (dgv == null) return;

                _docConfigs[dgv] = new Tuple<string, string, int>(columnName, prefix, digits);

                dgv.DataBindingComplete -= Dgv_DataBindingComplete;
                dgv.DataBindingComplete += Dgv_DataBindingComplete;

                dgv.CellFormatting -= Dgv_CellFormatting;
                dgv.CellFormatting += Dgv_CellFormatting;
            }

            private static void Dgv_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
            {
                var dgv = sender as DataGridView;
                if (dgv == null || !_docConfigs.ContainsKey(dgv)) return;

                var tag = _docConfigs[dgv];

                if (!dgv.Columns.Contains(tag.Item1)) return;

                dgv.Columns[tag.Item1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }

            private static void Dgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
            {
                var dgv = sender as DataGridView;
                if (dgv == null || !_docConfigs.ContainsKey(dgv)) return;

                var tag = _docConfigs[dgv];

                string columnName = tag.Item1;
                string prefix = tag.Item2;
                int digits = tag.Item3;

                if (dgv.Columns[e.ColumnIndex].Name != columnName) return;
                if (e.Value == null) return;

                if (int.TryParse(e.Value.ToString(), out int number))
                {
                    e.Value = prefix + number.ToString($"D{digits}");
                    e.FormattingApplied = true;
                }
            }
        }

        public static class TextboxFormatter
        {
            public static void TextboxDecimalFormat(IEnumerable<TextBox> textBoxes)
            {
                foreach (var txt in textBoxes)
                {
                    txt.TextChanged -= TextBox_TextChanged;
                    txt.TextChanged += TextBox_TextChanged;
                }
            }

            private static void TextBox_TextChanged(object sender, EventArgs e)
            {
                TextBox txt = sender as TextBox;
                if (txt == null)
                    return;

                // If the user is typing (has focus), skip formatting
                if (txt.Focused)
                    return;

                if (string.IsNullOrWhiteSpace(txt.Text))
                    return;

                decimal value;
                if (decimal.TryParse(txt.Text, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out value))
                {
                    txt.TextChanged -= TextBox_TextChanged; // prevent recursion

                    // Bug #212 (Trello): a MONEY-tagged field (e.g. Sales Invoice's
                    // txt_pwd_discount) went through this same reformatter on every
                    // blur, which used to always write a bare number back - undoing
                    // whatever peso-sign currency formatting the field's own MONEY
                    // handling elsewhere had applied. Every other (non-MONEY) caller
                    // of TextboxDecimalFormat keeps its previous plain-number behavior.
                    bool isMoney = txt.Tag?.ToString()?.IndexOf("MONEY", StringComparison.OrdinalIgnoreCase) >= 0;
                    if (isMoney)
                    {
                        txt.Text = value.ToString("C2", CultureInfo.GetCultureInfo("en-PH"));
                        txt.AccessibleDescription = value.ToString();
                    }
                    else
                    {
                        txt.Text = value.ToString();
                    }

                    txt.TextChanged += TextBox_TextChanged;
                }
            }
        }

        public static void AddEmbeddedControl(this ListView lv, Control c, int col, int row)
        {
            Rectangle r = lv.GetSubItemBounds(row, col);
            c.Bounds = r;
            lv.Controls.Add(c);
        }

        public static Rectangle GetSubItemBounds(this ListView lv, int row, int col)
        {
            Rectangle r = lv.Items[row].Bounds;
            int left = r.Left;

            for (int i = 0; i < col; i++)
                left += lv.Columns[i].Width;

            return new Rectangle(left, r.Top, lv.Columns[col].Width, r.Height);
        }

        public static TextBox CreateSearchBox(string placeholderText, EventHandler onTextChanged)
        {
            TextBox txtSearch = new TextBox
            {
                Name = "txt_search",
                Dock = DockStyle.Top,
                ForeColor = Color.Gray,
                Text = placeholderText
            };

            // Event handlers
            txtSearch.Enter += (s, e) =>
            {
                if (txtSearch.Text == placeholderText)
                {
                    txtSearch.Text = "";
                    txtSearch.ForeColor = Color.Black;
                }
            };

            txtSearch.Leave += (s, e) =>
            {
                if (string.IsNullOrEmpty(txtSearch.Text))
                {
                    txtSearch.Text = placeholderText;
                    txtSearch.ForeColor = Color.Gray;
                }
            };

            if (onTextChanged != null)
                txtSearch.TextChanged += onTextChanged;

            return txtSearch;
        }

        public static void SetChildControlsEnabled(Control[] parents, bool readOnly, string[] excludeNames)
        {
            foreach (Control parent in parents)
            {
                foreach (Control control in parent.Controls)
                {
                    // Skip excluded controls
                    if (excludeNames != null && excludeNames.Contains(control.Name))
                        continue;

                    if (control is TextBox textBox)
                    {
                        textBox.ReadOnly = readOnly;
                        textBox.BackColor = readOnly ? Color.FromArgb(235, 235, 235) : Color.White;
                    }
                    else if (control is ComboBox comboBox)
                    {
                        comboBox.Enabled = !readOnly;
                        comboBox.DropDownStyle = readOnly ? ComboBoxStyle.DropDown : ComboBoxStyle.DropDownList;
                        comboBox.BackColor = readOnly ? Color.FromArgb(235, 235, 235) : Color.White;
                    }
                    else if (control is DateTimePicker datePicker)
                        datePicker.Enabled = !readOnly; // No true ReadOnly, fallback behavior

                    else if (control is CheckBox checkBox)
                        checkBox.Enabled = !readOnly; // Prevent user from changing value

                    // Recurse into child containers
                    if (control.HasChildren)
                        SetChildControlsEnabled(new Control[] { control }, readOnly, excludeNames);
                }
            }
        }

        public static void SetChildControlsEnabledInclude(Control[] parents, bool readOnly, string[] includeNames)
        {
            foreach (Control parent in parents)
            {
                foreach (Control control in parent.Controls)
                {
                    bool shouldAffect = includeNames == null || includeNames.Contains(control.Name);

                    if (shouldAffect)
                    {
                        if (control is TextBox textBox)
                        {
                            textBox.ReadOnly = readOnly;
                            textBox.BackColor = readOnly ? Color.FromArgb(235, 235, 235) : Color.White;
                        }

                        else if (control is ComboBox comboBox)
                        {
                            comboBox.Enabled = !readOnly;
                            comboBox.DropDownStyle = readOnly ? ComboBoxStyle.DropDown : ComboBoxStyle.DropDownList;
                            comboBox.BackColor = readOnly ? Color.FromArgb(235, 235, 235) : Color.White;
                        }

                        else if (control is DateTimePicker datePicker)
                            datePicker.Enabled = !readOnly;

                        else if (control is CheckBox checkBox)
                            checkBox.AutoCheck = !readOnly;
                    }

                    // Recurse into child containers
                    if (control.HasChildren)
                        SetChildControlsEnabledInclude(new Control[] { control }, readOnly, includeNames);
                }
            }
        }

        public static DataTable ToDataTable<T>(List<T> items)
        {
            var dataTable = new DataTable(typeof(T).Name);

            // Get all properties of T
            var props = typeof(T).GetProperties();

            foreach (var prop in props)
            {
                dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            foreach (var item in items)
            {
                var values = new object[props.Length];
                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }

            return dataTable;
        }

        public static void HandleNumericColumns(DataGridView dgv, DataGridViewEditingControlShowingEventArgs e, string[] numericColumnNames, params char[] extraAllowedChars)
        {
            if (dgv.CurrentCell == null)
                return;

            string columnName = dgv.Columns[dgv.CurrentCell.ColumnIndex].Name;

            // Always detach first
            e.Control.KeyPress -= NumericColumn_KeyPress;

            if (numericColumnNames.Contains(columnName))
            {
                // Pass allowed characters via Tag
                if (e.Control is TextBox tb)
                {
                    tb.Tag = extraAllowedChars;
                }

                e.Control.KeyPress += NumericColumn_KeyPress;
            }
        }

        private static void NumericColumn_KeyPress(object sender, KeyPressEventArgs e)
        {
            var tb = sender as TextBox;
            var extraAllowedChars = tb?.Tag as char[];

            // Allow control keys
            if (char.IsControl(e.KeyChar))
                return;

            // Allow digits
            if (char.IsDigit(e.KeyChar))
                return;

            // Allow decimal point (only once)
            if (e.KeyChar == '.' && tb != null && !tb.Text.Contains("."))
                return;

            // Allow extra characters
            if (extraAllowedChars != null &&
                extraAllowedChars.Contains(e.KeyChar))
                return;

            // Block everything else
            e.Handled = true;
        }

        public static async Task<bool> ValidateDataGridViewCells(DataGridView dgv, string[] columnsToCheck, bool showError = true)
        {
            bool hasError = false;
            List<DataGridViewCell> invalidCells = new List<DataGridViewCell>();

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.IsNewRow) continue;

                foreach (string colName in columnsToCheck)
                {
                    if (!dgv.Columns.Contains(colName))
                        continue;

                    var cell = row.Cells[colName];
                    string value = cell?.Value?.ToString()?.Trim();

                    bool isEmpty = string.IsNullOrEmpty(value);
                    bool isZero = false;

                    if (decimal.TryParse(value, out decimal numericValue))
                        isZero = numericValue == 0;

                    if (isEmpty || isZero)
                    {
                        hasError = true;
                        invalidCells.Add(cell);
                        cell.Style.BackColor = Color.Red;
                    }
                }
            }

            if (hasError)
            {
                if (showError)
                    ShowDialogMessage("error", "Please ensure all required fields are filled.");

                // Wait 3 seconds before resetting color
                await Task.Delay(3000);

                foreach (var cell in invalidCells)
                {
                    cell.Style.BackColor = Color.White;
                }
            }

            return hasError;
        }

        /// <summary>
        /// Set placeholder text (cue banner) in a TextBox.
        /// </summary>
        /// 
        public static class Placeholder
        {
            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, string lParam);

            private const int EM_SETCUEBANNER = 0x1501;

            public static void SetPlaceholder(TextBox textBox, string placeholder)
            {
                if (textBox == null) throw new ArgumentNullException(nameof(textBox));

                // If handle already exists, set immediately
                if (textBox.IsHandleCreated)
                {
                    SendMessage(textBox.Handle, EM_SETCUEBANNER, 0, placeholder);
                }
                else
                {
                    // If not, wait for handle creation
                    textBox.HandleCreated += (s, e) =>
                    {
                        SendMessage(textBox.Handle, EM_SETCUEBANNER, 0, placeholder);
                    };
                }
            }
        }

        public static class NumericTextBox
        {
            private static Dictionary<TextBox, char[]> allowedCharsMap = new Dictionary<TextBox, char[]>();

            public static void HandleNumericTextBox(params TextBox[] textBoxes)
            {
                HandleNumericTextBox(textBoxes, null);
            }

            public static void HandleNumericTextBox(TextBox[] textBoxes, params char[] extraAllowedChars)
            {
                if (textBoxes == null) return;

                foreach (var textBox in textBoxes)
                {
                    if (textBox == null) continue;

                    textBox.KeyPress -= NumericTextBox_KeyPress;
                    allowedCharsMap[textBox] = extraAllowedChars; // store separately
                    textBox.KeyPress += NumericTextBox_KeyPress;
                }
            }

            private static void NumericTextBox_KeyPress(object sender, KeyPressEventArgs e)
            {
                var tb = sender as TextBox;
                if (tb == null) return;

                allowedCharsMap.TryGetValue(tb, out char[] extraAllowedChars);

                if (char.IsControl(e.KeyChar) || char.IsDigit(e.KeyChar))
                    return;

                if (e.KeyChar == '.' && !tb.Text.Contains('.'))
                    return;

                if (extraAllowedChars != null && extraAllowedChars.Contains(e.KeyChar))
                    return;

                e.Handled = true;
            }
        }

        public static void AllowOnlyNumbers(TextBox txt, params char[] extraAllowedChars)
        {
            txt.KeyPress += (s, e) =>
            {
                char ch = e.KeyChar;

                // Allow control keys (Backspace, Delete, Enter, Arrows, etc.)
                if (char.IsControl(ch))
                {
                    e.Handled = false;
                    return;
                }

                // Allow extra characters provided by caller
                if (extraAllowedChars.Contains(ch))
                {
                    e.Handled = false;
                    return;
                }

                // Allow digits only
                if (!char.IsDigit(ch))
                {
                    e.Handled = true;
                }
            };
        }

        public static class DatagridviewMapper
        {
            // Model mapper for DataGridView / DataTable
            public static List<T> BuildModelsFromData<T>(object dataSource) where T : new()
            {
                var models = new List<T>();
                var modelType = typeof(T);
                var properties = modelType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                // --- CASE 1: DataGridView ---
                if (dataSource is DataGridView dgv)
                {
                    if (dgv.Rows.Count == 0)
                        return models;

                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        if (row.IsNewRow)
                            continue;

                        // 🔹 Check if row has ANY data in mapped columns
                        bool rowHasData = false;

                        foreach (var prop in properties)
                        {
                            if (!dgv.Columns.Contains(prop.Name))
                                continue;

                            var cellValue = row.Cells[prop.Name].Value;

                            if (cellValue != null &&
                                !string.IsNullOrWhiteSpace(cellValue.ToString()))
                            {
                                rowHasData = true;
                                break;
                            }
                        }

                        // ⛔ Skip completely empty rows
                        if (!rowHasData)
                            continue;

                        var model = new T();

                        foreach (var prop in properties)
                        {
                            if (!dgv.Columns.Contains(prop.Name))
                                continue;

                            var value = row.Cells[prop.Name].Value;
                            SetModelPropertyValue(model, prop, value);
                        }

                        models.Add(model);
                    }

                    return models;
                }

                // --- CASE 2: DataTable ---
                if (dataSource is DataTable dt)
                {
                    if (dt.Rows.Count == 0)
                        return models;

                    foreach (DataRow dr in dt.Rows)
                    {
                        var model = new T();

                        foreach (var prop in properties)
                        {
                            if (!dt.Columns.Contains(prop.Name))
                                continue;

                            var value = dr[prop.Name];
                            SetModelPropertyValue(model, prop, value);
                        }

                        models.Add(model);
                    }

                    return models;
                }

                return models;
            }

            // Helper method for safe conversion and assignment
            private static void SetModelPropertyValue<T>(
                T model,
                PropertyInfo prop,
                object value)
            {
                if (value == null || value == DBNull.Value)
                    return;

                try
                {
                    object convertedValue = Convert.ChangeType(
                        value,
                        Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType
                    );

                    prop.SetValue(model, convertedValue);
                }
                catch
                {
                    // Intentionally ignored
                }
            }
        }

        // Model mapper for panels
        public static T BuildModelFromPanels<T>(Panel[] panels) where T : new()
        {
            var model = new T();
            var modelType = typeof(T);

            foreach (var prop in modelType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                Control control = null;

                foreach (var panel in panels)
                {
                    control = panel.Controls
                        .Cast<Control>()
                        .FirstOrDefault(c =>
                            c.Name.Equals("txt_" + prop.Name, StringComparison.OrdinalIgnoreCase) ||
                            c.Name.Equals("dtp_" + prop.Name, StringComparison.OrdinalIgnoreCase) ||
                            c.Name.Equals("cmb_" + prop.Name, StringComparison.OrdinalIgnoreCase));

                    if (control != null)
                        break;
                }

                if (control == null)
                    continue;

                object value = null;

                if (control is TextBox textBox)
                {
                    string tag = textBox.Tag?.ToString() ?? "";
                    bool isMoney = tag.IndexOf("MONEY", StringComparison.OrdinalIgnoreCase) >= 0;
                    bool isDocument = tag.IndexOf("DOCUMENT", StringComparison.OrdinalIgnoreCase) >= 0;

                    if (isMoney)
                    {
                        // MONEY: try exact stored value first
                        if (!string.IsNullOrWhiteSpace(textBox.AccessibleDescription) &&
                            decimal.TryParse(textBox.AccessibleDescription, out decimal exactVal))
                        {
                            value = exactVal;
                        }
                        else
                        {
                            // fallback parse formatted currency
                            if (decimal.TryParse(
                                textBox.Text,
                                NumberStyles.Currency,
                                CultureInfo.GetCultureInfo("en-PH"),
                                out decimal parsedDecimal))
                            {
                                value = parsedDecimal;
                            }
                            else
                            {
                                value = 0m;
                            }
                        }
                    }
                    else if (isDocument)
                    {
                        // DOCUMENT: get numeric value from AccessibleDescription
                        if (!string.IsNullOrWhiteSpace(textBox.AccessibleDescription) &&
                            int.TryParse(textBox.AccessibleDescription, out int docVal))
                        {
                            value = docVal;
                        }
                        else
                        {
                            // fallback: remove prefix and parse numeric part
                            string numericPart = new string(textBox.Text.Where(char.IsDigit).ToArray());
                            if (int.TryParse(numericPart, out int fallbackVal))
                                value = fallbackVal;
                            else
                                value = 0;
                        }
                    }
                    else
                    {
                        value = textBox.Text;
                    }
                }
                else if (control is ComboBox comboBox)
                {
                    if (comboBox.Tag?.ToString() == "DYNAMIC")
                        value = comboBox.SelectedValue;
                    else
                        value = comboBox.Text;
                }
                else if (control is DateTimePicker dateTimePicker)
                {
                    value = dateTimePicker.Value.ToString("MM/dd/yyyy");
                }

                if (value != null && prop.CanWrite)
                {
                    try
                    {
                        Type targetType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                        object convertedValue = Convert.ChangeType(value, targetType);
                        prop.SetValue(model, convertedValue);
                    }
                    catch
                    {
                        // Optional: log error
                    }
                }
            }

            return model;
        }

        public static bool ValidateControlsValues(Panel pnl)
        {
            bool isError = false;

            foreach (Control control in pnl.Controls)
            {
                string tag = control.Tag as string;
                if (string.IsNullOrEmpty(tag))
                    continue;

                bool isRequired = tag.IndexOf("REQUIRED", StringComparison.OrdinalIgnoreCase) >= 0;
                bool isMoney = tag.IndexOf("MONEY", StringComparison.OrdinalIgnoreCase) >= 0;

                if (control is TextBox textBox)
                {
                    string value = textBox.Text.Trim();

                    // REQUIRED validation
                    if (isRequired && string.IsNullOrEmpty(value))
                    {
                        FlashRed(textBox);
                        isError = true;
                        continue;
                    }

                    // MONEY validation
                    if (isMoney && !string.IsNullOrEmpty(value))
                    {
                        if (!decimal.TryParse(
                                value,
                                NumberStyles.Currency,
                                CultureInfo.GetCultureInfo("en-PH"),
                                out decimal moneyValue)
                            || moneyValue < 0)
                        {
                            FlashRed(textBox);
                            isError = true;
                            continue;
                        }
                    }

                    textBox.BackColor = Color.FromArgb(235, 235, 235);
                }
                else if (control is ComboBox comboBox)
                {
                    if (isRequired && comboBox.SelectedIndex < 0)
                    {
                        FlashRed(comboBox);
                        isError = true;
                    }
                    else
                    {
                        comboBox.BackColor = Color.FromArgb(235, 235, 235);
                    }
                }
                else if (control is DateTimePicker dtp)
                {
                    if (isRequired)
                    {
                        if (dtp.Value == dtp.MinDate || dtp.Value == default(DateTime))
                        {
                            FlashRed(dtp);
                            isError = true;
                        }
                        else
                        {
                            dtp.CalendarMonthBackground = Color.FromArgb(235, 235, 235);
                            dtp.BackColor = Color.FromArgb(235, 235, 235);
                        }
                    }
                }
            }

            return isError;
        }

        public static bool ValidateControlsValues(params Panel[] panels)
        {
            bool isError = false;

            foreach (var pnl in panels)
            {
                foreach (Control control in pnl.Controls)
                {
                    string tag = control.Tag as string;
                    if (string.IsNullOrEmpty(tag))
                        continue;

                    bool isRequired = tag.IndexOf("REQUIRED", StringComparison.OrdinalIgnoreCase) >= 0;
                    bool isMoney = tag.IndexOf("MONEY", StringComparison.OrdinalIgnoreCase) >= 0;

                    if (control is TextBox textBox)
                    {
                        string value = textBox.Text.Trim();

                        // REQUIRED validation
                        if (isRequired && string.IsNullOrEmpty(value))
                        {
                            FlashRed(textBox);
                            isError = true;
                            continue;
                        }

                        // MONEY validation
                        if (isMoney && !string.IsNullOrEmpty(value))
                        {
                            decimal moneyValue;

                            // Try exact stored value first (if you use AccessibleDescription)
                            if (!string.IsNullOrWhiteSpace(textBox.AccessibleDescription) &&
                                decimal.TryParse(textBox.AccessibleDescription, out moneyValue))
                            {
                                // valid
                            }
                            else if (!decimal.TryParse(
                                        value,
                                        NumberStyles.Currency,
                                        CultureInfo.GetCultureInfo("en-PH"),
                                        out moneyValue))
                            {
                                FlashRed(textBox);
                                isError = true;
                                continue;
                            }

                            if (moneyValue < 0)
                            {
                                FlashRed(textBox);
                                isError = true;
                                continue;
                            }
                        }

                        textBox.BackColor = Color.FromArgb(235, 235, 235);
                    }
                    else if (control is ComboBox comboBox)
                    {
                        if (isRequired && comboBox.SelectedIndex < 0)
                        {
                            FlashRed(comboBox);
                            isError = true;
                        }
                        else
                        {
                            comboBox.BackColor = Color.FromArgb(235, 235, 235);
                        }
                    }
                    else if (control is DateTimePicker dtp)
                    {
                        if (isRequired)
                        {
                            if (dtp.Value == dtp.MinDate || dtp.Value == default(DateTime))
                            {
                                FlashRed(dtp);
                                isError = true;
                            }
                            else
                            {
                                dtp.CalendarMonthBackground = Color.FromArgb(235, 235, 235);
                                dtp.BackColor = Color.FromArgb(235, 235, 235);
                            }
                        }
                    }
                }
            }

            return isError;
        }

        public static void FlashRed(Control control)
        {
            Color originalColor = control.BackColor;
            control.BackColor = Color.Red;

            var timer = new System.Windows.Forms.Timer();
            timer.Interval = 3000; // 3 seconds
            timer.Tick += (s, e) =>
            {
                control.BackColor = originalColor;
                timer.Stop();
                timer.Dispose();
            };
            timer.Start();
        }

        /// <summary>
        /// Show loading overlay inside a DataGridView
        /// </summary>
        // The one standard loading screen (spec 2.1): "Loading... Please wait", it
        // covers the WHOLE PAGE rather than one section, and it clears only once
        // every part of the page has finished loading.
        //
        // Rewritten 2026-09-12. The previous version broke all three of those rules
        // and the bugs were not cosmetic:
        //
        //   - It overlaid whatever control the caller passed - in practice a single
        //     DataGridView - so the grid was covered while the toolbar, the header
        //     fields and the action buttons stayed live. A user could press Save on
        //     a form whose data had not arrived yet.
        //
        //   - One static overlay for the entire app, first-wins: a second load
        //     starting while one was up returned silently, and then the FIRST
        //     HideLoading tore the overlay down while the other load was still
        //     running. A page with two or three fetches cleared early, every time.
        //
        //   - HideLoading disposed and nulled the static field while removing the
        //     panel from whichever control it was handed. Hand it a different
        //     control than Show got - easy, since callers pass grids - and the
        //     overlay is orphaned on screen with nothing left pointing at it.
        //
        // Now: the host is resolved UP to the page that contains it, input to the
        // page is held back for the duration, and shows are REF-COUNTED per
        // page, so the screen lifts on the last completion rather than the first.
        //
        // Callers need no change - every existing call site passes some control on
        // the page, which is exactly what this resolves from.
        public static class Loading
        {
            // Spec 2.1's exact wording. Callers may pass their own message, but the
            // default is the standard one and should stay that way.
            public const string StandardMessage = "Loading... Please wait";

            private class PageOverlay
            {
                public Panel Panel;
                public int Depth;
            }

            // Clicks, keys and wheel turns aimed at anything on a covered page are swallowed
            // here, before the control they were meant for sees them. This used to be done by
            // disabling the page's controls and switching them all back on when the screen
            // cleared - which restored the state from when the screen went UP, so a save that
            // settles the form read-only (spec 2.1) while the screen is up would have had its
            // locked panels switched straight back on. Holding the input back instead leaves
            // every control's Enabled state to the page's own code. The cover itself still takes
            // the mouse: there is nothing on it to press, and a wheel turn over it scrolls the page.
            private class InputBlocker : IMessageFilter
            {
                private const int FirstKeyMessage = 0x0100;   // WM_KEYDOWN
                private const int LastKeyMessage = 0x0109;    // WM_UNICHAR
                private const int FirstMouseMessage = 0x0201; // WM_LBUTTONDOWN - movement passes
                private const int LastMouseMessage = 0x020E;  // WM_MOUSEHWHEEL

                public bool PreFilterMessage(ref Message m)
                {
                    if (Overlays.Count == 0) return false;

                    bool input = (m.Msg >= FirstKeyMessage && m.Msg <= LastKeyMessage)
                        || (m.Msg >= FirstMouseMessage && m.Msg <= LastMouseMessage);
                    if (!input) return false;

                    Control target = Control.FromChildHandle(m.HWnd);
                    for (Control c = target; c != null; c = c.Parent)
                    {
                        PageOverlay record;
                        if (Overlays.TryGetValue(c, out record))
                            return target != record.Panel;
                    }

                    return false;
                }
            }

            private static bool inputBlockerInstalled;

            // Keyed per page, so two pages loading at once do not cancel each other.
            private static readonly Dictionary<Control, PageOverlay> Overlays =
                new Dictionary<Control, PageOverlay>();

            // Resolves the PAGE a control belongs to - the UserControl sitting in the
            // tab - and deliberately stops short of the Form.
            //
            // The first version of this accepted `c is UserControl || c is Form` while
            // walking all the way up, overwriting as it went, so it always finished on
            // the Form: the loading screen covered the entire window, sidebar, red box
            // and tab strip included, instead of the tab's content.
            //
            // Outermost UserControl rather than innermost, because pages are built from
            // nested UserControls (ItemSetUC inside a quotation, BpiBranchUC inside a
            // partner) and covering only the inner one would leave most of the page
            // live. The Form is used only when there is no UserControl above the
            // control at all, which is the modal-dialog case - there the dialog IS the
            // page.
            private static Control ResolvePage(Control control)
            {
                if (control == null) return null;

                Control page = null;
                for (Control c = control; c != null; c = c.Parent)
                {
                    if (c is UserControl) page = c;
                }

                // A dialog, or a control not parented yet - a load kicked off from a
                // constructor, where walking up finds nothing.
                return page ?? control.FindForm() ?? control;
            }

            public static void ShowLoading(Control host, string message = StandardMessage)
            {
                Control page = ResolvePage(host);
                if (page == null || page.IsDisposed) return;

                PageOverlay existing;
                if (Overlays.TryGetValue(page, out existing))
                {
                    // Another fetch on the same page. Count it and leave the screen up.
                    existing.Depth++;
                    return;
                }

                // Deliberately NOT Dock = Fill. A Fill-docked child is added at the end
                // of the Controls collection, which makes it dock FIRST and lays every
                // other docked sibling out in what is left - nothing. The siblings then
                // reflow again when it is removed, and any page whose layout is not
                // perfectly reversible comes back subtly rearranged.
                //
                // Explicit bounds keep the overlay out of dock layout entirely.
                // ClientRectangle rather than DisplayRectangle on purpose - on a
                // scrollable page DisplayRectangle can be larger than the visible area,
                // and a child that big extends the scroll region, which is its own
                // phantom-space bug.
                //
                // No anchors either. Sales Quotation starts loading from its Load event,
                // before its tab has given it its final size, and it scrolls (AutoScroll):
                // an anchored overlay stayed at the size it started with - a grey block over
                // one corner, the message out of sight (user-reported 2026-09-15). The overlay
                // is instead put back over the visible area whenever the page lays out,
                // resizes or scrolls, for as long as it is up (keepOverPage, below).
                var overlay = new Panel
                {
                    // Darker than a plain grey wash so the white box stands out against it.
                    BackColor = Color.FromArgb(150, 40, 40, 40),
                    Name = "pnl_page_loading",
                    Bounds = page.ClientRectangle,
                };

                // Everything is painted by the cover itself, in one pass into an off-screen buffer,
                // and redrawn in full whenever it resizes. The box used to be a child panel of its
                // own and the cover was drawn straight to the screen in two passes - the page behind
                // it, then the grey - with Windows filling the box in as a separate, later step. At
                // the start of every load that showed as a flicker: grey with a hole in it, then the
                // box (user-reported 2026-09-15). Cover, box, ring and message now reach the screen
                // in the same frame.
                typeof(Control).GetMethod("SetStyle", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                    ?.Invoke(overlay, new object[] { ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw, true });

                // A plain box in the middle of the covered page (the page stays covered, spec 2.1):
                // a turning ring above the standard message, moved by a UI-thread timer so it keeps
                // turning while the page awaits the API and stops only if the UI thread itself is
                // busy. Square corners like the rest of the app; the message in the default C# font,
                // bold as a modal's header is (1.4); neutral greys, since red is the attention colour
                // and there is no blue convention. The spec names only the message and the full-page
                // cover, so the ring and the box are a provisional standard, chosen 2026-09-15 - the
                // sliding bar, rounded corners and Segoe UI first tried with them were taken out the
                // same day at the user's request.
                var messageFont = new Font(Control.DefaultFont, FontStyle.Bold);
                var boxSize = new Size(Math.Max(240, TextRenderer.MeasureText(message, messageFont).Width + 64), 108);
                var ink = Color.FromArgb(70, 70, 70);
                var faint = Color.FromArgb(225, 225, 225);
                int angle = 0;

                // Worked out at paint time, so the box stays in the middle as the cover follows the page.
                Func<Rectangle> boxBounds = () => new Rectangle(
                    Math.Max(0, (overlay.ClientSize.Width - boxSize.Width) / 2),
                    Math.Max(0, (overlay.ClientSize.Height - boxSize.Height) / 2),
                    boxSize.Width, boxSize.Height);
                Func<Rectangle> ringBounds = () =>
                {
                    var b = boxBounds();
                    return new Rectangle(b.X + b.Width / 2 - 20, b.Y + 18, 40, 40);
                };

                overlay.Paint += (s, e) =>
                {
                    var g = e.Graphics;

                    var b = boxBounds();
                    using (var fill = new SolidBrush(Color.White))
                        g.FillRectangle(fill, b);

                    // Anti-aliased for the ring only; the box edges are straight.
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    var ring = ringBounds();
                    using (var track = new Pen(faint, 5))
                    using (var arc = new Pen(ink, 5))
                    {
                        arc.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                        arc.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                        g.DrawEllipse(track, ring);
                        g.DrawArc(arc, ring, angle, 100);
                    }

                    TextRenderer.DrawText(g, message, messageFont, new Rectangle(b.X, b.Y + 68, b.Width, 24), ink,
                        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                };

                var spin = new System.Windows.Forms.Timer { Interval = 40 };
                spin.Tick += (s, e) =>
                {
                    angle = (angle + 12) % 360;
                    // Only the ring changes from one frame to the next.
                    var ring = ringBounds();
                    ring.Inflate(6, 6);
                    overlay.Invalidate(ring);
                };
                overlay.Disposed += (s, e) =>
                {
                    spin.Dispose();
                    messageFont.Dispose();
                };
                spin.Start();

                var record = new PageOverlay { Panel = overlay, Depth = 1 };

                // Suspended around the add so the overlay never triggers a layout pass
                // of its own over the page's real controls.
                page.SuspendLayout();
                page.Controls.Add(overlay);
                overlay.BringToFront();
                page.ResumeLayout(false);

                // Painted now rather than whenever the message loop next gets to it. The caller
                // usually carries straight on with work of its own - binding, the first request -
                // and until the cover has painted the page shows through where it should be.
                overlay.Update();

                // Input to the page is held back until the screen clears (InputBlocker). A
                // Cancel pressed during a long save would otherwise throw the save away.
                if (!inputBlockerInstalled)
                {
                    Application.AddMessageFilter(new InputBlocker());
                    inputBlockerInstalled = true;
                }

                // Bounds are written only when they differ, and the overlay is brought forward
                // only when something has been put above it, so the Layout this answers never
                // sets itself off again. Unhooked when HideLoading disposes the overlay.
                EventHandler keepOverPage = (s, e) =>
                {
                    if (overlay.IsDisposed || page.IsDisposed || overlay.Parent != page) return;
                    if (overlay.Bounds != page.ClientRectangle) overlay.Bounds = page.ClientRectangle;
                    if (page.Controls.GetChildIndex(overlay) != 0) overlay.BringToFront();
                };
                LayoutEventHandler keepOnLayout = (s, e) => keepOverPage(s, e);
                ScrollEventHandler keepOnScroll = (s, e) => keepOverPage(s, e);
                var scrollable = page as ScrollableControl;

                page.Layout += keepOnLayout;
                page.ClientSizeChanged += keepOverPage;
                if (scrollable != null) scrollable.Scroll += keepOnScroll;
                // The mouse wheel scrolls a page without raising Scroll, and every kind of scroll
                // carries the overlay along with the content - so it also snaps back when moved.
                overlay.LocationChanged += keepOverPage;
                overlay.Disposed += (s, e) =>
                {
                    page.Layout -= keepOnLayout;
                    page.ClientSizeChanged -= keepOverPage;
                    if (scrollable != null) scrollable.Scroll -= keepOnScroll;
                };

                Overlays[page] = record;
            }

            public static void HideLoading(Control host)
            {
                Control page = ResolvePage(host);
                if (page == null) return;

                PageOverlay record;
                if (!Overlays.TryGetValue(page, out record)) return;

                // Only the last outstanding load clears the screen (spec 2.1).
                record.Depth--;
                if (record.Depth > 0) return;

                Overlays.Remove(page);

                if (!page.IsDisposed)
                {
                    page.SuspendLayout();
                    page.Controls.Remove(record.Panel);
                    page.ResumeLayout(true);
                }

                record.Panel.Dispose();
            }

            // Clears a page's loading screen no matter how many shows are outstanding.
            // For a failure path that has to bail out of several fetches at once - a
            // dropped connection - where matching every Show with a Hide is not
            // practical. Never call it to "make sure" the screen is gone: that is the
            // early-clear bug this class was rewritten to remove.
            public static void ForceHide(Control host)
            {
                Control page = ResolvePage(host);
                if (page == null) return;

                PageOverlay record;
                if (!Overlays.TryGetValue(page, out record)) return;

                record.Depth = 1;
                HideLoading(page);
            }
        }

        // Fits the page in the active tab to the space the window gives it (spec 1.3: responsive,
        // never by squeezing the centre pane). A page is never made smaller than its natural size -
        // its Designer size, or whatever size its own code later gives it - but it is stretched to
        // fill the tab whenever the window has more room than that. Nearly every page is built from
        // docked bands (header Top, body Fill, footer Bottom), so the bands, grids, inner tab
        // controls and right-aligned toolbar buttons follow the new size; fields placed at fixed
        // positions inside a band stay where they are. A page bigger than the window keeps its size:
        // the tab control grows past the window and the panel it sits in scrolls.
        //
        // Before this only the tab control was sized. The page kept its Designer size and sat in
        // the top-left corner of its tab with dead space to the right and below on a large monitor
        // (user-reported 2026-09-15). Proportional Control.Scale() was tried in August 2026 and
        // garbled the pages' AutoSize labels, so pages are stretched, never scaled. Copy-identical
        // in the four apps that open pages in tabs (sales, inventory, engineering, accounting).
        public static class PageFit
        {
            private static readonly Dictionary<Control, Size> NaturalSizes = new Dictionary<Control, Size>();

            // Set while Fit resizes a page, so the page's SizeChanged can tell that apart from the
            // page resizing itself.
            private static bool fitting;

            // Starts watching a page. Safe to call more than once for the same page.
            public static void Track(Control page, Action refit)
            {
                if (page == null || page.IsDisposed || NaturalSizes.ContainsKey(page)) return;

                NaturalSizes[page] = page.Size;

                // Only Fit sizes a tracked page. Anchored to the right or bottom edge it would also
                // stretch on its own, that stretched size would be taken for its natural size, and it
                // could never shrink again; AutoSize would fight the sizes Fit gives it.
                page.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                page.AutoSize = false;

                // A page may resize itself once it is open - Quotation changes its height when it
                // switches between Quick Quote and Project, Orders sets its own width - and the size it
                // chooses becomes the one it is never made smaller than.
                page.SizeChanged += (s, e) =>
                {
                    if (fitting || page.IsDisposed) return;
                    NaturalSizes[page] = page.Size;
                    refit?.Invoke();
                };
                page.Disposed += (s, e) => NaturalSizes.Remove(page);
            }

            // The page showing in the selected tab. Pages replace themselves inside their own tab by
            // adding the next page and hiding or disposing themselves (Opportunities -> Quotation,
            // Quotation -> Sales Order and back), so the one on screen is the last one added.
            public static Control ActivePage(TabControl tabs)
            {
                // SelectedTab can throw while the Designer's SelectedIndex points at a tab that does
                // not exist yet - every fresh launch, before anything is opened - so check first.
                if (tabs == null || tabs.TabPages.Count == 0) return null;
                TabPage selected = tabs.SelectedTab;
                if (selected == null) return null;

                for (int i = selected.Controls.Count - 1; i >= 0; i--)
                {
                    if (!selected.Controls[i].IsDisposed) return selected.Controls[i];
                }
                return null;
            }

            // Sizes the tab control, and the page in its selected tab, to the scrolling panel (host)
            // they sit in. refit is what the host calls to run this again.
            public static void Fit(ScrollableControl host, TabControl tabs, Action refit)
            {
                if (host == null || tabs == null) return;

                Size available = host.ClientSize;
                Control page = ActivePage(tabs);

                // The tab control goes at the top-left of the host's content, not of its visible
                // area. Bounds are client coordinates, which move as the host scrolls: placed at
                // (0, 0) while the host was scrolled down, the tab control landed that far below the
                // start of the content, and when the page then shrank (Sales Quotation, Project back
                // to Quick Quote) the scroll range collapsed and left that distance as empty space
                // above the tab (user-reported 2026-09-15). AutoScrollPosition is where the content's
                // origin currently is.
                Point origin = host.AutoScrollPosition;

                // Nothing open, or a page that docks itself and so looks after its own size.
                if (page == null || page.Dock != DockStyle.None)
                {
                    tabs.Bounds = new Rectangle(origin.X, origin.Y, available.Width, available.Height);
                    return;
                }

                Track(page, refit);
                Size natural;
                if (!NaturalSizes.TryGetValue(page, out natural)) natural = page.Size;

                Padding padding = page.Parent is TabPage tabPage ? tabPage.Padding : Padding.Empty;

                // The tab strip and border around a tab's display area, plus the tab page's padding.
                int chromeWidth = tabs.Width - tabs.DisplayRectangle.Width + padding.Horizontal;
                int chromeHeight = tabs.Height - tabs.DisplayRectangle.Height + padding.Vertical;

                tabs.Bounds = new Rectangle(origin.X, origin.Y,
                    Math.Max(available.Width, natural.Width + chromeWidth),
                    Math.Max(available.Height, natural.Height + chromeHeight));

                Rectangle display = tabs.DisplayRectangle;
                fitting = true;
                try
                {
                    page.Size = new Size(
                        Math.Max(natural.Width, display.Width - padding.Horizontal),
                        Math.Max(natural.Height, display.Height - padding.Vertical));
                }
                finally
                {
                    fitting = false;
                }
            }
        }

        public static void ResetControls(Panel[] pnls)
        {
            foreach (Panel pnl in pnls)
            {
                foreach (Control control in pnl.Controls)
                {
                    // Check if the control is a TextBox
                    if (control is TextBox textBox)
                    {
                        // Reset the TextBox's text
                        textBox.Text = "";
                    }
                    else if (control is ComboBox combobox)
                    {
                        combobox.SelectedIndex = -1;
                    }
                    // Reset DateTimePicker to current date
                    else if (control is DateTimePicker datePicker)
                    {
                        datePicker.Value = DateTime.Now;   // or DateTime.Today
                    }
                }
            }
        }

        public static void SetButtonVisibility(ToolStrip toolStrip, Control parentControl, IEnumerable<string> visibleButtons, IEnumerable<string> hiddenButtons)
        {
            if (toolStrip == null && parentControl == null) return;

            var allControls = new List<Control>();

            if (parentControl != null)
                allControls.AddRange(GetAllControls(parentControl));

            // ToolStrip buttons
            var toolStripButtons = toolStrip?.Items
                .OfType<ToolStripButton>()
                .ToDictionary(b => b.Name, b => b);

            // Show buttons
            foreach (var buttonName in visibleButtons ?? Enumerable.Empty<string>())
            {
                if (toolStripButtons != null && toolStripButtons.TryGetValue(buttonName, out var tsBtn))
                    tsBtn.Visible = true;

                var ctrl = allControls.FirstOrDefault(c => c.Name == buttonName);
                if (ctrl != null)
                    ctrl.Visible = true;
            }

            // Hide buttons
            foreach (var buttonName in hiddenButtons ?? Enumerable.Empty<string>())
            {
                if (toolStripButtons != null && toolStripButtons.TryGetValue(buttonName, out var tsBtn))
                    tsBtn.Visible = false;

                var ctrl = allControls.FirstOrDefault(c => c.Name == buttonName);
                if (ctrl != null)
                    ctrl.Visible = false;
            }
        }

        private static IEnumerable<Control> GetAllControls(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                yield return c;

                foreach (var child in GetAllControls(c))
                    yield return child;
            }
        }

        public static void ResetControls(Panel pnl)
        {
            foreach (Control control in pnl.Controls)
            {
                // Check if the control is a TextBox
                if (control is TextBox textBox)
                {
                    // Reset the TextBox's text
                    textBox.Text = "";
                    textBox.AccessibleDescription = "";
                }
                else if (control is ComboBox combobox)
                {
                    combobox.SelectedIndex = -1;
                }
                // Reset DateTimePicker to current date
                else if (control is DateTimePicker datePicker)
                {
                    datePicker.Value = DateTime.Now;   // or DateTime.Today
                }
            }
        }

        public static Dictionary<string, dynamic> GetControlsValues(Panel pnl)
        {

            Dictionary<string,dynamic> values = new Dictionary<string, dynamic>();

            foreach (Control control in pnl.Controls)
            {
                // Check if the control is a TextBox
                if (control is TextBox textBox)
                {
                    string key = textBox.Name.Replace("txt_", "");
                    string val = "";

                    if (textBox.Tag != null && textBox.Tag.Equals("MONEY"))
                    {

                        val = String.Format("{0}", textBox.Text.ToString().Replace(",", ""));
                    }
                    else
                    {
                        val = String.Format("{0}", textBox.Text.ToString());
                    }
                    values.Add(key, val);
                } 

                // Check if the control is a Combobox
                if (control is ComboBox comboBox)
                {
                    string key = comboBox.Name.Replace("cmb_", "");
                    string val = "";



                    if (comboBox.Tag == "DYNAMIC")
                    {
                        key = key + "_id";
                        values.Add(key, comboBox.SelectedValue);
                    }

                    else
                    {
                        val = comboBox.Text.ToString();

                        values.Add(key, val);
                    }
                }

                // Check if the control is a Checkbox
                if (control is CheckBox checkbox)
                {
                    string key = checkbox.Name.Replace("chk_", "");
                    string val = String.Format("{0}", checkbox.Checked ? 1 : 0);
                    values.Add(key, val);
                }

                // Check if the control is a DATETIME PICKER
                if (control is DateTimePicker dateTimePicker)
                {
                    string key = dateTimePicker.Name.Replace("dtp_", "");
                    string val = String.Format("{0:yyyy-MM-dd}", dateTimePicker.Value);
                    values.Add(key, val);
                }

                // Check if the control is a NUMERIC
                if (control is NumericUpDown numericUpDown)
                {
                    string key = numericUpDown.Name.Replace("txt_", "");
                    string val = String.Format("{0}", numericUpDown.Value);
                    values.Add(key, val);
                }
            }

            return values;
        }
         
        public static Dictionary<string, dynamic> GetControlsValues(Panel pnl1, Panel pnl2)
        { 

            Dictionary<string, dynamic> values = new Dictionary<string, dynamic>();

            foreach (Control control in pnl1.Controls)
            {
                // Check if the control is a TextBox
                if (control is TextBox textBox)
                {
                    string key = textBox.Name.Replace("txt_", "");
                    string val = "";

                    if (textBox.Tag.ToString() == "MONEY")
                    {

                        val = String.Format("{0}", textBox.Text.ToString().Replace(",",""));
                    }
                    else
                    {
                        val = String.Format("'{0}'", textBox.Text.ToString());
                    }
                    values.Add(key, val);
                }

                // Check if the control is a Combobox
                if (control is ComboBox comboBox)
                {
                    string key = comboBox.Name.Replace("cmb_", "");
                    string val = "";
                    if (string.IsNullOrEmpty(comboBox.Text))
                    {
                        val = "";
                    }
                    else
                    {
                        val = String.Format("'{0}'", comboBox.Text.ToString());
                    }
                    values.Add(key, val);
                }

                // Check if the control is a Checkbox
                if (control is CheckBox checkbox)
                {
                    string key = checkbox.Name.Replace("chk_", "");
                    string val = String.Format("{0}", checkbox.Checked ? 1 : 0);
                    values.Add(key, val);
                }

                // Check if the control is a DATETIME PICKER
                if (control is DateTimePicker dateTimePicker)
                {
                    string key = dateTimePicker.Name.Replace("dtp_", "");

                    string val = String.Format("'{0:yyyy-MM-dd}'", dateTimePicker.Value);

                    //string val = String.Format("'{0}'", dateTimePicker.Value);
                    values.Add(key, val);
                }

                // Check if the control is a NUMERIC
                if (control is NumericUpDown numericUpDown)
                {
                    string key = numericUpDown.Name.Replace("txt_", "");
                    string val = String.Format("'{0}'", numericUpDown.Value);
                    values.Add(key, val);
                }
            }

            foreach (Control control in pnl2.Controls)
            {
                // Check if the control is a TextBox
                if (control is TextBox textBox)
                {
                    string key = textBox.Name.Replace("txt_", "");
                    string val = "";

                    if (textBox.Tag.ToString() == "MONEY")
                    {

                        val = String.Format("'{0}'", textBox.Text.ToString().Replace(",", ""));
                    }
                    else
                    {
                        val = String.Format("'{0}'", textBox.Text.ToString().Replace(",", ""));
                    }
                    values.Add(key, val);
                }

                // Check if the control is a Combobox
                if (control is ComboBox comboBox)
                {
                    string key = comboBox.Name.Replace("cmb_", "");
                    string val = "";
                    if (string.IsNullOrEmpty(comboBox.Text))
                    {
                        val = "";
                    }
                    else
                    {
                        val = String.Format("'{0}'", comboBox.Text.ToString());
                    }
                    values.Add(key, val);
                }

                // Check if the control is a Checkbox
                if (control is CheckBox checkbox)
                {
                    string key = checkbox.Name.Replace("chk_", "");
                    string val = String.Format("{0}", checkbox.Checked ? 1 : 0);
                    values.Add(key, val);
                }

                // Check if the control is a DATETIME PICKER
                if (control is DateTimePicker dateTimePicker)
                {
                    string key = dateTimePicker.Name.Replace("dtp_", "");
                    string val = String.Format("'{0}'", dateTimePicker.Value);
                    values.Add(key, val);
                }

                // Check if the control is a NUMERIC
                if (control is NumericUpDown numericUpDown)
                {
                    string key = numericUpDown.Name.Replace("num_", "");
                    string val = String.Format("{0}", numericUpDown.Value);
                    values.Add(key, val);
                }

            }

            return values;
        }

        public static Dictionary<string, dynamic> GetControlsValues(Panel[] pnl1)
        {
            Dictionary<string, dynamic> values = new Dictionary<string, dynamic>();
            foreach (Panel pnl in pnl1)
            {
                foreach (Control control in pnl.Controls)
                {
                    // Check if the control is a TextBox
                    if (control is TextBox textBox)
                    {
                        string key = textBox.Name.Replace("txt_", "");
                        dynamic val = null;
                        if (textBox.Tag != null && textBox.Tag.ToString() == "MONEY")
                        {
                            if (decimal.TryParse(textBox.AccessibleDescription, out decimal exactVal))
                            {
                                val = exactVal;
                            }
                            else
                            {
                                // fallback to parsing cleaned text
                                string isParsed = GetCleanedPriceValue(textBox.Text);
                                if (decimal.TryParse(isParsed, out decimal tempVal))
                                {
                                    val = tempVal;
                                }
                                else
                                {
                                    MessageBox.Show("Invalid money format. Please enter a valid number.");
                                    val = 0.00;
                                }
                            }
                        }
                        else if (textBox.Tag != null && textBox.Tag is List<int> ids && ids.Count > 0)
                        {
                            // Assuming Tag contains a list of IDs (if applicable)
                            values.Add(key + "_id", ids);  // Add the list of IDs under the key + "_id"
                        }
                        else
                        {
                            val = textBox.Text.ToString();
                        }
                        values[key] = val;
                    }
                    if (control is ComboBox comboBox)
                    {
                        string key = comboBox.Name.Replace("cmb_", "");
                        string val = "";
                        if (comboBox.Tag == "DYNAMIC")
                        {
                            key = key + "_id";
                            values.Add(key, comboBox.SelectedValue);
                        }
                        else
                        {
                            val = comboBox.Text.ToString();
                            values.Add(key, val);
                        }
                    }
                    if (control is CheckBox checkbox)
                    {
                        string key = checkbox.Name.Replace("chk_", "");
                        string val = String.Format("{0}", checkbox.Checked ? 1 : 0);
                        values.Add(key, val);
                    }
                    if (control is DateTimePicker dateTimePicker)
                    {
                        string key = dateTimePicker.Name.Replace("dtp_", "");
                        string val = String.Format("{0:yyyy-MM-dd HH:mm:ss}", dateTimePicker.Value);
                        values.Add(key, val);
                    }
                    if (control is NumericUpDown numericUpDown)
                    {
                        string key = numericUpDown.Name.Replace("txt_", "");
                        string val = String.Format("'{0}'", numericUpDown.Value);
                        values.Add(key, val);
                    }
                }
            }
            return values;
        }

        public static void BindControls(Panel[] pnl_list, DataTable dt, int selectedIndex = 0)
        {
            Dictionary<string, dynamic> values = new Dictionary<string, dynamic>();

            foreach (var col_name in dt.Columns)
            {
                foreach (var pnl in pnl_list)
                {
                    foreach (Control control in pnl.Controls)
                    {
                        if (control.Name.Contains(col_name.ToString()))
                        {
                            string column_name = col_name.ToString();
                            Console.WriteLine(column_name);

                            // Check if the control is a TextBox
                            if (control is TextBox textBox && textBox.Name.Replace("txt_", "") == column_name)
                            {
                                string key = textBox.Name.Replace("txt_", "");
                                object rawValue = dt.Rows[selectedIndex][column_name];

                                // MONEY FORMAT
                                if (textBox.Tag?.ToString().Contains("MONEY") == true)
                                {
                                    if (decimal.TryParse(rawValue.ToString(), out decimal moneyVal))
                                    {
                                        textBox.Text = moneyVal.ToString("C2", System.Globalization.CultureInfo.GetCultureInfo("en-PH"));
                                        textBox.AccessibleDescription = moneyVal.ToString(); // Store precise value
                                    }
                                    else
                                    {
                                        textBox.Text = "₱0.00";
                                        textBox.AccessibleDescription = "0";
                                    }
                                }

                                // DOCUMENT FORMAT
                                else if (textBox.Tag?.ToString().StartsWith("DOCUMENT") == true)
                                {
                                    string tag = textBox.Tag.ToString();   // e.g. "DOCUMENTAV REQUIRED"

                                    // Remove "DOCUMENT" and split by space, take first part
                                    string prefix = tag.Substring("DOCUMENT".Length).Split(' ')[0]; // "AV"

                                    if (int.TryParse(rawValue?.ToString(), out int docNumber))
                                    {
                                        textBox.Text = prefix + docNumber.ToString("D8");
                                        textBox.AccessibleDescription = docNumber.ToString(); // Store real value
                                    }
                                    else
                                    {
                                        textBox.Text = prefix + "00000000";
                                        textBox.AccessibleDescription = "0"; // fallback real value
                                    }
                                }

                                // MULTI TAG
                                else if (textBox.Tag is List<int> ids && ids.Count > 0)
                                {
                                    textBox.Text = string.Join(", ", ids);
                                }

                                // DEFAULT
                                else
                                {
                                    if (selectedIndex < 0 || selectedIndex >= dt.Rows.Count)
                                    {
                                        Console.WriteLine("IndexOutOfRangeException selectedIndex");
                                        return;
                                    }

                                    textBox.Text = rawValue?.ToString() ?? "";
                                }
                            }

                            // Check if the control is a Combobox
                            if (control is ComboBox comboBox)
                            {
                                Console.WriteLine($"This is a combobox: {comboBox.Name}");
                                string key = comboBox.Name.Replace("cmb_", "") + "_id";
                                comboBox.BackColor = Color.FromArgb(235, 235, 235);

                                if (comboBox.Tag == "DYNAMIC")
                                {
                                    Console.WriteLine("DYNAMICS:", comboBox.Name);
                                    string rawVal = dt.Rows[selectedIndex][key].ToString();

                                    if (comboBox.DataSource != null)
                                    {
                                        // Items are loaded, bind normally
                                        comboBox.SelectedValue = rawVal;
                                    }
                                    else
                                    {
                                        // Items not loaded yet (view mode) — store for deferred binding
                                        comboBox.AccessibleDescription = rawVal;
                                        comboBox.Text = dt.Rows[selectedIndex][column_name].ToString();
                                    }
                                }
                                else if (comboBox.Tag == "MULTIVALUE")
                                {
                                    string rawValue = dt.Rows[selectedIndex][column_name].ToString();
                                    var multiValues = rawValue.Split(',')
                                        .Select(v => v.Trim())
                                        .Where(v => !string.IsNullOrEmpty(v))
                                        .ToList();

                                    comboBox.Text = multiValues.FirstOrDefault() ?? string.Empty;

                                    foreach (var val in multiValues)
                                        comboBox.Items.Add(val);

                                    if (multiValues.Count > 0)
                                        comboBox.SelectedIndex = 0;
                                }
                                else
                                {
                                    // View mode — no items loaded, just display the text value
                                    string displayValue = dt.Rows[selectedIndex][column_name].ToString();

                                    if (comboBox.Items.Count > 0)
                                    {
                                        // Try to select matching item first
                                        int matchIndex = comboBox.FindStringExact(displayValue);
                                        if (matchIndex >= 0)
                                            comboBox.SelectedIndex = matchIndex;
                                        else
                                            comboBox.Text = displayValue;
                                    }
                                    else
                                    {
                                        // No items — force text display and store raw value
                                        comboBox.DropDownStyle = ComboBoxStyle.DropDown; // must be DropDown to allow free text
                                        comboBox.Text = displayValue;
                                        comboBox.AccessibleDescription = displayValue; // stash for later if needed
                                        comboBox.BackColor = Color.FromArgb(235, 235, 235);
                                    }
                                }
                            }

                            // Check if the control is a Checkbox
                            if (control is CheckBox checkbox)
                            {
                                //to hand outofbound rows
                                if (selectedIndex < 0 || selectedIndex >= dt.Rows.Count)
                                {
                                    Console.WriteLine("IndexOutOfRangeException  ");
                                    return;
                                }
                                string key = checkbox.Name.Replace("chk_", "");
                                checkbox.Checked = (string)dt.Rows[selectedIndex][column_name].ToString() == "1" ||
                                (string)dt.Rows[selectedIndex][column_name].ToString().ToLower() == "true"
                                ? true : false;
                            }
                            // Check if the control is a DATETIME PICKER
                            if (control is DateTimePicker dateTimePicker)
                            {
                                if (selectedIndex < 0 || selectedIndex >= dt.Rows.Count)
                                    return;

                                object rawValue = dt.Rows[selectedIndex][column_name];

                                if (rawValue != DBNull.Value &&
                                    DateTime.TryParse(rawValue.ToString(), out DateTime parsedDate))
                                {
                                    dateTimePicker.Format = DateTimePickerFormat.Custom;
                                    dateTimePicker.CustomFormat = "MM/dd/yyyy";   // your format
                                    dateTimePicker.Value = parsedDate;
                                }
                                else
                                {
                                    // Make it appear empty
                                    dateTimePicker.Format = DateTimePickerFormat.Custom;
                                    dateTimePicker.CustomFormat = " ";
                                }
                            }
                            // Check if the control is a NUMERIC
                            if (control is NumericUpDown numericUpDown)
                            {
                                string key = numericUpDown.Name.Replace("txt_", "");
                                numericUpDown.Text = (string)dt.Rows[selectedIndex][column_name].ToString();
                            }
                        }
                    }
                }
            }
        }

        public static string GetLocalIPAddress()
        {
            string localIP = string.Empty;

            // Get the host name
            string hostName = Dns.GetHostName();

            // Get the list of IP addresses associated with the host
            foreach (var ip in Dns.GetHostAddresses(hostName))
            {
                // Check if it's an IPv4 address
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                    break; // Exit the loop after Getting the first IPv4 address
                }
            }

            return localIP;
        }
        public static string GetSerialNumber()
        {
            try
            {
                string serialNumber = string.Empty;
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard");

                foreach (ManagementObject mo in searcher.Get())
                {
                    serialNumber = mo["SerialNumber"].ToString();
                    break; // Assuming only one motherboard
                }
                return serialNumber;
            }
            catch (Exception ex)
            {
                
                Console.WriteLine("Error: " + ex.Message);
                return "";
            }
        }
        public static void ShowDialogMessage(string status,string message="")
        {
            switch (status)
            {
                case "success":
                    MessageBox.Show(message, "SMPC SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case "error":
                    MessageBox.Show(message, "SMPC SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                default:
                    // Handle unexpected status values
                    MessageBox.Show("Unknown status: " + status, "SMPC SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
            }
        }
        public static void CopyFileTo(string filePath,string destinationPath)
        {
            try
            {
                File.Copy(filePath, destinationPath, true);
            }
            catch (Exception)
            { 
                throw;
            }
        }
        public static DataTable ConvertDataGridViewToDataTable(DataGridView dgv)
        {
            DataTable dataTable = new DataTable();

            // Add columns to DataTable
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                dataTable.Columns.Add(column.Name);
            }

            // Add rows to DataTable
            foreach (DataGridViewRow row in dgv.Rows)
            {
                // Skip the new row placeholder if it's present
                if (!row.IsNewRow)
                {
                    DataRow dataRow = dataTable.NewRow();
                    for (int i = 0; i < dgv.Columns.Count; i++)
                    {
                        dataRow[i] = row.Cells[i].Value;
                    }
                    dataTable.Rows.Add(dataRow);
                }
            }

            return dataTable;
        }
        public static string MoneyFormat(double money)
        {
            return String.Format("{0:N2}", money);
        } 
        public static void GetModalData(TextBox textBox, DataView dataView)
        {
            int recordIndex = 0;
            textBox.Text = "";

            foreach (DataRowView rowView in dataView)
            {

                textBox.Text += recordIndex == 0 ? rowView["name"].ToString() : ", " + rowView["name"].ToString();
                recordIndex++;

            }

        }
        public static DataTable FilterDataTable(DataTable dataTable, string searchTerm, params string[] columnsToSearch)
        {
            if (dataTable == null || columnsToSearch == null || columnsToSearch.Length == 0)
            {
                return dataTable;
            }

            searchTerm = searchTerm?.ToLower() ?? string.Empty;

            var filteredRows = dataTable.AsEnumerable().Where(row =>
                columnsToSearch.Any(column =>
                    row[column]?.ToString().ToLower().Contains(searchTerm) == true));

            return filteredRows.Any() ? filteredRows.CopyToDataTable() : dataTable.Clone();
        }

        public static void GetBPIModalData(TextBox textBox, DataView dataView, int columnIndex)
        {
            if (dataView != null && dataView.Count > 0)
            {
                textBox.Text = dataView[0][columnIndex].ToString();
            }
        }
        public static void SetRowNumber(DataGridView grid, DataGridViewRowPostPaintEventArgs e, int columnIndex = 0)
        {
            if (grid != null && e.RowIndex >= 0 && columnIndex >= 0 && columnIndex < grid.ColumnCount)
            {
                grid.Rows[e.RowIndex].Cells[columnIndex].Value = (e.RowIndex + 1).ToString();
            }
        }
        public static void ClearDataGridView(DataGridView grid)
        {
            if (grid != null && grid.Rows.Count > 0)
            {
                grid.Rows.Clear();
            }
        }
        public static void LoadDirectory(string path, TreeView treeView)
        { 
            // Clear any existing nodes
            treeView.Nodes.Clear();

            // Get the top-level directory and create a root node
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            TreeNode rootNode = new TreeNode(dirInfo.Name);
            treeView.Nodes.Add(rootNode);

            // Load subdirectories and files recursively
            LoadSubdirectoriesAndFiles(rootNode, dirInfo.FullName);
        } 
        private static void LoadSubdirectoriesAndFiles(TreeNode parentNode, string path)
        {
            try
            {
                // Get all subdirectories in the given path
                string[] subdirectories = Directory.GetDirectories(path);

                foreach (string subdirectory in subdirectories)
                {
                    // Create a node for the subdirectory
                    DirectoryInfo dirInfo = new DirectoryInfo(subdirectory);
                    TreeNode subDirNode = new TreeNode(dirInfo.Name);

                    // Add the subdirectory node to the parent node
                    parentNode.Nodes.Add(subDirNode);

                    // Recursively load subdirectories and files into the current subdirectory node
                    LoadSubdirectoriesAndFiles(subDirNode, subdirectory);
                }

                // Get all files in the current directory and add them as leaf nodes
                string[] files = Directory.GetFiles(path);
                foreach (string file in files)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    TreeNode fileNode = new TreeNode(fileInfo.Name);
                    fileNode.Tag = file; // Store the full file path in the Tag property
                    parentNode.Nodes.Add(fileNode); // Add the file node
                }
            }
            catch (UnauthorizedAccessException)
            {
                // Handle access permissions issues if necessary
            }
        }

        public static Dictionary<string, dynamic> MergeDictionaries(params Dictionary<string, dynamic>[] dictionaries)
        {
            var mergedDict = new Dictionary<string, dynamic>();

            foreach (var dict in dictionaries)
            {
                foreach (var kvp in dict)
                {
                    mergedDict[kvp.Key] = kvp.Value;
                }
            }
            return mergedDict;
        }

        public static Dictionary<string, dynamic> ConvertFieldsToDecimal(Dictionary<string, dynamic> data)
        {
            return data.ToDictionary(
                kvp => kvp.Key,
                kvp =>
                {
                    if (kvp.Value == null)
                        return 0.00m;

                    try
                    {
                        return Convert.ToDecimal(kvp.Value);
            }
                    catch
                    {
                        return kvp.Value; // Return original if conversion fails
            }
                }
            );
        }

        public static bool ConvertFieldPropertiesType<T>(Dictionary<string, object> data, string[] keys)
        {
            if (keys == null || keys.Length == 0)
            {
                MessageBox.Show("No keys provided for conversion.");
                return false;
            }

            bool success = true;

            foreach (var key in keys)
            {
                if (!data.ContainsKey(key))
                {
                    MessageBox.Show($"Missing field: {key.Replace("_", " ")}");
                    success = false;
                    continue;
                }

                if (data[key] is T correctTypeValue)
                {
                    data[key] = correctTypeValue; // already valid
                    continue;
                }

                if (!(data[key] is string value))
                {
                    MessageBox.Show($"Invalid value type for {key.Replace("_", " ")}. Expected a string.");
                    success = false;
                    continue;
                }

                value = value.Trim();

                if (string.IsNullOrEmpty(value))
                {
                    MessageBox.Show($"Empty value for {key.Replace("_", " ")}");
                    success = false;
                    continue;
                }

                object converted = null;

                if (typeof(T) == typeof(int) && int.TryParse(value, out var intVal))
                    converted = intVal;
                else if (typeof(T) == typeof(float) && float.TryParse(value, out var floatVal))
                    converted = floatVal;
                else if (typeof(T) == typeof(double) && double.TryParse(value, out var doubleVal))
                    converted = doubleVal;
                else if (typeof(T) == typeof(decimal) && decimal.TryParse(value, out var decVal))
                    converted = decVal;
                else if (typeof(T) == typeof(bool) && bool.TryParse(value, out var boolVal))
                    converted = boolVal;
                else
                {
                    MessageBox.Show($"Invalid format for {key.Replace("_", " ")}");
                    success = false;
                    continue;
                }

                data[key] = converted;
            }

            return success;
        }

        public static string GetCleanedPriceValue(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return "0";
            // Remove currency symbols and thousands separators
            var cleaned = input.Replace("₱", "")
                               .Replace("$", "")
                               .Replace(",", "")
                               .Trim();
            return cleaned;
        }
        public static string FormatAsCurrency(TextBox textbox, decimal value, string currency = "PHP")
        {

            string currencyType = currency == "PHP" ? "en-PH" : "en-US";
            // Format and assign
            textbox.Text = value.ToString("C2", System.Globalization.CultureInfo.GetCultureInfo(currencyType));
            textbox.Tag = "MONEY";
            textbox.AccessibleDescription = value.ToString();
            return textbox.Text;
        }

    }

} 
