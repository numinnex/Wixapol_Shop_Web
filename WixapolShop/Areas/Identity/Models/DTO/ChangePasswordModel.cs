using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WixapolShop.Areas.Identity.Models.DTO
{
    public class ChangePasswordModel
    {
        [Required]
        [Display(Name = "Old Password")]
        public string OldPassword { get; set; }
        [Required]
        [PasswordPropertyText]
        [Display(Name = "New Password")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*[#$^+=!*()@%&]).{6,}$",
            ErrorMessage = "Password is too weak!")]
        public string NewPassword { get; set; }
        [Required]
        [PasswordPropertyText]
        [Display(Name = "Confirm New Password")]
        [Compare("NewPassword", ErrorMessage = "Passwords dont match!")]
        public string NewPasswordConfirmed { get; set; }


    }
}
