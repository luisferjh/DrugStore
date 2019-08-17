using DrugStore.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugStore.Data.Mapping.Sales
{
    public class OrderIncomeDetailsMap : IEntityTypeConfiguration<OrderIncomeDetails>
    {
        public void Configure(EntityTypeBuilder<OrderIncomeDetails> builder)
        {
            // validations
            builder.Property(x => x.Amount).IsRequired();
            builder.Property(x => x.UnitCost).HasColumnType("decimal(11,2)").IsRequired();

            //Relations
            builder.ToTable("OrderIncomeDetails")
                  .HasKey(p => p.IdOrderIncomeDetails);

            //Cascade Overrired on Product Entity
            builder.HasOne(x => x.Product)
              .WithMany(x => x.OrderIncomeDetails)
              .OnDelete(DeleteBehavior.Restrict);

            // Casacade on OrderIncome Entity by EF Core
        }
    }
}
