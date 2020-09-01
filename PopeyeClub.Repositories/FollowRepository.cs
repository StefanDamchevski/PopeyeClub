using PopeyeClub.Data;
using PopeyeClub.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace PopeyeClub.Repositories
{
    public class FollowRepository : IFollowRepository
    {
        private readonly ApplicationDbContext context;

        public FollowRepository(ApplicationDbContext context )
        {
            this.context = context;
        }

        public void Create(Follow follow)
        {
            context.Follows.Add(follow);
            context.SaveChanges();
        }

        public int GetAllFollowers(string userId)
        {
            return context.Follows.Where(x => x.FromUserId.Equals(userId) && x.IsFollowed.Equals(true)).Count();
        }

        public int GetAllFollowing(string userId)
        {
            return context.Follows.Where(x => x.ToUserId.Equals(userId) && x.IsFollowed.Equals(true)).Count();
        }

        public List<Follow> GetByIds(string userId)
        {
            return context.Follows.Where(x => x.FromUserId.Equals(userId) && x.IsFollowed.Equals(true)).ToList();
        }

        public Follow GetRelation(string currentUserId, string userId)
        {
            return context.Follows.FirstOrDefault(x => x.FromUserId.Equals(currentUserId) && x.ToUserId.Equals(userId));
        }

        public void Update(Follow follow)
        {
            context.Follows.Update(follow);
            context.SaveChanges();
        }
    }
}
