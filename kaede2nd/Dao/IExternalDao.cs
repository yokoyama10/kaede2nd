using System;
using System.Collections.Generic;

using Seasar.Dao.Attrs;
using kaede2nd.Entity;

namespace kaede2nd.Dao
{
    [Bean(typeof(External))]
    public interface IExternalDao
    {
        //INSERT
        int Insert(External receipt);
        //UPDATE
        int Update(External receipt);
        //DELETE
        int Delete(External receipt);

        /**取得**/

        List<External> GetAll();

        [Query("external_id = /*external_id*/")]
        List<External> GetById(UInt32 external_id);
    }
}
