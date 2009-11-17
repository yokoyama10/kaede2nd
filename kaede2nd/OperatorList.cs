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
    public partial class OperatorList : MyForm
    {
        List<Operator> oplist = new List<Operator>();


        public OperatorList()
        {
            InitializeComponent();
            this.Text += " (" + GlobalData.Instance.data.bumonName + ")";

            this.AddColumn(this.dataGridView1, ColumnType.OperatorId);
            this.AddColumn(this.dataGridView1, ColumnType.OperatorName);
            this.AddColumn(this.dataGridView1, ColumnType.OperatorComment);

            this.addDGVEvents(this.dataGridView1);

            this.RefreshShowing();
        }

        private void RefreshShowing()
        {
            this.dataGridView1.Enabled = false;
            this.dataGridView1.Rows.Clear();

            IOperatorDao opDao = GlobalData.getIDao<IOperatorDao>();

            this.oplist = opDao.GetAll();

            foreach (Operator op in this.oplist)
            {
                DataGridViewRow row = this.dataGridView1.Rows[this.dataGridView1.Rows.Add()];
                this.setRowValue(row, op);
            }

            dataGridView1.Sort(dataGridView1.Columns[ColumnName.operatorId], ListSortDirection.Ascending);

            this.dataGridView1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.RefreshShowing();
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (this.oplist == null) { throw new InvalidOperationException(); }

            DataGridView dgv = (DataGridView)sender;
            if (!dgv.Enabled) { return; }
            if (e.ColumnIndex == 0) { return; }

            if (dgv.Rows[e.RowIndex].IsNewRow) { return; }

            var opDao = GlobalData.getIDao<IOperatorDao>();
            Operator op;

            if (dgv[ColumnName.operatorId, e.RowIndex].Value == null)
            {
                op = new Operator();

                foreach (KeyValuePair<string, ColumnInfo> kvp in this.colinfos)
                {
                    this.changeDBdata(kvp.Key, op, dgv[kvp.Key, e.RowIndex].Value);
                }

                opDao.Insert(op);
                dgv[ColumnName.operatorId, e.RowIndex].Value = op.operator_id;

                this.oplist.Add(op);
            }
            else
            {
                var ops = from i in this.oplist where i.operator_id == ((uint)dgv[ColumnName.operatorId, e.RowIndex].Value) select i;
                if (ops.Count() == 0) { return; }

                op = ops.Single();

                this.changeDBdata(dgv.Columns[e.ColumnIndex].Name, op, dgv[e.ColumnIndex, e.RowIndex].Value);

                try
                {
                    opDao.Update(op);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("更新できませんでした: " + ex.ToString());
                    return;
                }
            }
        }
    }
}
