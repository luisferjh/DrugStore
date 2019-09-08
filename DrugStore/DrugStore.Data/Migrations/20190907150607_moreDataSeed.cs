using Microsoft.EntityFrameworkCore.Migrations;

namespace DrugStore.Data.Migrations
{
    public partial class moreDataSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 5,
                column: "Condition",
                value: false);

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "IdProduct", "BarCode", "Condition", "IdCategory", "IdLaboratory", "Indicative", "Price", "ProductName", "Stock" },
                values: new object[] { 12, "", false, 2, 1, "Tableta", 2300m, "Buscapina", 0 });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "IdProduct", "BarCode", "Condition", "IdCategory", "IdLaboratory", "Indicative", "Price", "ProductName", "Stock" },
                values: new object[] { 13, "", true, 1, 2, "Tableta", 700m, "Ampicilina", 5 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 13);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 5,
                column: "Condition",
                value: true);
        }
    }
}
