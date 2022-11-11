using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Wixapol_DataAccess.UnitOfWork.Interface;
using WixapolShop.ErrorModel;

namespace WixapolShop.Areas.Customer.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IActionResult Index()
        {
            var products = _unitOfWork.Product.GetAll().Where(x => x.IsDiscounted).ToList();

            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}