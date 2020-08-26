using PopeyeClub.Data;
using System.Collections.Generic;

namespace PopeyeClub.Services.Interfaces
{
    public interface INotificationService
    {
        void Create(string currentUserId, string userId, string type);
        List<Notification> GetAll(string currentUserId);
    }
}
