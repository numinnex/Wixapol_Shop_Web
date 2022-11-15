using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wixapol_DataAccess.Models;
using Wixapol_DataAccess.Models.Abstractions;
using Wixapol_Utils.UtilityModels;

namespace Wixapol_Utils
{
    public static class ExtensionMethods
    {
        public static (List<string> SpecNames, List<string> SpecValues) SpecificationParser(this Product product, ISpecification specification)
        {

            List<string> specNames = new();
            List<string> specValues = new();

            if (specification is null)
            {
                return (specNames, specValues);
            }

            specNames = specification.SpecName.Split(';').ToList();
            specValues = specification.SpecValue.Split(';').ToList();

            return (specNames, specValues);

        }

        public static WarrantyDateTime ParseWarrantyLength(this Product product)
        {
            int years = 0;
            int months = 0;

            string warrantyDate = product.WarrantyLength;

            for (int i = 0; i < warrantyDate.Length; ++i)
            {
                if (warrantyDate[i] == 'y' && i != warrantyDate.Length - 1)
                {
                    years = int.Parse(warrantyDate[0..i]);
                    months = int.Parse(warrantyDate[(i + 1)..(warrantyDate.Length - 1)]);
                    break;
                }
                else if (warrantyDate[i] == 'y')
                {
                    years = int.Parse(warrantyDate[0..i]);
                }
                else if (warrantyDate[i] == 'm')
                {
                    months = int.Parse(warrantyDate[0..i]);
                }
            }

            WarrantyDateTime warrantyOffest = new() { Years = years, Months = months };

            return warrantyOffest;
        }

        public static double CalculateDiscountedPrice(this Product product)
        {

            if (product.IsDiscounted)
            {
                double? price = product.RetailPrice - (product.RetailPrice * (product.DiscountAmount / 100));
                return Math.Round((double)price, 2, MidpointRounding.AwayFromZero);
            }
            else
            {
                return product.RetailPrice;
            }

        }

    }

}
