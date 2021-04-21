using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace rs1_pet_shop_webapp.EntityModels
{
    public class Proizvod
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public int? ProizvodjacId { get; set; }
        public Proizvodjac Proizvodjac { get; set; }
        public int? KategorijaId { get; set; }
        public Kategorija Kategorija { get; set; }
    }
}
