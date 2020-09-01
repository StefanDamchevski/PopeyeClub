using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PopeyeClub.Data;
using PopeyeClub.Repositories.Interfaces;

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

        public void Delete(int postId)
        {
            Post post = new Post
            {
                Id = postId,
            };

            context.Posts.Remove(post);
            context.SaveChanges();
        }

        public List<Post> GetAll(List<string> followIds, string userId)
        {
            return context.Posts
                .Include(x => x.User)
                .Include(x => x.PostLikes)
                .Include(x => x.PostComments)
                    .ThenInclude(x => x.CommentLikes)
                .Include(x => x.PostComments)
                    .ThenInclude(x => x.User)
                .Include(x => x.UserPostSaves)
                .OrderByDescending(x => x.DateCreated)
                .Where(x => followIds.Contains(x.UserId) || x.UserId.Equals(userId))
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

        public List<Post> GetByIds(List<int> postIds)
        {
            return context.Posts
                .Include(x => x.PostLikes)
                .Include(x => x.PostComments)
                .Where(x => postIds.Contains(x.Id))
                .ToList();
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
