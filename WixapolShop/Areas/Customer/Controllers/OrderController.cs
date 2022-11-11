using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe.Checkout;
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Summary(ShoppingCartVM shoppingCartVM)
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
            //TODO - clean this shit up
            if (ModelState.IsValid)
            {
                List<ShoppingCart> shoppingCarts = _unitOfWork.ShoppingCart.GetShoppingCartByUserId(sale.UserId);

                sale.SaleDetail = new();

                sale.OrderStatus = SD.StatusPending;
                sale.PaymentStatus = SD.PaymentPending;
                sale.SaleDate = DateTime.UtcNow;
                sale.PaymentDate = DateTime.UtcNow;


                sale.OrderId = _unitOfWork.Order.CreateOrder(sale.Order);

                int saleId = _unitOfWork.Sale.CreateSale(sale);

                SetupSaleDetailInformation(shoppingCarts, sale.SaleDetail);
                sale.SaleDetail.ForEach(x =>
                {
                    x.SaleId = saleId;
                    _unitOfWork.SaleDetail.CreateSaleDetail(x);
                });

                var domain = "https://localhost:7068/";

                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string>
                    {
                        "card",
                    },

                    LineItems = new List<SessionLineItemOptions>(),
                    Mode = "payment",
                    SuccessUrl = domain + $"customer/order/OrderConfirmation?id={saleId}",
                    CancelUrl = domain + "customer/shoppingcart/index"
                };
                foreach (var shoppingCart in shoppingCarts)
                {
                    Product product = _unitOfWork.Product.GetById(shoppingCart.ProductId);
                    var sesionLineItem = new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)((Math.Round(product.CalculateDiscountedPrice() + CalculateTax(product), 2, MidpointRounding.AwayFromZero)) * 100.00),
                            TaxBehavior = "inclusive",
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = product.Name
                            },
                        },
                        Quantity = shoppingCart.Count,
                    };
                    options.LineItems.Add(sesionLineItem);
                }

                var service = new SessionService();
                Session session = service.Create(options);

                _unitOfWork.Sale.UpdateSessionInformation(saleId, session.Id, session.PaymentIntentId);

                Response.Headers.Add("Location", session.Url);
                return new StatusCodeResult(303);

                //return View(sale);
            }

            return View(sale);
        }

        [Authorize]
        public IActionResult OrderConfirmation(int id)
        {

            //TODO - clean this shit up too
            var sale = _unitOfWork.Sale.GetById(id);
            var service = new SessionService();

            Session session = service.Get(sale.SessionId);

            SaleDisplayVM saleVM = new();

            if (session.PaymentStatus.ToLower() == "paid")
            {

                //update status
                _unitOfWork.Sale.UpdateStatus(id, SD.StatusApproved, SD.PaymentApproved);
                _unitOfWork.Sale.UpdateSessionInformation(id, session.Id, session.PaymentIntentId);

                sale = _unitOfWork.Sale.GetById(id);
                sale.SaleDetail = _unitOfWork.SaleDetail.GetBySaleId(id);

                foreach (var saleDetail in sale.SaleDetail)
                {
                    _unitOfWork.Product.DecreaseQuantity(saleDetail.ProductId, saleDetail.Quantity);
                    saleVM.Products.Add(_unitOfWork.Product.GetById(saleDetail.ProductId));
                }

                _unitOfWork.ShoppingCart.DeleteShoppingCartsByUserId(sale.UserId);

                HttpContext.Session.Clear();

                saleVM.Sale = sale;
                saleVM.Sale.Order = _unitOfWork.Order.GetById(saleVM.Sale.OrderId);
                saleVM.SaleId = id;

                saleVM.EmailAdress = _identityDb.Users.FirstOrDefault(x => x.Id == sale.UserId).Email;


                return View(saleVM);
            }
            return View(new SaleDisplayVM());
        }
        private double CalculateTax(Product product)
        {
            return product.CalculateDiscountedPrice() * (product.TaxRate / 100);
        }
    }
}
