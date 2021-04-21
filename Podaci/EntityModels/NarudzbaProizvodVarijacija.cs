using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace rs1_pet_shop_webapp.EntityModels
{

    
    public class NarudzbaProizvodVarijacija
    {
        [Key, Column(Order = 0)]
        public int NarudzbaId { get; set; }
        public Narudzba Narudzba { get; set; }
        [Key, Column(Order = 1)]
        public int ProizvodVarijacijaId { get; set; }
        public ProizvodVarijacija ProizvodVarijacija { get; set; }
        public int Kolicina { get; set; }
    }
}
