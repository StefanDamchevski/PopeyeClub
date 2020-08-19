using System.Threading.Tasks;
using PopeyeClub.Services.Dto;

namespace PopeyeClub.Services.Interfaces
{
    public interface IAuthService
    {
        Task<Response> SignInAsync(string email, string password);
        Task SignOutAsync();
    }
}