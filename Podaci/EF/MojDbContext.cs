using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using rs1_pet_shop_webapp.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rs1_pet_shop_webapp.EF
{
    public class MojDbContext : DbContext
    {
        public MojDbContext() : base() { }
        public MojDbContext(DbContextOptions<MojDbContext> options)
            : base(options)
        { }

        public DbSet<Admin> Admin { get; set; }
        public DbSet<Adresa> Adresa { get; set; }
        public DbSet<Drzava> Drzava { get; set; }
        public DbSet<Kategorija> Kategorija { get; set; }
        public DbSet<Komentar> Komentar { get; set; }
        public DbSet<Kontakt> Kontakt { get; set; }
        public DbSet<Korisnik> Korisnik { get; set; }
        public DbSet<Narudzba> Narudzba { get; set; }
        public DbSet<NarudzbaProizvodVarijacija> NarudzbaProizvodVarijacija { get; set; }
        public DbSet<PopustKupon> PopustKupon { get; set; }
        public DbSet<Poslovnica> Poslovnica { get; set; }
        public DbSet<Proizvod> Proizvod { get; set; }
        public DbSet<Proizvodjac> Proizvodjac { get; set; }
        public DbSet<ProizvodVarijacija> ProizvodVarijacija { get; set; }
        public DbSet<Recenzija> Recenzija { get; set; }
        public DbSet<Slika> Slika { get; set; }
        public DbSet<Transakcija> Transakcija { get; set; }
        public DbSet<Novosti> Novosti { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString: @" Server=app.fit.ba,1431;
                                                             Database=p2023_PetShop;
                                                             User ID=p2023_PetShopUser;
                                                             Trusted_Connection=false;
                                                             Password=g$36Qn1n;
                                                             MultipleActiveResultSets=true; ");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NarudzbaProizvodVarijacija>()
                .HasKey(a => new { a.NarudzbaId, a.ProizvodVarijacijaId });

            modelBuilder.Entity<Drzava>().HasData(
                new { Id = 1, Naziv = "Bosna i Hercegovina" },
                new { Id = 2, Naziv = "Srbija" },
                new { Id = 3, Naziv = "Crna Gora" },
                new { Id = 4, Naziv = "Hrvatska" },
                new { Id = 5, Naziv = "Slovenija" },
                new { Id = 6, Naziv = "Makedonija" }
                );

            modelBuilder.Entity<Slika>().HasData(
                new { Id = 1, Putanja = "whiskas.png" },
                new { Id = 2, Putanja = "poslovnica.jpg" },
                new { Id = 3, Putanja = "toys.jpg" },
                new { Id = 4, Putanja = "hrana.jpg" },
                new { Id = 5, Putanja = "odjeca.jpg" },
                new { Id = 6, Putanja = "igracka.jpg" }
               );

            modelBuilder.Entity<Poslovnica>().HasData(
                new { Id = 1, Adresa = "Hase Hasanovica 13", BrojTelefona = "033/456-333", Grad = "Sarajevo" },
                new { Id = 2, Adresa = "Senzacional 55", BrojTelefona = "036/782-411", Grad = "Mostar" },
                new { Id = 3, Adresa = "Tvrdzava 22", BrojTelefona = "032/412-111", Grad = "Travnik" }
               );

            modelBuilder.Entity<Proizvodjac>().HasData(
                new { Id = 1, Naziv = "Mars", DrzavaId = 1 },
                new { Id = 2, Naziv = "Nagger", DrzavaId = 3 },
                new { Id = 3, Naziv = "Royal cani", DrzavaId = 2 },
                new { Id = 4, Naziv = "Purina", DrzavaId = 4 }
               );

            modelBuilder.Entity<Kategorija>().HasData(
                new { Id = 1, Vrsta = "Hrana" },
                new { Id = 2, Vrsta = "Odjeca" },
                new { Id = 3, Vrsta = "Igracke" },
                new { Id = 4, Vrsta = "Ostalo" }
               );

            modelBuilder.Entity<PopustKupon>().HasData(
                new { Id = 1, Kod = "dvajest", Iznos = 20, VrstaPopusta = "posto" },
                new { Id = 2, Kod = "cener", Iznos = 10, VrstaPopusta = "fix" },
                new { Id = 3, Kod = "cvaja", Iznos = 20, VrstaPopusta = "fix" },
                new { Id = 4, Kod = "trisha", Iznos = 30, VrstaPopusta = "posto" }
               );

            modelBuilder.Entity<Novosti>().HasData(
                new { Id = 1, Naslov = "Whiskas proizvodi - snizenje", Text = "Kupute svasta nesto i dobijte puno popusta za svaku kupovinu whiskas prozivoda", Datum = "11/11/2020", SlikaId = 1 },
                new { Id = 2, Naslov = "Otvorenje nove poslovnice u Travniku", Text = "Otvorenje nove poslovnice u Travniku. Pozivamo vas da nam se pridruzite i uvelicate otvorenje do sad najvece poslovnice na cijelom blakanu.", Datum = "12/11/2020", SlikaId = 2 },
                new { Id = 3, Naslov = "Nove igracke stigle u nase trgovine", Text = "Najnoviji asortiman igackica za vase zivotinjice.", Datum = "13/11/2020", SlikaId = 3 }
                );

            modelBuilder.Entity<Korisnik>().HasData(
                new { Id = 1, Ime = "Ajdin", Prezime = "Vreto", Email = "ajdinvreto@edu.fit.ba", Password = "ajdin", BrojTelefona = "060300360", DrzavaId = 1 }
                );

            modelBuilder.Entity<Admin>().HasData(
                new { Id = 1, Ime = "Abdulah", Prezime = "Proho", Email = "abdulahproho@edu.fit.ba", Password = "abdulah" }
                );

            modelBuilder.Entity<Proizvod>().HasData(
                new { Id = 1, Naziv = "Hrana za macke", ProizvodjacId = 1, KategorijaId = 1 },
                new { Id = 2, Naziv = "Igracka za macke", ProizvodjacId = 2, KategorijaId = 3 },
                new { Id = 3, Naziv = "Odjeca za macke", ProizvodjacId = 3, KategorijaId = 2 },
                new { Id = 4, Naziv = "Odjeca za macke", ProizvodjacId = 4, KategorijaId = 2 },
                new { Id = 5, Naziv = "Igracke za macke", ProizvodjacId = 3, KategorijaId = 3 },
                new { Id = 6, Naziv = "Nesto drugo", ProizvodjacId = 2, KategorijaId = 4 },
                new { Id = 7, Naziv = "Hrana za ptice", ProizvodjacId = 1, KategorijaId = 1 },
                new { Id = 8, Naziv = "Igracke za macke", ProizvodjacId = 1, KategorijaId = 3 },
                new { Id = 9, Naziv = "Nesto trece", ProizvodjacId = 4, KategorijaId = 4 },
                new { Id = 10, Naziv = "Hrana za pse", ProizvodjacId = 3, KategorijaId = 1 }
                );

            modelBuilder.Entity<ProizvodVarijacija>().HasData(
                new { Id = 1, Opis = "Hrana za macke whiskas mnogo dobra", Cijena = 10.00, Velicina = "Veliko", SlikaId = 4, ProizvodId = 1 },
                new { Id = 2, Opis = "Igracka za macke iz danske", Cijena = 50.00, Velicina = "Srednje", SlikaId = 6, ProizvodId = 2 },
                new { Id = 3, Opis = "Odjeca za macke armani", Cijena = 20.00, Velicina = "Srednje", SlikaId = 5, ProizvodId = 3 },
                new { Id = 4, Opis = "Odjeca za macke gucci", Cijena = 5.00, Velicina = "Malo", SlikaId = 5, ProizvodId = 4 },
                new { Id = 5, Opis = "Igracke za macke lego", Cijena = 3.00, Velicina = "Malo", SlikaId = 6, ProizvodId = 5 },
                new { Id = 6, Opis = "Nesto drugo aa", Cijena = 4.00, Velicina = "Veliko", SlikaId = 5, ProizvodId = 6 },
                new { Id = 7, Opis = "Hrana za ptice i papagaje", Cijena = 10.00, Velicina = "Veliko", SlikaId = 4, ProizvodId = 7 },
                new { Id = 8, Opis = "Igracke za macke cici mici", Cijena = 26.00, Velicina = "Veliko", SlikaId = 6, ProizvodId = 8 },
                new { Id = 9, Opis = "Nesto trece aaaaa", Cijena = 13.00, Velicina = "Srednje", SlikaId = 5, ProizvodId = 9 },
                new { Id = 10, Opis = "Hrana za pse aw aw", Cijena = 11.00, Velicina = "Malo", SlikaId = 6, ProizvodId = 10 }
                );
        }

    }
}
