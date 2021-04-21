using Microsoft.EntityFrameworkCore.Migrations;

namespace Podaci.Migrations
{
    public partial class kontaktEdit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KorisnikId",
                table: "Kontakt",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Odgovor",
                table: "Kontakt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Kontakt_KorisnikId",
                table: "Kontakt",
                column: "KorisnikId");

            migrationBuilder.AddForeignKey(
                name: "FK_Kontakt_Korisnik_KorisnikId",
                table: "Kontakt",
                column: "KorisnikId",
                principalTable: "Korisnik",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kontakt_Korisnik_KorisnikId",
                table: "Kontakt");

            migrationBuilder.DropIndex(
                name: "IX_Kontakt_KorisnikId",
                table: "Kontakt");

            migrationBuilder.DropColumn(
                name: "KorisnikId",
                table: "Kontakt");

            migrationBuilder.DropColumn(
                name: "Odgovor",
                table: "Kontakt");
        }
    }
}
