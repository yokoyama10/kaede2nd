using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using kaede2nd.Entity;
using System.IO;

namespace kaede2nd
{
    public partial class GassanForm : MyForm
    {
        private readonly DatabaseAccess yourData;
        private List<HenkinJoined> joinedlist = null;

        public GassanForm()
        {
            DatabaseAccess da = null;
            LoginForm lf = new LoginForm(
                delegate(DatabaseAccess obj) { da = obj; }, "合算先の部門を選択");
            lf.ShowDialog();

            if (da == null) { this.Close(); return; }
            this.yourData = da;

            InitializeComponent();
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.DefaultCellStyle.BackColor = Color.White;
            this.dataGridView1.RowTemplate.Height = 20;

            ColumnInfo colinfo = null;
            DataGridViewColumn col;

            //出品者
            colinfo = this.newColumn<DataGridViewTextBoxColumn>(ColumnName.shuppinsha, ColumnType.Unknown);
            colinfo.sortComparison = delegate(DataGridViewCell c1, DataGridViewCell c2)
            {
                return String.Compare((string)c1.Tag, (string)c2.Tag, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.CompareOptions.Ordinal);
            };
            colinfo.CellvalueSet = delegate(DataGridViewCell cell, object obj)
            {
                Item i = ((HenkinJoined)obj).ExampleItem;
                cell.Value = i.item__Receipt.getSellerString();
                cell.Tag = i.item__Receipt.getSellerSortKey();
            };
            col = colinfo.col;
            col.ValueType = typeof(string);
            col.ReadOnly = true;
            col.Width = GlobalData.moziWidth * 18;

            this.dataGridView1.Columns.Add(col);

            //売上額
            colinfo = this.newColumn<DataGridViewTextBoxColumn>(ColumnName.uriageGaku, ColumnType.Unknown);
            colinfo.CellvalueSet = delegate(DataGridViewCell cell, object obj)
            {
                HenkinJoined h = (HenkinJoined)obj;
                cell.Value = this.getSellSum(h.Items1) + this.getSellSum(h.Items2);
            };
            col = colinfo.col;
            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            col.ValueType = typeof(Int32);
            col.DefaultCellStyle.Format = "#,##0";
            col.ReadOnly = true;
            col.Width = GlobalData.moziWidth * 7;

            this.dataGridView1.Columns.Add(col);

            /*
            //返金額
            colinfo = this.newColumn<DataGridViewTextBoxColumn>(ColumnName.henkinGaku, ColumnType.Unknown);
            colinfo.CellvalueSet = delegate(DataGridViewCell cell, object obj)
            {
                HenkinJoined h = (HenkinJoined)obj;
                long sum = this.getSellSum(h.Items1) + this.getSellSum(h.Items2);
                cell.Value = ((long)Math.Floor(sum * 0.95 / 10.0)) * 10;
            };
            col = colinfo.col;
            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            col.ValueType = typeof(Int32);
            col.DefaultCellStyle.Format = "#,##0";
            col.ReadOnly = true;
            col.Width = GlobalData.moziWidth * 7;

            this.dataGridView1.Columns.Add(col);
            */


            //Sum1
            colinfo = this.newColumn<DataGridViewTextBoxColumn>(GlobalData.Instance.data.bumonName, ColumnType.Unknown);
            colinfo.sortComparison = Globals.longCellComparison;
            colinfo.CellvalueSet = delegate(DataGridViewCell cell, object obj)
            {
                HenkinJoined h = (HenkinJoined)obj;
                this.setCellSellSumString(cell, h.Items1);
            }; 
            col = colinfo.col;
            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            col.ValueType = typeof(string);
            col.DefaultCellStyle.BackColor = GlobalData.Instance.data.symbolColor;
            col.ReadOnly = true;
            col.Width = GlobalData.moziWidth * 7;

            this.dataGridView1.Columns.Add(col);


            //返品個数1
            colinfo = this.newColumn<DataGridViewTextBoxColumn>(ColumnName.henpinKosuu + "1", ColumnType.Unknown);
            colinfo.sortComparison = Globals.longCellComparison;
            colinfo.CellvalueSet = delegate(DataGridViewCell cell, object obj)
            {
                HenkinJoined h = (HenkinJoined)obj;
                this.setCellReturnCount(cell, h.Items1);
            };
            col = colinfo.col;
            col.ValueType = typeof(string);
            col.DefaultCellStyle.BackColor = GlobalData.Instance.data.symbolColor;
            col.ReadOnly = true;
            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            col.Width = GlobalData.moziWidth * 7;
            this.dataGridView1.Columns.Add(col);

            //Sum2
            string c = (GlobalData.Instance.data.bumonName == this.yourData.bumonName)
                ? GlobalData.Instance.data.bumonName + "_"
                : this.yourData.bumonName;
            colinfo = this.newColumn<DataGridViewTextBoxColumn>(c, ColumnType.Unknown);
            colinfo.sortComparison = Globals.longCellComparison;
            colinfo.CellvalueSet = delegate(DataGridViewCell cell, object obj)
            {
                HenkinJoined h = (HenkinJoined)obj;
                this.setCellSellSumString(cell, h.Items2);
            };
            col = colinfo.col;
            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            col.ValueType = typeof(string);
            col.DefaultCellStyle.BackColor = this.yourData.symbolColor;
            col.ReadOnly = true;
            col.Width = GlobalData.moziWidth * 7;

            this.dataGridView1.Columns.Add(col);

            //返品個数2
            colinfo = this.newColumn<DataGridViewTextBoxColumn>(ColumnName.henpinKosuu + "2", ColumnType.Unknown);
            colinfo.sortComparison = Globals.longCellComparison;
            colinfo.CellvalueSet = delegate(DataGridViewCell cell, object obj)
            {
                HenkinJoined h = (HenkinJoined)obj;
                this.setCellReturnCount(cell, h.Items2);
            };
            col = colinfo.col;
            col.ValueType = typeof(string);
            col.DefaultCellStyle.BackColor = this.yourData.symbolColor;
            col.ReadOnly = true;
            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            col.Width = GlobalData.moziWidth * 7;
            this.dataGridView1.Columns.Add(col);


            this.addDGVEvents(this.dataGridView1);
            this.RefreshData();
        }

        private long getSellSum(IEnumerable<Item> items)
        {
            if (items == null) { return 0; }
            return items.Sum(it => (long)(it.item_sellprice ?? 0));
        }

        private void setCellSellSumString(DataGridViewCell cell, IEnumerable<Item> items)
        {
            string s = "-";
            long sum = -1;

            if (items == null) { cell.Style.ForeColor = Color.Gray; }
            else { sum = this.getSellSum(items); s = sum.ToString("#,##0"); }

            cell.Value = s;
            cell.Tag = sum;
        }

        private void setCellReturnCount(DataGridViewCell cell, IEnumerable<Item> items)
        {
            string s = "-";
            long count = -1;

            if (items == null) { cell.Style.ForeColor = Color.Gray; }
            else { count = (from i in items where i.item_sellprice.HasValue == false select i).Count();
                  s = count.ToString(); }

            cell.Value = s;
            cell.Tag = count;
        }

        public class grpComparer<T1, T2> : IEqualityComparer<IGrouping<T1, T2>>
        {
            bool IEqualityComparer<IGrouping<T1, T2>>.Equals(IGrouping<T1, T2> x, IGrouping<T1, T2> y)
            {
                return x.Key.Equals(y.Key);
            }

            int IEqualityComparer<IGrouping<T1, T2>>.GetHashCode(IGrouping<T1, T2> obj)
            {
                return obj.Key.GetHashCode();
            }
        }

        private class HenkinJoined
        {
            public string Key; public Item ExampleItem;
            public List<Item> Items1; public List<Item> Items2;
        }


        private string makeCompareSellerString(string seller)
        {
            string ret = seller;

            ret = Microsoft.VisualBasic.Strings.StrConv(ret, Microsoft.VisualBasic.VbStrConv.Narrow, 0);
            ret = ret.ToLowerInvariant();
            ret = System.Text.RegularExpressions.Regex.Replace(ret, "st", "期");
            ret = System.Text.RegularExpressions.Regex.Replace(ret, "nd", "期");
            ret = System.Text.RegularExpressions.Regex.Replace(ret, "rd", "期");
            ret = System.Text.RegularExpressions.Regex.Replace(ret, "th", "期");
            ret = System.Text.RegularExpressions.Regex.Replace(ret, " ", "");

            return ret;

        }

        private void RefreshData()
        {

            var myiDao = GlobalData.getIDao<kaede2nd.Dao.IItemDao>();
            var myGrp =
                (from i in myiDao.GetAll()
                 //where i.item_sellprice.HasValue == true
                 group i by this.makeCompareSellerString(i.item__Receipt.getSellerString()));


            var youriDao = this.yourData.getIDao<kaede2nd.Dao.IItemDao>();
            var yourGrp =
                (from i in youriDao.GetAll()
                 //where i.item_sellprice.HasValue == true
                 group i by this.makeCompareSellerString(i.item__Receipt.getSellerString()));

            IEqualityComparer<IGrouping<string, Item>> comp = new grpComparer<string, Item>();
            //(long?)(myg.Sum(item => (long)item.item_sellprice.Value))
            var dest = (from myg in myGrp
                        join yog in yourGrp
                        on myg.Key equals yog.Key
                        select new HenkinJoined
                        {
                            Key = myg.Key,
                            ExampleItem = myg.First(),
                            Items1 = myg.ToList(),
                            Items2 = yog.ToList()
                        }
                       ).Union(
                            myGrp.Except(yourGrp, comp).Select(
                                ggg => new HenkinJoined
                                {
                                    Key = ggg.Key,
                                    ExampleItem = ggg.First(),
                                    Items1 = ggg.ToList(),
                                    Items2 = null
                                }
                                )
                       ).Union(
                            yourGrp.Except(myGrp, comp).Select(
                                ggg => new HenkinJoined
                                {
                                    Key = ggg.Key,
                                    ExampleItem = ggg.First(),
                                    Items1 = null,
                                    Items2 = ggg.ToList()
                                }
                                )
                       );
            this.joinedlist = dest.ToList();


            this.dataGridView1.Rows.Clear();
            foreach (var j in this.joinedlist)
            {
                DataGridViewRow row = this.dataGridView1.Rows[this.dataGridView1.Rows.Add()];
                this.setRowValue(row, j);
            }

            this.dataGridView1.VirtualMode = true;
            this.dataGridView1.VirtualMode = false;
            this.dataGridView1.Enabled = true;
            this.dataGridView1.Focus();
        }

        private void button_reload_Click(object sender, EventArgs e)
        {
            this.RefreshData();
        }

        private void button_csv_Click(object sender, EventArgs e)
        {
            if (this.joinedlist == null) { return; }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = "総合返金額.csv";
            sfd.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath);
            sfd.Filter = "CSVファイル (*.csv)|*.csv";
            sfd.RestoreDirectory = true;
            //var list = this.getListSelected();

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                using (Stream st = sfd.OpenFile())
                {
                    using (StreamWriter stw = new StreamWriter(st, Encoding.GetEncoding(932)))
                    {
                        stw.WriteLine("出品者,売上総額," +
                            (this.dataGridView1.Columns[2].HeaderText + "売上").ToCSVString() + "," +
                            (this.dataGridView1.Columns[2].HeaderText + "返品個数").ToCSVString() + "," +
                            (this.dataGridView1.Columns[4].HeaderText + "売上").ToCSVString() + "," +
                            (this.dataGridView1.Columns[4].HeaderText + "返品個数").ToCSVString());
                      
                        for(int i=0; i < this.dataGridView1.Rows.Count; i++)
                        {
                            var row = this.dataGridView1.Rows[i];
                            stw.Write(((string)row.Cells[0].Value).ToCSVString());
                            stw.Write(",");
                            stw.Write(row.Cells[1].Value.ToString());
                            stw.Write(",");
                            stw.Write(((string)row.Cells[2].Value).ToCSVString());
                            stw.Write(",");
                            stw.Write(((string)row.Cells[3].Value).ToCSVString());
                            stw.Write(",");
                            stw.Write(((string)row.Cells[4].Value).ToCSVString());
                            stw.Write(",");
                            stw.Write(((string)row.Cells[5].Value).ToCSVString());
                            stw.Write("\n");
                        }
                    stw.Close();
                    }
                    st.Close();
                }
            }
        }

        private List<HenkinJoined> getListSelected()
        {
            if (this.dataGridView1.SelectedRows.Count == 0)
            {
                return this.joinedlist;
            }
            else
            {
                var list = new List<HenkinJoined>();

                for (int i = 0; i < this.dataGridView1.SelectedRows.Count; i++)
                {
                    DataGridViewRow row = this.dataGridView1.SelectedRows[i];
                    object shupp = row.Cells[ColumnName.shuppinsha].Value;
                    if (shupp == null) { return null; }

                    var jls = from j in this.joinedlist where j.ExampleItem.item__Receipt.getSellerString() == (string)shupp select j;
                    if (jls.Count() == 0) { return null; }

                    list.Add(jls.Single());
                }

                return list;
            }
        }
    }
}
