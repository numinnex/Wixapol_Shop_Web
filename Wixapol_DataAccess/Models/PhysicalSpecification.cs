using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wixapol_DataAccess.Models.Abstractions;

namespace Wixapol_DataAccess.Models
{
    public class PhysicalSpecification : ISpecification
    {
        public int? Id { get; set; }
        [Display(Name = "Physical Specification Values")]
        [MinLength(2)]
        public string? SpecName { get; set; }
        [Display(Name = "Physical Specification Details")]
        [MinLength(2)]
        public string? SpecValue { get; set; }
    }
}
