using rs1_pet_shop_webapp.EntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace rs1_pet_shop_webapp.ViewModels.Admin
{
    public class AdminPopustKuponVM : AdminVM
    {
        public int Id { get; set; }
        [Required]
        public int Iznos { get; set; }
        [Required]
        public string Kod { get; set; }
        [Required]
        public string VrstaPopusta { get; set; }
        public List<PopustKupon> PopustKuponList { get; set; }
    }
}
