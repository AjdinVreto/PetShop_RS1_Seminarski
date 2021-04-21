using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rs1_pet_shop_webapp.EntityModels
{
    public class Poslovnica
    {
        public int Id { get; set; }
        public string Adresa { get; set; }
        public string BrojTelefona { get; set; }
        public string Grad { get; set; }
    }
}
