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
using smpc_accounting_app.Services;
using smpc_accounting_app.Models;
using smpc_accounting_app.Shared;

namespace smpc_accounting_app.Pages.Transactions.AccountsPayable.InvoiceReceipt.InvoiceReceiptModals
{
    public partial class InvoiceSearchPO : Form
    {
        public DataTable SelectedPO { get; private set; } = null;
        public DataTable SelectedRows { get; private set; } = null;
        public List<string> SelectedPOLabels { get; private set; } = null;
        public float? SelectedTotalAmount { get; private set; } = null;
        GeneralService<PurchaseOrderInvoiceList> serviceSetup;
        private int _supplierID;
        private PurchaseOrderInvoiceList _podata;
        private List<PurchaseOrderInvoiceModel> _parentdata;
        private List<PurchaseOrderDetailsInvoiceModel> _childdata;
        public DataTable ExistingPOs { get; set; }

        public InvoiceSearchPO(int supplierId)
        {
            InitializeComponent();

            // Center the modal relative to its parent form
            this.StartPosition = FormStartPosition.CenterParent;

            _supplierID = supplierId;
        }

        private async void InvoiceSearchPO_Load(object sender, EventArgs e)
        {
            try
            {
                await LoadPurchaseOrder();
            }
            catch (Exception ex)
            {
                Helpers.ShowDialogMessage("error", $"Failed to load: {ex.Message}");
            }
        }

        private async Task LoadPurchaseOrder()
        {
            try
            {
                serviceSetup = new GeneralService<PurchaseOrderInvoiceList>(ApiEndPoints.INVOICE_RECEIPT_PO + _supplierID);

                _podata = await serviceSetup.GetAsModel();

                _parentdata = _podata?.purchase_order_view;
                _childdata = _podata?.purchase_order_details_view;

                if (_parentdata == null || !_parentdata.Any())
                    throw new InvalidOperationException("No Purchase Orders found.");

                RenderPurchaseOrders();
                PopulatePurchaseOrderTree();
            }
            catch (NullReferenceException)
            {
                Helpers.ShowDialogMessage("error", "No Purchase Order found.");
            }
            catch (InvalidOperationException)
            {
                Helpers.ShowDialogMessage("error", "No Purchase Orders found.");
                this.Close();
            }
            catch (Exception)
            {
                // Catch real unexpected errors
                Helpers.ShowDialogMessage("error", "No Purchase Orders found.");
                this.Close();
            }
        }

        private void PopulatePurchaseOrderTree()
        {
            treev_main.BeginUpdate();

            // Get or create root node
            TreeNode rootNode = treev_main.Nodes
                .Cast<TreeNode>()
                .FirstOrDefault(n => n.Text == "All Purchase Order");

            if (rootNode == null)
            {
                rootNode = new TreeNode("All Purchase Order");
                treev_main.Nodes.Add(rootNode);
            }

            rootNode.Nodes.Clear();

            // Add PO nodes
            foreach (var parent in _parentdata)
            {
                TreeNode poNode = new TreeNode("PO#" + parent.po_number)
                {
                    Tag = parent // store full object for later use
                };

                rootNode.Nodes.Add(poNode);
            }

            rootNode.Expand();
            treev_main.EndUpdate();
        }

        private void RenderPurchaseOrders()
        {
            flow_main.SuspendLayout();
            flow_main.Controls.Clear();

            foreach (var parent in _parentdata)
            {
                Panel panel = CreatePOPanel(parent);
                flow_main.Controls.Add(panel);
            }

            flow_main.ResumeLayout();
        }

        private Panel CreatePOPanel(PurchaseOrderInvoiceModel parent)
        {
            Panel panel = new Panel
            {
                Width = flow_main.ClientSize.Width - 25,
                Height = 260,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(5)
            };

            // Hidden Parent ID
            Label lblId = new Label
            {
                Text = parent.id.ToString(),
                Visible = false
            };

            Label lblPoNo = CreateHeaderLabel($"PO #: {parent.po_number}", 10);
            Label lblDate = CreateHeaderLabel($"Date: {parent.doc_date}", 200);
            Label lblSupplier = new Label
            {
                Text = $"Supplier: {parent.supplier_name}",
                Left = 400,
                Top = 8,
                Width = panel.Width - 410,   // fits inside panel
                Height = 18,
                AutoSize = false,
                AutoEllipsis = true,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };

            Label lblTotalAmount = new Label
            {
                Text = $"Total Amount: {parent.total_amount_po}",
                Left = 400,
                Top = 8,
                Width = panel.Width - 410,   // fits inside panel
                Height = 18,
                AutoSize = false,
                AutoEllipsis = true,
                TextAlign = ContentAlignment.MiddleLeft,
                Visible = false,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };

            panel.Controls.AddRange(new Control[]
            {
                lblId, lblPoNo, lblDate, lblSupplier, lblTotalAmount
            });

            DataGridView dgv = CreateChildGrid(parent.id);
            dgv.Top = 35;
            dgv.Left = 10;

            panel.Controls.Add(dgv);

            return panel;
        }

        private Label CreateHeaderLabel(string text, int left)
        {
            return new Label
            {
                Text = text,
                Left = left,
                Top = 8,
                AutoSize = true,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
        }

        private DataGridView CreateChildGrid(int parentId)
        {
            DataGridView dgv = new DataGridView
            {
                Width = flow_main.ClientSize.Width - 60,
                Height = 200,
                AllowUserToAddRows = false,
                ReadOnly = false,
                RowHeadersVisible = true,
                SelectionMode = DataGridViewSelectionMode.RowHeaderSelect,
                AutoGenerateColumns = true,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    BackColor = Color.Gainsboro
                }
            };

            // === Columns ===
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "purchase_order_details_id",
                Visible = false,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.Gainsboro
                }
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "based_id",
                Visible = false,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.Gainsboro
                }
            });

            dgv.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = " ",
                Width = 30,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.Gainsboro
                }
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "item_code",
                DataPropertyName = "item_code",
                HeaderText = "Item Code",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.Gainsboro
                }
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "item_description",
                DataPropertyName = "item_description",
                HeaderText = "Description",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.Gainsboro
                }
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "order_uom",
                DataPropertyName = "order_uom",
                HeaderText = "UOM",
                Width = 40,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.Gainsboro
                }
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "order_qty",
                DataPropertyName = "order_qty",
                HeaderText = "Qty. Available",
                Width = 80,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.Gainsboro
                }
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "req_uom",
                DataPropertyName = "req_uom",
                HeaderText = "Invoice UOM",
                Width = 80,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.Gainsboro
                }
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "req_qty",
                DataPropertyName = "req_qty",
                HeaderText = "Qty. Invoiced",
                Width = 80,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.Gainsboro
                }
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "unit_price",
                Visible = false
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "total_cost",
                Visible = false
            });

            // === Rows ===
            var children = _childdata.Where(c => c.based_id == parentId);

            foreach (var child in children)
            {
                dgv.Rows.Add(
                    child.purchase_order_details_id,
                    child.based_id,
                    false,
                    child.item_code,
                    child.item_description,
                    child.order_uom,
                    child.order_qty,
                    child.req_uom,
                    child.req_qty,
                    child.unit_price,
                    child.total_cost
                );
            }

            return dgv;
        }

        private void InvoiceSearchPO_Resize(object sender, EventArgs e)
        {
            foreach (Panel panel in flow_main.Controls.OfType<Panel>())
            {
                panel.Width = flow_main.ClientSize.Width - 25;

                var dgv = panel.Controls.OfType<DataGridView>().FirstOrDefault();
                if (dgv != null)
                    dgv.Width = flow_main.ClientSize.Width - 60;
            }
        }

        private void treev_main_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Parent == null)
            {
                RenderPurchaseOrders(); // show all
                return;
            }

            if (e.Node.Tag is PurchaseOrderInvoiceModel selectedPO)
            {
                ShowSinglePurchaseOrder(selectedPO);
            }
        }

        private void ShowSinglePurchaseOrder(PurchaseOrderInvoiceModel parent)
        {
            flow_main.SuspendLayout();
            flow_main.Controls.Clear();

            Panel panel = CreatePOPanel(parent);
            flow_main.Controls.Add(panel);

            flow_main.ResumeLayout();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            SelectedRows = new DataTable();
            SelectedPOLabels = new List<string>();

            bool columnsInitialized = false;

            foreach (Panel panel in flow_main.Controls.OfType<Panel>())
            {
                DataGridView dgv = panel.Controls.OfType<DataGridView>().FirstOrDefault();
                if (dgv == null) continue;

                dgv.EndEdit(); // commit checkbox edits

                bool panelHasCheckedRow = false;

                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (row.Cells[2] is DataGridViewCheckBoxCell chk &&
                        chk.Value != null &&
                        (bool)chk.Value)
                    {
                        // Initialize DataTable columns once
                        if (!columnsInitialized)
                        {
                            foreach (DataGridViewColumn col in dgv.Columns)
                            {
                                SelectedRows.Columns.Add(col.Name);
                            }
                            columnsInitialized = true;
                        }

                        // Add row to SelectedRows
                        DataRow dr = SelectedRows.NewRow();
                        foreach (DataGridViewColumn col in dgv.Columns)
                        {
                            dr[col.Name] = row.Cells[col.Index].Value ?? DBNull.Value;
                        }
                        SelectedRows.Rows.Add(dr);

                        panelHasCheckedRow = true;
                    }
                }

                // If this panel has at least 1 checked row → get supplier label
                if (panelHasCheckedRow)
                {
                    Label lblPoNo = panel.Controls
                        .OfType<Label>()
                        .FirstOrDefault(l => l.Text.StartsWith("PO #:"));

                    if (lblPoNo != null)
                    {
                        string poNumber = lblPoNo.Text.Replace("PO #:", "").Trim();

                        if (!SelectedPOLabels.Contains(poNumber))
                            SelectedPOLabels.Add(poNumber);
                    }
                }
            }

            // Validation
            if (SelectedRows.Rows.Count == 0)
            {
                Helpers.ShowDialogMessage("error", "Please select at least one item.");
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btn_unselect_all_Click(object sender, EventArgs e)
        {
            foreach (Panel panel in flow_main.Controls.OfType<Panel>())
            {
                foreach (DataGridView dgv in panel.Controls.OfType<DataGridView>())
                {
                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        if (row.Cells[2] is DataGridViewCheckBoxCell chk)
                        {
                            bool isChecked = chk.Value != null && (bool)chk.Value;
                            if (isChecked)
                            {
                                chk.Value = false;
                            }
                        }
                    }
                }
            }
        }

        private void btn_select_all_Click(object sender, EventArgs e)
        {
            foreach (Panel panel in flow_main.Controls.OfType<Panel>())
            {
                foreach (DataGridView dgv in panel.Controls.OfType<DataGridView>())
                {
                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        if (row.Cells[2] is DataGridViewCheckBoxCell chk)
                        {
                            bool isChecked = chk.Value != null && (bool)chk.Value;
                            if (!isChecked)
                            {
                                chk.Value = true;
                            }
                        }
                    }
                }
            }
        }
    }
}
