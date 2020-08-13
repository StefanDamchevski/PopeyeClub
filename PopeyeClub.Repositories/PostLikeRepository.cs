using PopeyeClub.Data;
using PopeyeClub.Repositories.Interfaces;
using System.Linq;

namespace PopeyeClub.Repositories
{
    public class PostLikeRepository : IPostLikeRepository
    {
        private readonly ApplicationDbContext context;

        public PostLikeRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Create(PostLike newLike)
        {
            context.PostLikes.Add(newLike);
            context.SaveChanges();
        }

        public PostLike GetByUserIdAndPostId(int postId, string userId)
        {
            return context.PostLikes.FirstOrDefault(x => x.UserId.Equals(userId) && x.PostId.Equals(postId));
        }

        public void Update(PostLike dbLike)
        {
            context.PostLikes.Update(dbLike);
            context.SaveChanges();
        }
    }
}
