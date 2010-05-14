using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace kaede2nd
{
    [Serializable]
    public class AppConfig
    {
        public List<Connection> ConnectList;

        [OptionalFieldAttribute]
        public bool ShowForm_RecentItem;

        [OptionalFieldAttribute]
        public bool ShowPrintDialog_AtTagPrint;

        [OptionalFieldAttribute]
        public string BackupDirectory;

        [NonSerialized]
        [System.Xml.Serialization.XmlIgnore]
        public string configPath;

        public AppConfig()
        {
            this.ConnectList = new List<Connection>();
            this.ShowForm_RecentItem = true;
            this.ShowPrintDialog_AtTagPrint = true;
            this.configPath = "";
            this.BackupDirectory = @".\backup";
        }


        [Serializable]
        public class Connection
        {
            public string cfgname;
            public string host;
            public string port;
            public string user;
            public string pass;
            public string dbname;
            public bool is_readonly;
        }



        // FileIO系のExceptionが飛ぶよ
        public void SaveToFile()
        {
            var seri = new System.Xml.Serialization.XmlSerializer(typeof(AppConfig));

            using (System.IO.FileStream fs = new System.IO.FileStream(this.configPath, System.IO.FileMode.Create))
            {
                seri.Serialize(fs, this);
                fs.Close();
            }
        }


        // FileIO系のExceptionが飛ぶよ
        public static AppConfig LoadFromFile(string path)
        {
            var seri = new System.Xml.Serialization.XmlSerializer(typeof(AppConfig));
            AppConfig config = null;

            using (System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Open))
            {
                config = (AppConfig)seri.Deserialize(fs);
                fs.Close();
            }

            config.configPath = path;

            return config;

        }

        public string GetConfigFileName()
        {
            if (string.IsNullOrEmpty(this.configPath)) { return ""; }
            return System.IO.Path.GetFileName(this.configPath);
        }
    }
}
