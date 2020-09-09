using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PopeyeClub.Hubs;
using PopeyeClub.Services.Interfaces;
using PopeyeClub.ViewModels.Like;

namespace PopeyeClub.Controllers
{
    [Authorize]
    public class CommentLikeController : Controller
    {
        private readonly ICommentLikeService commentLikeService;
        private readonly IHubContext<ChatHub> hub;

        public CommentLikeController(ICommentLikeService commentLikeService, IHubContext<ChatHub> hub)
        {
            this.commentLikeService = commentLikeService;
            this.hub = hub;
        }

        [HttpPost]
        public async Task<IActionResult> AddCommentLike([FromBody] RequestCommentLike model)
        {
            if (model is null)
            {
                return BadRequest(model);
            }
            else
            {
                string currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                string currentUserName = User.Identity.Name;
                string userId = commentLikeService.Create(model.CommentId, currentUserId, currentUserName);

                if(userId != currentUserId)
                {
                    await hub.Clients.User(userId).SendAsync("RecieveNotification");
                }

                return Ok();
            }
        }

        [HttpPost]
        public IActionResult RemoveCommentLike([FromBody] RequestCommentLike model)
        {
            if (model is null)
            {
                return BadRequest(model);
            }
            else
            {
                string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                commentLikeService.Update(model.CommentId, userId);
                return Ok();
            }
        }
    }
}
