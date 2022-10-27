using Microsoft.AspNetCore.Mvc;
using Wixapol_DataAccess.Models;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Producent producent)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Producent.CreateProducent(producent);
                TempData["success"] = "Successfully created producent";
                return RedirectToAction("Managment");
            }
            TempData["failure"] = "Couldnt create producent";
            return View(producent);

        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }
            _unitOfWork.Producent.DeleteProducent(id);
            TempData["success"] = "Successfully deleted producent";
            return RedirectToAction("Managment");

        }
        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }
            var producent = _unitOfWork.Producent.GetById(id);
            return View(producent);
        }
        public IActionResult Edit(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }
            var producent = _unitOfWork.Producent.GetById(id);
            return View(producent);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Producent producent)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Producent.UpdateProducent(producent);
                TempData["success"] = "Successfully updated producent";
                return RedirectToAction("Managment");

            }
            TempData["failure"] = "Couldnt update producent";
            return View(producent);

        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Managment()
        {
            var producents = _unitOfWork.Producent.GetAll();
            return View(producents);
        }

    }
}
