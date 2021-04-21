using Microsoft.EntityFrameworkCore.Migrations;

namespace Podaci.Migrations
{
    public partial class a : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PopustKupon",
                keyColumn: "Id",
                keyValue: 1,
                column: "VrstaPopusta",
                value: "posto");

            migrationBuilder.UpdateData(
                table: "PopustKupon",
                keyColumn: "Id",
                keyValue: 2,
                column: "VrstaPopusta",
                value: "fix");

            migrationBuilder.UpdateData(
                table: "PopustKupon",
                keyColumn: "Id",
                keyValue: 3,
                column: "VrstaPopusta",
                value: "fix");

            migrationBuilder.UpdateData(
                table: "PopustKupon",
                keyColumn: "Id",
                keyValue: 4,
                column: "VrstaPopusta",
                value: "posto");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PopustKupon",
                keyColumn: "Id",
                keyValue: 1,
                column: "VrstaPopusta",
                value: null);

            migrationBuilder.UpdateData(
                table: "PopustKupon",
                keyColumn: "Id",
                keyValue: 2,
                column: "VrstaPopusta",
                value: null);

            migrationBuilder.UpdateData(
                table: "PopustKupon",
                keyColumn: "Id",
                keyValue: 3,
                column: "VrstaPopusta",
                value: null);

            migrationBuilder.UpdateData(
                table: "PopustKupon",
                keyColumn: "Id",
                keyValue: 4,
                column: "VrstaPopusta",
                value: null);
        }
    }
}
