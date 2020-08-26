using PopeyeClub.Data;
using System.Collections.Generic;

namespace PopeyeClub.Repositories.Interfaces
{
    public interface INotificationRepository
    {
        void Create(Notification notification);
        List<Notification> GetAll(string currentUserId);
    }
}
