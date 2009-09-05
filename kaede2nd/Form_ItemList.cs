using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Common;
using System.IO;
using Seasar.Framework.Container;

using kaede2nd.Entity;
using kaede2nd.Dao;

namespace kaede2nd
{
    public partial class Form_ItemList : MyForm
    {
        public delegate List<Item> ItemReturnDelegate();

        private ItemReturnDelegate itemReturner;
        private List<Item> itemList;

        public Form_ItemList(ItemReturnDelegate returner)
            : this()
        {
            this.itemReturner = returner;
        }

        public Form_ItemList()
        {
            this.itemList = null;
            this.itemReturner = null;

            InitializeComponent();

            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.DefaultCellStyle.BackColor = GlobalData.Instance.symbolColor;
            this.dataGridView1.RowTemplate.Height = 20;

            ColumnInfo colinfo;
            DataGridViewColumn col;

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
            this.dataGridView1.Columns.Add(col);

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
            this.dataGridView1.Columns.Add(col);

            colinfo = this.newColumn<DataGridViewTextBoxColumn>(ColumnName.shuppinsha);
            colinfo.sortComparison = delegate(DataGridViewCell c1, DataGridViewCell c2)
            {
                return String.Compare((string)c1.Tag, (string)c2.Tag, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.CompareOptions.Ordinal);
            };
            colinfo.CellvalueSet = delegate(DataGridViewCell cell, object obj)
            {
                Receipt r = ((Item)obj).item__Receipt;

                cell.Value = r.getSellerString();
                cell.Tag = r.getSellerSortKey();
            };
            col = colinfo.col;
            col.ValueType = typeof(string);
            col.ReadOnly = true;
            col.Width = GlobalData.moziWidth * 18;
            this.dataGridView1.Columns.Add(col);



            colinfo = this.newColumn<DataGridViewTextBoxColumn>(ColumnName.shouhinMei);
            colinfo.imeMode = ImeMode.On;
            colinfo.DBvalueSet = delegate(object obj, object val) { ((Item)obj).item_name = Globals.strNoNull((string)val); };
            colinfo.CellvalueSet = delegate(DataGridViewCell cell, object obj)
            {
                cell.Value = ((Item)obj).item_name;
            };
            col = colinfo.col;
            col.ValueType = typeof(string);
            col.Width = GlobalData.moziWidth * 20;
            this.dataGridView1.Columns.Add(col);

            colinfo = this.newColumn<DataGridViewTextBoxColumn>(ColumnName.teika);
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
            this.dataGridView1.Columns.Add(col);


            colinfo = this.newColumn<DataGridViewCheckBoxColumn>(ColumnName.henpin);
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
            this.dataGridView1.Columns.Add(col);



            colinfo = this.newColumn<DataGridViewTextBoxColumn>(ColumnName.uketsukeNitizi);
            colinfo.sortComparison = ColumnInfo.DateTimeCellComp;

            colinfo.CellvalueSet = delegate(DataGridViewCell cell, object obj)
            {
                Globals.setCellByDateTimeN(cell, ((Item)obj).item_receipt_time, "不明");
            };
            col = colinfo.col;
            col.ValueType = typeof(string);
            col.ReadOnly = true;
            col.Width = GlobalData.moziWidth * 11;
            this.dataGridView1.Columns.Add(col);



            colinfo = this.newColumn<DataGridViewTextBoxColumn>(ColumnName.comment);
            colinfo.DBvalueSet = delegate(object obj, object val) { ((Item)obj).item_comment = (string)val; };
            colinfo.CellvalueSet = delegate(DataGridViewCell cell, object obj)
            {
                cell.Value = ((Item)obj).item_comment;
            };
            col = colinfo.col;
            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            col.ValueType = typeof(string);
            col.Width = GlobalData.moziWidth * 18;
            this.dataGridView1.Columns.Add(col);


            colinfo = this.newColumn<DataGridViewTextBoxColumn>(ColumnName.baika);
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
            this.dataGridView1.Columns.Add(col);

            colinfo = this.newColumn<DataGridViewTextBoxColumn>(ColumnName.hanbaiNitizi);
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
            this.dataGridView1.Columns.Add(col);


            colinfo = this.newColumn<DataGridViewTextBoxColumn>(ColumnName.hanbaiComment);
            colinfo.DBvalueSet = delegate(object obj, object val) { ((Item)obj).item_sellcomment = (string)val; };
            colinfo.CellvalueSet = delegate(DataGridViewCell cell, object obj)
            {
                cell.Value = ((Item)obj).item_sellcomment;
            };
            col = colinfo.col;
            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            col.ValueType = typeof(string);
            col.Width = GlobalData.moziWidth * 18;
            this.dataGridView1.Columns.Add(col);

            this.addDGVEvents(this.dataGridView1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.renewItemList();
        }

        private void renewItemList()
        {
            if (this.itemReturner == null) { return; }

            dataGridView1.Enabled = false;
            this.dataGridView1.Rows.Clear();

            this.itemList = this.itemReturner();

            foreach (Item it in this.itemList)
            {
                DataGridViewRow row = this.dataGridView1.Rows[this.dataGridView1.Rows.Add()];
                this.setRowValue(row, it);
            }

            dataGridView1.Enabled = true;
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (!dgv.Enabled) { return; }
            if (e.ColumnIndex == 0) { return; }

            if (dgv.Rows[e.RowIndex].IsNewRow) { return; }

            if (this.itemList == null) { throw new InvalidOperationException(); }

            var itemDao = GlobalData.getIDao<IItemDao>();
            Item it;

            if (dgv[ColumnName.shinaBan, e.RowIndex].Value == null)
            {
                return;
            }
            else
            {
                it = this.GetItemFromList((UInt32)dgv[ColumnName.shinaBan, e.RowIndex].Value);
                this.changeDBdata(dgv.Columns[e.ColumnIndex].Name, it, dgv[e.ColumnIndex, e.RowIndex].Value);
                itemDao.Update(it);
            }

        }

        private Item GetItemFromList(UInt32 id)
        {
            return (from ites in this.itemList
                    where ites.item_id == id
                    select ites)
                    .Single();
        }

        private void Form_ItemList_Load(object sender, EventArgs e)
        {
            this.renewItemList();
            this.Form_ItemList_Resize(sender, e);
        }

        private void Form_ItemList_Resize(object sender, EventArgs e)
        {
            Size s = this.ClientSize;
            s.Height -= 60;
            this.dataGridView1.Size = s;
        }


        private void dgv_DeleteSelectedRow()
        {
            DataGridView dgv = this.dataGridView1;
            if (!dgv.Enabled) { return; }
            if (this.itemList == null) { throw new InvalidOperationException(); }

            if (MessageBox.Show("選択した " + dgv.SelectedRows.Count.ToString() + "行を削除しますか？", "削除確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                for (int i = this.dataGridView1.SelectedRows.Count - 1; i >= 0; i--)
                {
                    DataGridViewRow row = this.dataGridView1.SelectedRows[i];

                    //if (dgv.RowCount <= e.RowIndex) { return; } //新規行のキャンセル

                    if (row.Cells[ColumnName.shinaBan].Value == null)
                    {
                    }
                    else
                    {

                        Item it = this.GetItemFromList((UInt32)row.Cells[ColumnName.shinaBan].Value);

                        var itemDao = GlobalData.getIDao<IItemDao>();
                        this.itemList.Remove(it);
                        itemDao.Delete(it);
                    }

                    if (!row.IsNewRow)
                    {
                        this.dataGridView1.Rows.Remove(row);
                    }
                }


            }
        }


        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                this.dgv_DeleteSelectedRow();
            }
        }
    }
}
