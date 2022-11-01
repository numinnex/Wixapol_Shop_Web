using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;
using Wixapol_DataAccess.Models;
using Wixapol_DataAccess.UnitOfWork.Interface;
using Wixapol_Utils;
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


                //Load Specifications for displaying
                LoadSpecificationsForProduct(productsVM);
                //Setting up filtering models
                SetupFilteringModels(productsVM);

                // TODO - handle this better
                if (productsVM.Products.Count > 0)
                {
                    productsVM.MaxPrice = (int)productsVM.ProductsByPrice.Keys.Last();
                }
                return View(productsVM);
            }
            else
            {
                var products = _unitOfWork.Product.GetAllByCategory(query.Categoryid).Where(x => x.Name.Contains(query.Phrase)).ToList();
                return View(products);
            }
        }

        private void LoadSpecificationsForProduct(ProductSearchVM productsVM)
        {
            productsVM.Products.ForEach(x =>
            x.BaseSpec = _unitOfWork.Specification.GetById(x.BaseSpecId, Specification.Basic) as BaseSpecification);
            productsVM.Products.ForEach(x =>
            x.AdvancedSpec = _unitOfWork.Specification.GetById(x.AdvancedSpecId, Specification.Advanced) as AdvancedSpecification);
            productsVM.Products.ForEach(x =>
            x.PhysicalSpec = _unitOfWork.Specification.GetById(x.PhysicalSpecId, Specification.Physical) as PhysicalSpecification);
        }
        private void SetupFilteringModels(ProductSearchVM productsVM)
        {
            productsVM.ProductsByCategory = new SortedDictionary<int, List<Product>>
                (productsVM.Products.GroupBy(key => key.Category.Id).ToDictionary(p => p.Key, p => p.ToList()));

            productsVM.ProductsByProducent = new SortedDictionary<string, List<Product>>
                (productsVM.Products.GroupBy(key => key.Producent.Name).ToDictionary(p => p.Key, p => p.ToList()));

            productsVM.ProductsByPrice = new SortedDictionary<double, List<Product>>
                (productsVM.Products.GroupBy(key => key.RetailPrice).ToDictionary(p => p.Key, p => p.ToList()));

        }

    }
}
