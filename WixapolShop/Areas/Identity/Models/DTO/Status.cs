using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;

namespace WixapolShop.Areas.Identity.Models.DTO
{
    public sealed class Status
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
