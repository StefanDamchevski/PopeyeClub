﻿using PopeyeClub.Data;
using System.Collections.Generic;

namespace PopeyeClub.Repositories.Interfaces
{
    public interface IPostRepository
    {
        List<Post> GetAll();
        void Create(Post post);
        Post GetById(int postId);
        List<Post> GetOtherPosts(int postId, string userId);
        List<Post> GetByIds(List<int> postIds);
    }
}
