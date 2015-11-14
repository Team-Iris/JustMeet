namespace JustMeet.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using JustMeet.Models;

    public sealed class Configuration : DbMigrationsConfiguration<JustMeet.Data.JustMeetDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(JustMeet.Data.JustMeetDbContext context)
        {
            ////  This method will be called after migrating to the latest version.

            ////  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            ////  to avoid creating duplicate seed data. E.g.
            ////
            ////    context.People.AddOrUpdate(
            ////      p => p.FullName,
            ////      new Person { FullName = "Andrew Peters" },
            ////      new Person { FullName = "Brice Lambson" },
            ////      new Person { FullName = "Rowan Miller" }
            ////    );
            ////
            context.Users.AddOrUpdate(
                u => u.FirstName, new User
                {
                    FirstName = "Pesho",
                    LastName = "Peshev",
                    DateOfBirth = new DateTime(1988, 02, 11),
                    IsMale = true,
                    IsOnline = true,
                    LastOnline = DateTime.Now,
                    UserName = "pesho@gmail.com",
                    Email = "pesho@gmail.com",
                    PasswordHash = "APrEJA5givNTsrcVR/0k/Hcvc6axh9VwTN7IVkE/HFsVCILo7Z4ue0vzbF7H/V5mhA==",
                    SecurityStamp = "8771ec8e-a3c8-4504-8e2e-966c023dbeb7"
                },
                new User
                {
                    FirstName = "Gosho",
                    LastName = "Goshev",
                    DateOfBirth = new DateTime(1989, 03, 12),
                    IsMale = true,
                    IsOnline = true,
                    LastOnline = DateTime.Now,
                    UserName = "gosho@gmail.com",
                    Email = "gosho@gmail.com",
                    PasswordHash = "APrEJA5givNTsrcVR/0k/Hcvc6axh9VwTN7IVkE/HFsVCILo7Z4ue0vzbF7H/V5mhA==",
                    SecurityStamp = "8771ec8e-a3c8-4504-8e2e-966c023dbeb7"
                },
                new User
                {
                    FirstName = "Mariika",
                    LastName = "Mircheva",
                    DateOfBirth = new DateTime(1994, 10, 22),
                    IsMale = false,
                    IsOnline = false,
                    LastOnline = new DateTime(2015, 11, 10, 12, 22, 12),
                    UserName = "mimeto@gmail.com",
                    Email = "mimeto@gmail.com",
                    PasswordHash = "APrEJA5givNTsrcVR/0k/Hcvc6axh9VwTN7IVkE/HFsVCILo7Z4ue0vzbF7H/V5mhA==",
                    SecurityStamp = "8771ec8e-a3c8-4504-8e2e-966c023dbeb7"
                },
                new User
                {
                    FirstName = "Ani",
                    LastName = "Nicheva",
                    DateOfBirth = new DateTime(1990, 10, 02),
                    IsMale = false,
                    IsOnline = true,
                    LastOnline = DateTime.Now,
                    UserName = "ani@gmail.com",
                    Email = "ani@gmail.com",
                    PasswordHash = "APrEJA5givNTsrcVR/0k/Hcvc6axh9VwTN7IVkE/HFsVCILo7Z4ue0vzbF7H/V5mhA==",
                    SecurityStamp = "8771ec8e-a3c8-4504-8e2e-966c023dbeb7"
                },
                new User
                {
                    FirstName = "Pesho",
                    LastName = "Ivanov",
                    DateOfBirth = new DateTime(1999, 05, 22),
                    IsMale = true,
                    IsOnline = true,
                    LastOnline = DateTime.Now,
                    UserName = "theOriginal@abv.bg",
                    Email = "theOriginal@abv.bg",
                    PasswordHash = "APrEJA5givNTsrcVR/0k/Hcvc6axh9VwTN7IVkE/HFsVCILo7Z4ue0vzbF7H/V5mhA==",
                    SecurityStamp = "8771ec8e-a3c8-4504-8e2e-966c023dbeb7"
                },
                new User
                {
                    FirstName = "Nina",
                    LastName = "Pesheva",
                    DateOfBirth = new DateTime(1986, 01, 05),
                    IsMale = false,
                    IsOnline = true,
                    LastOnline = DateTime.Now,
                    UserName = "ninka@idontcare.com",
                    Email = "ninka@idontcare.com",
                    PasswordHash = "APrEJA5givNTsrcVR/0k/Hcvc6axh9VwTN7IVkE/HFsVCILo7Z4ue0vzbF7H/V5mhA==",
                    SecurityStamp = "8771ec8e-a3c8-4504-8e2e-966c023dbeb7"
                },
                new User
                {
                    FirstName = "Stamat",
                    LastName = "Goshev",
                    DateOfBirth = new DateTime(1988, 07, 12),
                    IsMale = true,
                    IsOnline = false,
                    LastOnline = new DateTime(2015, 11, 02, 01, 11, 23),
                    UserName = "stamat@stamat.org",
                    Email = "stamat@stamat.org",
                    PasswordHash = "APrEJA5givNTsrcVR/0k/Hcvc6axh9VwTN7IVkE/HFsVCILo7Z4ue0vzbF7H/V5mhA==",
                    SecurityStamp = "8771ec8e-a3c8-4504-8e2e-966c023dbeb7"
                },
                new User
                {
                    FirstName = "Ginchety",
                    LastName = "Gineva",
                    DateOfBirth = new DateTime(1993, 09, 12),
                    IsMale = false,
                    IsOnline = true,
                    LastOnline = DateTime.Now,
                    UserName = "ginka@facebook.com",
                    Email = "ginka@facebook.com",
                    PasswordHash = "APrEJA5givNTsrcVR/0k/Hcvc6axh9VwTN7IVkE/HFsVCILo7Z4ue0vzbF7H/V5mhA==",
                    SecurityStamp = "8771ec8e-a3c8-4504-8e2e-966c023dbeb7"
                },
                new User
                {
                    FirstName = "Anonimen",
                    LastName = "Tayna",
                    DateOfBirth = new DateTime(1889, 03, 12),
                    IsMale = true,
                    IsOnline = true,
                    LastOnline = DateTime.Now,
                    UserName = "lovemachine@pernik.com",
                    Email = "lovemachine@pernik.com",
                    PasswordHash = "APrEJA5givNTsrcVR/0k/Hcvc6axh9VwTN7IVkE/HFsVCILo7Z4ue0vzbF7H/V5mhA==",
                    SecurityStamp = "8771ec8e-a3c8-4504-8e2e-966c023dbeb7"
                },
                new User
                {
                    FirstName = "Linka",
                    LastName = "Gosheva",
                    DateOfBirth = new DateTime(1989, 12, 12),
                    IsMale = false,
                    IsOnline = true,
                    LastOnline = DateTime.Now,
                    UserName = "linka@linka.com",
                    Email = "linka@linka.com",
                    PasswordHash = "APrEJA5givNTsrcVR/0k/Hcvc6axh9VwTN7IVkE/HFsVCILo7Z4ue0vzbF7H/V5mhA==",
                    SecurityStamp = "8771ec8e-a3c8-4504-8e2e-966c023dbeb7"
                }
            );
        }
    }
}
