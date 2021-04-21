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
    public class AdminProizvodjacVM : AdminVM
    {
        public int Id { get; set; }
        [Required]
        public string Naziv { get; set; }
        [Required]
        public int DrzavaId { get; set; }
        public string Drzava { get; set; }

        public List<Proizvodjac> ProizvodjaciList { get; set; }
        public List<Drzava> DrzavaList { get; set; }
    }
}
