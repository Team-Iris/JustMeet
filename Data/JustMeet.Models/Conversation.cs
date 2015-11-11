namespace JustMeet.Models
{
    using System;

    public class Conversation
    {
        public int Id { get; set; }

        public string Topic { get; set; }

        public string Text { get; set; }

        public DateTime StartedOn { get; set; }

        public int AuthorId { get; set; }

        public virtual User Author { get; set; }

        public int FriendshipId { get; set; }

        public virtual Friendship Friendship { get; set; }
    }
}