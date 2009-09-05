using System;
using System.Collections.Generic;
using System.Text;

using Seasar.Dao.Attrs;

namespace kaede2nd.Entity
{
    /*
   CREATE TABLE `receipt` (
   `receipt_id` int(10) unsigned,
   `receipt_pass` char(8),
   `receipt_seller` char(4),
   `receipt_seller_branch` int(10) unsigned,
   `receipt_time` datetime,
   `receipt_operator` int(10) unsigned,
   `receipt_payback` tinyint(1),
   `receipt_comment` varchar(255),
   PRIMARY KEY (`receipt_id`),
   KEY `receipt_operator` (`receipt_operator`)
   ) ENGINE=InnoDB DEFAULT CHARSET utf8;
    
     */

    [Table("receipt")]
    public class Receipt
    {
        public const string seller_EXT = "9090";
        public const string seller_LAGACY = "9001";
        public const string seller_DONATE = "9002";

        [ID(IDType.IDENTITY)]
        public UInt32 receipt_id { get; set; }

        public string receipt_pass { get; set; }
        public string receipt_seller { get; set; } //4-letter chars
        public string receipt_seller_exname { get; set; }
        //public UInt32? receipt_seller_branch { get; set; }
        public DateTime? receipt_time { get; set; }

        public UInt32? receipt_operator { get; set; }

        [Relno(0), Relkeys("receipt_operator:operator_id")]
        public Operator receipt__Operator { get; set; }

        public bool? receipt_payback { get; set; }
        public string receipt_comment { get; set; }




        public string getSellerString()
        {
            string ret = this.receipt_seller;

            switch (this.receipt_seller)
            {
                case seller_EXT:
                    {
                        //TODO
                        /*if (this.External != null)
                        {
                            ret = this.External.getSellerString();
                        }
                        else
                        {
                            ret = "不明な外部者";
                        }*/
                        return this.receipt_seller_exname;
                    }
                case seller_LAGACY:
                    {
                        return "遺産";
                    }
                case seller_DONATE:
                    {
                        return "寄付";
                    }
                default:
                    if (this.receipt_seller.Substring(0, 1) == "9")
                    {
                        return "ERR: 不明";
                    }

                    return this.receipt_seller.Substring(0, 1) + "年" + this.receipt_seller.Substring(1, 1) + "組 " +
                        this.receipt_seller.Substring(2, 2) + "番 " + this.receipt_seller_exname;
            }

        }

        public string getSellerSortKey()
        {
            string sortKey;

            switch (this.receipt_seller)
            {
                case kaede2nd.Entity.Receipt.seller_EXT:
                    {
                        sortKey = "C" + this.receipt_seller_exname;
                        break;
                    }
                case kaede2nd.Entity.Receipt.seller_LAGACY:
                    {
                        sortKey = "E";
                        break;
                    }
                case kaede2nd.Entity.Receipt.seller_DONATE:
                    {
                        sortKey = "D";
                        break;
                    }
                default:
                    {
                        if (Globals.isChugaku(this.receipt_seller.Substring(1, 1)))
                        {
                            sortKey = "A" + this.receipt_seller;
                        }
                        else
                        {
                            sortKey = "B" + this.receipt_seller;
                        }
                        break;
                    }
            }

            return sortKey;

        }

        public string getPaybackString()
        {
            if (!this.receipt_payback.HasValue) { return "？"; }

            if (this.receipt_payback.Value == true)
            {
                return "済";
            }
            else
            {
                return "未";
            }

        }

    }
}
