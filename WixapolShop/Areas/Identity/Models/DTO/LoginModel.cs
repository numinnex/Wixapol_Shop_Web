using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WixapolShop.Areas.Identity.Models.DTO
{
    public sealed class LoginModel
    {
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }


    }
}
