using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Wixapol_DataAccess.UnitOfWork.Interface;
using Wixapol_Utils.StaticDetails;

namespace WixapolShop.Views.Shared.Components.ShoppingCartMoney
{
    public class ShoppingCartMoneyViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;

        public ShoppingCartMoneyViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if (claim is not null)
            {
                if (HttpContext.Session.GetString(SD.SessionCartMoney) is not null)
                {
                    return View("Default", HttpContext.Session.GetString(SD.SessionCartMoney));
                }
                else
                {
                    HttpContext.Session.SetString(SD.SessionCartMoney,
                        Math.Round((double)_unitOfWork.ShoppingCart.GetShoppingCartByUserId(claim.Value).Sum(x => x.Total), 2, MidpointRounding.AwayFromZero).ToString());

                    return View("Default", HttpContext.Session.GetString(SD.SessionCartMoney));
                }
            }
            else
            {
                HttpContext.Session.Clear();
                return View("Default", "0.00");
            }
        }

    }
}
