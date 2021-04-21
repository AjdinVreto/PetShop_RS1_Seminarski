using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using rs1_pet_shop_webapp.Models;

namespace rs1_pet_shop_webapp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            ViewData["activeSideTab"] = "DodajProizvod";
            return View();
        }

        public IActionResult LogOut()
        {
            return Redirect("/Home");
        }

        [Route("{controller=Admin}/{*url}", Order = 999)]
        [AllowAnonymous]
        public IActionResult CatchAll()
        {
            Response.StatusCode = 404;
            return View("Error",new ErrorVM { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

        }

    }
}
