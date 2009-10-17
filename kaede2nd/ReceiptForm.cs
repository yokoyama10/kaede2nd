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

namespace kaede2nd
{
    public partial class ReceiptForm : MyItemFormBase
    {
        private Receipt receipt;

        private bool isNewReceipt;

        public ReceiptForm()
        {
            InitializeComponent();

            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.DefaultCellStyle.BackColor = GlobalData.Instance.symbolColor;

            this.dataGridView1.enterToTab = GlobalData.Instance.enterToTab;
            this.商品名編集Enterで右移動ToolStripMenuItem.Checked = GlobalData.Instance.enterToTab;

            this.商品名でIMEオンToolStripMenuItem.Checked = GlobalData.Instance.itemNameImeOn;

            ColumnInfo colinfo;
            DataGridViewColumn col;

            colinfo = this.newColumn<DataGridViewTextBoxColumn>(ColumnName.shinaBan);
            colinfo.CellvalueSet = delegate(DataGridViewCell cell, object obj)
            {
                cell.Value = ((Item)obj).item_id;
            };
            col = colinfo.col;
            col.ValueType = typeof(UInt32);
            col.ReadOnly = true;
            col.DefaultCellStyle.Format = "00000";
            col.Width = GlobalData.moziWidth * 6;
            this.dataGridView1.Columns.Add(col);

            colinfo = this.newColumn<DataGridViewTextBoxColumn>(ColumnName.shouhinMei);
            colinfo.imeMode = this.商品名でIMEオンToolStripMenuItem.Checked ? ImeMode.On : ImeMode.Off;
            colinfo.DBvalueSet = delegate(object obj, object val) { ((Item)obj).item_name = Globals.strNoNull((string)val); };
            colinfo.CellvalueSet = delegate(DataGridViewCell cell, object obj)
            {
                cell.Value = ((Item)obj).item_name;
            };
            col = colinfo.col;
            col.ValueType = typeof(string);
            col.Width = GlobalData.moziWidth * 30;
            this.dataGridView1.Columns.Add(col);


            colinfo = this.newColumn<DataGridViewTextBoxColumn>(ColumnName.isbn);
            colinfo.imeMode = ImeMode.NoControl;
            colinfo.DBvalueSet = delegate(object obj, object val) { ((Item)obj).item_isbn = Globals.convToDecimal(val); };
            colinfo.CellvalueSet = delegate(DataGridViewCell cell, object obj)
            {
                cell.Value = ((Item)obj).item_isbn;
            };
            col = colinfo.col;
            col.Visible = false;
            col.ReadOnly = true;
            col.ValueType = typeof(decimal);
            col.Width = GlobalData.moziWidth * 20;
            this.dataGridView1.Columns.Add(col);



            colinfo = this.newColumn<DataGridViewTextBoxColumn>(ColumnName.teika);
            colinfo.defaultVal = (UInt32)0;
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
            col.Width = GlobalData.moziWidth * 4;
            col.SortMode = DataGridViewColumnSortMode.Automatic;
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


            colinfo = this.newColumn<DataGridViewTextBoxColumn>(ColumnName.bunsatsu);
            colinfo.imeMode = ImeMode.Off;
            colinfo.DBvalueSet = delegate(object obj, object val) { ((Item)obj).item_volumes = Globals.convToUInt32(val); };
            colinfo.CellvalueSet = delegate(DataGridViewCell cell, object obj)
            {
                cell.Value = ((Item)obj).item_volumes;
            };
            col = colinfo.col;
            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            col.ValueType = typeof(UInt32);
            col.Width = GlobalData.moziWidth * 6;
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
                Globals.setCellByDateTimeN(cell, ((Item)obj).item_selltime, "");
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
            
            /*
            this.combo_operator.DataSource = GlobalData.Instance.operators;
            this.combo_operator.DisplayMember = "operator_name";
            this.combo_operator.ValueMember = "operator_id";
            this.combo_operator.SelectedValue = GlobalData.Instance.nowOperator;
            */

            this.Text = "受付票: 新規" + " (" + GlobalData.Instance.bumonName + ")";
            this.text_rid.Text = "新規";
            this.isNewReceipt = true;


            this.receipt = null;

            this.商品リストを表示LToolStripMenuItem.Enabled = false;
            this.dataGridView1.Enabled = false;
            this.dataGridView1.Rows.Clear();
            this.itemList = null;
        }

        public ReceiptForm(uint rid) : this()
        {
            var receiptDao = GlobalData.getIDao<IReceiptDao>();
            List<Receipt> rs = receiptDao.GetById(rid);

            if (rs.Count == 0)
            {
                throw new Exception("指定したReceiptが見つかりませんでした");
            }

            this.SetReceipt(rs[0]);
        }

        public Receipt GetReceipt()
        {
            return this.receipt;
        }

        private void SetReceipt(kaede2nd.Entity.Receipt r)
        {
            this.receipt = r;
            this.isNewReceipt = false;
            this.label_sakini.Visible = false;

            this.Text = "受付票: " + r.receipt_id.ToString("'R'0000") + " (" + GlobalData.Instance.bumonName + ")";
            this.text_rid.Text = r.receipt_id.ToString("'R'0000");
            this.text_pass.Text = r.receipt_pass;

            switch (r.receipt_seller)
            {
                case kaede2nd.Entity.Receipt.seller_EXT:
                    {
                        this.text_external.Text = r.receipt_seller_exname;
                        this.radio_external.Select();
                        break;
                    }
                case kaede2nd.Entity.Receipt.seller_LAGACY:
                    {
                        this.radio_legacy.Select();
                        break;
                    }
                case kaede2nd.Entity.Receipt.seller_DONATE:
                    {
                        this.radio_donate.Select();
                        break;
                    }
                default:
                    /*
                    if (this.receipt_seller.Substring(0, 1) == "9")
                    {
                        return "ERR: 不明";
                    }
                    */
                    this.radio_zaigaku.Select();
                    this.text_zai_nen.Text = r.receipt_seller.Substring(0,1);
                    this.text_zai_kumi.Text = r.receipt_seller.Substring(1, 1);
                    this.text_zai_ban.Text = r.receipt_seller.Substring(2, 2);
                    this.text_external.Text = r.receipt_seller_exname;
                    break;
            }

            this.check_payback.CheckState = Globals.getCheckboxCheckState(r.receipt_payback);

            //this.combo_operator.SelectedValue =
            //    (r.receipt__Operator != null ? r.receipt__Operator.operator_id : 0);

            this.dataGridView1.Enabled = false;
            this.dataGridView1.Rows.Clear();

            var itemDao = GlobalData.getIDao<IItemDao>();
            this.itemList = itemDao.GetReceiptItem(r.receipt_id);

            foreach (Item it in this.itemList)
            {
                DataGridViewRow row = this.dataGridView1.Rows[this.dataGridView1.Rows.Add()];

                this.setRowValue(row, it);
            }

            this.dataGridView1.VirtualMode = true;
            this.dataGridView1.VirtualMode = false;
            this.dataGridView1.Enabled = true;
            this.商品リストを表示LToolStripMenuItem.Enabled = true;
        }

        private void RefreshShowing()
        {
            if (this.isNewReceipt == true) { return; }
            
            var receiptDao = GlobalData.getIDao<IReceiptDao>();
            List<Receipt> rs = receiptDao.GetById(this.receipt.receipt_id);

            if (rs.Count == 0)
            {
                throw new Exception("指定したReceiptが見つかりませんでした");
            }

            this.SetReceipt(rs[0]);
        }

        private void SellerRadio_Changed(object sender, EventArgs e)
        {
            RadioButton rd = (RadioButton)sender;

            this.SuspendLayout();

            this.SetZaigakuEnabled(false);
            this.label_external.Enabled = false;
            this.text_external.Enabled = false;
            if (rd == this.radio_zaigaku)
            {
                this.SetZaigakuEnabled(true);
                this.label_external.Enabled = true;
                this.text_external.Enabled = true;

                //this.text_Enter_SelectAll(this.text_zai_nen, null);
                this.text_zai_nen.SelectAll();
                this.text_zai_nen.Focus();
            }
            else if (rd == this.radio_external)
            {
                this.label_external.Enabled = true;
                this.text_external.Enabled = true;
                this.text_external.SelectAll();
                this.text_external.Focus();

                var receiptDao = GlobalData.getIDao<IReceiptDao>();
                List<Receipt> reces = receiptDao.GetBySellerString(Receipt.seller_EXT);
                foreach ( Receipt r in reces ) {
                    if (this.text_external.Items.Contains(r.receipt_seller_exname))
                    {
                        //重複はスキップ
                        continue;
                    }
                    this.text_external.Items.Add(r.receipt_seller_exname);
                }
            }

            this.ResumeLayout();
        }

        private void SetZaigakuEnabled(bool enable)
        {
            this.text_zai_nen.Enabled = enable;
            this.text_zai_kumi.Enabled = enable;
            this.text_zai_ban.Enabled = enable;
            this.label_nen.Enabled = enable;
            this.label_kumi.Enabled = enable;
            this.label_ban.Enabled = enable;
        }

        private void ReceiptForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Enter)
            {
                this.button1.PerformClick();
            }
            else if (e.KeyCode == Keys.F5)
            {
                this.RefreshShowing();
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (this.receipt == null) { return; }

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
                it = new Item();
                it.item_receipt_id = this.receipt.receipt_id;
                it.item__Receipt = this.receipt;
                it.item_receipt_time = DateTime.Now;

                foreach (KeyValuePair<string, ColumnInfo> kvp in this.colinfos)
                {
                    this.changeDBdata(kvp.Key, it, dgv[kvp.Key, e.RowIndex].Value);
                }

                itemDao.Insert(it);
                dgv[ColumnName.shinaBan, e.RowIndex].Value = it.item_id;

                this.itemList.Add(it);

                GlobalData.Instance.recentItemForm.AddRecentItemId(it.item_id);
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.isNewReceipt)
                {
                    this.receipt = new Receipt();
                }

                receipt.receipt_pass = this.text_pass.Text;

                if (this.radio_external.Checked)
                {
                    receipt.receipt_seller = kaede2nd.Entity.Receipt.seller_EXT;
                    receipt.receipt_seller_exname = this.text_external.Text;
                }
                else if (this.radio_legacy.Checked)
                {
                    receipt.receipt_seller = kaede2nd.Entity.Receipt.seller_LAGACY;
                    receipt.receipt_seller_exname = null;
                }
                else if (this.radio_donate.Checked)
                {
                    receipt.receipt_seller = kaede2nd.Entity.Receipt.seller_DONATE;
                    receipt.receipt_seller_exname = null;
                }
                else
                { //在学

                    int nen;
                    //正規化
                    if (int.TryParse(this.text_zai_nen.Text, out nen) == false || !(1 <= nen && nen <= 3))
                    {
                        this.text_zai_nen.Focus();
                        throw new Exception("学年の値が不正です");
                    }


                    if (string.IsNullOrEmpty(this.text_zai_kumi.Text))
                    {
                        this.text_zai_kumi.Focus();
                        throw new Exception("組の値が空です");
                    }

                    string kumi = this.text_zai_kumi.Text.ToUpperInvariant().Substring(0, 1);
                    if (kumi != "1" && kumi != "2" && kumi != "3" && kumi != "4"
                        && kumi != "A" && kumi != "B" && kumi != "C")
                    {
                        this.text_zai_kumi.Focus();
                        throw new Exception("組の値が不正です");
                    }

                    int ban;
                    if (int.TryParse(this.text_zai_ban.Text, out ban) == false || !(0 <= ban && ban <= 99))
                    {
                        this.text_zai_ban.Focus();
                        throw new Exception("出席番号の値が不正です");
                    }

                    receipt.receipt_seller = nen.ToString("0") + kumi + ban.ToString("00");
                    receipt.receipt_seller_exname = this.text_external.Text;
                }

                receipt.receipt_payback = Globals.getBoolFromCheckState(this.check_payback.CheckState);

                //this.combo_operator.SelectedValue =
                //    (r.receipt__Operator != null ? r.receipt__Operator.operator_id : 0);

                GlobalData.Instance.mainForm.DoRefresh();


            }
            catch (Exception excep)
            {
                if (this.isNewReceipt)
                {
                    this.receipt = null;
                }
                MessageBox.Show(excep.Message);
                return;
            }

            var receiptDao = GlobalData.getIDao<IReceiptDao>();

            if (this.isNewReceipt)
            {
                receipt.receipt_time = DateTime.Now;

                receiptDao.Insert(receipt);
                //this.text_rid.Text = receipt.receipt_id.ToString("'R'0000");
                this.SetReceipt(receipt);

                //this.itemList = new List<Item>();
                //this.isNewReceipt = false;
                //this.dataGridView1.Enabled = true;

                this.dataGridView1.ClearSelection();
                this.dataGridView1.Rows[0].Cells[ColumnName.shouhinMei].Selected = true;
                this.dataGridView1.Focus();
            }
            else
            {
                try
                {
                    receiptDao.Update(receipt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("更新できませんでした: " + ex.ToString());
                    return;
                }
                this.SetReceipt(receipt);
            }
        }

        private void text_Enter_SelectAll(object sender, EventArgs e)
        {
            if (sender == this.text_external)
            {
                do
                {
                    if (this.text_external.Text != "") { break; }

                    int nen;
                    //正規化
                    if (int.TryParse(this.text_zai_nen.Text, out nen) == false || !(1 <= nen && nen <= 3))
                    {
                        break;
                    }

                    if (string.IsNullOrEmpty(this.text_zai_kumi.Text)) { break; }
                    string kumi = this.text_zai_kumi.Text.ToUpperInvariant().Substring(0, 1);
                    if (kumi != "1" && kumi != "2" && kumi != "3" && kumi != "4"
                        && kumi != "A" && kumi != "B" && kumi != "C")
                    {
                        break;
                    }

                    int ban;
                    if (int.TryParse(this.text_zai_ban.Text, out ban) == false || !(0 <= ban && ban <= 99))
                    {
                        break;
                    }

                    string seller = nen.ToString("0") + kumi + ban.ToString("00");

                    var receiptDao = GlobalData.getIDao<IReceiptDao>();
                    List<Receipt> reces = receiptDao.GetBySellerString(seller);

                    var names = from r in reces select r.receipt_seller_exname;
                    this.text_external.Items.AddRange(names.Distinct().ToArray());

                    if (reces.Count == 0) { break; }
                    else { this.text_external.Text = reces[0].receipt_seller_exname; }
                } while (false);
            }

            if (sender is TextBox)
            {
                ((TextBox)sender).SelectAll();
            }
            else if (sender is ComboBox)
            {
                ((ComboBox)sender).SelectAll();
            }
        }


        protected override bool IsEditableImpl()
        {
            if (this.receipt == null) { return false; }
            if (!this.dataGridView1.Enabled) { return false; }
            if (this.itemList == null) { throw new InvalidOperationException(); }

            return true;
        }



        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            if (e.KeyCode == Keys.Delete)
            {
                this.dgv_DeleteSelectedRow();
                return;
            }

            if (e.KeyCode == Keys.D && e.Control && !e.Shift && !e.Alt)
            {
                dgv.ExtendSelection();
                this.contextMenuStrip_rowHeader.Show(dgv, 0, 0);
                return;
            }

            if (e.KeyCode == Keys.S && e.Control && !e.Shift && !e.Alt)
            {
                DataGridViewSelectedCellCollection cells = dgv.SelectedCells;
                if (cells.Count > 0)
                {
                    if (dgv[ColumnName.shinaBan, cells[0].RowIndex].Value == null) { return; }

                    string res;
                    if (TitleSplitForm.Show_Dialog((string)dgv[ColumnName.shouhinMei, cells[0].RowIndex].Value, out res)
                         == DialogResult.OK)
                    {
                        string res2;
                        if (InputBox.Show_Dialog("「" + res + "」の巻数を入力してください", "セット商品支援", "", out res2, ImeMode.Off, HorizontalAlignment.Left)
                            == DialogResult.OK)
                        {
                            dgv[ColumnName.shouhinMei, cells[0].RowIndex].Value = res + " " + res2;
                        }
                    }
                }
                return;
            }
        }



        private void 商品名でIMEオンToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsm = (ToolStripMenuItem)sender;
            GlobalData.Instance.itemNameImeOn = tsm.Checked;
            this.colinfos[ColumnName.shouhinMei].imeMode = tsm.Checked ? ImeMode.On : ImeMode.Off;
        }

        private void 商品名編集Enterで右移動ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsm = (ToolStripMenuItem)sender;
            GlobalData.Instance.enterToTab = tsm.Checked;
            this.dataGridView1.enterToTab = tsm.Checked;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.contextMenuStrip_temp.Show((Button)sender, new Point(0, ((Button)sender).Size.Height));
        }

        private void 票印刷ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (this.receipt == null) { return; }
            DataGridView dgv = this.dataGridView1;
            if (!dgv.Enabled) { return; }
            if (this.itemList == null) { throw new InvalidOperationException(); }


            PageSetupDialog psd = new PageSetupDialog();
            psd.EnableMetric = true; //cf. KB814355 et http://dobon.net/vb/dotnet/graphics/pagesetupdialogbug.html
            psd.PageSettings = GlobalData.Instance.receipt_pageSettings;
            psd.PrinterSettings = GlobalData.Instance.receipt_printerSettings;
            psd.AllowMargins = false;
            psd.ShowDialog();

            PrintDialog prid = new PrintDialog();
            prid.PrinterSettings = GlobalData.Instance.receipt_printerSettings;
            prid.UseEXDialog = true;
            prid.Document = new ReceiptPrintDocument(this.receipt, GlobalData.Instance.receipt_pageSettings, GlobalData.Instance.receipt_printerSettings);
            DialogResult pdres = prid.ShowDialog();

            if (pdres != DialogResult.OK) { return; }

            try
            {
                prid.Document.Print();
            }
            catch (Exception iex)
            {
                MessageBox.Show("印刷が実行できませんでした: " + iex.ToString());
            }
        }

        private void 最新の情報に更新RToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.RefreshShowing();
        }

        private void 商品リストを表示LToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ( this.receipt == null ) { return; }
            uint rid = this.receipt.receipt_id;
            try
            {
                Form_ItemList f = new Form_ItemList(delegate()
                {
                    var itemDao = GlobalData.getIDao<IItemDao>();
                    return itemDao.GetReceiptItem(rid);
                }, "R" + rid.ToString("0000") + " " + this.receipt.getSellerString() + " の商品リスト", "R" + rid.ToString("0000"));
                f.Show();
            }
            catch (Exception excep)
            {
                MessageBox.Show("商品一覧ウィンドウが生成できませんでした: " + excep.Message);
            }
        }

        
    }

}
