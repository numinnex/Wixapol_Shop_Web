using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Wixapol_DataAccess.UnitOfWork.Interface;
using Wixapol_Utils.StaticDetails;

namespace WixapolShop.Views.Shared.Components.ShoppingCart
{
    public class ShoppingCartCountViewComponent : ViewComponent
    {

        private readonly IUnitOfWork _unitOfWork;

        public ShoppingCartCountViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if (claim is not null)
            {
                if (HttpContext.Session.GetInt32(SD.SessionCartCount) is not null)
                {
                    return View("Default", HttpContext.Session.GetInt32(SD.SessionCartCount));
                }
                else
                {
                    HttpContext.Session.SetInt32(SD.SessionCartCount,
                        _unitOfWork.ShoppingCart.GetShoppingCartByUserId(claim.Value).Count);

                    return View("Default", (HttpContext.Session.GetInt32(SD.SessionCartCount)));
                }
            }
            else
            {
                HttpContext.Session.Clear();
                return View("Default", 0);
            }
        }

    }
}
