using PopeyeClub.Data;
using PopeyeClub.Repositories.Interfaces;
using PopeyeClub.Services.Interfaces;

namespace PopeyeClub.Services
{
    public class PostLikeService : IPostLikeService
    {
        private readonly IPostLikeRepository postLikeRepository;

        public PostLikeService(IPostLikeRepository postLikeRepository)
        {
            this.postLikeRepository = postLikeRepository;
        }

        public void Create(int postId, string userId)
        {
            PostLike dbLike = postLikeRepository.GetByUserIdAndPostId(postId, userId);

            if(dbLike is null)
            {
                PostLike newLike = new PostLike
                {
                    PostId = postId,
                    UserId = userId,
                    Status = true,
                };

                postLikeRepository.Create(newLike);
            }
            else
            {
                dbLike.Status = true;
                postLikeRepository.Update(dbLike);
            }
        }

        public void Update(int postId, string userId)
        {
            PostLike dbLike = postLikeRepository.GetByUserIdAndPostId(postId, userId);
            
            if(dbLike != null)
            {
                dbLike.Status = false;
                postLikeRepository.Update(dbLike);
            }
        }
    }
}
