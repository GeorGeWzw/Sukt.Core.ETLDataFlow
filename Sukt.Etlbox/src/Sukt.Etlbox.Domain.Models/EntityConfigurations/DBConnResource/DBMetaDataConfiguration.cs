using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sukt.Etlbox.Domain.Models.DBConnResource;
using SuktCore.Shared;
using System;

namespace Sukt.Etlbox.Domain.Models.EntityConfigurations.DBConnResource
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
