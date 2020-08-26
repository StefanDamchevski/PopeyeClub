using System.Collections.Generic;
using PopeyeClub.Data;

namespace PopeyeClub.Repositories.Interfaces
{
    public interface IPostRepository
    {
        List<Post> GetAll(List<string> followIds, string userId);
        void Create(Post post);
        Post GetById(int postId);
        List<Post> GetOtherPosts(int postId, string userId);
        List<Post> GetByIds(List<int> postIds);
    }
}
