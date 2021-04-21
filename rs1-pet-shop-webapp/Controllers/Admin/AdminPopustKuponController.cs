using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using rs1_pet_shop_webapp.EF;
using rs1_pet_shop_webapp.EntityModels;
using rs1_pet_shop_webapp.ViewModels.Admin;

namespace rs1_pet_shop_webapp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminPopustKuponController : Controller
    {
        private MojDbContext dbContext;
        public AdminPopustKuponController()
        {
            dbContext = new MojDbContext();
        }

        [Route("/Admin/PopustKupon", Name = "AdminKuponIndex")]
        [HttpGet]
        public IActionResult Index()
        {
            var kuponi = dbContext.PopustKupon.Select(k => new PopustKupon { Id = k.Id, Iznos = k.Iznos, Kod = k.Kod, VrstaPopusta = k.VrstaPopusta }).ToList();
            var model = new AdminPopustKuponVM { Title = "Popust kupon", PopustKuponList = kuponi };
            return View("Views/Admin/PopustKuponi/Index.cshtml", model);
        }

        [Route("/Admin/PopustKupon/Obrisi", Name = "AdminKuponObrisi")]
        [HttpPost]
        public IActionResult Obrisi(int id)
        {
            var kupon = new PopustKupon { Id = id };

            dbContext.PopustKupon.Attach(kupon);
            dbContext.PopustKupon.Remove(kupon);
            dbContext.SaveChanges();

            return View("Views/Admin/PopustKuponi/Index.cshtml");
        }

        [Route("/Admin/PopustKupon/Dodaj", Name = "AdminKuponDodaj")]
        [HttpGet]
        public IActionResult Dodaj()
        {
            return View("Views/Admin/PopustKuponi/Dodaj.cshtml");
        }
        [Route("/Admin/PopustKupon/Dodaj", Name = "AdminKuponDodaj")]
        [HttpPost]
        public IActionResult Dodaj(AdminPopustKuponVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Title = "Popust kupon";
                return View("Views/Admin/PopustKuponi/Dodaj.cshtml");
            }

            var kupon = new PopustKupon
            {
                Iznos = model.Iznos,
                Kod = model.Kod,
                VrstaPopusta = model.VrstaPopusta
            };
            dbContext.Add(kupon);
            dbContext.SaveChanges();

            return RedirectToRoute("AdminKuponIndex");
        }

        [Route("/Admin/PopustKupon/Uredi", Name = "AdminKuponUredi")]
        [HttpGet]
        public IActionResult Uredi(int id)
        {
            var kupon = dbContext.PopustKupon.Find(id);
            
            if(kupon == null)
            {
                return NotFound();
            }

            var model = new AdminPopustKuponVM
            {
                Id = kupon.Id,
                Iznos = kupon.Iznos,
                Kod = kupon.Kod,
                VrstaPopusta = kupon.VrstaPopusta,
                Title = "Popust kupon"
            };

            return View("Views/Admin/PopustKuponi/Uredi.cshtml", model);
        }
        [Route("/Admin/PopustKupon/Uredi", Name = "AdminKuponUredi")]
        [HttpPost]
        public IActionResult Uredi(AdminPopustKuponVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Title = "Popust kupon";
                return View("Views/Admin/PopustKuponi/Uredi.cshtml", model);
            }

            var kupon = dbContext.PopustKupon.Find(model.Id);
            kupon.Iznos = model.Iznos;
            kupon.Kod = model.Kod;
            kupon.VrstaPopusta = model.VrstaPopusta;

            dbContext.Update(kupon);
            dbContext.SaveChanges();

            return RedirectToRoute("AdminKuponIndex");
        }
    }
}