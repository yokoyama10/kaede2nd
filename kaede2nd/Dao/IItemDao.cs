using System;
using System.Collections.Generic;

using Seasar.Dao.Attrs;
using kaede2nd.Entity;

namespace kaede2nd.Dao
{
    [Bean(typeof(Item))]
    public interface IItemDao
    {
        //INSERT
        int Insert(Item item);
        //UPDATE
        int Update(Item item);
        //DELETE
        int Delete(Item item);

        /**取得**/

        List<Item> GetAll();

        [Query("item_id = /*item_id*/")]
        List<Item> GetItemById(UInt32 item_id);

        [Query("item_receipt_id = /*receipt_id*/")]
        List<Item> GetReceiptItem(UInt32 receipt_id);

        [Sql("SELECT COUNT(*) FROM item WHERE item_receipt_id = /*receipt_id*/")]
        UInt32 CountReceiptItem(UInt32 receipt_id);

        [Sql("ALTER TABLE item AUTO_INCREMENT=1")]
        void ResetItemIdNumber();


        /**監査**/
        [Query("item_kansa_end IS NULL   AND   item_sellprice IS NOT NULL")]
        List<Item> GetNeedKansaItem();
        [Sql("SELECT COUNT(*) FROM item WHERE " +
               "item_kansa_end IS NULL   AND   item_sellprice IS NOT NULL")]
        UInt32 CountNeedKansaItem();

        [Sql("SELECT SUM(item_sellprice) FROM item WHERE " +
               "item_kansa_end IS NULL   AND   item_sellprice IS NOT NULL")]
        UInt32 SumNeedKansaItem_SellPrice();

        [Query("item_kansa_end IS NULL   AND   item_sellprice IS NOT NULL   AND   item_kansa_flag1 IS NULL")]
        List<Item> GetNeedKansaItem_NotFlagged1();
        [Sql("SELECT COUNT(*) FROM item WHERE " +
               "item_kansa_end IS NULL   AND   item_sellprice IS NOT NULL   AND   item_kansa_flag1 IS NULL")]
        UInt32 CountNeedKansaItem_NotFlagged1();

        [Query("item_kansa_end IS NULL   AND   item_sellprice IS NOT NULL   AND   item_kansa_flag2 IS NULL")]
        List<Item> GetNeedKansaItem_NotFlagged2();
        [Sql("SELECT COUNT(*) FROM item WHERE " +
               "item_kansa_end IS NULL   AND   item_sellprice IS NOT NULL   AND   item_kansa_flag2 IS NULL")]
        UInt32 CountNeedKansaItem_NotFlagged2();

        [Query("item_kansa_end IS NULL   AND   item_sellprice IS NOT NULL   AND   item_kansa_flag3 IS NULL")]
        List<Item> GetNeedKansaItem_NotFlagged3();
        [Sql("SELECT COUNT(*) FROM item WHERE " +
               "item_kansa_end IS NULL   AND   item_sellprice IS NOT NULL   AND   item_kansa_flag3 IS NULL")]
        UInt32 CountNeedKansaItem_NotFlagged3();
    }
}
