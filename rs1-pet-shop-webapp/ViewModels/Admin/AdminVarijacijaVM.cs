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
    public class AdminVarijacijaVM : AdminVM
    {
        public int Id { get; set; }
        [Required]
        public string Opis { get; set; }
        [Required]
        public double Cijena { get; set; }
        [Required]
        public string Velicina { get; set; }
        // [Required]
        public int? SlikaId { get; set; }
        public string Slika { get; set; }
        [Required]
        public int? ProizvodId { get; set; }
        [DisplayName("Slika")]
        public IFormFile SlikaFile { get; set; }
        public Proizvod Proizvod { get; set; }
        public List<ProizvodVarijacija> VarijacijaList { get; set; }
        public List<Proizvod> ProizvodList { get; set; }
        public List<Slika> SlikaList { get; set; }
    }
}
