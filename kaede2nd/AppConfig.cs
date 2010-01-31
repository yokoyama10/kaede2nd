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

        public AppConfig()
        {
            this.ConnectList = new List<Connection>();
            this.ShowForm_RecentItem = true;
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
        }
    }
}
