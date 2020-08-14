using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PopeyeClub.Services.Interfaces;
using PopeyeClub.ViewModels.Like;
using System.Security.Claims;

namespace PopeyeClub.Controllers
{
    [Authorize]
    public class CommentLikeController : Controller
    {
        private readonly ICommentLikeService commentLikeService;

        public CommentLikeController(ICommentLikeService commentLikeService)
        {
            this.commentLikeService = commentLikeService;
        }

        [HttpPost]
        public IActionResult AddCommentLike([FromBody] RequestCommentLike model)
        {
            if (model is null)
            {
                return BadRequest(model);
            }
            else
            {
                string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                commentLikeService.Create(model.CommentId, userId);
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
