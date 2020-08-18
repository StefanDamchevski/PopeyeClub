namespace PopeyeClub.Services.Interfaces
{
    public interface IPostSaveService
    {
        void Create(int postId, string userId);
        void Update(int postId, string userId);
    }
}
