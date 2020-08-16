using PopeyeClub.Data;
using System.Collections.Generic;

namespace PopeyeClub.Services.Interfaces
{
    public interface IPostService
    {
        List<Post> GetAll();
        void Create(string userId, byte[] postImage);
        Post GetById(int postId);
    }
}
