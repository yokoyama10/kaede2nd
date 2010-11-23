using System;
using System.Collections.Generic;

using Seasar.Dao.Attrs;
using kaede2nd.Entity;

namespace kaede2nd.Dao
{
    [Bean(typeof(Receipt))]
    public interface IReceiptDao
    {
        //INSERT
        int Insert(Receipt receipt);
        //UPDATE
        int Update(Receipt receipt);
        //DELETE
        int Delete(Receipt receipt);

        /**取得**/

        List<Receipt> GetAll();

        [Query("receipt_id = /*receipt_id*/")]
        List<Receipt> GetById(UInt32 receipt_id);

        [Query("receipt_seller = /*receipt_seller*/")]
        List<Receipt> GetBySellerString(string receipt_seller);


    }
}
