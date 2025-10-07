using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace smpc_accounting_app.Pages
{
    public partial class SetupSelectionModal : Form
    {
        private string Title { get; }
        private string EndPoint { get;}
        private Dictionary <string,string> columnMappings { get; set; }
        private Dictionary<string, string> result { get; set; }
        //private DataView result { get; set; }
        private DataTable Dt { get; set; }
        public SetupSelectionModal(string title, string api, DataTable dt, Dictionary<string,string> columnMappings,  int recordIndex=0)
        {
            InitializeComponent();
       
            lbl_title.Text = title;
            this.Text = title;
            this.EndPoint = api;
            this.columnMappings = columnMappings;
            this.Dt = dt;
        }

        private void SelectionModal_Load(object sender, EventArgs e)

        {
            DataTable filteredTable = new DataTable();

            dg_general.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            foreach (var pair in columnMappings)
            {
                if (this.Dt.Columns.Contains(pair.Key))
                {
                    filteredTable.Columns.Add(pair.Value, this.Dt.Columns[pair.Key].DataType);
                }
            }

            foreach (DataRow row in this.Dt.Rows)
            {
                DataRow newRow = filteredTable.NewRow();
                foreach (var pair in columnMappings)
                {
                 


                    if (this.Dt.Columns.Contains(pair.Key))
                    {

                        newRow[pair.Value] = row[pair.Key];
                    }
                   
                }

       
                filteredTable.Rows.Add(newRow);
            }

            dg_general.DataSource = filteredTable;
            dg_general.Columns["ORDER ID"].Visible = false;

        }

        public Dictionary<string, string> GetResult()
        {
            return result;
        }


        private void dg_general_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string base_id = dg_general.Rows[e.RowIndex].Cells[0].Value.ToString();

                Dictionary<string, string> data = new Dictionary<string, string>()
                {
                    { "id", base_id}
                };

                this.result = data;
                this.DialogResult = DialogResult.OK;
                this.Close();
            };
            }
        }
}
