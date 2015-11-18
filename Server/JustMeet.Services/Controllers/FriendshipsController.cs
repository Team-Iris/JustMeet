namespace JustMeet.Services.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Cors;
    using Data.Contracts;
    using Microsoft.AspNet.Identity;
    using Models.Friendship;

    [Authorize]
    [EnableCors("*", "*", "*")]
    public class FriendshipsController : ApiController
    {
        private readonly IFriendshipsService friendships;
        private readonly IUsersService users;

        public FriendshipsController(IFriendshipsService friendshipsService, IUsersService usersService)
        {
            this.friendships = friendshipsService;
            this.users = usersService;
        }

        [Route("api/friendships/all")]
        public IHttpActionResult Get()
        {
            string currentUserId = this.User.Identity.GetUserId();
            var result = this.friendships
                .All(currentUserId)
                .Select(u => new FriendshipResponseModel
                {
                    FirstName = u.FirstUser.Id == currentUserId ? u.SecondUser.FirstName : u.FirstUser.FirstName,
                    SecondName = u.FirstUser.Id == currentUserId ? u.SecondUser.LastName : u.FirstUser.LastName,
                    Email = u.FirstUser.Id == currentUserId ? u.SecondUser.Email : u.FirstUser.Email,
                    IsApproved = u.IsApproved
                })
                .ToList();

            if (result == null)
            {
                return this.NotFound();
            }

            return this.Ok(result);
        }

        [Route("api/friendships/request")]
        public IHttpActionResult Post(string email)
        {
            string currentUserId = this.User.Identity.GetUserId();
            string secondUserId = this.users.FindUserByEmail(email).Id;

            if (string.IsNullOrEmpty(secondUserId))
            {
                return this.BadRequest();
            }

            this.friendships.Add(currentUserId, secondUserId);

            return this.Ok();
        }

        // Approve friendship
        [Route("api/friendships/request")]
        public IHttpActionResult Put(string email)
        {
            string currentUserId = this.User.Identity.GetUserId();
            string requestingUserId = this.users.FindUserByEmail(email).Id;

            if (string.IsNullOrEmpty(requestingUserId))
            {
                return this.BadRequest();
            }

            var approved = this.friendships.Approve(requestingUserId, currentUserId);

            if (approved)
            {
                return this.Ok();
            }

            return this.NotFound();
        }

        // Delete friendship
        [Route("api/friendships/request")]
        public IHttpActionResult Delete(string email)
        {
            string currentUserId = this.User.Identity.GetUserId();
            string otherUserId = this.users.FindUserByEmail(email).Id;

            var result = this.friendships.Delete(currentUserId, otherUserId);

            if (result)
            {
                return this.Ok();
            }

            return this.NotFound();
        }
    }
}