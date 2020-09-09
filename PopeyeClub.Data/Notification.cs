using System;
using System.ComponentModel.DataAnnotations;

namespace PopeyeClub.Data
{
    public class Notification
    {
        public int Id { get; set; }
        [Required]
        public string FromUserId { get; set; }
        public ApplicationUser FromUser { get; set; }
        [Required]
        public string ToUserId { get; set; }
        public ApplicationUser ToUser { get; set; }
        public Enums.NotificationType Type { get; set; }
        [Required]
        public DateTime DateSent { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
