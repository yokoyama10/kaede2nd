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
    public partial class ConfigForm : MyForm
    {
        private DbConfig dconf;

        public ConfigForm()
        {
            InitializeComponent();

            this.button_ok.Enabled = !GlobalData.Instance.data.isReadonly;

            this.dconf = DbConfig.MakeFromDB(GlobalData.Instance.data);

            this.textBox_bumon.Text = this.dconf.getValue("bumonname");
            this.textBox_company.Text = this.dconf.getValue("companyname");
            this.pictureBox1.BackColor = Color.FromArgb(this.dconf.getValueInt("symbolcolor_argb"));
            this.pictureBox2.BackColor = Color.FromArgb(this.dconf.getValueInt("bumontextcolor_argb"));
            this.textBox_barcode.Text = this.dconf.getValue("barcodeprefix");
            this.checkBox_imeon.Checked = this.dconf.getValueBool("itemname_imeon");
            this.checkBox_entertotab.Checked = this.dconf.getValueBool("entertotab");
        }

        private void show_colorDialog(PictureBox pictBox)
        {
            ColorDialog cdia = new ColorDialog();
            cdia.Color = pictBox.BackColor;
            cdia.FullOpen = true;
            cdia.AnyColor = true;

            if (cdia.ShowDialog() == DialogResult.OK)
            {
                pictBox.BackColor = cdia.Color;
            }

            cdia.Dispose();
        }

        private void button_color_Click(object sender, EventArgs e)
        {
            this.show_colorDialog(this.pictureBox1);
        }

        private void button_color2_Click(object sender, EventArgs e)
        {
            this.show_colorDialog(this.pictureBox2);
        }


        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            if (!Globals.isValidBarcodePrefix(this.textBox_barcode.Text))
            {
                MessageBox.Show("バーコード識別子が不正です。0-9の数字を2文字指定してください。");
                this.textBox_barcode.SelectAll();
                this.textBox_barcode.Focus();
                return;
            }

            DbConfig.setValue("bumonname", this.textBox_bumon.Text);
            DbConfig.setValue("companyname", this.textBox_company.Text);
            DbConfig.setValueInt("symbolcolor_argb", this.pictureBox1.BackColor.ToArgb());
            DbConfig.setValueInt("bumontextcolor_argb", this.pictureBox2.BackColor.ToArgb());
            DbConfig.setValue("barcodeprefix", this.textBox_barcode.Text);
            DbConfig.setValueBool("itemname_imeon", this.checkBox_imeon.Checked);
            DbConfig.setValueBool("entertotab", this.checkBox_entertotab.Checked);

            this.DialogResult = DialogResult.OK;
            this.Close();

        }


    }
}
