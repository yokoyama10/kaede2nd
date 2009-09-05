﻿using System;
using System.Collections.Generic;
using System.Text;

using Seasar.Dao.Attrs;

namespace kaede2nd.Entity
{

    /*
    CREATE TABLE `operator` (
       `operator_id` int(10) unsigned,
       `operator_name` varchar(255),
       `operator_comment` text,
       PRIMARY KEY (`operator_id`),
       UNIQUE KEY (`operator_name`)
    ) ENGINE=InnoDB DEFAULT CHARSET utf8;
    */

    [Table("operator")]
    public class Operator
    {
        [ID(IDType.IDENTITY)]
        public UInt32 operator_id { get; set; }
        public string operator_name { get; set; }
        public string operator_comment { get; set; }
    }
}
