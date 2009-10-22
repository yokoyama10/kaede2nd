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
    public partial class SellForm : Form
    {
        private Item curItem = null;

        private Item privItem = null;
        private uint? privItemSellPrice = null;
        private DateTime? privItemSellTime = null;
        private Operator privItemSellOperator = null;

        private Operator nowOperator = null;

        public SellForm()
        {
            InitializeComponent();
            this.Text = "売却 (" + GlobalData.Instance.bumonName + ")";
        }

        private void textBox_Enter(object sender, EventArgs e)
        {
            TextBox tx = (TextBox)sender;
            tx.SelectAll();
        }

        private void textBox_ban_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string s = this.textBox_ban.Text;
                if ( s == null ) { return; }

                uint shinaBan;

                if ( Globals.TryParseBarcode(s, out shinaBan) ) {
                }
                else
                {
                    if (!uint.TryParse(this.textBox_ban.Text, out shinaBan))
                    {
                        this.textBox_ban_err("不明な文字列です");
                        return;
                    }
                }

                if (!(0 <= shinaBan && shinaBan <= 99999))
                {
                    this.textBox_ban_err("不正な品番です");
                }

                IItemDao itemDao = GlobalData.getIDao<IItemDao>();
                List<Item> its = itemDao.GetItemById(shinaBan);
                if (its.Count == 0)
                {
                    this.textBox_ban_err("品番に該当する商品がありません");
                    return;
                }

                this.setItem(its[0]);
            }
        }

        private void setItem(Item it)
        {
            this.curItem = it;

            this.textBox_ban.Text = it.item_id.ToString();

            this.textBox_name.BackColor = SystemColors.Control;
            this.textBox_name.Text = it.item_name;
            this.textBox_teika.Text = it.item_tagprice.ToString("#,##0");
            this.textBox_nebiki.Text = it.item_return /*FIXME*/ ? "×" : "○";
            if (it.item_sellprice.HasValue)
            {
                this.label_sellzumi.Visible = true;
                this.label_sellzumi.Text = "売却済 \\" + it.item_sellprice.Value.ToString("#,##0");
                this.textBox_baika.Text = it.item_sellprice.Value.ToString();
                this.button_mibai.Visible = true;
            }
            else
            {
                this.label_sellzumi.Visible = false;
                this.button_mibai.Visible = false;
                this.textBox_baika.Text = "";
            }

            this.label_baikaEnter.Visible = true;
            this.textBox_baika.ReadOnly = false;
            this.textBox_baika.BackColor = SystemColors.Window;
            this.textBox_baika.Focus();
            this.textBox_baika.SelectAll();
        }

        private void textBox_ban_err(string err)
        {
            this.curItem = null;

            this.textBox_name.BackColor = Color.LightYellow;
            this.textBox_name.Text = err;
            this.textBox_teika.Text = "";
            this.textBox_nebiki.Text = "";

            this.label_sellzumi.Visible = false;
            this.button_mibai.Visible = false;

            this.textBox_baika.BackColor = SystemColors.Control;
            this.textBox_baika.ReadOnly = true;
            this.label_baikaEnter.Visible = false;

            this.textBox_ban.Focus();
            this.textBox_ban.SelectAll();
        }

        private void textBox_baika_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.curItem == null) { return; }
                string s = this.textBox_baika.Text;

                uint sute;
                if (Globals.TryParseBarcode(s, out sute))
                {
                    this.textBox_baika_err();
                    return;
                }

                uint baika;
                if (s == "-")
                {
                    baika = curItem.item_tagprice;
                }
                else
                {
                    if (!uint.TryParse(s, out baika))
                    {
                        this.textBox_baika_err();
                        return;
                    }
                }

                privItem = curItem;
                privItemSellPrice = curItem.item_sellprice;
                privItemSellTime = curItem.item_selltime;
                privItemSellOperator = curItem.item_sell__Operator;

                curItem.item_sellprice = baika;
                curItem.item_selltime = DateTime.Now;
                curItem.item_sell__Operator = this.nowOperator;


                Item i = curItem;
                System.Threading.Thread t = new System.Threading.Thread(
                    delegate(object item)
                    {
                        IItemDao idao = GlobalData.getIDao<IItemDao>();
                        idao.Update((Item)item);
                    }
                );
                t.Start(i);

                this.textBox_ban_err("次の品番を入力してね");
            }
        }

        private void textBox_baika_err()
        {
            this.textBox_baika.BackColor = Color.LightPink;
            this.textBox_baika.Focus();
            this.textBox_baika.SelectAll();
        }

        private void SellForm_Load(object sender, EventArgs e)
        {
            this.textBox_ban_err("品番を入力してください");

            this.button1.PerformClick();
        }

        private void SellForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Z)
            {
                if (this.privItem == null) { return; }

                uint? priv_canceledsellprice = this.privItem.item_sellprice;
                this.privItem.item_sellprice = privItemSellPrice;
                this.privItem.item_selltime = privItemSellTime;
                this.privItem.item_sell__Operator = privItemSellOperator;

                IItemDao idao = GlobalData.getIDao<IItemDao>();
                idao.Update(this.privItem);

                this.setItem(this.privItem);

                this.textBox_baika.Text = priv_canceledsellprice.ToString();
                this.textBox_baika.Focus();
                this.textBox_baika.SelectAll();

                this.privItem = null;                
            }
        }

        private void button_mibai_Click(object sender, EventArgs e)
        {
            if (curItem == null) { return; }

            privItem = curItem;
            privItemSellPrice = curItem.item_sellprice;
            privItemSellTime = curItem.item_selltime;
            privItemSellOperator = curItem.item_sell__Operator;

            curItem.item_sellprice = null;
            curItem.item_selltime = null;

            IItemDao idao = GlobalData.getIDao<IItemDao>();
            idao.Update(curItem);

            this.textBox_ban_err("次の品番を入力してね");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string err = "オペレータID";
            while (true)
            {
                string res;
                DialogResult dres = InputBox.ShowIntDialog("あなたのオペレータIDを入力してください", "売却入力者", err, out res);

                if (dres == DialogResult.Cancel)
                {
                    this.Close();
                    return;
                }
                else
                {
                    uint oid;
                    if (!uint.TryParse(res, out oid))
                    {
                        err = "不正な文字列です";
                        continue;
                    }

                    IOperatorDao opdao = GlobalData.getIDao<IOperatorDao>();
                    List<Operator> lo = opdao.GetById(oid);

                    if (lo.Count != 1)
                    {
                        err = "該当するオペレータIDがありません";
                        continue;
                    }

                    this.nowOperator = lo[0];
                    break;
                }
            }

            this.textBox_operator.Text = this.nowOperator.operator_name;
        }
    }
}
