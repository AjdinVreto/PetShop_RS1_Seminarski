using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace rs1_pet_shop_webapp.ViewModels.Korisnik.Profil
{
    public class ProfilOsnoveVM
    {
        public int Id { get; set; }
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Ime: 2-50 karaktera !")]
        [Required(ErrorMessage = "Unesite ime")]
        public string Ime { get; set; }
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Prezime: 2-50 karaktera !")]
        [Required(ErrorMessage = "Unesite prezime")]
        public string Prezime { get; set; }
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail nije validan")]
        [Required(ErrorMessage = "Unesite email")]
        public string Email { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Broj nije validan")]
        [Required(ErrorMessage = "Unesite broj telefona")]
        public string BrojTelefona { get; set; }
        public int DrzavaId { get; set; }
        public List<SelectListItem> Drzave { get; set; }

    }
}
