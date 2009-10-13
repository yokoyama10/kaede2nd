using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Xml;
using AmazonProductAdvtApi;
using kaede2nd.Entity;
using kaede2nd.Dao;

namespace kaede2nd
{
    public partial class MyItemFormBase : MyForm
    {
        protected DataGridView formDGV;
        protected List<Item> itemList;

        public MyItemFormBase()
        {
            this.formDGV = null;


            InitializeComponent();
        }


        protected Item GetItemFromList(UInt32 id)
        {
            return (from ites in this.itemList
                    where ites.item_id == id
                    select ites)
                    .Single();
        }


        private bool IsEditable()
        {
            if (this.formDGV == null) { return false; }
            return this.IsEditableImpl();
        }

        protected virtual bool IsEditableImpl()
        {
            return true;
        }

        protected void アイテムを削除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.dgv_DeleteSelectedRow();
        }

        protected void まとめて定価設定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.IsEditable() == false) { return; }

            if (this.formDGV.SelectedRows.Count == 0) { return; }

            string res;
            DialogResult dres = InputBox.ShowIntDialog("選択した " + this.formDGV.SelectedRows.Count.ToString() + "件の商品に設定する定価を入力してください", "まとめて定価設定", "ここに入力", out res);

            if (dres == DialogResult.OK)
            {
                UInt32 teika;
                if (UInt32.TryParse(res, out teika))
                {
                    for (int i = this.formDGV.SelectedRows.Count - 1; i >= 0; i--)
                    {
                        DataGridViewRow row = this.formDGV.SelectedRows[i];

                        if (row.Cells[ColumnName.shinaBan].Value == null)
                        {
                        }
                        else
                        {
                            row.Cells[ColumnName.teika].Value = teika;
                        }
                    }
                    return;
                }
                else
                {
                    MessageBox.Show("正しい数値が入力されませんでした", "まとめて定価設定", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void まとめて返品設定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.IsEditable() == false) { return; }

            DialogResult dres = MessageBox.Show(
                "選択した " + this.formDGV.SelectedRows.Count.ToString() + "件の商品に返品フラグを設定します。\n"
                + "[はい] 返品希望にする\n"
                + "[いいえ] 返品希望しないにする", "まとめて返品設定",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.None, MessageBoxDefaultButton.Button3);

            if (dres == DialogResult.Cancel)
            { return; }
            else
            {
                bool isHenpin = (dres == DialogResult.Yes);
                for (int i = this.formDGV.SelectedRows.Count - 1; i >= 0; i--)
                {
                    DataGridViewRow row = this.formDGV.SelectedRows[i];

                    if (row.Cells[ColumnName.shinaBan].Value == null)
                    {
                    }
                    else
                    {
                        row.Cells[ColumnName.henpin].Value = Globals.getCheckboxString(isHenpin);
                    }
                }
                return;
            }
        }

        private void タグを印刷ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.IsEditable() == false) { return; }

            List<Item> items = new List<Item>();

            for (int i = 0; i < this.formDGV.SelectedRows.Count; i++)
            {
                DataGridViewRow row = this.formDGV.SelectedRows[i];

                if (row.Cells[ColumnName.shinaBan].Value == null)
                {
                }
                else
                {
                    items.Add(this.GetItemFromList((UInt32)row.Cells[ColumnName.shinaBan].Value));
                }
            }

            ItemsPrintDocument.PrintItems(items);
        }


        protected void dgv_DeleteSelectedRow()
        {
            if (this.IsEditable() == false) { return; }

            if (this.formDGV.SelectedRows.Count == 0) { return; }

            if (MessageBox.Show("選択した " + this.formDGV.SelectedRows.Count.ToString() + "行を削除しますか？", "削除確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                for (int i = this.formDGV.SelectedRows.Count - 1; i >= 0; i--)
                {
                    DataGridViewRow row = this.formDGV.SelectedRows[i];

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
                        this.formDGV.Rows.Remove(row);
                    }
                }


            }
        }


        protected void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (this.IsEditable() == false) { return; }

            DataGridView dgv = (DataGridView)sender;
            if (!dgv.Enabled) { return; }
            if (this.itemList == null) { throw new InvalidOperationException(); }

            //if (dgv.RowCount <= e.RowIndex) { return; } //新規行のキャンセル

            if (e.Row.Cells[ColumnName.shinaBan].Value == null)
            {
            }
            else
            {

                Item it = this.GetItemFromList((UInt32)e.Row.Cells[ColumnName.shinaBan].Value);

                var itemDao = GlobalData.getIDao<IItemDao>();
                //this.itemList.Remove(it);
                itemDao.Delete(it);
            }
        }

    }
}
