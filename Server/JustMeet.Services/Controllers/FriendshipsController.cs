namespace JustMeet.Services.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Http;
    using System.Web.Http.Cors;
    using Data.Contracts;
    using Models.Friendship;
    using Microsoft.AspNet.Identity;

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
                    FirstName = u.SecondUser.FirstName,
                    SecondName = u.SecondUser.LastName,
                    IsApproved = u.IsApproved
                })
                .ToList();

            if (result == null)
            {
                return this.NotFound();
            }

            return this.Ok(result);
        }

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
    }
}