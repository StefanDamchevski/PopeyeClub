using PopeyeClub.ViewModels.Like;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace PopeyeClub.ViewModels.Comment
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string UserImage { get; set; }
        public string UserName { get; set; }
        public List<CommentLikeViewModel> CommentLikes { get; set; }
        public Enums.CommentLikeStatus CommentLikeStatus { get; set; }
    }
}
