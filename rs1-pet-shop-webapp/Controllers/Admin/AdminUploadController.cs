using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rs1_pet_shop_webapp.Data;
using rs1_pet_shop_webapp.EF;
using rs1_pet_shop_webapp.EntityModels;
using rs1_pet_shop_webapp.ViewModels;

namespace rs1_pet_shop_webapp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminUploadController: Controller
    {
        private MojDbContext dbContext;
        private IWebHostEnvironment webHostEnvironment;
        public AdminUploadController(IWebHostEnvironment hostEnvironment)
        {
            dbContext = new MojDbContext();
            webHostEnvironment = hostEnvironment;
        }
        public async Task<IActionResult> DodajProizvod()
        {
            List<Slika> slike = await dbContext.Slika.OrderByDescending(x => x.Id).ToListAsync();
            ViewData["slike"] = slike;
            ViewData["activeSideTab"] = "DodajProizvod";
            return View("Views/Admin/ImageUpload.cshtml");
        }
        [HttpPost]
        public async Task<IActionResult> ImageUpload(ImageUploadVM model)
        {
            string uniqueFileName = UploadedFile(model);
            Slika slika = new Slika
            {
                Putanja = uniqueFileName
            };
            dbContext.Add(slika);
            await dbContext.SaveChangesAsync();

            List<Slika> slike = await dbContext.Slika.OrderByDescending(x => x.Id).ToListAsync();
            ViewData["slike"] = slike;

            return View("Views/Admin/ImageUpload.cshtml");
        }
        public IActionResult UrediProizvod()
        {
            ViewData["activeSideTab"] = "UrediProizvod";
            return View("Views/Admin/Index.cshtml");
        }

        private string UploadedFile(ImageUploadVM model)
        {
            string uniqueFileName = null;

            if (model.Slika != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Slika.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Slika.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

    }
}