using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PopeyeClub.Services.Interfaces;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PopeyeClub.Controllers
{
    [Authorize]
    public class FollowController : Controller
    {
        private readonly IFollowService followService;

        public FollowController(IFollowService followService)
        {
            this.followService = followService;
        }

        public async Task<IActionResult> SendFollowRequset(string userId)
        {
            if(userId != null)
            {
                string currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                await followService.Create(currentUserId, userId);
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
