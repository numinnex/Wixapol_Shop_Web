using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Wixapol_DataAccess.Models;
using Wixapol_DataAccess.UnitOfWork.Interface;
using Wixapol_Utils;
using Wixapol_Utils.StaticDetails;
using Wixapol_Utils.UtilityModels;
using WixapolShop.Areas.Customer.Models;
using WixapolShop.Areas.Customer.ViewModels;
using WixapolShop.Areas.Identity.Models.Domain;

namespace WixapolShop.Areas.Customer.Controllers
{
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IdentityDatabaseContext _identityDb;

        public OrderController(IUnitOfWork unitOfWork, IdentityDatabaseContext identityDb)
        {
            _unitOfWork = unitOfWork;
            _identityDb = identityDb;
        }

        private string GetUserId()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            return claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        private void SetupSaleDetailInformation(List<ShoppingCart> shoppingCarts, List<SaleDetail> saleDetails)
        {

            foreach (var shoppingCart in shoppingCarts)
            {
                SaleDetail saleDetail = new();
                Product product = _unitOfWork.Product.GetById(shoppingCart.ProductId);

                saleDetail.ProductId = shoppingCart.ProductId;
                saleDetail.Quantity = shoppingCart.Count;
                saleDetail.SubTotal = shoppingCart.SubTotal;
                saleDetail.Tax = shoppingCart.TaxAmount;
                saleDetail.Total = shoppingCart.Total;
                saleDetail.DiscountAmount = shoppingCart.DiscountAmount;


                WarrantyDateTime warrantyOffest = product.ParseWarrantyLength();

                saleDetail.WarrantyExpiration = DateTime.UtcNow.AddYears(warrantyOffest.Years).AddMonths(warrantyOffest.Months);

                saleDetails.Add(saleDetail);
            }
        }

        [Authorize]
        public IActionResult Index(ShoppingCartVM shoppingCartVM)
        {

            string userId = GetUserId();
            Sale sale = new();

            sale.UserId = userId;

            sale.Order = new();

            ApplicationUser user = _identityDb.Set<ApplicationUser>().FirstOrDefault(x => x.Id == userId);

            sale.Order.Adress = user.Adress;
            sale.Order.PhoneNumber = user.PhoneNumber;
            sale.Order.Email = user.Email;
            //sale.Order.City
            sale.Order.Name = user.FirstName;
            sale.Order.PostalCode = user.PostalCode;

            sale.SubTotal = shoppingCartVM.SubTotal;
            sale.Tax = shoppingCartVM.Tax;
            sale.Total = shoppingCartVM.Total;


            return View(sale);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Checkout(Sale sale)
        {
            if (ModelState.IsValid)
            {
                List<ShoppingCart> shoppingCarts = _unitOfWork.ShoppingCart.GetShoppingCartByUserId(sale.UserId);

                sale.SaleDetail = new();

                sale.OrderStatus = SD.StatusPending;
                sale.PaymentStatus = SD.PaymentPending;
                sale.SaleDate = DateTime.UtcNow;

                sale.OrderId = _unitOfWork.Order.CreateOrder(sale.Order);

                int saleId = _unitOfWork.Sale.CreateSale(sale);

                SetupSaleDetailInformation(shoppingCarts, sale.SaleDetail);
                sale.SaleDetail.ForEach(x => x.SaleId = saleId);


            }
            return Ok();
        }
    }
}
