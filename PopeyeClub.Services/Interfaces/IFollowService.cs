using PopeyeClub.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PopeyeClub.Services.Interfaces
{
    public interface IFollowService
    {
        Task Create(string currentUserId, string userId, string currentUserName);
        List<string> GetIds(string userId);
        bool GetIsFollowed(string currentUserId, string userId);
        bool GetIsSent(string currentUserId, string userId);
        int GetFollowersCount(string userId);
        int GetFollowingCount(string userId);
        void Update(string currentUserId, string userId, bool isFollowed, bool isSent);
        List<Follow> GetAllUserRelations(string userId);
    }
}
