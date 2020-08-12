using PopeyeClub.Data;
using PopeyeClub.ViewModels.Post;
using PopeyeClub.ViewModels.User;
using System;

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

        internal static OverviewViewModel ToOverviewViewModel(this Post post)
        {
            return new OverviewViewModel
            {
                UserId = post.UserId,
                UserName = post.User.UserName,
                ProfilePicture = Convert.ToBase64String(post.User.ProfilePicture),
                PostImage = Convert.ToBase64String(post.PostImage),
                DaysAgo = DateTime.Now.Subtract(post.DateCreated).Days,
            };
        }
    }
}