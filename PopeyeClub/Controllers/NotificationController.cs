using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PopeyeClub.Helpers;
using PopeyeClub.Hubs;
using PopeyeClub.Services.Interfaces;
using PopeyeClub.ViewModels;
using PopeyeClub.ViewModels.Notification;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PopeyeClub.Controllers
{
    [Authorize]
    public class NotificationController : Controller
    {
        private readonly INotificationService notificationService;
        private readonly IFollowService followService;
        private readonly IHubContext<ChatHub> hub;

        public NotificationController(INotificationService notificationService, IFollowService followService, IHubContext<ChatHub> hub)
        {
            this.notificationService = notificationService;
            this.followService = followService;
            this.hub = hub;
        }

        public IActionResult Overview()
        {
            string currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            
            List<NotificationViewModel> models = notificationService.GetAll(currentUserId)
                .OrderByDescending(x => x.DateSent)
                .Select(x => x.ToNotificationViewModel())
                .ToList();

            foreach (NotificationViewModel model in models)
            {
                if (model.Type.Equals(ViewModelEnums.NotificationViewModelType.Follow))
                {
                    bool status = followService.GetIsFollowed(model.UserFromId, currentUserId);
                    bool followBack = followService.GetIsFollowed(currentUserId, model.UserFromId);

                    if ((status && followBack) || followBack)
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

            return Ok(models);
        }

        [HttpPost]
        public async Task<IActionResult> OpenConnection(string connectionId, string applicationName)
        {
            await hub.Groups.AddToGroupAsync(connectionId, applicationName);
            return Ok();
        }
    }
}
