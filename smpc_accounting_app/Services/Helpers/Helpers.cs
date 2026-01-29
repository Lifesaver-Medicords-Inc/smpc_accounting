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
        public static class DataGridViewFormatter
        {
            public static void DataGridViewDecimalFormat(
                DataGridView dgv,
                IEnumerable<string> columnNames)
            {
                if (dgv == null) return;

                var cols = columnNames.ToArray();

                // store target columns
                dgv.Tag = cols;

                dgv.DataBindingComplete -= Dgv_DataBindingComplete;
                dgv.DataBindingComplete += Dgv_DataBindingComplete;

                dgv.CellValueChanged -= Dgv_CellValueChanged;
                dgv.CellValueChanged += Dgv_CellValueChanged;

                dgv.CurrentCellDirtyStateChanged -= Dgv_CurrentCellDirtyStateChanged;
                dgv.CurrentCellDirtyStateChanged += Dgv_CurrentCellDirtyStateChanged;
            }

            //FORMATS AFTER DATASOURCE BINDING
            private static void Dgv_DataBindingComplete(
                object sender,
                DataGridViewBindingCompleteEventArgs e)
            {
                var dgv = sender as DataGridView;
                if (dgv == null) return;

                var cols = dgv.Tag as string[];
                if (cols == null) return;

                foreach (string colName in cols)
                {
                    if (!dgv.Columns.Contains(colName)) continue;

                    dgv.Columns[colName].DefaultCellStyle.Format = "N2";
                    dgv.Columns[colName].DefaultCellStyle.Alignment =
                        DataGridViewContentAlignment.MiddleRight;
                }
            }

            //COMMIT EDIT ONLY AFTER USER FINISHES
            private static void Dgv_CurrentCellDirtyStateChanged(
                object sender, EventArgs e)
            {
                var dgv = sender as DataGridView;
                if (dgv == null) return;

                if (dgv.IsCurrentCellDirty)
                {
                    dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
            }

            //FORMAT VALUE AFTER EDIT (NOT WHILE TYPING)
            private static void Dgv_CellValueChanged(
                object sender, DataGridViewCellEventArgs e)
            {
                var dgv = sender as DataGridView;
                if (dgv == null || e.RowIndex < 0 || e.ColumnIndex < 0)
                    return;

                var cols = dgv.Tag as string[];
                if (cols == null) return;

                var columnName = dgv.Columns[e.ColumnIndex].Name;
                if (!cols.Contains(columnName)) return;

                var cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (cell.Value == null) return;

                if (decimal.TryParse(
                    cell.Value.ToString(),
                    NumberStyles.Any,
                    CultureInfo.InvariantCulture,
                    out decimal value))
                {
                    cell.Value = value; // formatting handled by DefaultCellStyle
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
                    txt.Text = value.ToString("0.00");
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

        public static void SetChildControlsEnabled(Control[] parents, bool enable, string[] excludeNames)
        {
            foreach (Control parent in parents)
            {
                foreach (Control control in parent.Controls)
                {
                    // Skip excluded controls
                    if (excludeNames != null && excludeNames.Contains(control.Name))
                        continue;

                    // Affect controls of these types
                    if (control is TextBox || control is ComboBox || control is CheckBox || control is DateTimePicker)
                        control.Enabled = enable;

                    // Recurse into child containers
                    if (control.HasChildren)
                        SetChildControlsEnabled(new Control[] { control }, enable, excludeNames);
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
            public static void HandleNumericTextBox(TextBox textBox, params char[] extraAllowedChars)
            {
                if (textBox == null) return;

                // Detach first to avoid duplicate handlers
                textBox.KeyPress -= NumericTextBox_KeyPress;

                // Store allowed chars in Tag
                textBox.Tag = extraAllowedChars;

                textBox.KeyPress += NumericTextBox_KeyPress;
            }

            private static void NumericTextBox_KeyPress(object sender, KeyPressEventArgs e)
            {
                var tb = sender as TextBox;
                if (tb == null) return;

                var extraAllowedChars = tb.Tag as char[];

                // Allow control keys (Backspace, Delete, etc.)
                if (char.IsControl(e.KeyChar))
                    return;

                // Allow digits
                if (char.IsDigit(e.KeyChar))
                    return;

                // Allow decimal point (only once)
                if (e.KeyChar == '.' && !tb.Text.Contains('.'))
                    return;

                // Allow extra characters
                if (extraAllowedChars != null &&
                    extraAllowedChars.Contains(e.KeyChar))
                    return;

                // Block everything else
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

            // Loop through each property of the model
            foreach (var prop in modelType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                Control control = null;

                // Search through all panels for a matching control
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
                    value = textBox.Text;
                else if (control is ComboBox comboBox)
                    value = comboBox.Text;
                else if (control is DateTimePicker dateTimePicker)
                    value = dateTimePicker.Value.ToString("MM/dd/yyyy");

                if (value != null && prop.CanWrite)
                {
                    try
                    {
                        object convertedValue = Convert.ChangeType(value, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                        prop.SetValue(model, convertedValue);
                    }
                    catch
                    {
                        // Ignore conversion errors or handle as needed
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
                // Handle TextBox
                if (control is TextBox textBox)
                {
                    if (string.Equals(textBox.Tag as string, "REQUIRED", StringComparison.OrdinalIgnoreCase)
                        && string.IsNullOrEmpty(textBox.Text))
                    {
                        FlashRed(control);
                        isError = true;
                    }
                    else
                    {
                        control.BackColor = SystemColors.Window;
                    }
                }

                // Handle ComboBox
                else if (control is ComboBox comboBox)
                {
                    if (string.Equals(comboBox.Tag as string, "REQUIRED", StringComparison.OrdinalIgnoreCase)
                        && comboBox.SelectedIndex < 0)
                    {
                        FlashRed(comboBox);
                        isError = true;
                    }
                    else
                    {
                        comboBox.BackColor = SystemColors.Window;
                    }
                }

                // Handle DateTimePicker
                else if (control is DateTimePicker dtp)
                {
                    if (string.Equals(dtp.Tag as string, "REQUIRED", StringComparison.OrdinalIgnoreCase))
                    {
                        // You can customize this check as needed
                        if (dtp.Value == dtp.MinDate || dtp.Value == default(DateTime))
                        {
                            FlashRed(dtp);
                            isError = true;
                        }
                        else
                        {
                            dtp.CalendarMonthBackground = SystemColors.Window;
                            dtp.BackColor = SystemColors.Window;
                        }
                    }
                }
            }

            return isError;
        }

        public static void FlashRed(Control control)
        {
            Color originalColor = SystemColors.Window;
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
        public static class Loading
        {
            private static UserControl overlayPanel;

            public static void ShowLoading(DataGridView dgv, string message = "Loading, please wait...")
            {
                if (overlayPanel != null) return; // already showing

                overlayPanel = new UserControl
                {
                    BackColor = Color.FromArgb(180, Color.Gray), // semi-transparent overlay
                    Dock = DockStyle.Fill
                };

                Label lblMessage = new Label
                {
                    AutoSize = false,
                    Dock = DockStyle.Fill,
                    Text = message,
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                overlayPanel.Controls.Add(lblMessage);

                // Add overlay inside the DataGridView's parent (so it sits on top of the grid)
                dgv.Controls.Add(overlayPanel);
                overlayPanel.BringToFront();
            }

            /// <summary>
            /// Hide the loading overlay from the DataGridView
            /// </summary>
            public static void HideLoading(DataGridView dgv)
            {
                if (overlayPanel != null)
                {
                    dgv.Controls.Remove(overlayPanel);
                    overlayPanel.Dispose();
                    overlayPanel = null;
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

        public static void SetButtonVisibility(ToolStrip toolStrip, IEnumerable<string> visibleButtons, IEnumerable<string> hiddenButtons)
        {
            if (toolStrip == null) return;

            // Make visible buttons visible
            foreach (var buttonName in visibleButtons ?? Enumerable.Empty<string>())
            {
                var btn = toolStrip.Items
                                   .OfType<ToolStripButton>()
                                   .FirstOrDefault(b => b.Name == buttonName);
                if (btn != null)
                    btn.Visible = true;
            }

            // Make hidden buttons invisible
            foreach (var buttonName in hiddenButtons ?? Enumerable.Empty<string>())
            {
                var btn = toolStrip.Items
                                   .OfType<ToolStripButton>()
                                   .FirstOrDefault(b => b.Name == buttonName);
                if (btn != null)
                    btn.Visible = false;
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


                                if (textBox.Tag == "MULTI" && textBox.Tag is List<int> ids && ids.Count > 0)
                                {
                                    textBox.Text = Helpers.MoneyFormat(double.Parse(dt.Rows[selectedIndex][column_name].ToString()));
                                }
                                else
                                {

                                    textBox.Text = (string)dt.Rows[selectedIndex][column_name].ToString();
                                }
                            }

                            // Check if the control is a Combobox
                            if (control is ComboBox comboBox)
                            {
                                string key = comboBox.Name.Replace("cmb_", "") + "_id";

                                if (comboBox.Tag == "DYNAMIC")
                                {

                                    //Console.WriteLine(comboBox.Name);
                                    comboBox.SelectedValue = (string)dt.Rows[selectedIndex][key].ToString();
                                }
                                else
                                {
                                    string keys = comboBox.Name.Replace("cmb_", "");
                                    //Console.WriteLine(comboBox.Name);
                                    comboBox.Text = (string)dt.Rows[selectedIndex][column_name].ToString();
                                }
                            }
                            if (control is CheckBox checkbox)
                            {
                                object value = dt.Rows[selectedIndex][column_name];

                                if (value == DBNull.Value)
                                {
                                    checkbox.Checked = false;
                                }
                                else if (value is bool b)
                                {
                                    checkbox.Checked = b;
                                }
                                else if (value is int i)
                                {
                                    checkbox.Checked = i == 1;
                                }
                                else
                                {
                                    checkbox.Checked = value.ToString() == "1" ||
                                                       value.ToString().Equals("true", StringComparison.OrdinalIgnoreCase);
                                }
                            }
                            // Check if the control is a DATETIME PICKER
                            if (control is DateTimePicker dateTimePicker)
                            {
                                string key = dateTimePicker.Name.Replace("dtp_", "");
                                string val = String.Format("'{0}'", dateTimePicker.Value);
                                object valueFromDataTable = dt.Rows[selectedIndex][column_name];
                                Type type = valueFromDataTable.GetType();
                                Console.WriteLine(type);

                                if (valueFromDataTable != DBNull.Value  )
                                {
                                    DateTime date = DateTime.Parse(valueFromDataTable.ToString());
                                    dateTimePicker.Value = date;
                                }
                                else
                                {
                                    dateTimePicker.Value = DateTime.Now;
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
