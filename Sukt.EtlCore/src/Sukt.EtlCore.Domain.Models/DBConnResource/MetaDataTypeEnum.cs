using System.ComponentModel;

namespace Sukt.EtlCore.Domain.Models.DBConnResource
{
    [Description("元数据类型")]
    public enum MetaDataTypeEnum
    {
        [Description("表")]
        MetaDataTable = 0,
        [Description("视图")]
        MetaDataView = 5,
        [Description("列")]
        MetaDataColunm = 10,
    }
}
