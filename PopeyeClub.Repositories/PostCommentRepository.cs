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
    }
}
