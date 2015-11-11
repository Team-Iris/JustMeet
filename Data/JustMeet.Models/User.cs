namespace JustMeet.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class User
    {
        private ICollection<Image> images;
        private ICollection<Conversation> conversations;

        public User()
        {
            this.images = new HashSet<Image>();
            this.conversations = new HashSet<Conversation>();
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public DateTime DateOfBirth { get; set; }

        public bool IsMale { get; set; }

        public string Description { get; set; }

        public bool IsOnline { get; set; }

        public DateTime LastOnline { get; set; }

        public virtual ICollection<Image> Images
        {
            get { return this.images; }
            set { this.images = value; }
        }

        public virtual ICollection<Conversation> Conversations
        {
            get { return this.conversations; }
            set { this.conversations = value; }
        }
    }
}