namespace JustMeet.Services.Data
{
    using System;
    using System.Linq;
    using Common.Constants;
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
                 .All()
                 .OrderByDescending(p => p.DateOfBirth);
        }

        public IQueryable<User> All(int page = UsersConstants.Page, int pageSize = UsersConstants.PageSize)
        {
            return this.users
                 .All()
                 .OrderByDescending(p => p.DateOfBirth)
                 .Skip((page - 1) * pageSize)
                 .Take(pageSize);
        }

        public IQueryable<User> BySexAndAge(bool sex, int ageStart = UsersConstants.AgeStart, int ageEnd = UsersConstants.AgeEnd)
        {
            return this.users
                 .All()
                 .Where(
                    u => u.IsMale == sex &&
                    (DateTime.Now.Year - u.DateOfBirth.Year) >= ageStart &&
                    (DateTime.Now.Year - u.DateOfBirth.Year) < ageEnd);
        }

        public IQueryable<User> Select(string id)
        {
            return this.users
                 .All()
                 .Where(u => u.Id == id);
        }

        public string Update(string userId, string firstName, string lastName, DateTime birthday, bool isMale, string description)
        {
            var currentUser = this.users.GetById(userId);

            currentUser.FirstName = firstName;
            currentUser.DateOfBirth = birthday;
            currentUser.IsMale = isMale;

            if (lastName != null)
            {
                currentUser.LastName = lastName;
            }

            if (description != null)
            {
                currentUser.Description = description;
            }

            this.users.SaveChanges();

            return string.Format("User {0} updated successfully!", currentUser.UserName);
        }

        public string Delete(string id)
        {
            this.users.Delete(id);
            this.users.SaveChanges();

            return string.Format("User deleted successfully!");
        }

        public User FindUserById(string id)
        {
            return this.users.GetById(id);
        }

        public User FindUserByEmail(string email)
        {
            return this.users.All().Where(u => u.Email == email).FirstOrDefault();
        }
    }
}
