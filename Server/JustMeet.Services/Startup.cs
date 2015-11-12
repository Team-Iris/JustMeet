using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Data;
using System.Data.Entity;
using JustMeet.Data.Migrations;

[assembly: OwinStartup(typeof(JustMeet.Services.Startup))]

namespace JustMeet.Services
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Data.JustMeetDbContext, Configuration>());
        }
    }
}
