using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rs1_pet_shop_webapp.ViewModels.Korisnik.Proizvod
{
    public class ProizvodDetaljiVM
    {
        public int KorisnikId { get; set; }
        public int ProizvodId { get; set; }
        public int ProizvodVarijacijaId { get; set; }
        public string Naziv { get; set; }
        public double Cijena { get; set; }
        public string Opis { get; set; }
        public string Velicina { get; set; }
        public string Slika { get; set; }
        public string Komentar { get; set; }
        public float Ocjena { get; set; }
        public int OcjenaKorisnika { get; set; }
        public int BrojGlasova { get; set; }
        public List<Row> Rows { get; set; }
        public class Row
        {
            public string Komentari { get; set; }
            public string Datum { get; set; }
            public string ImePrezimeKorisnika { get; set; }
        }
    }
}
