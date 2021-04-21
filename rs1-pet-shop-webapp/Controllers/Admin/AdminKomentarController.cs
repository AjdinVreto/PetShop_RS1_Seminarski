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
    public class AdminKomentarController : Controller
    {
        private MojDbContext dbContext;
        public AdminKomentarController()
        {
            dbContext = new MojDbContext();
        }

        [Route("/Admin/Komentari", Name = "AdminKomentarIndex")]
        [HttpGet]
        public IActionResult Index()
        {
            var komentari = dbContext.Komentar
                .Where(c => !c.Odobren)
                .Select(c => new Komentar
                {
                    Id = c.Id,
                    Datum = c.Datum,
                    KorisnikId = c.KorisnikId,
                    Korisnik = c.Korisnik,
                    Odobren = c.Odobren,
                    Proizvod = c.Proizvod,
                    ProizvodId = c.ProizvodId,
                    Text = c.Text
                }).ToList();
            var model = new AdminKomentarVM
            {
                Title = "Komentari",
                KomentarList = komentari
            };
            return View("Views/Admin/Komentar.cshtml", model);
        }

        [Route("/Admin/Komentar/Odobri", Name = "AdminKomentarOdobri")]
        [HttpPost]
        public IActionResult Odobri(int id)
        {
            var komentar = dbContext.Komentar.Find(id);
            if (komentar == null) return NotFound();

            komentar.Odobren = true;
            dbContext.Komentar.Attach(komentar);
            dbContext.SaveChanges();

            return RedirectToRoute("AdminKomentarIndex");
        }

        [Route("/Admin/Komentar/Odbij", Name = "AdminKomentarOdbij")]
        [HttpPost]
        public IActionResult Odbij(int id)
        {
            var komentar = dbContext.Komentar.Find(id);
            if (komentar == null) return NotFound();

            komentar.Odobren = false;
            dbContext.Komentar.Remove(komentar);
            dbContext.SaveChanges();
            return RedirectToRoute("AdminKomentarIndex");
        }
    }
}