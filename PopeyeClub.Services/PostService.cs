using System;
using System.Collections.Generic;
using PopeyeClub.Data;
using PopeyeClub.Repositories.Interfaces;
using PopeyeClub.Services.Interfaces;

namespace PopeyeClub.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository postRepository;
        private readonly IFollowService followService;

        public PostService(IPostRepository postRepository, IFollowService followService)
        {
            this.postRepository = postRepository;
            this.followService = followService;
        }

        public void Create(string userId, byte[] postImage)
        {
            Post post = new Post
            {
                UserId = userId,
                PostImage = postImage,
                DateCreated = DateTime.Now,
            };

            postRepository.Create(post);
        }

        public void Delete(int postId)
        {
            postRepository.Delete(postId);
        }

        public List<Post> GetAll(string userId)
        {
            List<string> followIds = followService.GetIds(userId);
            return postRepository.GetAll(followIds, userId);
        }

        public Post GetById(int postId)
        {
            return postRepository.GetById(postId);
        }

        public List<Post> GetByIds(List<int> postIds)
        {
            return postRepository.GetByIds(postIds);
        }

        public List<Post> GetOtherPosts(int postId,string userId)
        {
            return postRepository.GetOtherPosts(postId,userId);
        }

        public Post GetPost(int postId)
        {
            return postRepository.GetPost(postId);
        }
    }
}
