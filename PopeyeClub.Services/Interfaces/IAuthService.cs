using PopeyeClub.Services.Dto;
using System.Threading.Tasks;

namespace PopeyeClub.Services.Interfaces
{
    public interface IAuthService
    {
        Task<Response> SignInAsync(string email, string password);
        Task SignOutAsync();
    }
}