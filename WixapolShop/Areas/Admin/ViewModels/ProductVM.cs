using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;
using Wixapol_DataAccess.Models;
using Wixapol_DataAccess.Models.Abstractions;

namespace WixapolShop.Areas.Admin.Models
{
    public class ProductVM
    {
        public Product Product { get; set; }
        //TODO - remove later
        public BaseSpecification BaseSpec { get; set; }
        public AdvancedSpecification AdvancedSpec { get; set; }
        public PhysicalSpecification? PhysicalSpec { get; set; }
        public List<SelectListItem>? CategoryList { get; set; }
        public List<SelectListItem>? ProducentList { get; set; }
        public Category? Category { get; set; }
        public Producent? Producent { get; set; }

    }
}
