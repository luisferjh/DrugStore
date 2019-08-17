using DrugStore.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugStore.Data.Mapping.People
{
    public class ProviderMap : IEntityTypeConfiguration<Provider>
    {
        public void Configure(EntityTypeBuilder<Provider> builder)
        {
            // Validations
            builder.Property(x => x.ProviderName).HasColumnType("varchar(100)").IsRequired();
            builder.Property(x => x.DocumentNumber).HasColumnType("varchar(20)");
            builder.Property(x => x.Address).HasColumnType("varchar(70)");
            builder.Property(x => x.PhoneNumber).HasColumnType("varchar(20)");
            builder.Property(x => x.Email).HasColumnType("varchar(30)");

            //Relations
            builder.ToTable("Provider")
              .HasKey(p => p.IdProvider);
        }
    }
}
