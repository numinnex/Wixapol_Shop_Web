using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WixapolShop.Areas.Identity.Models.Domain
{
    public sealed class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Adress { get; set; }
        public string? PostalCode { get; set; }



    }
}
