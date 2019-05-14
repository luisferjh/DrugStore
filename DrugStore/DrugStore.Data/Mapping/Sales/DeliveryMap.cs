using DrugStore.Entities.Sales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugStore.Data.Mapping.Sales
{
    public class DeliveryMap : IEntityTypeConfiguration<Delivery>
    {
        public void Configure(EntityTypeBuilder<Delivery> builder)
        {
            //Validations
            builder.Property(x => x.Adress).HasMaxLength(30).IsRequired();
            builder.Property(x => x.Date).IsRequired();
            builder.Property(x => x.State).HasMaxLength(20).IsRequired();

            // Relations
            builder.ToTable("Delivery")
               .HasKey(p => p.IdSale);

            // relation with Sale entity 1 to 1
            builder.HasOne(p => p.Sale)
                .WithOne(i => i.Delivery)
                .HasForeignKey<Delivery>(d => d.IdSale);               

            //Override cascade on Cliente Entity By EF Core
            builder.HasOne(p => p.Client)
               .WithMany(i => i.Deliveries)               
               .OnDelete(DeleteBehavior.Restrict);

            // only relation Delivery with Sale it will be on cascade
        }
    }
}
