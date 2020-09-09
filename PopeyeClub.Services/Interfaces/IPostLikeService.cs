namespace PopeyeClub.Services.Interfaces
{
    public interface IPostLikeService
    {
        void Update(int postId, string userId);
        string Create(int postId, string userId, string currentUserName);
    }
}
