using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PopeyeClub.Data;
using PopeyeClub.Hubs;
using PopeyeClub.Services.Interfaces;

namespace PopeyeClub.Controllers
{
    [Authorize]
    public class PostCommentController : Controller
    {
        private readonly IPostCommentService postCommentService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHubContext<ChatHub> hub;
        private readonly INotificationService notificationService;

        public PostCommentController(IPostCommentService postCommentService,
            UserManager<ApplicationUser> userManager,
            IHubContext<ChatHub> hub,
            INotificationService notificationService)
        {
            this.postCommentService = postCommentService;
            this.userManager = userManager;
            this.hub = hub;
            this.notificationService = notificationService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(int postId, string comment)
        {
            if (string.IsNullOrEmpty(comment) || postId == default)
            {
                return BadRequest();
            }
            else
            {
                string currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                postCommentService.Create(postId, comment, currentUserId);

                ApplicationUser user = userManager.Users.FirstOrDefault(x => x.Id.Equals(currentUserId));

                PostComment postComment = postCommentService.Get(postId, currentUserId, comment);

                if(currentUserId != postComment.Post.UserId)
                {
                    notificationService.Create(currentUserId, postComment.Post.UserId, "PostComment", user.UserName);
                    await hub.Clients.User(postComment.Post.UserId).SendAsync("RecieveNotification");
                }


                return Ok(new
                {
                    PostId = postId,
                    Comment = comment,
                    Username = user.UserName,
                    UserId = user.Id,
                    UserImage = Convert.ToBase64String(user.ProfilePicture),
                    CommentId = postComment.Id,
                });
            }
        }

        public IActionResult Delete(int commentId, int postId)
        {
            if(commentId != 0)
            {
                postCommentService.Delete(commentId);
            }
            return RedirectToAction("Details", "Post", new {PostId = postId});
        }
    }
}
