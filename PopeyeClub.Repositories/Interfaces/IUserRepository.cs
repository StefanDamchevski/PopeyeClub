using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PopeyeClub.Data;

namespace PopeyeClub.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetByEmailAsync(string email);
        Task<ApplicationUser> GetByUserNameAsync(string userName);
        Task<ApplicationUser> GetByIdAsync(string userId);
        Task<IdentityResult> UpdateAsync(ApplicationUser user);
        Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string oldPassword, string newPassword);
        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
        Task CreateAsync(ApplicationUser user, string password);
        ApplicationUser GetById(string userId);
        bool CheckForUsername(string username, string userId);
        List<ApplicationUser> GetAll(string userId);
    }
}
