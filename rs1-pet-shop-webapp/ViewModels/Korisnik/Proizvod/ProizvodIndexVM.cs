using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rs1_pet_shop_webapp.ViewModels.Korisnik.Proizvod
{
    public class ProizvodIndexVM
    {
        public int ProizvodVarijacijaId { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public double Cijena { get; set; }
        public string Slika { get; set; }
    }
}
