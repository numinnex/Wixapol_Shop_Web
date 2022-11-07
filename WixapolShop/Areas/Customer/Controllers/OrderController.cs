using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Wixapol_DataAccess.Models;
using Wixapol_DataAccess.UnitOfWork.Interface;
using Wixapol_Utils;
using Wixapol_Utils.UtilityModels;
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

        private void SetupSaleDetailInformation(ShoppingCartVM shoppingCartVM, List<SaleDetail> saleDetails)
        {


            foreach (var shoppingCart in shoppingCartVM.ShoppingCarts)
            {
                SaleDetail saleDetail = new();

                saleDetail.ProductId = shoppingCart.ProductId;
                saleDetail.Quantity = shoppingCart.Count;
                saleDetail.SubTotal = shoppingCart.SubTotal;
                saleDetail.Tax = shoppingCart.TaxAmount;
                saleDetail.Total = shoppingCart.Total;
                saleDetail.DiscountAmount = shoppingCart.DiscountAmount;

                Product product = _unitOfWork.Product.GetById(shoppingCart.ProductId);

                WarrantyDateTime warrantyOffest = product.ParseWarrantyLength();

                saleDetail.WarrantyExpiration = DateTime.UtcNow.AddYears(warrantyOffest.Years).AddMonths(warrantyOffest.Months);

                saleDetails.Add(saleDetail);
            }
        }

        [Authorize]
        public IActionResult Index(ShoppingCartVM shoppingCartVM)
        {

            Sale sale = new();

            sale.Order = new();

            ApplicationUser user = _identityDb.Users.Where(x => x.Id == GetUserId()).FirstOrDefault();

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
    }
}
