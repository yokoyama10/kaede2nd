﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Xml;
using AmazonProductAdvtApi;
using kaede2nd.Entity;

namespace kaede2nd
{

    public class MyDataGridView : System.Windows.Forms.DataGridView
    {

        public bool enterToTab;

        public MyDataGridView()
        {
            this.enterToTab = false;
        }

        protected override bool ProcessDataGridViewKey(System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                
                this.ProcessEnterKey(e.KeyData);
                if (this.CurrentCell is DataGridViewTextBoxCell &&
                    this.CurrentCell.IsInEditMode) { return true; }

                if ( this.ColumnCount >=2 && this.SelectedCells.Count >= 1 )
                {
                    int rindex = this.SelectedCells[0].RowIndex;
                    this.ClearSelection();
                    this[1, rindex].Selected = true;
                    this.CurrentCell = this[1, rindex];
                }

                return true;
            }

            return base.ProcessDataGridViewKey(e);
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == System.Windows.Forms.Keys.Enter)
            {
                if (this.enterToTab && this.CurrentCell.OwningColumn.Name == ColumnName.shouhinMei)
                {
                    this.ProcessTabKey(Keys.Tab);
                    return true;
                }

                this.ProcessEnterKey(keyData);
                if (this.CurrentCell.IsInEditMode) { return true; }

                if (this.ColumnCount >= 2 && this.SelectedCells.Count >= 1)
                {
                    int rindex = this.SelectedCells[0].RowIndex;
                    this.ClearSelection();
                    this[1, rindex].Selected = true;
                    this.CurrentCell = this[1, rindex];
                }

                return true;
            }
            return base.ProcessDialogKey(keyData);
        }
    }

    public class MyForm : System.Windows.Forms.Form
    {
        protected System.Collections.Generic.Dictionary<string, ColumnInfo> colinfos;
        protected WebClient webc;

        public MyForm()
        {
            this.colinfos = new Dictionary<string, ColumnInfo>();
            this.webc = new WebClient();
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
                        ControlUtil.SafelyOperated(this, (MethodInvoker)delegate() {
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

    public class ColumnInfo
    {
        public System.Windows.Forms.DataGridViewColumn col;
        public System.Windows.Forms.ImeMode imeMode;
        public object defaultVal;

        public Comparison<DataGridViewCell> sortComparison;

        public delegate void DBvalueSetDelegate(object obj, object value);
        public DBvalueSetDelegate DBvalueSet;

        public delegate void CellValueSetDelegate(DataGridViewCell cell, object obj);
        public CellValueSetDelegate CellvalueSet;

        public ColumnInfo(System.Windows.Forms.DataGridViewColumn col)
        {
            this.col = col;
            this.imeMode = System.Windows.Forms.ImeMode.NoControl;
            this.defaultVal = null;
            this.sortComparison = null;
            this.DBvalueSet = null;
            this.CellvalueSet = null;
        }


        //static

        public static Comparison<DataGridViewCell> DateTimeCellComp =
            delegate(DataGridViewCell c1, DataGridViewCell c2)
            {
                DateTime cc1 = DateTime.MinValue;
                DateTime cc2 = DateTime.MaxValue;
                if (c1.Tag is DateTime) { cc1 = (DateTime)c1.Tag; }
                if (c2.Tag is DateTime) { cc2 = (DateTime)c2.Tag; }
                return DateTime.Compare(cc1, cc2);
            };

    }

    public static class Globals
    {
        public const string check_trueVal = "TRUE";
        public const string check_falseVal = "FALSE";
        public const string check_unkVal = "UNK";


        public static string getTimeString(DateTime? dt)
        {
            if (dt.HasValue)
            {
                return dt.Value.ToString("MM/dd HH:mm");
            }
            else
            {
                return "不明";
            }
        }

        public static System.Windows.Forms.CheckState getCheckboxCheckState(bool? bl)
        {
            if (!bl.HasValue) { return System.Windows.Forms.CheckState.Indeterminate; }

            if (bl.Value == true)
            {
                return System.Windows.Forms.CheckState.Checked;
            }
            else
            {
                return System.Windows.Forms.CheckState.Unchecked;
            }
        }

        public static bool? getBoolFromCheckState(System.Windows.Forms.CheckState cs)
        {
            switch (cs)
            {
                case System.Windows.Forms.CheckState.Indeterminate: return null;
                case System.Windows.Forms.CheckState.Checked: return true;
                case System.Windows.Forms.CheckState.Unchecked: return false;
                default: return null; //Exception?
            }
        }

        public static string getCheckboxString(bool? bl)
        {
            if (bl.HasValue)
            {
                return Globals.getCheckboxString(bl.Value);
            }
            else
            {
                return Globals.check_unkVal;
            }
        }

        public static string getCheckboxString(bool bl)
        {
            return (bl ? Globals.check_trueVal : Globals.check_falseVal);
        }

        public static string getCheckboxStringNoNull(bool? bl, bool defaultVal)
        {
            bool v;
            if (bl.HasValue) { v = bl.Value; }
            else { v = defaultVal; }

            return Globals.getCheckboxString(v);

        }

        public static bool getBoolFromCheckboxString(string str)
        {
            switch (str)
            {
                case Globals.check_trueVal: { return true; }
                case Globals.check_falseVal: { return false; }
                default: { throw new InvalidOperationException("不明なThreeState値です"); }
            }
        }

        public static bool? getNullableBoolFromCheckboxString(string str)
        {
            switch (str)
            {
                case Globals.check_trueVal: { return true; }
                case Globals.check_falseVal: { return false; }
                case Globals.check_unkVal: { return null; }
                default: { throw new InvalidOperationException("不明なThreeState値です"); }
            }
        }

        public static bool isChugaku(string kumi)
        {
            if (kumi == "A" || kumi == "B" || kumi == "C") { return true; }
            return false;
        }

        public static int getChugakuClassNum(string kumi)
        {
            switch (kumi)
            {
                case "A": return 1;
                case "B": return 2;
                case "C": return 3;
                default: throw new ArgumentException("kumi: must be A-C.");
            }
        }

        public static UInt32? convToUInt32(object obj)
        {
            if (obj == null) { return null; }
            if (obj == System.DBNull.Value) { return null; }
            return (UInt32)obj;
        }

        public static decimal? convToDecimal(object obj)
        {
            if (obj == null) { return null; }
            if (obj == System.DBNull.Value) { return null; }
            return (decimal)obj;
        }

        public static string strNoNull(string str)
        {
            if (str == null) { return String.Empty; }
            return str;
        }

        public static bool setCellByDateTimeN(DataGridViewCell cell, DateTime? dt, string defaultVal)
        {
            if (dt.HasValue)
            {
                cell.Tag = dt.Value;
                cell.Value = Globals.getTimeString(dt);
                return true;
            }
            else
            {
                cell.Tag = DateTime.MinValue;
                cell.Value = defaultVal;
                return false;
            }
        }

    }

    public static class KaedeExMethods
    {
        public static void WriteStringSJIS(this BinaryWriter bw, string str)
        {
            if (str != null)
            {
                bw.Write(Encoding.GetEncoding(932).GetBytes(str));
            }
            bw.Write((byte)0);
        }

        public static Int32 ToKaedeInt(this UInt32? val)
        {
            if (val.HasValue) { return (Int32)val.Value; }
            else { return 0; }
        }

        public static SByte ToKaedeBool(this bool b)
        {
            return (SByte)(b ? 1 : 0);
        }

        public static SByte ToKaedeBool(this bool? val)
        {
            if (val.HasValue) { return val.Value.ToKaedeBool(); }
            else { return 0; }
        }

        public static UInt32 ToUnixTime(this DateTime dt)
        {
            return (UInt32)(dt.ToFileTimeUtc() / 10000000 - 11644506000L);
        }

    }

    public static class ControlUtil
    {
        public static object SafelyOperated(Control context, Delegate process)
        {
            return ControlUtil.SafelyOperated(context, process, null);
        }

        public static object SafelyOperated(Control context, Delegate process, params object[] args)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (process == null)
            {
                throw new ArgumentNullException("process");
            }

            if (!(context.IsHandleCreated))
            {
                return null;
            }
            if (context.InvokeRequired)
            {
                return context.Invoke(process, args);
            }
            else
            {
                return process.DynamicInvoke(args);
            }
        }


        public static void DGV_ExSelect(DataGridView dgv)
        {
            if (dgv == null) { throw new NullReferenceException(); }

            if (dgv.SelectedRows.Count == 0)
            {
                DataGridViewSelectedCellCollection cells = dgv.SelectedCells;
                for (int i = 0; i < cells.Count; i++)
                {
                    dgv.Rows[cells[i].RowIndex].Selected = true;
                }
            }
        }

    }

    public static class ColumnName
    {
        public readonly static string shouhinMei = "商品名";
        public readonly static string shinaBan = "品番";
        public readonly static string isbn = "ISBN";
        public readonly static string teika = "定価";
        public readonly static string tataki = "たたき";
        public readonly static string henpin = "返品";
        public readonly static string comment = "コメント";
        public readonly static string baika = "売価";
        public readonly static string bunsatsu = "分冊";
        public readonly static string hanbaiNitizi = "販売日時";
        public readonly static string hanbaiComment = "販売コメント";

        public readonly static string hyouBan = "票番";
        public readonly static string shuppinsha = "出品者";
        public readonly static string kosuu = "個数";
        public readonly static string uketsukeNitizi = "受付日時";
        public readonly static string henkinZumi = "返金済";

    }

    public class GlobalData
    {
        private static GlobalData instance;

        public const int moziWidth = 8;

        //DBConnectInfo
        public string db_host, db_port, db_user, db_pass, db_dbname;

        public Seasar.Framework.Container.IS2Container container;
        public Seasar.Extension.Tx.Impl.TxDataSource txDataSource;
        public List<kaede2nd.Entity.Operator> operators;
        public List<kaede2nd.Entity.External> externals;
        public UInt32 nowOperator;
        //public System.Data.DataTable operators;

        public RecentItem recentItemForm;

        public Form1 mainForm;

        public System.Drawing.Printing.PageSettings pageSettings;
        public System.Drawing.Printing.PrinterSettings printerSettings;

        public System.Drawing.Printing.PageSettings receipt_pageSettings;
        public System.Drawing.Printing.PrinterSettings receipt_printerSettings;

        public string bumonName = "不明な部門";
        public System.Drawing.Color symbolColor = System.Drawing.SystemColors.Window;
        public string windowTitle = "ゆかり姫萌え萌えソフトウェア";

        public bool itemNameImeOn = true;
        public bool enterToTab = false;

        public uint[] crcTable;


        private GlobalData() { }

        public static T getIDao<T>()
        {
            return (T)GlobalData.Instance.container.GetComponent(typeof(T));
        }


        public static void makeInstance(string host, string port, string user, string pass, string dbname)
        {
            GlobalData.makeInstance("host=" + host + ";port=" + port
                + ";user id=" + user + ";password=" + pass
                + ";database=" + dbname + ";charset=utf8;");

            GlobalData.Instance.db_host = host;
            GlobalData.Instance.db_port = port;
            GlobalData.Instance.db_user = user;
            GlobalData.Instance.db_pass = pass;
            GlobalData.Instance.db_dbname = dbname;
        }

        public static void disposeInstance()
        {
            if (instance == null) { return; }
            instance = null;
        }

        private static void makeInstance(string connectStr)
        {
            if (instance == null)
            {
                instance = new GlobalData();
                instance.container = Seasar.Framework.Container.Factory.S2ContainerFactory.Create("kaede2nd/Dao.dicon");

                instance.txDataSource = (Seasar.Extension.Tx.Impl.TxDataSource)
                    instance.container.GetComponent(typeof(Seasar.Extension.Tx.Impl.TxDataSource), "SqlDataSource");
                instance.txDataSource.ConnectionString = connectStr;

                instance.container.Init();

                instance.nowOperator = 2;
                /*instance.operators = new System.Data.DataTable();
                instance.operators.Columns.Add("ID", typeof(UInt32));
                instance.operators.Columns.Add("NAME", typeof(string));
                instance.operators.Columns.Add("COMMENT", typeof(string));*/


                var opeDao = GlobalData.getIDao<kaede2nd.Dao.IOperatorDao>();
                instance.operators = opeDao.GetAll();
                instance.operators.Insert(0, new kaede2nd.Entity.Operator() { operator_id = 0, operator_name = "不明" });

                var extDao = GlobalData.getIDao<kaede2nd.Dao.IExternalDao>();
                instance.externals = extDao.GetAll();
                instance.externals.Sort(delegate(kaede2nd.Entity.External x, kaede2nd.Entity.External y)
                {
                    return String.Compare(x.external_type, y.external_type);
                });

                instance.recentItemForm = new RecentItem();
                instance.mainForm = null;


                instance.pageSettings = new System.Drawing.Printing.PageSettings();
                instance.pageSettings.Color = true;
                instance.printerSettings = new System.Drawing.Printing.PrinterSettings();


                instance.receipt_pageSettings = new System.Drawing.Printing.PageSettings();
                instance.receipt_pageSettings.Color = false;
                instance.receipt_printerSettings = new System.Drawing.Printing.PrinterSettings();


                instance.crcTable = new uint[256];

                for (uint n = 0; n < 256; n++)
                {
                    uint c = n;
                    for (uint k = 0; k < 8; k++)
                    {
                        if ((c & 1) != 0)
                        {
                            c = 0xedb88320U ^ (c >> 1);
                        }
                        else
                        {
                            c = c >> 1;
                        }
                    }
                    instance.crcTable[n] = c;
                }
                //instance.externals.Insert(0, new kaede2nd.Entity.External() { external_id = 0, external_name = "新規..." });

                /*foreach (var op in oplist)
                {
                    instance.operators.Rows.Add(op.operator_id, op.operator_name, op.operator_comment);
                }*/
            }
        }

        public static GlobalData Instance
        {
            get
            {
                if (instance == null) { throw new Exception("先にmakeInstanceをしてね"); }
                return instance;
            }
        }

    }

    public class KaedeFileStream : FileStream
    {
        public uint not_crc;

        public KaedeFileStream(string path, FileMode mode)
            : base(path, mode)
        {
            this.not_crc = 0xffffffff;
        }

        public uint getCrc()
        {
            return ~this.not_crc;
        }

        public override void WriteByte(byte value)
        {
            this.not_crc = GlobalData.Instance.crcTable[((this.not_crc) ^ (value)) & 0xff] ^ ((this.not_crc) >> 8);
            base.WriteByte(value);
        }
    }

}
