﻿using PopeyeClub.Data;
using PopeyeClub.Repositories.Interfaces;
using PopeyeClub.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace PopeyeClub.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository postRepository;

        public PostService(IPostRepository postRepository)
        {
            this.postRepository = postRepository;
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

        public List<Post> GetAll()
        {
            return postRepository.GetAll();
        }
    }
}
