namespace JustMeet.Services.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Cors;
    using AutoMapper.QueryableExtensions;
    using Common.Constants;
    using Data.Contracts;
    using Microsoft.AspNet.Identity;
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
            var currentUserId = this.User.Identity.GetUserId();

            if (this.users.FindUserById(currentUserId) == null)
            {
                return this.NotFound();
            }

            var result = this.users
                .Select(currentUserId)
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

            var currentUserId = this.User.Identity.GetUserId();

            var updatedProfileId = this.users.Update(currentUserId, model.FirstName, model.LastName, model.DateOfBirth, model.IsMale, model.Description);

            return this.Ok(updatedProfileId);
        }

        [Authorize]
        [Route("api/users/profile")]
        public IHttpActionResult Delete()
        {
            var currentUserId = this.User.Identity.GetUserId();

            if (this.users.FindUserById(currentUserId) == null)
            {
                return this.NotFound();
            }

            var result = this.users.Delete(currentUserId);

            return this.Ok(result);
        }

        [Authorize]
        [Route("api/users/all")]
        public IHttpActionResult Get(int page = UsersConstants.Page, int pageSize = UsersConstants.PageSize)
        {
            var result = this.users
                .All(page, pageSize)
                .ProjectTo<UserDetailsResponseModel>()
                .ToList();

            return this.Ok(result);
        }

        [Authorize]
        [Route("api/users/search")]
        public IHttpActionResult Get(bool sex, int ageStart = UsersConstants.AgeStart, int ageEnd = UsersConstants.AgeEnd)
        {
            var result = this.users
                .BySexAndAge(sex, ageStart, ageEnd)
                .ProjectTo<UserDetailsResponseModel>()
                .ToList();

            return this.Ok(result);
        }
    }
}