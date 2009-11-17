using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace kaede2nd
{
    static class Program
    {
        public const string 御名 = "\u7530\u6751\u3086\u304b\u308a";


        public static bool continueProg;

        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ToolStripManager.RenderMode = ToolStripManagerRenderMode.System;
            continueProg = true;

            while (continueProg)
            {
                continueProg = false;
                Application.Run(new Form1());
            }
        }
    }
}

/*
SELECT date(item_selltime) as SellDate, sum(item_sellprice) FROM en_gara.item WHERE item_sellprice IS NOT NULL GROUP BY SellDate

*/
