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
                .Include(x => x.PostComments)
                    .ThenInclude(x => x.CommentLikes)
                .Include(x => x.UserPostSaves)
                .OrderByDescending(x => x.DateCreated)
                .ToList();
        }

        public Post GetById(int postId)
        {
            return context.Posts
                .Include(x => x.User)
                .Include(x => x.PostLikes)
                .Include(x => x.PostComments)
                    .ThenInclude(x => x.User)
                 .Include(x => x.PostComments)
                    .ThenInclude(x => x.CommentLikes)
                .Include(x => x.UserPostSaves)
                .FirstOrDefault(x => x.Id.Equals(postId));
        }

        public List<Post> GetOtherPosts(int postId, string userId)
        {
            return context.Posts
                .Include(x => x.PostLikes)
                .Include(x => x.PostComments)
                .Where(x => x.UserId.Equals(userId) && x.Id != postId)
                .ToList();
        }
    }
}
