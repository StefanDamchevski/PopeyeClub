using PopeyeClub.ViewModels.Comment;
using PopeyeClub.ViewModels.Like;
using System.Collections.Generic;

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
        public Enums.PostLikeStatus PostLikeStatus { get; set; }

    }
}
