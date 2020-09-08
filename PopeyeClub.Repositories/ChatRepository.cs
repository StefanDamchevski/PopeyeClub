using Microsoft.EntityFrameworkCore;
using PopeyeClub.Data;
using PopeyeClub.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace PopeyeClub.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly ApplicationDbContext context;

        public ChatRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Create(ChatRoom room)
        {
            context.ChatRooms.Add(room);
            context.SaveChanges();
        }

        public List<ChatRoom> GetAllRooms(List<string> chatRoomNames)
        {
            return context.ChatRooms.Where(x => chatRoomNames.Contains(x.RoomName)).ToList();
        }

        public ChatRoom GetByRoomName(string firstUserId, string secondUserId)
        {
            return context.ChatRooms
                .Include(x => x.Messages)
                    .ThenInclude(x => x.User)
                .FirstOrDefault(x => x.RoomName.Equals(firstUserId + secondUserId) || x.RoomName.Equals(secondUserId + firstUserId));
        }

        public ChatRoom GetByRoomName(string chatroomName)
        {
            return context.ChatRooms.Include(x => x.Messages).ThenInclude(x => x.User).FirstOrDefault(x => x.RoomName.Equals(chatroomName));
        }
    }
}
