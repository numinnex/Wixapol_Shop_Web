using WixapolShop.Areas.Identity.Models.DTO;

namespace WixapolShop.AuthenticationRepository.Interfaces
{
    public interface IUserSettingsService
    {
        Task<Status> ChangePassword(string userId, string oldPassword, string newPassword);
    }
}
