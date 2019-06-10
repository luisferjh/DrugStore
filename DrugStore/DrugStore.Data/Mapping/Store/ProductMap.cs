using DrugStore.Entities.Store;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugStore.Data.Mapping.Store
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // Validations
            builder.Property(x => x.ProductName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.BarCode).HasMaxLength(50);
            builder.Property(x => x.Indicative).HasMaxLength(250);
            builder.Property(x => x.Stock).IsRequired();  
            builder.Property(x => x.Price).IsRequired();
         
            // Relations
            builder.ToTable("Product")
                .HasKey(p => p.IdProduct);

            //override the cascade imposed by EF Core in category
            builder.HasOne(x => x.Category)
                .WithMany(x => x.Products)
                .OnDelete(DeleteBehavior.Restrict);

            //override the cascade imposed by EF Core in laboratory
            builder.HasOne(x => x.Laboratory)
               .WithMany(x => x.Products)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
