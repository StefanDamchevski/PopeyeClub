using System.Collections.Generic;
using System.Threading.Tasks;
using PopeyeClub.Data;
using PopeyeClub.Services.Dto;

namespace PopeyeClub.Services.Interfaces
{
    public interface IUserService
    {
        Task<ApplicationUser> GetByEmailAsync(string email);
        Task<ApplicationUser> GetByNameAsync(string roomName);
        Task<ApplicationUser> GetByIdAsync(string userId);
        Task<Response> CreateAsync(string email, string userName, string password, byte[] image);
        Task<Response> UpdateAsync(string userId, string email, string phone, string username, bool isPrivate);
        Task<Response> ChangePasswordAsync(string userId, string oldPassword, string newPassword);
        Task<Response> SoftDeleteAsync(string userId, string password);
        Task UpdateProfilePictureAsync(byte[] picture, string userId);
        Task RemoveIsDeletedAsync(ApplicationUser user);
        List<ApplicationUser> GetAll(string userId);
        ApplicationUser GetById(string userId);
    }
}
