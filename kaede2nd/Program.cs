using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace kaede2nd
{
    static class Program
    {
        public const string 御名 = "\u7530\u6751\u3086\u304b\u308a";

        public static AppConfig config;
        public static string configFile = "kaedecfg.xml";
        public static bool continueProg;

        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            var seri = new System.Xml.Serialization.XmlSerializer(typeof(AppConfig));

            string cfgFullPath = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), configFile);
            if (!System.IO.File.Exists(cfgFullPath))
            {
                AppConfig c = new AppConfig();
                c.ConnectList.Add(new AppConfig.Connection
                {
                    cfgname = "テスト部門",
                    host = "localhost",
                    port = "3306",
                    user = "username",
                    pass = "password",
                    dbname = "database_name"
                });
                System.IO.FileStream fs = new System.IO.FileStream(cfgFullPath, System.IO.FileMode.OpenOrCreate);

                seri.Serialize(fs, c);
                fs.Close();

                MessageBox.Show("コンフィグファイル (" + configFile + ") を作成しました。");
            }

            try
            {
                System.IO.FileStream fs = new System.IO.FileStream(cfgFullPath, System.IO.FileMode.Open);
                config = (AppConfig)seri.Deserialize(fs);
            }
            catch (Exception e)
            {
                MessageBox.Show("コンフィグファイル (" + configFile + ") が正常に開けませんでした: " + e.Message);
                Application.Exit();
            }


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
