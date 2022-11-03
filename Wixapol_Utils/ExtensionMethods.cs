using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wixapol_DataAccess.Models;
using Wixapol_DataAccess.Models.Abstractions;

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

    }

}
