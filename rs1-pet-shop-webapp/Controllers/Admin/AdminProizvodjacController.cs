using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using rs1_pet_shop_webapp.EF;
using rs1_pet_shop_webapp.EntityModels;
using rs1_pet_shop_webapp.ViewModels.Admin;

namespace rs1_pet_shop_webapp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminProizvodjacController : Controller
    {
        private MojDbContext dbContext;

        public AdminProizvodjacController()
        {
            dbContext = new MojDbContext();
        }

        [Route("/Admin/Proizvodjac", Name = "AdminProizvodjacIndex")]
        public IActionResult Index()
        {
            var proizvodjaci = dbContext.Proizvodjac.Select(p => new Proizvodjac
            {
                Id = p.Id,
                Naziv = p.Naziv,
                DrzavaId = p.Drzava.Id,
                Drzava = p.Drzava
            }).ToList();

            var model = new AdminProizvodjacVM { Title = "Pregled", ProizvodjaciList = proizvodjaci };

            return View("Views/Admin/Proizvodjac/Index.cshtml", model);
        }

        [Route("/Admin/Proizvodjac/Dodaj", Name = "AdminProizvodjacDodaj")]
        [HttpGet]
        public IActionResult Dodaj()
        {
            var drzave = dbContext.Drzava.Select(d => new Drzava { Id = d.Id, Naziv = d.Naziv }).ToList();
            var model = new AdminProizvodjacVM { Title = "Dodaj", DrzavaList = drzave };
            return View("Views/Admin/Proizvodjac/Dodaj.cshtml", model);
        }
        [Route("/Admin/Proizvodjac/Dodaj", Name = "AdminProizvodjacDodaj")]
        [HttpPost]
        public async Task<IActionResult> Dodaj(AdminProizvodjacVM model)
        {
            if (!ModelState.IsValid)
            {
                var drzave = dbContext.Drzava.Select(d => new Drzava { Id = d.Id, Naziv = d.Naziv }).ToList();
                model.Title = "Dodaj";
                model.DrzavaList = drzave;
                return View("Views/Admin/Proizvodjac/Dodaj.cshtml", model);
            }

            var proizvodjac = new Proizvodjac { Naziv = model.Naziv, DrzavaId = model.DrzavaId };
            dbContext.Add(proizvodjac);
            await dbContext.SaveChangesAsync();

            return RedirectToRoute("AdminProizvodjacIndex");
        }
        [Route("/Admin/Proizvodjac/Obrisi", Name = "AdminProizvodjacObrisi")]
        [HttpPost]
        public async Task<IActionResult> Obrisi(int id)
        {
            try
            {
                Proizvodjac proizvodjac = dbContext.Proizvodjac.Find(id);

                dbContext.Proizvodjac.Attach(proizvodjac);
                dbContext.Proizvodjac.Remove(proizvodjac);

                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                if (!dbContext.Proizvodjac.Any(i => i.Id == id))
                {
                    return NotFound();
                }
            }
            return RedirectToRoute("AdminProizvodjacIndex");
        }

        [Route("/Admin/Proizvodjac/Uredi", Name = "AdminProizvodjacUredi")]
        [HttpGet]
        public IActionResult Uredi(int id)
        {
            var proizvodjac = dbContext.Proizvodjac.Select(p => new Proizvodjac { Id = p.Id, Drzava = p.Drzava, DrzavaId = p.DrzavaId, Naziv = p.Naziv }).Where(p => p.Id == id).FirstOrDefault();
            if (proizvodjac == null)
            {
                return NotFound();
            }

            var drzave = dbContext.Drzava.Select(d => new Drzava { Id = d.Id, Naziv = d.Naziv }).ToList();
            var model = new AdminProizvodjacVM
            {
                DrzavaList = drzave,
                Title = "Uredi",
                Id = proizvodjac.Id,
                Naziv = proizvodjac.Naziv,
                Drzava = proizvodjac.Drzava.Naziv,
                DrzavaId = proizvodjac.Drzava.Id,
            };

            return View("Views/Admin/Proizvodjac/Uredi.cshtml", model);
        }
        [Route("/Admin/Proizvodjac/Uredi", Name = "AdminProizvodjacUredi")]
        [HttpPost]
        public async Task<IActionResult> Uredi(AdminProizvodjacVM model)
        {
            if (!ModelState.IsValid)
            {
                var drzave = dbContext.Drzava.Select(d => new Drzava { Id = d.Id, Naziv = d.Naziv }).ToList();
                model.DrzavaList = drzave;
                return View("Views/Admin/Proizvodjac/Uredi.cshtml", model);
            }

            var proizvodjac = dbContext.Proizvodjac.Select(p => new Proizvodjac { Id = p.Id, Naziv = p.Naziv, Drzava = p.Drzava, DrzavaId = p.DrzavaId }).Where(p=>p.Id==model.Id).FirstOrDefault();
            if (proizvodjac == null) return NotFound();

            proizvodjac.Naziv = model.Naziv;
            proizvodjac.DrzavaId = model.DrzavaId;
            proizvodjac.Drzava = dbContext.Drzava.Where(d => d.Id == model.DrzavaId).FirstOrDefault();
            
            Console.WriteLine($"DRZAVA{model.Drzava} {model.DrzavaId}");
            Console.WriteLine($"DRZAVA 2 {proizvodjac.Drzava.Naziv} {proizvodjac.DrzavaId}");

            dbContext.Update(proizvodjac);
            await dbContext.SaveChangesAsync();

            return RedirectToRoute("AdminProizvodjacIndex");
        }
    }
}