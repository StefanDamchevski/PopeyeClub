using Microsoft.AspNetCore.Identity;
using PopeyeClub.Data;
using System.Threading.Tasks;

namespace PopeyeClub.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetByEmailAsync(string email);
        Task CreateAsync(ApplicationUser user, string password);
        Task<ApplicationUser> GetByIdAsync(string userId);
        Task<ApplicationUser> GetByUserName(string userName);
        bool CheckForUsername(string username, string userId);
        Task<IdentityResult> UpdateAsync(ApplicationUser user);
        Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string oldPassword, string newPassword);
    }
}
