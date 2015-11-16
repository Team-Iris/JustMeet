namespace JustMeet.Services.Data.Contracts
{
    using System;
    using System.Linq;
    using Common.Constants;
    using Models;

    public interface IUsersService
    {
        IQueryable<User> All();

        IQueryable<User> All(int page = UsersConstants.Page, int pageSize = UsersConstants.PageSize);

        IQueryable<User> BySexAndAge(bool sex, int ageStart = UsersConstants.AgeStart, int ageEnd = UsersConstants.AgeEnd);

        IQueryable<User> Select(string email);

        string Update(string username, string firstName, string lastName, DateTime birthday, bool isMale, string description);

        string Delete(string username);

        User FindUserById(string id);
    }
}
