using Identity.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using rs1_pet_shop_webapp.Areas.Identity.Data;
using rs1_pet_shop_webapp.Controllers.Identity;
using rs1_pet_shop_webapp.EF;
using rs1_pet_shop_webapp.EntityModels;
using rs1_pet_shop_webapp.ViewModels.Korisnik.Korpa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rs1_pet_shop_webapp.Controllers.KorisnikControllers
{
    public class KorpaController : Controller
    {
        private readonly MojDbContext ctx;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly MyRoleManager _roleManager;

        public KorpaController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<AccountController> logger,
            RoleManager<IdentityRole> roleMgr,
            UserManager<ApplicationUser> userMrg,
            MojDbContext context
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _roleManager = new MyRoleManager(roleMgr, userMrg);
            ctx = context;
        }
        public IActionResult Index()
        {
            var user = _signInManager.UserManager.GetUserAsync(User);
            Korisnik korisnik = ctx.Korisnik.Where(x => x.UserId.Equals(user.Result.Id)).FirstOrDefault();

            var narudzba = ctx.Narudzba.Where(x => x.KorisnikId == korisnik.Id && x.Aktivna == true).FirstOrDefault();
            var x = 0;

            if (narudzba != null)
            {
                var npv = ctx.NarudzbaProizvodVarijacija.Where(x => x.Narudzba.KorisnikId == korisnik.Id && x.Narudzba.Aktivna == true).Include(x => x.ProizvodVarijacija).ToList();
                foreach (var item in npv)
                {
                    x = (int)(x + (item.ProizvodVarijacija.Cijena * item.Kolicina));
                }

                KorpaIndexVM ki = new KorpaIndexVM
                {
                    NarudzbaId = narudzba.Id,
                    Ukupno = x,
                    Rows = ctx.NarudzbaProizvodVarijacija.Where(x => x.Narudzba.KorisnikId == korisnik.Id && x.Narudzba.Aktivna == true).Select(x => new KorpaIndexVM.Row
                    {
                        ProizvodId = x.ProizvodVarijacija.Id,
                        Naziv = x.ProizvodVarijacija.Proizvod.Naziv,
                        Opis = x.ProizvodVarijacija.Opis,
                        Cijena = x.ProizvodVarijacija.Cijena,
                        Kolicina = x.Kolicina,
                        Slika = x.ProizvodVarijacija.Slika.Putanja,
                    }).ToList()
                };
                return View(ki);
            }

            return View();
        }

        public async Task<IActionResult> ObrisiIzKorpeAsync(int id)
        {
            var proizvod = ctx.NarudzbaProizvodVarijacija.Where(x => x.ProizvodVarijacijaId == id);

            ctx.NarudzbaProizvodVarijacija.RemoveRange(proizvod);
            await ctx.SaveChangesAsync();

            return Redirect("/Korpa/Index");
        }

        public async Task IzmjeniKolicinu(int id, int narid, int kol)
        {
            var proizvod = ctx.NarudzbaProizvodVarijacija.Where(x => x.ProizvodVarijacijaId == id && x.NarudzbaId == narid).FirstOrDefault();
            proizvod.Kolicina = kol;

            await ctx.SaveChangesAsync();
        }

        public IActionResult Kupon(string kupon)
        {
            var user = _signInManager.UserManager.GetUserAsync(User);
            Korisnik korisnik = ctx.Korisnik.Where(x => x.UserId.Equals(user.Result.Id)).FirstOrDefault();

            var kpn = ctx.PopustKupon.Where(x => x.Kod.Equals(kupon)).FirstOrDefault();
            var narudzba = ctx.Narudzba.Where(x => x.KorisnikId == korisnik.Id && x.Aktivna == true).FirstOrDefault();
            var x = 0;

            var npv = ctx.NarudzbaProizvodVarijacija.Where(x => x.Narudzba.KorisnikId == korisnik.Id && x.Narudzba.Aktivna == true).Include(x => x.ProizvodVarijacija).ToList();
            foreach (var item in npv)
            {
                x = (int)(x + (item.ProizvodVarijacija.Cijena * item.Kolicina));
            }

            if (kpn != null)
            {
                if (kpn.VrstaPopusta.Equals("fix"))
                {
                    x = x - kpn.Iznos;
                }
                else
                {
                    x = x - ((kpn.Iznos * x) / 100);
                }
            }

            KorpaIndexVM ki = new KorpaIndexVM
            {
                NarudzbaId = narudzba.Id,
                Ukupno = x,
                Rows = ctx.NarudzbaProizvodVarijacija.Where(x => x.Narudzba.KorisnikId == korisnik.Id && x.Narudzba.Aktivna == true).Select(x => new KorpaIndexVM.Row
                {
                    ProizvodId = x.ProizvodVarijacija.Id,
                    Naziv = x.ProizvodVarijacija.Proizvod.Naziv,
                    Opis = x.ProizvodVarijacija.Opis,
                    Cijena = x.ProizvodVarijacija.Cijena,
                    Kolicina = x.Kolicina,
                    Slika = x.ProizvodVarijacija.Slika.Putanja,
                }).ToList()
            };

            return View("Index", ki);
        }
    }
}
