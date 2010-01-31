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

        public LoginForm()
        {
            InitializeComponent();

            /*
            text_host.Text = "halfed-note";
            text_port.Text = "3306";
            text_user.Text = "ennichi";
            text_pass.Text = "itsuki";
            this.comboBox1.SelectedIndex = 0;
            */

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
                    dbname = "database_name"
                });
            }

            foreach (var c in this.connectList)
            {
                this.comboBox1.Items.Add(c.cfgname);
            }
            this.comboBox1.SelectedIndex = 0;
            this.setTextbox(0);
        }

        public LoginForm(Action<DatabaseAccess> setter, string formTitle) : this()
        {
            this.DbAccessSetter = setter;
            this.Text = formTitle;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
            GlobalData.makeInstance("host=" + text_host.Text + ";port=" + text_port.Text 
                + ";user id=" + text_user.Text + ";password=" + text_pass.Text
                + ";database=" + text_dbname.Text + ";charset=utf8;");
            */

            DatabaseAccess data;
            if (this.DbAccessSetter == null)
            {
                GlobalData.makeInstance(text_host.Text, text_port.Text, text_user.Text, text_pass.Text, text_dbname.Text);
                data = GlobalData.Instance.data;
            }
            else
            {
                data = new DatabaseAccess(text_host.Text, text_port.Text, text_user.Text, text_pass.Text, text_dbname.Text);
                this.DbAccessSetter(data);
            }


            /*
            if (this.comboBox1.SelectedIndex == 0)
            {
                itemNameImeOn = true;
                enterToTab = false;
                data.bumonName = "ガラクタ部門";
                data.symbolColor = Color.MistyRose; //-6943
            }
            else if (this.comboBox1.SelectedIndex == 1)
            {
                itemNameImeOn = false;
                enterToTab = true;
                data.bumonName = "古本部門";
                data.symbolColor = Color.LightCyan; //-2031617
            }
            else if (this.comboBox1.SelectedIndex == 2)
            {
                itemNameImeOn = true;
                enterToTab = false;
                data.bumonName = "テスト部門";
            }
            else if (this.comboBox1.SelectedIndex == 3)
            {
                itemNameImeOn = true;
                enterToTab = false;
                data.bumonName = "松代実験場";
            }
            else
            {
                itemNameImeOn = true;
                enterToTab = false;
                data.bumonName = "不明な部門";
            }
            */

            //Config取得
            var icdao = data.getIDao<kaede2nd.Dao.IConfigDao>();
            var lc = icdao.Get();
            kaede2nd.Entity.ConfigEntity cfg;
            if (lc.Count == 0)
            {
                cfg = new kaede2nd.Entity.ConfigEntity();
                icdao.Insert(cfg);
            }
            else
            {
                cfg = lc.Single();
            }
            data.bumonName = cfg.config_bumonname;
            data.companyName = cfg.config_companyname;
            data.symbolColor = System.Drawing.Color.FromArgb(cfg.config_symbolcolor_argb);


            if (this.DbAccessSetter == null)
            {
                GlobalData.Instance.windowTitle = "ゆかり姫萌え萌えソフトウェア";

                GlobalData.Instance.barcodePrefix = cfg.config_barcodeprefix;
                GlobalData.Instance.itemNameImeOn = cfg.config_itemname_imeon;
                GlobalData.Instance.enterToTab = cfg.config_entertotab;

            }

        }


        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void setTextbox(int index)
        {
            AppConfig.Connection c = this.connectList[index];
            this.text_host.Text = c.host;
            this.text_port.Text = c.port;
            this.text_user.Text = c.user;
            this.text_pass.Text = c.pass;
            this.text_dbname.Text = c.dbname;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBox1.SelectedIndex < this.connectList.Count)
            {
                this.setTextbox(this.comboBox1.SelectedIndex);
            }

            /*
            if (this.comboBox1.SelectedIndex == 0)
            {
                text_dbname.Text = "en_gara";
            }
            else if (this.comboBox1.SelectedIndex == 1)
            {
                text_dbname.Text = "en_furu";
            }
            else if (this.comboBox1.SelectedIndex == 2)
            {
                text_dbname.Text = "en_test2";
            }
            else if (this.comboBox1.SelectedIndex == 3)
            {
                text_host.Text = "localhost";
                text_dbname.Text = "en_test";
            }
            */
        }

        private void button3_Click(object sender, EventArgs e)
        {
            text_host.Text = "ma.hocha.org";
            text_port.Text = "3307";
        }

    }
}
