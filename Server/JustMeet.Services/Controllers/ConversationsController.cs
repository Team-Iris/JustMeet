namespace JustMeet.Services.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Http;
    using System.Web.Http.Cors;

    using Providers;
    using Models.Conversation;
    using JustMeet.Data;
    using JustMeet.Models;

    [EnableCors("*", "*", "*")]
    public class ConversationsController : ApiController
    {
        private JustMeetDbContext data;

        public ConversationsController(JustMeetDbContext data)
        {
            this.data = data;
        }

        [HttpPost]
        public IHttpActionResult Post(ConversationRequestModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var topic = this.data.Conversations.Select(x => x.Topic).FirstOrDefault();
            var text = this.data.Conversations.Select(x => x.Text).FirstOrDefault();
            var startedOn = this.data.Conversations.Select(x => x.StartedOn).FirstOrDefault();
            var currentUserName = this.User.Identity.Name;
            var currentUser = this.data.Users.Where(u => u.UserName == currentUserName).FirstOrDefault();

            var conversationToAdd = new Conversation
            {
                Topic = model.Topic,
                Text = model.Text,
                StartedOn = model.StartedOn,
                Author = currentUser
            };

            this.data.Conversations.Add(conversationToAdd);
            this.data.SaveChanges();

            PubNubNotificationProvider.Notify(conversationToAdd.Topic);

            return this.Ok(conversationToAdd.Id);
        }
    }
}