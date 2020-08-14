namespace PopeyeClub.Services.Interfaces
{
    public interface IPostCommentService
    {
        void Create(int postId, string comment, string userId);
    }
}
