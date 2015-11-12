namespace JustMeet.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class User : IdentityUser
    {
        private ICollection<Image> images;
        private ICollection<Conversation> conversations;

        public User()
        {
            this.images = new HashSet<Image>();
            this.conversations = new HashSet<Conversation>();
        }

        ////public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(25)]
        public string FirstName { get; set; }

        [MinLength(3)]
        [MaxLength(25)]
        public string LastName { get; set; }

        ////[Required]
        ////[MinLength(3)]
        ////[MaxLength(25)]
        ////public string Username { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
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

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager, string authenticationType)
        {
            //// Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            //// Add custom user claims here
            return userIdentity;
        }
    }
}