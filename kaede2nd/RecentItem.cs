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
            this.AddRecentItemId(iid, true);
        }

        public void AddRecentItemId(UInt32 iid, bool doRedraw)
        {
            RecentItemSet ris = this.GetItemSetFromId(iid);
            if (ris != null)
            {
                if (ris.printed == false) { return; }
                else { this.recentList.Remove(ris); }
            }

            this.recentList.Add(new RecentItemSet(iid));

            if (doRedraw)
            {
                this.ReDraw();
            }
        }

        public void ReDraw()
        {
            this.dataGridView1.Rows.Clear();
            uint kensuu = 0;

            kaede2nd.Dao.IItemDao idao = GlobalData.getIDao<kaede2nd.Dao.IItemDao>();
            for ( int cnt = this.recentList.Count-1; cnt >= 0; cnt-- )
            {
                if (this.recentList[cnt].printed == true) { continue; }
                var itl = idao.GetItemById(this.recentList[cnt].item_id);
                if (itl.Count == 0) { this.recentList.RemoveAt(cnt); continue; }

                DataGridViewRow row = this.dataGridView1.Rows[this.dataGridView1.Rows.Add()];
                this.setRowValue(row, itl[0]);
                kensuu += itl[0].GetTagPrintCount();
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
            this.dataGridView1.DefaultCellStyle.BackColor = GlobalData.Instance.data.symbolColor;
            this.dataGridView1.RowTemplate.Height = 20;


            this.AddColumn(this.dataGridView1, ColumnType.ItemId);
            this.AddColumn(this.dataGridView1, ColumnType.ReceiptIdButton);
            this.AddColumn(this.dataGridView1, ColumnType.ItemName);
            this.AddColumn(this.dataGridView1, ColumnType.TagPrice);
            this.AddColumn(this.dataGridView1, ColumnType.IsHenpin);
            this.AddColumn(this.dataGridView1, ColumnType.Bunsatsu);
            this.AddColumn(this.dataGridView1, ColumnType.ItemReceiveTime);
            this.AddColumn(this.dataGridView1, ColumnType.ItemComment);

            this.addDGVEvents(this.dataGridView1);
        }

        private void RecentItem_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();

            if (e.CloseReason == CloseReason.UserClosing)
            {
                Program.config.ShowForm_RecentItem = false;
            }
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
            uint printCount = 0;

            kaede2nd.Dao.IItemDao idao = GlobalData.getIDao<kaede2nd.Dao.IItemDao>();
            for (int cnt = 0; cnt < this.recentList.Count; cnt++)
            {
                var itl = idao.GetItemById(this.recentList[cnt].item_id);
                if (itl.Count == 0) { continue; }


                if (printAll == false)
                {
                    if ((printCount + itl[0].GetTagPrintCount() ) > this.countPerPage) { break; }
                }

                items.Add(itl[0]);
                this.recentList[cnt].printed = true;
                printCount += itl[0].GetTagPrintCount();
                
            }

            ItemsPrintDocument.PrintItems(items);

            this.ReDraw();
        }

        private void RecentItem_Shown(object sender, EventArgs e)
        {
            this.Text = "最近追加された商品リスト (" + GlobalData.Instance.data.bumonName + ")";
        }

        private void 選択した商品を印刷SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.dataGridView1.ExtendSelection();

            List<Item> items = new List<Item>();

            kaede2nd.Dao.IItemDao idao = GlobalData.getIDao<kaede2nd.Dao.IItemDao>();
            for (int i = 0; i < this.dataGridView1.SelectedRows.Count; i++)
            {
                var itl = idao.GetItemById(this.recentList[i].item_id);
                if (itl.Count == 0) { continue; }
                items.Add(itl[0]);
            }

            items.Sort(delegate(Item a, Item b) { return a.item_id.CompareTo(b.item_id); });

            foreach (Item it in items)
            {
                RecentItemSet ris = this.GetItemSetFromId(it.item_id);
                if (ris != null)
                {
                    ris.printed = true;
                }
            }

            ItemsPrintDocument.PrintItems(items);
            this.ReDraw();
        }

        private RecentItemSet GetItemSetFromId(uint iid)
        {
            foreach (RecentItemSet ris in this.recentList)
            {
                if (ris.item_id == iid) { return ris; }
            }

            return null;
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
