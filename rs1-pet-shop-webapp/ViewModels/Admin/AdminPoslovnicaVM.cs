using rs1_pet_shop_webapp.EntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace rs1_pet_shop_webapp.ViewModels.Admin
{
    public class AdminPoslovnicaVM : AdminVM
    {
        public int Id { get; set; }
        [Required]
        public string Adresa { get; set; }
        [Required]
        public string BrojTelefona { get; set; }
        [Required]
        public string Grad { get; set; }

        public List<Poslovnica> PoslovnicaList { get; set; }
    }
}
