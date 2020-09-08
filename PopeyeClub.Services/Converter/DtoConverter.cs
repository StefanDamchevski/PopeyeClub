using PopeyeClub.Data;
using PopeyeClub.Services.Dto.Chat;
using System;
using System.Linq;

namespace PopeyeClub.Services.Converter
{
    public static class DtoConverter
    {
        internal static ChatRoomDto ToChatRoomDto(this ChatRoom chatRoom)
        {
            return new ChatRoomDto
            {
                ChatRoomId = chatRoom.Id,
                RoomName = chatRoom.RoomName,
            };
        }

        internal static JoinRoomDto ToJoinRoomDto(this ChatRoom chatRoom)
        {
            return new JoinRoomDto
            {
                ChatRoomId = chatRoom.Id,
                ChatRoomName = chatRoom.RoomName,
                Messages = chatRoom.Messages?.Select(x => x.ToMessageDto()).ToList(),
            };
        }

        internal static MessageDto ToMessageDto(this Message message)
        {
            return new MessageDto
            {
                DateCreated = message.DateCreated.ToString("dd/MM/yyyy hh:mm"),
                Text = message.Text,
                UserName = message.User.UserName,
                UserImage = Convert.ToBase64String(message.User.ProfilePicture),
            };
        }
    }
}