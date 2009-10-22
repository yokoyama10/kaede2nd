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
    public partial class InputBox : Form
    {
        private InputBox()
        {
            InitializeComponent();
        }

        public InputBox(string msgText, string title, string defaultVal) : this()
        {
            this.textBox1.Text = msgText;
            this.Text = title;
            this.textBox2.Text = defaultVal;
        }

        private void InputBox_Shown(object sender, EventArgs e)
        {
            this.textBox2.Focus();
            this.textBox2.SelectAll();
        }

        public static DialogResult Show_Dialog(string msgText, string title, string defaultVal, out string result
            , ImeMode imeMode, HorizontalAlignment align)
        {
            InputBox f = new InputBox(msgText, title, defaultVal);

            f.textBox2.ImeMode = imeMode;
            f.textBox2.TextAlign = align;

            DialogResult dres = f.ShowDialog();
            if (dres == DialogResult.OK)
            {
                result = f.textBox2.Text;
            }
            else
            {
                result = null;
            }

            f.Dispose();

            return dres;
        }

        public static DialogResult ShowIntDialog(string msgText, string title, string defaultVal, out string result)
        {
            return InputBox.Show_Dialog(msgText, title, defaultVal, out result, ImeMode.Off, HorizontalAlignment.Right);
        }
    }
}
