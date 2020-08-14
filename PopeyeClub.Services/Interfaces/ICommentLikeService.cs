namespace PopeyeClub.Services.Interfaces
{
    public interface ICommentLikeService
    {
        void Create(int commentId, string userId);
        void Update(int commentId, string userId);
    }
}
