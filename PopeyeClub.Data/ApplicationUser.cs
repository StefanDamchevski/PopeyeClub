using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PopeyeClub.Data
{
    public class ApplicationUser : IdentityUser
    {
        public byte[] ProfilePicture { get; set; }
        [Required]
        public bool IsPrivate { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
    }
}