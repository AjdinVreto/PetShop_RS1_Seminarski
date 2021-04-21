using Identity.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ReflectionIT.Mvc.Paging;
using rs1_pet_shop_webapp.Areas.Identity.Data;
using rs1_pet_shop_webapp.Controllers.Identity;
using rs1_pet_shop_webapp.EF;
using rs1_pet_shop_webapp.EntityModels;
using rs1_pet_shop_webapp.ViewModels.Korisnik.Proizvod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rs1_pet_shop_webapp.Controllers.KorisnikControllers
{
    public class ProizvodController : Controller
    {
        private readonly MojDbContext ctx;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly MyRoleManager _roleManager;
        public ProizvodController(
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

        [AllowAnonymous]
        public async Task<IActionResult> Index(int pageIndex = 1)
        {
            var proizvodi = ctx.ProizvodVarijacija.Include(x => x.Proizvod).Include(x => x.Slika).AsNoTracking().OrderByDescending(x => x.Id);

            var model = await PagingList.CreateAsync(proizvodi, 9, pageIndex);

            return View(model);
        }

        public async Task<IActionResult> SortirajUzlazno(int pageIndex = 1)
        {
            var proizvodi = ctx.ProizvodVarijacija.Include(x => x.Proizvod).Include(x => x.Slika).AsNoTracking().OrderBy(x => x.Proizvod.Naziv);

            var model = await PagingList.CreateAsync(proizvodi, 9, pageIndex);
            model.Action = "SortirajUzlazno";

            return View("Index", model);
        }

        public async Task<IActionResult> SortirajSilazno(int pageIndex = 1)
        {
            var proizvodi = ctx.ProizvodVarijacija.Include(x => x.Proizvod).Include(x => x.Slika).AsNoTracking().OrderByDescending(x => x.Proizvod.Naziv);

            var model = await PagingList.CreateAsync(proizvodi, 9, pageIndex);
            model.Action = "SortirajSilazno";

            return View("Index", model);
        }

        public async Task<IActionResult> SortirajPoCijeniUzlazno(int pageIndex = 1)
        {
            var proizvodi = ctx.ProizvodVarijacija.Include(x => x.Proizvod).Include(x => x.Slika).AsNoTracking().OrderBy(x => x.Cijena);

            var model = await PagingList.CreateAsync(proizvodi, 9, pageIndex);
            model.Action = "SortirajPoCijeniUzlazno";

            return View("Index", model);
        }

        public async Task<IActionResult> SortirajPoCijeniSilazno(int pageIndex = 1)
        {
            var proizvodi = ctx.ProizvodVarijacija.Include(x => x.Proizvod).Include(x => x.Slika).AsNoTracking().OrderByDescending(x => x.Cijena);

            var model = await PagingList.CreateAsync(proizvodi, 9, pageIndex);
            model.Action = "SortirajPoCijeniSilazno";

            return View("Index", model);
        }

        [AllowAnonymous]
        public IActionResult DetaljiProizvoda(int id)
        {
            var user = _signInManager.UserManager.GetUserAsync(User);
            Korisnik korisnik = ctx.Korisnik.Where(x => x.UserId.Equals(user.Result.Id)).FirstOrDefault();

            var proizvod = ctx.ProizvodVarijacija.Where(x => x.Id == id).Include(x => x.Proizvod).Include(x => x.Slika).FirstOrDefault();
            var rec = ctx.Recenzija.Where(x => x.ProizvodId == proizvod.ProizvodId).ToList();
            int ocjene = 0;
            int ocjenaKorisnika = ctx.Recenzija.Where(x => x.ProizvodId == proizvod.ProizvodId && x.KorisnikId == korisnik.Id).Select(x => x.Ocjena).FirstOrDefault();

            foreach (var item in rec)
            {
                ocjene += item.Ocjena;
            }

            float ukupnaOcjena = (float)ocjene / rec.Count();

            ProizvodDetaljiVM pd = new ProizvodDetaljiVM
            {
                KorisnikId = korisnik.Id,
                ProizvodId = proizvod.Proizvod.Id,
                ProizvodVarijacijaId = id,
                Naziv = proizvod.Proizvod.Naziv,
                Opis = proizvod.Opis,
                Velicina = proizvod.Velicina,
                Cijena = proizvod.Cijena,
                Slika = proizvod.Slika.Putanja,
                Ocjena = ukupnaOcjena,
                BrojGlasova = rec.Count(),
                OcjenaKorisnika = ocjenaKorisnika,
                Rows = ctx.Komentar.Where(x => x.ProizvodId == proizvod.Proizvod.Id && x.Odobren == true).Select(x => new ProizvodDetaljiVM.Row
                {
                    ImePrezimeKorisnika = x.Korisnik.Ime + " " + x.Korisnik.Prezime,
                    Komentari = x.Text,
                    Datum = x.Datum
                }).ToList()
            };

            return View(pd);
        }

        [HttpPost]
        public async Task PostaviKomentar(ProizvodDetaljiVM pd)
        {
            Komentar k = new Komentar
            {
                ProizvodId = pd.ProizvodId,
                KorisnikId = pd.KorisnikId,
                Datum = DateTime.Now.ToString("dd/MM/yyyy"),
                Odobren = true,
                Text = pd.Komentar
            };

            ctx.Komentar.Add(k);
            await ctx.SaveChangesAsync();
        }

        public async Task OcjeniProizvod(int proizvodId, int korisnikId, int ocjena)
        {
            var recenzija = ctx.Recenzija.Where(x => x.ProizvodId == proizvodId && x.KorisnikId == korisnikId).FirstOrDefault();

            if (recenzija != null)
            {
                recenzija.Ocjena = ocjena;
                recenzija.Datum = DateTime.Now.ToString("dd/MM/yyyy");

                await ctx.SaveChangesAsync();
            }
            else
            {
                Recenzija rec = new Recenzija
                {
                    KorisnikId = korisnikId,
                    ProizvodId = proizvodId,
                    Ocjena = ocjena,
                    Datum = DateTime.Now.ToString("dd/MM/yyyy")
                };
                ctx.Recenzija.Add(rec);
                await ctx.SaveChangesAsync();
            }
        }

        public async Task<IActionResult> PretragaPoKategoriji(string naziv, int pageIndex = 1)
        {
            var proizvodi = ctx.ProizvodVarijacija.Include(x => x.Proizvod).Include(x => x.Slika).Where(x => x.Proizvod.Kategorija.Vrsta.Equals(naziv)).AsNoTracking().OrderBy(x => x.Id);

            var model = await PagingList.CreateAsync(proizvodi, 6, pageIndex);
            model.Action = "PretragaPoKategoriji";
            model.RouteValue = new RouteValueDictionary {
            { "naziv", naziv} };

            return View("Index", model);
        }

        public async Task<IActionResult> PretragaPoProizvodjacu(string naziv, int pageIndex = 1)
        {
            var proizvodi = ctx.ProizvodVarijacija.Include(x => x.Proizvod).Include(x => x.Slika).Where(x => x.Proizvod.Proizvodjac.Naziv.Equals(naziv)).AsNoTracking().OrderBy(x => x.Id);

            var model = await PagingList.CreateAsync(proizvodi, 6, pageIndex);
            model.Action = "PretragaPoProizvodjacu";
            model.RouteValue = new RouteValueDictionary {
            { "naziv", naziv} };

            return View("Index", model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> PretragaPoNazivu(string pretraga, int pageIndex = 1)
        {
            var proizvodi = ctx.ProizvodVarijacija.Include(x => x.Proizvod).Include(x => x.Slika).Where(x => x.Proizvod.Naziv.Contains(pretraga)).AsNoTracking().OrderBy(x => x.Id);

            var model = await PagingList.CreateAsync(proizvodi, 6, pageIndex);
            model.Action = "PretragaPoNazivu";
            model.RouteValue = new RouteValueDictionary {
            { "pretraga", pretraga} };

            return View("Index", model);
        }
    }
}
