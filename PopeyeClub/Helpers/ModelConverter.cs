using PopeyeClub.Data;
using PopeyeClub.ViewModels.Like;
using PopeyeClub.ViewModels.Post;
using PopeyeClub.ViewModels.User;
using System;
using System.Linq;

namespace PopeyeClub.Helpers
{
    public static class ModelConverter
    {
        internal static ProfileViewModel ToProfileViewModel(this ApplicationUser user)
        {
            return new ProfileViewModel
            {
                UserId = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                UserImage = Convert.ToBase64String(user.ProfilePicture),
                IsPrivate = user.IsPrivate,
                IsDeleted = user.IsDeleted,
                Posts = user.Posts?.OrderByDescending(x => x.DateCreated).Select(x => x.ToUserPostViewModel()).ToList(),
            };
        }
        internal static EditProfileViewModel ToEditViewModel(this ApplicationUser user)
        {
            return new EditProfileViewModel
            {
                UserId = user.Id,
                Email = user.Email,
                Username = user.UserName,
                Phone = user.PhoneNumber,
                IsPrivate = user.IsPrivate,
            };
        }

        internal static UserPostsViewModel ToUserPostViewModel(this Post post)
        {
            return new UserPostsViewModel
            {
                PostId = post.Id,
                PostImage = Convert.ToBase64String(post.PostImage),
                PostLikesCount = post.PostLikes.Where(x => x.Status.Equals(true)).Count(),
            };
        }

        internal static OverviewViewModel ToOverviewViewModel(this Post post)
        {
            return new OverviewViewModel
            {
                PostId = post.Id,
                UserId = post.UserId,
                UserName = post.User.UserName,
                ProfilePicture = Convert.ToBase64String(post.User.ProfilePicture),
                PostImage = Convert.ToBase64String(post.PostImage),
                DaysAgo = DateTime.Now.Subtract(post.DateCreated).Days,
                PostLikes = post.PostLikes?.Select(x => x.ToPostLikeViewModel()).ToList(),
            };
        }

        internal static PostLikeViewModel ToPostLikeViewModel(this PostLike postLike)
        {
            return new PostLikeViewModel
            {
                Id = postLike.Id,
                UserId = postLike.UserId,
                PostId = postLike.PostId,
                Status = postLike.Status,
            };
        }
    }
}