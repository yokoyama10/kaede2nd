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
    public partial class MyForm : Form
    {

        protected System.Collections.Generic.Dictionary<string, ColumnInfo> colinfos;
        protected System.Net.WebClient webc;   

        public MyForm()
        {
            InitializeComponent();

            this.colinfos = new Dictionary<string, ColumnInfo>();
            this.webc = new System.Net.WebClient();
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


        //for DGV event
        protected void addDGVEvents(DataGridView dgv)
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
