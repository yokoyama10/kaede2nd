using System;
using System.Collections.Generic;

using Seasar.Dao.Attrs;
using kaede2nd.Entity;

namespace kaede2nd.Dao
{
    [Bean(typeof(Operator))]
    public interface IOperatorDao
    {
        //INSERT
        int Insert(Operator receipt);
        //UPDATE
        int Update(Operator receipt);
        //DELETE
        int Delete(Operator receipt);

        [Sql("DELETE FROM operator")]
        void DeleteAll();

        /**取得**/

        List<Operator> GetAll();

        [Query("operator_id = /*operator_id*/")]
        List<Operator> GetById(UInt32 operator_id);

    }

}
