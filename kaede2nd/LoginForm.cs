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

        public LoginForm()
        {
            InitializeComponent();

            text_host.Text = "halfed-note";
            text_port.Text = "3306";
            text_user.Text = "ennichi";
            text_pass.Text = "itsuki";
            this.comboBox1.SelectedIndex = 0;
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


            bool itemNameImeOn, enterToTab;

            string deity = "ゆかり姫"; //部門ごとに神が違うなら
            string barcodePrefix = "58"; //部門ごとに分けたいなら

            if (this.comboBox1.SelectedIndex == 0)
            {
                itemNameImeOn = true;
                enterToTab = false;
                data.bumonName = "ガラクタ部門";
                data.symbolColor = Color.MistyRose;
            }
            else if (this.comboBox1.SelectedIndex == 1)
            {
                itemNameImeOn = false;
                enterToTab = true;
                data.bumonName = "古本部門";
                data.symbolColor = Color.LightCyan;
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

            if (this.DbAccessSetter == null)
            {
                GlobalData.Instance.barcodePrefix = barcodePrefix;
                GlobalData.Instance.itemNameImeOn = itemNameImeOn;
                GlobalData.Instance.enterToTab = enterToTab;
                GlobalData.Instance.windowTitle = deity + "萌え萌えソフトウェア";
            }

        }


        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
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
        }

        private void button3_Click(object sender, EventArgs e)
        {
            text_host.Text = "ma.hocha.org";
            text_port.Text = "3307";
        }

    }
}
