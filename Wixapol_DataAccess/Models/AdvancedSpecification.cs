using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wixapol_DataAccess.Models
{
    public class AdvancedSpecification
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Advanced Specification Values")]
        public string AdvancedSpecName { get; set; }
        [Required]
        [Display(Name = "Advanced Specification Details")]
        public string AdvancedSpecValues { get; set; }
    }
}
