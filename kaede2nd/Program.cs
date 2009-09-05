using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace kaede2nd
{
    static class Program
    {

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

・返品わすれる
・１６ごと印刷
・IMEバーコードで2回Enter?
・バーコード打鍵速度


*/
