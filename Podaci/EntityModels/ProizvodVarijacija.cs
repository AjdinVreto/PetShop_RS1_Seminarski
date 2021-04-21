using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace rs1_pet_shop_webapp.EntityModels
{
    public class ProizvodVarijacija
    {
        public int Id { get; set; }
        public string Opis { get; set; }
        public double Cijena { get; set; }
        public string Velicina { get; set; }
        public int? SlikaId { get; set; }
        public Slika Slika { get; set; }
        public int? ProizvodId { get; set; }
        public Proizvod Proizvod { get; set; }
    }
}
