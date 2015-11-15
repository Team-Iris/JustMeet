namespace JustMeet.Data
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using Models;

    public interface IJustMeetDbContext
    {
        IDbSet<User> Users { get; set; }

        IDbSet<Conversation> Conversations { get; set; }

        IDbSet<Friendship> Friendships { get; set; }

        IDbSet<Image> Images { get; set; }

        int SaveChanges();

        void Dispose();

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}
