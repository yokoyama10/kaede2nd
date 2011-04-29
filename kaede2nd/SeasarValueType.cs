using System;
using System.Data;

using Seasar.Extension.ADO.Types;
using Seasar.Extension.ADO;

namespace kaede2nd
{
    public static class SeasarValueType
    {
        public readonly static IValueType UINT32 = new kaede2nd.SeasarTypes.UInt32Type();
        public readonly static IValueType NULLABLE_UINT32 = new kaede2nd.SeasarTypes.NullableUInt32Type();

        public static void AddValueType()
        {
            IValueType objType = ValueTypes.GetValueType(null);

            if (ValueTypes.GetValueType(typeof(UInt32)) == objType)
            {
                ValueTypes.RegisterValueType(typeof(UInt32), UINT32);
            }
            if (ValueTypes.GetValueType(typeof(Nullable<UInt32>)) == objType)
            {
                ValueTypes.RegisterValueType(typeof(Nullable<UInt32>), NULLABLE_UINT32);
            }
        }
    }
}

namespace kaede2nd.SeasarTypes
{
    public class UInt32Type : PrimitiveBaseType, IValueType
    {
        #region IValueType メンバ

        public override void BindValue(IDbCommand cmd, string columnName, object value)
        {
            BindValue(cmd, columnName, value, DbType.Int64);
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
            BindValue(cmd, columnName, value, DbType.Int64);
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
