using System.ComponentModel.DataAnnotations;

namespace PopeyeClub.Data
{
    public class UserPostSave
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        [Required]
        public int PostId { get; set; }
        public Post Post { get; set; }
        [Required]
        public bool IsSaved { get; set; }
    }
}
