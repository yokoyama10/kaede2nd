using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace kaede2nd
{
    public partial class LoginForm : Form
    {
        private readonly Action<DatabaseAccess> DbAccessSetter = null;
        private List<AppConfig.Connection> connectList;

        private List<LinkLabel> linkLabels;
        private List<ToolTip> toolTips;

        public LoginForm()
        {
            InitializeComponent();

            this.textBox_backupdest.Text = Program.config.BackupDirectory;

            this.connectList = Program.config.ConnectList;
            if (this.connectList == null) { this.connectList = new List<AppConfig.Connection>(); }
            if (this.connectList.Count == 0)
            {
                this.connectList.Add(new AppConfig.Connection
                {
                    cfgname = "テスト部門",
                    host = "localhost",
                    port = "3306",
                    user = "username",
                    pass = "password",
                    dbname = "database_name",
                    is_readonly = false
                });
            }

            foreach (var c in this.connectList)
            {
                this.comboBox1.Items.Add(c.cfgname);
            }
            this.comboBox1.SelectedIndex = 0;
            this.setTextbox(0);


            this.linkLabels = new List<LinkLabel>();
            this.toolTips = new List<ToolTip>();

            int cnt = 0;
            foreach (var sq in Program.config.RecentSQLiteFile)
            {
                LinkLabel ll = new LinkLabel();
                this.linkLabels.Add(ll);
                ll.Location = new Point(19, 104 + cnt * 22);
                ll.Size = new Size(248, 12);
                ll.Text = sq.name + " [" + System.IO.Path.GetFileName(sq.path) + "]";
                ll.Tag = sq.path;

                ToolTip tt = new ToolTip();
                this.toolTips.Add(tt);
                tt.SetToolTip(ll, sq.path);
                
                ll.Click += new EventHandler(linklabels_Click);
                this.tabPage2.Controls.Add(ll);

                cnt++;
            }

            if (Program.config.DefaultSQLType == AppConfig.SQLType.MySQL)
            {
                this.tabControl1.SelectedTab = this.tabPage1;
            }
            else if (Program.config.DefaultSQLType == AppConfig.SQLType.SQLite)
            {
                this.tabControl1.SelectedTab = this.tabPage2;
            }
        }


        public LoginForm(Action<DatabaseAccess> setter, string formTitle) : this()
        {
            this.DbAccessSetter = setter;
            this.Text = formTitle;

            this.checkBox_MySQL_readonly.Checked = true;
            this.checkBox_MySQL_readonly.Enabled = false;
            this.checkBox_SQLite_readonly.Checked = true;
            this.checkBox_SQLite_readonly.Enabled = false;

            this.label_backupdest.Enabled = false;
            this.textBox_backupdest.Enabled = false;
        }



        private void linklabels_Click(object sender, EventArgs e)
        {
            LinkLabel ll = (LinkLabel)sender;
            string path = (string)((LinkLabel)sender).Tag;

            if (!System.IO.File.Exists(path))
            {
                MessageBox.Show("ファイルが存在しません:\n" + path);
                Program.config.DeleteRecentSQLite(path);
                return;
            }
            this.OpenSQLite(path);
        }

        private void button_SQLite_open_Click(object sender, EventArgs e)
        {
            OpenFileDialog dia = new OpenFileDialog();
            dia.Filter =
                @"SQLite3 データベース (*.sqlite;*.sqlite3)|*.sqlite;*.sqlite3|すべてのファイル (*.*)|*.*";
            dia.Title = "データベースファイルを開く";
            dia.RestoreDirectory = true;
            dia.ShowReadOnly = this.checkBox_SQLite_readonly.Enabled;
            dia.ReadOnlyChecked = this.checkBox_SQLite_readonly.Checked;

            if (dia.ShowDialog() == DialogResult.OK)
            {
                this.checkBox_SQLite_readonly.Checked = dia.ReadOnlyChecked;
                this.OpenSQLite(dia.FileName);
            }
        }

        private void OpenSQLite(string path)
        {
            this.text_host.Text = DatabaseAccess.SQLiteHostString;
            this.text_dbname.Text = path;
            this.checkBox_MySQL_readonly.Checked = this.checkBox_SQLite_readonly.Checked;

            Program.config.DefaultSQLType = AppConfig.SQLType.SQLite;

            this.Connect();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.config.DefaultSQLType = AppConfig.SQLType.MySQL;
            this.Connect();
        }

        private void Connect()
        {

            DatabaseAccess data;
            if (this.DbAccessSetter == null)
            {
                GlobalData.makeInstance(text_host.Text, text_port.Text, text_user.Text, text_pass.Text, text_dbname.Text);
                GlobalData.Instance.data.isReadonly = checkBox_MySQL_readonly.Checked;
                data = GlobalData.Instance.data;
            }
            else
            {
                data = new DatabaseAccess(text_host.Text, text_port.Text, text_user.Text, text_pass.Text, text_dbname.Text);
                data.isReadonly = true;
                this.DbAccessSetter(data);
            }
            
            //Config取得
            var cfg = DbConfig.MakeFromDB(data);

            data.bumonName = cfg.getValue("bumonname");
            data.companyName = cfg.getValue("companyname");
            data.symbolColor = System.Drawing.Color.FromArgb(cfg.getValueInt("symbolcolor_argb"));
            data.bumonTextColor = System.Drawing.Color.FromArgb(cfg.getValueInt("bumontextcolor_argb"));

            if (this.DbAccessSetter == null)
            {
                GlobalData.Instance.windowTitle = "ゆかり姫萌え萌えソフトウェア";

                GlobalData.Instance.barcodePrefix = cfg.getValue("barcodeprefix");
                if (!Globals.isValidBarcodePrefix(GlobalData.Instance.barcodePrefix))
                {
                    MessageBox.Show("バーコード識別子 (barcodeprefix) が不正です。正常なバーコードが印刷されませんよ");
                    GlobalData.Instance.barcodePrefix = "00";
                }
                GlobalData.Instance.itemNameImeOn = cfg.getValueBool("itemname_imeon");
                GlobalData.Instance.enterToTab = cfg.getValueBool("entertotab");

                Program.config.BackupDirectory = this.textBox_backupdest.Text;

                GlobalData.Instance.recentItemForm = new RecentItem();

            }

            if (data.IsSQLite())
            {
                Program.config.AddRecentSQLite(data.companyName, text_dbname.Text);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();

        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (this.DbAccessSetter == null)
            {
                System.Environment.Exit(0);
            }
            else
            {
                this.Close();
            }
        }

        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.DialogResult != DialogResult.OK && this.DbAccessSetter == null)
            {
                System.Environment.Exit(0);
            }
        }

        private void setTextbox(int index)
        {
            AppConfig.Connection c = this.connectList[index];
            this.text_host.Text = c.host;
            this.text_port.Text = c.port;
            this.text_user.Text = c.user;
            this.text_pass.Text = c.pass;
            this.text_dbname.Text = c.dbname;
            this.checkBox_MySQL_readonly.Checked = c.is_readonly;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBox1.SelectedIndex < this.connectList.Count)
            {
                this.setTextbox(this.comboBox1.SelectedIndex);
            }
        }




        private void button_SQLite_create_Click(object sender, EventArgs e)
        {

            SaveFileDialog dia = new SaveFileDialog();
            dia.Filter =
                @"SQLite3 データベース (*.sqlite;*.sqlite3)|*.sqlite;*.sqlite3|すべてのファイル (*.*)|*.*";
            dia.Title = "データベースファイルを新規作成";
            dia.RestoreDirectory = true;
            dia.AddExtension = true;
            dia.OverwritePrompt = false;
            dia.DefaultExt = "sqlite3";

            if (dia.ShowDialog() == DialogResult.OK)
            {
                if (System.IO.File.Exists(dia.FileName))
                {
                    MessageBox.Show("既に存在するファイルの上書きは出来ません。");
                    return;
                }

                try
                {

                    var data = new DatabaseAccess(DatabaseAccess.SQLiteHostString, null, null, null, dia.FileName);
                    
                    IDbConnection con = data.txDataSource.GetConnection();
                    con.Open();

                    IDbCommand com;

                    com = con.CreateCommand();
                    com.CommandText = kaede2nd.Entity.Operator.create_sqlite;
                    com.ExecuteNonQuery();
                    
                    com = con.CreateCommand();
                    com.CommandText = kaede2nd.Entity.ConfigEntity.create_sqlite;
                    com.ExecuteNonQuery();

                    com = con.CreateCommand();
                    com.CommandText = kaede2nd.Entity.Receipt.create_sqlite;
                    com.ExecuteNonQuery();

                    com = con.CreateCommand();
                    com.CommandText = kaede2nd.Entity.Item.create_sqlite;
                    com.ExecuteNonQuery();
                    
                    con.Close();

                }
                catch (Exception cree)
                {
                    MessageBox.Show("新規作成に失敗しました:\n" + cree.Message);
                    return;
                }

                this.OpenSQLite(dia.FileName);
            }
            
        }


    }
}
