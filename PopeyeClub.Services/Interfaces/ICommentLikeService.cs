namespace PopeyeClub.Services.Interfaces
{
    public interface ICommentLikeService
    {
        string Create(int commentId, string userId, string currentUserName);
        void Update(int commentId, string userId);
    }
}
