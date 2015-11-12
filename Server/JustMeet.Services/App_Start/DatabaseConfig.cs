namespace JustMeet.Services
{
    using System.Data.Entity;
    using Data;
    using Data.Migrations;

    public static class DatabaseConfig
    {
        public static void Initialize()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<JustMeetDbContext, Configuration>());

            JustMeetDbContext.Create().Database.Initialize(true);
        }
    }
}