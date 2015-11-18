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
             .Where(f => (f.FirstUserId == firstUserId && f.SecondUserId == secondUserId) || (f.SecondUserId == firstUserId && f.FirstUserId == secondUserId))
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

        public bool Delete(string firstUserId, string secondUserId)
        {
            var isDeleted = false;
            var friendship = this.friendships
             .All()
             .Where(f => (f.FirstUserId == firstUserId && f.SecondUserId == secondUserId) || (f.SecondUserId == firstUserId && f.FirstUserId == secondUserId))
             .FirstOrDefault();

            if (friendship != null)
            {
                this.friendships.Delete(friendship);
                this.friendships.SaveChanges();
                isDeleted = true;
            }

            return isDeleted;
        }

        public IQueryable<Friendship> All(string userId)
        {
            return this.friendships
             .All()
             .Where(f => f.FirstUserId == userId || f.SecondUserId == userId)
             .OrderByDescending(f => f.IsApproved);
        }

        public bool Approve(string firstUserId, string secondUserId)
        {
            var successfullyApproved = false;
            var friendship = this.friendships
                .All()
                .Where(f => f.FirstUserId == firstUserId && f.SecondUserId == secondUserId)
                .FirstOrDefault();

            if (friendship != null)
            {
                friendship.IsApproved = true;
                successfullyApproved = true;
            }

            this.friendships.SaveChanges();
            return successfullyApproved;
        }
    }
}
