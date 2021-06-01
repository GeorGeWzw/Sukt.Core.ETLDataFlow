using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sukt.Etlbox.Domain.Models.DBConnResource;
using SuktCore.Shared;
using System;

namespace Sukt.Etlbox.Domain.Models.EntityConfigurations.DBConnResource
{
    public class DBConnectionConfiguration : AggregateRootMappingConfiguration<DBConnection, Guid>
    {
        public override void Map(EntityTypeBuilder<DBConnection> b)
        {
            b.HasKey(o => o.Id);
            b.ToTable("ETL_DBConnection").HasComment("数据库连接管理");
        }
    }
}
