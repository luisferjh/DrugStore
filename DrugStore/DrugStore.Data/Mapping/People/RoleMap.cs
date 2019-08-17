using DrugStore.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugStore.Data.Mapping.People
{
    public class RoleMap : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            //Validations
            builder.Property(x => x.RoleName).HasColumnType("varchar(30)").IsRequired();
            builder.Property(x => x.Description).HasColumnType("varchar(100)").IsRequired();
            builder.Property(x => x.Condition).IsRequired();

            //Relations
            builder.ToTable("Role")
               .HasKey(p => p.IdRole);
        }
    }
}
