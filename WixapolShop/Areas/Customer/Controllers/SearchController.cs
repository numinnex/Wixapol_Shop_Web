using Microsoft.AspNetCore.Mvc;
using Wixapol_DataAccess.Models;
using Wixapol_DataAccess.UnitOfWork.Interface;
using WixapolShop.Areas.Customer.Models;

namespace WixapolShop.Areas.Customer.Controllers
{
    public class SearchController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public SearchController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult ProductsCart(SearchModel query)
        {
            if (query.Categoryid == -1)
            {
                var products = _unitOfWork.Product.GetAllByNamePattern(query.Phrase);
                return View(products);
            }
            else
            {
                var products = _unitOfWork.Product.GetAllByCategory(query.Categoryid).Where(x => x.Name.Contains(query.Phrase)).ToList();
                return View(products);
            }


        }
    }
}
