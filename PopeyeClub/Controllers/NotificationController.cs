using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PopeyeClub.Helpers;
using PopeyeClub.Services.Interfaces;
using PopeyeClub.ViewModels;
using PopeyeClub.ViewModels.Notification;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace PopeyeClub.Controllers
{
    [Authorize]
    public class NotificationController : Controller
    {
        private readonly INotificationService notificationService;
        private readonly IFollowService followService;

        public NotificationController(INotificationService notificationService, IFollowService followService)
        {
            this.notificationService = notificationService;
            this.followService = followService;
        }

        public IActionResult Overview()
        {
            string currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            
            List<NotificationViewModel> models = notificationService.GetAll(currentUserId)
                .OrderByDescending(x => x.DateSent)
                .Select(x => x.ToNotificationViewModel())
                .ToList();

            foreach (NotificationViewModel model  in models)
            {
                if (model.Type == ViewModelEnums.NotificationViewModelType.Follow)
                {
                    bool status = followService.GetIsFollowed(model.UserFromId, currentUserId);
                    bool followBack = followService.GetIsFollowed(currentUserId, model.UserFromId);

                    if (status && followBack)
                    {
                        model.Status = ViewModelEnums.FollowStatus.FollowedBack;
                    }
                    else if(status)
                    {
                        model.Status = ViewModelEnums.FollowStatus.Accepted;
                    }
                    else
                    {
                        model.Status = ViewModelEnums.FollowStatus.None;
                    }
                }
            }

            return View(models);
        }
    }
}
