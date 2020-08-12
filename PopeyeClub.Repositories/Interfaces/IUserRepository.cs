using Microsoft.AspNetCore.Identity;
using PopeyeClub.Data;
using System.Threading.Tasks;

namespace PopeyeClub.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetByEmailAsync(string email);
        Task<ApplicationUser> GetByIdAsync(string userId);
        Task<ApplicationUser> GetByUserNameAsync(string userName);
        Task<IdentityResult> UpdateAsync(ApplicationUser user);
        Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string oldPassword, string newPassword);
        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
        Task CreateAsync(ApplicationUser user, string password);
        bool CheckForUsername(string username, string userId);
    }
}
