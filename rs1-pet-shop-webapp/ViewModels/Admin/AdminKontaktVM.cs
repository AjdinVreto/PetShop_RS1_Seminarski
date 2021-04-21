using Microsoft.AspNetCore.Http;
using rs1_pet_shop_webapp.EntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace rs1_pet_shop_webapp.ViewModels.Admin
{
    public class AdminKontaktVM : AdminVM
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }
        public bool Odgovoreno { get; set; }

        public int? AdminId { get; set; }
        public rs1_pet_shop_webapp.EntityModels.Admin Admin { get; set; }
        public List<Kontakt> KontaktList { get; set; }
    }
}
