using Microsoft.EntityFrameworkCore;
using PopeyeClub.Data;
using PopeyeClub.Repositories.Interfaces;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

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

        public PostComment Get(int postId, string userId, string comment)
        {
            return context.PostComments.FirstOrDefault(x => x.PostId.Equals(postId) && x.UserId.Equals(userId) && x.Text.Equals(comment));
        }
    }
}
