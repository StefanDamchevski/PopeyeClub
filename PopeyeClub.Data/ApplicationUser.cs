﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace PopeyeClub.Data
{
    public class ApplicationUser : IdentityUser
    {
        public byte[] ProfilePicture { get; set; }
        [Required]
        public bool IsPrivate { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
        public List<Post> Posts { get; set; }
        public List<PostLike> PostLikes { get; set; }
        public List<PostComment> PostComments { get; set; }
        public List<UserPostSave> UserPostSaves { get; set; }
    }
}