using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace rs1_pet_shop_webapp.EntityModels
{
    public class PopustKupon
    {
        public int Id { get; set; }
        public int Iznos { get; set; }
        public string Kod { get; set; }
        public string VrstaPopusta { get; set; }
    }
}
