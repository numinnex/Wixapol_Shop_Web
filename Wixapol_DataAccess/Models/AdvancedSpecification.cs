using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wixapol_DataAccess.Models.Abstractions;

namespace Wixapol_DataAccess.Models
{
    public sealed class AdvancedSpecification : ISpecification
    {
        public int? Id { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(300)]
        [Display(Name = "Advanced Specification Values")]
        public string SpecName { get; set; }
        [Required]
        [MinLength(3)]
        [Display(Name = "Advanced Specification Details")]
        public string SpecValue { get; set; }
    }
}
