namespace JustMeet.Services.Data.Contracts
{
    using System.Linq;
    using Models;

    public interface IFriendshipsService
    {
        IQueryable<Friendship> All(string userId);

        void Add(string firstUserId, string secondUserId);
    }
}