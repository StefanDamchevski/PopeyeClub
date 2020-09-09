using System;
using System.Linq;
using PopeyeClub.Data;
using PopeyeClub.Services.Dto.Chat;
using PopeyeClub.ViewModels;
using PopeyeClub.ViewModels.Chat;
using PopeyeClub.ViewModels.Comment;
using PopeyeClub.ViewModels.Follow;
using PopeyeClub.ViewModels.Like;
using PopeyeClub.ViewModels.Notification;
using PopeyeClub.ViewModels.Post;
using PopeyeClub.ViewModels.User;

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
                PostCommentCount = post.PostComments.Count(),
            };
        }

        internal static OverviewViewModel ToOverviewViewModel(this Post post)
        {
            return new OverviewViewModel
            {
                PostId = post.Id,
                UserId = post.UserId,
                UserName = post.User?.UserName,
                ProfilePicture = Convert.ToBase64String(post.User?.ProfilePicture),
                PostImage = Convert.ToBase64String(post.PostImage),
                DaysAgo = DateTime.Now.Subtract(post.DateCreated).Days,
                PostLikes = post.PostLikes?.Select(x => x.ToPostLikeViewModel()).ToList(),
                PostComments = post.PostComments?.OrderBy(x => x.DateCreated).Take(2).Select(x => x.ToCommentViewModel()).ToList(),
                UserPostSaves = post.UserPostSaves?.Select(x => x.ToPostSaveViewModel()).ToList(),
                PostCommentsCount = post.PostComments.Count(),
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

        internal static CommentViewModel ToCommentViewModel(this PostComment comment)
        {
            return new CommentViewModel
            {
                Id = comment.Id,
                Text = comment.Text,
                UserImage = Convert.ToBase64String(comment.User?.ProfilePicture),
                UserName = comment.User?.UserName,
                UserId = comment.UserId,
                PostId = comment.PostId,
                CommentLikes = comment.CommentLikes?.Select(x => x.ToCommentLikeViewModel()).ToList(),
                DaysAgo = DateTime.Now.Subtract(comment.DateCreated).Days,
                CommentLikesCount = comment.CommentLikes.Where(x => x.Status.Equals(true)).Count(),
            };
        }

        internal static CommentLikeViewModel ToCommentLikeViewModel(this CommentLike commentLike)
        {
            return new CommentLikeViewModel
            {
                Id = commentLike.Id,
                UserId = commentLike.UserId,
                CommentId = commentLike.CommentId,
                Status = commentLike.Status,
            };
        }

        internal static PostDetailsViewModel ToPostDetailsViewModel(this Post post)
        {
            return new PostDetailsViewModel
            {
                PostId = post.Id,
                UserId = post.UserId,
                UserImage = Convert.ToBase64String(post.User?.ProfilePicture),
                PostImage = Convert.ToBase64String(post.PostImage),
                CreatedBy = post.User?.UserName,
                IsPrivate = post.User.IsPrivate,
                DaysAgo = DateTime.Now.Subtract(post.DateCreated).Days,
                Comments = post.PostComments?.OrderBy(x => x.DateCreated).Select(x => x.ToCommentViewModel()).ToList(),
                PostLikes = post.PostLikes?.Select(x => x.ToPostLikeViewModel()).ToList(),
                UserPostSaves = post.UserPostSaves?.Select(x => x.ToPostSaveViewModel()).ToList(),
            };
        }

        internal static UserPostSaveViewModel ToPostSaveViewModel(this UserPostSave postSave)
        {
            return new UserPostSaveViewModel
            {
                Id = postSave.Id,
                PostId = postSave.PostId,
                UserId = postSave.UserId,
                Status = postSave.IsSaved,
            };
        }

        internal static NotificationViewModel ToNotificationViewModel(this Notification notification)
        {
            return new NotificationViewModel
            {
                NotificationId = notification.Id,
                UserFromId = notification.FromUserId,
                UserFromName = notification.FromUser.UserName,
                Type = notification.Type.ToTypeViewModel(),
                DateSent = notification.DateSent,
                NotificationMessage = notification.Message,
            };
        }

        internal static ViewModelEnums.NotificationViewModelType ToTypeViewModel(this Enums.NotificationType type)
        {
            return type switch
            {
                Enums.NotificationType.Follow => ViewModelEnums.NotificationViewModelType.Follow,
                _ => ViewModelEnums.NotificationViewModelType.Other,
            };
        }

        internal static FollowViewModel ToFollowViewModel(this Follow follow)
        {
            return new FollowViewModel
            {
                FollowId = follow.Id,
                FromUserId = follow.FromUserId,
                ToUserId = follow.ToUserId,
                Status = follow.IsFollowed,
            };
        }

        internal static ChatRoomViewModel ToChatRoomViewModel(this ChatRoomDto roomDto)
        {
            return new ChatRoomViewModel
            {
                RoomId = roomDto.ChatRoomId,
                UserId = roomDto.UserId,
                UserImage = roomDto.UserImage,
                ChatroomName = roomDto.RoomName,
                ChatRoomDisplayName = roomDto.DisplayRoomName,
            };
        }
    }
}