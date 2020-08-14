using PopeyeClub.Data;

namespace PopeyeClub.Repositories.Interfaces
{
    public interface IPostLikeRepository
    {
        PostLike Get(int postId, string userId);
        void Create(PostLike newLike);
        void Update(PostLike dbLike);
    }
}
