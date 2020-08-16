using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PopeyeClub.Helpers;
using PopeyeClub.Services.Interfaces;
using PopeyeClub.ViewModels.Comment;
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

            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            foreach (OverviewViewModel post in models)
            {
                PostLikeViewModel postLike = post.PostLikes?.FirstOrDefault(x => x.UserId.Equals(userId));

                if(postLike is null)
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

                foreach (CommentViewModel comment in post.PostComments)
                {
                    CommentLikeViewModel commentLike = comment.CommentLikes?.FirstOrDefault(x => x.UserId.Equals(userId));

                    if (commentLike is null)
                    {
                        comment.CommentLikeStatus = Enums.CommentLikeStatus.None;
                    }
                    else if (!commentLike.Status)
                    {
                        comment.CommentLikeStatus = Enums.CommentLikeStatus.None;
                    }
                    else
                    {
                        comment.CommentLikeStatus = Enums.CommentLikeStatus.Liked;
                    }
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

        public IActionResult Details(int postId)
        {
            PostDetailsViewModel model = postService.GetById(postId).ToPostDetailsViewModel();

            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            PostLikeViewModel postLike = model.PostLikes?.FirstOrDefault(x => x.UserId.Equals(userId));

            if (postLike is null)
            {
                model.PostLikeStatus = Enums.PostLikeStatus.None;
            }
            else if (!postLike.Status)
            {
                model.PostLikeStatus = Enums.PostLikeStatus.None;
            }
            else
            {
                model.PostLikeStatus = Enums.PostLikeStatus.Liked;
            }

            foreach (CommentViewModel comment in model.Comments)
            {
                CommentLikeViewModel commentLike = comment.CommentLikes?.FirstOrDefault(x => x.UserId.Equals(userId));

                if (commentLike is null)
                {
                    comment.CommentLikeStatus = Enums.CommentLikeStatus.None;
                }
                else if (!commentLike.Status)
                {
                    comment.CommentLikeStatus = Enums.CommentLikeStatus.None;
                }
                else
                {
                    comment.CommentLikeStatus = Enums.CommentLikeStatus.Liked;
                }
            }
            return View(model);
        }
    }
}