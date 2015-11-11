namespace JustMeet.Models
{
    using System;
    using System.Collections.Generic;

    public class Friendship
    {
        private ICollection<Conversation> conversations;

        public Friendship()
        {
            this.conversations = new HashSet<Conversation>();
        }

        public int Id { get; set; }

        public int FirstUserId { get; set; }

        public User FirstUser { get; set; }

        public int SecondUserId { get; set; }

        public User SecondUser { get; set; }

        public bool IsApproved { get; set; }

        public virtual ICollection<Conversation> Conversations
        {
            get { return this.conversations; }
            set { this.conversations = value; }
        }
    }
}
