using rs1_pet_shop_webapp.EntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace rs1_pet_shop_webapp.ViewModels.Admin
{
    public class AdminDodajProizvodVM : AdminVM
    {
        public int Id { get; set; }
        [Required]
        public string Naziv { get; set; }
        [Required]
        public int? ProizvodjacId { get; set; }
        public Proizvodjac Proizvodjac { get; set; }
        [Required]
        public int? KategorijaId { get; set; }
        public Kategorija Kategorija { get; set; }
        public List<Kategorija> KategorijaList { get; set; }
        public List<Proizvodjac> ProizvodjacList { get; set; }
        public List<Proizvod> ProizvodList { get; set; }
    }
}
