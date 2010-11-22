using System;
using System.Data;

using Seasar.Extension.ADO.Types;

namespace kaede2nd
{
    public static class SeasarValueType
    {
        public readonly static Seasar.Extension.ADO.IValueType UINT32 = new Seasar.Extension.ADO.Types.UInt32Type();
        public readonly static Seasar.Extension.ADO.IValueType NULLABLE_UINT32 = new Seasar.Extension.ADO.Types.NullableUInt32Type();

        public static void AddValueType()
        {
            Seasar.Extension.ADO.Types.ValueTypes.RegisterValueType(typeof(UInt32), UINT32);
            Seasar.Extension.ADO.Types.ValueTypes.RegisterValueType(typeof(Nullable<UInt32>), NULLABLE_UINT32);
        }
    }
}

namespace Seasar.Extension.ADO.Types
{
    public class UInt32Type : PrimitiveBaseType, IValueType
    {
        #region IValueType メンバ

        public override void BindValue(IDbCommand cmd, string columnName, object value)
        {
            BindValue(cmd, columnName, value, DbType.UInt32);
        }

        #endregion

        protected override object GetValue(object value)
        {
            if (value == DBNull.Value)
            {
                return null;
            }
            else
            {
                return Convert.ToUInt32(value);
            }
        }
    }

    public class NullableUInt32Type : NullableBaseType, IValueType
    {
        #region IValueType メンバ

        public override void BindValue(IDbCommand cmd, string columnName, object value)
        {
            BindValue(cmd, columnName, value, DbType.UInt32);
        }

        #endregion

        protected override object GetValue(object value)
        {
            if (value == DBNull.Value)
            {
                return null;
            }
            else if (value is int)
            {
                return new Nullable<UInt32>((UInt32)value);
            }
            else
            {
                return Convert.ToUInt32(value);
            }
        }
    }
}
