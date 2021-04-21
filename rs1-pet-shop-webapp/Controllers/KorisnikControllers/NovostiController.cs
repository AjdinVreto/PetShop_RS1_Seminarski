using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using rs1_pet_shop_webapp.EF;
using rs1_pet_shop_webapp.EntityModels;
using rs1_pet_shop_webapp.ViewModels.Korisnik;
using rs1_pet_shop_webapp.ViewModels.Korisnik.Home;

namespace rs1_pet_shop_webapp.Controllers.KorisnikControllers
{
    public class NovostiController : Controller
    {
        private MojDbContext ctx;

        public NovostiController(MojDbContext context)
        {
            ctx = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NovostiDetalji(int id)
        {
            NovostiDetaljiVM n = ctx.Novosti.Where(x => x.Id == id).Select(x => new NovostiDetaljiVM
            {
                Id = x.Id,
                Naslov = x.Naslov,
                Tekst = x.Text,
                Slika = x.Slika.Putanja
            }).SingleOrDefault();

            return View(n);
        }
    }
}
