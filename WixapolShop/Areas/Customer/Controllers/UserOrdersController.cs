using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Wixapol_DataAccess.UnitOfWork.Interface;
using WixapolShop.Areas.Customer.ViewModels;

namespace WixapolShop.Areas.Customer.Controllers
{
    public class UserOrdersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserOrdersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private string GetUserId()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            return claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
        public IActionResult Orders(string status = "all")
        {
            //should've used seperate query on each instead of linq + magic strings + ratio
            var sales = _unitOfWork.Sale.GetByUserId(GetUserId());
            if (status == "approved")
            {
                sales = sales.Where(x => x.OrderStatus.ToLower() == "approved").ToList();
            }
            else if (status == "shipped")
            {
                sales = sales.Where(x => x.OrderStatus.ToLower() == "shipped").ToList();
            }
            else if (status == "processing")
            {
                sales = sales.Where(x => x.OrderStatus.ToLower() == "processing").ToList();
            }
            else if (status == "cancelled")
            {
                sales = sales.Where(x => x.OrderStatus.ToLower() == "cancelled").ToList();
            }
            else if (status == "completed")
            {
                sales = sales.Where(x => x.OrderStatus.ToLower() == "completed").ToList();
            }
            else if (status == "pending")
            {
                sales = sales.Where(x => x.OrderStatus.ToLower() == "pending").ToList();
            }
            return View(sales);
        }
        public IActionResult OrderDetails(int saleId)
        {
            SaleDisplayVM saleVM = new();

            saleVM.SaleId = saleId;
            saleVM.Sale = _unitOfWork.Sale.GetById(saleId);
            saleVM.Sale.SaleDetail = _unitOfWork.SaleDetail.GetBySaleId(saleId);
            saleVM.Sale.Order = _unitOfWork.Order.GetById(saleVM.Sale.OrderId);

            foreach (var saleDetail in saleVM.Sale.SaleDetail)
            {
                saleVM.Products.Add(_unitOfWork.Product.GetById(saleDetail.ProductId));
            }

            return View(saleVM);
        }

    }
}
