using System;
using PopeyeClub.Data;
using PopeyeClub.Repositories.Interfaces;
using PopeyeClub.Services.Interfaces;

namespace PopeyeClub.Services
{
    public class PostCommentService : IPostCommentService
    {
        private readonly IPostCommentRepository postCommentRepostiory;

        public PostCommentService(IPostCommentRepository postCommentRepostiory)
        {
            this.postCommentRepostiory = postCommentRepostiory;
        }

        public void Create(int postId, string comment, string userId)
        {
            PostComment postComment = new PostComment()
            {
                PostId = postId,
                Text = comment,
                UserId = userId,
                DateCreated = DateTime.Now
            };

            postCommentRepostiory.Create(postComment);
        }

        public void Delete(int commentId)
        {
            postCommentRepostiory.Delete(commentId);
        }

        public PostComment Get(int postId, string userId, string comment)
        {
            return postCommentRepostiory.Get(postId, userId, comment);
        }
    }
}
