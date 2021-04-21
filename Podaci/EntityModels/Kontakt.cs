using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rs1_pet_shop_webapp.EntityModels
{
    public class Kontakt
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }
        public bool Odgovoreno { get; set; }

        public int? AdminId { get; set; }
        public Admin Admin { get; set; }
        public string? Odgovor { get; set; }
        public int? KorisnikId { get; set; }
        public Korisnik Korisnik { get; set; }
    }
}
