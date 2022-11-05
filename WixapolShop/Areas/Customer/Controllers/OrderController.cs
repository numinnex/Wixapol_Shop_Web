using Microsoft.AspNetCore.Mvc;
using WixapolShop.Areas.Customer.ViewModels;

namespace WixapolShop.Areas.Customer.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index(ShoppingCartVM shoppingCart)
        {

            return View();
        }
    }
}
