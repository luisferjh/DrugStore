using DrugStore.Data.Mapping.People;
using DrugStore.Data.Mapping.Sales;
using DrugStore.Data.Mapping.Store;
using DrugStore.Entities.Orders;
using DrugStore.Entities.Sales;
using DrugStore.Entities.Store;
using DrugStore.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System;

namespace DrugStore.Data
{
    public class DbContextDrugStore:DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Laboratory> Laboratories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Losses> Losses { get; set; }
        public DbSet<LossDetail> LossDetails { get; set; }
        public DbSet<OrderIncome> OrderIncomes { get; set; }
        public DbSet<OrderIncomeDetails> OrderIncomeDetails { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleDetail> saleDetails { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

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
