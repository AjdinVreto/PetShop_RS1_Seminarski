using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rs1_pet_shop_webapp.Data;
using rs1_pet_shop_webapp.EF;
using rs1_pet_shop_webapp.EntityModels;
using rs1_pet_shop_webapp.ViewModels;
using rs1_pet_shop_webapp.ViewModels.Admin;

namespace rs1_pet_shop_webapp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminProizvodController : Controller
    {
        private MojDbContext dbContext;
        public AdminProizvodController()
        {
            dbContext = new MojDbContext();
        }

        [Route("/Admin/Proizvod", Order = 10, Name = "AdminProizvodIndex")]
        public IActionResult Index()
        {
            var ProizvodiList = dbContext.Proizvod.Select(p => new Proizvod
            {
                Id = p.Id,
                Naziv = p.Naziv,
                Kategorija = p.Kategorija,
                KategorijaId = p.Kategorija.Id,
                Proizvodjac = p.Proizvodjac,
                ProizvodjacId = p.Proizvodjac.Id
            }).ToList();
            var model = new AdminDodajProizvodVM { Title = "Pregled", ProizvodList = ProizvodiList };
            return View("Views/Admin/Proizvod/Index.cshtml", model);
        }

        [Route("/Admin/Proizvod/Dodaj", Order = 5, Name = "AdminProizvodDodaj")]
        [HttpGet("Dodaj")]
        public IActionResult Dodaj()
        {

            var proizvodjaci = dbContext.Proizvodjac.Select(p => new Proizvodjac { Id = p.Id, Naziv = p.Naziv, Drzava = p.Drzava, DrzavaId = p.Drzava.Id }).ToList();
            var kategorije = dbContext.Kategorija.Select(k => new Kategorija { Id = k.Id, Vrsta = k.Vrsta }).ToList();

            var model = new AdminDodajProizvodVM { Title = "Dodaj", ProizvodjacList = proizvodjaci, KategorijaList = kategorije };
            return View("Views/Admin/Proizvod/Dodaj.cshtml", model);
        }

        [Route("/Admin/Proizvod/Dodaj", Order = 5, Name = "AdminProizvodDodaj")]
        [HttpPost]
        public IActionResult Dodaj(AdminDodajProizvodVM model)
        {

            if (!ModelState.IsValid)
            {
                var proizvodjaci = dbContext.Proizvodjac.Select(p => new Proizvodjac { Id = p.Id, Naziv = p.Naziv, Drzava = p.Drzava, DrzavaId = p.Drzava.Id }).ToList();
                model.ProizvodjacList = proizvodjaci;

                var kategorije = dbContext.Kategorija.Select(k => new Kategorija { Id = k.Id, Vrsta = k.Vrsta }).ToList();
                model.KategorijaList = kategorije;

                model.Title = "Proizvodi";

                return View("Views/Admin/Proizvod/Uredi.cshtml", model);
            }

            var proizvod = new Proizvod
            {
                Naziv = model.Naziv,
                ProizvodjacId = model.ProizvodjacId,
                KategorijaId = model.KategorijaId
            };

            dbContext.Add(proizvod);

            dbContext.SaveChanges();
            return RedirectToRoute("AdminProizvodIndex");
        }

        [Route("/Admin/Proizvod/Obrisi", Order = 5, Name = "AdminProizvodObrisi")]
        [HttpPost]
        public async Task<IActionResult> Obrisi(int id)
        {
            try
            {
                Proizvod proizvod = dbContext.Proizvod.Find(id);

                dbContext.Proizvod.Attach(proizvod);
                dbContext.Proizvod.Remove(proizvod);

                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                if (!dbContext.Proizvod.Any(i => i.Id == id))
                {
                    return NotFound();
                }
            }
            return RedirectToRoute("AdminProizvodIndex");
        }

        [Route("/Admin/Proizvod/Uredi", Order = 5, Name = "AdminProizvodUredi")]
        [HttpGet]
        public IActionResult Uredi(int id)
        {
            var proizvod = dbContext.Proizvod.Find(id);
            if (proizvod == null)
            {
                return NotFound();
            }

            var proizvodjaci = dbContext.Proizvodjac.Select(p => new Proizvodjac { Id = p.Id, Naziv = p.Naziv, Drzava = p.Drzava, DrzavaId = p.Drzava.Id }).ToList();
            var kategorije = dbContext.Kategorija.Select(k => new Kategorija { Id = k.Id, Vrsta = k.Vrsta }).ToList();

            var model = new AdminDodajProizvodVM
            {
                Title = "Uredi",
                Id = proizvod.Id,
                Naziv = proizvod.Naziv,
                Kategorija = proizvod.Kategorija,
                KategorijaId = proizvod.KategorijaId,
                Proizvodjac = proizvod.Proizvodjac,
                ProizvodjacId = proizvod.ProizvodjacId,
                ProizvodjacList = proizvodjaci,
                KategorijaList = kategorije
            };

            return View("Views/Admin/Proizvod/Uredi.cshtml", model);
        }
        [Route("/Admin/Proizvod/Uredi", Name = "AdminProizvodUredi")]
        [HttpPost]
        public IActionResult Uredi(AdminDodajProizvodVM model)
        {
            if (!ModelState.IsValid)
            {
                var proizvodjaci = dbContext.Proizvodjac.Select(p => new Proizvodjac { Id = p.Id, Naziv = p.Naziv, Drzava = p.Drzava, DrzavaId = p.Drzava.Id }).ToList();
                model.ProizvodjacList = proizvodjaci;

                var kategorije = dbContext.Kategorija.Select(k => new Kategorija { Id = k.Id, Vrsta = k.Vrsta }).ToList();
                model.KategorijaList = kategorije;

                model.Title = "Uredi";
                return View("Views/Admin/Proizvod/Uredi.cshtml", model);
            }

            var proizvod = dbContext.Proizvod.Find(model.Id);

            if (proizvod == null)
            {
                return NotFound();
            }

            proizvod.Naziv = model.Naziv;
            proizvod.KategorijaId = model.KategorijaId;
            proizvod.ProizvodjacId = model.ProizvodjacId;

            dbContext.Update(proizvod);

            dbContext.SaveChanges();
            return RedirectToRoute("AdminProizvodIndex");
        }
    }
}