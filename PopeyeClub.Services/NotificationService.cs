using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration configuration;

        public NotificationService(INotificationRepository notificationRepository, IConfiguration configuration)
        {
            this.notificationRepository = notificationRepository;
            this.configuration = configuration;
        }

        public void Create(string currentUserId, string userId, string type, string currentUsername)
        {
            Enums.NotificationType.TryParse(type, out Enums.NotificationType result);

            Notification notification = new Notification()
            {
                FromUserId = currentUserId,
                ToUserId = userId,
                Type = result,
                DateSent = DateAndTime.Now,
            };

            switch (result)
            {
                case Enums.NotificationType.PostLike:
                    notification.Message = currentUsername + configuration["PostLike"];
                    break;
                case Enums.NotificationType.PostComment:
                    notification.Message = currentUsername + configuration["PostComment"];
                    break;
                case Enums.NotificationType.CommentLike:
                    notification.Message = currentUsername + configuration["CommentLike"];
                    break;
                case Enums.NotificationType.Follow:
                    notification.Message = currentUsername + configuration["Follow"];
                    break;
            }

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
