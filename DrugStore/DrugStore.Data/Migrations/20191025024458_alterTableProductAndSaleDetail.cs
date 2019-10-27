using Microsoft.EntityFrameworkCore.Migrations;

namespace DrugStore.Data.Migrations
{
    public partial class alterTableProductAndSaleDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitCost",
                table: "SaleDetail");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Product",
                newName: "UnitPrice");

            migrationBuilder.AddColumn<decimal>(
                name: "SalePrice",
                table: "Product",
                type: "decimal(11,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 1,
                columns: new[] { "SalePrice", "UnitPrice" },
                values: new object[] { 1000m, 800m });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 2,
                columns: new[] { "SalePrice", "UnitPrice" },
                values: new object[] { 4000m, 3500m });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 3,
                columns: new[] { "SalePrice", "UnitPrice" },
                values: new object[] { 5000m, 4500m });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 4,
                columns: new[] { "SalePrice", "UnitPrice" },
                values: new object[] { 3000m, 2400m });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 5,
                columns: new[] { "SalePrice", "UnitPrice" },
                values: new object[] { 2500m, 1970m });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 6,
                columns: new[] { "SalePrice", "UnitPrice" },
                values: new object[] { 6000m, 5320m });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 7,
                columns: new[] { "SalePrice", "UnitPrice" },
                values: new object[] { 3000m, 2600m });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 8,
                columns: new[] { "SalePrice", "UnitPrice" },
                values: new object[] { 1500m, 1250m });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 9,
                columns: new[] { "SalePrice", "UnitPrice" },
                values: new object[] { 1750m, 1500m });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 10,
                columns: new[] { "SalePrice", "UnitPrice" },
                values: new object[] { 1800m, 1460m });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 11,
                columns: new[] { "SalePrice", "UnitPrice" },
                values: new object[] { 1300m, 1100m });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 12,
                columns: new[] { "SalePrice", "UnitPrice" },
                values: new object[] { 2300m, 1910m });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 13,
                columns: new[] { "SalePrice", "UnitPrice" },
                values: new object[] { 700m, 380m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SalePrice",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                table: "Product",
                newName: "Price");

            migrationBuilder.AddColumn<decimal>(
                name: "UnitCost",
                table: "SaleDetail",
                type: "decimal(11,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 1,
                column: "Price",
                value: 1000m);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 2,
                column: "Price",
                value: 4000m);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 3,
                column: "Price",
                value: 5000m);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 4,
                column: "Price",
                value: 3000m);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 5,
                column: "Price",
                value: 2500m);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 6,
                column: "Price",
                value: 6000m);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 7,
                column: "Price",
                value: 3000m);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 8,
                column: "Price",
                value: 1500m);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 9,
                column: "Price",
                value: 1750m);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 10,
                column: "Price",
                value: 1800m);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 11,
                column: "Price",
                value: 1300m);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 12,
                column: "Price",
                value: 2300m);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 13,
                column: "Price",
                value: 700m);
        }
    }
}
