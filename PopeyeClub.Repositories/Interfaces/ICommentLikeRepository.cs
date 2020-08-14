using PopeyeClub.Data;

namespace PopeyeClub.Repositories.Interfaces
{
    public interface ICommentLikeRepository
    {
        CommentLike Get(int commentId, string userId);
        void Create(CommentLike commentLike);
        void Update(CommentLike dbCommentLike);
    }
}
