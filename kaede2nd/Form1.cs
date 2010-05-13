﻿using System;
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
using System.Drawing.Printing;

namespace kaede2nd
{
    public partial class Form1 : MyForm
    {

        private List<Receipt> receiptlist;
      
 
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Form1()
        {
            /*
            System.IO.FileInfo info = new System.IO.FileInfo("app.config"); // アセンブリがdllの場合は".dll.config"
            log4net.Config.XmlConfigurator.Configure(log4net.LogManager.GetRepository(), info);
            */

            logger.Info("MessageTest");

            using (LoginForm lf = new LoginForm())
            {
                if (lf.ShowDialog() == DialogResult.Cancel)
                {
                    Application.Exit();
                    return;
                }
            }

            GlobalData.Instance.mainForm = this;
            //GlobalDataセット完了

            IOperatorDao opDao = GlobalData.getIDao<IOperatorDao>();

            //Form Init...
            InitializeComponent();

            this.Text = GlobalData.Instance.data.bumonName + " - " + GlobalData.Instance.windowTitle;
            this.label_company.Text = GlobalData.Instance.data.companyName;

            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.DefaultCellStyle.BackColor = GlobalData.Instance.data.symbolColor;

            

            this.AddColumn(this.dataGridView1, ColumnType.ReceiptIdButton);
            this.AddColumn(this.dataGridView1, ColumnType.SellerName);
            this.AddColumn(this.dataGridView1, ColumnType.ReceiptReceiveTime);
            this.AddColumn(this.dataGridView1, ColumnType.ReceiptComment);

            /*
             
            ColumnInfo colinfo;
            DataGridViewColumn col;
             
            
            colinfo = this.newColumn<DataGridViewTextBoxColumn>(ColumnName.kosuu);
            col = colinfo.col;
            col.ValueType = typeof(UInt32);
            col.ReadOnly = true;
            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            col.Width = GlobalData.moziWidth * 5;
            this.dataGridView1.Columns.Add(col);
            */



            /*
            colinfo = this.newColumn<DataGridViewTextBoxColumn>("受付者");
            col = colinfo.col;
            col.ValueType = typeof(string);
            col.ReadOnly = true;
            col.Visible = false;
            col.Width = GlobalData.moziWidth * 11;
            this.dataGridView1.Columns.Add(col);
            */

            /*
            colinfo = this.newColumn<DataGridViewCheckBoxColumn>(ColumnName.henkinZumi);
            colinfo.DBvalueSet = delegate(object obj, object val) { ((Receipt)obj).receipt_payback = Globals.getNullableBoolFromCheckboxString((string)val); };
            colinfo.CellvalueSet = delegate(DataGridViewCell cell, object obj)
            {
                cell.Value = Globals.getCheckboxString(((Receipt)obj).receipt_payback);
            };
            col = colinfo.col;
            col.ValueType = typeof(string);
            ((DataGridViewCheckBoxColumn)col).ThreeState = true;
            ((DataGridViewCheckBoxColumn)col).TrueValue = Globals.check_trueVal;
            ((DataGridViewCheckBoxColumn)col).FalseValue = Globals.check_falseVal;
            ((DataGridViewCheckBoxColumn)col).IndeterminateValue = Globals.check_unkVal;
            col.Width = GlobalData.moziWidth * 8;
            col.SortMode = DataGridViewColumnSortMode.Automatic;
            this.dataGridView1.Columns.Add(col);
            */

            

            /*
            DataGridViewComboBoxColumn combocol = new DataGridViewComboBoxColumn();
            combocol.ValueType = typeof(UInt32?);
            combocol.DataPropertyName = "_受付者";
            combocol.HeaderText = "受付者";

            DataTable opTable = new DataTable("___OperatorTable");
            opTable.Columns.Add("Display", typeof(string));
            opTable.Columns.Add("Value", typeof(UInt32?));
            opTable.Rows.Add("不明", null);
            foreach (var op in GlobalData.Instance.operators)
            {
                opTable.Rows.Add(op.operator_name, op.operator_id);
            }

            combocol.ValueMember = "Value";
            combocol.DisplayMember = "Display";

            this.dataGridView1.Columns.Add(combocol);
            */

            this.addDGVEvents(this.dataGridView1);
            this.button3.Enabled = !GlobalData.Instance.data.isReadonly;
            this.新Receiptを追加UToolStripMenuItem.Enabled = !GlobalData.Instance.data.isReadonly;
            this.品番カウンタをセットしなおすToolStripMenuItem.Enabled = !GlobalData.Instance.data.isReadonly;
            this.売却ウィンドウSToolStripMenuItem.Enabled = !GlobalData.Instance.data.isReadonly;
            this.監査ウィンドウWToolStripMenuItem.Enabled = !GlobalData.Instance.data.isReadonly;

            this.renewReceipts();

        }


        private void Form1_Load(object sender, EventArgs e)
        {
            GlobalData.Instance.recentItemForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.renewReceipts();
        }

        public void DoRefresh()
        {
            System.Threading.Thread t = new System.Threading.Thread(
                (delegate() {
                    System.Threading.Thread.Sleep(300);
                    this.Invoke((MethodInvoker)delegate() { this.renewReceipts(); });
                }));
            t.Start();
        }

        //[Receipts]
        private void renewReceipts()
        {

            this.dataGridView1.Enabled = false;
            this.dataGridView1.Rows.Clear();

            IReceiptDao receiptDao = GlobalData.getIDao<IReceiptDao>();
            IItemDao itemDao = GlobalData.getIDao<IItemDao>();


            this.receiptlist = receiptDao.GetAll();
            
            foreach (Receipt r in this.receiptlist)
            {
                DataGridViewRow row = this.dataGridView1.Rows[this.dataGridView1.Rows.Add()];

                this.setRowValue(row, r);

                //個数 - のこす
                //row.Cells["個数"].Value = itemDao.CountReceiptItem(r.receipt_id);
            }

            dataGridView1.Sort(dataGridView1.Columns[ColumnName.hyouBan], ListSortDirection.Ascending);
            dataGridView1.Enabled = true;

            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.Rows.Count - 1;
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ReceiptForm rf = new ReceiptForm();
            rf.Show();
        }

        //[Print]
        private void button4_Click(object sender, EventArgs e)
        {
            ControlUtil.DGV_ExSelect(this.dataGridView1);
            this.PrintSelections();
        }

        private void PrintSelections()
        {

            var rows = this.dataGridView1.SelectedRows;

            List<Item> items = new List<Item>();
            var itemDao = GlobalData.getIDao<IItemDao>();

            if (rows.Count == 0) { return; }
            for (int i = 0; i < rows.Count; i++)
            {
                items.AddRange(itemDao.GetReceiptItem((UInt32)rows[i].Cells[ColumnName.hyouBan].Value));
            }

            ItemsPrintDocument.PrintItems(items);
        }


        //[AllItems]
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                Form_ItemList f = new Form_ItemList(delegate()
                {
                    var itemDao = GlobalData.getIDao<IItemDao>();
                    return itemDao.GetAll();
                }, "全商品リスト", "allItems");
                f.Show();
            }
            catch (Exception excep)
            {
                MessageBox.Show("全商品ウィンドウが生成できませんでした: " + excep.Message);
            }
        }


        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (dgv.Enabled == false) { return; }

            if (this.receiptlist == null) { throw new InvalidOperationException(); }

            Receipt r = (from rs in this.receiptlist
                         where rs.receipt_id == (UInt32)dgv[ColumnName.hyouBan, e.RowIndex].Value
                         select rs)
                        .Single();

            
            this.changeDBdata(dgv.Columns[e.ColumnIndex].Name, r, dgv[e.ColumnIndex, e.RowIndex].Value);


            IReceiptDao receiptDao = GlobalData.getIDao<IReceiptDao>();

            try
            {
                receiptDao.Update(r);
            }
            catch (Exception ex)
            {
                MessageBox.Show("更新できませんでした: " + ex.ToString());
                return;
            }

            System.Diagnostics.Debug.WriteLine
                ("Receipt: (index: " + r.receipt_id.ToString() + ") Updated.");
        }


        private void kaedeOutput(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = GlobalData.Instance.data.bumonName + ".kae";
            sfd.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath);
            sfd.Filter = "楓ちゃん萌え萌え filez (*.kae)|*.kae";
            sfd.RestoreDirectory = true;

            if (sfd.ShowDialog() != DialogResult.OK) { return; }


            const string kae_header = "Kaede chan moemoe software by 51st. ennichi han";
            const int kae_file_version = 1003;
            const int kae_version = 1005; //kaede_rg005

            IItemDao itemDao = GlobalData.getIDao<IItemDao>();
            List<Item> items = itemDao.GetAll();

            Dictionary<string, int> exSellers = new  Dictionary<string,int>();
            int exnum = 1;
            foreach (Item it in items)
            {
                if (it.item__Receipt.receipt_seller == Receipt.seller_EXT)
                {
                    string exname = it.item__Receipt.receipt_seller_exname;
                    if (! exSellers.ContainsKey(exname))
                    {
                        exSellers.Add(exname, exnum);
                        exnum++;
                    }
                }
            }

            try
            {

                using (Stream fs = sfd.OpenFile())
                {
                    BinaryWriter bw = new BinaryWriter(fs);

                    bw.Write(Encoding.ASCII.GetBytes(kae_header));
                    bw.Write(kae_file_version);
                    bw.Write(kae_version);

                    //OB
                    bw.Write(exSellers.Count);
                    foreach (var exs in exSellers)
                    {
                        bw.WriteStringSJIS(exs.Key);
                    }

                    //teacher
                    bw.Write(0);

                    //genre
                    bw.Write(0);

                    //shape
                    bw.Write(0);

                    //item
                    bw.Write(items.Count);

                    foreach (Item it in items)
                    {
                        //ID
                        bw.Write((Int32)it.item_id);


                        //Seller
                        Int32 seller;

                        string sellerstr = it.item__Receipt.receipt_seller;

                        switch (sellerstr)
                        {
                            case Receipt.seller_EXT:
                                {
                                    seller = 0x10000000 + exSellers[it.item__Receipt.receipt_seller_exname];
                                    break;
                                }
                            case Receipt.seller_LAGACY:
                                {
                                    seller = 0x30000000;
                                    break;
                                }
                            case Receipt.seller_DONATE:
                                {
                                    seller = 0x40000000;
                                    break;
                                }
                            default:
                                {
                                    int nen = int.Parse(sellerstr.Substring(0, 1));
                                    string kumi = sellerstr.Substring(1, 1);
                                    int kumi_i;
                                    if (Globals.isChugaku(kumi))
                                    {
                                        kumi_i = Globals.getChugakuClassNum(kumi);
                                    }
                                    else
                                    {
                                        nen += 3;
                                        kumi_i = int.Parse(kumi);
                                    }
                                    int ban = int.Parse(sellerstr.Substring(2, 2));

                                    seller = 0x01000000 * nen + 0x00100000 * kumi_i + ban;
                                    break;
                                }
                        }

                        bw.Write(seller);

                        bw.WriteStringSJIS(it.item_name);
                        bw.WriteStringSJIS(it.item_comment);

                        bw.Write(it.item_tagprice);
                        bw.Write(it.item_sellprice.ToKaedeInt());

                        //Genre
                        bw.Write((Int16)0);
                        bw.Write((Int16)0);
                        bw.Write((Int16)0);
                        //Shape
                        bw.Write((Int16)0);

                        //is_sold
                        bw.Write(it.item_sellprice.HasValue.ToKaedeBool());
                        //is_returned
                        bw.Write((SByte)0);
                        //to_be_return
                        bw.Write(it.item_return.ToKaedeBool());
                        //to_be_discount
                        bw.Write(it.item_tataki.ToKaedeBool());

                        if (it.item__Receipt.receipt_time.HasValue)
                        {
                            bw.Write(it.item__Receipt.receipt_time.Value.ToUnixTime());
                        }
                        else
                        {
                            bw.Write((UInt32)0);
                        }

                        if (it.item_selltime.HasValue)
                        {
                            bw.Write(it.item_selltime.Value.ToUnixTime());
                        }
                        else
                        {
                            bw.Write((UInt32)0);
                        }

                        //Item_scheduled_date
                        bw.Write((SByte)0);
                        //is_by_auction
                        bw.Write((SByte)0);

                        //refund_rate
                        bw.Write((Int32)(-1));

                    }

                    //cash
                    bw.Write(888);

                    //refund_rate
                    bw.Write(100);

                    bw.Flush();
                    fs.Close();

                }

                using (FileStream fs = new FileStream(sfd.FileName, FileMode.Open, FileAccess.ReadWrite))
                {
                    uint not_crc = 0xffffffff;

                    BinaryReader br = new BinaryReader(fs);

                    while (fs.Position < fs.Length)
                    {
                        byte data = br.ReadByte();
                        not_crc = GlobalData.Instance.crcTable[((not_crc) ^ (data)) & 0xff] ^ ((not_crc) >> 8);
                    }


                    BinaryWriter bw = new BinaryWriter(fs);
                    bw.Write(~not_crc);
                    bw.Flush();


                    fs.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("kaeファイルの作成エラー: " + ex.ToString());
            }
        }

        private void ログイン画面に戻るLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GlobalData.disposeInstance();
            Program.continueProg = true;

            this.Close();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            //mysqldump

            const string mysqldumpPath = @"mysqldump";
            const string dumpDest = @"backup";

            string dumpFullPath = Path.Combine(Directory.GetCurrentDirectory(), dumpDest);
            if (new DirectoryInfo(dumpFullPath).Exists != true)
            {
                MessageBox.Show("バックアップディレクトリが見つかりません");
            }

            //FIXME: ディレクトリトラバーサルとか
            string dumpdbDest = Path.Combine(dumpFullPath, GlobalData.Instance.data.db_dbname);
            DirectoryInfo dbdestInfo = new DirectoryInfo(dumpdbDest);
            if (dbdestInfo.Exists != true)
            {
                dbdestInfo.Create();
            }


            string destdest = GlobalData.Instance.data.db_dbname + "_" + DateTime.Now.ToString("yyyyMMddHHmm") + ".sql";
            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();
            //psi.RedirectStandardOutput = true;
            psi.CreateNoWindow = true;
            psi.UseShellExecute = false;
            psi.FileName = mysqldumpPath;
            psi.Arguments = "--host=" + GlobalData.Instance.data.db_host + " --port=" + GlobalData.Instance.data.db_port +
                " --user=" + GlobalData.Instance.data.db_user + " --password=" + GlobalData.Instance.data.db_pass +
                " --dump-date --skip-lock-tables --result-file=\"" + Path.Combine(dumpdbDest, destdest) + "\" " + GlobalData.Instance.data.db_dbname;

            System.Diagnostics.Process p = System.Diagnostics.Process.Start(psi);
            p.WaitForExit();

            System.IO.FileInfo fi = new System.IO.FileInfo(Path.Combine(dumpdbDest, destdest));

            string loc = dumpDest + "\\" + GlobalData.Instance.data.db_dbname + "\\" + destdest;
            try
            {
                MessageBox.Show(fi.Length.ToString("#,##0") + " バイトを " + loc + " にバックアップしました。サイズが小さすぎると失敗（かも）", "バックアップ完了");
            }
            catch
            {
                MessageBox.Show(loc + "のファイルサイズが取得できませんでした。バックアップに失敗した（かも）。", "バックアップ失敗");
            }
            /*
            System.Threading.Thread t = new System.Threading.Thread(
                delegate() { 


                    
                    using (StreamWriter sw = new StreamWriter(Path.Combine(dumpdbDest, destdest), false))
                    {
                        System.Diagnostics.Process p = System.Diagnostics.Process.Start(psi);
                        while (!p.StandardOutput.EndOfStream)
                        {
                            char[] buf = new char[4096];
                            int ret = p.StandardOutput.Read(buf, 0, 4096);
                            sw.Write(buf, 0, ret);
                        }

                        sw.Close();

                        p.WaitForExit();

                    }
                    

                    //p.StandardOutput
                }
            );

            t.Start();
            */
        }

        private void 選択中の票を印刷PToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ControlUtil.DGV_ExSelect(this.dataGridView1);
            this.PrintSelections();
        }

        private void ページ設定UToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PageSetupDialog psd = new PageSetupDialog();
            psd.EnableMetric = true; //cf. KB814355 et http://dobon.net/vb/dotnet/graphics/pagesetupdialogbug.html
            psd.PageSettings = GlobalData.Instance.pageSettings;
            psd.PrinterSettings = GlobalData.Instance.printerSettings;
            psd.AllowMargins = false;
            psd.ShowDialog();
        }

        private void 品番カウンタをセットしなおすToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            IDbConnection con = GlobalData.Instance.txDataSource.Instance.GetConnection();
            con.Open();
            IDbCommand com = con.CreateCommand();
            com.CommandText = "ALTER TABLE item AUTO_INCREMENT=0";
            com.ExecuteNonQuery();
            */

            var idao = GlobalData.getIDao<IItemDao>();
            idao.ResetItemIdNumber();
            MessageBox.Show("品番カウンタをリセットしました");
        }

        private void 新Receiptを追加UToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.button3.PerformClick();
        }

        private void 最新の情報に更新RToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.button1.PerformClick();
        }

        private void 最近追加された商品リストLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GlobalData.Instance.recentItemForm.Show();
        }

        private void バージョン情報ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new Version()).ShowDialog();
        }

        private void cSVで出力CToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_ItemList f = new Form_ItemList(delegate()
            {
                var itemDao = GlobalData.getIDao<IItemDao>();
                return itemDao.GetAll();
            }, "全商品", "allItems");

            f.outCSV();
        }

        private void 終了XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 売却ウィンドウSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f is SellForm)
                {
                    f.Activate();
                    return;
                }
            }

            (new SellForm()).Show();
        }

        private void オペレーターIDを管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f is OperatorList)
                {
                    f.Activate();
                    return;
                }
            }

            (new OperatorList()).Show();
        }

        private void 監査ウィンドウWToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f is KansaForm)
                {
                    f.Activate();
                    return;
                }
            }

            (new KansaForm()).Show();

        }

        private void 返金返品ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f is HenkinForm)
                {
                    f.Activate();
                    return;
                }
            }

            (new HenkinForm()).Show();

            /*
            IItemDao iDao = GlobalData.getIDao<IItemDao>();
            List<Item> items = iDao.GetAll();

            var grps = (from i in items
                        group i by i.item__Receipt.getSellerString());
            foreach (var g in grps)
            {
                System.Diagnostics.Debug.Write(g.Key);
                System.Diagnostics.Debug.Write(",");
                System.Diagnostics.Debug.Write(
                    (from i in g where i.item_sellprice.HasValue select i.item_sellprice.Value).Sum(a => (long)a).ToString()
                    );
                System.Diagnostics.Debug.Write(",");
                System.Diagnostics.Debug.Write(
                    (from i in g where i.item_sellprice.HasValue == false select i).Count().ToString()
                    );
                System.Diagnostics.Debug.Write("\n");
            }
             * */
                        
        }


        private void 各部門の返金額合算ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f is GassanForm)
                {
                    f.Activate();
                    return;
                }
            }

            Form ff = new GassanForm();
            if (ff.IsDisposed) { return; }
            ff.Show();
        }

        private void タグ印刷ごとにダイアログを表示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GlobalData.Instance.showPrintDialog = this.タグ印刷ごとにダイアログを表示ToolStripMenuItem.Checked;
        }

        
    }

}
