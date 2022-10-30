using Microsoft.AspNetCore.Mvc;
using WixapolShop.Areas.Customer.Models;

namespace WixapolShop.Views.Shared.Components.SearchForm
{
    public class SearchFormViewComponent : ViewComponent
    {

        public async Task<IViewComponentResult> InvokeAsync() =>
        await Task.FromResult(View(new SearchModel()));
    }
}
