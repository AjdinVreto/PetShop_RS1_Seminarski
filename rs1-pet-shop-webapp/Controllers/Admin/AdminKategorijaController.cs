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
    public class AdminKategorijaController : Controller
    {
        private MojDbContext dbContext;
        public AdminKategorijaController()
        {
            dbContext = new MojDbContext();
        }

        [Route("/Admin/Kategorija", Order = 10, Name = "AdminKategorijaIndex")]
        public IActionResult Index()
        {
            List<Kategorija> KategorijaList = dbContext.Kategorija.Select(kategorija => new Kategorija { Id = kategorija.Id, Vrsta = kategorija.Vrsta }).ToList();

            var model = new AdminKategorijaVM { Title = "Pregled", KategorijaList = KategorijaList };
            return View("Views/Admin/Kategorija.cshtml", model);
        }

        [Route("/Admin/Kategorija/Dodaj", Order = 5, Name = "AdminKategorijaDodaj")]
        [HttpPost]
        public async Task<IActionResult> Dodaj(AdminKategorijaVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Title = "Dodaj";
                return View("Views/Admin/Kategorija.cshtml", model);
            }
            Kategorija Kategorija = new Kategorija { Vrsta = model.Vrsta };
            await dbContext.Kategorija.AddAsync(Kategorija);
            await dbContext.SaveChangesAsync();

            return RedirectToRoute("AdminKategorijaIndex");
        }

        [Route("/Admin/Kategorija/Obrisi", Order = 5, Name = "AdminKategorijaObrisi")]
        [HttpPost]
        public async Task<IActionResult> Obrisi(int id)
        {
            try
            {
                Kategorija kategorija = dbContext.Kategorija.Find(id);

                dbContext.Kategorija.Attach(kategorija);
                dbContext.Kategorija.Remove(kategorija);

                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                if (!dbContext.Kategorija.Any(i => i.Id == id))
                {
                    return NotFound();
                }
            }
            return RedirectToRoute("AdminKategorijaIndex");
        }

        [Route("/Admin/Kategorija/Uredi", Order = 5, Name = "AdminKategorijaUredi")]
        [HttpPost]
        public async Task<IActionResult> Uredi(AdminKategorijaVM model)
        {
            if (ModelState.IsValid)
            {
                Kategorija kategorija = dbContext.Kategorija.Find(model.Id);

                kategorija.Vrsta = model.Vrsta;

                dbContext.Update(kategorija);
                await dbContext.SaveChangesAsync();
            }
            return RedirectToRoute("AdminKategorijaIndex");
        }
    }
}