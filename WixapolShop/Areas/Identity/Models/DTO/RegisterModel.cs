using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WixapolShop.Areas.Identity.Models.DTO
{
    public class RegisterModel
    {
        public string? Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DisplayName("Username")]
        public string UserName { get; set; }
        [Required]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*[#$^+=!*()@%&]).{6,}$",
            ErrorMessage = "Minimum length 6 and must contain 1 Uppercase,1 lowercase, 1 special character and 1 digit")]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match!")]
        public string ConfirmPassword { get; set; }
        public string? Role { get; set; }
    }
}
