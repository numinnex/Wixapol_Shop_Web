using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.VisualBasic;
using System.Security.Claims;
using Wixapol_DataAccess.UnitOfWork.Interface;
using WixapolShop.Views.CreationModels;

namespace WixapolShop.Areas.Customer.Controllers
{
    [Authorize]
    public class RatingController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public RatingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        private string GetUserId()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            return claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RateCM rateCM)
        {
            if (ModelState.IsValid)
            {
                rateCM.Rate.UserId = GetUserId();

                _unitOfWork.Rate.CreateRate(rateCM.Rate);

                TempData["rateCreated"] = "Review created successfully";
                ModelState.Clear();
                return RedirectToAction("Display", "DisplayProduct", new { productId = rateCM.Rate.ProductId });
            }
            else
            {
                TempData["rateError"] = "Error couldn't create the review";
                return RedirectToAction("Display", "DisplayProduct", new { productId = rateCM.Rate.ProductId });
            }

        }
        public IActionResult Remove(int rateId, int productId)
        {
            _unitOfWork.Rate.DeleteRate(rateId);

            TempData["rateDeleted"] = "Review deleted successfully";

            return RedirectToAction("Display", "DisplayProduct", new { productId = productId });
        }
    }
}
