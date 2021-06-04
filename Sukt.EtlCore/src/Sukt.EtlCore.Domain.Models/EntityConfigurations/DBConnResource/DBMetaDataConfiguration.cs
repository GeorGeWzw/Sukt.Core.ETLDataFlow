using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sukt.EntityFrameworkCore.MappingConfiguration;
using Sukt.EtlCore.Domain.Models.DBConnResource;
using System;

namespace Sukt.EtlCore.Domain.Models.EntityConfigurations.DBConnResource
{
    public class DBMetaDataConfiguration : EntityMappingConfiguration<DBMetaData, Guid>
    {
        public override void Map(EntityTypeBuilder<DBMetaData> b)
        {
            b.HasKey(o => o.Id);
            b.ToTable("ETL_DBMetaData").HasComment("元数据管理");
        }
    }
}
