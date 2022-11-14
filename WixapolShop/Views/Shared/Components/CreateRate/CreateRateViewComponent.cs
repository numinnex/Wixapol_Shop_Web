using Microsoft.AspNetCore.Mvc;
using Wixapol_DataAccess.Models;
using WixapolShop.Areas.Customer.Models;
using WixapolShop.Views.CreationModels;

namespace WixapolShop.Views.Shared.Components.CreateRate
{
    public class CreateRateViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int productId)
        {
            RateCM rateCM = new();
            rateCM.Rate = new();
            rateCM.ProductId = productId;

            return await Task.FromResult(View(rateCM));
        }
    }
}
