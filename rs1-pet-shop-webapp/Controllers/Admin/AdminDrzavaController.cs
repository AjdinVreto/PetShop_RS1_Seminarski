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
    public class AdminDrzavaController : Controller
    {
        private MojDbContext dbContext;
        public AdminDrzavaController()
        {
            dbContext = new MojDbContext();
        }

        [Route("/Admin/Drzava", Order = 10, Name = "AdminDrzavaIndex")]
        public IActionResult Index()
        {
            List<Drzava> DrzavaList = dbContext.Drzava.Select(drzava => new Drzava { Id = drzava.Id, Naziv = drzava.Naziv }).ToList();

            var model = new AdminDrzavaVM { Title = "Pregled", DrzavaList = DrzavaList };
            return View("Views/Admin/Drzava.cshtml", model);
        }

        [Route("/Admin/Drzava/Dodaj", Order = 5, Name = "AdminDrzavaDodaj")]
        [HttpPost]
        public async Task<IActionResult> Dodaj(AdminDrzavaVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Title = "Dodaj";
                return View("Views/Admin/Drzava.cshtml", model);
            }
            Drzava drzava = new Drzava { Naziv = model.Naziv };
            await dbContext.Drzava.AddAsync(drzava);
            await dbContext.SaveChangesAsync();

            return RedirectToRoute("AdminDrzavaIndex");
        }

        [Route("/Admin/Drzava/Obrisi", Order = 5, Name = "AdminDrzavaObrisi")]
        [HttpPost]
        public async Task<IActionResult> Obrisi(int id)
        {
            try
            {
                Drzava drzava = dbContext.Drzava.Find(id);

                dbContext.Drzava.Attach(drzava);
                dbContext.Drzava.Remove(drzava);

                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                if (!dbContext.Drzava.Any(i => i.Id == id))
                {
                    return NotFound();
                }
            }
            return RedirectToRoute("AdminDrzavaIndex");
        }

        [Route("/Admin/Drzava/Uredi", Order = 5, Name = "AdminDrzavaUredi")]
        [HttpPost]
        public async Task<IActionResult> Uredi(AdminDrzavaVM model)
        {
            if (ModelState.IsValid)
            {
                Drzava drzava = dbContext.Drzava.Find(model.Id);

                drzava.Naziv = model.Naziv;

                dbContext.Update(drzava);
                await dbContext.SaveChangesAsync();
            }
            return RedirectToRoute("AdminDrzavaIndex");
        }
    }
}