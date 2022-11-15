using Microsoft.AspNetCore.Mvc;
using Wixapol_DataAccess.UnitOfWork.Interface;
using Wixapol_DataAccess.Models;
using Microsoft.AspNetCore.Authorization;

namespace WixapolShop.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            if (id is not null || id != 0)
            {
                _unitOfWork.Category.DeleteCategory(id);
                TempData["success"] = "Category deleted successfully";
                return RedirectToAction("Managment");
            }
            return BadRequest();
        }
        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _unitOfWork.Category.GetById(id);
            return View(categoryFromDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.UpdateCategory(category);
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Managment");
            }
            return View(category);

        }
        public IActionResult Edit(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _unitOfWork.Category.GetById(id);
            return View(categoryFromDb);
        }
        public IActionResult Managment()
        {
            var categories = _unitOfWork.Category.GetAll();

            return View(categories);

        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.CreateCategory(category);
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Managment");
            }
            return View(category);
        }
    }
}
