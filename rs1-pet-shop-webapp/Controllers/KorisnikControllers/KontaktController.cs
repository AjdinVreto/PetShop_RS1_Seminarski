using Identity.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using rs1_pet_shop_webapp.Areas.Identity.Data;
using rs1_pet_shop_webapp.Controllers.Identity;
using rs1_pet_shop_webapp.EF;
using rs1_pet_shop_webapp.EntityModels;
using rs1_pet_shop_webapp.ViewModels.Korisnik.Kontakt;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace rs1_pet_shop_webapp.Controllers.KorisnikControllers
{
    public class KontaktController : Controller
    {
        private readonly MojDbContext ctx;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly MyRoleManager _roleManager;

        public KontaktController(
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
        [HttpGet]
        public IActionResult Index()
        { 
            var kontakti = ctx.Kontakt.Select(x => new KontaktFormaVM.Row
            {
                id = x.Id,
                email = x.Email,
                ime = x.Ime,
                odgovoreno = x.Odgovoreno,
                text = x.Text,
                odgovor = x.Odgovor,
            }).ToList();

            KontaktFormaVM tk = new KontaktFormaVM();
            tk.kontakti = kontakti;

            return Ok(tk);
        }

        [AllowAnonymous]
        public IActionResult vratiOdgovor(int id)
        {
            var odgovor = ctx.Kontakt.Where(x => x.Id == id).FirstOrDefault();

            return Ok(odgovor);
        }

        public IActionResult OtvoriAngularApp()
        {
            return Redirect("http://localhost:4200/");
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Snimi([FromBody] KontaktFormaVM.Row tk)
        {
            Kontakt k = new Kontakt
            {
                AdminId = null,
                Odgovor = null,
                Odgovoreno = false,
                Email = tk.email,
                Ime = tk.ime,
                Text = tk.text,
                KorisnikId = null
            };

            ctx.Kontakt.Add(k);
            ctx.SaveChanges();

            return Ok();
        }
    }
}
