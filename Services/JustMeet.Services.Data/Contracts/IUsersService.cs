namespace JustMeet.Services.Data.Contracts
{
    using System.Linq;
    using JustMeet.Models;

    public interface IUsersService
    {
        IQueryable<User> All();
    }
}
