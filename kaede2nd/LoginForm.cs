﻿using System;
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
        }

        public LoginForm(Action<DatabaseAccess> setter, string formTitle) : this()
        {
            this.DbAccessSetter = setter;
            this.Text = formTitle;

            this.label_backupdest.Enabled = false;
            this.textBox_backupdest.Enabled = false;
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
                GlobalData.Instance.data.isReadonly = checkBox_readonly.Checked;
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

        private void setTextbox(int index)
        {
            AppConfig.Connection c = this.connectList[index];
            this.text_host.Text = c.host;
            this.text_port.Text = c.port;
            this.text_user.Text = c.user;
            this.text_pass.Text = c.pass;
            this.text_dbname.Text = c.dbname;
            this.checkBox_readonly.Checked = c.is_readonly;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBox1.SelectedIndex < this.connectList.Count)
            {
                this.setTextbox(this.comboBox1.SelectedIndex);
            }
        }

    }
}
