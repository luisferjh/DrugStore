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
            builder.Property(x => x.Total).HasColumnType("decimal(11,2)").IsRequired();
            builder.Property(x => x.State).HasColumnType("varchar(20)");

            //Relations
            builder.ToTable("OrderIncome")
                  .HasKey(p => p.IdOrderIncome);

            //Cascade Overrired on Provider Entity
            builder.HasOne(x => x.Provider)
              .WithMany(x => x.OrderIncomes)
              .OnDelete(DeleteBehavior.Restrict);

            //Cascade Overrired on OrderIncome Entity
            builder.HasOne(x => x.User)
              .WithMany(x => x.OrderIncomes)
              .OnDelete(DeleteBehavior.Restrict);

            
        }
    }
}
