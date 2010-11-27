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
    public partial class TitleSplitForm : MyForm
    {
        private string res = null;
        private List<Button> buttons = new List<Button>();

        public TitleSplitForm()
        {
            InitializeComponent();
        }

        public TitleSplitForm(string title) : this()
        {
            this.text_moto.Text = title;
           

            int ind = 0;
            int oldind = -100;
            while ((ind = title.IndexOfAny(new char[]{' ', '(', '〈'}, ind+1)) != -1)
            {
                if ((ind - oldind) <= 1) { continue; }
                oldind = ind;

                this.addButton(title.Substring(0, ind));
            }
            this.addButton(title);
        }

        private void addButton(string t)
        {
            int index = this.buttons.Count;

            Button b = new Button();
            b.Size = new Size(449, 32);
            b.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            b.Location = new Point(11, 15 + 38 * index);
            b.Text = t;
            b.TabIndex = index;
            b.Font = new Font(b.Font.FontFamily, 14, b.Font.Style);
            b.Click += new EventHandler(button_Click);
            b.Enter += new EventHandler(button_Enter);
            b.Leave += new EventHandler(button_Leave);

            this.panel1.Controls.Add(b);
            this.buttons.Add(b);
        }

        void button_Leave(object sender, EventArgs e)
        {
            ((Button)sender).BackColor = SystemColors.Control;
        }

        void button_Enter(object sender, EventArgs e)
        {
            ((Button)sender).BackColor = Color.Yellow;
        }

        void button_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.res = ((Button)sender).Text;
            this.Close();
        }

        public static DialogResult Show_Dialog(string title, out string result)
        {
            TitleSplitForm f = new TitleSplitForm(title);

            DialogResult dres = f.ShowDialog();
            if (dres == DialogResult.OK && f.res != null)
            {
                result = f.res;
            }
            else
            {
                result = null;
            }

            return dres;
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void text_moto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.DialogResult = DialogResult.OK;
                this.res = ((TextBox)sender).Text;
                this.Close();
            }
        }
    }
}
