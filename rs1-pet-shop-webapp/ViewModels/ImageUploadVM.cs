using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace rs1_pet_shop_webapp.ViewModels
{
    public class ImageUploadVM
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public IFormFile Slika { get; set; }
    }
}
