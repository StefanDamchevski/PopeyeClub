using PopeyeClub.Services.Dto.Chat;
using System.Threading.Tasks;

namespace PopeyeClub.Services.Interfaces
{
    public interface IMessageService
    {
       Task<MessageDto> Create(string userId, int chatroomId, string text);
    }
}
