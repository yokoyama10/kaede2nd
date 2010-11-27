using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace kaede2nd
{
    public partial class Version : Form
    {
        Timer tim;
        public Version()
        {
            InitializeComponent();
            this.label_ver.Text = @"Version: 2010-11-27";
            this.tim_Tick(null, EventArgs.Empty);

            tim = new Timer();
            tim.Interval = 1000;
            tim.Tick += new EventHandler(tim_Tick);
            tim.Enabled = true;

            
        }

        private static string birth(DateTime d, string name, string canoname, int month, int date)
        {
            if (d.Month == month && d.Day == date)
            {
                return canoname + "さん、御誕生日おめでとう！";
            }
            else
            {
                DateTime d1 = new DateTime(d.Year, month, date, 0, 0, 0);
                DateTime d2 = new DateTime(d.Year + 1, month, date, 0, 0, 0);

                TimeSpan ts = d1 - d;
                if (ts.Milliseconds < 0) { ts = d2 - d; }
                return string.Format(name + "の誕生日まで、あと{0}日と{1:00}:{2:00}:{3:00}", ts.Days, ts.Hours, ts.Minutes, ts.Seconds);
            }
        }

        private void tim_Tick(object sender, EventArgs e)
        {
            DateTime d = DateTime.Now.ToUniversalTime().AddHours(9);

            //date
            this.label_date.Text = string.Format("王国歴{0}年{1}月{2}日 {3} (UTC+9)", d.Year - 1975, d.Month, d.Day, d.ToString("HH:mm:ss"));

            //hime
            this.label_hime.Text = birth(d, "ゆかり姫", Program. /**/
 御名
                                                                 /**/ , 2, 27);

            //kaede
            this.label_kaede.Text = birth(d, "楓ちゃん", "柏木楓", 11, 15);
        }

        private void Version_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.tim.Enabled = false;
            this.tim = null;
        }

    }
}
