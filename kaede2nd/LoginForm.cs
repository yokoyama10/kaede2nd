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
        private readonly bool isViewOnlyDB = false;
 
        public LoginForm()
        {
            InitializeComponent();

            if (this.isViewOnlyDB == false)
            {
                text_host.Text = "halfed-note";
                text_port.Text = "3306";
                text_user.Text = "ennichi";
                text_pass.Text = "itsuki";
            }
            else
            {
                text_host.Text = "ma.hocha.org";
                text_port.Text = "3307";
                text_user.Text = "ennichi";
                text_pass.Text = "3251";

                this.Text += "（読み取り専用Ver）";
            }
            this.comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
            GlobalData.makeInstance("host=" + text_host.Text + ";port=" + text_port.Text 
                + ";user id=" + text_user.Text + ";password=" + text_pass.Text
                + ";database=" + text_dbname.Text + ";charset=utf8;");
            */

            GlobalData.makeInstance(text_host.Text, text_port.Text, text_user.Text, text_pass.Text, text_dbname.Text);

            string deity = "ゆかり姫";
            if (this.comboBox1.SelectedIndex == 0)
            {
                GlobalData.Instance.itemNameImeOn = true;
                GlobalData.Instance.enterToTab = false;
                GlobalData.Instance.bumonName = "ガラクタ部門";
                GlobalData.Instance.symbolColor = Color.MistyRose;
            }
            else if (this.comboBox1.SelectedIndex == 1)
            {
                GlobalData.Instance.itemNameImeOn = false;
                GlobalData.Instance.enterToTab = true;
                GlobalData.Instance.bumonName = "古本部門";
                GlobalData.Instance.symbolColor = Color.LightCyan;
            }
            else if (this.comboBox1.SelectedIndex == 2)
            {
                GlobalData.Instance.itemNameImeOn = true;
                GlobalData.Instance.enterToTab = false;
                GlobalData.Instance.bumonName = "松代実験場";
            }

            GlobalData.Instance.windowTitle = deity + "萌え萌えソフトウェア";

            if (this.isViewOnlyDB)
            {
                GlobalData.Instance.bumonName += "（閲覧専用）";
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
