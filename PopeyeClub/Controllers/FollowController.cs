using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PopeyeClub.Hubs;
using PopeyeClub.Services.Interfaces;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PopeyeClub.Controllers
{
    [Authorize]
    public class FollowController : Controller
    {
        private readonly IFollowService followService;
        private readonly IChatService chatService;
        private readonly IHubContext<ChatHub> hub;

        public FollowController(IFollowService followService, IChatService chatService, IHubContext<ChatHub> hub)
        {
            this.followService = followService;
            this.chatService = chatService;
            this.hub = hub;
        }

        public async Task<IActionResult> SendFollowRequset(string userId)
        {
            if (userId != null)
            {
                string currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                string currentUserName = User.Identity.Name;
                await followService.Create(currentUserId, userId, currentUserName);

                bool isFollowed = followService.GetIsFollowed(userId, currentUserId);
                bool isFollowedBack = followService.GetIsFollowed(currentUserId, userId);

                if (isFollowed && isFollowedBack)
                {
                    chatService.Create(userId, currentUserId);
                }
            }

            await hub.Clients.User(userId).SendAsync("RecieveNotification");

            return RedirectToAction("Profile", "User", new { UserId = userId });
        }

        public IActionResult Unfollow(string userId)
        {
            if(userId != null)
            {
                string currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                followService.Update(currentUserId, userId, false, false);
            }

            return RedirectToAction("Profile", "User", new { UserId = userId });
        }

        public IActionResult Accept(string userId)
        {
            if(userId != null)
            {
                string currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                followService.Update(userId, currentUserId, true, true);
            }

            return RedirectToAction("Profile", "User", new { UserId = userId });
        }

        public IActionResult Decline(string userId)
        {
            if(userId != null)
            {
                string currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                followService.Update(userId, currentUserId, false, false);
            }

            return RedirectToAction("Profile", "User", new { UserId = userId });
        }
    }
}