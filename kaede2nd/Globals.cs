using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;

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

    
        public enum ColumnType
        {
            Unknown = 0,

            ItemId,
            ReceiptIdButton,
            SellerName,
            ItemName,
            TagPrice,
            IsHenpin,
            ItemReceiveTime,
            ItemComment,
            SellPrice,
            SellTime,
            Isbn,
            Bunsatsu,

            ReceiptReceiveTime,
            ReceiptComment,

            OperatorId,
            OperatorName,
            OperatorComment,
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

        public readonly ColumnType columnType;

        public ColumnInfo(System.Windows.Forms.DataGridViewColumn col)
            : this(col, ColumnType.Unknown)
        { }

        public ColumnInfo(System.Windows.Forms.DataGridViewColumn col, ColumnType type)
        {
            this.col = col;
            this.imeMode = System.Windows.Forms.ImeMode.NoControl;
            this.defaultVal = null;
            this.sortComparison = null;
            this.DBvalueSet = null;
            this.CellvalueSet = null;

            this.columnType = type;
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

        public static Comparison<DataGridViewCell> longCellComparison
                = delegate(DataGridViewCell c1, DataGridViewCell c2)
                {
                    return ((long)c1.Tag).CompareTo((long)c2.Tag);
                };


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

        public static bool isValidBarcodePrefix(string pref)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(pref, @"^\d\d$");
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


        public static bool TryParseBarcode(string s, out uint itemid)
        {
            if (s.Length == 8 && s.StartsWith(GlobalData.Instance.barcodePrefix))
            {
                char check = s[7];

                byte[] di = new byte[7];
                byte cdi;
                try
                {
                    for (int i = 0; i < 7; i++)
                    {
                        if (!char.IsDigit(s[i])) { throw new Exception(); }
                        di[i] = byte.Parse(s.Substring(i, 1));
                    }

                    if (!char.IsDigit(check)) { throw new Exception(); }
                    cdi = byte.Parse(check.ToString());

                    //CheckDigit
                    int c1 = (di[0] + di[2] + di[4] + di[6]) * 3 +
                            di[1] + di[3] + di[5];
                    int c2 = (10 - (c1 % 10)) % 10;

                    if (c2.ToString() != check.ToString())
                    {
                        throw new Exception();
                    }

                }
                catch
                {
                    itemid = 0;
                    return false;
                }

                itemid = uint.Parse(s.Substring(2, 5));
                return true;
            }
            else
            {
                itemid = 0;
                return false;
            }
        }

        public static int CalcAllPages(int itemCount, int itemPerPage)
        {
            double d = (double)itemCount / (double)itemPerPage;
            return (int)Math.Ceiling(d);
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

        public static string ToCSVString(this string str)
        {
            if (str == null) { return "\"\""; /* "" */ }
            return "\"" + str.Replace("\"", "\"\"") + "\"";
        }

        public static void ExtendSelection(this DataGridView dgv)
        {
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

        public readonly static string operatorId = "OpID";
        public readonly static string operatorName = "名前";

        public readonly static string itiran = "一覧";
        public readonly static string uriageGaku = "売上額";
        public readonly static string henkinGaku = "返金額";
        public readonly static string henpinKosuu = "返品個数";


    }

    public class DatabaseAccess
    {
        //DBConnectInfo
        public readonly string db_host, db_port, db_user, db_pass, db_dbname;
        public readonly SQLType db_type;

        public Seasar.Framework.Container.IS2Container container;
        public Seasar.Extension.Tx.Impl.TxDataSource txDataSource;

        public bool isReadonly;

        public string bumonName = "不明な部門";
        public string companyName = "不明な縁日班不明部門";
        public System.Drawing.Color symbolColor = System.Drawing.SystemColors.Window;
        public System.Drawing.Color bumonTextColor = System.Drawing.Color.Black;

        public DatabaseAccess(string host, string port, string user, string pass, string dbname, SQLType dbtype)
        {
            this.db_type = dbtype;

            if (dbtype == SQLType.SQLite)
            {
                string con = "Data Source=\"" + dbname
                        + "\";New=True;Compress=False;Synchronous=Off;UTF8Encoding=True;Version=3";
                this.db_host = host;
                this.db_port = null;
                this.db_user = null;
                this.db_pass = null;
                this.db_dbname = dbname;

                this.container = Seasar.Framework.Container.Factory.S2ContainerFactory.Create("kaede2nd/Dao_sqlite.dicon");

                this.txDataSource = (Seasar.Extension.Tx.Impl.TxDataSource)
                    this.container.GetComponent(typeof(Seasar.Extension.Tx.Impl.TxDataSource), "SqlDataSource");
                this.txDataSource.ConnectionString = con;

            }
            else if ( dbtype == SQLType.MySQL )
            {
                string con = "host=" + host + ";port=" + port
                    + ";user id=" + user + ";password=" + pass
                    + ";database=" + dbname + ";charset=utf8;";

                this.db_host = host;
                this.db_port = port;
                this.db_user = user;
                this.db_pass = pass;
                this.db_dbname = dbname;

                this.container = Seasar.Framework.Container.Factory.S2ContainerFactory.Create("kaede2nd/Dao_mysql.dicon");

                this.txDataSource = (Seasar.Extension.Tx.Impl.TxDataSource)
                    this.container.GetComponent(typeof(Seasar.Extension.Tx.Impl.TxDataSource), "SqlDataSource");
                this.txDataSource.ConnectionString = con;
            }
            else if (dbtype == SQLType.MSSQL)
            {
                string con;
                if (user == "SSPI")
                {
                    con = "Integrated Security=SSPI;Server=" + host + ";Initial Catalog=" + dbname;
                }
                else
                {
                    con = "User ID=" + user + ";Password='" + pass + "';Server=" + host + ";Initial Catalog=" + dbname;
                }

                this.db_host = host;
                this.db_port = port;
                this.db_user = user;
                this.db_pass = pass;
                this.db_dbname = dbname;

                this.container = Seasar.Framework.Container.Factory.S2ContainerFactory.Create("kaede2nd/Dao_mssql.dicon");

                this.txDataSource = (Seasar.Extension.Tx.Impl.TxDataSource)
                    this.container.GetComponent(typeof(Seasar.Extension.Tx.Impl.TxDataSource), "SqlDataSource");
                this.txDataSource.ConnectionString = con;
            }
            else
            {
                throw new Exception("Unknown SQLType");
            }

            this.container.Init();
        }

        public T getIDao<T>()
        {
            return (T)this.container.GetComponent(typeof(T));
        }

    }

    /*
    public static class ItemGroupTools
    {
        public static IEnumerable<IGrouping<string, Item>> GetItemGroup(DatabaseAccess data)
        {
            var iDao = data.getIDao<kaede2nd.Dao.IItemDao>();
            List<Item> items = iDao.GetAll();

            return (from i in items
                              group i by i.item__Receipt.getSellerString())
                              .OrderByDescending(g => ItemGroupTools.countHenpinItems(g));
        }

        public static int countHenpinItems(IGrouping<string, Item> grp)
        {
            return (from i in grp where i.item_sellprice.HasValue == false select i).Count();
        }
    }
    */

    public class GlobalData
    {
        private static GlobalData instance;

        public const int moziWidth = 8;

        public DatabaseAccess data;

        public RecentItem recentItemForm;

        public Form1 mainForm;

        public System.Drawing.Printing.PageSettings pageSettings;
        public System.Drawing.Printing.PrinterSettings printerSettings;

        public System.Drawing.Printing.PageSettings receipt_pageSettings;
        public System.Drawing.Printing.PrinterSettings receipt_printerSettings;

        public string windowTitle = "ゆかり姫萌え萌えソフトウェア";
        public string barcodePrefix = "00"; //数字二文字 \d\d

        public bool itemNameImeOn = true;
        public bool enterToTab = false;


        private GlobalData() { }

        public static T getIDao<T>()
        {
            return (T)GlobalData.Instance.data.getIDao<T>();
        }

        public static void disposeInstance()
        {
            if (instance == null) { return; }
            instance = null;
        }

        public static void makeInstance(string host, string port, string user, string pass, string dbname, SQLType dbtype)
        {
            if (instance == null)
            {
                instance = new GlobalData();

                instance.data = new DatabaseAccess(host, port, user, pass, dbname, dbtype);

                instance.recentItemForm = null;
                instance.mainForm = null;


                instance.pageSettings = new System.Drawing.Printing.PageSettings();
                instance.pageSettings.Color = true;
                instance.printerSettings = new System.Drawing.Printing.PrinterSettings();


                instance.receipt_pageSettings = new System.Drawing.Printing.PageSettings();
                instance.receipt_pageSettings.Color = false;
                instance.receipt_printerSettings = new System.Drawing.Printing.PrinterSettings();
              
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

}
