using PopeyeClub.Data;
using PopeyeClub.Repositories.Interfaces;
using PopeyeClub.Services.Interfaces;

namespace PopeyeClub.Services
{
    public class PostSaveService : IPostSaveService
    {
        private readonly IPostSaveRepository postSaveRepository;

        public PostSaveService(IPostSaveRepository postSaveRepository)
        {
            this.postSaveRepository = postSaveRepository;
        }

        public void Create(int postId, string userId)
        {
            UserPostSave dbPostSave = postSaveRepository.Get(postId, userId);

            if(dbPostSave is null)
            {
                UserPostSave postSave = new UserPostSave
                {
                    PostId = postId,
                    UserId = userId,
                    IsSaved = true,
                };

                postSaveRepository.Create(postSave);
            }
            else
            {
                dbPostSave.IsSaved = true;
                postSaveRepository.Update(dbPostSave);
            }
        }

        public void Update(int postId, string userId)
        {
            UserPostSave dbPostSave = postSaveRepository.Get(postId, userId);
            
            if(dbPostSave != null)
            {
                dbPostSave.IsSaved = false;
                postSaveRepository.Update(dbPostSave);
            }
        }
    }
}
