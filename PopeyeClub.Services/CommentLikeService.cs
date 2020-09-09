using PopeyeClub.Data;
using PopeyeClub.Repositories.Interfaces;
using PopeyeClub.Services.Interfaces;

namespace PopeyeClub.Services
{
    public class CommentLikeService : ICommentLikeService
    {
        private readonly ICommentLikeRepository commentLikeRepository;
        private readonly IPostCommentService postCommentService;
        private readonly INotificationService notificationService;

        public CommentLikeService(ICommentLikeRepository commentLikeRepository, IPostCommentService postCommentService, INotificationService notificationService)
        {
            this.commentLikeRepository = commentLikeRepository;
            this.postCommentService = postCommentService;
            this.notificationService = notificationService;
        }

        public string Create(int commentId, string userId, string currentUserName)
        {
            CommentLike dbCommentLike = commentLikeRepository.Get(commentId, userId);

            PostComment postComment = postCommentService.GetById(commentId);

            if(dbCommentLike is null)
            {
                CommentLike commentLike = new CommentLike
                {
                    CommentId = commentId,
                    UserId = userId,
                    Status = true,
                };

                commentLikeRepository.Create(commentLike);

                if(postComment.UserId != userId)
                {
                    notificationService.Create(userId, postComment.UserId, "CommentLike", currentUserName);
                }
            }
            else
            {
                dbCommentLike.Status = true;
                commentLikeRepository.Update(dbCommentLike);
            }

            return postComment.UserId;
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
