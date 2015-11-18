namespace JustMeet.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Conversation
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Topic { get; set; }

        [Required]
        [MaxLength(300)]
        public string Text { get; set; }

        public DateTime StartedOn { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        public int FriendshipId { get; set; }

        public virtual Friendship Friendship { get; set; }
    }
}