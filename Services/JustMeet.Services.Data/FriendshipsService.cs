namespace JustMeet.Services.Data
{
    using System;
    using System.Linq;
    using Contracts;
    using JustMeet.Data;
    using Models;

    public class FriendshipsService : IFriendshipsService
    {
        private readonly IRepository<Friendship> friendships;
        private readonly IRepository<User> users;

        public FriendshipsService(IRepository<Friendship> friendshipsRepo, IRepository<User> usersRepo)
        {
            this.friendships = friendshipsRepo;
            this.users = usersRepo;
        }

        public void Add(string firstUserId, string secondUserId)
        {
            var friendshipExists = this.friendships
             .All()
             .Where(f => (f.FirstUserId == firstUserId && f.SecondUserId == secondUserId)||(f.SecondUserId == firstUserId && f.FirstUserId == secondUserId))
             .ToList();

            if (friendshipExists.Count > 0)
            {
                return;
            }

            this.friendships.Add(new Friendship()
            {
                IsApproved = false,
                FirstUserId = firstUserId,
                SecondUserId = secondUserId
            });

            this.friendships.SaveChanges();
        }

        public IQueryable<Friendship> All(string userId)
        {
            return this.friendships
             .All()
             .Where(f => f.FirstUserId == userId)
             .OrderByDescending(f => f.IsApproved);
        }
    }
}
