using PopeyeClub.Data;

namespace PopeyeClub.Repositories.Interfaces
{
    public interface IPostCommentRepository
    {
        void Create(PostComment postComment);
    }
}
