using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PopeyeClub.Helpers;
using PopeyeClub.Services.Interfaces;
using PopeyeClub.ViewModels;
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
        private readonly IFollowService followService;

        public PostController(IPostService postService, IFollowService followService)
        {
            this.postService = postService;
            this.followService = followService;
        }

        public IActionResult Overview()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            List<OverviewViewModel> models = postService.GetAll(userId).Select(x => x.ToOverviewViewModel()).ToList();

            foreach (OverviewViewModel post in models)
            {
                PostLikeViewModel postLike = post.PostLikes?.FirstOrDefault(x => x.UserId.Equals(userId));

                if(postLike is null)
                {
                    post.LikeStatus = ViewModelEnums.PostLikeStatus.None;
                }
                else if(!postLike.Status)
                {
                    post.LikeStatus = ViewModelEnums.PostLikeStatus.None;
                }
                else
                {
                    post.LikeStatus = ViewModelEnums.PostLikeStatus.Liked;
                }

                UserPostSaveViewModel postSave = post.UserPostSaves?.FirstOrDefault(x => x.UserId.Equals(userId));

                if(postSave is null)
                {
                    post.PostSaveStatus = ViewModelEnums.PostSaveStatus.None;
                }
                else if (!postSave.Status)
                {
                    post.PostSaveStatus = ViewModelEnums.PostSaveStatus.None;
                }
                else
                {
                    post.PostSaveStatus = ViewModelEnums.PostSaveStatus.Saved;
                }

                foreach (CommentViewModel comment in post.PostComments)
                {
                    CommentLikeViewModel commentLike = comment.CommentLikes?.FirstOrDefault(x => x.UserId.Equals(userId));

                    if (commentLike is null)
                    {
                        comment.CommentLikeStatus = ViewModelEnums.CommentLikeStatus.None;
                    }
                    else if (!commentLike.Status)
                    {
                        comment.CommentLikeStatus = ViewModelEnums.CommentLikeStatus.None;
                    }
                    else
                    {
                        comment.CommentLikeStatus = ViewModelEnums.CommentLikeStatus.Liked;
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

            model.IsFollowed = followService.GetIsFollowed(userId, model.UserId);

            if((model.IsPrivate && !model.IsFollowed))
            {
                if(userId != model.UserId)
                {
                    return RedirectToAction("Profile", "User", new { model.UserId});
                }
            }

            PostLikeViewModel postLike = model.PostLikes?.FirstOrDefault(x => x.UserId.Equals(userId));

            if (postLike is null)
            {
                model.PostLikeStatus = ViewModelEnums.PostLikeStatus.None;
            }
            else if (!postLike.Status)
            {
                model.PostLikeStatus = ViewModelEnums.PostLikeStatus.None;
            }
            else
            {
                model.PostLikeStatus = ViewModelEnums.PostLikeStatus.Liked;
            }

            foreach (CommentViewModel comment in model.Comments)
            {
                CommentLikeViewModel commentLike = comment.CommentLikes?.FirstOrDefault(x => x.UserId.Equals(userId));

                if (commentLike is null)
                {
                    comment.CommentLikeStatus = ViewModelEnums.CommentLikeStatus.None;
                }
                else if (!commentLike.Status)
                {
                    comment.CommentLikeStatus = ViewModelEnums.CommentLikeStatus.None;
                }
                else
                {
                    comment.CommentLikeStatus = ViewModelEnums.CommentLikeStatus.Liked;
                }
            }

            model.UserPosts = postService.GetOtherPosts(postId, model.UserId)
                        .OrderByDescending(x => x.DateCreated)
                        .Select(x => x.ToUserPostViewModel())
                        .ToList();

            UserPostSaveViewModel postSave = model.UserPostSaves?.FirstOrDefault(x => x.PostId.Equals(postId) && x.UserId.Equals(userId));

            if(postSave is null)
            {
                model.PostSaveStatus = ViewModelEnums.PostSaveStatus.None;
            }
            else if (!postSave.Status)
            {
                model.PostSaveStatus = ViewModelEnums.PostSaveStatus.None;
            }
            else
            {
                model.PostSaveStatus = ViewModelEnums.PostSaveStatus.Saved;
            }

            return View(model);
        }

        public IActionResult Delete(int postId)
        {
            postService.Delete(postId);
            return RedirectToAction(nameof(Overview));
        }
    }
}