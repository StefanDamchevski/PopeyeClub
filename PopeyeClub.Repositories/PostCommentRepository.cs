using System.Linq;
using PopeyeClub.Data;
using PopeyeClub.Repositories.Interfaces;

namespace PopeyeClub.Repositories
{
    public class PostCommentRepository : IPostCommentRepository
    {
        private readonly ApplicationDbContext context;

        public PostCommentRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Create(PostComment postComment)
        {
            context.PostComments.Add(postComment);
            context.SaveChanges();
        }

        public void Delete(int commentId)
        {
            PostComment postComment = new PostComment
            {
                Id = commentId,
            };

            context.PostComments.Remove(postComment);
            context.SaveChanges();
        }

        public PostComment Get(int postId, string userId, string comment)
        {
            return context.PostComments.FirstOrDefault(x => x.PostId.Equals(postId) && x.UserId.Equals(userId) && x.Text.Equals(comment));
        }
    }
}
