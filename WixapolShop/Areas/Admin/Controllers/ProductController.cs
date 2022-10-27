using Microsoft.AspNetCore.Mvc;
using Wixapol_DataAccess.Models;
using WixapolShop.Areas.Admin.Models;

namespace WixapolShop.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Create()
        {
            ProductVM productVM = new ProductVM()
            {
                Product = new Product(),
            };

            return View(productVM);
        }
    }
}
