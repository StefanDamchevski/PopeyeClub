using Microsoft.AspNetCore.Identity;

namespace PopeyeClub.Data
{
    public class ApplicationUser : IdentityUser
    {
        public byte[] ProfilePicture { get; set; }
    }
}