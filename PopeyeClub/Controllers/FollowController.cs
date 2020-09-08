using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PopeyeClub.Data;
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

        public FollowController(IFollowService followService, IChatService chatService)
        {
            this.followService = followService;
            this.chatService = chatService;
        }

        public async Task<IActionResult> SendFollowRequset(string userId)
        {
            if (userId != null)
            {
                string currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                await followService.Create(currentUserId, userId);

                bool isFollowed = followService.GetIsFollowed(userId, currentUserId);
                bool isFollowedBack = followService.GetIsFollowed(currentUserId, userId);

                if (isFollowed && isFollowedBack)
                {
                    chatService.Create(userId, currentUserId);
                }
            }

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

            return RedirectToAction("Overview", "Notification");
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