using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PopeyeClub.Services.Interfaces;
using System.Security.Claims;

namespace PopeyeClub.Controllers
{
    [Authorize]
    public class PostCommentController : Controller
    {
        private readonly IPostCommentService postCommentService;

        public PostCommentController(IPostCommentService postCommentService)
        {
            this.postCommentService = postCommentService;
        }

        [HttpPost]
        public IActionResult Create(int postId, string comment)
        {
            if (string.IsNullOrEmpty(comment) || postId == default)
            {
                return RedirectToAction("Overview", "Post");
            }
            else
            {
                string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                postCommentService.Create(postId, comment, userId);
                return RedirectToAction("Overview", "Post");
            }

        }
    }
}
