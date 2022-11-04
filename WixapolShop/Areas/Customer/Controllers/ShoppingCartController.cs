using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Wixapol_DataAccess.Models;
using Wixapol_DataAccess.UnitOfWork.Interface;
using WixapolShop.Areas.Customer.ViewModels;

namespace WixapolShop.Areas.Customer.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ShoppingCartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Setup(ProductDisplayVM productVM)
        {

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);


            ShoppingCart existingCart = _unitOfWork.ShoppingCart.GetShoppingCartByUserId(claim.Value).
                Where(s => s.ProductId == productVM.Product.Id).FirstOrDefault();

            if (existingCart is null)
            {
                _unitOfWork.ShoppingCart.CreateShoppingCart(new ShoppingCart
                {
                    ProductId = productVM.Product.Id,
                    UserId = claim.Value,
                    Count = productVM.Count
                });
            }
            else
            {
                //TODO - Add logic with checking quantity in stock before updating the shopping cart count
                _unitOfWork.ShoppingCart.UpdateShoppingCartProductCount(existingCart.Id, existingCart.Count + productVM.Count);
            }



            return RedirectToAction(nameof(Index));
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
