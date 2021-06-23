using System;
using System.Text.Json;
using Sukt.EtlCore.WorkNode.Block.BlockOption.Enum;

namespace Sukt.EtlCore.WorkNode.Block
{
    public static class JsonElementExtension
    {
        public static ValueType GetValue(this JsonElement e, FieldTypeEnum type)
        {
            return type switch
            {
                FieldTypeEnum.Double => e.GetDouble(),
                FieldTypeEnum.Integer => e.GetInt32(),
                FieldTypeEnum.Boolean => e.GetBoolean(),
                FieldTypeEnum.Guid => e.GetGuid(),
                FieldTypeEnum.DateTime => e.GetDateTime(),
                _ => throw  new InvalidCastException($"can not convert to {type}")
            };
        }
        
        public static object GetReferenceValue(this JsonElement e, FieldTypeEnum type)
        {
            return type switch
            {
                FieldTypeEnum.String => e.GetString(),
                _ => throw  new InvalidCastException($"can not convert to {type}")
            };
        }
    }
}
