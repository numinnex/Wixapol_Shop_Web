using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Wixapol_DataAccess.Models;
using Wixapol_DataAccess.UnitOfWork.Interface;
using WixapolShop.Areas.Admin.Models;

namespace WixapolShop.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitofWork;
        private readonly IWebHostEnvironment _hostEnviroment;

        public ProductController(IUnitOfWork unitofWork, IWebHostEnvironment hostEnviroment)
        {
            _unitofWork = unitofWork;
            _hostEnviroment = hostEnviroment;
        }

        private string ParseFile(ProductVM obj, IFormFile file)
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
                return @"images\products" + fileName + extension;
            }
            return "Failure";

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductVM obj, IFormFile file)
        {

            var cultureInfo = new CultureInfo("pl-PL");
            cultureInfo.NumberFormat.NumberDecimalSeparator = ".";
            cultureInfo.NumberFormat.CurrencyDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = cultureInfo;

            ModelState.Remove("Product.RetailPrice");
            if (!Decimal.TryParse(obj.Product.RetailPriceStringify, out decimal price))
            {
                ModelState.AddModelError("RetailPrice", "Invalid Retail Price");
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
                if (!String.IsNullOrWhiteSpace(obj.PhysicalSpec.SpecName) && !String.IsNullOrWhiteSpace(obj.PhysicalSpec.SpecValues))
                {
                    obj.Product.PhysicalSpecId = _unitofWork.Specification.CreateSpecification(obj.PhysicalSpec, Specification.Physical);
                }

                _unitofWork.Product.CreateProduct(obj.Product);
                TempData["success"] = "Successfully created product";

                return Ok();


            }
            return View(obj);
        }
        public IActionResult Create()
        {
            ProductVM productVM = new ProductVM()
            {
                Product = new Product(),
                CategoryList = _unitofWork.Category.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList(),
                ProducentList = _unitofWork.Producent.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList(),
            };

            return View(productVM);
        }
    }
}
