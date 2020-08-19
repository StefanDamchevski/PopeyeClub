using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PopeyeClub.Data;
using PopeyeClub.Repositories.Interfaces;
using PopeyeClub.Services.Dto;
using PopeyeClub.Services.Interfaces;

namespace PopeyeClub.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
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

        public async Task<Response> CreateAsync(string email, string userName, string password, byte[] picture)
        {
            ApplicationUser dbUser = await userRepository.GetByUserNameAsync(userName);

            Response response = new Response();

            if (dbUser is null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    Email = email,
                    UserName = userName,
                    ProfilePicture = picture
                };

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

        public async Task RemoveIsDeletedAsync(ApplicationUser user)
        {
            user.IsDeleted = false;
            _ = await userRepository.UpdateAsync(user);
        }

        public async Task<Response> SoftDeleteAsync(string userId, string password)
        {
            ApplicationUser user = await userRepository.GetByIdAsync(userId);
            Response response = new Response();
            if(user != null)
            {
                bool isPasswordValid = await userRepository.CheckPasswordAsync(user, password);
                if (isPasswordValid)
                {
                    user.IsDeleted = true;
                    IdentityResult result = await userRepository.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        response.Succeeded = true;
                    }
                }
                else
                {
                    response.Succeeded = false;
                }
            }
            else
            {
                response.Succeeded = false;
            }
            return response;
        }

        public async Task<Response> UpdateAsync(string userId, string email, string phone, string username, bool isPrivate)
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
                user.IsPrivate = isPrivate;

                if(phone != default)
                {
                    user.PhoneNumber = phone;
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

        public async Task UpdateProfilePictureAsync(byte[] picture, string userId)
        {
            ApplicationUser user = await userRepository.GetByIdAsync(userId);

            if(user != null)
            {
                user.ProfilePicture = picture;
                _ = await userRepository.UpdateAsync(user);
            }
        }
    }
}
