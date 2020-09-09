using PopeyeClub.Data;

namespace PopeyeClub.Services.Interfaces
{
    public interface IPostCommentService
    {
        void Create(int postId, string comment, string userId);
        PostComment Get(int postId, string userId, string comment);
        void Delete(int commentId);
        PostComment GetById(int commentId);
    }
}
