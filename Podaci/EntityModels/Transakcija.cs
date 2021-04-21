using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rs1_pet_shop_webapp.EntityModels
{
    public class Transakcija
    {
        public int Id { get; set; }
        public double Iznos { get; set; }
        public string NacinPlacanja { get; set; }
        public string Datum { get; set; }
        public int? PopustKuponId { get; set; }
        public PopustKupon PopustKupon { get; set; }
        public int? NarudzbaId { get; set; }
        public Narudzba Narudzba { get; set; }
    }
}
