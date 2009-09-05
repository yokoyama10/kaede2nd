using System;
using System.Collections.Generic;
using System.Text;

using Seasar.Dao.Attrs;

namespace kaede2nd.Entity
{

    /*
        CREATE TABLE `external` (
           `external_id` int(10) unsigned,
           `external_type` char(2),
           `external_name` varchar(255),
           `external_comment` text,
           PRIMARY KEY (`external_id`),
           UNIQUE KEY (`external_name`)
        ) ENGINE=InnoDB DEFAULT CHARSET utf8;
    */

    [Table("external")]
    public class External
    {
        static public Dictionary<string, string> ExtTypes = new Dictionary<string, string>()
        { {"OB", "OB"}, {"TC", "教員"}, {"TA", "他校"} };

        [ID(IDType.IDENTITY)]
        public UInt32 external_id { get; set; }
        public string external_type { get; set; }
        public string external_name { get; set; }
        public string external_comment { get; set; }

        public string GetFullString()
        {
            return this.external_name + " (" + this.GetTypeString() + ")";
        }

        public string GetTypeString()
        {
            string ret;
            if (External.ExtTypes.TryGetValue(this.external_type, out ret) == false) { ret = "不明"; }

            return ret;
        }
    }
}
