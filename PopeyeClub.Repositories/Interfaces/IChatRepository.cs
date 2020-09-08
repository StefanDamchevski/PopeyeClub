using PopeyeClub.Data;
using System.Collections.Generic;

namespace PopeyeClub.Repositories.Interfaces
{
    public interface IChatRepository
    {
        void Create(ChatRoom room);
        List<ChatRoom> GetAllRooms(List<string> chatRoomNames);
        ChatRoom GetByRoomName(string firstUserId, string secondUserId);
        ChatRoom GetByRoomName(string chatroomName);
    }
}
