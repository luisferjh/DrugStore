using DrugStore.Data.Mapping.People;
using DrugStore.Data.Mapping.Sales;
using DrugStore.Data.Mapping.Store;
using DrugStore.Entities.Log;
using DrugStore.Entities.Orders;
using DrugStore.Entities.Sales;
using DrugStore.Entities.Store;
using DrugStore.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

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
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (entityType.ClrType.GetCustomAttributes(typeof(AuditableAttribute), true).Length > 0)
                {
                    modelBuilder.Entity(entityType.Name).Property<DateTime>("DateOn");
                }
            }

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

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            var timestamp = DateTime.Now;

            foreach (var entry in ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added))
            {
                if (entry.Entity.GetType().GetCustomAttributes(typeof(AuditableAttribute), true).Length > 0)
                {
                    if (entry.State == EntityState.Added)
                    {
                        entry.Property("DateOn").CurrentValue = timestamp;
                    }
                }
            }

            return base.SaveChanges();  
        }
    }
}
