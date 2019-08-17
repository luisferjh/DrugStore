using DrugStore.Entities.Store;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugStore.Data.Mapping.Store
{
    public class LossesMap : IEntityTypeConfiguration<Losses>
    {
        public void Configure(EntityTypeBuilder<Losses> builder)
        {
            //Validations         
            builder.Property(x => x.Cause).HasColumnType("varchar(50)").IsRequired();
            builder.Property(x => x.State).HasColumnType("varchar(20)").IsRequired();

            // Relations
            builder.ToTable("Losses")
              .HasKey(p => p.IdLosses);
           
        }
    }
}
