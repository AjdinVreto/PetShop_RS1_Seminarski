using AspNetCore.Reporting;
using Identity.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using rs1_pet_shop_webapp.Areas.Identity.Data;
using rs1_pet_shop_webapp.Controllers.Identity;
using rs1_pet_shop_webapp.EF;
using rs1_pet_shop_webapp.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TmpReportDesign;

namespace rs1_pet_shop_webapp.Controllers.KorisnikControllers
{
    public class ReportController : Controller
    {
        private readonly MojDbContext ctx;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly MyRoleManager _roleManager;
        public ReportController(
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

        public static List<ReportVM> getNarudzbe(MojDbContext ctx, SignInManager<ApplicationUser> _signInManager, ClaimsPrincipal User)
        {
            var user = _signInManager.UserManager.GetUserAsync(User);
            Korisnik korisnik = ctx.Korisnik.Where(x => x.UserId.Equals(user.Result.Id)).FirstOrDefault();

            List<ReportVM> podaci = ctx.Transakcija.Include(x => x.Narudzba).Where(x => x.Narudzba.KorisnikId == korisnik.Id).Select(x => new ReportVM
            {
                Datum = x.Datum,
                NacinPlacanja = x.NacinPlacanja,
                Iznos = x.Iznos.ToString() + " " + "KM"
            }).ToList();

            return podaci;
        }
        public IActionResult Index()
        {
            var user = _signInManager.UserManager.GetUserAsync(User);
            Korisnik korisnik = ctx.Korisnik.Where(x => x.UserId.Equals(user.Result.Id)).FirstOrDefault();

            LocalReport lrp = new LocalReport("Report/Report.rdlc");
            List<ReportVM> podaci = getNarudzbe(ctx, _signInManager, User);
            lrp.AddDataSource("DataSet1", podaci);

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("Korisnik", korisnik.Ime + " " + korisnik.Prezime);

            ReportResult res = lrp.Execute(RenderType.Pdf, parameters: parameters);
            return File(res.MainStream, "application/pdf");
        }
    }
}
