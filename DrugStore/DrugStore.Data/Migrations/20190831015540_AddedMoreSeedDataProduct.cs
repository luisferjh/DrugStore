using Microsoft.EntityFrameworkCore.Migrations;

namespace DrugStore.Data.Migrations
{
    public partial class AddedMoreSeedDataProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 4,
                columns: new[] { "Indicative", "Price", "Stock" },
                values: new object[] { "Tableta", 3000m, 45 });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "IdProduct", "BarCode", "Condition", "IdCategory", "IdLaboratory", "Indicative", "Price", "ProductName", "Stock" },
                values: new object[,]
                {
                    { 5, "", true, 1, 1, "Tableta", 2500m, "Albendazol", 15 },
                    { 6, "", true, 2, 2, "Polvo", 6000m, "Velgolax", 5 },
                    { 7, "", true, 2, 3, "Tableta", 3000m, "Pirantel", 12 },
                    { 8, "", true, 1, 4, "Tableta", 1500m, "Naproxeno", 25 },
                    { 9, "", true, 1, 3, "Ampolla", 1750m, "Betametasona", 10 },
                    { 10, "", true, 2, 4, "Tableta", 1800m, "Advil Max", 10 },
                    { 11, "", true, 2, 1, "Tableta", 1300m, "Descongel", 10 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 11);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 4,
                columns: new[] { "Indicative", "Price", "Stock" },
                values: new object[] { "Liquido", 5000m, 10 });
        }
    }
}
