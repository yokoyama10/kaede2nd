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

                if (s.Length == 8 && s.StartsWith(GlobalData.Instance.barcodePrefix))
                {
                    char check = s[7];

                    byte[] di = new byte[7];
                    byte cdi;
                    try
                    {
                        for (int i = 0; i < 7; i++)
                        {
                            if (!char.IsDigit(s[i])) { throw new Exception(); }
                            di[i] = byte.Parse(s.Substring(i, 1));
                        }

                        if (!char.IsDigit(check)) { throw new Exception(); }
                        cdi = byte.Parse(check.ToString());

                        //CheckDigit
                        int c1 = (di[0] + di[2] + di[4] + di[6]) * 3 +
                                di[1] + di[3] + di[5];
                        int c2 = (10 - (c1 % 10)) % 10;

                        if (c2.ToString() != check.ToString())
                        {
                            throw new Exception();
                        }

                    }
                    catch
                    {
                        this.textBox_ban_err("バーコードの値が不正です");
                        return;
                    }

                    shinaBan = uint.Parse(s.Substring(2, 5));
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

                this.curItem = its[0];

                this.textBox_name.BackColor = SystemColors.Control;
                this.textBox_name.Text = its[0].item_name;
                this.textBox_teika.Text = its[0].item_tagprice.ToString("#,##0");
                this.textBox_nebiki.Text = its[0].item_return /*FIXME*/ ? "×" : "○";

                this.textBox_baika.Focus();
                this.textBox_baika.SelectAll();
            }
        }

        private void textBox_ban_err(string err)
        {
            this.curItem = null;

            this.textBox_name.BackColor = Color.LightYellow;
            this.textBox_name.Text = err;
            this.textBox_teika.Text = "";
            this.textBox_nebiki.Text = "";

            this.textBox_ban.Focus();
            this.textBox_ban.SelectAll();
        }
    }
}
