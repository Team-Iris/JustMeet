namespace JustMeet.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Friendship
    {
        private ICollection<Conversation> conversations;

        public Friendship()
        {
            this.conversations = new HashSet<Conversation>();
        }

        public int Id { get; set; }

        [Required]
        public string FirstUserId { get; set; }

        public User FirstUser { get; set; }

        [Required]
        public string SecondUserId { get; set; }

        public User SecondUser { get; set; }

        public bool IsApproved { get; set; }

        public virtual ICollection<Conversation> Conversations
        {
            get { return this.conversations; }
            set { this.conversations = value; }
        }
    }
}
