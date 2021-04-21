using Microsoft.EntityFrameworkCore.Migrations;

namespace Podaci.Migrations
{
    public partial class izmjene : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KorisikId",
                table: "Narudzba");

            migrationBuilder.InsertData(
                table: "Proizvod",
                columns: new[] { "Id", "KategorijaId", "Naziv", "ProizvodjacId" },
                values: new object[,]
                {
                    { 1, 1, "Hrana za macke", 1 },
                    { 2, 3, "Igracka za macke", 2 },
                    { 3, 2, "Odjeca za macke", 3 },
                    { 4, 2, "Odjeca za macke", 4 },
                    { 5, 3, "Igracke za macke", 3 },
                    { 6, 4, "Nesto drugo", 2 },
                    { 7, 1, "Hrana za ptice", 1 },
                    { 8, 3, "Igracke za macke", 1 },
                    { 9, 4, "Nesto trece", 4 },
                    { 10, 1, "Hrana za pse", 3 }
                });

            migrationBuilder.InsertData(
                table: "Slika",
                columns: new[] { "Id", "Putanja" },
                values: new object[,]
                {
                    { 4, "hrana.jpg" },
                    { 5, "odjeca.jpg" },
                    { 6, "igracka.jpg" }
                });

            migrationBuilder.InsertData(
                table: "ProizvodVarijacija",
                columns: new[] { "Id", "Cijena", "Opis", "ProizvodId", "SlikaId", "Velicina" },
                values: new object[,]
                {
                    { 1, 10.0, "Hrana za macke whiskas mnogo dobra", 1, 4, "Veliko" },
                    { 7, 10.0, "Hrana za ptice i papagaje", 7, 4, "Veliko" },
                    { 3, 20.0, "Odjeca za macke armani", 3, 5, "Srednje" },
                    { 4, 5.0, "Odjeca za macke gucci", 4, 5, "Malo" },
                    { 6, 4.0, "Nesto drugo aa", 6, 5, "Veliko" },
                    { 9, 13.0, "Nesto trece aaaaa", 9, 5, "Srednje" },
                    { 2, 50.0, "Igracka za macke iz danske", 2, 6, "Srednje" },
                    { 5, 3.0, "Igracke za macke lego", 5, 6, "Malo" },
                    { 8, 26.0, "Igracke za macke cici mici", 8, 6, "Veliko" },
                    { 10, 11.0, "Hrana za pse aw aw", 10, 6, "Malo" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProizvodVarijacija",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProizvodVarijacija",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProizvodVarijacija",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProizvodVarijacija",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ProizvodVarijacija",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ProizvodVarijacija",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ProizvodVarijacija",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ProizvodVarijacija",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ProizvodVarijacija",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ProizvodVarijacija",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Proizvod",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Proizvod",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Proizvod",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Proizvod",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Proizvod",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Proizvod",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Proizvod",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Proizvod",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Proizvod",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Proizvod",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Slika",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Slika",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Slika",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.AddColumn<int>(
                name: "KorisikId",
                table: "Narudzba",
                type: "int",
                nullable: true);
        }
    }
}
