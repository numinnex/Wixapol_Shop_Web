using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Wixapol_DataAccess.UnitOfWork.Interface;
using WixapolShop.Areas.Customer.ViewModels;
using WixapolShop.Areas.Identity.Models.Domain;
using WixapolShop.Migrations;

namespace WixapolShop.Areas.Customer.Controllers
{
    [Authorize]
    public class UserProfileController : Controller
    {
        private readonly IdentityDatabaseContext _db;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserProfileController(IdentityDatabaseContext db, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _db = db;
            _userManager = userManager;
        }

        private string GetUserId()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            return claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        public IActionResult Profile()
        {
            return View();
        }
        public IActionResult Settings()
        {
            ProfileSettingsVM vm = new ProfileSettingsVM();
            var user = _db.Users.Where(x => x.Id == GetUserId()).FirstOrDefault();

            vm.User.Adress = user.Adress;
            vm.User.FirstName = user.FirstName;
            vm.User.LastName = user.LastName;
            vm.User.UserName = user.UserName;
            vm.User.Email = user.Email;
            vm.User.PhoneNumber = user.PhoneNumber;
            vm.User.PostalCode = user.PostalCode;

            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Settings(ProfileSettingsVM model)
        {
            if (ModelState.IsValid)
            {
                var user = _db.Users.SingleOrDefault(x => x.Id == GetUserId());
                if (user is not null)
                {
                    user.UserName = model.User.UserName;
                    user.NormalizedUserName = model.User.UserName.ToUpper();

                    await _userManager.SetUserNameAsync(user, model.User.UserName);

                    user.FirstName = model.User.FirstName;
                    user.LastName = model.User.LastName;
                    user.Email = model.User.Email;
                    user.Adress = model.User.Adress;
                    user.PhoneNumber = model.User.PhoneNumber;
                    user.PostalCode = model.User.PostalCode;

                    _db.SaveChanges();
                    TempData["success"] = "successfully changed user settings!";
                    return View(model);
                }
                else
                {
                    TempData["error"] = "Couldnt find user";
                    return View(model);
                }
            }
            return View(model);
        }
    }
}
