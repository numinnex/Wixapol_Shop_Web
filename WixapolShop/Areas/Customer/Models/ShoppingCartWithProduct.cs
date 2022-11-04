using Wixapol_DataAccess.Models;

namespace WixapolShop.Areas.Customer.Models
{
    public class ShoppingCartWithProduct
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public double TaxAmount { get; set; } = 0;
        public string UserId { get; set; }
    }
}
