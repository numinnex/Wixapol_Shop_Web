using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WixapolShop.Areas.Identity.Models.DTO;
using WixapolShop.ErrorModel;
using WixapolShop.IdentityRepository.Interfaces;

namespace WixapolShop.Areas.Identity.Controllers
{
    public class UserAuthenticationController : Controller
    {
        private readonly IUserAuthenticationService _service;

        public UserAuthenticationController(IUserAuthenticationService service)
        {
            _service = service;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _service.LoginAsync(model);

            if (result.StatusCode == 1)
            {
                return RedirectToAction("Index", "Home", new { area = "Customer" });
            }
            else
            {
                //Would be better to return different status codes instead of parsing the message
                if (result.Message.Contains("password"))
                {
                    TempData["msgFailurePassword"] = result.Message;
                }
                else if (result.Message.Contains("username"))
                {
                    TempData["msgFailureUsername"] = result.Message;
                }

            }
            return RedirectToAction(nameof(Login));
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            model.Role = "user";

            var result = await _service.RegistrationAsync(model);

            if (result.StatusCode == 0)
            {
                TempData["msgFailure"] = result.Message;
            }
            else if (result.StatusCode == 1)
            {
                TempData["msgSuccess"] = result.Message;
            }

            return RedirectToAction(nameof(Register));
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _service.LogoutAsync();
            return RedirectToAction("Index", "Home", new { area = "Customer" });
        }


        public async Task<IActionResult> Reg()
        {
            var model = new RegisterModel()
            {
                UserName = "admin2",
                Name = "Admin Name",
                Email = "admin2@admin.com",
                Password = "Admin@12345#",
                Role = "admin"
            };

            var result = await _service.RegistrationAsync(model);

            return Ok(result);
        }




    }
}
