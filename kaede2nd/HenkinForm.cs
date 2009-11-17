using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using kaede2nd.Entity;
using kaede2nd.Dao;
using System.IO;

namespace kaede2nd
{
    public partial class HenkinForm : MyForm
    {
        private IEnumerable<IGrouping<string, Item>> itemGroup = null;

        public HenkinForm()
        {
            InitializeComponent();
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.DefaultCellStyle.BackColor = GlobalData.Instance.data.symbolColor;
            this.dataGridView1.RowTemplate.Height = 20;

            ColumnInfo colinfo = null;
            DataGridViewColumn col;

            //[一覧]
            colinfo = this.newColumn<DataGridViewButtonColumn>(ColumnName.itiran, ColumnType.Unknown);
            colinfo.CellvalueSet = delegate(DataGridViewCell cell, object obj)
            {
                cell.Value = "一覧";
            };
            col = colinfo.col;
            col.ValueType = typeof(string);
            col.ReadOnly = true;
            col.DefaultCellStyle.BackColor = System.Drawing.SystemColors.ButtonFace;
            col.Width = GlobalData.moziWidth * 6;
            col.SortMode = DataGridViewColumnSortMode.NotSortable;

            this.dataGridView1.Columns.Add(col);


            //出品者
            colinfo = this.newColumn<DataGridViewTextBoxColumn>(ColumnName.shuppinsha, ColumnType.Unknown);
            colinfo.sortComparison = delegate(DataGridViewCell c1, DataGridViewCell c2)
            {
                return String.Compare((string)c1.Tag, (string)c2.Tag, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.CompareOptions.Ordinal);
            };
            colinfo.CellvalueSet = delegate(DataGridViewCell cell, object obj)
            {
                var g = (IGrouping<string, Item>)obj;

                cell.Value = g.First().item__Receipt.getSellerString();
                cell.Tag = g.First().item__Receipt.getSellerSortKey();
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
                var g = (IGrouping<string, Item>)obj;
                cell.Value = (from i in g where i.item_sellprice.HasValue select i.item_sellprice.Value).Sum(a => (long)a);
            };
            col = colinfo.col;
            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            col.ValueType = typeof(Int32);
            col.DefaultCellStyle.Format = "#,##0";
            col.ReadOnly = true;
            col.Width = GlobalData.moziWidth * 7;

            this.dataGridView1.Columns.Add(col);


            //返品個数
            colinfo = this.newColumn<DataGridViewButtonColumn>(ColumnName.henpinKosuu, ColumnType.Unknown);
            colinfo.CellvalueSet = delegate(DataGridViewCell cell, object obj)
            {
                var g = (IGrouping<string, Item>)obj;
                cell.Value = this.countHenpinItems(g);
            };
            col = colinfo.col;
            col.ValueType = typeof(Int32);
            col.ReadOnly = true;
            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            col.Width = GlobalData.moziWidth * 7;
            col.DefaultCellStyle.BackColor = System.Drawing.SystemColors.ButtonFace;
            col.SortMode = DataGridViewColumnSortMode.Automatic;
            this.dataGridView1.Columns.Add(col);


            this.addDGVEvents(this.dataGridView1);

            this.RefreshData();
        }

        public void RefreshData()
        {
            IItemDao iDao = GlobalData.getIDao<IItemDao>();
            List<Item> items = iDao.GetAll();

            this.itemGroup = (from i in items
                              group i by i.item__Receipt.getSellerString())
                              .OrderByDescending(g => this.countHenpinItems(g));

            this.dataGridView1.Rows.Clear();
            foreach (var g in this.itemGroup)
            {
                DataGridViewRow row = this.dataGridView1.Rows[this.dataGridView1.Rows.Add()];
                this.setRowValue(row, g);
            }

            this.dataGridView1.VirtualMode = true;
            this.dataGridView1.VirtualMode = false;
            this.dataGridView1.Enabled = true;
            this.dataGridView1.Focus();
        }

        protected override void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            DataGridView dgv = (DataGridView)sender;
            if (dgv.Enabled == false) { return; }
            if (this.itemGroup == null) { return; }

            if (e.RowIndex < 0) { return; }
            object shupp = dgv[ColumnName.shuppinsha, e.RowIndex].Value;
            if (shupp == null) { return; }

            var grps = from gg in this.itemGroup where gg.Key == (string)shupp select gg;
            if (grps.Count() == 0) { return; }

            var grp = grps.Single();

            if (dgv.Columns[e.ColumnIndex].Name == ColumnName.henpinKosuu)
            {
                Form_ItemList f = new Form_ItemList(
                    delegate()
                    {
                        return (from i in grp where i.item_sellprice.HasValue == false select i).ToList();
                    }, grp.Key + "の返品物一覧", "Seller"
                );
                f.Show();
            }
            else if (dgv.Columns[e.ColumnIndex].Name == ColumnName.itiran)
            {
                Form_ItemList f = new Form_ItemList(
                    delegate()
                    {
                        return grp.ToList();
                    }, grp.Key + "の出品物一覧", "Return"
                );
                f.Show();
            }
        }

        private List<IGrouping<string, Item>> getGrpListSelected()
        {
            if (this.dataGridView1.SelectedRows.Count == 0)
            {
                return this.itemGroup.ToList();
            }
            else
            {
                var list = new List<IGrouping<string, Item>>();

                for (int i = 0; i < this.dataGridView1.SelectedRows.Count; i++)
                {
                    DataGridViewRow row = this.dataGridView1.SelectedRows[i];
                    object shupp = row.Cells[ColumnName.shuppinsha].Value;
                    if (shupp == null) { return null; }

                    var grps = from gg in this.itemGroup where gg.Key == (string)shupp select gg;
                    if (grps.Count() == 0) { return null; }

                    list.Add(grps.Single());
                }

                return list;
            }
        }

        private void button_print_Click(object sender, EventArgs e)
        {

            if (this.itemGroup == null) { return; }
            DataGridView dgv = this.dataGridView1;
            if (!dgv.Enabled) { return; }

            var list = this.getGrpListSelected();
            if (list == null) { return; }


            ReturnListPrintDocument.PrintType type;
            ReturnListPrintDocument.SortType sort = ReturnListPrintDocument.SortType.ItemId;
            if (sender == this.button_return_print)
            {
                type = ReturnListPrintDocument.PrintType.Return;
            }
            else if (sender == this.button_meisai_print)
            {
                type = ReturnListPrintDocument.PrintType.MeisaiWithoutReturn;
                if (MessageBox.Show("売価の高い順に並び替えますか？", "明細印刷", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    sort = ReturnListPrintDocument.SortType.SellPriceDesc;
                }
            }
            else
            {
                throw new InvalidOperationException();
            }

            PageSetupDialog psd = new PageSetupDialog();
            psd.EnableMetric = true; //cf. KB814355 et http://dobon.net/vb/dotnet/graphics/pagesetupdialogbug.html
            psd.PageSettings = GlobalData.Instance.receipt_pageSettings;
            psd.PrinterSettings = GlobalData.Instance.receipt_printerSettings;
            psd.AllowMargins = false;
            psd.ShowDialog();

            PrintDialog prid = new PrintDialog();
            prid.PrinterSettings = GlobalData.Instance.receipt_printerSettings;
            prid.UseEXDialog = true;


            prid.Document = new ReturnListPrintDocument(list, GlobalData.Instance.receipt_pageSettings,
                GlobalData.Instance.receipt_printerSettings, type, sort);
            DialogResult pdres = prid.ShowDialog();

            if (pdres != DialogResult.OK) { return; }

            PrintPreviewDialog ppp = new PrintPreviewDialog();
            ppp.Document = prid.Document;
            ppp.Show();
            return;

            try
            {
                prid.Document.Print();
            }
            catch (Exception iex)
            {
                MessageBox.Show("印刷が実行できませんでした: " + iex.ToString());
            }
        }

        private void button_refresh_Click(object sender, EventArgs e)
        {
            this.RefreshData();
        }

        private int countHenpinItems(IGrouping<string, Item> grp)
        {
            return (from i in grp where i.item_sellprice.HasValue == false select i).Count();
        }

        private void button_csv_Click(object sender, EventArgs e)
        {
            if (this.itemGroup == null) { return; }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = GlobalData.Instance.data.bumonName + "_返金返品.csv";
            sfd.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath);
            sfd.Filter = "CSVファイル (*.csv)|*.csv";
            sfd.RestoreDirectory = true;

            if (sfd.ShowDialog() == DialogResult.OK)
            {

                using (Stream st = sfd.OpenFile())
                {
                    using (StreamWriter stw = new StreamWriter(st, Encoding.GetEncoding(932)))
                    {
                        stw.WriteLine("出品者,売上額,返品個数");

                        for(int i = 0; i < this.dataGridView1.Rows.Count; i++)
                        {
                            var r = this.dataGridView1.Rows[i];
                            stw.Write(((string)r.Cells[ColumnName.shuppinsha].Value).ToCSVString());
                            stw.Write(",");
                            stw.Write(r.Cells[ColumnName.uriageGaku].Value.ToString());
                            stw.Write(",");
                            stw.Write(r.Cells[ColumnName.henpinKosuu].Value.ToString());
                            stw.Write("\n");
                        }

                        stw.Close();
                    }
                    st.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (this.itemGroup == null) { return; }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = GlobalData.Instance.data.bumonName + "_返品リスト.csv";
            sfd.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath);
            sfd.Filter = "CSVファイル (*.csv)|*.csv";
            sfd.RestoreDirectory = true;

            if (sfd.ShowDialog() == DialogResult.OK)
            {

                var list = this.getGrpListSelected();
                using (Stream st = sfd.OpenFile())
                {
                    using (StreamWriter stw = new StreamWriter(st, Encoding.GetEncoding(932)))
                    {
                        stw.WriteLine(",票番,品番,商品名,定価,返品?");
                        foreach (var g in list)
                        {
                            var its = (from i in g where i.item_sellprice.HasValue == false select i).ToList();
                            if (its.Count == 0) { continue; }

                            stw.WriteLine(g.Key.ToCSVString());

                            uint reid = 0;
                            int count = 1;
                            foreach (Item it in its)
                            {
                                stw.Write(count.ToString());
                                stw.Write(",");
                                if (reid != it.item_receipt_id)
                                {
                                    stw.Write("R" + it.item_receipt_id.ToString("0000"));
                                    reid = it.item_receipt_id;
                                }
                                stw.Write(",");
                                stw.Write(it.item_id.ToString("00000"));
                                stw.Write(",");
                                stw.Write(it.item_name.ToCSVString());
                                stw.Write(",");
                                stw.Write(it.item_tagprice.ToString());
                                stw.Write(",");
                                if (it.item_return == true)
                                {
                                    stw.Write("返品");
                                }
                                stw.Write("\n");
                                count++;
                            }
                            stw.Write("\n");
                        }

                        stw.Close();
                    }
                    st.Close();
                }
            }
        }

    }
}
