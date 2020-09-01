using Microsoft.EntityFrameworkCore;
using PopeyeClub.Data;
using PopeyeClub.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace PopeyeClub.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationDbContext context;

        public NotificationRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Create(Notification notification)
        {
            context.Notifications.Add(notification);
            context.SaveChanges();
        }

        public void Delete(Notification notification)
        {
            context.Notifications.Remove(notification);
            context.SaveChanges();
        }

        public Notification Get(string currentUserId, string userId, Enums.NotificationType result)
        {
            return context.Notifications.FirstOrDefault(x => x.FromUserId.Equals(userId) && x.ToUserId.Equals(currentUserId) && x.Type.Equals(result));
        }

        public List<Notification> GetAll(string currentUserId)
        {
            return context.Notifications
                .Include(x => x.FromUser)
                .Where(x => x.ToUserId.Equals(currentUserId))
                .ToList();
        }
    }
}
