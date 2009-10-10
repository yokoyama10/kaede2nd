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
       `item_adjust` int(11),
       `item_isbn` bigint(20) unsigned,
       `item_volumes` int(10) unsigned DEFAULT NULL,
       `item_comment` varchar(255),
       `item_sellcomment` text,
       `item_userspace` text,
       PRIMARY KEY (`item_id`),
       KEY `item_receipt_id` (`item_receipt_id`),
       KEY `item_receipt_operator` (`item_receipt_operator`)
        ) ENGINE=InnoDB DEFAULT CHARSET utf8;
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
        [Relno(1), Relkeys("item_receipt_operator:operator_id")]
        public Operator item_receipt__Operator { get; set; }
        public UInt32? item_sellprice { get; set; }
        public DateTime? item_selltime { get; set; }
        public Int32? item_adjust { get; set; }
        public Decimal? item_isbn { get; set; }
        public UInt32? item_volumes { get; set; }
        public string item_comment { get; set; }
        public string item_sellcomment { get; set; }
        public string item_userspace { get; set; }

        public static string GetCSVHeader()
        {
            return "id,出品者,商品名,定価,売価,返す?,売れた?,コメント";
        }

        public string GetCSVLine()
        {
            List<string> rets = new List<string>();

            rets.Add(this.item_id.ToString("00000"));
            rets.Add(this.item__Receipt.getSellerString().ToCSVString());
            rets.Add(this.item_name.ToCSVString());
            rets.Add(this.item_tagprice.ToString());
            rets.Add((this.item_sellprice ?? 0).ToString());
            rets.Add(this.item_return ? "返" : "不返");
            rets.Add(this.item_sellprice.HasValue ? "既売" : "未売");
            rets.Add(this.item_comment.ToCSVString());

            return string.Join(",",rets.ToArray());
        }

    }
}
