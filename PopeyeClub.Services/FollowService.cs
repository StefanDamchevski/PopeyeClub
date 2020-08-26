﻿using PopeyeClub.Data;
using PopeyeClub.Repositories.Interfaces;
using PopeyeClub.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PopeyeClub.Services
{
    public class FollowService : IFollowService
    {
        private readonly IFollowRepository followRepository;
        private readonly IUserService userService;
        private readonly INotificationService notificationService;

        public FollowService(IFollowRepository followRepository, IUserService userService, INotificationService notificationService)
        {
            this.followRepository = followRepository;
            this.userService = userService;
            this.notificationService = notificationService;
        }

        public async Task Create(string currentUserId, string userId)
        {
            ApplicationUser user = await userService.GetByIdAsync(userId);

            Follow dbFollow = followRepository.GetRelation(currentUserId, userId);

            if(user != null && dbFollow is null)
            {
                Follow follow = new Follow()
                {
                    FromUserId = currentUserId,
                    ToUserId = userId,
                };

                if (user.IsPrivate)
                {
                    follow.IsFollowed = false;
                    follow.IsSent = true;
                }
                else
                {
                    follow.IsFollowed = true;
                    follow.IsSent = true;
                }

                notificationService.Create(currentUserId, userId, "Follow");
                followRepository.Create(follow);
            }
        }

        public List<string> GetIds(string userId)
        {
            List<Follow> follows = followRepository.GetByIds(userId);

            List<string> ids = new List<string>();

            foreach (Follow follow in follows)
            {
                ids.Add(follow.ToUserId);
            }

            return ids;
        }

        public bool GetIsFollowed(string currentUserId, string userId)
        {
            Follow follow = followRepository.GetRelation(currentUserId, userId);

            if (follow != null)
            {
                return follow.IsFollowed;
            }

            return false;
        }

        public bool GetIsSent(string currentUserId, string userId)
        {
            Follow follow = followRepository.GetRelation(currentUserId, userId);

            if(follow != null)
            {
                return follow.IsSent;
            }
            return false;
        }

        public void Update(string currentUserId, string userId, bool isFollowed, bool isSent)
        {
            Follow follow = followRepository.GetRelation(currentUserId, userId);

            if(follow != null)
            {
                follow.IsSent = isSent;
                follow.IsFollowed = isFollowed;

                followRepository.Update(follow);
            }
        }
    }
}