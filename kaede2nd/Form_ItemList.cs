using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using kaede2nd.Entity;
using kaede2nd.Dao;
using System.Text.RegularExpressions;

namespace kaede2nd
{
    public partial class Form_ItemList : MyItemFormBase
    {
        public delegate List<Item> ItemReturnDelegate();

        private string csvname;
        private ItemReturnDelegate itemReturner;
        private string searchText;
        public bool useRegex;

        public Form_ItemList(ItemReturnDelegate returner, string windowTitle, string csvName)
            : this()
        {
            this.Text = windowTitle;
            this.itemReturner = returner;
            this.csvname = csvName;
        }

        public Form_ItemList()
        {
            this.itemList = null;
            this.itemReturner = null;

            this.searchText = null;
            this.useRegex = false;

            InitializeComponent();

            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.DefaultCellStyle.BackColor = GlobalData.Instance.symbolColor;
            this.dataGridView1.RowTemplate.Height = 20;

            this.AddColumn(this.dataGridView1, ColumnType.ItemId);
            this.AddColumn(this.dataGridView1, ColumnType.ReceiptIdButton);
            this.AddColumn(this.dataGridView1, ColumnType.SellerName);
            this.AddColumn(this.dataGridView1, ColumnType.ItemName);
            this.AddColumn(this.dataGridView1, ColumnType.TagPrice);
            this.AddColumn(this.dataGridView1, ColumnType.IsHenpin);
            this.AddColumn(this.dataGridView1, ColumnType.ItemReceiveTime);
            this.AddColumn(this.dataGridView1, ColumnType.ItemComment);
            this.AddColumn(this.dataGridView1, ColumnType.SellPrice);
            this.AddColumn(this.dataGridView1, ColumnType.SellTime);

            this.addDGVEvents(this.dataGridView1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.renewItemList();
        }

        private void renewItemList()
        {
            if (this.itemReturner == null) { return; }

            Regex reg = null;
            if (this.searchText != null && this.useRegex)
            {
                try
                {
                    reg = new Regex(this.searchText, RegexOptions.IgnoreCase);
                }
                catch (Exception e)
                {
                    MessageBox.Show("正規表現がおかしいです: " + e.ToString());
                    return;
                }
            }

            dataGridView1.Enabled = false;
            this.dataGridView1.Rows.Clear();

            this.itemList = this.itemReturner();

            foreach (Item it in this.itemList)
            {
                if (this.searchText != null)
                {
                    if (this.useRegex)
                    {
                        if (!reg.IsMatch(it.item_name)) { continue; }
                    }
                    else
                    {
                        if (!it.item_name.Contains(this.searchText))
                        {
                            continue;
                        }
                    }
                }

                DataGridViewRow row = this.dataGridView1.Rows[this.dataGridView1.Rows.Add()];
                this.setRowValue(row, it);
            }

            this.dataGridView1.VirtualMode = true;
            this.dataGridView1.VirtualMode = false;
            this.dataGridView1.Enabled = true;
            this.dataGridView1.Focus();
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (!dgv.Enabled) { return; }
            if (e.ColumnIndex == 0) { return; }

            if (dgv.Rows[e.RowIndex].IsNewRow) { return; }

            if (this.itemList == null) { throw new InvalidOperationException(); }

            if (dgv.Columns[e.ColumnIndex].Name == ColumnName.shouhinMei)
            {
                this.ConvBarcode(dgv[e.ColumnIndex, e.RowIndex]);
            }

            var itemDao = GlobalData.getIDao<IItemDao>();
            Item it;

            if (dgv[ColumnName.shinaBan, e.RowIndex].Value == null)
            {
                return;
            }
            else
            {
                it = this.GetItemFromList((UInt32)dgv[ColumnName.shinaBan, e.RowIndex].Value);
                this.changeDBdata(dgv.Columns[e.ColumnIndex].Name, it, dgv[e.ColumnIndex, e.RowIndex].Value);

                try
                {
                    itemDao.Update(it);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("更新できませんでした: " + ex.ToString());
                    return;
                }
            }

        }

        private void Form_ItemList_Load(object sender, EventArgs e)
        {
            this.renewItemList();
        }

        protected override bool IsEditableImpl()
        {
            if (!this.dataGridView1.Enabled) { return false; }
            if (this.itemList == null) { throw new InvalidOperationException(); }

            return true;
        }


        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                this.dgv_DeleteSelectedRow();
            }
        }


        private void Form_ItemList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                this.renewItemList();
            }
            else if (e.KeyCode == Keys.F && e.Control)
            {
                this.textBox1.Focus();
            }
        }

        public void outCSV()
        {
            this.button2_Click(null, EventArgs.Empty);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.itemReturner == null) { return; }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = GlobalData.Instance.bumonName + "_" + csvname + ".csv";
            sfd.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath);
            sfd.Filter = "CSVファイル (*.csv)|*.csv";
            sfd.RestoreDirectory = true;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                using ( Stream st = sfd.OpenFile() )
                {
                    using (StreamWriter stw = new StreamWriter(st, Encoding.GetEncoding(932)))
                    {
                        stw.WriteLine(Item.GetCSVHeader());

                        List<Item> its = this.itemReturner();
                        foreach (Item i in its)
                        {
                            stw.WriteLine(i.GetCSVLine());
                        }

                        stw.Close();
                    }
                    st.Close();
                }
            }
        }

        private void button_search_Click(object sender, EventArgs e)
        {
            this.searchText = this.textBox1.Text;
            if (string.IsNullOrEmpty(this.searchText)) { this.searchText = null; }

            this.useRegex = this.checkBox_regex.Checked;

            this.renewItemList();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            TextBox tbx = (TextBox)sender;

            if (tbx.ForeColor == SystemColors.InactiveCaptionText)
            {
                tbx.ForeColor = SystemColors.WindowText;
                tbx.Text = "";
            }
            else
            {
                tbx.SelectAll();
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { this.button_search.PerformClick(); }
        }


    }
}
