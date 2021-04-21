using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using rs1_pet_shop_webapp.Data;
using rs1_pet_shop_webapp.EF;
using rs1_pet_shop_webapp.EntityModels;
using rs1_pet_shop_webapp.SignalR;
using rs1_pet_shop_webapp.ViewModels;
using rs1_pet_shop_webapp.ViewModels.Admin;

namespace rs1_pet_shop_webapp.Controllers
{
    [Authorize(Roles="Admin")]
    public class AdminVarijacijaController : Controller
    {
        private readonly MojDbContext dbContext;
        private IWebHostEnvironment webHostEnvironment;
        IHubContext<MyHub> _hubContext;

        public AdminVarijacijaController(IWebHostEnvironment hostEnvironment, Microsoft.AspNetCore.SignalR.IHubContext<MyHub> hubContext)
        {
            webHostEnvironment = hostEnvironment;
            dbContext = new MojDbContext();
            this._hubContext = hubContext;
        }

        [Route("/Admin/Varijacija", Order = 10, Name = "AdminVarijacijaIndex")]
        public IActionResult Index()
        {
            var VarijacijaList = dbContext.ProizvodVarijacija.Select(v => new ProizvodVarijacija
            {
                Id = v.Id,
                Cijena = v.Cijena,
                Opis = v.Opis,
                Proizvod = v.Proizvod,
                ProizvodId = v.ProizvodId,
                Slika = v.Slika,
                SlikaId = v.SlikaId,
                Velicina = v.Velicina
            }).ToList();
            var model = new AdminVarijacijaVM { Title = "Pregled", VarijacijaList = VarijacijaList };
            return View("Views/Admin/Varijacija/Index.cshtml", model);
        }

        [Route("/Admin/Varijacija/Dodaj", Order = 5, Name = "AdminVarijacijaDodaj")]
        [HttpGet]
        public IActionResult Dodaj()
        {

            var proizvodi = dbContext.Proizvod.Select(p => new Proizvod
            {
                Id = p.Id,
                Kategorija = p.Kategorija,
                KategorijaId = p.Kategorija.Id,
                Naziv = p.Naziv,
                Proizvodjac = p.Proizvodjac,
                ProizvodjacId = p.Proizvodjac.Id
            }).ToList();

            var model = new AdminVarijacijaVM { Title = "Dodaj", ProizvodList = proizvodi };
            return View("Views/Admin/Varijacija/Dodaj.cshtml", model);
        }
        [Route("/Admin/Varijacija/Dodaj", Order = 5, Name = "AdminVarijacijaDodaj")]
        [HttpPost]
        public async Task<IActionResult> Dodaj(AdminVarijacijaVM model)
        {

            if (!ModelState.IsValid /*|| model.SlikaFile == null*/)
            {
                //    if(model.SlikaFile == null)
                //    {
                //        ModelState.AddModelError("Slika", "Slika je obavezna");
                //    }

                var proizvodi = dbContext.Proizvod.Select(p => new Proizvod
                {
                    Id = p.Id,
                    Kategorija = p.Kategorija,
                    KategorijaId = p.Kategorija.Id,
                    Naziv = p.Naziv,
                    Proizvodjac = p.Proizvodjac,
                    ProizvodjacId = p.Proizvodjac.Id
                }).ToList();
                model.ProizvodList = proizvodi;

                model.Title = "Varijacija";

                return View("Views/Admin/Varijacija/Dodaj.cshtml", model);
            }

            string uniqueFileName = UploadedFile(model);
            Slika slika = new Slika
            {
                Putanja = uniqueFileName
            };
            dbContext.Add(slika);
            await dbContext.SaveChangesAsync();

            var varijacija = new ProizvodVarijacija
            {
                Id = model.Id,
                Cijena = model.Cijena,
                Opis = model.Opis,
                Velicina = model.Velicina,
                ProizvodId = model.ProizvodId,
                SlikaId = model.SlikaId,
                Slika = slika
            };

            dbContext.Add(varijacija);

            dbContext.SaveChanges();

            var v = dbContext.ProizvodVarijacija.Include(x => x.Proizvod).Include(x => x.Proizvod.Proizvodjac).Where(x => x.Id == varijacija.Id).FirstOrDefault();

            _hubContext.Clients.All.SendAsync("ReceiveMessage", "korisniče", v.Proizvod.Naziv + " " + "proizvodjača" + " " + v.Proizvod.Proizvodjac.Naziv);
            return RedirectToRoute("AdminVarijacijaIndex");
        }

        [Route("/Admin/Varijacija/Obrisi", Order = 5, Name = "AdminVarijacijaObrisi")]
        [HttpPost]
        public async Task<IActionResult> Obrisi(int id)
        {
            try
            {
                ProizvodVarijacija varijacija = dbContext.ProizvodVarijacija.Find(id);

                dbContext.ProizvodVarijacija.Attach(varijacija);
                dbContext.ProizvodVarijacija.Remove(varijacija);

                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                if (!dbContext.ProizvodVarijacija.Any(i => i.Id == id))
                {
                    return NotFound();
                }
            }
            return RedirectToRoute("AdminVarijacijaIndex");
        }

        [Route("/Admin/Varijacija/Uredi", Order = 5, Name = "AdminVarijacijaUredi")]
        [HttpGet]
        public IActionResult Uredi(int id)
        {
            var varijacija = dbContext.ProizvodVarijacija.Where(v => v.Id == id).Select(v => new ProizvodVarijacija
            {
                Id = v.Id,
                Cijena = v.Cijena,
                Opis = v.Opis,
                Proizvod = v.Proizvod,
                ProizvodId = v.Proizvod.Id,
                Slika = v.Slika,
                SlikaId = v.SlikaId,
                Velicina = v.Velicina
            }).FirstOrDefault();
            if (varijacija == null)
            {
                return NotFound();
            }

            var proizvodi = dbContext.Proizvod.Select(p => new Proizvod
            {
                Id = p.Id,
                Kategorija = p.Kategorija,
                KategorijaId = p.Kategorija.Id,
                Naziv = p.Naziv,
                Proizvodjac = p.Proizvodjac,
                ProizvodjacId = p.Proizvodjac.Id
            }).ToList();

            var model = new AdminVarijacijaVM
            {
                Title = "Uredi",
                Id = varijacija.Id,
                ProizvodList = proizvodi,
                Cijena = varijacija.Cijena,
                Opis = varijacija.Opis,
                Proizvod = varijacija.Proizvod,
                ProizvodId = varijacija.Proizvod.Id,
                SlikaId = varijacija.SlikaId,
                Slika = varijacija.Slika.Putanja,
                Velicina = varijacija.Velicina
            };
            //if (varijacija.Slika != null)
            //{
            //    model.Slika = varijacija.Slika.Putanja;
            //}

            return View("Views/Admin/Varijacija/Uredi.cshtml", model);
        }
        [Route("/Admin/Varijacija/Uredi", Name = "AdminVarijacijaUredi")]
        [HttpPost]
        public async Task<IActionResult> Uredi(AdminVarijacijaVM model)
        {
            Slika slika;
            ProizvodVarijacija varijacija = dbContext.ProizvodVarijacija.Find(model.Id);
            if (!ModelState.IsValid)
            {
                slika = dbContext.Slika.Find(varijacija.SlikaId);
                var proizvodi = dbContext.Proizvod.Select(p => new Proizvod
                {
                    Id = p.Id,
                    Kategorija = p.Kategorija,
                    KategorijaId = p.Kategorija.Id,
                    Naziv = p.Naziv,
                    Proizvodjac = p.Proizvodjac,
                    ProizvodjacId = p.Proizvodjac.Id
                }).ToList();
                //.Select(v => new ProizvodVarijacija { Id = v.Id, Cijena = v.Cijena, Opis = v.Opis, Proizvod = v.Proizvod, ProizvodId =v.ProizvodId, Velicina = v.Velicina, Slika = v.Slika, SlikaId = v.SlikaId }).ToList();
                model.ProizvodList = proizvodi;
                model.Slika = slika.Putanja;

                model.Title = "Uredi";
                return View("Views/Admin/Varijacija/Uredi.cshtml", model);
            }

            if (varijacija == null)
            {
                return NotFound();
            }

            if (model.SlikaFile != null)
            {
                string uniqueFileName = UploadedFile(model);
                slika = new Slika
                {
                    Putanja = uniqueFileName
                };

                dbContext.Add(slika);
                await dbContext.SaveChangesAsync();

                varijacija.SlikaId = slika.Id;
                varijacija.Slika = slika;
            }


            varijacija.Opis = model.Opis;
            varijacija.Velicina = model.Velicina;
            varijacija.Cijena = model.Cijena;
            varijacija.ProizvodId = model.ProizvodId;

            dbContext.Update(varijacija);

            dbContext.SaveChanges();
            return RedirectToRoute("AdminVarijacijaIndex");
        }
        private string UploadedFile(AdminVarijacijaVM model)
        {
            string uniqueFileName = null;

            if (model.SlikaFile != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.SlikaFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.SlikaFile.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}