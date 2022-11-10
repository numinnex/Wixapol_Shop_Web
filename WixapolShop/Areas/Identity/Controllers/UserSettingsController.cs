using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WixapolShop.Areas.Identity.Models.DTO;
using WixapolShop.AuthenticationRepository.Interfaces;
using WixapolShop.IdentityRepository.Interfaces;

namespace WixapolShop.Areas.Identity.Controllers
{
    [Authorize]
    public class UserSettingsController : Controller
    {
        private readonly IUserSettingsService _service;

        private string GetUserId()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            return claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
        public UserSettingsController(IUserSettingsService service)
        {
            _service = service;
        }
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.ChangePassword(GetUserId(), model.OldPassword, model.NewPassword);

                if (result.StatusCode == 1)
                {
                    TempData["success"] = result.Message;
                }
                else
                {
                    TempData["error"] = result.Message;
                }
                return View(model);
            }
            return View(model);
        }
    }
}
