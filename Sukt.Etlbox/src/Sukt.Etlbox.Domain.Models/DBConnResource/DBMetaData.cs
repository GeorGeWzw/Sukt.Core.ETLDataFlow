using SuktCore.Shared.Entity;
using System;
using System.ComponentModel;

namespace Sukt.Etlbox.Domain.Models.DBConnResource
{
    public class DBMetaData : EntityBase<Guid>, IFullAuditedEntity<Guid>
    {
        public DBMetaData(MetaDataTypeEnum metaDataType, string name, Guid parentId, string describe)
        {
            MetaDataType = metaDataType;
            Name = name;
            Describe = describe;
            this.ParentId = parentId;
        }
        #region Func
        public void Delete()
        {
            this.IsDeleted = true;
        }
        #endregion
        /// <summary>
        /// 元数据类型
        /// </summary>
        [DisplayName("元数据类型")]
        public MetaDataTypeEnum MetaDataType { get; private set; }
        /// <summary>
        /// 元数据名称
        /// </summary>
        [DisplayName("元数据名称")]
        public string Name { get; private set; }
        public DBConnection Connection { get; private set; }
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        public string Describe { get; private set; }
        /// <summary>
        /// 父级ID
        /// </summary>
        [DisplayName("父级ID")]
        public Guid ParentId { get; private set; }
        #region 公共字段
        /// <summary>
        /// 是否删除
        /// </summary>
        [DisplayName("是否删除")]
        public bool IsDeleted { get; set; }
        [DisplayName("创建人")]
        public Guid CreatedId { get; set; }
        [DisplayName("创建时间")]
        public DateTime CreatedAt { get; set; }
        [DisplayName("最后修改人")]
        public Guid? LastModifyId { get; set; }
        [DisplayName("最后修改时间")]
        public DateTime LastModifedAt { get; set; }
        #endregion
    }
}
