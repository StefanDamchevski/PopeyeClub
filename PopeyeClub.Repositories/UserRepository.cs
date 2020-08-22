using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PopeyeClub.Data;
using PopeyeClub.Repositories.Interfaces;

namespace PopeyeClub.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UserRepository(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public bool CheckForUsername(string username, string userId)
        {
            return userManager.Users.Where(x => x.Id != userId && x.UserName.Equals(username)).Any();
        }

        public async Task CreateAsync(ApplicationUser user, string password)
        {
            await userManager.CreateAsync(user, password);
        }

        public async Task<ApplicationUser> GetByEmailAsync(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }

        public ApplicationUser GetById(string userId)
        {
            return userManager.Users
                .Include(x => x.Posts)
                    .ThenInclude(x => x.PostComments)
                .Include(x => x.Posts)
                    .ThenInclude(x => x.PostLikes)
                .Include(x => x.UserPostSaves)
                .FirstOrDefault(x => x.Id.Equals(userId));
        }

        public async Task<ApplicationUser> GetByUserNameAsync(string userName)
        {
            return await userManager.FindByNameAsync(userName);
        }

        public async Task<IdentityResult> UpdateAsync(ApplicationUser user)
        {
            return await userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string oldPassword, string newPassword)
        {
            return await userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        }

        public async Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
        {
            return await userManager.CheckPasswordAsync(user, password);
        }

        public async Task<ApplicationUser> GetByIdAsync(string userId)
        {
            return await userManager.FindByIdAsync(userId);
        }

        public List<ApplicationUser> GetAll(string userId)
        {
            return userManager.Users.Where(x => x.Id != userId && x.IsDeleted.Equals(false)).ToList();
        }
    }
}
