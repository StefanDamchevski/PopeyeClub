using System;

namespace PopeyeClub.Data
{
    public class Message
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int ChatRoomId { get; set; }
        public ChatRoom ChatRoom { get; set; }
        public string Text { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
