using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using rs1_pet_shop_webapp.EF;
using rs1_pet_shop_webapp.EntityModels;
using rs1_pet_shop_webapp.ViewModels;

namespace rs1_pet_shop_webapp.Controllers
{
    public class LoginController : Controller
    {
        private MojDbContext db;

        public LoginController(MojDbContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginVM loginPodaci)
        {
            var korisnik = db.Korisnik.Where(x => x.Email.Equals(loginPodaci.Email) && x.Password.Equals(loginPodaci.Password)).FirstOrDefault();

            if(korisnik != null)
            {
                return Redirect("Home");
            }

            var admin = db.Admin.Where(x => x.Email.Equals(loginPodaci.Email) && x.Password.Equals(loginPodaci.Password)).FirstOrDefault();

            if(admin != null)
            {
                return Redirect("Admin");
            }

            ViewData["keyError"] = "Korisnik nije pronadjen";

            return View();
        }
    }
}
