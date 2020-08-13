using PopeyeClub.Data;

namespace PopeyeClub.Repositories.Interfaces
{
    public interface IPostLikeRepository
    {
        PostLike GetByUserIdAndPostId(int postId, string userId);
        void Create(PostLike newLike);
        void Update(PostLike dbLike);
    }
}
