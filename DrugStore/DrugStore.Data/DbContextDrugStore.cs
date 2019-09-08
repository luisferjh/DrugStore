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
                    modelBuilder.Entity(entityType.Name).Property<DateTime>("LastUpdated");
                }
            }

            modelBuilder.Entity<User>().Property<DateTime>("DateOn");

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

            //initialize data
            modelBuilder.Entity<Category>().HasData(
                new Category() {IdCategory = 1, Name = "Generico", Description = "productos de caracter generico", Condition = true },
                new Category() {IdCategory = 2, Name = "Comercial", Description = "productos de caracter comercial", Condition = true });
            
            //Laboratory
            modelBuilder.Entity<Laboratory>().HasData(
                new Laboratory
                {
                    IdLaboratory = 1,
                    LaboratoryName = "Bayer",
                    Description = "Bayer es una compañía de innovación que investiga" +
                                      " en las áreas de Ciencias de la Vida centradas en la " +
                                      "salud y la agricultura.",
                    Condition = true
                },
                new Laboratory
                {
                    IdLaboratory = 2,
                    LaboratoryName = "Tecnoquimicas",
                    Description = "Somos un grupo empresarial colombiano de reconocido liderazgo en la " +
                                    "industria farmacéutica y de consumo masivo, comprometido desde hace más " +
                                    "de 80 años con el crecimiento económico y el avance social de nuestro país",
                    Condition = true
                },
                new Laboratory
                {
                    IdLaboratory = 3,
                    LaboratoryName = "Genfar",
                    Description = "Genfar es una empresa que creció con el compromiso de fabricar y llevar " +
                                    "medicamentos de calidad a cada vez más personas",
                    Condition = true
                },
                new Laboratory
                {
                    IdLaboratory = 4,
                    LaboratoryName = "Sanofi",
                    Description = "ofrecemos soluciones de salud innovadoras en una amplia gama de afecciones " +
                                    "de salud: ya sea una afección común como un resfriado, alergias, problemas " +
                                    "digestivos o afecciones graves tales como el cáncer o la esclerosis múltiple",
                    Condition = true
                });

            //Product
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    IdProduct = 1,
                    IdCategory = 1,
                    IdLaboratory = 2,
                    ProductName = "Acetaminofen",
                    BarCode = "sdfsdf",
                    Indicative = "tabletas, 3 al dia",
                    Stock = 100,
                    Price = 1000,
                    Condition = true
                },
                new Product
                {
                    IdProduct = 2,
                    IdCategory = 1,
                    IdLaboratory = 1,
                    ProductName = "Trimebutina",
                    BarCode = "sdfsdf",
                    Indicative = "tabletas",
                    Stock = 10,
                    Price = 4000,
                    Condition = true
                }, 
                new Product
                {
                    IdProduct = 3,
                    IdCategory = 2,
                    IdLaboratory = 4,
                    ProductName = "Dolex",
                    BarCode = "",
                    Indicative = "Liquido",
                    Stock = 10,
                    Price = 5000,
                    Condition = true
                },
                new Product
                {
                    IdProduct = 4,
                    IdCategory = 2,
                    IdLaboratory = 4,
                    ProductName = "Dolex",
                    BarCode = "",
                    Indicative = "Tableta",
                    Stock = 45,
                    Price = 3000,
                    Condition = true
                }, 
                new Product
                {
                    IdProduct = 5,
                    IdCategory = 1,
                    IdLaboratory = 1,
                    ProductName = "Albendazol",
                    BarCode = "",
                    Indicative = "Tableta",
                    Stock = 15,
                    Price = 2500,
                    Condition = false
                },
                new Product
                {
                    IdProduct = 6,
                    IdCategory = 2,
                    IdLaboratory = 2,
                    ProductName = "Velgolax",
                    BarCode = "",
                    Indicative = "Polvo",
                    Stock = 5,
                    Price = 6000,
                    Condition = true
                },
                new Product
                {
                    IdProduct = 7,
                    IdCategory = 2,
                    IdLaboratory = 3,
                    ProductName = "Pirantel",
                    BarCode = "",
                    Indicative = "Tableta",
                    Stock = 12,
                    Price = 3000,
                    Condition = true
                },
                new Product
                {
                    IdProduct = 8,
                    IdCategory = 1,
                    IdLaboratory = 4,
                    ProductName = "Naproxeno",
                    BarCode = "",
                    Indicative = "Tableta",
                    Stock = 25,
                    Price = 1500,
                    Condition = true
                }, 
                new Product
                {
                    IdProduct = 9,
                    IdCategory = 1,
                    IdLaboratory = 3,
                    ProductName = "Betametasona",
                    BarCode = "",
                    Indicative = "Ampolla",
                    Stock = 10,
                    Price = 1750,
                    Condition = true
                }, 
                new Product
                {
                    IdProduct = 10,
                    IdCategory = 2,
                    IdLaboratory = 4,
                    ProductName = "Advil Max",
                    BarCode = "",
                    Indicative = "Tableta",
                    Stock = 10,
                    Price = 1800,
                    Condition = true
                },
                new Product
                {
                    IdProduct = 11,
                    IdCategory = 2,
                    IdLaboratory = 1,
                    ProductName = "Descongel",
                    BarCode = "",
                    Indicative = "Tableta",
                    Stock = 10,
                    Price = 1300,
                    Condition = true
                },
                new Product
                {
                    IdProduct = 12,
                    IdCategory = 2,
                    IdLaboratory = 1,
                    ProductName = "Buscapina",
                    BarCode = "",
                    Indicative = "Tableta",
                    Stock = 0,
                    Price = 2300,
                    Condition = false
                },                
                new Product
                {
                    IdProduct = 13,
                    IdCategory = 1,
                    IdLaboratory = 2,
                    ProductName = "Ampicilina",
                    BarCode = "",
                    Indicative = "Tableta",
                    Stock = 5,
                    Price = 700,
                    Condition = true
                });


            //Provider
            modelBuilder.Entity<Provider>().HasData(
                new Provider
                {
                    IdProvider = 1,
                    ProviderName = "Copydroga",
                    PhoneNumber = "3005007070"
                }, 
                new Provider
                {
                    IdProvider = 2,
                    ProviderName = "DyDMendicamentos",
                    PhoneNumber = "32253457070"
                },
                new Provider
                {
                    IdProvider = 3,
                    ProviderName = "Distribuciones pastor julio",
                    PhoneNumber = "3005006670"
                },
                new Provider
                {
                    IdProvider = 4,
                    ProviderName = "Distribuciones La Fe",
                    PhoneNumber = "3015237070"
                });

            // Client
            modelBuilder.Entity<Client>().HasData(
                new Client
                {
                    IdClient = 1,
                    Name = "Juan",
                    LastName = "Mendoza",
                    DocumentType = "CC",
                    DocumentNumber = "1065789741",
                    PhoneNumber = "3005698741",
                    Condition = true                    
                }, 
                new Client
                {
                    IdClient = 2,
                    Name = "Andres",
                    LastName = "Quintero",
                    DocumentType = "CC",
                    DocumentNumber = "1065852654",
                    PhoneNumber = "3011235689",
                    Condition = true
                });
                             
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            var timestamp = DateTime.Now;

            foreach (var entry in ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
            {
                if (entry.Entity.GetType().GetCustomAttributes(typeof(AuditableAttribute), true).Length > 0)
                {
                    entry.Property("LastUpdated").CurrentValue = timestamp;

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
