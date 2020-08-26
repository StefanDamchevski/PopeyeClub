using System.Collections.Generic;
using PopeyeClub.ViewModels.Comment;
using PopeyeClub.ViewModels.Like;

namespace PopeyeClub.ViewModels.Post
{
    public class PostDetailsViewModel
    {
        public int PostId { get; set; }
        public string PostImage { get; set; }
        public string UserId { get; set; }
        public string CreatedBy { get; set; }
        public string UserImage { get; set; }
        public int DaysAgo { get; set; }
        public List<PostLikeViewModel> PostLikes { get; set; }
        public List<CommentViewModel> Comments { get; set; }
        public ViewModelEnums.PostLikeStatus PostLikeStatus { get; set; }
        public List<UserPostsViewModel> UserPosts { get; set; }
        public List<UserPostSaveViewModel> UserPostSaves { get; set; }
        public ViewModelEnums.PostSaveStatus PostSaveStatus { get; set; }
    }
}
