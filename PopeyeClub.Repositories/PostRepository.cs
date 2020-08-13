using Microsoft.EntityFrameworkCore;
using PopeyeClub.Data;
using PopeyeClub.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace PopeyeClub.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext context;

        public PostRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Create(Post post)
        {
            context.Posts.Add(post);
            context.SaveChanges();
        }

        public List<Post> GetAll()
        {
            return context.Posts
                .Include(x => x.User)
                .Include(x => x.PostLikes)
                .OrderByDescending(x => x.DateCreated)
                .ToList();
        }
    }
}
