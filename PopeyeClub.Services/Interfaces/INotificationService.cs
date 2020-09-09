using PopeyeClub.Data;
using System.Collections.Generic;

namespace PopeyeClub.Services.Interfaces
{
    public interface INotificationService
    {
        List<Notification> GetAll(string currentUserId);
        void Create(string currentUserId, string userId, string type, string currentUserName);
        void Delete(string currentUserId, string userId, string type);
    }
}
