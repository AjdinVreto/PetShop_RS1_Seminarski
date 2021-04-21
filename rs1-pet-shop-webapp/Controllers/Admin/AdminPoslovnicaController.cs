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
    public class AdminPoslovnicaController : Controller
    {
        private MojDbContext dbContext;
        public AdminPoslovnicaController() {
            dbContext = new MojDbContext();
        }

        [Route("/Admin/Poslovnica", Name = "AdminPoslovnicaIndex")]
        [HttpGet]
        public IActionResult Index()
        {
            List<Poslovnica> poslovnice = dbContext.Poslovnica.Select(p => new Poslovnica
            {
                Id = p.Id,
                BrojTelefona = p.BrojTelefona,
                Grad = p.Grad,
                Adresa = p.Adresa
            }).ToList();

            AdminPoslovnicaVM model = new AdminPoslovnicaVM
            {
                Title = "Poslovnice",
                PoslovnicaList = poslovnice
            };

            return View("Views/Admin/Poslovnice/Index.cshtml", model);
        }

        [Route("/Admin/Poslovnica/Obrisi", Name = "AdminPoslovnicaObrisi")]
        [HttpPost]
        public IActionResult Obrisi(int id)
        {
            var poslovnica = dbContext.Poslovnica.Find(id);

            if (poslovnica == null)
            {
                return NotFound();
            }

            //dbContext.Poslovnica.Attach(poslovnica);
            dbContext.Poslovnica.Remove(poslovnica);
            dbContext.SaveChanges();

            return RedirectToRoute("AdminPoslovnicaIndex");
        }

        [Route("/Admin/Poslovnica/Uredi", Name = "AdminPoslovnicaUredi")]
        [HttpGet]
        public IActionResult Uredi(int id)
        {
            var poslovnica = dbContext.Poslovnica.Find(id);

            if (poslovnica == null)
            {
                return NotFound();
            }

            var model = new AdminPoslovnicaVM
            {
                Title = "Poslovnice",
                Id = poslovnica.Id,
                Adresa = poslovnica.Adresa,
                Grad = poslovnica.Grad,
                BrojTelefona = poslovnica.BrojTelefona
            };

            return View("Views/Admin/Poslovnice/Uredi.cshtml", model);
        }

        [Route("/Admin/Poslovnica/Uredi", Name = "AdminPoslovnicaUredi")]
        [HttpPost]
        public IActionResult Uredi(AdminPoslovnicaVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Title = "Poslovnice";
                return View("Views/Admin/Poslovnice/Uredi.cshtml", model);
            }

            var poslovnica = dbContext.Poslovnica.Find(model.Id);

            if (poslovnica == null)
            {
                return NotFound();
            }

            poslovnica.Adresa = model.Adresa;
            poslovnica.BrojTelefona = model.BrojTelefona;
            poslovnica.Grad = model.Grad;
            dbContext.Update(poslovnica);

            dbContext.SaveChanges();
            return RedirectToRoute("AdminPoslovnicaIndex");
        }
        [Route("/Admin/Poslovnica/Dodaj", Name = "AdminPoslovnicaDodaj")]
        [HttpGet]
        public IActionResult Dodaj()
        {
            return View("Views/Admin/Poslovnice/Dodaj.cshtml");
        }

        [Route("/Admin/Poslovnica/Dodaj", Name = "AdminPoslovnicaDodaj")]
        [HttpPost]
        public IActionResult Dodaj(AdminPoslovnicaVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Title = "Poslovnice";
                return View("Views/Admin/Poslovnice/Dodaj.cshtml",model);
            }

            var poslovnica = new Poslovnica
            {
                Grad = model.Grad,
                Adresa = model.Adresa,
                BrojTelefona = model.BrojTelefona
            };

            dbContext.Poslovnica.Add(poslovnica);
            dbContext.SaveChanges();

            return RedirectToRoute("AdminPoslovnicaIndex");
        }
    }
}