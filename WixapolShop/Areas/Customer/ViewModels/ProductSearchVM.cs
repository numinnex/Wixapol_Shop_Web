using System.ComponentModel.DataAnnotations;
using Wixapol_DataAccess.Models;
using WixapolShop.Areas.Customer.Models;

namespace WixapolShop.Areas.Customer.ViewModels
{
    public sealed class ProductSearchVM
    {
        public SearchModel query { get; set; }
        public int? MinPrice { get; set; } = 0;
        public int? MaxPrice { get; set; } = 7000;
        public List<int>? CategoryIdList { get; set; }
        public List<string>? ProducentIdList { get; set; }
        public List<Product> Products { get; set; }
        public Dictionary<int, List<string>>? ProductsByCategory { get; set; }
        public Dictionary<string, List<string>>? ProductsByProducent { get; set; }
        public SortedDictionary<double, List<string>>? ProductsByPrice { get; set; }

    }
}
