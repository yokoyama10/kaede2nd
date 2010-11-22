using System;
using System.Collections.Generic;
using System.Text;

using Seasar.Dao.Attrs;

namespace kaede2nd.Entity
{
    [Table("config")]
    public class ConfigEntity
    {
        /*
CREATE TABLE  `en_test`.`config` (
  `config_name` char(128) NOT NULL,
  `config_value` text NOT NULL,
  PRIMARY KEY  (`config_name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
         */

        public string config_name { get; set; }
        public string config_value { get; set; }

        public ConfigEntity() { }

        public ConfigEntity(string name, string value)
        {
            this.config_name = name;
            this.config_value = value;
        }
    }
}
