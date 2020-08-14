using PopeyeClub.Data;
using PopeyeClub.Repositories.Interfaces;
using PopeyeClub.Services.Interfaces;

namespace PopeyeClub.Services
{
    public class CommentLikeService : ICommentLikeService
    {
        private readonly ICommentLikeRepository commentLikeRepository;

        public CommentLikeService(ICommentLikeRepository commentLikeRepository)
        {
            this.commentLikeRepository = commentLikeRepository;
        }

        public void Create(int commentId, string userId)
        {
            CommentLike dbCommentLike = commentLikeRepository.Get(commentId, userId);

            if(dbCommentLike is null)
            {
                CommentLike commentLike = new CommentLike
                {
                    CommentId = commentId,
                    UserId = userId,
                    Status = true,
                };

                commentLikeRepository.Create(commentLike);
            }
            else
            {
                dbCommentLike.Status = true;
                commentLikeRepository.Update(dbCommentLike);
            }
        }

        public void Update(int commentId, string userId)
        {
            CommentLike dbCommentLike = commentLikeRepository.Get(commentId, userId);

            if(dbCommentLike != null)
            {
                dbCommentLike.Status = false;
                commentLikeRepository.Update(dbCommentLike);
            }
        }
    }
}
