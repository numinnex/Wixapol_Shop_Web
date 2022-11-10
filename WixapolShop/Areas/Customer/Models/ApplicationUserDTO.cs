using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WixapolShop.Areas.Customer.Models
{
    public sealed class ApplicationUserDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string? PhoneNumber { get; set; }

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
