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

            ColumnInfo colinfo;
            DataGridViewColumn col;

            this.AddColumn(this.dataGridView1, ColumnType.ItemId);
            this.AddColumn(this.dataGridView1, ColumnType.ReceiptIdButton);

            colinfo = this.newColumn<DataGridViewTextBoxColumn>(ColumnName.shuppinsha);
            colinfo.sortComparison = delegate(DataGridViewCell c1, DataGridViewCell c2)
            {
                return String.Compare((string)c1.Tag, (string)c2.Tag, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.CompareOptions.Ordinal);
            };
            colinfo.CellvalueSet = delegate(DataGridViewCell cell, object obj)
            {
                Receipt r = ((Item)obj).item__Receipt;

                cell.Value = r.getSellerString();
                cell.Tag = r.getSellerSortKey();
            };
            col = colinfo.col;
            col.ValueType = typeof(string);
            col.ReadOnly = true;
            col.Width = GlobalData.moziWidth * 18;
            this.dataGridView1.Columns.Add(col);



            colinfo = this.newColumn<DataGridViewTextBoxColumn>(ColumnName.shouhinMei);
            colinfo.imeMode = ImeMode.On;
            colinfo.DBvalueSet = delegate(object obj, object val) { ((Item)obj).item_name = Globals.strNoNull((string)val); };
            colinfo.CellvalueSet = delegate(DataGridViewCell cell, object obj)
            {
                cell.Value = ((Item)obj).item_name;
            };
            col = colinfo.col;
            col.ValueType = typeof(string);
            col.Width = GlobalData.moziWidth * 20;
            this.dataGridView1.Columns.Add(col);

            colinfo = this.newColumn<DataGridViewTextBoxColumn>(ColumnName.teika);
            colinfo.imeMode = ImeMode.Off;
            colinfo.DBvalueSet = delegate(object obj, object val) { ((Item)obj).item_tagprice = (val is UInt32) ? (UInt32)val : 0; };
            colinfo.CellvalueSet = delegate(DataGridViewCell cell, object obj)
            {
                cell.Value = ((Item)obj).item_tagprice;
            };
            col = colinfo.col;
            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            col.ValueType = typeof(UInt32);
            col.Width = GlobalData.moziWidth * 6;
            this.dataGridView1.Columns.Add(col);


            colinfo = this.newColumn<DataGridViewCheckBoxColumn>(ColumnName.henpin);
            col = colinfo.col;
            colinfo.defaultVal = Globals.check_falseVal;
            colinfo.DBvalueSet = delegate(object obj, object val) { ((Item)obj).item_return = Globals.getBoolFromCheckboxString((string)val); };
            colinfo.CellvalueSet = delegate(DataGridViewCell cell, object obj)
            {
                cell.Value = Globals.getCheckboxStringNoNull(((Item)obj).item_return, false);
            };
            col.ValueType = typeof(string);
            ((DataGridViewCheckBoxColumn)col).ThreeState = false;
            ((DataGridViewCheckBoxColumn)col).TrueValue = Globals.check_trueVal;
            ((DataGridViewCheckBoxColumn)col).FalseValue = Globals.check_falseVal;
            //((DataGridViewCheckBoxColumn)col).IndeterminateValue = Globals.check_unkVal;
            col.Width = GlobalData.moziWidth * 8;
            col.SortMode = DataGridViewColumnSortMode.Automatic;
            this.dataGridView1.Columns.Add(col);



            colinfo = this.newColumn<DataGridViewTextBoxColumn>(ColumnName.uketsukeNitizi);
            colinfo.sortComparison = ColumnInfo.DateTimeCellComp;

            colinfo.CellvalueSet = delegate(DataGridViewCell cell, object obj)
            {
                Globals.setCellByDateTimeN(cell, ((Item)obj).item_receipt_time, "不明");
            };
            col = colinfo.col;
            col.ValueType = typeof(string);
            col.ReadOnly = true;
            col.Width = GlobalData.moziWidth * 11;
            this.dataGridView1.Columns.Add(col);



            colinfo = this.newColumn<DataGridViewTextBoxColumn>(ColumnName.comment);
            colinfo.DBvalueSet = delegate(object obj, object val) { ((Item)obj).item_comment = (string)val; };
            colinfo.CellvalueSet = delegate(DataGridViewCell cell, object obj)
            {
                cell.Value = ((Item)obj).item_comment;
            };
            col = colinfo.col;
            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            col.ValueType = typeof(string);
            col.Width = GlobalData.moziWidth * 18;
            this.dataGridView1.Columns.Add(col);


            colinfo = this.newColumn<DataGridViewTextBoxColumn>(ColumnName.baika);
            colinfo.imeMode = ImeMode.Off;
            colinfo.DBvalueSet = delegate(object obj, object val) { ((Item)obj).item_sellprice = Globals.convToUInt32(val); };
            colinfo.CellvalueSet = delegate(DataGridViewCell cell, object obj)
            {
                cell.Value = ((Item)obj).item_sellprice;
            };
            col = colinfo.col;
            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            col.ValueType = typeof(UInt32);
            col.Width = GlobalData.moziWidth * 6;
            this.dataGridView1.Columns.Add(col);

            colinfo = this.newColumn<DataGridViewTextBoxColumn>(ColumnName.hanbaiNitizi);
            colinfo.sortComparison = ColumnInfo.DateTimeCellComp;

            colinfo.CellvalueSet = delegate(DataGridViewCell cell, object obj)
            {
                Item it = (Item)obj;
                if (it.item_selltime.HasValue)
                {
                    cell.Tag = it.item_selltime.Value;
                    cell.Value = Globals.getTimeString(it.item_selltime);
                }
                else
                {
                    cell.Tag = DateTime.MinValue;
                    cell.Value = "";
                }
            };
            col = colinfo.col;
            col.ValueType = typeof(string);
            col.ReadOnly = true;
            col.Width = GlobalData.moziWidth * 11;
            this.dataGridView1.Columns.Add(col);


            colinfo = this.newColumn<DataGridViewTextBoxColumn>(ColumnName.hanbaiComment);
            colinfo.DBvalueSet = delegate(object obj, object val) { ((Item)obj).item_sellcomment = (string)val; };
            colinfo.CellvalueSet = delegate(DataGridViewCell cell, object obj)
            {
                cell.Value = ((Item)obj).item_sellcomment;
            };
            col = colinfo.col;
            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            col.ValueType = typeof(string);
            col.Width = GlobalData.moziWidth * 18;
            this.dataGridView1.Columns.Add(col);

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
