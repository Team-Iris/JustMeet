namespace JustMeet.Services.Data
{
    using System;
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
                 .All()
                 .OrderByDescending(p => p.DateOfBirth);
        }

        public IQueryable<User> All(int page = 1, int pageSize = 3)
        {
            return this.users
                 .All()
                 .OrderByDescending(p => p.DateOfBirth)
                 .Skip((page - 1) * pageSize)
                 .Take(pageSize);
        }

        public IQueryable<User> BySexAndAge(bool sex, int ageStart = 18, int ageEnd = 100) // TODO: Constants
        {
            return this.users
                 .All()
                 .Where(
                    u => u.IsMale == sex &&
                    (DateTime.Now.Year - u.DateOfBirth.Year) >= ageStart &&
                    (DateTime.Now.Year - u.DateOfBirth.Year) < ageEnd);
        }

        public IQueryable<User> Select(string email)
        {
            return this.users
                 .All()
                 .Where(u => u.Email == email);
        }

        public string Update(string username, string firstName, string lastName, DateTime birthday, bool isMale, string description)
        {
            var currentUser = this.users
                                        .All()
                                        .Where(u => u.Email == username)
                                        .FirstOrDefault();

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

            return string.Format("User {0} updated successfully!", username);
        }


        public string Delete(string username)
        {
            var userToDelete = this.users
                                       .All()
                                       .Where(u => u.Email == username)
                                       .FirstOrDefault();

            this.users.Delete(userToDelete);
            this.users.SaveChanges();

            return string.Format("User deleted successfully!");
        }
    }
}
