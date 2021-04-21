using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rs1_pet_shop_webapp.EntityModels
{
    public class Korisnik
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string BrojTelefona { get; set; }
        public int DrzavaId { get; set; }
        public Drzava Drzava { get; set; }
    }
}
