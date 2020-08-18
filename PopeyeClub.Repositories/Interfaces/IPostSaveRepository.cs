using PopeyeClub.Data;

namespace PopeyeClub.Repositories.Interfaces
{
    public interface IPostSaveRepository
    {
        UserPostSave Get(int postId, string userId);
        void Create(UserPostSave postSave);
        void Update(UserPostSave dbPostSave);
    }
}
