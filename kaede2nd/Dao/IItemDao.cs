﻿using System;
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
    }
}