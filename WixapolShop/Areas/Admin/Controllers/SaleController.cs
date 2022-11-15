using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Wixapol_DataAccess.Models;
using Wixapol_DataAccess.UnitOfWork.Interface;
using Wixapol_Utils.StaticDetails;

namespace WixapolShop.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class SaleController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public SaleController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Managment()
        {
            return View();
        }
        public IActionResult Edit(int id)
        {
            Sale sale = _unitOfWork.Sale.GetByOrderId(id);
            sale.Order = _unitOfWork.Order.GetById(id);
            sale.SaleDetail = _unitOfWork.SaleDetail.GetBySaleId(sale.Id);
            return View(sale);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Sale sale)
        {
            _unitOfWork.Sale.UpdateSale(sale);
            _unitOfWork.Order.UpdateOrder(sale.Order, sale.OrderId);

            TempData["Success"] = "Sale updated successfully.";

            return RedirectToAction("Edit", "Sale", new { id = sale.Id });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Processing(Sale sale)
        {
            _unitOfWork.Sale.UpdateStatus(sale.Id, SD.StatusInProcess, sale.PaymentStatus);

            TempData["Success"] = "Order status updated successfully.";

            return RedirectToAction("Edit", "Sale", new { id = sale.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Cancelled(Sale sale)
        {
            if (sale.PaymentStatus == SD.PaymentApproved)
            {
                var options = new RefundCreateOptions()
                {
                    Reason = RefundReasons.RequestedByCustomer,
                    PaymentIntent = sale.PaymentIntentId,
                };


                var service = new RefundService();
                if (options.PaymentIntent is not null)
                {
                    Refund refund = service.Create(options);
                }

                _unitOfWork.Sale.UpdateStatus(sale.Id, SD.StatusCancelled, SD.StatusRefunded);
            }
            else
            {
                _unitOfWork.Sale.UpdateStatus(sale.Id, SD.StatusCancelled, SD.StatusCancelled);
            }

            TempData["Success"] = "Order cancelled successfully.";

            return RedirectToAction("Edit", "Sale", new { id = sale.Id });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Shipped(Sale sale)
        {
            sale.ShippingDate = DateTime.UtcNow;
            sale.OrderStatus = SD.StatusShipped;
            _unitOfWork.Sale.UpdateSale(sale);

            TempData["Success"] = "Order shipped successfully.";

            return RedirectToAction("Edit", "Sale", new { id = sale.Id });
        }


        #region API CALLS
        public IActionResult GetAll(string status = "all")
        {
            if (status == "all")
            {
                var sales = _unitOfWork.Sale.GetAll();
                return Json(new { data = sales });
            }
            else
            {
                var sales = _unitOfWork.Sale.GetByStatus(status);
                return Json(new { data = sales });
            }
        }

        #endregion
    }
}
