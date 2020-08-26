using PopeyeClub.ViewModels.Like;
using System.Collections.Generic;

namespace PopeyeClub.ViewModels.Comment
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string UserId { get; set; }
        public string UserImage { get; set; }
        public string UserName { get; set; }
        public int DaysAgo{ get; set; }
        public int PostId { get; set; }
        public int CommentLikesCount { get; set; }
        public List<CommentLikeViewModel> CommentLikes { get; set; }
        public ViewModelEnums.CommentLikeStatus CommentLikeStatus { get; set; }
    }
}
