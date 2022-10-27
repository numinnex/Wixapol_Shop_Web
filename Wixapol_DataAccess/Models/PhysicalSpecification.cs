using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wixapol_DataAccess.Models
{
    public class PhysicalSpecification
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Physical Specification Values")]
        public string PhysicalSpecName { get; set; }
        [Required]
        [Display(Name = "Physical Specification Details")]
        public string PhysicalSpecValues { get; set; }
    }
}
