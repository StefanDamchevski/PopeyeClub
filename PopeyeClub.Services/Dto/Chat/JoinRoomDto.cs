using System.Collections.Generic;

namespace PopeyeClub.Services.Dto.Chat
{
    public class JoinRoomDto
    {
        public int ChatRoomId { get; set; }
        public string ChatRoomName { get; set; }
        public string ChatRoomPicture { get; set; }
        public string ChatUserName { get; set; }
        public string UserId { get; set; }
        public List<MessageDto> Messages { get; set; }
    }
}