namespace JustMeet.Services.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Cors;
    using AutoMapper.QueryableExtensions;
    using Data.Contracts;
    using Models.User;

    [EnableCors("*", "*", "*")]
    public class UsersController : ApiController
    {
        private readonly IUsersService users;

        public UsersController(IUsersService usersService)
        {
            this.users = usersService;
        }

        [Authorize]
        [Route("api/users/profile")]
        public IHttpActionResult Get()
        {
            var currentUser = this.User.Identity.Name;

            var result = this.users
                .Select(currentUser)
                .ProjectTo<UserDetailsResponseModel>()
                .FirstOrDefault();

            return this.Ok(result);
        }

        [Authorize]
        [Route("api/users/profile")]
        public IHttpActionResult Post(UserUpdateRequestModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var currentUser = this.User.Identity.Name;

            var updatedProfileId = this.users.Update(currentUser, model.FirstName, model.LastName, model.DateOfBirth, model.IsMale, model.Description);

            return this.Ok(updatedProfileId);
        }

        [Authorize]
        [Route("api/users/profile")]
        public IHttpActionResult Delete()
        {
            var currentUser = this.User.Identity.Name;

            var result = this.users.Delete(currentUser);

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
    }
}