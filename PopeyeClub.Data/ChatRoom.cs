using System.ComponentModel.DataAnnotations;

namespace PopeyeClub.Data
{
    public class ChatRoom
    {
        public int Id { get; set; }
        [Required]
        public string RoomName { get; set; }
    }
}