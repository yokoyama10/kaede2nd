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
        private uint kansa_flagnum = 1;

        private Timer timer1;

        public KansaForm() : this(1)
        {
        }

        public KansaForm(uint flagnum)
        {
            if (1 <= flagnum && flagnum <= 3)
            {
                InitializeComponent();
                this.kansa_flagnum = flagnum;
                this.Text = "監査 #" + flagnum.ToString() + " (" + GlobalData.Instance.bumonName + ")";
                this.label_kansacnt.Text = this.Text;
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }

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

                switch (this.kansa_flagnum)
                {
                    case 1:
                        cnt = itDao.CountNeedKansaItem_NotFlagged1();
                        break;
                    case 2:
                        cnt = itDao.CountNeedKansaItem_NotFlagged2();
                        break;
                    case 3:
                        cnt = itDao.CountNeedKansaItem_NotFlagged3();
                        break;
                    default:
                        throw new InvalidOperationException();
                }

                ControlUtil.SafelyOperated(this.textBox_remain, (MethodInvoker)delegate(){
                    this.textBox_remain.Text = cnt.ToString();
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

                this.kansaItem(its[0]);
            }
        }

        private void kansaItem(Item it)
        {
            this.curItem = it;

            this.textBox_ban.Text = it.item_id.ToString();

            this.textBox_name.BackColor = SystemColors.Control;
            this.textBox_name.Text = it.item_name;
            this.textBox_teika.Text = it.item_tagprice.ToString("#,##0");
            this.textBox_nebiki.Text = it.item_return /*FIXME*/ ? "×" : "○";

            if (it.item_sellprice.HasValue)
            {
                this.textBox_baika.Text = it.item_sellprice.Value.ToString();
                this.button_mibai.Enabled = true;
                this.button_teisei.Enabled = true;
                this.textBox_baika.BackColor = SystemColors.Control;


                if (it.item_kansa_end.HasValue)
                {
                    //監査済み
                    this.label_error.Text = "監査が完全終了した品です。昨日販売では？";
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
                            this.label_error.Text = "だれかが監査したようです";
                        }
                        else
                        {
                            this.label_error.Text = lop[0].operator_name + " が監査したようです";
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

                        this.label_error.Text = "監査しました";

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

                this.label_error.Text = "未売却は監査できません";
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

            this.kansaItem(curItem);
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

        private uint? GetKansaFlag(Item it)
        {
            if (it == null) { throw new NullReferenceException(); }

            switch (this.kansa_flagnum)
            {
                case 1:
                    return it.item_kansa_flag1;
                case 2:
                    return it.item_kansa_flag2;
                case 3:
                    return it.item_kansa_flag3;
                default:
                    throw new InvalidOperationException();
            }
        }

        private void SetKansaFlag(Item it, uint? val)
        {
            if (it == null) { throw new NullReferenceException(); }

            switch (this.kansa_flagnum)
            {
                case 1:
                    it.item_kansa_flag1 = val;
                    return;
                case 2:
                    it.item_kansa_flag2 = val;
                    return;
                case 3:
                    it.item_kansa_flag3 = val;
                    return;
                default:
                    throw new InvalidOperationException();
            }
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
                DialogResult dres = InputBox.ShowIntDialog("修正後の売価を入力してください", "売価修正", def, out res);

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
                    itDao.Update(this.curItem);

                    this.kansaItem(this.curItem);

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

                        switch (this.kansa_flagnum)
                        {
                            case 1:
                                return itemDao.GetNeedKansaItem_NotFlagged1();
                            case 2:
                                return itemDao.GetNeedKansaItem_NotFlagged2();
                            case 3:
                                return itemDao.GetNeedKansaItem_NotFlagged3();
                            default:
                                throw new InvalidOperationException();
                        }

                    }, "未" + this.Text, "未監査");
            }

            this.listForm.Show();
            this.listForm.Activate();
        }

        private void KansaForm_Load(object sender, EventArgs e)
        {
            this.button1.PerformClick();
        }
    }
}
