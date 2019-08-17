﻿// <auto-generated />
using System;
using DrugStore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DrugStore.Data.Migrations
{
    [DbContext(typeof(DbContextDrugStore))]
    partial class DbContextDrugStoreModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DrugStore.Entities.Orders.OrderIncome", b =>
                {
                    b.Property<int>("IdOrderIncome")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateOn");

                    b.Property<int>("IdProvider");

                    b.Property<int>("IdUser");

                    b.Property<string>("State")
                        .HasColumnType("varchar(20)");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(11,2)");

                    b.HasKey("IdOrderIncome");

                    b.HasIndex("IdProvider");

                    b.HasIndex("IdUser");

                    b.ToTable("OrderIncome");
                });

            modelBuilder.Entity("DrugStore.Entities.Orders.OrderIncomeDetails", b =>
                {
                    b.Property<int>("IdOrderIncomeDetails")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Amount");

                    b.Property<DateTime>("DueDate");

                    b.Property<int>("IdOrderIncome");

                    b.Property<int>("IdProduct");

                    b.Property<decimal>("UnitCost")
                        .HasColumnType("decimal(11,2)");

                    b.HasKey("IdOrderIncomeDetails");

                    b.HasIndex("IdOrderIncome");

                    b.HasIndex("IdProduct");

                    b.ToTable("OrderIncomeDetails");
                });

            modelBuilder.Entity("DrugStore.Entities.Sales.Delivery", b =>
                {
                    b.Property<int>("IdSale");

                    b.Property<string>("Adress")
                        .IsRequired()
                        .HasColumnType("varchar(30)");

                    b.Property<DateTime>("DateOn");

                    b.Property<int>("IdClient");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.HasKey("IdSale");

                    b.HasIndex("IdClient");

                    b.ToTable("Delivery");
                });

            modelBuilder.Entity("DrugStore.Entities.Sales.Sale", b =>
                {
                    b.Property<int>("IdSale")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateOn");

                    b.Property<int?>("IdClient");

                    b.Property<int>("IdUser");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(11,2)");

                    b.Property<string>("TypeSale")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<string>("VoucherNumber")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.Property<string>("VoucherSeries")
                        .IsRequired()
                        .HasColumnType("varchar(7)");

                    b.HasKey("IdSale");

                    b.HasIndex("IdClient");

                    b.HasIndex("IdUser");

                    b.ToTable("Sale");
                });

            modelBuilder.Entity("DrugStore.Entities.Sales.SaleDetail", b =>
                {
                    b.Property<int>("IdSaleDetail")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Amount");

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(11,2)");

                    b.Property<int>("IdProduct");

                    b.Property<int>("IdSale");

                    b.Property<decimal>("UnitCost")
                        .HasColumnType("decimal(11,2)");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(11,2)");

                    b.HasKey("IdSaleDetail");

                    b.HasIndex("IdProduct");

                    b.HasIndex("IdSale");

                    b.ToTable("SaleDetail");
                });

            modelBuilder.Entity("DrugStore.Entities.Store.Category", b =>
                {
                    b.Property<int>("IdCategory")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Condition");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("IdCategory");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("DrugStore.Entities.Store.Laboratory", b =>
                {
                    b.Property<int>("IdLaboratory")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Condition");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("LaboratoryName")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("IdLaboratory");

                    b.ToTable("Laboratory");
                });

            modelBuilder.Entity("DrugStore.Entities.Store.LossDetail", b =>
                {
                    b.Property<int>("IdDetailLosses")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Amount");

                    b.Property<int>("IdLosses");

                    b.Property<int>("IdProduct");

                    b.Property<decimal>("UnitCost")
                        .HasColumnType("decimal(11,2)");

                    b.HasKey("IdDetailLosses");

                    b.HasIndex("IdLosses");

                    b.HasIndex("IdProduct");

                    b.ToTable("LossDetail");
                });

            modelBuilder.Entity("DrugStore.Entities.Store.Losses", b =>
                {
                    b.Property<int>("IdLosses")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Cause")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("DateOn");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.HasKey("IdLosses");

                    b.ToTable("Losses");
                });

            modelBuilder.Entity("DrugStore.Entities.Store.Product", b =>
                {
                    b.Property<int>("IdProduct")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BarCode")
                        .HasColumnType("varchar(50)");

                    b.Property<bool>("Condition");

                    b.Property<int>("IdCategory");

                    b.Property<int>("IdLaboratory");

                    b.Property<string>("Indicative")
                        .HasColumnType("varchar(250)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(11,2)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<int>("Stock");

                    b.HasKey("IdProduct");

                    b.HasIndex("IdCategory");

                    b.HasIndex("IdLaboratory");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("DrugStore.Entities.Users.Client", b =>
                {
                    b.Property<int>("IdClient")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Condition");

                    b.Property<string>("DocumentNumber")
                        .IsRequired()
                        .HasConversion(new ValueConverter<string, string>(v => default(string), v => default(string), new ConverterMappingHints(size: 64)))
                        .HasColumnType("varchar(20)");

                    b.Property<string>("DocumentType")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(30)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("varchar(20)");

                    b.HasKey("IdClient");

                    b.ToTable("Client");
                });

            modelBuilder.Entity("DrugStore.Entities.Users.Provider", b =>
                {
                    b.Property<int>("IdProvider")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("varchar(70)");

                    b.Property<string>("DocumentNumber")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(30)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("ProviderName")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("IdProvider");

                    b.ToTable("Provider");
                });

            modelBuilder.Entity("DrugStore.Entities.Users.Role", b =>
                {
                    b.Property<int>("IdRole")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Condition");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("varchar(30)");

                    b.HasKey("IdRole");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("DrugStore.Entities.Users.User", b =>
                {
                    b.Property<int>("IdUser")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("varchar(70)");

                    b.Property<bool>("Condition");

                    b.Property<DateTime>("DateOn");

                    b.Property<string>("DocumentNumber")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<string>("DocumentType")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(70)");

                    b.Property<int>("IdRole");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("IdUser");

                    b.HasIndex("IdRole");

                    b.ToTable("User");
                });

            modelBuilder.Entity("DrugStore.Entities.Orders.OrderIncome", b =>
                {
                    b.HasOne("DrugStore.Entities.Users.Provider", "Provider")
                        .WithMany("OrderIncomes")
                        .HasForeignKey("IdProvider")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DrugStore.Entities.Users.User", "User")
                        .WithMany("OrderIncomes")
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("DrugStore.Entities.Orders.OrderIncomeDetails", b =>
                {
                    b.HasOne("DrugStore.Entities.Orders.OrderIncome", "OrderIncome")
                        .WithMany("OrderIncomeDetails")
                        .HasForeignKey("IdOrderIncome")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DrugStore.Entities.Store.Product", "Product")
                        .WithMany("OrderIncomeDetails")
                        .HasForeignKey("IdProduct")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("DrugStore.Entities.Sales.Delivery", b =>
                {
                    b.HasOne("DrugStore.Entities.Users.Client", "Client")
                        .WithMany("Deliveries")
                        .HasForeignKey("IdClient")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DrugStore.Entities.Sales.Sale", "Sale")
                        .WithOne("Delivery")
                        .HasForeignKey("DrugStore.Entities.Sales.Delivery", "IdSale")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DrugStore.Entities.Sales.Sale", b =>
                {
                    b.HasOne("DrugStore.Entities.Users.Client", "Client")
                        .WithMany("Sales")
                        .HasForeignKey("IdClient")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DrugStore.Entities.Users.User", "User")
                        .WithMany("Sales")
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("DrugStore.Entities.Sales.SaleDetail", b =>
                {
                    b.HasOne("DrugStore.Entities.Store.Product", "Products")
                        .WithMany("SaleDetails")
                        .HasForeignKey("IdProduct")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DrugStore.Entities.Sales.Sale", "Sale")
                        .WithMany("SaleDetails")
                        .HasForeignKey("IdSale")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DrugStore.Entities.Store.LossDetail", b =>
                {
                    b.HasOne("DrugStore.Entities.Store.Losses", "Losses")
                        .WithMany("LossDetails")
                        .HasForeignKey("IdLosses")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DrugStore.Entities.Store.Product", "Product")
                        .WithMany("LossDetails")
                        .HasForeignKey("IdProduct")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("DrugStore.Entities.Store.Product", b =>
                {
                    b.HasOne("DrugStore.Entities.Store.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("IdCategory")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DrugStore.Entities.Store.Laboratory", "Laboratory")
                        .WithMany("Products")
                        .HasForeignKey("IdLaboratory")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("DrugStore.Entities.Users.User", b =>
                {
                    b.HasOne("DrugStore.Entities.Users.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("IdRole")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
