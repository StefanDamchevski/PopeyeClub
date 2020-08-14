using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PopeyeClub.Data
{
    public class PostComment
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        [Required]
        public int PostId { get; set; }
        public Post Post { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        public List<CommentLike> CommentLikes { get; set; }
    }
}