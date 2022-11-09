using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Wixapol_DataAccess.Models;
using Wixapol_DataAccess.UnitOfWork.Interface;
using WixapolShop.Areas.Admin.Models;

namespace WixapolShop.Areas.Admin.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitofWork;
        private readonly IWebHostEnvironment _hostEnviroment;

        public ProductController(IUnitOfWork unitofWork, IWebHostEnvironment hostEnviroment)
        {
            _unitofWork = unitofWork;
            _hostEnviroment = hostEnviroment;
        }

        private string ParseFile(ProductCreationVM obj, IFormFile file)
        {
            string wwwRootPath = _hostEnviroment.WebRootPath;

            if (file is not null)
            {
                string fileName = Guid.NewGuid().ToString();
                string uploadPath = Path.Combine(wwwRootPath, @"images\products");
                string extension = Path.GetExtension(file.FileName);

                if (obj.Product.ProductImage is not null)
                {
                    string oldImagePath = Path.Combine(wwwRootPath, obj.Product.ProductImage.TrimStart('\\'));

                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                using (var fileStream = new FileStream(Path.Combine(uploadPath, fileName + extension), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                return @"\images\products\" + fileName + extension;
            }
            return "Failure";

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductCreationVM obj, IFormFile file)
        {

            // Cheat to fix incorrect decimal value seperation
            var cultureInfo = new CultureInfo("pl-PL");
            cultureInfo.NumberFormat.NumberDecimalSeparator = ".";
            cultureInfo.NumberFormat.CurrencyDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentUICulture = cultureInfo;
            System.Threading.Thread.CurrentThread.CurrentCulture = cultureInfo;

            ModelState.Remove("Product.RetailPrice");
            if (!Double.TryParse(obj.Product.RetailPriceStringify, out double price))
            {
                ModelState.AddModelError("Product.RetailPriceStringify", "Invalid Product Price");
            }
            else
            {
                obj.Product.RetailPrice = price;
            }

            if (ModelState.IsValid)
            {
                string imagePath = ParseFile(obj, file);

                if (imagePath != "Failure")
                {
                    obj.Product.ProductImage = imagePath;
                }

                obj.Product.BaseSpecId = _unitofWork.Specification.CreateSpecification(obj.BaseSpec, Specification.Basic);
                obj.Product.AdvancedSpecId = _unitofWork.Specification.CreateSpecification(obj.AdvancedSpec, Specification.Advanced);
                if (!String.IsNullOrWhiteSpace(obj.PhysicalSpec.SpecName) || !String.IsNullOrWhiteSpace(obj.PhysicalSpec.SpecValue))
                {
                    obj.Product.PhysicalSpecId = _unitofWork.Specification.CreateSpecification(obj.PhysicalSpec, Specification.Physical);
                }

                _unitofWork.Product.CreateProduct(obj.Product);
                TempData["success"] = "Successfully created product";

                return RedirectToAction("Managment");


            }
            return View(obj);
        }
        public IActionResult Create()
        {
            ProductCreationVM productVM = new ProductCreationVM()
            {
                Product = new Product(),
                CategoryList = _unitofWork.Category.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList(),
                ProducentList = _unitofWork.Producent.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name + " " + (x.ProducentCode),
                    Value = x.Id.ToString()
                }).ToList(),
            };

            return View(productVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductCreationVM obj, IFormFile? file)
        {
            // Cheat to fix incorrect decimal value seperation

            ModelState.Remove("Product.RetailPrice");
            if (!Double.TryParse(obj.Product.RetailPriceStringify, out double price))
            {
                ModelState.AddModelError("Product.RetailPriceStringify", "Invalid Product Price");
            }
            else
            {
                obj.Product.RetailPrice = price;
            }
            obj.BaseSpec.Id = obj.Product.BaseSpecId;
            obj.AdvancedSpec.Id = obj.Product.AdvancedSpecId;
            obj.PhysicalSpec.Id = obj.Product.PhysicalSpecId;

            if (ModelState.IsValid)
            {
                string imagePath = ParseFile(obj, file);

                if (imagePath != "Failure")
                {
                    obj.Product.ProductImage = imagePath;
                }

                _unitofWork.Specification.UpdateSpecification(obj.BaseSpec, Specification.Basic);
                _unitofWork.Specification.UpdateSpecification(obj.AdvancedSpec, Specification.Advanced);
                if (obj.PhysicalSpec.Id is null && obj.PhysicalSpec is not null)
                {
                    obj.Product.PhysicalSpecId = _unitofWork.Specification.CreateSpecification(obj.PhysicalSpec, Specification.Physical);
                }
                else
                {
                    _unitofWork.Specification.UpdateSpecification(obj.PhysicalSpec, Specification.Physical);
                }

                if ((obj.PhysicalSpec.SpecValue is null || obj.PhysicalSpec.SpecName is null) && obj.PhysicalSpec.Id is not null)
                {
                    obj.Product.PhysicalSpecId = null;
                }

                _unitofWork.Product.UpdateProduct(obj.Product);
                TempData["success"] = "Successfully updated product";

                return RedirectToAction("Managment");
            }
            return View(obj);

        }
        public IActionResult Edit(int? id)
        {
            ProductCreationVM productVM = new ProductCreationVM()
            {

                Product = _unitofWork.Product.GetById(id),


                CategoryList = _unitofWork.Category.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList(),
                ProducentList = _unitofWork.Producent.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name + (x.ProducentCode),
                    Value = x.Id.ToString()
                }).ToList(),
            };
            productVM.Product.ProducentId = productVM.Product.Producent.Id;
            productVM.Product.CategoryId = productVM.Product.Category.Id;
            productVM.Product.RetailPriceStringify = productVM.Product.RetailPrice.ToString();

            productVM.BaseSpec = _unitofWork.Specification.GetById(productVM.Product.BaseSpecId, Specification.Basic) as BaseSpecification;
            productVM.AdvancedSpec = _unitofWork.Specification.GetById(productVM.Product.AdvancedSpecId, Specification.Advanced) as AdvancedSpecification;
            productVM.PhysicalSpec = _unitofWork.Specification.GetById(productVM.Product.PhysicalSpecId, Specification.Physical) as PhysicalSpecification;

            return View(productVM);

        }

        public IActionResult Managment()
        {
            return View();
        }


        #region API CALLS
        public IActionResult GetAll()
        {
            var products = _unitofWork.Product.GetAll();
            return Json(new { data = products });
        }
        public IActionResult Delete(int? id)
        {
            var product = _unitofWork.Product.GetById(id);
            _unitofWork.Product.DeleteProduct(id);

            _unitofWork.Specification.DeleteSpecification(product.BaseSpecId, Specification.Basic);
            _unitofWork.Specification.DeleteSpecification(product.AdvancedSpecId, Specification.Advanced);
            if (product.PhysicalSpecId is not null)
            {
                _unitofWork.Specification.DeleteSpecification(product.PhysicalSpecId, Specification.Physical);
            }

            return Json(new { success = true, message = "Delete successful" });
        }


        #endregion

    }
}
