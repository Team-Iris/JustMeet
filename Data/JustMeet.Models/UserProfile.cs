namespace JustMeet.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class UserProfile
    {
        private ICollection<Image> images;
        private ICollection<Conversation> conversations;

        public UserProfile()
        {
            this.images = new HashSet<Image>();
            this.conversations = new HashSet<Conversation>();
        }

        [Key, ForeignKey("User")]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(25)]
        public string FirstName { get; set; }

        [MinLength(3)]
        [MaxLength(25)]
        public string LastName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public bool IsMale { get; set; }

        public string Description { get; set; }

        public bool IsOnline { get; set; }

        public DateTime? LastOnline { get; set; }

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
