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
    public partial class KansaSelect : Form
    {
        public KansaForm fkansa = null;

        public KansaSelect()
        {
            InitializeComponent();
            this.button1.Text = GlobalData.Instance.data.bumonName + " #1";
            this.button2.Text = GlobalData.Instance.data.bumonName + " #2";
            this.button3.Text = GlobalData.Instance.data.bumonName + " #3";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.fkansa = new KansaForm(1);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.fkansa = new KansaForm(2);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.fkansa = new KansaForm(3);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.fkansa = null;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
