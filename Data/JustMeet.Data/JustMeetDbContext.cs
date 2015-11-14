namespace JustMeet.Data
{
    using System.Data.Entity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;

    public class JustMeetDbContext : IdentityDbContext<User>, IJustMeetDbContext
    {
        public JustMeetDbContext() : base("JustMeet")
        {
        }

        public virtual IDbSet<Conversation> Conversations { get; set; }

        public virtual IDbSet<Friendship> Friendships { get; set; }

        public virtual IDbSet<Image> Images { get; set; }

        public static JustMeetDbContext Create()
        {
            return new JustMeetDbContext();
        }
    }
}