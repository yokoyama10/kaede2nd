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
    public partial class KansaForm : Form
    {
        private Item curItem = null;

        private Operator nowOperator = null;

        private Timer timer1;

        public KansaForm()
        {
            InitializeComponent();
            this.Text = "監査  (" + GlobalData.Instance.data.bumonName + ")";

            this.timer1 = new Timer();
            timer1.Interval = 5 * 1000;
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Enabled = true;

            this.RefreshRemain();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.RefreshRemain();
        }

        private void RefreshRemain()
        {
            System.Threading.Thread t = new System.Threading.Thread(delegate()
            {
                IItemDao itDao = GlobalData.getIDao<IItemDao>();
                UInt32 cnt;

                cnt = itDao.CountNeedKansaItem_NotFlagged();

                ControlUtil.SafelyOperated(this.textBox_remain, (MethodInvoker)delegate()
                {
                    this.textBox_remain.Text = cnt.ToString();
                });

                UInt32 allcnt;
                allcnt = itDao.CountNeedKansaItem();

                ControlUtil.SafelyOperated(this.textBox_allkansa, (MethodInvoker)delegate()
                {
                    this.textBox_allkansa.Text = allcnt.ToString();
                });

                UInt32 sum;
                sum = itDao.SumNeedKansaItem_SellPrice();
                ControlUtil.SafelyOperated(this.textBox_sum, (MethodInvoker)delegate()
                {
                    this.textBox_sum.Text = sum.ToString("#,##0");
                });
            });

            t.IsBackground = true;
            t.Start();
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

                if (!(0 <= shinaBan && shinaBan <= 999999))
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

                this.DoKansaItem(its[0]);
            }
        }

        private void DoKansaItem(Item it)
        {
            this.curItem = it;

            this.textBox_ban.Text = it.item_id.ToString();

            this.textBox_name.BackColor = SystemColors.Control;
            this.textBox_name.Text = it.item_name;
            this.textBox_teika.Text = it.item_tagprice.ToString("#,##0");
            this.textBox_nebiki.Text = it.item_return /*FIXME*/ ? "×" : "○";

            if (it.item_sellprice.HasValue)
            {
                if (it.item_sellprice.Value == it.item_tagprice)
                {
                    this.textBox_baika.Text = "-- " + it.item_sellprice.Value.ToString();
                }
                else
                {
                    this.textBox_baika.Text = it.item_sellprice.Value.ToString();
                }

                if (it.item_sell__Operator != null)
                {
                    this.textBox_sellop.Text = it.item_sell__Operator.operator_name;
                }
                else
                {
                    this.textBox_sellop.Text = "不明";
                }

                this.textBox_selltime.Text = Globals.getTimeString(it.item_selltime);

                this.button_mibai.Enabled = true;
                this.button_teisei.Enabled = true;
                this.textBox_baika.BackColor = SystemColors.Control;


                if (it.item_kansa_end.HasValue)
                {
                    //監査済み
                    this.label_error.Text = "監査対象ではありません。本日の販売ではない可能性が。";
                    this.label_error.Visible = true;
                }
                else
                {
                    if (this.GetKansaFlag(it).HasValue)
                    {
                        IOperatorDao opDao = GlobalData.getIDao<IOperatorDao>();
                        List<Operator> lop = opDao.GetById(this.GetKansaFlag(it).Value);

                        if (lop.Count() == 0)
                        {
                            this.label_error.Text = "だれかが既に監査したようです";
                        }
                        else
                        {
                            this.label_error.Text = lop[0].operator_name + " が既に監査しています";
                        }

                        this.label_error.Visible = true;
                    }
                    else
                    {
                        System.Threading.Thread t = new System.Threading.Thread(
                            delegate(object item)
                            {
                                Item itemmm = (Item)item;
                                this.SetKansaFlag(itemmm, this.nowOperator.operator_id);

                                IItemDao idao = GlobalData.getIDao<IItemDao>();
                                idao.Update(itemmm);
                            }
                        );

                        t.IsBackground = true;
                        t.Start(it);

                        this.label_error.Text = "監査しました。次の品番を入力してね";

                        this.RefreshRemain();
                    }
                }

            }
            else
            {
                this.button_teisei.Enabled = true;
                this.button_mibai.Enabled = false;
                this.textBox_baika.Text = "未売却";
                this.textBox_baika.BackColor = Color.LightPink;
                this.textBox_sellop.Text = null;
                this.textBox_selltime.Text = null;

                this.label_error.Text = "未売却の商品は監査できません";
                this.label_error.Visible = true;
            }

            this.textBox_ban.Focus();
            this.textBox_ban.SelectAll();
        }

        private void textBox_ban_err(string err)
        {
            this.curItem = null;

            this.textBox_name.BackColor = Color.LightYellow;
            this.textBox_name.Text = err;
            this.textBox_teika.Text = "";
            this.textBox_nebiki.Text = "";

            this.button_teisei.Enabled = false;
            this.button_mibai.Enabled = false;

            this.textBox_baika.BackColor = SystemColors.Control;
            this.textBox_baika.ReadOnly = true;

            this.textBox_ban.Focus();
            this.textBox_ban.SelectAll();
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

        private void button_mibai_Click(object sender, EventArgs e)
        {
            if (curItem == null) { return; }

            curItem.item_sellprice = null;
            curItem.item_selltime = null;

            IItemDao idao = GlobalData.getIDao<IItemDao>();
            idao.Update(curItem);

            this.DoKansaItem(idao.GetItemById(curItem.item_id).First());
            this.label_error.Text = "商品を未売却にしました";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Operator op;
            if (SelectOperator.ShowSelectOperatorDialog(out op) == DialogResult.OK)
            {
                this.nowOperator = op;
            }
            else
            {
                this.Close();
                return;
            }

            this.textBox_operator.Text = this.nowOperator.operator_name;
            this.textBox_ban_err("品番を入力してください");
        }

        private uint? GetKansaFlag(Item it)
        {
            if (it == null) { throw new NullReferenceException(); }

            return it.item_kansa_flag1;
        }

        private void SetKansaFlag(Item it, uint? val)
        {
            if (it == null) { throw new NullReferenceException(); }

            it.item_kansa_flag1 = val;
        }

        private void button_teisei_Click(object sender, EventArgs e)
        {
            if (this.curItem == null) { return; }

            string def;

            if (this.curItem.item_sellprice.HasValue)
            {
                def = this.curItem.item_sellprice.ToString();
            }
            else
            {
                def = "未売却";
            }

            while (true)
            {
                string res;
                DialogResult dres = InputBox.ShowIntDialog("修正後の売価を入力してください\n商品名: " + this.curItem.item_name, "売価修正", def, out res);

                if (dres == DialogResult.Cancel)
                {
                    return;
                }
                else
                {
                    uint baika;
                    if (!uint.TryParse(res, out baika))
                    {
                        def = "不正な文字列です";
                        continue;
                    }

                    IItemDao itDao = GlobalData.getIDao<IItemDao>();
                    this.curItem.item_sellprice = baika;
                    this.curItem.item_selltime = DateTime.Now;
                    this.curItem.item_sell_operator = this.nowOperator.operator_id;
                    itDao.Update(this.curItem);

                    this.DoKansaItem(itDao.GetItemById(curItem.item_id).First());
                    this.label_error.Text = "金額訂正が完了しました";

                    return;
                }
            }
        }

        private Form_ItemList listForm = null;
        private void button2_Click(object sender, EventArgs e)
        {
            if (this.listForm == null || this.listForm.IsDisposed)
            {
                this.listForm = new Form_ItemList(
                    (Form_ItemList.ItemReturnDelegate)delegate()
                    {
                        var itemDao = GlobalData.getIDao<IItemDao>();

                        return itemDao.GetNeedKansaItem_NotFlagged();

                    }, "未" + this.Text, "未監査");
            }

            this.listForm.Show();
            this.listForm.Activate();
        }

        private Form_ItemList allListForm = null;
        private void button3_Click(object sender, EventArgs e)
        {
            if (this.allListForm == null || this.allListForm.IsDisposed)
            {
                this.allListForm = new Form_ItemList(
                    (Form_ItemList.ItemReturnDelegate)delegate()
                    {
                        var itemDao = GlobalData.getIDao<IItemDao>();
                        return itemDao.GetNeedKansaItem();
                    }, "監査対象", "監査対象");

            }

            this.allListForm.Show();
            this.allListForm.Activate();
        }

        private void KansaForm_Load(object sender, EventArgs e)
        {
            this.button1.PerformClick();
        }

        private void button_allend_Click(object sender, EventArgs e)
        {

            DialogResult res = MessageBox.Show("このコマンドは、当日の監査が終了したときに一度だけ実行してください。\n" +
                            "\"監査対象\" の品をクリアしますか？", "監査完了？", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            if (res != DialogResult.Yes) { return; }
            
            var itemDao = GlobalData.getIDao<IItemDao>();
            List<Item> lit = itemDao.GetNeedKansaItem();

            DateTime dt = DateTime.Now;
            for (int i = 0; i < lit.Count; i++)
            {
                lit[i].item_kansa_end = dt;
                itemDao.Update(lit[i]);
            }

            this.RefreshRemain();
        }

    }
}
