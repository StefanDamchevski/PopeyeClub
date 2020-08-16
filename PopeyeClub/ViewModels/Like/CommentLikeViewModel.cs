namespace PopeyeClub.ViewModels.Like
{
    public class CommentLikeViewModel
    {
        public int? Id { get; set; }
        public string UserId { get; set; }
        public int? CommentId { get; set; }
        public bool Status { get; set; }
    }
}