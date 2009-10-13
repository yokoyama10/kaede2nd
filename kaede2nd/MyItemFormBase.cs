﻿using System;
using System.Collections.Generic;
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
        protected System.Net.WebClient webc; 

        public MyItemFormBase()
        {
            this.formDGV = null;
            this.webc = new System.Net.WebClient();

            InitializeComponent();
        }

        protected override void addDGVEvents(DataGridView dgv)
        {
            base.addDGVEvents(dgv);
            dgv.RowHeaderMouseClick += this.dgv_RowHeaderMouseClick;

            this.formDGV = dgv;
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

        private void dgv_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            if (e.Button == MouseButtons.Right)
            {
                if (dgv.Rows[e.RowIndex].Selected == false)
                {
                    dgv.ClearSelection();
                    dgv.Rows[e.RowIndex].Selected = true;
                }

                System.Drawing.Rectangle r = dgv.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                this.contextMenuStrip_rowHeader.Show(dgv, e.X + r.X, e.Y + r.Y);
            }
        }

#region Amazon関連


        protected readonly string tooltip_ISBNsearching = "ISBN検索中";
        //別スレッド
        protected void setTitleConvIsbnThread(object obj)
        {
            DataGridViewCell cell = (DataGridViewCell)obj;

            if (cell.ToolTipText == this.tooltip_ISBNsearching) { return; }

            bool f = false;
            try
            {
                ControlUtil.SafelyOperated(this, (MethodInvoker)delegate() { cell.ToolTipText = this.tooltip_ISBNsearching; });
                f = this.setTitleConvIsbn_Impl(cell);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "ISBN検索");
            }
            finally
            {
                if (f == false)
                {
                    //書名をセットしなかった
                }

                ControlUtil.SafelyOperated(this, (MethodInvoker)delegate() { cell.ToolTipText = null; });
            }
        }


        //別スレッド
        protected bool setTitleConvIsbn_Impl(DataGridViewCell cell)
        {
            if (cell.DataGridView.Columns[cell.ColumnIndex].Name != ColumnName.shouhinMei)
            {
                throw new Exception("商品名の列以外ではISBNサーチはできません");
            }

            string isbn = (string)cell.Value;
            isbn = Microsoft.VisualBasic.Strings.StrConv(isbn, Microsoft.VisualBasic.VbStrConv.Narrow, 0);

            if (!System.Text.RegularExpressions.Regex.IsMatch(isbn, @"^\d{13}$"))
            {
                return false;
            }

            SignedRequestHelper helper = new SignedRequestHelper("13R1P6WSEW5Y6CP6MVR2", "yv69PdUY09Mosx/k4T9mwiP2xbcDZG6HMF4fsNuX", "ecs.amazonaws.jp");
            IDictionary<string, string> requestParams = new Dictionary<string, String>();
            requestParams["Service"] = "AWSECommerceService";
            requestParams["Version"] = "2009-03-31";
            requestParams["SearchIndex"] = "Books";
            requestParams["Operation"] = "ItemLookup";
            requestParams["IdType"] = "ISBN";
            requestParams["ItemId"] = isbn;
            string url = helper.Sign(requestParams);

            // @"http://webservices.amazon.co.jp/onca/xml?SearchIndex=Books&Operation=ItemLookup&IdType=ISBN&AWSAccessKeyId=13R1P6WSEW5Y6CP6MVR2&ItemId=" + isbn
            using (Stream st = this.webc.OpenRead(url))
            {
                if (st == null) { return false; }
                using (StreamReader sr = new StreamReader(st, Encoding.UTF8))
                {
                    XmlDocument xdoc = new XmlDocument();
                    xdoc.Load(sr);

                    /*
                    XmlNamespaceManager xman = new XmlNamespaceManager(xdoc.NameTable);
                    xman.AddNamespace("aws", @"http://webservices.amazon.com/AWSECommerceService/2005-10-05");
                    XmlNode xtitle = xdoc.SelectSingleNode(@"/aws:ItemLookupResponse/aws:Items/aws:Item/aws:ItemAttributes/aws:Title", xman);
                    */

                    XmlNodeList xlist = xdoc.GetElementsByTagName("Title", @"http://webservices.amazon.com/AWSECommerceService/2009-03-31");

                    if (xlist.Count > 0)
                    {
                        XmlNode xtitle = xlist.Item(0);
                        ControlUtil.SafelyOperated(this, (MethodInvoker)delegate()
                        {
                            cell.DataGridView[ColumnName.isbn, cell.RowIndex].Value = decimal.Parse(isbn);
                            cell.Value = xtitle.InnerText;
                        });
                    }
                    else
                    {
                        ControlUtil.SafelyOperated(this, (MethodInvoker)delegate()
                        {
                            cell.DataGridView[ColumnName.isbn, cell.RowIndex].Value = null;
                        });
                        return false;
                    }

                    sr.Close();
                }
                st.Close();
            }

            return true;
        }

#endregion


    }
}