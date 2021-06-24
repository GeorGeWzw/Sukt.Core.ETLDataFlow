using System;
using System.Text.Json;
using Sukt.EtlCore.WorkNode.Block.BlockOption.Enum;

namespace Sukt.EtlCore.WorkNode.Block
{
    public static class JsonElementExtension
    {
        /// <summary>
        /// 获取值类型的value
        /// </summary>
        /// <param name="e"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ValueType GetValue(this JsonElement e, FieldTypeEnum type)
        {
            return type switch
            {
                FieldTypeEnum.Double => e.GetDouble(),
                FieldTypeEnum.Integer => e.GetInt32(),
                FieldTypeEnum.Long => e.GetInt64(),
                FieldTypeEnum.Boolean => e.GetBoolean(),
                FieldTypeEnum.Guid => e.GetGuid(),
                FieldTypeEnum.DateTimeOffset => e.GetDateTimeOffset(),
                FieldTypeEnum.DateTime => e.GetDateTime(),
                _ => throw  new InvalidCastException($"can not convert {e.GetRawText()} to {type}")
            };
        }

        /// <summary>
        /// 获取引用类型的value
        /// </summary>
        /// <param name="e"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object GetReferenceValue(this JsonElement e, FieldTypeEnum type)
        {
            return type switch
            {
                FieldTypeEnum.String => e.GetString(),
                _ => throw  new InvalidCastException($"can not convert {e.GetRawText()} to {type}")
            };
        }
    }
}
