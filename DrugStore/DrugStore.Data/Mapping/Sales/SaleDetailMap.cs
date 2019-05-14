using DrugStore.Entities.Sales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugStore.Data.Mapping.Sales
{
    public class SaleDetailMap : IEntityTypeConfiguration<SaleDetail>
    {
        public void Configure(EntityTypeBuilder<SaleDetail> builder)
        {
            //Validations
            builder.Property(x => x.Amount).IsRequired();
            builder.Property(x => x.Discount).IsRequired();
            builder.Property(x => x.UnitCost).IsRequired();
            builder.Property(x => x.UnitPrice).IsRequired(); 


            // Relations
            builder.ToTable("SaleDetail")
                  .HasKey(p => p.IdSaleDetail);

            //override cascade on Product Entity
            builder.HasOne(x => x.Products)
              .WithMany(x => x.SaleDetails)
              .OnDelete(DeleteBehavior.Restrict);

            // Allow cascade on Entity Sale by EF Core
        }
    }
}
