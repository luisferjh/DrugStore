using DrugStore.Data.Mapping.People;
using DrugStore.Data.Mapping.Sales;
using DrugStore.Data.Mapping.Store;
using DrugStore.Entities.Store;
using Microsoft.EntityFrameworkCore;
using System;

namespace DrugStore.Data
{
    public class DbContextDrugStore:DbContext
    {
        public DbSet<Category> Categories { get; set; }

        public DbContextDrugStore(DbContextOptions<DbContextDrugStore> options)
            :base(options)
        {
               
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.ApplyConfiguration(new OrderIncomeMap());
            modelBuilder.ApplyConfiguration(new OrderIncomeDetailsMap());
            modelBuilder.ApplyConfiguration(new DeliveryMap());
            modelBuilder.ApplyConfiguration(new SaleMap());
            modelBuilder.ApplyConfiguration(new SaleDetailMap());
            modelBuilder.ApplyConfiguration(new LossesMap());
            modelBuilder.ApplyConfiguration(new LossDetailMap());
            modelBuilder.ApplyConfiguration(new LaboratoryMap());
            modelBuilder.ApplyConfiguration(new ClientMap());
            modelBuilder.ApplyConfiguration(new RoleMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new ProviderMap());
        }
    }
}
