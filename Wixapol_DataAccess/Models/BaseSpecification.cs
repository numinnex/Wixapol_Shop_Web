using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wixapol_DataAccess.Models
{
    public class BaseSpecification
    {
        public int Id { get; set; }
        [Required]
        [Display(Name ="Base Specification Values")]
        public string BaseSpecName { get; set; }
        [Required]
        [MaxLength(800)]
        [Display(Name ="Base Specification Details")]
        public string BaseSpecValues { get; set; }
    }
}
