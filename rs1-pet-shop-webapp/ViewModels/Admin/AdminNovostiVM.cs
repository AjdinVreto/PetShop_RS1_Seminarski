using Microsoft.AspNetCore.Http;
using rs1_pet_shop_webapp.EntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace rs1_pet_shop_webapp.ViewModels.Admin
{
    public class AdminNovostiVM : AdminVM
    {
        public int Id { get; set; }
        [Required]
        public string Naslov { get; set; }
        [Required]
        public string Text { get; set; }
        public string Datum { get; set; }
        public int SlikaId { get; set; }
        public string Slika { get; set; }
        [DisplayName("Slika")]
        [Required]
        public IFormFile SlikaFile { get; set; }
    }
}
