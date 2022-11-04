using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;
using Wixapol_DataAccess.Models;
using Wixapol_DataAccess.UnitOfWork.Interface;
using Wixapol_Utils;
using WixapolShop.Areas.Customer.Models;
using System.Text.Json;
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
            ProductSearchVM productsVM = new();
            query.Phrase = query.Phrase.Trim();

            if (query.Categoryid == -1)
            {
                productsVM.query = query;
                productsVM.Products = _unitOfWork.Product.GetAllByNamePattern(query.Phrase);

                if (productsVM.Products.Count > 0)
                {
                    //Load Specifications for displaying
                    LoadSpecificationsForProduct(productsVM);
                    //Setting up filtering models
                    SetupFilteringModels(productsVM);
                    productsVM.MaxPrice = (int)Math.Ceiling(productsVM.ProductsByPrice.Last().Key);
                }

                return View(productsVM);
            }
            else
            {
                productsVM.query = query;
                productsVM.Products = _unitOfWork.Product.GetAllByCategoryAndName(query.Categoryid, query.Phrase);

                if (productsVM.Products.Count > 0)
                {
                    //Load Specifications for displaying
                    LoadSpecificationsForProduct(productsVM);
                    //Setting up filtering models
                    SetupFilteringModels(productsVM);
                    productsVM.MaxPrice = (int)Math.Ceiling(productsVM.ProductsByPrice.Last().Key);
                }
                return View(productsVM);
            }
        }
        public IActionResult FilterProductsCart(ProductSearchVM productsVM)
        {

            productsVM.Products = _unitOfWork.Product.GetAllByNamePattern(productsVM.query.Phrase);

            if (productsVM.CategoryIdList is not null)
            {
                productsVM.Products = productsVM.Products.Where(x => productsVM.CategoryIdList.Contains(x.Category.Id)).ToList();
            }
            if (productsVM.ProducentIdList is not null)
            {
                productsVM.Products = productsVM.Products.Where(x => productsVM.ProducentIdList.Contains(x.Producent.Name)).ToList();
            }
            if (productsVM.MinPrice >= 0 && productsVM.MaxPrice >= 0)
            {
                productsVM.Products = productsVM.Products.Where(x => x.RetailPrice >= productsVM.MinPrice && x.RetailPrice <= productsVM.MaxPrice).ToList();
            }

            LoadSpecificationsForProduct(productsVM);
            SetupFilteringModels(productsVM);
            productsVM.MaxPrice = (int)Math.Ceiling(productsVM.ProductsByPrice.Last().Key);

            return View("ProductsCart", productsVM);

        }

        private void LoadSpecificationsForProduct(ProductSearchVM productsVM)
        {
            //productsVM.Products.ForEach(x =>
            //x.BaseSpec = _unitOfWork.Specification.GetById(x.BaseSpecId, Specification.Basic) as BaseSpecification);

            productsVM.Products.ForEach(x =>
            x.AdvancedSpec = _unitOfWork.Specification.GetById(x.AdvancedSpecId, Specification.Advanced) as AdvancedSpecification);

            //productsVM.Products.ForEach(x =>
            //x.PhysicalSpec = _unitOfWork.Specification.GetById(x.PhysicalSpecId, Specification.Physical) as PhysicalSpecification);
        }
        private void SetupFilteringModels(ProductSearchVM productsVM)
        {
            productsVM.ProductsByCategory = productsVM.Products.GroupBy(x => x.Category.Id, p => p.Category.Name).ToDictionary(x => x.Key, p => p.ToList());

            productsVM.ProductsByProducent = productsVM.Products.GroupBy(x => x.Producent.Name, p => p.Name).ToDictionary(x => x.Key, p => p.ToList());

            productsVM.ProductsByPrice = new SortedDictionary<double, List<string>>(productsVM.Products.GroupBy(x => x.RetailPrice, p => p.Name).ToDictionary(x => x.Key, p => p.ToList()));
        }

    }
}
