using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using kaede2nd.Entity;

namespace kaede2nd
{
    public partial class RecentItem : MyForm
    {
        /*
        private static RecentItem _instance = new RecentItem();
        public static RecentItem Instance { get { return RecentItem._instance; } }
        */

        private int countPerPage = 16;

        private List<RecentItemSet> recentList = new List<RecentItemSet>();
        public void AddRecentItemId(UInt32 iid)
        {
            this.recentList.Add(new RecentItemSet(iid));
            this.ReDraw();
        }

        public void ReDraw()
        {
            this.dataGridView1.Rows.Clear();
            int kensuu = 0;

            kaede2nd.Dao.IItemDao idao = GlobalData.getIDao<kaede2nd.Dao.IItemDao>();
            for ( int cnt = this.recentList.Count-1; cnt >= 0; cnt-- )
            {
                if (this.recentList[cnt].printed == true) { continue; }
                var itl = idao.GetItemById(this.recentList[cnt].item_id);
                if (itl.Count == 0) { this.recentList.RemoveAt(cnt); continue; }

                DataGridViewRow row = this.dataGridView1.Rows[this.dataGridView1.Rows.Add()];
                this.setRowValue(row, itl[0]);
                kensuu++;
            }

            this.text_kensuu.Text = kensuu.ToString();
            if (kensuu >= this.countPerPage)
            {
                this.text_kensuu.ForeColor = Color.HotPink;
                this.BackColor = Color.Red;
            }
            else
            {
                this.text_kensuu.ForeColor = SystemColors.WindowText;
                this.BackColor = SystemColors.Control;
            }
        }

        public RecentItem()
        {
            InitializeComponent();

            this.button_print.Text = "最古" + this.countPerPage.ToString() + "件印刷 (Ctrl+P)";

            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ReadOnly = true;
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
                Globals.setCellByDateTimeN(cell, ((Item)obj).item_selltime, "不明");
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

            this.addDGVEvents(this.dataGridView1);
        }

        private void RecentItem_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void 最新の情報に更新RToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ReDraw();
        }

        private void 常に手前に表示AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.TopMost = 常に手前に表示AToolStripMenuItem.Checked;
        }

        private void button_ext_Click(object sender, EventArgs e)
        {
            this.contextMenuStrip1.Show((Button)sender, new Point(0, ((Button)sender).Size.Height));
        }

        private void RecentItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.P)
            {
                this.button_print.PerformClick();
            }
            else if (e.KeyCode == Keys.F5)
            {
                this.最新の情報に更新RToolStripMenuItem.PerformClick();
            }
            else if (e.KeyCode == Keys.F5)
            {
            }
        }

        private void DeleteOldPrinted()
        {
            for (int cnt = this.recentList.Count - 1; cnt >= 0; cnt--)
            {
                if (this.recentList[cnt].printed == true)
                {
                    this.recentList.RemoveAt(cnt);
                }
            }
        }

        private void 最後に印刷した商品をリストに復活ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int cnt = this.recentList.Count - 1; cnt >= 0; cnt--)
            {
                if (this.recentList[cnt].printed == true)
                {
                    this.recentList[cnt].printed = false;
                }
            }
            this.ReDraw();
        }


        private void button_print_Click(object sender, EventArgs e)
        {
            this.DoPrint(false);
        }

        private void 表示商品全てを印刷PToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.DoPrint(true);
        }

        private void DoPrint( bool printAll )
        {
            this.DeleteOldPrinted();

            List<Item> items = new List<Item>();

            kaede2nd.Dao.IItemDao idao = GlobalData.getIDao<kaede2nd.Dao.IItemDao>();
            for (int cnt = 0; cnt < this.recentList.Count; cnt++)
            {
                var itl = idao.GetItemById(this.recentList[cnt].item_id);
                if (itl.Count == 0) { continue; }

                items.Add(itl[0]);
                this.recentList[cnt].printed = true;

                if (printAll == false)
                {
                    if (items.Count >= this.countPerPage) { break; }
                }
            }

            ItemsPrintDocument.PrintItems(items);

            this.ReDraw();
        }
    }

    public class RecentItemSet
    {
        public UInt32 item_id;
        public bool printed;

        public RecentItemSet(UInt32 item_id)
        {
            this.item_id = item_id;
            this.printed = false;
        }
    }
}
