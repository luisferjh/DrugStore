using Microsoft.EntityFrameworkCore.Migrations;

namespace DrugStore.Data.Migrations
{
    public partial class addMoreSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Laboratory",
                type: "varchar(250)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)");

            migrationBuilder.AlterColumn<string>(
                name: "DocumentNumber",
                table: "Client",
                type: "varchar(20)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)");

            migrationBuilder.InsertData(
                table: "Client",
                columns: new[] { "IdClient", "Condition", "DocumentNumber", "DocumentType", "LastName", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, true, "1065789741", "CC", "Mendoza", "Juan", "3005698741" },
                    { 2, true, "1065852654", "CC", "Quintero", "Andres", "3011235689" }
                });

            migrationBuilder.InsertData(
                table: "Laboratory",
                columns: new[] { "IdLaboratory", "Condition", "Description", "LaboratoryName" },
                values: new object[,]
                {
                    { 1, true, "Bayer es una compañía de innovación que investiga en las áreas de Ciencias de la Vida centradas en la salud y la agricultura.", "Bayer" },
                    { 2, true, "Somos un grupo empresarial colombiano de reconocido liderazgo en la industria farmacéutica y de consumo masivo, comprometido desde hace más de 80 años con el crecimiento económico y el avance social de nuestro país", "Tecnoquimicas" },
                    { 3, true, "Genfar es una empresa que creció con el compromiso de fabricar y llevar medicamentos de calidad a cada vez más personas", "Genfar" },
                    { 4, true, "ofrecemos soluciones de salud innovadoras en una amplia gama de afecciones de salud: ya sea una afección común como un resfriado, alergias, problemas digestivos o afecciones graves tales como el cáncer o la esclerosis múltiple", "Sanofi" }
                });

            migrationBuilder.InsertData(
                table: "Provider",
                columns: new[] { "IdProvider", "Address", "DocumentNumber", "Email", "PhoneNumber", "ProviderName" },
                values: new object[,]
                {
                    { 1, null, null, null, "3005007070", "Copydroga" },
                    { 2, null, null, null, "32253457070", "DyDMendicamentos" },
                    { 3, null, null, null, "3005006670", "Distribuciones pastor julio" },
                    { 4, null, null, null, "3015237070", "Distribuciones La Fe" }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "IdProduct", "BarCode", "Condition", "IdCategory", "IdLaboratory", "Indicative", "Price", "ProductName", "Stock" },
                values: new object[,]
                {
                    { 2, "sdfsdf", true, 1, 1, "tabletas", 4000m, "Trimebutina", 10 },
                    { 1, "sdfsdf", true, 1, 2, "tabletas, 3 al dia", 1000m, "Acetaminofen", 100 },
                    { 3, "", true, 2, 4, "Liquido", 5000m, "Dolex", 10 },
                    { 4, "", true, 2, 4, "Liquido", 5000m, "Dolex", 10 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Client",
                keyColumn: "IdClient",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Client",
                keyColumn: "IdClient",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Laboratory",
                keyColumn: "IdLaboratory",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "IdProduct",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Provider",
                keyColumn: "IdProvider",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Provider",
                keyColumn: "IdProvider",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Provider",
                keyColumn: "IdProvider",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Provider",
                keyColumn: "IdProvider",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Laboratory",
                keyColumn: "IdLaboratory",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Laboratory",
                keyColumn: "IdLaboratory",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Laboratory",
                keyColumn: "IdLaboratory",
                keyValue: 4);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Laboratory",
                type: "varchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(250)");

            migrationBuilder.AlterColumn<string>(
                name: "DocumentNumber",
                table: "Client",
                type: "varchar(20)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldNullable: true);
        }
    }
}
