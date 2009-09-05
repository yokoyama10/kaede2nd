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

        /**取得**/

        List<Operator> GetAll();
    }
}
