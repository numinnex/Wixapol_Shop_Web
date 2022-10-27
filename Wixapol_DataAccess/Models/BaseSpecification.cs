﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wixapol_DataAccess.Models.Abstractions;

namespace Wixapol_DataAccess.Models
{
    public class BaseSpecification : ISpecification
    {
        public int? Id { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(300)]
        [Display(Name = "Base Specification Values")]
        public string SpecName { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(800)]
        [Display(Name = "Base Specification Details")]
        public string SpecValues { get; set; }
    }
}