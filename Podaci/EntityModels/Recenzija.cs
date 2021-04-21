using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rs1_pet_shop_webapp.EntityModels
{
    public class Recenzija
    {
        public int Id { get; set; }
        public int Ocjena { get; set; }
        public string Datum { get; set; }
        public int? KorisnikId { get; set; }
        public Korisnik Korisnik { get; set; }
        public int? ProizvodId { get; set; }
        public Proizvod Proizvod { get; set; }
    }
}
