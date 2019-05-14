using DrugStore.Entities.Sales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugStore.Data.Mapping.Sales
{
    public class SaleMap : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            // Validations
            builder.Property(x => x.TypeSale).HasMaxLength(20).IsRequired();
            builder.Property(x => x.VoucherSeries).HasMaxLength(7);
            builder.Property(x => x.VoucherNumber).HasMaxLength(10).IsRequired();
            builder.Property(x => x.SaleDate).IsRequired();
            builder.Property(x => x.TotalPrice).IsRequired();
            builder.Property(x => x.State).HasMaxLength(20).IsRequired();

            // Relations
            builder.ToTable("Sale")
                .HasKey(p => p.IdSale);

            //override cascade on User Entity
            builder.HasOne(x => x.User)
              .WithMany(x => x.Sales)
              .OnDelete(DeleteBehavior.Restrict);

            //override cascade on Client Entity
            builder.HasOne(x => x.Client)
              .WithMany(x => x.Sales)
              .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
