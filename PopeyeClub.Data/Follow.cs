using System.ComponentModel.DataAnnotations;

namespace PopeyeClub.Data
{
    public class Follow
    {
        public int Id { get; set; }
        [Required]
        public string FromUserId { get; set; }
        public ApplicationUser FromUser { get; set; }
        [Required]
        public string ToUserId { get; set; }
        public ApplicationUser ToUser { get; set; }
        public bool IsFollowed { get; set; }
        public bool IsSent { get; set; }
    }
}
