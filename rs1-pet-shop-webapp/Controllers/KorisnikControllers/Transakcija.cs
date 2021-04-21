using Identity.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using rs1_pet_shop_webapp.Areas.Identity.Data;
using rs1_pet_shop_webapp.Controllers.Identity;
using rs1_pet_shop_webapp.EF;
using rs1_pet_shop_webapp.EntityModels;
using rs1_pet_shop_webapp.ViewModels.Korisnik.Transakcija;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rs1_pet_shop_webapp.Controllers.KorisnikControllers
{
    public class TransakcijaController : Controller
    {
        private readonly MojDbContext ctx;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly MyRoleManager _roleManager;
        public TransakcijaController(
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

        public IActionResult Index(int ukupno)
        {
            TransakcijaIndexVM ti = new TransakcijaIndexVM
            {
                Ukupno = ukupno
            };

            return View(ti);
        }

        public IActionResult UspjesnaKupovina(TransakcijaIndexVM ti)
        {
            var user = _signInManager.UserManager.GetUserAsync(User);
            Korisnik korisnik = ctx.Korisnik.Where(x => x.UserId.Equals(user.Result.Id)).FirstOrDefault();

            if (ModelState.IsValid)
            {
                var narudzba = ctx.Narudzba.Where(x => x.KorisnikId == korisnik.Id && x.Aktivna == true).Include(x => x.Korisnik).FirstOrDefault();

                TransakcijaUspjesnoVM tu = new TransakcijaUspjesnoVM
                {
                    ImePrezime = narudzba.Korisnik.Ime + " " + narudzba.Korisnik.Prezime
                };

                narudzba.Zavrsena = true;
                narudzba.Aktivna = false;

                Transakcija t = new Transakcija
                {
                    NacinPlacanja = "Kartica",
                    Datum = DateTime.Now.ToString("dd/MM/yyyy"),
                    NarudzbaId = narudzba.Id,
                    Iznos = ti.Ukupno
                };

                ctx.Transakcija.Add(t);
                ctx.SaveChanges();

                return View(tu);
            }
            else
            {
                return RedirectToAction("Index", ti.Ukupno);
            }
        }
    }
}
