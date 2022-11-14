using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Wixapol_DataAccess.Models;
using Wixapol_DataAccess.UnitOfWork.Interface;
using Wixapol_Utils;
using Wixapol_Utils.StaticDetails;
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
        private void UpdateOrCreateShoppingCart(string userId, int productId, int count)
        {
            ShoppingCart existingCart = _unitOfWork.ShoppingCart.GetShoppingCartByUserId(userId).
                Where(s => s.ProductId == productId).FirstOrDefault();

            if (existingCart is null)
            {
                _unitOfWork.ShoppingCart.CreateShoppingCart(new ShoppingCart
                {
                    ProductId = productId,
                    UserId = userId,
                    Count = count
                });
            }
            else
            {
                _unitOfWork.ShoppingCart.UpdateShoppingCartProductCount(existingCart.Id, existingCart.Count + count);
            }
        }
        public IActionResult Test()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Setup(ProductDisplayVM productVM)
        {

            string userId = GetUserId();

            UpdateOrCreateShoppingCart(userId, productVM.Product.Id, productVM.Count);
            return RedirectToAction(nameof(Index));
        }
        [Authorize]
        public IActionResult AddToCartSingular(int productId)
        {

            string userId = GetUserId();
            UpdateOrCreateShoppingCart(userId, productId, 1);
            return RedirectToAction(nameof(Index));
        }
        [Authorize]
        public IActionResult RemoveFromCart(int cartId)
        {
            _unitOfWork.ShoppingCart.DeleteShoppingCart(cartId);

            return RedirectToAction(nameof(Index));
        }


        [Authorize]
        public IActionResult IncrementCount(int cartId)
        {
            _unitOfWork.ShoppingCart.IncrementShoppingCartProductCount(cartId);
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public IActionResult DecrementCount(int cartId)
        {
            _unitOfWork.ShoppingCart.DecrementShoppingCartProductCount(cartId);
            return RedirectToAction(nameof(Index));
        }

        #region Helper Methods
        private double CalculateTax(Product product, int count)
        {
            return Math.Round(product.CalculateDiscountedPrice() * count *
                (product.TaxRate / 100), 2, MidpointRounding.ToEven);
        }

        private void UpdateTotalSubTotalAndTax(ShoppingCartVM shoppingCartVM)
        {

            foreach (var shoppingCart in shoppingCartVM.ShoppingCarts)
            {

                double subTotal = Math.Round(shoppingCart.Product.CalculateDiscountedPrice() * shoppingCart.Count, 2, MidpointRounding.AwayFromZero);
                double taxAmount = CalculateTax(shoppingCart.Product, shoppingCart.Count);
                double total = Math.Round(subTotal + taxAmount, 2, MidpointRounding.AwayFromZero);

                shoppingCart.SubTotal = subTotal;
                shoppingCart.TaxAmount = taxAmount;
                shoppingCart.Total = total;

                _unitOfWork.ShoppingCart.UpdateShoppingCart(new ShoppingCart
                { Id = shoppingCart.Id, SubTotal = subTotal, TaxAmount = taxAmount, Total = total });
            }

            shoppingCartVM.SubTotal = Math.Round(shoppingCartVM.ShoppingCarts.Sum(x => x.SubTotal), 2, MidpointRounding.AwayFromZero);
            shoppingCartVM.Tax = Math.Round(shoppingCartVM.ShoppingCarts.Sum(x => x.TaxAmount), 2, MidpointRounding.AwayFromZero);
            shoppingCartVM.Total = Math.Round(shoppingCartVM.ShoppingCarts.Sum(x => x.Total), 2, MidpointRounding.AwayFromZero);
        }

        private void SetupProductsForDisplay(ShoppingCartVM shoppingCartVM)
        {
            foreach (var shoppingCart in shoppingCartVM.ShoppingCarts)
            {
                shoppingCart.Product = (_unitOfWork.Product.GetById(shoppingCart.ProductId));
            }
        }
        private void CheckIfCountDoesntExceedQuantityInStock(List<ShoppingCartWithProduct> shoppingCartProducts)
        {
            foreach (var shoppingCart in shoppingCartProducts)
            {
                if (shoppingCart.Count > shoppingCart.Product.QuantityInStock)
                {
                    TempData["failure"] = $"We dont have {shoppingCart.Count} of {string.Join(" ", shoppingCart.Product.Producent.Name, shoppingCart.Product.Name)} in stock!";
                    shoppingCart.Count = shoppingCart.Product.QuantityInStock;
                    _unitOfWork.ShoppingCart.UpdateShoppingCartProductCount(shoppingCart.Id, shoppingCart.Count);
                }
            }
        }
        private void CheckIfCountIsntNegative(List<ShoppingCartWithProduct> shoppingCartProducts)
        {
            foreach (var shoppingCart in shoppingCartProducts)
            {
                if (shoppingCart.Count < 1)
                {
                    TempData["failure"] = $"{string.Join(" ", shoppingCart.Product.Producent.Name, shoppingCart.Product.Name)} count cant be smaller than 1!";
                    shoppingCart.Count = 1;
                    _unitOfWork.ShoppingCart.UpdateShoppingCartProductCount(shoppingCart.Id, shoppingCart.Count);
                }
            }

        }
        #endregion Helper Methods

        [Authorize]
        public IActionResult Index()
        {
            string userId = GetUserId();

            ShoppingCartVM shoppingCartVM = new();

            List<ShoppingCart> shoppingCarts = _unitOfWork.ShoppingCart.GetShoppingCartByUserId(userId);

            HttpContext.Session.SetInt32(SD.SessionCartCount, shoppingCarts.Count);


            //This is filthy af
            shoppingCartVM.ShoppingCarts = shoppingCarts.Select(x => new ShoppingCartWithProduct()
            {
                ProductId = x.ProductId,
                Count = x.Count,
                Id = x.Id,
                UserId = x.UserId,

            }).ToList();

            SetupProductsForDisplay(shoppingCartVM);
            CheckIfCountDoesntExceedQuantityInStock(shoppingCartVM.ShoppingCarts);
            CheckIfCountIsntNegative(shoppingCartVM.ShoppingCarts);
            UpdateTotalSubTotalAndTax(shoppingCartVM);

            HttpContext.Session.SetString(SD.SessionCartMoney,
                Math.Round((double)_unitOfWork.ShoppingCart.GetShoppingCartByUserId(userId).Sum(x => x.Total), 2, MidpointRounding.AwayFromZero).ToString());

            return View(shoppingCartVM);
        }
    }
}
