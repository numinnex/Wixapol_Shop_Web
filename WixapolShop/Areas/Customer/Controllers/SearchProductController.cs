using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;
using Wixapol_DataAccess.Models;
using Wixapol_DataAccess.UnitOfWork.Interface;
using WixapolShop.Areas.Customer.Models;
using WixapolShop.Areas.Customer.ViewModels;

namespace WixapolShop.Areas.Customer.Controllers
{
    public class SearchProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public SearchProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult ProductsCart(SearchModel query)
        {
            if (query.Categoryid == -1)
            {
                ProductSearchVM productsVM = new();

                productsVM.query = query;
                productsVM.Products = _unitOfWork.Product.GetAllByNamePattern(query.Phrase);

                productsVM.ProductsByCategory = new SortedDictionary<int, List<Product>>
                    (productsVM.Products.GroupBy(key => key.Category.Id).ToDictionary(p => p.Key, p => p.ToList()));

                productsVM.ProductsByProducent = new SortedDictionary<int, List<Product>>
                    (productsVM.Products.GroupBy(key => key.Producent.Id).ToDictionary(p => p.Key, p => p.ToList()));

                productsVM.ProductsByPrice = new SortedDictionary<double, List<Product>>
                    (productsVM.Products.GroupBy(key => key.RetailPrice).ToDictionary(p => p.Key, p => p.ToList()));

                return View(productsVM);

            }
            else
            {
                var products = _unitOfWork.Product.GetAllByCategory(query.Categoryid).Where(x => x.Name.Contains(query.Phrase)).ToList();
                return View(products);
            }


        }
    }
}
