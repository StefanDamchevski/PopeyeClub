using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PopeyeClub.Data
{
    public class Post
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        [Required]
        public byte[] PostImage { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        public List<PostLike> PostLikes { get; set; }
        public List<PostComment> PostComments { get; set; }
        public List<UserPostSave> UserPostSaves { get; set; }
    }
}
