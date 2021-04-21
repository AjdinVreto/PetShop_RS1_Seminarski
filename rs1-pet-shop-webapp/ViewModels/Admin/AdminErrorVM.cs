using rs1_pet_shop_webapp.ViewModels.Admin;
using System;

namespace rs1_pet_shop_webapp.Models
{
    public class ErrorVM: AdminVM
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
