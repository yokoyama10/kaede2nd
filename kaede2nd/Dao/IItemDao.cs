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

        [Sql("DELETE FROM item")]
        void DeleteAll();

        /**取得**/

        List<Item> GetAll();

        [Query("item_id = /*item_id*/")]
        List<Item> GetItemById(UInt32 item_id);

        [Query("item_receipt_id = /*receipt_id*/")]
        List<Item> GetReceiptItem(UInt32 receipt_id);

        [Sql("SELECT COUNT(*) FROM item")]
        UInt32 CountAll();

        [Sql("SELECT COUNT(*) FROM item WHERE item_sellprice IS NOT NULL")]
        UInt32 CountSoldItem();

        [Sql("SELECT SUM(item_sellprice) FROM item")]
        UInt32 SumSellPrice();

        [Sql("SELECT COUNT(*) FROM item WHERE item_receipt_id = /*receipt_id*/")]
        UInt32 CountReceiptItem(UInt32 receipt_id);

        [Sql("ALTER TABLE item AUTO_INCREMENT=1")]
        void ResetItemIdNumber_MySQL();

        /** http://msdn.microsoft.com/ja-jp/library/ms176057.aspx **/
        [Sql("DBCC CHECKIDENT ('item', RESEED, 0)")]
        void ResetItemIdNumber_MSSQL_1();
        [Sql("DBCC CHECKIDENT ('item', RESEED)")]
        void ResetItemIdNumber_MSSQL_2();

        [Sql("UPDATE sqlite_sequence SET seq=1 WHERE name='item'")]
        void ResetItemIdNumber_SQLite();


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
        List<Item> GetNeedKansaItem_NotFlagged();
        [Sql("SELECT COUNT(*) FROM item WHERE " +
               "item_kansa_end IS NULL   AND   item_sellprice IS NOT NULL   AND   item_kansa_flag1 IS NULL")]
        UInt32 CountNeedKansaItem_NotFlagged();

    }
}
