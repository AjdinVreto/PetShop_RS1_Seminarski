using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using rs1_pet_shop_webapp.EF;
using rs1_pet_shop_webapp.EntityModels;
using rs1_pet_shop_webapp.Models;
using rs1_pet_shop_webapp.ViewModels.Korisnik.Home;

namespace rs1_pet_shop_webapp.Controllers.KorisnikControllers
{
    public class HomeController : Controller
    {
        private MojDbContext ctx;

        public HomeController(MojDbContext context)
        {
            ctx = context;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            var novosti = ctx.Novosti.Select(x => new HomeNovosti
            {
                Id = x.Id,
                Naslov = x.Naslov,
                Tekst = x.Text,
                Datum = x.Datum,
                Slika = x.Slika.Putanja
            }).ToList();

            var proizvodi = ctx.ProizvodVarijacija.OrderByDescending(x => x.Id).Take(12).Select(x => new HomeProizvodi
            {
                ProizvodId = x.Id,
                ProizvodOpis = x.Opis,
                ProizvodCijena = x.Cijena,
                ProizvodNaziv = x.Proizvod.Naziv,
                ProizvodSlika = x.Slika.Putanja
            }).ToList();

            var tuple = new Tuple<List<HomeNovosti>, List<HomeProizvodi>>(novosti, proizvodi);

            return View(tuple);
        }
    }
}
