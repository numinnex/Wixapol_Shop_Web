using Wixapol_DataAccess.Models;
using WixapolShop.Areas.Customer.Models;

namespace WixapolShop.Areas.Customer.ViewModels
{
    public class ShoppingCartVM
    {
        public List<ShoppingCartWithProduct> ShoppingCarts { get; set; } = new();
        public double SubTotal { get; set; } = 0;
        public double Tax { get; set; } = 0;
        public double Total { get; set; } = 0;
    }
}
