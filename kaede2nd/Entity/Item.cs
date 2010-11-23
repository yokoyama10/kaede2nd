using System;
using System.Collections.Generic;
using System.Text;

using Seasar.Dao.Attrs;

namespace kaede2nd.Entity
{

    [Table("item")]
    public class Item
    {
        /*
         * ALTER TABLE item ADD COLUMN item_volumes INT(10) UNSIGNED DEFAULT NULL AFTER item_isbn;
         * 
         * ALTER TABLE item ADD COLUMN `item_sell_operator` INT(10) UNSIGNED DEFAULT NULL AFTER item_selltime;
         * ALTER TABLE item ADD CONSTRAINT `item_ibfk_4` FOREIGN KEY (`item_sell_operator`) REFERENCES `operator` (`operator_id`) ON DELETE SET NULL;
         * 
         * ALTER TABLE item ADD COLUMN `item_kansa_end` DATETIME DEFAULT NULL AFTER item_sell_operator;
         * ALTER TABLE item ADD COLUMN `item_kansa_flag1` INT(10) UNSIGNED DEFAULT NULL AFTER item_kansa_end;
         * //ALTER TABLE item ADD COLUMN `item_kansa_flag2` INT(10) UNSIGNED DEFAULT NULL AFTER item_kansa_flag1;
         * //ALTER TABLE item ADD COLUMN `item_kansa_flag3` INT(10) UNSIGNED DEFAULT NULL AFTER item_kansa_flag2;
         */

        /*
        CREATE TABLE `item` (
       `item_id` int(10) unsigned,
       `item_receipt_id` int(10) unsigned,
       `item_name` varchar(255),
       `item_tagprice` int(10) unsigned,
       `item_tataki` tinyint(1),
       `item_return` tinyint(1),
       `item_genre` varchar(255),
       `item_sellway` varchar(255),
       `item_receipt_time` datetime,
       `item_receipt_operator` int(10) unsigned,
       `item_sellprice` int(10) unsigned,
       `item_selltime` datetime,
       `item_sell_operator` int(10) unsigned DEFAULT NULL,
       `item_kansa_end` DATETIME DEFAULT NULL,
       `item_kansa_flag1` int(10) unsigned DEFAULT NULL,
       `item_adjust` int(11),
       `item_isbn` bigint(20) unsigned,
       `item_volumes` int(10) unsigned DEFAULT NULL,
       `item_comment` varchar(255),
       `item_sellcomment` text,
       `item_userspace` text,
       PRIMARY KEY (`item_id`),
       CONSTRAINT `item_ibfk_1` FOREIGN KEY (`item_receipt_id`) REFERENCES `receipt` (`receipt_id`) ON DELETE CASCADE,
       CONSTRAINT `item_ibfk_2` FOREIGN KEY (`item_receipt_operator`) REFERENCES `operator` (`operator_id`) ON DELETE SET NULL,
       CONSTRAINT `item_ibfk_3` FOREIGN KEY (`item_sell_operator`) REFERENCES `operator` (`operator_id`) ON DELETE SET NULL
       ) ENGINE=InnoDB DEFAULT CHARSET=utf8;
          
         
        */

        [ID(IDType.IDENTITY)]
        public UInt32 item_id { get; set; }

        public UInt32 item_receipt_id { get; set; }
        [Relno(0), Relkeys("item_receipt_id:receipt_id")]
        public Receipt item__Receipt { get; set; }

        public string item_name { get; set; }
        public UInt32 item_tagprice { get; set; }
        public bool item_tataki { get; set; }
        public bool item_return { get; set; }
        public string item_genre { get; set; }
        public string item_sellway { get; set; }
        public DateTime? item_receipt_time { get; set; }

        public UInt32? item_receipt_operator { get; set; }
        [Relno(1), Relkeys("item_receipt_operator:operator_id")]
        public Operator item_receipt__Operator { get; set; }

        public UInt32? item_sellprice { get; set; }
        public DateTime? item_selltime { get; set; }

        public UInt32? item_sell_operator { get; set; }
        [Relno(2), Relkeys("item_sell_operator:operator_id")]
        public Operator item_sell__Operator { get; set; }

        public DateTime? item_kansa_end { get; set; }
        public UInt32? item_kansa_flag1 { get; set; }
        public Int32? item_adjust { get; set; }
        public Decimal? item_isbn { get; set; }
        public UInt32? item_volumes { get; set; }
        public string item_comment { get; set; }
        public string item_sellcomment { get; set; }
        public string item_userspace { get; set; }


        public static string create_sqlite = @"
CREATE TABLE item (
	item_id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
	item_receipt_id INTEGER NOT NULL,
	item_name VARCHAR(255) NOT NULL,
	item_tagprice INTEGER NOT NULL default 0,
	item_tataki BOOL NOT NULL default FALSE,
	item_return BOOL NOT NULL default TRUE,
	item_genre VARCHAR(255) default NULL,
	item_sellway VARCHAR(255) default NULL,

	item_receipt_time DATETIME default NULL,
	item_receipt_operator INTEGER default NULL,

	item_sellprice INTEGER default NULL,
	item_selltime DATETIME default NULL,
	item_sell_operator INTEGER default NULL,

	item_kansa_end DATETIME default NULL,
	item_kansa_flag1 INTEGER default NULL,
	
	item_adjust INTEGER default NULL,

	item_isbn TEXT default NULL,
	item_volumes INTEGER default NULL,

	item_comment VARCHAR(255) default NULL,
	item_sellcomment TEXT default NULL,
	item_userspace TEXT default NULL,

	FOREIGN KEY ( item_receipt_id ) REFERENCES receipt(receipt_id) ON DELETE CASCADE,
	FOREIGN KEY ( item_receipt_operator ) REFERENCES operator(operator_id) ON DELETE SET NULL,
	FOREIGN KEY ( item_sell_operator ) REFERENCES operator(operator_id) ON DELETE SET NULL

);
";

        public static string GetCSVHeader()
        {
            return "id,票番,出品者,商品名,定価,売価,返す?,売却日時,コメント,監査終了日時";
        }

        public string GetCSVLine()
        {
            List<string> rets = new List<string>();

            rets.Add(this.item_id.ToString("00000"));
            rets.Add("R" + this.item_receipt_id.ToString("0000"));
            rets.Add(this.item__Receipt.getSellerString().ToCSVString());
            rets.Add(this.item_name.ToCSVString());
            rets.Add(this.item_tagprice.ToString());
            rets.Add((this.item_sellprice ?? 0).ToString());
            rets.Add(this.item_return ? "返" : "不返");
            rets.Add(this.item_sellprice.HasValue ? Globals.getTimeString(this.item_selltime) : "未売");
            rets.Add(this.item_comment.ToCSVString());
            rets.Add(this.item_kansa_end.HasValue ? Globals.getTimeString(this.item_kansa_end) : "未終了");

            return string.Join(",",rets.ToArray());
        }

        public uint GetTagPrintCount()
        {
            if (this.item_volumes.HasValue && this.item_volumes.Value != 0)
            {
                return this.item_volumes.Value;
            }
            else
            {
                return 1;
            }
        }

    }
}
