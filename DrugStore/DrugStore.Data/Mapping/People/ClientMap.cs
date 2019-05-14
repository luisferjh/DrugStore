using DrugStore.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugStore.Data.Mapping.People
{
    public class ClientMap : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            // Validations 
            builder.Property(x => x.Name).HasMaxLength(30).IsRequired(); 
            builder.Property(x => x.LastName).HasMaxLength(30).IsRequired();
            builder.Property(x => x.DocumentType).HasMaxLength(20);
            builder.Property(x => x.DocumentNumber).HasMaxLength(20);
            builder.Property(x => x.PhoneNumber).HasMaxLength(20);

            // Relations
            builder.ToTable("Client")
               .HasKey(p => p.IdClient);
        }
    }
}
