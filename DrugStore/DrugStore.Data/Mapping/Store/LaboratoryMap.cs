using DrugStore.Entities.Store;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugStore.Data.Mapping.Store
{
    public class LaboratoryMap : IEntityTypeConfiguration<Laboratory>
    {
        public void Configure(EntityTypeBuilder<Laboratory> builder)
        {
            //Validations
            builder.Property(x => x.LaboratoryName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(100).IsRequired();

            //Relations
            builder.ToTable("Laboratory")
                .HasKey(p => p.IdLaboratory);
        }
    }
}
