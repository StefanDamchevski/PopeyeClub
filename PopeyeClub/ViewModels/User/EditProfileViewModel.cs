using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;

namespace PopeyeClub.ViewModels.User
{
    public class EditProfileViewModel
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [AllowNull]
        [Phone]
        public string Phone { get; set; }
        [AllowNull]
        public Image ProfilePicture { get; set; }
    }
}
