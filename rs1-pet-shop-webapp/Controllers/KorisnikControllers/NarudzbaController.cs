using Identity.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using rs1_pet_shop_webapp.Areas.Identity.Data;
using rs1_pet_shop_webapp.Controllers.Identity;
using rs1_pet_shop_webapp.EF;
using rs1_pet_shop_webapp.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rs1_pet_shop_webapp.Controllers.KorisnikControllers
{
    public class NarudzbaController : Controller
    {
        private readonly MojDbContext ctx;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly MyRoleManager _roleManager;
        public NarudzbaController(
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
        public void Index(int id)
        {
            var user = _signInManager.UserManager.GetUserAsync(User);
            Korisnik korisnik = ctx.Korisnik.Where(x => x.UserId.Equals(user.Result.Id)).FirstOrDefault();

            var narudzba = ctx.Narudzba.Where(x => x.KorisnikId == korisnik.Id && x.Aktivna == true).FirstOrDefault();
            var proizvod = ctx.ProizvodVarijacija.Find(id);

            if (narudzba == null)
            {
                Narudzba n = new Narudzba
                {
                    KorisnikId = korisnik.Id,
                    Aktivna = true,
                    Datum = DateTime.Now.ToString("dd/MM/yyyy"),
                    Zavrsena = false,
                    AdresaId = null
                };

                ctx.Narudzba.Add(n);
                ctx.SaveChanges();

                NarudzbaProizvodVarijacija npv = new NarudzbaProizvodVarijacija
                {
                    ProizvodVarijacijaId = proizvod.Id,
                    NarudzbaId = n.Id,
                    Kolicina = 1
                };

                ctx.NarudzbaProizvodVarijacija.Add(npv);
                ctx.SaveChanges();
            }
            else
            {
                NarudzbaProizvodVarijacija npv = new NarudzbaProizvodVarijacija
                {
                    ProizvodVarijacijaId = proizvod.Id,
                    NarudzbaId = narudzba.Id,
                    Kolicina = 1
                };

                ctx.NarudzbaProizvodVarijacija.Add(npv);
                ctx.SaveChanges();
            }
        }
    }
}
