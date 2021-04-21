using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rs1_pet_shop_webapp.EntityModels
{
    public class Komentar
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool Odobren { get; set; }
        public string Datum { get; set; }
        public int? KorisnikId { get; set; }
        public Korisnik Korisnik { get; set; }
        public int? ProizvodId { get; set; }
        public Proizvod Proizvod { get; set; }
    }
}
