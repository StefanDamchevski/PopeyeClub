using PopeyeClub.Data;
using PopeyeClub.Repositories.Interfaces;
using PopeyeClub.Services.Converter;
using PopeyeClub.Services.Dto.Chat;
using PopeyeClub.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace PopeyeClub.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository chatRepository;
        private readonly IUserService userService;
        private readonly IFollowService followService;

        public ChatService(IChatRepository chatRepository, IUserService userService, IFollowService followService)
        {
            this.chatRepository = chatRepository;
            this.userService = userService;
            this.followService = followService;
        }

        public void Create(string userId, string currentUserId)
        {
            ChatRoom room = new ChatRoom
            {
                RoomName = userId + currentUserId,
            };

            chatRepository.Create(room);
        }

        public List<ChatRoomDto> GetAllRooms(string currentUserId)
        {
            List<Follow> follows = followService.GetAllUserRelations(currentUserId);

            List<ChatRoomDto> chatRoomDtos = new List<ChatRoomDto>();

            foreach (Follow follow in follows)
            {
                ChatRoomDto dto = chatRoomDtos.FirstOrDefault(x => x.DisplayRoomName.Equals(follow.FromUser.UserName) || x.DisplayRoomName.Equals(follow.ToUser.UserName));
                if (dto == null)
                {
                    ChatRoomDto chatRoom = chatRepository.GetByRoomName(follow.FromUserId, follow.ToUserId).ToChatRoomDto();
                    if (chatRoom.RoomName.Contains(follow.FromUserId) && follow.FromUserId != currentUserId)
                    {
                        chatRoom.DisplayRoomName = follow.FromUser.UserName;
                        chatRoom.UserImage = Convert.ToBase64String(follow.FromUser.ProfilePicture);
                        chatRoom.UserId = follow.FromUserId;
                    }
                    if (chatRoom.RoomName.Contains(follow.ToUserId) && follow.ToUserId != currentUserId)
                    {
                        chatRoom.DisplayRoomName = follow.ToUser.UserName;
                        chatRoom.UserImage = Convert.ToBase64String(follow.ToUser.ProfilePicture);
                        chatRoom.UserId = follow.ToUserId;
                    }
                    if (chatRoom != null)
                    {
                        chatRoomDtos.Add(chatRoom);
                    }
                }
            }

            return chatRoomDtos;
        }

        public async Task<JoinRoomDto> GetByRoomName(string chatroomName, string currentUserId)
        {
            JoinRoomDto room = chatRepository.GetByRoomName(chatroomName).ToJoinRoomDto();

            string firstUserId = room.ChatRoomName.Substring(0, room.ChatRoomName.Length / 2);
            string secondUserId = room.ChatRoomName.Substring(firstUserId.Length);

            if(currentUserId != firstUserId)
            {
                ApplicationUser user = await userService.GetByIdAsync(firstUserId);
                room.ChatRoomPicture = Convert.ToBase64String(user.ProfilePicture);
                room.ChatUserName = user.UserName;
                room.UserId = user.Id;
            }
            else
            {
                ApplicationUser user = await userService.GetByIdAsync(secondUserId);
                room.ChatRoomPicture = Convert.ToBase64String(user.ProfilePicture);
                room.ChatUserName = user.UserName;
                room.UserId = user.Id;
            }

            return room;
        }
    }
}