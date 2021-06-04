﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sukt.EtlCore.Domain.Models.SystemFoundation.DataDictionary;
using Sukt.EntityFrameworkCore.MappingConfiguration;
using System;

namespace Sukt.EtlCore.Domain.Models.EntityConfigurations.SystemFoundation
{
    public class DataDictionaryConfiguration : EntityMappingConfiguration<DataDictionaryEntity, Guid>
    {
        public override void Map(EntityTypeBuilder<DataDictionaryEntity> b)
        {
            b.HasKey(o => o.Id);
            b.Property(o => o.Title).HasMaxLength(50).IsRequired();
            b.Property(o => o.Code).HasMaxLength(50).IsRequired();
            b.Property(o => o.Value).HasMaxLength(50);
            b.Property(o => o.Sort).HasDefaultValue(0);
            b.ToTable("DataDictionary");
        }
    }
}