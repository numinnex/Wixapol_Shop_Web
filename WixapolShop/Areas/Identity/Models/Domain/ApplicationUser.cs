using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WixapolShop.Areas.Identity.Models.Domain
{
    public sealed class ApplicationUser : IdentityUser
    {

        [Display(Name = "First Name")]
        public string? FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }
        public string? Adress { get; set; }
        [Display(Name = "Postal Code")]
        [RegularExpression(pattern: @"^\d{1,2}-\d{1,3}$", ErrorMessage = "Enter valid postal code!")]
        public string? PostalCode { get; set; }

    }
}
