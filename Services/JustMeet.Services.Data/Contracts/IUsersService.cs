namespace JustMeet.Services.Data.Contracts
{
    using System.Linq;
    using Models;

    public interface IUsersService
    {
        IQueryable<User> All();

        IQueryable<User> All(int page = 1, int pageSize = 3); // TODO: Constants

        IQueryable<User> BySexAndAge(bool sex, int ageStart = 18, int ageEnd = 100);

        IQueryable<User> Select(string email);
    }
}
