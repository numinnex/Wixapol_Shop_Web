using Wixapol_DataAccess.Models;

namespace WixapolShop.Areas.Customer.ViewModels
{
    public sealed class SaleDisplayVM
    {
        public Sale Sale { get; set; }
        public List<Product> Products { get; set; } = new();
        public int SaleId { get; set; }
        public string EmailAdress { get; set; }
    }
}
