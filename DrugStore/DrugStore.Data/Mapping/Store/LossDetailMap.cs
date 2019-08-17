using DrugStore.Entities.Store;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugStore.Data.Mapping.Store
{
    public class LossDetailMap : IEntityTypeConfiguration<LossDetail>
    {
        public void Configure(EntityTypeBuilder<LossDetail> builder)
        {
            // Validations
            builder.Property(x => x.UnitCost).HasColumnType("decimal(11,2)").IsRequired();
            builder.Property(x => x.Amount).IsRequired();

            // Relations
            builder.ToTable("LossDetail")
              .HasKey(p => p.IdDetailLosses);

            //Override cascade on Product by EF Core
            builder.HasOne(x => x.Product)
                .WithMany(x => x.LossDetails)
                .OnDelete(DeleteBehavior.Restrict);

            // other cascade made it by EF Core for the table Losses
            // is allow it
        }
    }
}
