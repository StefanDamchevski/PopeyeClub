using PopeyeClub.ViewModels.Post;
using System.Collections.Generic;

namespace PopeyeClub.ViewModels.User
{
    public class ProfileViewModel
    {
        public string UserId { get; set; }
        public string UserImage { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool IsPrivate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsFollowed { get; set; }
        public bool IsSent { get; set; }
        public List<UserPostsViewModel> Posts { get; set; }
        public List<UserPostsViewModel> SavedPosts { get; set; }
        public int FollowersCount { get; set; }
        public int FollowingCount { get; set; }
    }
}
