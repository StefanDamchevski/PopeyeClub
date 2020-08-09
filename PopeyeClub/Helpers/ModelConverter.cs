using PopeyeClub.Data;
using PopeyeClub.ViewModels.User;
using System;
using System.Drawing;

namespace PopeyeClub.Helpers
{
    public static class ModelConverter
    {
        public static ProfileViewModel ToProfileViewModel(this ApplicationUser user)
        {
            return new ProfileViewModel
            {
                UserId = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                UserImage = Convert.ToBase64String(user.ProfilePicture)
            };
        }
        public static EditProfileViewModel ToEditViewModel(this ApplicationUser user)
        {
            return new EditProfileViewModel
            {
                UserId = user.Id,
                Email = user.Email,
                Username = user.UserName,
                Phone = user.PhoneNumber,
                ProfilePicture = (Bitmap)(new ImageConverter().ConvertFrom(user.ProfilePicture))
            };
        }
    }
}
