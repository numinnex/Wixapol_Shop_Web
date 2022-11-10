using Microsoft.AspNetCore.Identity;
using WixapolShop.Areas.Identity.Models.Domain;
using WixapolShop.Areas.Identity.Models.DTO;
using WixapolShop.AuthenticationRepository.Interfaces;

namespace WixapolShop.AuthenticationRepository.Implementation
{
    public class UserSettingsService : IUserSettingsService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserSettingsService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Status> ChangePassword(string userId, string oldPassword, string newPassword)
        {
            Status status = new();
            var user = await _userManager.FindByIdAsync(userId);

            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);

            if (result.Succeeded)
            {
                status.StatusCode = 1;
                status.Message = "Changed user password successfully";

            }
            else
            {
                foreach (var error in result.Errors)
                {
                    status.StatusCode = 0;
                    status.Message = error.Description;
                }
            }

            return status;
        }
    }
}
