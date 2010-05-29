using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using kaede2nd.Entity;
using kaede2nd.Dao;

namespace kaede2nd
{
    public class DbConfig
    {
        private static List<ConfigEntity> defaults
            = new List<ConfigEntity>() { 
                new ConfigEntity( "bumonname", "不明な部門"),
                new ConfigEntity( "companyname", "不明な縁日班不明部門"),
                new ConfigEntity( "barcodeprefix", "00"),
                new ConfigEntity( "symbolcolor_argb", "-1"),
                new ConfigEntity( "itemname_imeon", "1"),
                new ConfigEntity( "entertotab", "0"),
                new ConfigEntity( "bumontextcolor_argb", "-16777216"),
            };

        private List<ConfigEntity> data;

        public static DbConfig MakeFromDB()
        {
            DbConfig c = new DbConfig();
            var cdao = GlobalData.getIDao<IConfigDao>();
            c.data = cdao.GetAll();

            return c;
        }

        public void setValue(string name, string value)
        {
            var cdao = GlobalData.getIDao<IConfigDao>();

            var l1 = from d in cdao.GetAll()
                     where d.config_name == name
                     select d;

            if (l1.Count() > 0)
            {
                var ccl = l1.First();
                ccl.config_value = value;

                cdao.Update(ccl);
            }
            else
            {
                var ccn = new ConfigEntity(name, value);
                cdao.Insert(ccn);
            }
        }

        public void setValueBool(string name, bool value)
        {
            this.setValue(name, value ? "1" : "0");
        }

        public void setValueInt(string name, Int32 value)
        {
            this.setValue(name, value.ToString());
        }

        public string getValue(string name)
        {
            var s1 = from d in this.data
                    where d.config_name == name
                    select d.config_value;
            if (s1.Count() > 0) { return s1.First(); }

            var s2 = from d in DbConfig.defaults
                    where d.config_name == name
                    select d.config_value;
            if (s2.Count() > 0) {
                return s2.First();
            }

            throw new ArgumentException();
        }

        public bool getValueBool(string name)
        {
            string s = this.getValue(name);
            if (string.IsNullOrEmpty(s)) { return false; }
            if (s == "0" || s.ToLower() == "false") { return false; }
            return true;
        }

        public Int32 getValueInt(string name)
        {
            string s = this.getValue(name);
            return Int32.Parse(s);
        }
    }
}
