using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rs1_pet_shop_webapp.ViewModels.Korisnik.Home
{
    public class HomeProizvodi
    {
        public int ProizvodId { get; set; }
        public string ProizvodNaziv { get; set; }
        public string ProizvodOpis { get; set; }
        public double ProizvodCijena { get; set; }
        public string ProizvodSlika { get; set; }
    }
}
