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


        protected Receipt getReceiptObj(object obj)
        {
            if (obj is Receipt) { return (Receipt)obj; }
            if (obj is Item) { return ((Item)obj).item__Receipt; }
            throw new InvalidOperationException();
        }

        protected ColumnInfo AddColumn(DataGridView dgv, ColumnType type)
        {
            ColumnInfo colinfo = null;
            DataGridViewColumn col;

            switch (type)
            {
                case ColumnType.ItemId:
                    colinfo = this.newColumn<DataGridViewTextBoxColumn>(ColumnName.shinaBan, ColumnType.ItemId);
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
                    colinfo = this.newColumn<DataGridViewButtonColumn>(ColumnName.hyouBan, ColumnType.ReceiptIdButton);
                    colinfo.CellvalueSet = delegate(DataGridViewCell cell, object obj)
                    {
                        Receipt r = this.getReceiptObj(obj);
                        cell.Value = r.receipt_id;
                    };
                    col = colinfo.col;
                    col.ValueType = typeof(UInt32);
                    col.ReadOnly = true;
                    col.DefaultCellStyle.BackColor = System.Drawing.SystemColors.ButtonFace;
                    col.DefaultCellStyle.Format = "'R'0000";
                    col.Width = GlobalData.moziWidth * 6;
                    col.SortMode = DataGridViewColumnSortMode.Automatic;

                    dgv.Columns.Add(col);
                    break;
                    
                case ColumnType.SellerName:
                    colinfo = this.newColumn<DataGridViewTextBoxColumn>(ColumnName.shuppinsha, ColumnType.SellerName);
                    colinfo.sortComparison = delegate(DataGridViewCell c1, DataGridViewCell c2)
                    {
                        return String.Compare((string)c1.Tag, (string)c2.Tag, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.CompareOptions.Ordinal);
                    };
                    colinfo.CellvalueSet = delegate(DataGridViewCell cell, object obj)
                    {
                        Receipt r = this.getReceiptObj(obj);

                        cell.Value = r.getSellerString();
                        cell.Tag = r.getSellerSortKey();
                    };
                    col = colinfo.col;
                    col.ValueType = typeof(string);
                    col.ReadOnly = true;
                    col.Width = GlobalData.moziWidth * 18;

                    dgv.Columns.Add(col);
                    break;

                case ColumnType.ItemName:
                    colinfo = this.newColumn<DataGridViewTextBoxColumn>(ColumnName.shouhinMei, ColumnType.ItemName);
                    colinfo.imeMode = ImeMode.On;
                    colinfo.DBvalueSet = delegate(object obj, object val) { ((Item)obj).item_name = Globals.strNoNull((string)val); };
                    colinfo.CellvalueSet = delegate(DataGridViewCell cell, object obj)
                    {
                        cell.Value = ((Item)obj).item_name;
                    };
                    col = colinfo.col;
                    col.ValueType = typeof(string);
                    col.Width = GlobalData.moziWidth * 20;

                    dgv.Columns.Add(col);
                    break;

                case ColumnType.TagPrice:
                    colinfo = this.newColumn<DataGridViewTextBoxColumn>(ColumnName.teika, ColumnType.TagPrice);
                    colinfo.imeMode = ImeMode.Off;
                    colinfo.DBvalueSet = delegate(object obj, object val) { ((Item)obj).item_tagprice = (val is UInt32) ? (UInt32)val : 0; };
                    colinfo.CellvalueSet = delegate(DataGridViewCell cell, object obj)
                    {
                        cell.Value = ((Item)obj).item_tagprice;
                    };
                    col = colinfo.col;
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    col.ValueType = typeof(UInt32);
                    col.Width = GlobalData.moziWidth * 6;

                    dgv.Columns.Add(col);
                    break;

                case ColumnType.IsHenpin:
                    colinfo = this.newColumn<DataGridViewCheckBoxColumn>(ColumnName.henpin, ColumnType.IsHenpin);
                    col = colinfo.col;
                    colinfo.defaultVal = Globals.check_falseVal;
                    colinfo.DBvalueSet = delegate(object obj, object val) { ((Item)obj).item_return = Globals.getBoolFromCheckboxString((string)val); };
                    colinfo.CellvalueSet = delegate(DataGridViewCell cell, object obj)
                    {
                        cell.Value = Globals.getCheckboxStringNoNull(((Item)obj).item_return, false);
                    };
                    col.ValueType = typeof(string);
                    ((DataGridViewCheckBoxColumn)col).ThreeState = false;
                    ((DataGridViewCheckBoxColumn)col).TrueValue = Globals.check_trueVal;
                    ((DataGridViewCheckBoxColumn)col).FalseValue = Globals.check_falseVal;
                    //((DataGridViewCheckBoxColumn)col).IndeterminateValue = Globals.check_unkVal;
                    col.Width = GlobalData.moziWidth * 8;
                    col.SortMode = DataGridViewColumnSortMode.Automatic;

                    dgv.Columns.Add(col);
                    break;

                case ColumnType.ItemReceiveTime:
                    colinfo = this.newColumn<DataGridViewTextBoxColumn>(ColumnName.uketsukeNitizi, ColumnType.ItemReceiveTime);
                    colinfo.sortComparison = ColumnInfo.DateTimeCellComp;

                    colinfo.CellvalueSet = delegate(DataGridViewCell cell, object obj)
                    {
                        Globals.setCellByDateTimeN(cell, ((Item)obj).item_receipt_time, "不明");
                    };
                    col = colinfo.col;
                    col.ValueType = typeof(string);
                    col.ReadOnly = true;
                    col.Width = GlobalData.moziWidth * 11;

                    dgv.Columns.Add(col);
                    break;

                case ColumnType.ItemComment:
                    colinfo = this.newColumn<DataGridViewTextBoxColumn>(ColumnName.comment, ColumnType.ItemComment);
                    colinfo.DBvalueSet = delegate(object obj, object val) { ((Item)obj).item_comment = (string)val; };
                    colinfo.CellvalueSet = delegate(DataGridViewCell cell, object obj)
                    {
                        cell.Value = ((Item)obj).item_comment;
                    };
                    col = colinfo.col;
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    col.ValueType = typeof(string);
                    col.Width = GlobalData.moziWidth * 18;

                    dgv.Columns.Add(col);
                    break;

                case ColumnType.SellPrice:
                    colinfo = this.newColumn<DataGridViewTextBoxColumn>(ColumnName.baika, ColumnType.SellPrice);
                    colinfo.imeMode = ImeMode.Off;
                    colinfo.DBvalueSet = delegate(object obj, object val) { ((Item)obj).item_sellprice = Globals.convToUInt32(val); };
                    colinfo.CellvalueSet = delegate(DataGridViewCell cell, object obj)
                    {
                        cell.Value = ((Item)obj).item_sellprice;
                    };
                    col = colinfo.col;
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    col.ValueType = typeof(UInt32);
                    col.Width = GlobalData.moziWidth * 6;

                    dgv.Columns.Add(col);
                    break;

                case ColumnType.SellTime:
                    colinfo = this.newColumn<DataGridViewTextBoxColumn>(ColumnName.hanbaiNitizi, ColumnType.SellTime);
                    colinfo.sortComparison = ColumnInfo.DateTimeCellComp;

                    colinfo.CellvalueSet = delegate(DataGridViewCell cell, object obj)
                    {
                        Item it = (Item)obj;
                        if (it.item_selltime.HasValue)
                        {
                            cell.Tag = it.item_selltime.Value;
                            cell.Value = Globals.getTimeString(it.item_selltime);
                        }
                        else
                        {
                            cell.Tag = DateTime.MinValue;
                            cell.Value = "";
                        }
                    };
                    col = colinfo.col;
                    col.ValueType = typeof(string);
                    col.ReadOnly = true;
                    col.Width = GlobalData.moziWidth * 11;

                    dgv.Columns.Add(col);
                    break;

                case ColumnType.Isbn:
                    colinfo = this.newColumn<DataGridViewTextBoxColumn>(ColumnName.isbn, ColumnType.Isbn);
                    colinfo.imeMode = ImeMode.NoControl;
                    colinfo.DBvalueSet = delegate(object obj, object val) { ((Item)obj).item_isbn = Globals.convToDecimal(val); };
                    colinfo.CellvalueSet = delegate(DataGridViewCell cell, object obj)
                    {
                        cell.Value = ((Item)obj).item_isbn;
                    };
                    col = colinfo.col;
                    col.Visible = false;
                    col.ReadOnly = true;
                    col.ValueType = typeof(decimal);
                    col.Width = GlobalData.moziWidth * 20;

                    dgv.Columns.Add(col);
                    break;

                case ColumnType.Bunsatsu:
                    colinfo = this.newColumn<DataGridViewTextBoxColumn>(ColumnName.bunsatsu, ColumnType.Bunsatsu);
                    colinfo.imeMode = ImeMode.Off;
                    colinfo.DBvalueSet = delegate(object obj, object val) { ((Item)obj).item_volumes = Globals.convToUInt32(val); };
                    colinfo.CellvalueSet = delegate(DataGridViewCell cell, object obj)
                    {
                        cell.Value = ((Item)obj).item_volumes;
                    };
                    col = colinfo.col;
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    col.ValueType = typeof(UInt32);
                    col.Width = GlobalData.moziWidth * 6;

                    dgv.Columns.Add(col);
                    break;

                case ColumnType.ReceiptReceiveTime:
                    colinfo = this.newColumn<DataGridViewTextBoxColumn>(ColumnName.uketsukeNitizi, ColumnType.ReceiptReceiveTime);
                    colinfo.sortComparison = ColumnInfo.DateTimeCellComp;
                    colinfo.CellvalueSet = delegate(DataGridViewCell cell, object obj)
                    {
                        Receipt r = this.getReceiptObj(obj);
                        Globals.setCellByDateTimeN(cell, r.receipt_time, "不明");
                    };
                    col = colinfo.col;
                    col.ValueType = typeof(string);
                    col.ReadOnly = true;
                    col.Width = GlobalData.moziWidth * 11;

                    dgv.Columns.Add(col);
                    break;

                case ColumnType.ReceiptComment:
                    colinfo = this.newColumn<DataGridViewTextBoxColumn>(ColumnName.comment, ColumnType.ReceiptComment);
                    colinfo.DBvalueSet = delegate(object obj, object val) { ((Receipt)obj).receipt_comment = (string)val; };
                    colinfo.CellvalueSet = delegate(DataGridViewCell cell, object obj)
                    {
                        Receipt r = this.getReceiptObj(obj);
                        cell.Value = r.receipt_comment;
                    };
                    col = colinfo.col;
                    col.ValueType = typeof(string);

                    dgv.Columns.Add(col);
                    break;

                case ColumnType.OperatorId:
                    colinfo = this.newColumn<DataGridViewTextBoxColumn>(ColumnName.operatorId, ColumnType.OperatorId);
                    colinfo.CellvalueSet = delegate(DataGridViewCell cell, object obj)
                    {
                        cell.Value = ((Operator)obj).operator_id;
                    };
                    col = colinfo.col;
                    col.ValueType = typeof(UInt32);
                    col.ReadOnly = true;
                    col.Width = GlobalData.moziWidth * 6;

                    dgv.Columns.Add(col);
                    break;

                case ColumnType.OperatorName:
                    colinfo = this.newColumn<DataGridViewTextBoxColumn>(ColumnName.operatorName, ColumnType.OperatorName);
                    colinfo.DBvalueSet = delegate(object obj, object val) { ((Operator)obj).operator_name = (string)val; };
                    colinfo.CellvalueSet = delegate(DataGridViewCell cell, object obj)
                    {
                        cell.Value = ((Operator)obj).operator_name;
                    };
                    col = colinfo.col;
                    col.ValueType = typeof(string);
                    col.Width = GlobalData.moziWidth * 12;

                    dgv.Columns.Add(col);
                    break;

                case ColumnType.OperatorComment:
                    colinfo = this.newColumn<DataGridViewTextBoxColumn>(ColumnName.comment, ColumnType.OperatorComment);
                    colinfo.DBvalueSet = delegate(object obj, object val) { ((Operator)obj).operator_comment = (string)val; };
                    colinfo.CellvalueSet = delegate(DataGridViewCell cell, object obj)
                    {
                        cell.Value = ((Operator)obj).operator_comment;
                    };
                    col = colinfo.col;
                    col.ValueType = typeof(string);
                    col.Width = GlobalData.moziWidth * 50;

                    dgv.Columns.Add(col);
                    break;

                default:
                    throw new NotImplementedException();
            }

            return colinfo;
        }

        protected ColumnInfo newColumn<T>(string name) where T : System.Windows.Forms.DataGridViewColumn, new()
        {
            return this.newColumn<T>(name, ColumnType.Unknown);
        }

        protected ColumnInfo newColumn<T>(string name, ColumnType type) where T : System.Windows.Forms.DataGridViewColumn, new()
        {
            ColumnInfo info = new ColumnInfo(new T(), type);

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
            dgv.ReadOnly = GlobalData.Instance.data.isReadonly;
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

        protected virtual void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
                                f.WindowState = FormWindowState.Normal;
                                f.BringToFront();
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
