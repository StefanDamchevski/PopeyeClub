using PopeyeClub.Data;

namespace PopeyeClub.Repositories.Interfaces
{
    public interface IPostCommentRepository
    {
        void Create(PostComment postComment);
        PostComment Get(int postId, string userId, string comment);
        void Delete(int commentId);
        PostComment GetById(int commentId);
    }
}
