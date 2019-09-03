using Microsoft.EntityFrameworkCore.Migrations;

namespace DrugStore.Data.Migrations
{
    public partial class addSeedDataCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "IdCategory", "Condition", "Description", "Name" },
                values: new object[] { 1, true, "productos de caracter generico", "Generico" });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "IdCategory", "Condition", "Description", "Name" },
                values: new object[] { 2, true, "productos de caracter comercial", "Comercial" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "IdCategory",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "IdCategory",
                keyValue: 2);
        }
    }
}
