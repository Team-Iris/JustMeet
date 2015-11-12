namespace JustMeet.Data
{
    using System.Data.Entity;

    using JustMeet.Models;

    public class JustMeetDbContext : DbContext
    {
        public JustMeetDbContext()
        : base("JustMeet")
        {
        }

        public virtual IDbSet<User> Users { get; set; }

        public virtual IDbSet<Conversation> Conversations { get; set; }

        public virtual IDbSet<Friendship> Friendships { get; set; }

        public virtual IDbSet<Image> Images { get; set; }
    }
}