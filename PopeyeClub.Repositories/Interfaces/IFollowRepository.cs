using PopeyeClub.Data;
using System.Collections.Generic;

namespace PopeyeClub.Repositories.Interfaces
{
    public interface IFollowRepository
    {
        Follow GetRelation(string currentUserId,string userId);
        void Create(Follow follow);
        void Update(Follow follow);
        List<Follow> GetByIds(string userId);
    }
}
