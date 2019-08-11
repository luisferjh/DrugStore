using DrugStore.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugStore.Data.Mapping.People
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //Validations
            builder.Property(x => x.IdRole).IsRequired();
            builder.Property(x => x.UserName).HasMaxLength(100).IsRequired();
            builder.Property(x => x.DocumentType).HasMaxLength(20);
            builder.Property(x => x.DocumentNumber).HasMaxLength(20);
            builder.Property(x => x.Address).HasMaxLength(70);
            builder.Property(x => x.PhoneNumber).HasMaxLength(20);
            builder.Property(x => x.Email).HasMaxLength(70).IsRequired();
            builder.Property(x => x.PasswordHash).HasColumnType("").HasMaxLength(64).IsRequired();
            builder.Property(x => x.PasswordSalt).HasMaxLength(64).IsRequired();
            builder.Property(x => x.Condition).IsRequired();

            //Relations
            builder.ToTable("User")
               .HasKey(p => p.IdUser);

            //Cascade Overrired on Role
            builder.HasOne(x => x.Role)
              .WithMany(x => x.Users)
              .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
