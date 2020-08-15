using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PopeyeClub.Data;
using PopeyeClub.Services.Interfaces;
using System;
using System.Linq;
using System.Security.Claims;

namespace PopeyeClub.Controllers
{
    [Authorize]
    public class PostCommentController : Controller
    {
        private readonly IPostCommentService postCommentService;
        private readonly UserManager<ApplicationUser> userManager;

        public PostCommentController(IPostCommentService postCommentService, UserManager<ApplicationUser> userManager)
        {
            this.postCommentService = postCommentService;
            this.userManager = userManager;
        }

        [HttpPost]
        public IActionResult Create(int postId, string comment)
        {
            if (string.IsNullOrEmpty(comment) || postId == default)
            {
                return BadRequest();
            }
            else
            {
                string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                postCommentService.Create(postId, comment, userId);
                ApplicationUser user = userManager.Users.FirstOrDefault(x => x.Id.Equals(userId));
                PostComment postComment = postCommentService.Get(postId, userId, comment);
                return Ok(new
                {
                    PostId = postId,
                    Comment = comment,
                    Username = user.UserName,
                    UserImage = Convert.ToBase64String(user.ProfilePicture),
                    CommentId = postComment.Id,
                });
            }
        }
    }
}
