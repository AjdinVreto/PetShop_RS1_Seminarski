using rs1_pet_shop_webapp.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rs1_pet_shop_webapp.ViewModels.Admin
{
    public class AdminKorisnikVM : AdminVM
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string BrojTelefona { get; set; }
        public int DrzavaId { get; set; }
        public Drzava Drzava { get; set; }
        public List<rs1_pet_shop_webapp.EntityModels.Korisnik> KorisnikList { get; set; }
        public int BrojNarudzbi { get; set; }
        public List<Narudzba> NarudzbaList { get; set; }
    }
}
