using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using PopeyeClub.Data;
using PopeyeClub.Repositories.Interfaces;
using PopeyeClub.Services.Dto;
using PopeyeClub.Services.Helpers;
using PopeyeClub.Services.Interfaces;
using System.Threading.Tasks;

namespace PopeyeClub.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository userRepository;
        private readonly IConfiguration configuration;

        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            this.userRepository = userRepository;
            this.configuration = configuration;
        }

        public async Task<Response> ChangePasswordAsync(string userId, string oldPassword, string newPassword)
        {
            ApplicationUser user = await userRepository.GetByIdAsync(userId);
            Response response = new Response();

            if(user != null)
            {
                IdentityResult result = await userRepository.ChangePasswordAsync(user, oldPassword, newPassword);
                if (result.Succeeded)
                {
                    response.Succeeded = true;
                }
                else
                {
                    response.Succeeded = false;
                    response.ErrorMessage = "Update Failed";
                }
            }
            else
            {
                response.Succeeded = false;
                response.ErrorMessage = "User does not exist";
            }
            return response;
        }

        public async Task<Response> CreateAsync(string email, string userName, string password, string picture)
        {
            ApplicationUser dbUser = await userRepository.GetByUserName(userName);

            Response response = new Response();

            if (dbUser == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.Email = email;
                user.UserName = userName;
                user.ProfilePicture = picture.ToByteArray();

                await userRepository.CreateAsync(user, password);

                response.Succeeded = true;
            }
            else
            {
                response.Succeeded = false;
                response.ErrorMessage = $"Username {userName} alredy exists";
            }

            return response;
        }

        public async Task<ApplicationUser> GetByEmailAsync(string email)
        {
            return await userRepository.GetByEmailAsync(email);
        }

        public async Task<ApplicationUser> GetByIdAsync(string userId)
        {
            return await userRepository.GetByIdAsync(userId);
        }

        public async Task<Response> UpdateAsync(string userId, string email, string phone, string username, byte[] image)
        {
            Response response = new Response();

            bool exists = userRepository.CheckForUsername(username, userId);

            if (exists)
            {
                response.Succeeded = false;
                response.ErrorMessage = $"User with {username} already exists";
                return response;
            }

            ApplicationUser user = await userRepository.GetByIdAsync(userId);

            if (user != null)
            {
                user.Email = email;
                user.UserName = username;
                if(phone != default)
                {
                    user.PhoneNumber = phone;
                }
                if(image == null)
                {
                    user.ProfilePicture = configuration["DefaultProfilePicture"].ToByteArray();
                }
                else
                {
                    user.ProfilePicture = image;
                }

                IdentityResult result = await userRepository.UpdateAsync(user);
                if (result.Succeeded)
                {
                    response.Succeeded = true;
                }
                else
                {
                    response.ErrorMessage = "Update Failed";
                }
            }
            else
            {
                response.Succeeded = false;
                response.ErrorMessage = "User does not exist";
            }

            return response;
        }
    }
}
