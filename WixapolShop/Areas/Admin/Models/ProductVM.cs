using Microsoft.AspNetCore.Mvc.Rendering;
using Wixapol_DataAccess.Models;

namespace WixapolShop.Areas.Admin.Models
{
    public class ProductVM
    {
        public Product Product { get; set; }
        public BaseSpecification BaseSpec { get; set; }
        public AdvancedSpecification AdvancedSpec { get; set; }
        public PhysicalSpecification PhysicalSpec { get; set; }
        public List<SelectListItem> CategoryList { get; set; }
        public List<SelectListItem> ProducentList { get; set; }


    }
}
