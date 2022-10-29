using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wixapol_DataAccess.Models.DTO
{
    public sealed class ProductDTOWithId
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public int ProducentId { get; set; }
        public int BaseSpecId { get; set; }
        public int AdvancedSpecId { get; set; }
        public int? PhysicalSpecId { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string ProductImage { get; set; }
        public bool IsDiscounted { get; set; }
        public double? DiscountAmount { get; set; }
        public string? DiscountCode { get; set; }
        public int QuantityInStock { get; set; } = 1;
        public double TaxRate { get; set; }
        public double RetailPrice { get; set; }
        public string WarrantyLength { get; set; }
    }
}
