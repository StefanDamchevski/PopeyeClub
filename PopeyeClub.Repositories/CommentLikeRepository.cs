using System.Linq;
using PopeyeClub.Data;
using PopeyeClub.Repositories.Interfaces;

namespace PopeyeClub.Repositories
{
    public class CommentLikeRepository : ICommentLikeRepository
    {
        private readonly ApplicationDbContext context;

        public CommentLikeRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Create(CommentLike commentLike)
        {
            context.CommentLikes.Add(commentLike);
            context.SaveChanges();
        }

        public CommentLike Get(int commentId, string userId)
        {
            return context.CommentLikes.FirstOrDefault(x => x.CommentId.Equals(commentId) && x.UserId.Equals(userId));
        }

        public void Update(CommentLike dbCommentLike)
        {
            context.CommentLikes.Update(dbCommentLike);
            context.SaveChanges();
        }
    }
}
