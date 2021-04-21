using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using rs1_pet_shop_webapp.Areas.Identity.Data;
using rs1_pet_shop_webapp.Controllers.Identity;
using rs1_pet_shop_webapp.EF;
using rs1_pet_shop_webapp.EntityModels;
using rs1_pet_shop_webapp.ViewModels.Korisnik.Profil;

namespace rs1_pet_shop_webapp.Controllers.KorisnikControllers
{
    public class ProfilController : Controller
    {
        private readonly MojDbContext ctx;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly MyRoleManager _roleManager;

        public ProfilController(
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

            ProfilOsnoveVM po = new ProfilOsnoveVM
            {
                Id = korisnik.Id,
                Ime = korisnik.Ime,
                Prezime = korisnik.Prezime,
                Email = korisnik.Email,
                BrojTelefona = korisnik.BrojTelefona,
                DrzavaId = korisnik.DrzavaId,
                Drzave = ctx.Drzava.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Naziv }).ToList()
            };

            return View(po);
        }

        [HttpPost]
        public async Task<IActionResult> Index(ProfilOsnoveVM po)
        {
            if (ModelState.IsValid)
            {
                var user = _signInManager.UserManager.GetUserAsync(User);
                Korisnik korisnik = ctx.Korisnik.Where(x => x.UserId.Equals(user.Result.Id)).FirstOrDefault();

                korisnik.Ime = po.Ime;
                korisnik.Prezime = po.Prezime;
                korisnik.Email = po.Email;
                korisnik.BrojTelefona = po.BrojTelefona;
                korisnik.DrzavaId = po.DrzavaId;

                await ctx.SaveChangesAsync();

                return Redirect("/Profil/Index");
            }
            else
            {
                po.Drzave = ctx.Drzava.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Naziv }).ToList();
                return View(po);
            }
        }

        public IActionResult Adrese()
        {
            var user = _signInManager.UserManager.GetUserAsync(User);
            Korisnik korisnik = ctx.Korisnik.Where(x => x.UserId.Equals(user.Result.Id)).FirstOrDefault();

            ProfilAdreseVM paf = new ProfilAdreseVM
            {
                KorisnikId = korisnik.Id
            };

            List<ProfilAdreseVM> pap = ctx.Adresa.Where(x => x.KorisnikId == korisnik.Id).Select(x => new ProfilAdreseVM
            {
                Adresa = x.Naziv,
                Grad = x.Grad
            }).ToList();

            ViewData["adreseKey"] = pap;

            return View(paf);
        }

        [HttpPost]
        public async Task<IActionResult> Adrese(ProfilAdreseVM pa)
        {
            if (ModelState.IsValid)
            {
                Adresa ad = new Adresa
                {
                    Grad = pa.Grad,
                    Naziv = pa.Adresa,
                    KorisnikId = pa.KorisnikId
                };

                ctx.Add(ad);
                await ctx.SaveChangesAsync();

                return Redirect("/Profil/Adrese");
            }
            else
            {
                List<ProfilAdreseVM> pap = ctx.Adresa.Where(x => x.KorisnikId == pa.KorisnikId).Select(x => new ProfilAdreseVM
                {
                    Adresa = x.Naziv,
                    Grad = x.Grad
                }).ToList();

                ViewData["adreseKey"] = pap;

                return View(pa);
            }
        }

        public IActionResult Password()
        {
            ProfilPasswordVM pp = new ProfilPasswordVM();

            return View(pp);
        }

        [HttpPost]
        public async Task<IActionResult> PromjeniPassword(ProfilPasswordVM pp)
        {
            var user = _signInManager.UserManager.GetUserAsync(User);
            Korisnik korisnik = ctx.Korisnik.Where(x => x.UserId.Equals(user.Result.Id)).FirstOrDefault();

            if (pp.noviPassword == pp.noviPasswordPonovo)
            {
                var result = await _signInManager.UserManager.ChangePasswordAsync(await user, pp.stariPassword, pp.noviPassword);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return Redirect("/Profil/Password");
                }
            }

            await _signInManager.RefreshSignInAsync(await user);

            return Redirect("/Profil/Index");
        }
    }
}
