using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rs1_pet_shop_webapp.EntityModels
{
    public class Adresa
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Grad { get; set; }
        public int? KorisnikId { get; set; }
        public Korisnik Korisnik { get; set; }
    }
}
