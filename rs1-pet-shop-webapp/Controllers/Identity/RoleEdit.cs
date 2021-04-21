using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using rs1_pet_shop_webapp.Areas.Identity.Data;

namespace Identity.Models
{
    public class RoleEdit
    {
        public IdentityRole Role { get; set; }
        public IEnumerable<ApplicationUser> Members { get; set; }
        public IEnumerable<ApplicationUser> NonMembers { get; set; }
    }
}