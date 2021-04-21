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
    [Authorize(Roles = "Admin")]
    public class AdminNarudzbaController : Controller
    {
        private MojDbContext dbContext;
        public AdminNarudzbaController()
        {
            dbContext = new MojDbContext();
        }

        [Route("/Admin/Narudzba", Name = "AdminNarudzbaIndex")]
        [HttpGet]
        public IActionResult Index()
        {
            var narudzbe = dbContext.Narudzba.Select(n => new Narudzba
            {
                Id = n.Id,
                Adresa = n.Adresa,
                AdresaId = n.AdresaId,
                Aktivna = n.Aktivna,
                Datum = n.Datum,
                Korisnik = n.Korisnik,
                KorisnikId = n.KorisnikId,
                Zavrsena = n.Zavrsena
            }).OrderByDescending(p=>p.Id).ToList();

            var model = new AdminNarudzbaVM
            {
                Title = "Narudzbe",
                NarudzbaList = narudzbe
            };
            return View("Views/Admin/Narudzba/Index.cshtml", model);
        }

        [Route("/Admin/Narudzba/Pregled", Name = "AdminNarudzbaPregled")]
        [HttpGet]
        public IActionResult Pregled(int id)
        {
            var narudzba = dbContext.Narudzba.Find(id);
            if (narudzba == null)
            {
                return NotFound();

            }

            var varijacije = dbContext.NarudzbaProizvodVarijacija
                .Include(p=>p.ProizvodVarijacija.Proizvod)
                .Where(p => p.NarudzbaId == narudzba.Id)
                .Select(v => new NarudzbaProizvodVarijacija
                {
                    Kolicina = v.Kolicina,
                    NarudzbaId = v.NarudzbaId,
                    ProizvodVarijacijaId = v.ProizvodVarijacijaId,
                    ProizvodVarijacija = v.ProizvodVarijacija,
                }).ToList();

            var model = new AdminNarudzbaVM
            {
                Title = "Pregled",
                Id = narudzba.Id,
                Korisnik = narudzba.Korisnik,
                KorisnikId = narudzba.KorisnikId,
                Adresa = narudzba.Adresa,
                AdresaId = narudzba.AdresaId,
                Aktivna = narudzba.Aktivna,
                Datum = narudzba.Datum,
                Zavrsena = narudzba.Zavrsena,
                VarijacijeList = varijacije,
            };

            return View("Views/Admin/Narudzba/Pregled.cshtml", model);
        }
    }
}