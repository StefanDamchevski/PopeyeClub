using PopeyeClub.Data;
using PopeyeClub.Services.Dto;
using System.Drawing;
using System.Threading.Tasks;

namespace PopeyeClub.Services.Interfaces
{
    public interface IUserService
    {
        Task<ApplicationUser> GetByEmailAsync(string email);
        Task<Response> CreateAsync(string email, string userName, string password, string image);
        Task<ApplicationUser> GetByIdAsync(string userId);
        Task<Response> UpdateAsync(string userId, string email, string phone, string username, byte[] image);
        Task<Response> ChangePasswordAsync(string userId, string oldPassword, string newPassword);
    }
}
