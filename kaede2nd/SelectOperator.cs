using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using kaede2nd.Dao;
using kaede2nd.Entity;

namespace kaede2nd
{
    public partial class SelectOperator : Form
    {
        private Operator retop;

        public SelectOperator()
        {
            InitializeComponent();

            IOperatorDao opDao = GlobalData.getIDao<IOperatorDao>();
            List<Operator> lop = opDao.GetAll();

            foreach (Operator op in lop)
            {
                this.listBox1.Items.Add(op);
            }

            if (lop.Count == 0)
            {
                MessageBox.Show("先に[機能]→[オペレーターIDを管理]で設定を済ませてください");
            }
        }

        public static DialogResult ShowSelectOperatorDialog(out Operator op)
        {
            SelectOperator f = new SelectOperator();
            DialogResult res = f.ShowDialog();

            op = f.retop;

            f.Dispose();

            return res;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedItem == null) { return; }
            this.textBox1.Text = ((Operator)this.listBox1.SelectedItem).operator_id.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            uint opid;
            if (uint.TryParse(this.textBox1.Text, out opid))
            {
                IOperatorDao opDao = GlobalData.getIDao<IOperatorDao>();
                List<Operator> lop = opDao.GetById(opid);
                if (lop.Count == 1)
                {
                    this.retop = lop[0];

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    return;
                }
            }

            this.textBox1.BackColor = Color.LightPink;
            this.textBox1.Focus();
            this.textBox1.SelectAll();
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.button1.PerformClick();
        }
    }
}
