using PopeyeClub.Data;
using PopeyeClub.Repositories.Interfaces;
using PopeyeClub.Services.Dto.Chat;
using PopeyeClub.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace PopeyeClub.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository messageRepository;
        private readonly IUserService userService;

        public MessageService(IMessageRepository messageRepository, IUserService userService)
        {
            this.messageRepository = messageRepository;
            this.userService = userService;
        }

        public async Task<MessageDto> Create(string userId, int chatroomId, string text)
        {
            ApplicationUser user = await userService.GetByIdAsync(userId);
            
            Message message = new Message
            {
                UserId = userId,
                ChatRoomId = chatroomId,
                Text = text,
                DateCreated = DateTime.Now,
            };

            messageRepository.Create(message);

            return new MessageDto
            {
                DateCreated = message.DateCreated.ToString("dd/MM/yyyy hh:mm"),
                Text = message.Text,
                UserName = user.UserName,
                UserImage = Convert.ToBase64String(user.ProfilePicture),
            };
        }
    }
}