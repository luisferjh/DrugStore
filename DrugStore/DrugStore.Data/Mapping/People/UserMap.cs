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
            builder.Property(x => x.UserName).HasColumnType("varchar(100)").IsRequired();
            builder.Property(x => x.DocumentType).HasColumnType("varchar(20)").IsRequired();
            builder.Property(x => x.DocumentNumber).HasColumnType("varchar(20)").IsRequired();
            builder.Property(x => x.Address).HasColumnType("varchar(70)");
            builder.Property(x => x.PhoneNumber).HasColumnType("varchar(20)").IsRequired();
            builder.Property(x => x.Email).HasColumnType("varchar(70)").IsRequired();
            builder.Property(x => x.PasswordHash).HasMaxLength(250).IsRequired();
            builder.Property(x => x.PasswordSalt).HasMaxLength(250).IsRequired();
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
