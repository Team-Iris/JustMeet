namespace JustMeet.Data
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;

    public class JustMeetDbContext : IdentityDbContext<User>, IJustMeetDbContext
    {
        public JustMeetDbContext() 
            : base("JustMeet")
        {
        }

        public virtual IDbSet<Conversation> Conversations { get; set; }

        public virtual IDbSet<Friendship> Friendships { get; set; }

        public virtual IDbSet<Image> Images { get; set; }

        public static JustMeetDbContext Create()
        {
            return new JustMeetDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}