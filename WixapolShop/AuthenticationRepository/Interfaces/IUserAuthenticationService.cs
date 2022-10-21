using WixapolShop.Areas.Identity.Models.DTO;

namespace WixapolShop.IdentityRepository.Interfaces
{
    public interface IUserAuthenticationService
    {
        Task<Status> LoginAsync(LoginModel model);
        Task<Status> RegistrationAsync(RegisterModel model);
        Task LogoutAsync();

    }
}
