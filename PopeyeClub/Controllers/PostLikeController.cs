using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PopeyeClub.Services.Interfaces;
using PopeyeClub.ViewModels.Like;

namespace PopeyeClub.Controllers
{
    [Authorize]
    public class PostLikeController : Controller
    {
        private readonly IPostLikeService postLikeService;

        public PostLikeController(IPostLikeService postLikeService)
        {
            this.postLikeService = postLikeService;
        }

        [HttpPost]
        public IActionResult AddPostLike([FromBody] RequestPostLike model)
        {
            if (model is null)
            {
                return BadRequest(model);
            }
            else
            {
                string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                postLikeService.Create(model.PostId, userId);
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
                string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                postLikeService.Update(model.PostId, userId);
                return Ok();
            }
        }
    }
}
