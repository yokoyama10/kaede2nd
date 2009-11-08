using System;
using System.Collections.Generic;
using System.Windows.Forms;

using kaede2nd.Entity;

namespace kaede2nd
{
    public partial class MyForm : Form
    {

        protected System.Collections.Generic.Dictionary<string, ColumnInfo> colinfos;

        public MyForm()
        {
            InitializeComponent();

            this.colinfos = new Dictionary<string, ColumnInfo>();
        }


        public enum ColumnType
        {
            ItemId,
            ReceiptIdButton,
        }

        protected void AddColumn(DataGridView dgv, ColumnType type)
        {
            ColumnInfo colinfo;
            DataGridViewColumn col;

            switch (type)
            {
                case ColumnType.ItemId:
                    colinfo = this.newColumn<DataGridViewTextBoxColumn>(ColumnName.shinaBan);
                    colinfo.CellvalueSet = delegate(DataGridViewCell cell, object obj)
                    {
                        cell.Value = ((Item)obj).item_id;
                    };
                    col = colinfo.col;
                    col.ValueType = typeof(UInt32);
                    col.ReadOnly = true;
                    col.DefaultCellStyle.Format = "00000";
                    col.Width = GlobalData.moziWidth * 6;

                    dgv.Columns.Add(col);
                    break;

                case ColumnType.ReceiptIdButton:
                    colinfo = this.newColumn<DataGridViewButtonColumn>(ColumnName.hyouBan);
                    colinfo.CellvalueSet = delegate(DataGridViewCell cell, object obj)
                    {
                        cell.Value = ((Item)obj).item__Receipt.receipt_id;
                    };
                    col = colinfo.col;
                    col.ValueType = typeof(UInt32);
                    col.ReadOnly = true;
                    col.DefaultCellStyle.BackColor = System.Drawing.SystemColors.ButtonFace;
                    col.DefaultCellStyle.Format = "'R'0000";
                    col.Width = GlobalData.moziWidth * 6;
                    col.SortMode = DataGridViewColumnSortMode.Automatic;
                    dgv.Columns.Add(col);
                default:
                    throw new NotImplementedException();
            }
        }

        protected ColumnInfo newColumn<T>(string name) where T : System.Windows.Forms.DataGridViewColumn, new()
        {
            ColumnInfo info = new ColumnInfo(new T());

            info.col.DataPropertyName = "_" + name;
            info.col.HeaderText = name;
            info.col.Name = name;

            this.colinfos.Add(name, info);
            return info;
        }

        protected System.Windows.Forms.ImeMode getImeMode(string key)
        {
            if (this.colinfos.ContainsKey(key))
            {
                return this.colinfos[key].imeMode;
            }
            else
            {
                return System.Windows.Forms.ImeMode.NoControl;
            }
        }

        protected void changeDBdata(string key, object obj, object val)
        {
            if (this.colinfos.ContainsKey(key) && this.colinfos[key].DBvalueSet != null)
            {
                this.colinfos[key].DBvalueSet(obj, val);
            }
        }

        protected void setDefaultValue(DataGridViewRow r)
        {
            foreach (KeyValuePair<string, ColumnInfo> kvp in this.colinfos)
            {
                if (kvp.Value.defaultVal != null)
                {
                    r.Cells[kvp.Key].Value = kvp.Value.defaultVal;
                }
            }
        }

        protected void setRowValue(DataGridViewRow r, object obj)
        {
            foreach (KeyValuePair<string, ColumnInfo> kvp in this.colinfos)
            {
                if (kvp.Value.CellvalueSet != null)
                {
                    kvp.Value.CellvalueSet(r.Cells[kvp.Key], obj);
                }
            }
        }


        //for DGV event
        protected virtual void addDGVEvents(DataGridView dgv)
        {
            dgv.DefaultValuesNeeded += dataGridView_DefaultValuesNeeded;
            dgv.RowsAdded += this.dataGridView_RowsAdded;
            dgv.CellEnter += this.dataGridView_CellEnter;
            dgv.SortCompare += this.dataGridView_SortCompare;
            dgv.CellContentClick += this.dataGridView_CellContentClick;
        }

        protected void dataGridView_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            this.setDefaultValue(e.Row);
        }

        protected void dataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (dgv.NewRowIndex != -1)
            {
                this.setDefaultValue(dgv.Rows[dgv.NewRowIndex]);
            }
        }

        protected void dataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            dgv.ImeMode = this.getImeMode(dgv.Columns[e.ColumnIndex].Name);

            if (dgv.NewRowIndex != -1)
            {
                this.setDefaultValue(dgv.Rows[dgv.NewRowIndex]);
            }
        }

        protected void dataGridView_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            string key = e.Column.Name;

            if (this.colinfos.ContainsKey(key) && this.colinfos[key].sortComparison != null)
            {
                DataGridView dgv = (DataGridView)sender;
                e.SortResult = this.colinfos[key].sortComparison(dgv[e.Column.Index, e.RowIndex1], dgv[e.Column.Index, e.RowIndex2]);
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }

        protected void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            DataGridView dgv = (DataGridView)sender;
            if (dgv.Enabled == false) { return; }

            if (dgv.Columns[e.ColumnIndex].Name != ColumnName.hyouBan) { return; }
            if (e.RowIndex < 0) { return; }

            uint receiptid = (uint)dgv[ColumnName.hyouBan, e.RowIndex].Value;

            try
            {
                foreach (Form f in Application.OpenForms)
                {
                    if (f is ReceiptForm)
                    {
                        Receipt r = ((ReceiptForm)f).GetReceipt();
                        if (r != null)
                        {
                            if (r.receipt_id == receiptid)
                            {
                                f.Activate();
                                return;
                            }
                        }
                    }
                }

                ReceiptForm rf = new ReceiptForm((UInt32)dgv[ColumnName.hyouBan, e.RowIndex].Value);
                rf.Show();
            }
            catch (Exception excep)
            {
                MessageBox.Show("Receiptウィンドウが生成できませんでした: " + excep.Message);
            }
        }

    }
}
