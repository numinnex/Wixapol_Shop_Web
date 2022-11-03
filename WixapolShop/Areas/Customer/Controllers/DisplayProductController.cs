using Microsoft.AspNetCore.Mvc;
using Wixapol_DataAccess.Models;
using Wixapol_DataAccess.UnitOfWork.Interface;
using WixapolShop.Areas.Customer.ViewModels;

namespace WixapolShop.Areas.Customer.Controllers
{
    public class DisplayProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DisplayProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Display(int productId)
        {
            ProductDisplayVM productVM = new();

            productVM.Product = _unitOfWork.Product.GetById(productId);

            int baseSpecId = productVM.Product.BaseSpecId;
            int advancedSpecId = productVM.Product.AdvancedSpecId;
            int? physicalSpecId = productVM.Product.PhysicalSpecId;


            productVM.Product.BaseSpec = _unitOfWork.Specification.GetById(baseSpecId, Specification.Basic) as BaseSpecification;
            productVM.Product.AdvancedSpec = _unitOfWork.Specification.GetById(advancedSpecId, Specification.Advanced) as AdvancedSpecification;
            if (physicalSpecId != 0)
            {
                productVM.Product.PhysicalSpec = _unitOfWork.Specification.GetById(physicalSpecId, Specification.Physical) as PhysicalSpecification;
            }


            return View(productVM);
        }
    }
}
