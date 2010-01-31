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
CREATE TABLE  `config` (
  `config_id` int(10) unsigned NOT NULL,
  `config_bumonname` varchar(255) NOT NULL DEFAULT '不明な部門',
  `config_companyname` varchar(255) NOT NULL DEFAULT '不明な縁日班不明部門',
  `config_barcodeprefix` char(2) NOT NULL DEFAULT '0',
  `config_symbolcolor_argb` int(11) NOT NULL DEFAULT '-1',
  `config_itemname_imeon` tinyint(1) NOT NULL DEFAULT '1',
  `config_entertotab` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`config_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
         */

        [Column("config_id")]
        public UInt32 __config_id { get; set; } //0固定。DO NOT TOUCH（これはひどい）

        
        public string config_bumonname { get; set; }
        public string config_companyname { get; set; }
        public string config_barcodeprefix { get; set; } //^[0-9][0-9]$
        public int config_symbolcolor_argb { get; set; }

        public bool config_itemname_imeon { get; set; }
        public bool config_entertotab { get; set; }

        //Constructor
        public ConfigEntity()
        {
            this.__config_id = 0;
            this.config_bumonname = "不明な部門";
            this.config_companyname = "不明な縁日班不明部門";
            this.config_barcodeprefix = "00";
            this.config_symbolcolor_argb = System.Drawing.Color.White.ToArgb();
            this.config_itemname_imeon = true;
            this.config_entertotab = false;
        }

    }
}
