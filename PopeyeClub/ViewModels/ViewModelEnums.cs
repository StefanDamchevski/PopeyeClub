namespace PopeyeClub.ViewModels
{
    public class ViewModelEnums
    {
        public enum PostLikeStatus
        {
            None,
            Liked,
        }

        public enum CommentLikeStatus
        {
            None,
            Liked,
        }

        public enum PostSaveStatus
        {
            None,
            Saved,
        }

        public enum FollowStatus
        {
            None,
            Accepted,
            FollowedBack,
        }
        
        public enum NotificationViewModelType
        {
            Other,
            Follow,
        }
    }
}