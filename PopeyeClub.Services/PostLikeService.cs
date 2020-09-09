using PopeyeClub.Data;
using PopeyeClub.Repositories.Interfaces;
using PopeyeClub.Services.Interfaces;

namespace PopeyeClub.Services
{
    public class PostLikeService : IPostLikeService
    {
        private readonly IPostLikeRepository postLikeRepository;
        private readonly IPostService postService;
        private readonly INotificationService notificationService;

        public PostLikeService(IPostLikeRepository postLikeRepository, IPostService postService, INotificationService notificationService)
        {
            this.postLikeRepository = postLikeRepository;
            this.postService = postService;
            this.notificationService = notificationService;
        }

        public string Create(int postId, string currentUserId, string currentUserName)
        {
            PostLike dbLike = postLikeRepository.Get(postId, currentUserId);
            Post post = postService.GetPost(postId);

            if(dbLike is null)
            {
                PostLike newLike = new PostLike
                {
                    PostId = postId,
                    UserId = currentUserId,
                    Status = true,
                };

                postLikeRepository.Create(newLike);
                if(currentUserId != post.UserId)
                {
                    notificationService.Create(currentUserId, post.UserId, "PostLike", currentUserName);
                }
            }
            else
            {
                dbLike.Status = true;
                postLikeRepository.Update(dbLike);
            }

            return post.UserId;
        }

        public void Update(int postId, string userId)
        {
            PostLike dbLike = postLikeRepository.Get(postId, userId);
            
            if(dbLike != null)
            {
                dbLike.Status = false;
                postLikeRepository.Update(dbLike);
            }
        }
    }
}
