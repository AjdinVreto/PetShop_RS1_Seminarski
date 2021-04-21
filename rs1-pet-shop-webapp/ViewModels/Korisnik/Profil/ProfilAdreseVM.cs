using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace rs1_pet_shop_webapp.ViewModels.Korisnik.Profil
{
    public class ProfilAdreseVM
    {
        [StringLength(70, MinimumLength = 2, ErrorMessage = "Adresa: 2-70 karaktera !")]
        [Required(ErrorMessage = "Unesite grad")]
        public string Adresa { get; set; }
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Grad: 2-50 karaktera !")]
        [Required(ErrorMessage = "Unesite adresu")]
        public string Grad { get; set; }
        public int KorisnikId { get; set; }
    }
}
