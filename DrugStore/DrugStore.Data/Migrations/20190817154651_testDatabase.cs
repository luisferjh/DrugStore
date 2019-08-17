using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DrugStore.Data.Migrations
{
    public partial class testDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    IdCategory = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false),
                    Description = table.Column<string>(type: "varchar(100)", nullable: false),
                    Condition = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.IdCategory);
                });

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    IdClient = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(30)", nullable: false),
                    LastName = table.Column<string>(type: "varchar(30)", nullable: false),
                    DocumentType = table.Column<string>(type: "varchar(20)", nullable: true),
                    DocumentNumber = table.Column<string>(type: "varchar(20)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(20)", nullable: true),
                    Condition = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.IdClient);
                });

            migrationBuilder.CreateTable(
                name: "Laboratory",
                columns: table => new
                {
                    IdLaboratory = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LaboratoryName = table.Column<string>(type: "varchar(50)", nullable: false),
                    Description = table.Column<string>(type: "varchar(100)", nullable: false),
                    Condition = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Laboratory", x => x.IdLaboratory);
                });

            migrationBuilder.CreateTable(
                name: "Losses",
                columns: table => new
                {
                    IdLosses = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Cause = table.Column<string>(type: "varchar(50)", nullable: false),
                    State = table.Column<string>(type: "varchar(20)", nullable: false),
                    DateOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Losses", x => x.IdLosses);
                });

            migrationBuilder.CreateTable(
                name: "Provider",
                columns: table => new
                {
                    IdProvider = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProviderName = table.Column<string>(type: "varchar(100)", nullable: false),
                    DocumentNumber = table.Column<string>(type: "varchar(20)", nullable: true),
                    Address = table.Column<string>(type: "varchar(70)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(20)", nullable: true),
                    Email = table.Column<string>(type: "varchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provider", x => x.IdProvider);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    IdRole = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleName = table.Column<string>(type: "varchar(30)", nullable: false),
                    Description = table.Column<string>(type: "varchar(100)", nullable: false),
                    Condition = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.IdRole);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    IdProduct = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdCategory = table.Column<int>(nullable: false),
                    IdLaboratory = table.Column<int>(nullable: false),
                    ProductName = table.Column<string>(type: "varchar(50)", nullable: false),
                    BarCode = table.Column<string>(type: "varchar(50)", nullable: true),
                    Indicative = table.Column<string>(type: "varchar(250)", nullable: true),
                    Stock = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(type: "decimal(11,2)", nullable: false),
                    Condition = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.IdProduct);
                    table.ForeignKey(
                        name: "FK_Product_Category_IdCategory",
                        column: x => x.IdCategory,
                        principalTable: "Category",
                        principalColumn: "IdCategory",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Product_Laboratory_IdLaboratory",
                        column: x => x.IdLaboratory,
                        principalTable: "Laboratory",
                        principalColumn: "IdLaboratory",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    IdUser = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdRole = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(type: "varchar(100)", nullable: false),
                    DocumentType = table.Column<string>(type: "varchar(20)", nullable: false),
                    DocumentNumber = table.Column<string>(type: "varchar(20)", nullable: false),
                    Address = table.Column<string>(type: "varchar(70)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(20)", nullable: false),
                    Email = table.Column<string>(type: "varchar(70)", nullable: false),
                    PasswordHash = table.Column<byte[]>(maxLength: 64, nullable: false),
                    PasswordSalt = table.Column<byte[]>(maxLength: 64, nullable: false),
                    Condition = table.Column<bool>(nullable: false),
                    DateOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.IdUser);
                    table.ForeignKey(
                        name: "FK_User_Role_IdRole",
                        column: x => x.IdRole,
                        principalTable: "Role",
                        principalColumn: "IdRole",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LossDetail",
                columns: table => new
                {
                    IdDetailLosses = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdLosses = table.Column<int>(nullable: false),
                    IdProduct = table.Column<int>(nullable: false),
                    UnitCost = table.Column<decimal>(type: "decimal(11,2)", nullable: false),
                    Amount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LossDetail", x => x.IdDetailLosses);
                    table.ForeignKey(
                        name: "FK_LossDetail_Losses_IdLosses",
                        column: x => x.IdLosses,
                        principalTable: "Losses",
                        principalColumn: "IdLosses",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LossDetail_Product_IdProduct",
                        column: x => x.IdProduct,
                        principalTable: "Product",
                        principalColumn: "IdProduct",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderIncome",
                columns: table => new
                {
                    IdOrderIncome = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdProvider = table.Column<int>(nullable: false),
                    IdUser = table.Column<int>(nullable: false),
                    Total = table.Column<decimal>(type: "decimal(11,2)", nullable: false),
                    State = table.Column<string>(type: "varchar(20)", nullable: true),
                    DateOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderIncome", x => x.IdOrderIncome);
                    table.ForeignKey(
                        name: "FK_OrderIncome_Provider_IdProvider",
                        column: x => x.IdProvider,
                        principalTable: "Provider",
                        principalColumn: "IdProvider",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderIncome_User_IdUser",
                        column: x => x.IdUser,
                        principalTable: "User",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sale",
                columns: table => new
                {
                    IdSale = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdUser = table.Column<int>(nullable: false),
                    IdClient = table.Column<int>(nullable: true),
                    TypeSale = table.Column<string>(type: "varchar(20)", nullable: false),
                    VoucherSeries = table.Column<string>(type: "varchar(7)", nullable: false),
                    VoucherNumber = table.Column<string>(type: "varchar(10)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(11,2)", nullable: false),
                    State = table.Column<string>(type: "varchar(20)", nullable: false),
                    DateOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sale", x => x.IdSale);
                    table.ForeignKey(
                        name: "FK_Sale_Client_IdClient",
                        column: x => x.IdClient,
                        principalTable: "Client",
                        principalColumn: "IdClient",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sale_User_IdUser",
                        column: x => x.IdUser,
                        principalTable: "User",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderIncomeDetails",
                columns: table => new
                {
                    IdOrderIncomeDetails = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdOrderIncome = table.Column<int>(nullable: false),
                    IdProduct = table.Column<int>(nullable: false),
                    Amount = table.Column<int>(nullable: false),
                    DueDate = table.Column<DateTime>(nullable: false),
                    UnitCost = table.Column<decimal>(type: "decimal(11,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderIncomeDetails", x => x.IdOrderIncomeDetails);
                    table.ForeignKey(
                        name: "FK_OrderIncomeDetails_OrderIncome_IdOrderIncome",
                        column: x => x.IdOrderIncome,
                        principalTable: "OrderIncome",
                        principalColumn: "IdOrderIncome",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderIncomeDetails_Product_IdProduct",
                        column: x => x.IdProduct,
                        principalTable: "Product",
                        principalColumn: "IdProduct",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Delivery",
                columns: table => new
                {
                    IdSale = table.Column<int>(nullable: false),
                    IdClient = table.Column<int>(nullable: false),
                    Adress = table.Column<string>(type: "varchar(30)", nullable: false),
                    State = table.Column<string>(type: "varchar(20)", nullable: false),
                    DateOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Delivery", x => x.IdSale);
                    table.ForeignKey(
                        name: "FK_Delivery_Client_IdClient",
                        column: x => x.IdClient,
                        principalTable: "Client",
                        principalColumn: "IdClient",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Delivery_Sale_IdSale",
                        column: x => x.IdSale,
                        principalTable: "Sale",
                        principalColumn: "IdSale",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SaleDetail",
                columns: table => new
                {
                    IdSaleDetail = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdSale = table.Column<int>(nullable: false),
                    IdProduct = table.Column<int>(nullable: false),
                    Amount = table.Column<int>(nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(11,2)", nullable: false),
                    UnitCost = table.Column<decimal>(type: "decimal(11,2)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(11,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleDetail", x => x.IdSaleDetail);
                    table.ForeignKey(
                        name: "FK_SaleDetail_Product_IdProduct",
                        column: x => x.IdProduct,
                        principalTable: "Product",
                        principalColumn: "IdProduct",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SaleDetail_Sale_IdSale",
                        column: x => x.IdSale,
                        principalTable: "Sale",
                        principalColumn: "IdSale",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Delivery_IdClient",
                table: "Delivery",
                column: "IdClient");

            migrationBuilder.CreateIndex(
                name: "IX_LossDetail_IdLosses",
                table: "LossDetail",
                column: "IdLosses");

            migrationBuilder.CreateIndex(
                name: "IX_LossDetail_IdProduct",
                table: "LossDetail",
                column: "IdProduct");

            migrationBuilder.CreateIndex(
                name: "IX_OrderIncome_IdProvider",
                table: "OrderIncome",
                column: "IdProvider");

            migrationBuilder.CreateIndex(
                name: "IX_OrderIncome_IdUser",
                table: "OrderIncome",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_OrderIncomeDetails_IdOrderIncome",
                table: "OrderIncomeDetails",
                column: "IdOrderIncome");

            migrationBuilder.CreateIndex(
                name: "IX_OrderIncomeDetails_IdProduct",
                table: "OrderIncomeDetails",
                column: "IdProduct");

            migrationBuilder.CreateIndex(
                name: "IX_Product_IdCategory",
                table: "Product",
                column: "IdCategory");

            migrationBuilder.CreateIndex(
                name: "IX_Product_IdLaboratory",
                table: "Product",
                column: "IdLaboratory");

            migrationBuilder.CreateIndex(
                name: "IX_Sale_IdClient",
                table: "Sale",
                column: "IdClient");

            migrationBuilder.CreateIndex(
                name: "IX_Sale_IdUser",
                table: "Sale",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_SaleDetail_IdProduct",
                table: "SaleDetail",
                column: "IdProduct");

            migrationBuilder.CreateIndex(
                name: "IX_SaleDetail_IdSale",
                table: "SaleDetail",
                column: "IdSale");

            migrationBuilder.CreateIndex(
                name: "IX_User_IdRole",
                table: "User",
                column: "IdRole");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Delivery");

            migrationBuilder.DropTable(
                name: "LossDetail");

            migrationBuilder.DropTable(
                name: "OrderIncomeDetails");

            migrationBuilder.DropTable(
                name: "SaleDetail");

            migrationBuilder.DropTable(
                name: "Losses");

            migrationBuilder.DropTable(
                name: "OrderIncome");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Sale");

            migrationBuilder.DropTable(
                name: "Provider");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Laboratory");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
