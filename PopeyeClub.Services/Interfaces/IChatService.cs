using PopeyeClub.Services.Dto.Chat;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PopeyeClub.Services.Interfaces
{
    public interface IChatService
    {
        List<ChatRoomDto> GetAllRooms(string currentUserId);
        Task<JoinRoomDto> GetByRoomName(string chatroomName, string currentUserId);
        void Create(string userId, string currentUserId);
    }
}