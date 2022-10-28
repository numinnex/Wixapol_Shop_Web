using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Wixapol_DataAccess.Models.Abstractions
{
    public interface ISpecification
    {
        public int? Id { get; set; }
        public string SpecName { get; set; }
        public string SpecValue { get; set; }
    }
}
