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
    public class AdminKontaktController : Controller
    {
        private MojDbContext dbContext;
        public AdminKontaktController()
        {
            dbContext = new MojDbContext();
        }
        [Route("/Admin/Kontakt", Name = "AdminKontaktIndex")]
        [HttpGet]
        public IActionResult Index()
        {
            var listaKontakata = dbContext.Kontakt.Select(c => new Kontakt
            {
                Id = c.Id,
                Ime = c.Ime,
                Email = c.Email,
                AdminId = c.Admin.Id,
                Admin = c.Admin,
                Odgovoreno = c.Odgovoreno,
                Text = c.Text
            }).OrderByDescending(c => c.Id).ToList();
            var model = new AdminKontaktVM
            {
                Title = "Kontakt",
                KontaktList = listaKontakata
            };
            return View("Views/Admin/Kontakt.cshtml", model);
        }

        [Route("/Admin/Kontakt/Obrisi", Name = "AdminKontaktObrisi")]
        [HttpPost]
        public string Obrisi(int id)
        {
            var kontak = new Kontakt { Id = id };
            dbContext.Kontakt.Attach(kontak);
            dbContext.Kontakt.Remove(kontak);

            dbContext.SaveChanges();

            return "Kontakt obrisan";
        }

        [Route("/Admin/Kontakt/Pregledaj", Name = "AdminKontaktPregledaj")]
        [HttpPost]
        public string Pregledaj(int id)
        {
            var kontak = dbContext.Kontakt.Find(id);

            if (kontak == null)
            {
                return "Zapis not found";
            }

            var adminId = 1;
            var admin = dbContext.Admin.Find(adminId);

            if (kontak.Odgovoreno)
            {
                kontak.AdminId = null;
                kontak.Admin = null;
            }
            else {
            kontak.AdminId = adminId;
            kontak.Admin = admin;
            }

            kontak.Odgovoreno = !kontak.Odgovoreno;

            dbContext.Update(kontak);
            dbContext.SaveChanges();

            return $"{(kontak.Odgovoreno ? adminId : 0)}";
        }
    }
}