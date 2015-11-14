namespace JustMeet.Services.Controllers
{
    using System.Web.Http;
    using System.Web.Http.Cors;
    using Data.Contracts;
    using System.Linq;

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
                .ToList();

            return this.Ok(result);
        }
    }
}