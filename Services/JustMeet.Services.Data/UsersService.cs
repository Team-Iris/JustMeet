namespace JustMeet.Services.Data
{
    using System.Linq;
    using Contracts;
    using JustMeet.Data;
    using Models;

    public class UsersService : IUsersService
    {
        private readonly IRepository<User> users;

        public UsersService(IRepository<User> usersRepo)
        {
            this.users = usersRepo;
        }

        public IQueryable<User> All()
        {
            return this.users
                 .All();
        }
    }
}
