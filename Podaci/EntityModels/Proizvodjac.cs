using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rs1_pet_shop_webapp.EntityModels
{
    public class Proizvodjac
    {
        public int Id { get; set; }
        public string Naziv { get; set; }

        public int? DrzavaId { get; set; }
        public Drzava Drzava { get; set; }
    }
}
