using Microsoft.VisualBasic;
using PopeyeClub.Data;
using PopeyeClub.Repositories.Interfaces;
using PopeyeClub.Services.Interfaces;
using System.Collections.Generic;

namespace PopeyeClub.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            this.notificationRepository = notificationRepository;
        }

        public void Create(string currentUserId, string userId, string type)
        {
            Enums.NotificationType.TryParse(type, out Enums.NotificationType result);

            Notification notification = new Notification()
            {
                FromUserId = currentUserId,
                ToUserId = userId,
                Type = result,
                DateSent = DateAndTime.Now,
            };

            notificationRepository.Create(notification);
        }

        public void Delete(string currentUserId, string userId, string type)
        {
            Enums.NotificationType.TryParse(type, out Enums.NotificationType result);
            Notification notification = notificationRepository.Get(currentUserId, userId, result);

            if(notification != null)
            {
                notificationRepository.Delete(notification);
            }
        }

        public List<Notification> GetAll(string currentUserId)
        {
            return notificationRepository.GetAll(currentUserId);
        }
    }
}
