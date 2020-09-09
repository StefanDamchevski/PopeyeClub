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
    public class PostLikeController : Controller
    {
        private readonly IPostLikeService postLikeService;
        private readonly IHubContext<ChatHub> hub;

        public PostLikeController(IPostLikeService postLikeService, IHubContext<ChatHub> hub)
        {
            this.postLikeService = postLikeService;
            this.hub = hub;
        }

        [HttpPost]
        public async Task<IActionResult> AddPostLike([FromBody] RequestPostLike model)
        {
            if (model is null)
            {
                return BadRequest(model);
            }
            else
            {
                string currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                string currentUserName = User.Identity.Name;
                string userId =  postLikeService.Create(model.PostId, currentUserId, currentUserName);

                if(currentUserId != userId)
                {
                    await hub.Clients.User(userId).SendAsync("RecieveNotification");
                }
                return Ok();
            }
        }

        [HttpPost]
        public IActionResult RemovePostLike([FromBody] RequestPostLike model)
        {
            if (model is null)
            {
                return BadRequest(model);
            }
            else
            {

                string currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                postLikeService.Update(model.PostId, currentUserId);
                return Ok();
            }
        }
    }
}
