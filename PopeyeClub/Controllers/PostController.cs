using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PopeyeClub.Helpers;
using PopeyeClub.Services.Interfaces;
using PopeyeClub.ViewModels.Like;
using PopeyeClub.ViewModels.Post;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace PopeyeClub.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IPostService postService;

        public PostController(IPostService postService)
        {
            this.postService = postService;
        }

        public IActionResult Overview()
        {
            List<OverviewViewModel> models = postService.GetAll().Select(x => x.ToOverviewViewModel()).ToList();
            foreach (var post in models)
            {
                PostLikeViewModel postLike = post.PostLikes?.FirstOrDefault(x => x.UserId.Equals(User.FindFirst(ClaimTypes.NameIdentifier).Value));
                if(postLike == null)
                {
                    post.LikeStatus = Enums.PostLikeStatus.None;
                }
                else if(!postLike.Status)
                {
                    post.LikeStatus = Enums.PostLikeStatus.None;
                }
                else
                {
                    post.LikeStatus = Enums.PostLikeStatus.Liked;
                }
            }
            return View(models);
        }

        [HttpPost]
        public IActionResult Create(IFormFile postImage)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            postService.Create(userId, postImage.ToByteArray());    
            return RedirectToAction(nameof(Overview));
        }
    }
}