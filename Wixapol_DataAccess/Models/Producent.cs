using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wixapol_DataAccess.Models
{
    public class Producent
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ProducentCode { get; set; }
        [Required]
        public string EAN { get; set; }
    }
}
