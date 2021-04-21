using Microsoft.EntityFrameworkCore.Migrations;

namespace Podaci.Migrations
{
    public partial class Inicijalnamigracija : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Drzava",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drzava", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kategorija",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vrsta = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategorija", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PopustKupon",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Iznos = table.Column<int>(type: "int", nullable: false),
                    Kod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VrstaPopusta = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PopustKupon", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Poslovnica",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrojTelefona = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Grad = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Poslovnica", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Slika",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Putanja = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slika", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kontakt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Odgovoreno = table.Column<bool>(type: "bit", nullable: false),
                    AdminId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kontakt", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kontakt_Admin_AdminId",
                        column: x => x.AdminId,
                        principalTable: "Admin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Korisnik",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrojTelefona = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DrzavaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnik", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Korisnik_Drzava_DrzavaId",
                        column: x => x.DrzavaId,
                        principalTable: "Drzava",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Proizvodjac",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DrzavaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proizvodjac", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Proizvodjac_Drzava_DrzavaId",
                        column: x => x.DrzavaId,
                        principalTable: "Drzava",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Novosti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naslov = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Datum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SlikaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Novosti", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Novosti_Slika_SlikaId",
                        column: x => x.SlikaId,
                        principalTable: "Slika",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Adresa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Grad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KorisnikId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adresa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Adresa_Korisnik_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Proizvod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProizvodjacId = table.Column<int>(type: "int", nullable: true),
                    KategorijaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proizvod", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Proizvod_Kategorija_KategorijaId",
                        column: x => x.KategorijaId,
                        principalTable: "Kategorija",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Proizvod_Proizvodjac_ProizvodjacId",
                        column: x => x.ProizvodjacId,
                        principalTable: "Proizvodjac",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Narudzba",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Aktivna = table.Column<bool>(type: "bit", nullable: false),
                    Zavrsena = table.Column<bool>(type: "bit", nullable: false),
                    Datum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KorisikId = table.Column<int>(type: "int", nullable: true),
                    KorisnikId = table.Column<int>(type: "int", nullable: true),
                    AdresaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Narudzba", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Narudzba_Adresa_AdresaId",
                        column: x => x.AdresaId,
                        principalTable: "Adresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Narudzba_Korisnik_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Komentar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Odobren = table.Column<bool>(type: "bit", nullable: false),
                    Datum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KorisnikId = table.Column<int>(type: "int", nullable: true),
                    ProizvodId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Komentar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Komentar_Korisnik_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Komentar_Proizvod_ProizvodId",
                        column: x => x.ProizvodId,
                        principalTable: "Proizvod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProizvodVarijacija",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cijena = table.Column<double>(type: "float", nullable: false),
                    Velicina = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SlikaId = table.Column<int>(type: "int", nullable: true),
                    ProizvodId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProizvodVarijacija", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProizvodVarijacija_Proizvod_ProizvodId",
                        column: x => x.ProizvodId,
                        principalTable: "Proizvod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProizvodVarijacija_Slika_SlikaId",
                        column: x => x.SlikaId,
                        principalTable: "Slika",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Recenzija",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ocjena = table.Column<int>(type: "int", nullable: false),
                    Datum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KorisnikId = table.Column<int>(type: "int", nullable: true),
                    ProizvodId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recenzija", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recenzija_Korisnik_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recenzija_Proizvod_ProizvodId",
                        column: x => x.ProizvodId,
                        principalTable: "Proizvod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transakcija",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Iznos = table.Column<double>(type: "float", nullable: false),
                    NacinPlacanja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Datum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PopustKuponId = table.Column<int>(type: "int", nullable: true),
                    NarudzbaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transakcija", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transakcija_Narudzba_NarudzbaId",
                        column: x => x.NarudzbaId,
                        principalTable: "Narudzba",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transakcija_PopustKupon_PopustKuponId",
                        column: x => x.PopustKuponId,
                        principalTable: "PopustKupon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NarudzbaProizvodVarijacija",
                columns: table => new
                {
                    NarudzbaId = table.Column<int>(type: "int", nullable: false),
                    ProizvodVarijacijaId = table.Column<int>(type: "int", nullable: false),
                    Kolicina = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NarudzbaProizvodVarijacija", x => new { x.NarudzbaId, x.ProizvodVarijacijaId });
                    table.ForeignKey(
                        name: "FK_NarudzbaProizvodVarijacija_Narudzba_NarudzbaId",
                        column: x => x.NarudzbaId,
                        principalTable: "Narudzba",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NarudzbaProizvodVarijacija_ProizvodVarijacija_ProizvodVarijacijaId",
                        column: x => x.ProizvodVarijacijaId,
                        principalTable: "ProizvodVarijacija",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Admin",
                columns: new[] { "Id", "Email", "Ime", "Password", "Prezime" },
                values: new object[] { 1, "abdulahproho@edu.fit.ba", "Abdulah", "abdulah", "Proho" });

            migrationBuilder.InsertData(
                table: "Drzava",
                columns: new[] { "Id", "Naziv" },
                values: new object[,]
                {
                    { 1, "Bosna i Hercegovina" },
                    { 2, "Srbija" },
                    { 3, "Crna Gora" },
                    { 4, "Hrvatska" },
                    { 5, "Slovenija" },
                    { 6, "Makedonija" }
                });

            migrationBuilder.InsertData(
                table: "Kategorija",
                columns: new[] { "Id", "Vrsta" },
                values: new object[,]
                {
                    { 3, "Igracke" },
                    { 4, "Ostalo" },
                    { 1, "Hrana" },
                    { 2, "Odjeca" }
                });

            migrationBuilder.InsertData(
                table: "PopustKupon",
                columns: new[] { "Id", "Iznos", "Kod", "VrstaPopusta" },
                values: new object[,]
                {
                    { 1, 20, "dvajest", null },
                    { 2, 10, "cener", null },
                    { 3, 20, "cvaja", null },
                    { 4, 30, "trisha", null }
                });

            migrationBuilder.InsertData(
                table: "Poslovnica",
                columns: new[] { "Id", "Adresa", "BrojTelefona", "Grad" },
                values: new object[,]
                {
                    { 1, "Hase Hasanovica 13", "033/456-333", "Sarajevo" },
                    { 2, "Senzacional 55", "036/782-411", "Mostar" },
                    { 3, "Tvrdzava 22", "032/412-111", "Travnik" }
                });

            migrationBuilder.InsertData(
                table: "Slika",
                columns: new[] { "Id", "Putanja" },
                values: new object[,]
                {
                    { 2, "poslovnica.jpg" },
                    { 1, "whiskas.png" },
                    { 3, "toys.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Korisnik",
                columns: new[] { "Id", "BrojTelefona", "DrzavaId", "Email", "Ime", "Password", "Prezime" },
                values: new object[] { 1, "060300360", 1, "ajdinvreto@edu.fit.ba", "Ajdin", "ajdin", "Vreto" });

            migrationBuilder.InsertData(
                table: "Novosti",
                columns: new[] { "Id", "Datum", "Naslov", "SlikaId", "Text" },
                values: new object[,]
                {
                    { 1, "11/11/2020", "Whiskas proizvodi - snizenje", 1, "Kupute svasta nesto i dobijte puno popusta za svaku kupovinu whiskas prozivoda" },
                    { 2, "12/11/2020", "Otvorenje nove poslovnice u Travniku", 2, "Otvorenje nove poslovnice u Travniku. Pozivamo vas da nam se pridruzite i uvelicate otvorenje do sad najvece poslovnice na cijelom blakanu." },
                    { 3, "13/11/2020", "Nove igracke stigle u nase trgovine", 3, "Najnoviji asortiman igackica za vase zivotinjice." }
                });

            migrationBuilder.InsertData(
                table: "Proizvodjac",
                columns: new[] { "Id", "DrzavaId", "Naziv" },
                values: new object[,]
                {
                    { 1, 1, "Mars" },
                    { 3, 2, "Royal cani" },
                    { 2, 3, "Nagger" },
                    { 4, 4, "Purina" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adresa_KorisnikId",
                table: "Adresa",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Komentar_KorisnikId",
                table: "Komentar",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Komentar_ProizvodId",
                table: "Komentar",
                column: "ProizvodId");

            migrationBuilder.CreateIndex(
                name: "IX_Kontakt_AdminId",
                table: "Kontakt",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Korisnik_DrzavaId",
                table: "Korisnik",
                column: "DrzavaId");

            migrationBuilder.CreateIndex(
                name: "IX_Narudzba_AdresaId",
                table: "Narudzba",
                column: "AdresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Narudzba_KorisnikId",
                table: "Narudzba",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_NarudzbaProizvodVarijacija_ProizvodVarijacijaId",
                table: "NarudzbaProizvodVarijacija",
                column: "ProizvodVarijacijaId");

            migrationBuilder.CreateIndex(
                name: "IX_Novosti_SlikaId",
                table: "Novosti",
                column: "SlikaId");

            migrationBuilder.CreateIndex(
                name: "IX_Proizvod_KategorijaId",
                table: "Proizvod",
                column: "KategorijaId");

            migrationBuilder.CreateIndex(
                name: "IX_Proizvod_ProizvodjacId",
                table: "Proizvod",
                column: "ProizvodjacId");

            migrationBuilder.CreateIndex(
                name: "IX_Proizvodjac_DrzavaId",
                table: "Proizvodjac",
                column: "DrzavaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProizvodVarijacija_ProizvodId",
                table: "ProizvodVarijacija",
                column: "ProizvodId");

            migrationBuilder.CreateIndex(
                name: "IX_ProizvodVarijacija_SlikaId",
                table: "ProizvodVarijacija",
                column: "SlikaId");

            migrationBuilder.CreateIndex(
                name: "IX_Recenzija_KorisnikId",
                table: "Recenzija",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Recenzija_ProizvodId",
                table: "Recenzija",
                column: "ProizvodId");

            migrationBuilder.CreateIndex(
                name: "IX_Transakcija_NarudzbaId",
                table: "Transakcija",
                column: "NarudzbaId");

            migrationBuilder.CreateIndex(
                name: "IX_Transakcija_PopustKuponId",
                table: "Transakcija",
                column: "PopustKuponId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Komentar");

            migrationBuilder.DropTable(
                name: "Kontakt");

            migrationBuilder.DropTable(
                name: "NarudzbaProizvodVarijacija");

            migrationBuilder.DropTable(
                name: "Novosti");

            migrationBuilder.DropTable(
                name: "Poslovnica");

            migrationBuilder.DropTable(
                name: "Recenzija");

            migrationBuilder.DropTable(
                name: "Transakcija");

            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "ProizvodVarijacija");

            migrationBuilder.DropTable(
                name: "Narudzba");

            migrationBuilder.DropTable(
                name: "PopustKupon");

            migrationBuilder.DropTable(
                name: "Proizvod");

            migrationBuilder.DropTable(
                name: "Slika");

            migrationBuilder.DropTable(
                name: "Adresa");

            migrationBuilder.DropTable(
                name: "Kategorija");

            migrationBuilder.DropTable(
                name: "Proizvodjac");

            migrationBuilder.DropTable(
                name: "Korisnik");

            migrationBuilder.DropTable(
                name: "Drzava");
        }
    }
}
