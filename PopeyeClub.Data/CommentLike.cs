using System.ComponentModel.DataAnnotations;

namespace PopeyeClub.Data
{
    public class CommentLike
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        [Required]
        public int CommentId { get; set; }
        public PostComment Comment { get; set; }
        [Required]
        public bool Status { get; set; }
    }
}
