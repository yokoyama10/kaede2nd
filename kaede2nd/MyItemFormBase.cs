using System;
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

        public MyItemFormBase()
        {
            this.formDGV = null;

            InitializeComponent();
        }

        protected override void addDGVEvents(DataGridView dgv)
        {
            base.addDGVEvents(dgv);
            dgv.RowHeaderMouseClick += this.dgv_RowHeaderMouseClick;
            dgv.UserDeletingRow += this.dgv_UserDeletingRow;

            this.formDGV = dgv;
        }

        protected Item GetItemFromList(UInt32 id)
        {
            return (from ites in this.itemList
                    where ites.item_id == id
                    select ites)
                    .Single();
        }


        protected bool IsEditable()
        {
            if (this.formDGV == null) { return false; }
            return this.IsEditableImpl();
        }

        protected virtual bool IsEditableImpl()
        {
            if (GlobalData.Instance.data.isReadonly) { return false; }
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

            items.Sort(delegate(Item a, Item b) { return a.item_id.CompareTo(b.item_id); });

            ItemsPrintDocument.PrintItems(items);
        }

        private void タグ印刷リストに追加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.IsEditable() == false) { return; }

            List<uint> itemids = new List<uint>();

            for (int i = 0; i < this.formDGV.SelectedRows.Count; i++)
            {
                DataGridViewRow row = this.formDGV.SelectedRows[i];

                if (row.Cells[ColumnName.shinaBan].Value == null)
                {
                }
                else
                {
                    itemids.Add((UInt32)row.Cells[ColumnName.shinaBan].Value);
                }
            }

            itemids.Sort();

            for (int i = 0; i < itemids.Count; i++)
            {

                GlobalData.Instance.recentItemForm.AddRecentItemId(itemids[i], false);
            }
            GlobalData.Instance.recentItemForm.ReDraw();
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


        protected void dgv_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
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

        public enum BarcodeType
        {
            Isbn, Jan
        }

        protected void ConvBarcode(DataGridViewCell cell)
        {
            string val = (string)cell.Value;
            if (string.IsNullOrEmpty(val)) { return; }

            if ( val.Length == 13 )
            {
                val = Microsoft.VisualBasic.Strings.StrConv(val, Microsoft.VisualBasic.VbStrConv.Narrow, 0);

                if (System.Text.RegularExpressions.Regex.IsMatch(val, @"^\d{13}$"))
                {
                    if (val.StartsWith("978") || val.StartsWith("979"))
                    {
                        System.Threading.Thread t = new System.Threading.Thread(this.setTitleConvIsbnThread);
                        t.Start(cell);
                    }
                    else if (val.StartsWith("45") || val.StartsWith("49"))
                    {
                        System.Threading.Thread t = new System.Threading.Thread(this.setTitleConvJanThread);
                        t.Start(cell);
                    }
                }
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(val, @"^4\d{9}$"))
            {
                System.Threading.Thread t = new System.Threading.Thread(this.setTitleConvIsbnThread);
                t.Start(cell);
            }
        }


        protected readonly string tooltip_BarcodeSearching = "ISBN/JAN検索中";
        //別スレッド
        protected void setTitleConvIsbnThread(object obj) { this.setTitleConvBarcodeThread(obj, BarcodeType.Isbn); }
        protected void setTitleConvJanThread(object obj) { this.setTitleConvBarcodeThread(obj, BarcodeType.Jan); }

        protected void setTitleConvBarcodeThread(object obj, BarcodeType type)
        {
            DataGridViewCell cell = (DataGridViewCell)obj;

            if (cell.ToolTipText == this.tooltip_BarcodeSearching) { return; }

            bool f = false;
            try
            {
                ControlUtil.SafelyOperated(this, (MethodInvoker)delegate() { cell.ToolTipText = this.tooltip_BarcodeSearching; });
                f = this.setTitleConvIsbn_Impl(cell, type);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "ISBN/JAN検索");
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
        protected bool setTitleConvIsbn_Impl(DataGridViewCell cell, BarcodeType type)
        {
            if (cell.DataGridView.Columns[cell.ColumnIndex].Name != ColumnName.shouhinMei)
            {
                throw new Exception("商品名の列以外ではISBN/JANサーチはできません");
            }

            string code = (string)cell.Value;

            SignedRequestHelper helper = new SignedRequestHelper("13R1P6WSEW5Y6CP6MVR2", "yv69PdUY09Mosx/k4T9mwiP2xbcDZG6HMF4fsNuX", "ecs.amazonaws.jp");


            IDictionary<string, string> requestParams = new Dictionary<string, String>();
            requestParams["Service"] = "AWSECommerceService";
            requestParams["Version"] = "2009-11-01";
            requestParams["Operation"] = "ItemLookup";
            requestParams["ItemId"] = code;

            if (type == BarcodeType.Isbn)
            {
                requestParams["IdType"] = "ISBN";
            }
            //else if (type == BarcodeType.Jan)
            else
            {
                requestParams["IdType"] = "EAN";
            }
            requestParams["SearchIndex"] = "All";

            string url = helper.Sign(requestParams);
            System.Diagnostics.Debug.WriteLine(url);

            System.Net.WebClient webc = new System.Net.WebClient();

            using (Stream st = webc.OpenRead(url))
            {
                if (st == null) { return false; }
                using (StreamReader sr = new StreamReader(st, Encoding.UTF8))
                {
                    XmlDocument xdoc = new XmlDocument();
                    xdoc.Load(sr);

                    XmlNodeList xlist = xdoc.GetElementsByTagName("Title", @"http://webservices.amazon.com/AWSECommerceService/2009-11-01");

                    if (xlist.Count > 0)
                    {
                        XmlNode xtitle = xlist.Item(0);
                        ControlUtil.SafelyOperated(this, (MethodInvoker)delegate()
                        {
                            cell.DataGridView[ColumnName.isbn, cell.RowIndex].Value = decimal.Parse(code);
                            cell.Value = xtitle.InnerText;
                        });
                        return true;
                    }
                }
            }

            ControlUtil.SafelyOperated(this, (MethodInvoker)delegate()
            {
                cell.DataGridView[ColumnName.isbn, cell.RowIndex].Value = null;
            });

            return false;
        }

#endregion



    }
}
