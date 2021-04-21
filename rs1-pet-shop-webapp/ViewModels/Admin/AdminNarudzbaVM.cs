using rs1_pet_shop_webapp.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rs1_pet_shop_webapp.ViewModels.Admin
{
    public class AdminNarudzbaVM : AdminVM
    {
        public int Id { get; set; }
        public bool Aktivna { get; set; }
        public bool Zavrsena { get; set; }
        public string Datum { get; set; }
        public int? KorisnikId { get; set; }
        public rs1_pet_shop_webapp.EntityModels.Korisnik Korisnik { get; set; }
        public int? AdresaId { get; set; }
        public Adresa Adresa { get; set; }
        public List<Narudzba> NarudzbaList { get; set; }
        public List<NarudzbaProizvodVarijacija> VarijacijeList { get; set; }
    }
}
