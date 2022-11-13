using Wixapol_DataAccess.Models;

namespace WixapolShop.Areas.Customer.ViewModels
{
    public sealed class ProductDisplayVM
    {
        public Product Product { get; set; }
        public int Count { get; set; }
        public List<Rate>? Rates { get; set; }

        public int? AvgRate { get; set; }
        public List<string>? UserNames { get; set; } = new();
        public double? PercentRecommend { get; set; }
        public Dictionary<int, int>? IndividualRatePercent { get; set; } = new();

    }
}
