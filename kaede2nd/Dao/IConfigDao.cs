using System;
using System.Collections.Generic;

using Seasar.Dao.Attrs;
using kaede2nd.Entity;

namespace kaede2nd.Dao
{
    [Bean(typeof(ConfigEntity))]
    public interface IConfigDao
    {
        [Query("config_id = 0")]
        List<ConfigEntity> Get();

        //INSERT
        int Insert(ConfigEntity item);
        //UPDATE
        int Update(ConfigEntity cfg);
    }
}
