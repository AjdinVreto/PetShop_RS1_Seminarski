using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rs1_pet_shop_webapp.EF;
using rs1_pet_shop_webapp.EntityModels;
using rs1_pet_shop_webapp.ViewModels.Admin;

namespace rs1_pet_shop_webapp.Controllers
{
    [Authorize(Roles="Admin")]
    public class AdminKorisnikController : Controller
    {
        private MojDbContext dbContext;
        public AdminKorisnikController()
        {
            dbContext = new MojDbContext();
        }

        [Route("/Admin/Korisnik", Name = "AdminKorisnikIndex")]
        public IActionResult Index()
        {

            var korisnici = dbContext.Korisnik.Select(k => new Korisnik
            {
                Id = k.Id,
                BrojTelefona = k.BrojTelefona,
                Drzava = k.Drzava,
                DrzavaId = k.DrzavaId,
                Email = k.Email,
                Ime = k.Ime,
                Prezime = k.Prezime
            }).ToList();

            var model = new AdminKorisnikVM
            {
                Title = "Korisnici",
                KorisnikList = korisnici
            };
            return View("Views/Admin/Korisnik/Index.cshtml", model);
        }

        [Route("/Admin/Korisnik/Pregled", Name = "AdminKorisnikPregled")]
        [HttpGet]
        public IActionResult Pregled(int id)
        {
            var korisnik = dbContext.Korisnik.Find(id);
            if (korisnik == null)
            {
                return NotFound();
            }
            korisnik.Drzava = dbContext.Drzava.Find(korisnik.DrzavaId);

            var narudzbe = dbContext.Narudzba
                //.Include(p=>p.Adresa)
                .Where(n => n.KorisnikId == korisnik.Id)
                .Select(n => new Narudzba
                {
                    Id = n.Id,
                    AdresaId = n.AdresaId,
                    Adresa = n.Adresa,
                    Aktivna = n.Aktivna,
                    Datum = n.Datum,
                    Korisnik = n.Korisnik,
                    KorisnikId = n.KorisnikId,
                    Zavrsena = n.Zavrsena
                }
                )
                .ToList();

            var model = new AdminKorisnikVM
            {
                Title = "Pregled",
                Ime = korisnik.Ime,
                Prezime = korisnik.Prezime,
                BrojTelefona = korisnik.BrojTelefona,
                Drzava = korisnik.Drzava,
                DrzavaId = korisnik.DrzavaId,
                Email = korisnik.Email,
                Id = korisnik.Id,
                Password = korisnik.Password,
                BrojNarudzbi = narudzbe.Count(),
                NarudzbaList = narudzbe
            };

            return View("Views/Admin/Korisnik/Pregled.cshtml", model);
        }
    }
}