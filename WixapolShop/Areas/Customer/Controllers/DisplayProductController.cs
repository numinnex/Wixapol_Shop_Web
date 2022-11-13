using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using Wixapol_DataAccess.Models;
using Wixapol_DataAccess.UnitOfWork.Interface;
using WixapolShop.Areas.Customer.ViewModels;
using WixapolShop.Areas.Identity.Models.Domain;

namespace WixapolShop.Areas.Customer.Controllers
{
    public class DisplayProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IdentityDatabaseContext _db;

        public DisplayProductController(IUnitOfWork unitOfWork, IdentityDatabaseContext db)
        {
            _db = db;
            _unitOfWork = unitOfWork;
        }

        private int CalculateAvgRate(ProductDisplayVM productVM)
        {
            return (int)Math.Round((double)productVM.Rates.Sum(x => x.RateValue) / productVM.Rates.Count);
        }
        private double CalculatePercentRecommend(ProductDisplayVM productVM)
        {
            double result = (double)productVM.Rates.Where(x => x.RateValue >= 4).ToList().Count / (double)productVM.Rates.Count;
            return Math.Round(result * 100.00, 1, MidpointRounding.ToEven);
        }

        private Dictionary<int, int> CalculateIndividualRatePercent(List<Rate> rates)
        {
            rates.Sort((x, y) => y.RateValue.CompareTo(x.RateValue));
            var groupRatesByValue = rates.GroupBy(x => x.RateValue).ToDictionary(x => x.Key, x => x.ToList().Count);

            var totalRates = groupRatesByValue.Values.Sum(x => x);

            foreach (var (key, value) in groupRatesByValue)
            {
                groupRatesByValue[key] = (int)(Math.Round(((double)value / totalRates), 1, MidpointRounding.ToZero) * 100);
            }

            return groupRatesByValue;
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


            productVM.Rates = _unitOfWork.Rate.GetByProductId(productVM.Product.Id);

            productVM.AvgRate = CalculateAvgRate(productVM);


            foreach (var rate in productVM.Rates)
            {
                rate.UserName = _db.Users.SingleOrDefault(x => x.Id == rate.UserId).UserName;
            }


            productVM.PercentRecommend = CalculatePercentRecommend(productVM);
            productVM.IndividualRatePercent = CalculateIndividualRatePercent(productVM.Rates);

            return View(productVM);
        }
    }
}
