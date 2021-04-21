using rs1_pet_shop_webapp.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rs1_pet_shop_webapp.ViewModels.Admin
{
    public class AdminKomentarVM : AdminVM
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool Odobren { get; set; }
        public string Datum { get; set; }
        public int? KorisnikId { get; set; }
        public rs1_pet_shop_webapp.EntityModels.Korisnik Korisnik { get; set; }
        public int? ProizvodId { get; set; }
        public Proizvod Proizvod { get; set; }
        public List<Komentar> KomentarList { get; set; }

    }
}
