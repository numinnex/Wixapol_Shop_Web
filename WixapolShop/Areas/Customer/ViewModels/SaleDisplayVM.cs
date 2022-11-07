using Wixapol_DataAccess.Models;

namespace WixapolShop.Areas.Customer.ViewModels
{
    public class SaleDisplayVM
    {
        public Sale Sale { get; set; }
        public List<Product> Products { get; set; }
    }
}
