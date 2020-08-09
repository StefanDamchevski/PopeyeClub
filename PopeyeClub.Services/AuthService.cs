using Microsoft.AspNetCore.Identity;
using PopeyeClub.Data;
using PopeyeClub.Services.Dto;
using PopeyeClub.Services.Interfaces;
using System.Threading.Tasks;

namespace PopeyeClub.Services
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IUserService userService;

        public AuthService(SignInManager<ApplicationUser> signInManager, IUserService userService)
        {
            this.signInManager = signInManager;
            this.userService = userService;
        }
        public async Task<Response> SignInAsync(string email, string password)
        {
            ApplicationUser user = await userService.GetByEmailAsync(email);

            Response response = new Response();

            if (user != null)
            {
                SignInResult result = await signInManager.PasswordSignInAsync(user, password, false, false);

                if (result.Succeeded)
                {
                    response.Succeeded = true;
                }
                else
                {
                    response.Succeeded = false;
                    response.ErrorMessage = "Log In Failed";
                }
            }
            else
            {
                response.Succeeded = false;
                response.ErrorMessage = "User doesn`t exist";
            }

            return response;
        }

        public async Task SignOutAsync()
        {
            await signInManager.SignOutAsync();
        }
    }
}
