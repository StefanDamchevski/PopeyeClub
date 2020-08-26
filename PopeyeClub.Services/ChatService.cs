using PopeyeClub.Data;
using PopeyeClub.Repositories.Interfaces;
using PopeyeClub.Services.Interfaces;

namespace PopeyeClub.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository chatRepository;
        private readonly IUserService userService;

        public ChatService(IChatRepository chatRepository, IUserService userService)
        {
            this.chatRepository = chatRepository;
            this.userService = userService;
        }

        public void Create(string userId)
        {
            ChatRoom room = new ChatRoom
            {
                RoomName = userService.GetById(userId).UserName,
            };

            chatRepository.Create(room);
        }
    }
}