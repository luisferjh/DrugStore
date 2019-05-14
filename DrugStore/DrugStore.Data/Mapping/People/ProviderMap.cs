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
            builder.Property(x => x.ProviderName).HasMaxLength(100).IsRequired();
            builder.Property(x => x.DocumentNumber).HasMaxLength(20);
            builder.Property(x => x.Address).HasMaxLength(70);
            builder.Property(x => x.PhoneNumber).HasMaxLength(20);
            builder.Property(x => x.Email).HasMaxLength(30);

            //Relations
            builder.ToTable("Provider")
              .HasKey(p => p.IdProvider);
        }
    }
}
