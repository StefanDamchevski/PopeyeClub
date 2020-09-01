using PopeyeClub.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PopeyeClub.Services.Interfaces
{
    public interface IFollowService
    {
        bool GetIsFollowed(string currentUserId, string userId);
        Task Create(string currentUserId, string userId);
        void Update(string currentUserId, string userId, bool isFollowed, bool isSent);
        bool GetIsSent(string currentUserId, string userId);
        List<string> GetIds(string userId);
        List<Follow> GetByIds(string userId);
        int GetFollowersCount(string userId);
        int GetFollowingCount(string userId);
    }
}
