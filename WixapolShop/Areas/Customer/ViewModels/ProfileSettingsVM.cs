using WixapolShop.Areas.Customer.Models;
using WixapolShop.Areas.Identity.Models.Domain;

namespace WixapolShop.Areas.Customer.ViewModels
{
    public sealed class ProfileSettingsVM
    {
        public ApplicationUserDTO User { get; set; } = new();


    }
}
