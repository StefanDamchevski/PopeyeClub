namespace PopeyeClub.Services.Interfaces
{
    public interface IPostLikeService
    {
        void Update(int postId, string userId);
        void Create(int postId, string userId);
    }
}
