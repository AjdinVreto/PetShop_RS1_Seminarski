using rs1_pet_shop_webapp.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rs1_pet_shop_webapp.ViewModels.Admin
{
    public class AdminKategorijaVM : AdminVM
    {
        public int Id { get; set; }
        public string Vrsta { get; set; }
        public List<Kategorija> KategorijaList { get; set; }
    }
}
