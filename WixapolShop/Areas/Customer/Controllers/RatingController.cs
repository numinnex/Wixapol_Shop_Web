using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WixapolShop.Views.CreationModels;

namespace WixapolShop.Areas.Customer.Controllers
{
    [Authorize]
    public class RatingController : Controller
    {
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RateCM rateCM)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Message = "Rate created successfully";
                ModelState.Clear();
                return PartialView("_ModalValidation");
            }
            return ViewComponent("CreateRate");

        }
    }
}
