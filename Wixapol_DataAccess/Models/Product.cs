using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wixapol_DataAccess.Models
{
    public sealed class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        [Required]
        [Display(Name = "Producent")]
        public int ProducentId { get; set; }
        public Producent? Producent { get; set; }
        [Required]
        public int BaseSpecId { get; set; }
        [Required]
        public int AdvancedSpecId { get; set; }
        public int? PhysicalSpecId { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }
        [Required]
        [Display(Name = "Long Description")]
        public string LongDescription { get; set; }
        [Display(Name = "Product Image")]
        public string? ProductImage { get; set; }
        [Required]
        [Display(Name = "Is The Product Discounted")]
        public bool IsDiscounted { get; set; }
        [Range(1, 100, ErrorMessage = "Discount Amount must be between 1 and 100!")]
        [Display(Name = "Discount Amount percentage")]
        public double? DiscountAmount { get; set; }
        [Display(Name = "Discount Code")]
        public string? DiscountCode { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be higher than 0!")]
        [Display(Name = "Quantity")]
        public int QuantityInStock { get; set; }
        [Required]
        [Range(1, 100, ErrorMessage = "Tax Rate must be between 1 and 100!")]
        [Display(Name = "Tax Rate")]
        public double TaxRate { get; set; }
        [Required]
        [Range(0.01, 10000000, ErrorMessage = "Product Price Must be positive and lower than 10000000")]
        [Display(Name = "Product Price")]
        public double RetailPrice { get; set; }
        [Required]
        [Display(Name = "Product Price")]
        public string RetailPriceStringify { get; set; }
        [Required]
        [Display(Name = "Warranty Length")]
        public string WarrantyLength { get; set; }




    }
}
