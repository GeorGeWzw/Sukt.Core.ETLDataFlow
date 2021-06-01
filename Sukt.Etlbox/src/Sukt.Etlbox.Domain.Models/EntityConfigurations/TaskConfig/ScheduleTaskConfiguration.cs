using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sukt.Etlbox.Domain.Models.TaskConfig;
using SuktCore.Shared;
using System;

namespace Sukt.Etlbox.Domain.Models.EntityConfigurations.TaskConfig
{
    public class ScheduleTaskConfiguration : EntityMappingConfiguration<ScheduleTask, Guid>
    {
        public override void Map(EntityTypeBuilder<ScheduleTask> b)
        {
            b.HasKey(o => o.Id);
            b.ToTable("ETL_ScheduleTask").HasComment("任务管理");
        }
    }
}
