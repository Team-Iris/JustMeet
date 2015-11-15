namespace JustMeet.Services.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Cors;
    using AutoMapper.QueryableExtensions;
    using Data.Contracts;
    using Models.User;

    public class UsersController : ApiController
    {
        private readonly IUsersService users;

        public UsersController(IUsersService usersService)
        {
            this.users = usersService;
        }

        [EnableCors("*", "*", "*")]
        public IHttpActionResult Get()
        {
            var result = this.users
                .All()
                .ProjectTo<UserDetailsResponseModel>()
                .ToList();

            return this.Ok(result);
        }

        [Authorize]
        [Route("api/users/all")]
        public IHttpActionResult Get(int page = 1, int pageSize = 3)
        {
            var result = this.users
                .All(page, pageSize)
                .ProjectTo<UserDetailsResponseModel>()
                .ToList();

            return this.Ok(result);
        }

        [Authorize]
        [Route("api/users/search")]
        public IHttpActionResult Get(bool sex, int ageStart = 18, int ageEnd = 100) //TODO: constants
        {
            var result = this.users
                .BySexAndAge(sex, ageStart, ageEnd)
                .ProjectTo<UserDetailsResponseModel>()
                .ToList();

            return this.Ok(result);
        }

        [Authorize]
        [Route("api/users/profile")]
        public IHttpActionResult Get(string email) //TODO: constants
        {
            var result = this.users
                .Select(email)
                .ProjectTo<UserDetailsResponseModel>()
                .FirstOrDefault();

            return this.Ok(result);
        }
    }
}