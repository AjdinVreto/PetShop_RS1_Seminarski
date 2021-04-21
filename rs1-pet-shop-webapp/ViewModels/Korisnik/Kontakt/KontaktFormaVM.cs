using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace rs1_pet_shop_webapp.ViewModels.Korisnik.Kontakt
{
    public class KontaktFormaVM
    {
        public class Row
        {
            public int id { get; set; }
            public string ime { get; set; }
            public string email { get; set; }
            public string text { get; set; }
            public bool odgovoreno { get; set; }
            public string odgovor { get; set; }
            public int korisnikid { get; set; }

        }

        public List<Row> kontakti { get; set; }
    }
}
