using DrugStore.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugStore.Data.Mapping.Sales
{
    public class OrderIncomeMap : IEntityTypeConfiguration<OrderIncome>
    {        
        public void Configure(EntityTypeBuilder<OrderIncome> builder)
        {
            // Validations
            builder.Property(x => x.DateEntry).IsRequired();
            builder.Property(x => x.Amount).IsRequired();
            builder.Property(x => x.Total).IsRequired();
            builder.Property(x => x.State).HasMaxLength(20);

            //Relations
            builder.ToTable("OrderIncome")
                  .HasKey(p => p.IdOrderIncome);

            //Cascade Overrired on Provider Entity
            builder.HasOne(x => x.provider)
              .WithMany(x => x.OrderIncomes)
              .OnDelete(DeleteBehavior.Restrict);

            //Cascade Overrired on OrderIncome Entity
            builder.HasOne(x => x.User)
              .WithMany(x => x.OrderIncomes)
              .OnDelete(DeleteBehavior.Restrict);

            
        }
    }
}
