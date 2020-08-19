using System.Linq;
using PopeyeClub.Data;
using PopeyeClub.Repositories.Interfaces;

namespace PopeyeClub.Repositories
{
    public class PostSaveRepository : IPostSaveRepository
    {
        private readonly ApplicationDbContext context;

        public PostSaveRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Create(UserPostSave postSave)
        {
            context.UserPostSaves.Add(postSave);
            context.SaveChanges();
        }

        public UserPostSave Get(int postId, string userId)
        {
            return context.UserPostSaves.FirstOrDefault(x => x.PostId.Equals(postId) && x.UserId.Equals(userId));
        }

        public void Update(UserPostSave dbPostSave)
        {
            context.UserPostSaves.Update(dbPostSave);
            context.SaveChanges();
        }
    }
}
