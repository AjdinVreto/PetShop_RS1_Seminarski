using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rs1_pet_shop_webapp.EntityModels
{
    public class Novosti
    {
        public int Id { get; set; }
        public string Naslov { get; set; }
        public string Text { get; set; }
        public string Datum { get; set; }
        public int SlikaId { get; set; }
        public Slika Slika { get; set; }
    }
}
