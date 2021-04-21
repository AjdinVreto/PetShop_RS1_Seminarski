using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace rs1_pet_shop_webapp.EntityModels
{
    public class Narudzba
    {
        public int Id { get; set; }
        public bool Aktivna { get; set; }
        public bool Zavrsena { get; set; }
        public string Datum { get; set; }
        public int? KorisnikId { get; set; }
        public Korisnik Korisnik { get; set; }
        public int? AdresaId { get; set; }
        public Adresa Adresa { get; set; }
    }
}
