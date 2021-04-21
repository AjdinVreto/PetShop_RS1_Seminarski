using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rs1_pet_shop_webapp.ViewModels.Korisnik.Korpa
{
    public class KorpaIndexVM
    {
        public int NarudzbaId { get; set; }
        public float Ukupno { get; set; }
        public List<Row> Rows { get; set; }
        public class Row
        {
            public int ProizvodId { get; set; }
            public string Naziv { get; set; }
            public string Opis { get; set; }
            public double Cijena { get; set; }
            public int Kolicina { get; set; }
            public string Slika { get; set; }
        }
    }
}
