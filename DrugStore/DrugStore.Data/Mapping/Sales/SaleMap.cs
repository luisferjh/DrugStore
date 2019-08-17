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
            builder.Property(x => x.TypeSale).HasColumnType("varchar(20)").IsRequired();
            builder.Property(x => x.VoucherSeries).HasColumnType("varchar(7)").IsRequired();
            builder.Property(x => x.VoucherNumber).HasColumnType("varchar(10)").IsRequired();       
            builder.Property(x => x.TotalPrice).HasColumnType("decimal(11,2)").IsRequired();
            builder.Property(x => x.State).HasColumnType("varchar(20)").IsRequired();

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
