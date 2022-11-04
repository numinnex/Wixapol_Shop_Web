using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Wixapol_DataAccess.Models;
using Wixapol_DataAccess.UnitOfWork.Interface;
using WixapolShop.Areas.Customer.Models;
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
        private string GetUserId()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            return claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Setup(ProductDisplayVM productVM)
        {

            string userId = GetUserId();
            ShoppingCart existingCart = _unitOfWork.ShoppingCart.GetShoppingCartByUserId(userId).
                Where(s => s.ProductId == productVM.Product.Id).FirstOrDefault();

            if (existingCart is null)
            {
                _unitOfWork.ShoppingCart.CreateShoppingCart(new ShoppingCart
                {
                    ProductId = productVM.Product.Id,
                    UserId = userId,
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
        [Authorize]
        public IActionResult RemoveFromCart(int cartId)
        {
            _unitOfWork.ShoppingCart.DeleteShoppingCart(cartId);

            return RedirectToAction(nameof(Index));
        }

        private double CalculateTax(ShoppingCartWithProduct shoppingCart)
        {
            return Math.Round(shoppingCart.Product.RetailPrice * shoppingCart.Count *
                (shoppingCart.Product.TaxRate / 100), 2, MidpointRounding.AwayFromZero);
        }
        private void UpdateSubTotalAndTotal(ShoppingCartVM shoppingCartVM)
        {

            shoppingCartVM.SubTotal = Math.Round(shoppingCartVM.SubTotal, 2, MidpointRounding.AwayFromZero);
            shoppingCartVM.Total = Math.Round(shoppingCartVM.Tax + shoppingCartVM.SubTotal, 2, MidpointRounding.AwayFromZero);
        }

        private void SetupProductsAndPricesForDisplay(ShoppingCartVM shoppingCartVM)
        {
            foreach (var shoppingCart in shoppingCartVM.ShoppingCarts)
            {
                shoppingCart.Product = (_unitOfWork.Product.GetById(shoppingCart.ProductId));

                shoppingCartVM.SubTotal += Math.Round(shoppingCart.Product.RetailPrice * shoppingCart.Count, 2, MidpointRounding.AwayFromZero);

                shoppingCart.TaxAmount = CalculateTax(shoppingCart);

                shoppingCartVM.Tax += Math.Round(shoppingCart.TaxAmount, 2, MidpointRounding.AwayFromZero);
            }
            UpdateSubTotalAndTotal(shoppingCartVM);
        }


        [Authorize]
        public IActionResult Index()
        {
            string userId = GetUserId();

            ShoppingCartVM shoppingCartVM = new();

            List<ShoppingCart> shoppingCarts = _unitOfWork.ShoppingCart.GetShoppingCartByUserId(userId);

            //This is filthy af
            shoppingCartVM.ShoppingCarts = shoppingCarts.Select(x => new ShoppingCartWithProduct()
            {
                ProductId = x.ProductId,
                Count = x.Count,
                Id = x.Id,
                UserId = x.UserId,

            }).ToList();

            SetupProductsAndPricesForDisplay(shoppingCartVM);

            return View(shoppingCartVM);
        }
    }
}
