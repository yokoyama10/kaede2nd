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
        private static string configFile = "kaedecfg.xml";

        public static bool continueProg;

        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            SeasarValueType.AddValueType();

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
                    dbname = "database_name",
                    is_readonly = false
                });

                c.configPath = cfgFullPath;
                c.SaveToFile();
                MessageBox.Show("コンフィグファイル (" + c.GetConfigFileName() + ") を作成しました。");
            }

            try
            {
                config = AppConfig.LoadFromFile(cfgFullPath);
            }
            catch (Exception e)
            {
                MessageBox.Show("コンフィグファイル (" + configFile + ") が正常に開けませんでした。\nファイルが間違っていないか確認するか、諦めて削除して起動しなおしてください。\n\n詳細:\n" + e.Message);
                System.Environment.Exit(-1);
            }


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ToolStripManager.RenderMode = ToolStripManagerRenderMode.System;
            continueProg = true;

            while (continueProg)
            {
                continueProg = false;
                Application.Run(new Form1());

                try
                {
                    config.SaveToFile();
                }
                catch (Exception e)
                {
                    MessageBox.Show("コンフィグファイル (" + config.GetConfigFileName() + ") が保存できませんでした。\n " + e.Message);
                }
            }
        }
    }
}

/*
SELECT date(item_selltime) as SellDate, sum(item_sellprice) FROM en_gara.item WHERE item_sellprice IS NOT NULL GROUP BY SellDate

*/
