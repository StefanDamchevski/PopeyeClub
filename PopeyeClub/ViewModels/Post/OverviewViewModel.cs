﻿using System.Collections.Generic;
using PopeyeClub.ViewModels.Comment;
using PopeyeClub.ViewModels.Like;

namespace PopeyeClub.ViewModels.Post
{
    public class OverviewViewModel
    {
        public int PostId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string ProfilePicture { get; set; }
        public string PostImage { get; set; }
        public int DaysAgo { get; set; }
        public List<PostLikeViewModel> PostLikes { get; set; }
        public List<CommentViewModel> PostComments { get; set; }
        public List<UserPostSaveViewModel> UserPostSaves { get; set; }
        public Enums.PostLikeStatus LikeStatus { get; set; }
        public Enums.PostSaveStatus PostSaveStatus { get; set; }
        public int PostCommentsCount { get; set; }
    }
}