using System.Collections.Generic;
using PopeyeClub.Data;

namespace PopeyeClub.Services.Interfaces
{
    public interface IPostService
    {
        List<Post> GetAll();
        void Create(string userId, byte[] postImage);
        Post GetById(int postId);
        List<Post> GetOtherPosts(int postId, string userId);
        List<Post> GetByIds(List<int> postIds);
    }
}
