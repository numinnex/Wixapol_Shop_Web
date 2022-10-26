using Microsoft.AspNetCore.Mvc;
using Wixapol_DataAccess.UnitOfWork.Interface;

namespace WixapolShop.Areas.Admin.Controllers
{
    public class ProducentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProducentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
