using PopeyeClub.Data;
using System.Collections.Generic;

namespace PopeyeClub.Repositories.Interfaces
{
    public interface IPostRepository
    {
        List<Post> GetAll();
        void Create(Post post);
    }
}
