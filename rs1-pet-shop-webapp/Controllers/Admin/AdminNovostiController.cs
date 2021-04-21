using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using rs1_pet_shop_webapp.EF;
using rs1_pet_shop_webapp.EntityModels;
using rs1_pet_shop_webapp.ViewModels.Admin;

namespace rs1_pet_shop_webapp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminNovostiController : Controller
    {
        private MojDbContext dbContext;
        private IWebHostEnvironment webHostEnvironment;
        public AdminNovostiController(IWebHostEnvironment hostEnvironment)
        {
            dbContext = new MojDbContext();
            webHostEnvironment = hostEnvironment;
        }

        [Route("/Admin/Novosti", Name = "AdminNovostiIndex")]
        public IActionResult Index()
        {
            List<AdminNovostiVM> novosti = dbContext.Novosti.Select(x => new AdminNovostiVM
            {
                Id = x.Id,
                Naslov = x.Naslov,
                Text = x.Text,
                Datum = x.Datum,
                Slika = x.Slika.Putanja
            }).OrderByDescending(x => x.Id).ToList();

            var model = new AdminNovostiListVM { Title = "Pregled", NovostiList = novosti };
            return View("Views/Admin/Novosti/Index.cshtml", model);
        }

        [Route("/Admin/Novosti/Dodaj", Name = "AdminNovostiDodaj")]
        [HttpGet]
        public IActionResult Dodaj()
        {
            var model = new AdminNovostiVM { Title = "Dodaj" };
            return View("Views/Admin/Novosti/Dodaj.cshtml", model);
        }

        [Route("/Admin/Novosti/Dodaj", Name = "AdminNovostiDodaj")]
        [HttpPost]
        public async Task<IActionResult> Dodaj(AdminNovostiVM model)
        {

            if (!ModelState.IsValid)
            {
                model.Title = "Dodaj";
                return View("Views/Admin/Novosti/Dodaj.cshtml", model);
            }

            string uniqueFileName = UploadedFile(model);
            Slika slika = new Slika
            {
                Putanja = uniqueFileName
            };
            dbContext.Add(slika);
            await dbContext.SaveChangesAsync();

            Novosti novost = new Novosti
            {
                Naslov = model.Naslov,
                Text = model.Text,
                SlikaId = slika.Id,
                Datum = DateTime.Now.ToString("dd\\/MM\\/yyyy")
            };
            dbContext.Add(novost);
            await dbContext.SaveChangesAsync();

            return RedirectToRoute("AdminNovostiIndex");
        }
        [Route("/Admin/Novosti/Uredi", Name = "AdminNovostiUredi")]
        [HttpGet]
        public IActionResult Uredi(int id)
        {
            var model = dbContext.Novosti.Select(x => new AdminNovostiVM
            {
                Title = "Uredi",
                Id = x.Id,
                Naslov = x.Naslov,
                Text = x.Text,
                Datum = x.Datum,
                Slika = x.Slika.Putanja
            }).Where(x => x.Id == id).FirstOrDefault();

            return View("Views/Admin/Novosti/Uredi.cshtml", model);
        }
        [Route("/Admin/Novosti/Uredi", Name = "AdminNovostiUredi")]
        [HttpPost]
        public async Task<IActionResult> Uredi(AdminNovostiVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Title = "Dodaj";
                return View("Views/Admin/Novosti/Uredi.cshtml", model);
            }

            Novosti novost = dbContext.Novosti.Find(model.Id);

            novost.Naslov = model.Naslov;
            novost.Text = model.Text;

            if (model.SlikaFile != null)
            {
                string uniqueFileName = UploadedFile(model);
                Slika slika = new Slika
                {
                    Putanja = uniqueFileName
                };
                dbContext.Add(slika);
                await dbContext.SaveChangesAsync();
                novost.SlikaId = slika.Id;
            }

            dbContext.Update(novost);
            await dbContext.SaveChangesAsync();

            return RedirectToRoute("AdminNovostiIndex");
        }
        [Route("/Admin/Novosti/Obrisi", Name = "AdminNovostiObrisi")]
        [HttpPost]
        public async Task<IActionResult> Obrisi(int id)
        {
            try
            {
                Novosti novost = dbContext.Novosti.Find(id);
                dbContext.Novosti.Attach(novost);
                dbContext.Novosti.Remove(novost);

                Slika slika = new Slika() { Id = novost.SlikaId };
                dbContext.Slika.Attach(slika);
                dbContext.Slika.Remove(slika);

                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                if (!dbContext.Novosti.Any(i => i.Id == id))
                {
                    return NotFound();
                }
            }
            return RedirectToRoute("AdminNovostiIndex");
        }

        private string UploadedFile(AdminNovostiVM model)
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