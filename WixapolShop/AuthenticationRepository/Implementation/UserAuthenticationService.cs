using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore.Internal;
using System.Security.Claims;
using System.Security.Policy;
using WixapolShop.Areas.Identity.Models.Domain;
using WixapolShop.Areas.Identity.Models.DTO;
using WixapolShop.IdentityRepository.Interfaces;

namespace WixapolShop.AuthenticationRepository.Implementation
{
    public sealed class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly SignInManager<ApplicationUser> _signInManger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserAuthenticationService(SignInManager<ApplicationUser> signInManger,
            UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _signInManger = signInManger;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<Status> LoginAsync(LoginModel model)
        {

            Status status = new();

            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user is null)
            {
                status.StatusCode = 0;
                status.Message = "Invalid username";
                return status;
            }
            if (!await _userManager.CheckPasswordAsync(user, model.Password))
            {
                status.StatusCode = 0;
                status.Message = "Invalid password";
                return status;
            }

            var signInresult = await _signInManger.PasswordSignInAsync(user, model.Password, model.RememberMe, true);
            if (signInresult.Succeeded)
            {

                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.UserName)
                };

                foreach (var role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }
                status.StatusCode = 1;
                status.Message = "Logged in Successfully";
                return status;
            }
            else if (signInresult.IsLockedOut)
            {
                status.StatusCode = 0;
                status.Message = "User is lockedout";
                return status;
            }
            else
            {
                status.StatusCode = 0;
                status.Message = "Error while log in";
                return status;
            }
        }

        public async Task LogoutAsync()
        {
            await _signInManger.SignOutAsync();
        }

        public async Task<Status> RegistrationAsync(RegisterModel model)
        {

            Status status = new();

            var userExists = await _userManager.FindByNameAsync(model.UserName);

            if (userExists is not null)
            {
                status.StatusCode = 0;
                status.Message = "Username already exists";
                return status;
            }

            ApplicationUser user = new()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                Email = model.Email,
                UserName = model.UserName,
                EmailConfirmed = false,

            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                status.StatusCode = 0;
                status.Message = "User creation failed";
                return status;

            }
            //ROLES
            if (!await _roleManager.RoleExistsAsync(model.Role))
            {
                await _roleManager.CreateAsync(new IdentityRole(model.Role));
                await _userManager.AddToRoleAsync(user, model.Role);
            }
            else
            {
                await _userManager.AddToRoleAsync(user, model.Role);
            }
            status.StatusCode = 1;
            status.Message = "User Registerred Successfully";

            return status;

        }
    }
}
