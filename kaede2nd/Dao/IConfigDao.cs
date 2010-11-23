using System;
using System.Collections.Generic;

using Seasar.Dao.Attrs;
using kaede2nd.Entity;

namespace kaede2nd.Dao
{
    [Bean(typeof(ConfigEntity))]
    public interface IConfigDao
    {
        List<ConfigEntity> GetAll();

        //INSERT
        int Insert(ConfigEntity cfg);
        //UPDATE
        int Update(ConfigEntity cfg);

    }
}
